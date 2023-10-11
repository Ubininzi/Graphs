using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Graphs
{
	class Graph
	{
		internal Dictionary<Vertex, List<(Vertex, double)>> AdjacencyList = new();
		internal bool wheighted;
		internal bool oriented;
		public Graph(Dictionary<Vertex, List<(Vertex, double)>> vertices, bool wheighted, bool oriented)
		{
			this.AdjacencyList = vertices;
			this.wheighted = wheighted;
			this.oriented = oriented;
		}
		public Graph(Graph graph)
		{
			foreach (var item in graph.AdjacencyList) {
				AddVertex(new Vertex(item.Key.Id));
			}
			foreach (var value in graph.AdjacencyList)
			{
				foreach (var item in value.Value)
				{
					AddEdge(GetVertexById(value.Key.Id), GetVertexById(item.Item1.Id),item.Item2);
				}
			}
			this.oriented = graph.oriented;
			this.wheighted = graph.wheighted;
		}
		public Graph()
		{
			AdjacencyList = new Dictionary<Vertex, List<(Vertex, double)>>();
			wheighted = false;
			oriented = false;
		}
		public Graph(bool wheighted , bool oriented)
		{
			AdjacencyList = new Dictionary<Vertex, List<(Vertex, double)>>();
			wheighted = false;
			oriented = false;
		}
		public Graph(string path)
		{
			double[][] AdjacencyMatrix = File.ReadAllLines(path).Select(str => str.Split(",").Select(c => Convert.ToDouble(c)).ToArray()).ToArray();
			for (int i = 0; i < AdjacencyMatrix.Length; i++) { 
				AddVertex(new Vertex($"V{i + 1}"));
			}
			for (int i = 0; i < AdjacencyMatrix.Length; i++)
			{
				for (int j = 0; j < AdjacencyMatrix.Length; j++)
				{
					if (AdjacencyMatrix[i][j] > 0) AdjacencyList[((GetVertexById($"V{i + 1}")))].Add(((GetVertexById($"V{j + 1}")), (AdjacencyMatrix[i][j]))); ;
					if (!wheighted && (AdjacencyMatrix[i][j] != 1 || AdjacencyMatrix[i][j] != -1)) wheighted = true;
					if (!oriented && AdjacencyMatrix[i][j] < 0) oriented = true;
				}
			}
		}
		public static Graph CompleteGraph(Graph graph) {
			Dictionary<Vertex, List<(Vertex, double)>> AdjList = new();
			foreach (var vertexK in graph.AdjacencyList.Keys)
			{
				AdjList.Add(vertexK, new List<(Vertex, double)>());
				foreach (var VertexV in graph.AdjacencyList.Keys)
				{
					if (vertexK != VertexV)	AdjList[vertexK].Add((VertexV, 1));
				}
			}
			return new Graph(AdjList,graph.wheighted,graph.oriented);
		}
		public static Graph ComplementGraph(Graph graph) {
			Dictionary<Vertex, List<(Vertex, double)>> AdjList = new();
			foreach (var vertexK in graph.AdjacencyList.Keys)
			{
				AdjList.Add(vertexK, new List<(Vertex, double)>());
				foreach (var VertexV in graph.AdjacencyList.Keys)
				{
					if (!graph.AdjacencyList[vertexK].Any(x => x.Item1 == VertexV) && VertexV != vertexK) AdjList[vertexK].Add((VertexV,1));
				}
			}
			return new Graph(AdjList, graph.wheighted, graph.oriented);
		}
		public static Graph? UnionGraph(Graph graph1, Graph graph2){	//используется проверка не через названия вершин а через экземпляры класса
			Dictionary<Vertex, List<(Vertex, double)>> AdjList = new();	//поэтому если вызывать два графа с одинаковыми названиями верщин но с разными экземплярами 
			if (graph1.AdjacencyList.Keys.Intersect(graph2.AdjacencyList.Keys).Any())   //проверка на уникальность не сработает(хорошо это или плохо?
				return null;                                                            //- ведь по сути вершины то разные хоть и называются одинаково)
			foreach (var item in graph1.AdjacencyList)
				AdjList.Add(item.Key,item.Value);
			foreach (var item in graph2.AdjacencyList)
				AdjList.Add(item.Key,item.Value);
			return new Graph(AdjList, false, false);
		}
		public static Graph? IntersectionGraph(Graph graph1, Graph graph2) {
			Dictionary<Vertex, List<(Vertex, double)>> AdjList = new();
			if (graph1.AdjacencyList.Keys.Intersect(graph2.AdjacencyList.Keys).Any())
				return null;
			foreach (var item in graph1.AdjacencyList) {
				AdjList.Add(item.Key, item.Value); }
			foreach (var item in graph2.AdjacencyList) {
				AdjList.Add(item.Key, item.Value); }
			Graph gr = new Graph(AdjList, false, false);
			foreach (var item1 in graph1.AdjacencyList)
			{
				foreach (var item2 in graph2.AdjacencyList)
				{
					gr.AddEdge(item1.Key,item2.Key);
				}
			}
			return gr;
		}
		internal void AddVertex(Vertex vertex)
		{
			AdjacencyList.Add(vertex, new List<(Vertex, double)>());
		}
		internal void RemoveVertex(Vertex vertex)
		{
			if (wheighted)
			{
				foreach (var vertices in AdjacencyList.Values)
				{
					try
					{
						vertices.Remove(vertices.First(x => (x.Item1 == vertex)));
					}
					catch { }
				}
			}
			else
			{
				foreach (var vertices in AdjacencyList.Values) vertices.Remove((vertex, 1));
			}
			AdjacencyList.Remove(vertex);
		}
		internal void AddEdge(Vertex vertex1, Vertex vertex2, double weight = 1)
		{
			if (!(AdjacencyList[vertex1].Contains((vertex2, weight)) && (AdjacencyList[vertex2].Contains((vertex1, weight)))))
			{
				if (vertex1 == vertex2 || oriented)
				{
					AdjacencyList[vertex1].Add((vertex2, weight));
				}
				else
				{
					AdjacencyList[vertex1].Add((vertex2, weight));
					AdjacencyList[vertex2].Add((vertex1, weight));
				}
			}
		}
		internal void RemoveEdge(Vertex vertex1, Vertex vertex2)
		{
			AdjacencyList[vertex1].Remove(AdjacencyList[vertex1].First(x => x.Item1 == vertex2));
			AdjacencyList[vertex2].Remove(AdjacencyList[vertex2].First(x => x.Item1 == vertex1));
		}
		internal double[][] CreateAdjacencyMatrix()
		{
			List<Vertex> vertexList = AdjacencyList.Keys.ToList();
			double[][] matrix = new double[AdjacencyList.Keys.Count][];
			foreach (Vertex v in vertexList) matrix[vertexList.IndexOf(v)] = new double[AdjacencyList.Keys.Count];
			//if (oriented){
			foreach (Vertex v in vertexList)
			{
				foreach (var adjVert in AdjacencyList[v])   // оптимизтровать для неорг графа(симметрия)
				{
					matrix[vertexList.IndexOf(v)][vertexList.IndexOf(adjVert.Item1)] = adjVert.Item2;
					if (oriented) matrix[vertexList.IndexOf(adjVert.Item1)][vertexList.IndexOf(v)] = -adjVert.Item2;
				}
			}
			//}else{
			//}
			return matrix;
		}
		internal Vertex GetVertexById(string id)
		{
			return AdjacencyList.Keys.First(x => x.Id == id);
		}
		internal bool IsVertexExists(string id)
		{
			try
			{
				GetVertexById(id);
			}
			catch (Exception)
			{
				return false;
			}

			return true;
		}
	}
	class Vertex
	{
		internal string Id;
		public Vertex(string Id)
		{
			this.Id = Id;
		}
	}
	class GraphUI {

		static public void UI(Graph graph)
		{
			Console.WriteLine(
				"1. Добавить вершину\n" +
				"2. Добавить ребро\n" +
				"3. Удалить вершину\n" +
				"4. Удалить ребро\n" +
				"5. Вывести список смежности\n" +
				"6. Вывести матрицу смежности\n"+
				"7. Сохранить список смежности в файл\n" +
				"8. Выход");
			string id;
			List<string> idList = new();
			while (true)
			{
				Console.WriteLine("выберете номер операции:");
				switch (Console.ReadLine())
				{
					case "1":
						Console.WriteLine("введите название новой вершины");
						id = Console.ReadLine();
						if (!graph.IsVertexExists(id)) graph.AddVertex(new Vertex(id));
						else Console.WriteLine("вершина уже существует");
						break;
					case "2":
						if (graph.wheighted)
						{
							Console.WriteLine("введите вершины, между которыми создается ребро и вес ребра(v1 v2 wheight)");
							idList = Console.ReadLine().Split(" ").ToList();
							graph.AddEdge(graph.GetVertexById(idList[0]), graph.GetVertexById(idList[1]), Convert.ToDouble(idList[2]));
						}
						else
						{
							Console.WriteLine("введите вершины, между которыми создается ребро(v1 v2)");
							idList = Console.ReadLine().Split(" ").ToList();
							graph.AddEdge(graph.GetVertexById(idList[0]), graph.GetVertexById(idList[1]));
						}
						break;
					case "3":
						Console.WriteLine("введите название удаляемой вершины");
						id = Console.ReadLine();
						if (graph.IsVertexExists(id)) graph.RemoveVertex(graph.GetVertexById(id));
						else Console.WriteLine("вершины не существует");
						break;
					case "4":
						Console.WriteLine("введите вершины, между которыми удаляется ребро(v1 v2)");
						idList = Console.ReadLine().Split(" ").ToList();
						graph.RemoveEdge(graph.GetVertexById(idList[0]), graph.GetVertexById(idList[1]));
						break;
					case "5":
						PrintAdjacencyList(graph);
						break;
					case "6":
						PrintAdjacencyMatrix(graph);
						break;
					case "7"://граф в файл
						Console.WriteLine("введите путь до файла:");        //проверка на существоание файла?
						WriteAdjacencyListToFile(Console.ReadLine(), graph);       //создание файла если он не существует?
						break;
					case "8":
						return;
					default:
						Console.WriteLine("Выбрана несущесвующая операция");
						break;
				}
			}
		}
		static public void PrintAdjacencyMatrix(Graph graph)
		{
			double[][] matrix = graph.CreateAdjacencyMatrix();
			for (int i = 0; i < matrix.Length; i++)
				Console.WriteLine(String.Join("  ", matrix[i]));
		}
		static public void WriteAdjacencyMatrixToFile(string path,Graph graph)
		{
			double[][] adjMatrix = graph.CreateAdjacencyMatrix();
			string res = "";
			for (int i = 0; i < adjMatrix.Length; i++)
				res += (String.Join(" ", adjMatrix[i]) + "\n");
			File.WriteAllText(path, res);
		}
		static public void PrintAdjacencyList(Graph graph)
		{
			string res = "";
			for (int i = 0; i < graph.AdjacencyList.Count; i++)
			{
				res += ($"{graph.AdjacencyList.ElementAt(i).Key.Id} : ");
				res += (String.Join("  ", (graph.AdjacencyList[graph.AdjacencyList.ElementAt(i).Key].Select(x => (x.Item1.Id).ToString() + "," + (x.Item2).ToString()).ToList())) + "\n");
				
			}
			Console.WriteLine(res);
		}
		static public void WriteAdjacencyListToFile(string path,Graph graph)
		{
			string res = "";
			for (int i = 0; i < graph.AdjacencyList.Count; i++)
			{
				res += ($"{graph.AdjacencyList.ElementAt(i).Key.Id} : ");
				res += (String.Join("  ", (graph.AdjacencyList[graph.AdjacencyList.ElementAt(i).Key].Select(x => (x.Item1.Id).ToString() + "," + (x.Item2).ToString()).ToList())) + "\n");
			}
			File.WriteAllText(path, res);
		}
	}
}
