using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Graphs
{
	class Graph
	{
		internal Dictionary<string, List<(string, double)>> AdjacencyList = new();
		internal bool wheighted;
		internal bool oriented;
		public Graph(Dictionary<string, List<(string, double)>> vertices, bool wheighted, bool oriented)
		{
			this.AdjacencyList = vertices;
			this.wheighted = wheighted;
			this.oriented = oriented;
		}
		public Graph(Graph graph)
		{
			foreach (var item in graph.AdjacencyList) {
				AddVertex(new string(item.Key));
			}
			foreach (var value in graph.AdjacencyList)
			{
				foreach (var item in value.Value)
				{
					AddEdge(value.Key,item.Item1,item.Item2);
				}
			}
			this.oriented = graph.oriented;
			this.wheighted = graph.wheighted;
		}
		public Graph()
		{
			AdjacencyList = new Dictionary<string, List<(string, double)>>();
			wheighted = false;
			oriented = false;
		}
		public Graph(bool wheighted , bool oriented)
		{
			AdjacencyList = new Dictionary<string, List<(string, double)>>();
			wheighted = false;
			oriented = false;
		}
		public Graph(string path)
		{
			double[][] AdjacencyMatrix = File.ReadAllLines(path).Select(str => str.Split(",").Select(c => Convert.ToDouble(c)).ToArray()).ToArray();
			for (int i = 0; i < AdjacencyMatrix.Length; i++) { 
				AddVertex(new string($"V{i + 1}"));
			}
			for (int i = 0; i < AdjacencyMatrix.Length; i++)
			{
				for (int j = 0; j < AdjacencyMatrix.Length; j++)
				{
					if (AdjacencyMatrix[i][j] > 0) AdjacencyList[((($"V{i + 1}")))].Add((($"V{j + 1}"), (AdjacencyMatrix[i][j]))); ;
					if (!wheighted && (AdjacencyMatrix[i][j] != 1 || AdjacencyMatrix[i][j] != -1)) wheighted = true;
					if (!oriented && AdjacencyMatrix[i][j] < 0) oriented = true;
				}
			}
		}
		public static Graph CompleteGraph(Graph graph) {
			Dictionary<string, List<(string, double)>> AdjList = new();
			foreach (var vertexK in graph.AdjacencyList.Keys)
			{
				AdjList.Add(vertexK, new List<(string, double)>());
				foreach (var VertexV in graph.AdjacencyList.Keys)
				{
					if (vertexK != VertexV)	AdjList[vertexK].Add((VertexV, 1));
				}
			}
			return new Graph(AdjList,graph.wheighted,graph.oriented);
		}
		public static Graph ComplementGraph(Graph graph) {
			Dictionary<string, List<(string, double)>> AdjList = new();
			foreach (var vertexK in graph.AdjacencyList.Keys)
			{
				AdjList.Add(vertexK, new List<(string, double)>());
				foreach (var VertexV in graph.AdjacencyList.Keys)
				{
					if (!graph.AdjacencyList[vertexK].Any(x => x.Item1 == VertexV) && VertexV != vertexK) AdjList[vertexK].Add((VertexV,1));
				}
			}
			return new Graph(AdjList, graph.wheighted, graph.oriented);
		}
		public static Graph? UnionGraph(Graph graph1, Graph graph2){	
			if (graph1.AdjacencyList.Keys.Intersect(graph2.AdjacencyList.Keys).Any())   
				return null;                                                            
			Dictionary<string, List<(string, double)>> AdjList = new();
			foreach (var item in graph1.AdjacencyList)
			{
				List<(string, double)> lst = new();
				foreach (var item2 in item.Value)
				{
					lst.Add((new string(item2.Item1), item2.Item2));
				}
				AdjList.Add(item.Key, lst);
			}
			foreach (var item in graph2.AdjacencyList)
			{
				List<(string, double)> lst = new();
				foreach (var item2 in item.Value)
				{
					lst.Add((new string(item2.Item1), item2.Item2));
				}
				AdjList.Add(item.Key, lst);
			}
			return new Graph(AdjList, false, false);
		}
		public static Graph? IntersectionGraph(Graph graph1, Graph graph2) {
			if (graph1.AdjacencyList.Keys.Intersect(graph2.AdjacencyList.Keys).Any())
				return null;
			Dictionary<string, List<(string, double)>> AdjList = new();
			foreach (var item in graph1.AdjacencyList) {
				List<(string, double)> lst = new();
				foreach (var item2 in item.Value) {
					lst.Add((new string(item2.Item1), item2.Item2));
				}
				AdjList.Add(item.Key, lst); }
			foreach (var item in graph2.AdjacencyList) {
				List<(string, double)> lst = new();
				foreach (var item2 in item.Value)
				{
					lst.Add((new string(item2.Item1), item2.Item2));
				}
				AdjList.Add(item.Key, lst);
			}
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
		internal void AddVertex(string vertex)
		{
			AdjacencyList.Add(vertex, new List<(string, double)>());
		}
		internal void RemoveVertex(string vertex)
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
		internal void AddEdge(string vertex1, string vertex2, double weight = 1)
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
		internal void RemoveEdge(string vertex1, string vertex2)
		{
			AdjacencyList[vertex1].Remove(AdjacencyList[vertex1].First(x => x.Item1 == vertex2));
			AdjacencyList[vertex2].Remove(AdjacencyList[vertex2].First(x => x.Item1 == vertex1));
		}
		internal void DFS(string vertex, string from, Dictionary<string,int> visited, ref bool HasCycle) {
			visited[vertex] = 1;
			foreach (var path in AdjacencyList[vertex])
			{
				if (path.Item1 == from)
				{
					continue;
				}
				else if (visited[path.Item1] == 0)
				{
					DFS(path.Item1, vertex, visited, ref HasCycle);
				}
				else if (visited[path.Item1] == 1)
				{
					HasCycle = true;
				}
			}
			visited[vertex] = 2;
		}
		internal int StrongConnections() {
			int Connections = 0;
			bool hasCycle = false;
			Dictionary<string, int> visited = new();
			foreach (var vert in AdjacencyList)
			{
				visited.Add(vert.Key, 0);
			}
			foreach (var vert in AdjacencyList.Keys) {
				if (visited[vert] == 0) {
					Connections++;
					DFS(vert, vert, visited, ref hasCycle);
				}
			}
			return Connections;
		}
		internal bool IsCyclical() {
			bool IsCyclical = false;
			Dictionary<string, int> visited = new();
			foreach (var vert in AdjacencyList)
			{
				visited.Add(vert.Key, 0);
			}
			foreach (var vert in AdjacencyList.Keys)
			{
				if (visited[vert] == 0)
				{
					DFS(vert, vert, visited, ref IsCyclical);
				}
			}
			return IsCyclical;
		}
		internal bool IsForest() {
			return (StrongConnections() > 1 && !IsCyclical());
		}
		internal double[][] CreateAdjacencyMatrix()
		{
			List<string> vertexList = AdjacencyList.Keys.ToList();
			double[][] matrix = new double[AdjacencyList.Keys.Count][];
			foreach (string v in vertexList) matrix[vertexList.IndexOf(v)] = new double[AdjacencyList.Keys.Count];
			//if (oriented){
			foreach (string v in vertexList)
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
		internal bool IsVertexExists(string id)
		{
			return AdjacencyList.Keys.Contains(id);
		}
		internal static Graph Kruskal(Graph graph) { 
			Graph res = new Graph(true,false);
			foreach (string vert in graph.AdjacencyList.Keys)
			{
				res.AddVertex(vert);
			}
			Dictionary<(string, string), double> dict = new();
			foreach (var vertex in graph.AdjacencyList)
			{
				foreach (var adjVert in vertex.Value)
				{
					dict.Add((vertex.Key,adjVert.Item1),adjVert.Item2);
				}
			}
			dict = dict.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
			foreach (var Edge in dict) {
				res.AddEdge(Edge.Key.Item1,Edge.Key.Item2,Edge.Value);
				if (res.IsCyclical())
				{
					res.RemoveEdge(Edge.Key.Item1, Edge.Key.Item2);
				}
			}
			return res;
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
				"8. вывести количество компонент сильной связности\n" +
				"9. определить деревянность графа\n" +
				"10. Выход");
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
						if (!graph.IsVertexExists(id)) graph.AddVertex(new string(id));
						else Console.WriteLine("вершина уже существует");
						break;
					case "2":
						if (graph.wheighted)
						{
							Console.WriteLine("введите вершины, между которыми создается ребро и вес ребра(v1 v2 wheight)");
							idList = Console.ReadLine().Split(" ").ToList();
							graph.AddEdge(idList[0],idList[1], Convert.ToDouble(idList[2]));
						}
						else
						{
							Console.WriteLine("введите вершины, между которыми создается ребро(v1 v2)");
							idList = Console.ReadLine().Split(" ").ToList();
							graph.AddEdge(idList[0], idList[1]);
						}
						break;
					case "3":
						Console.WriteLine("введите название удаляемой вершины");
						id = Console.ReadLine();
						if (graph.IsVertexExists(id)) graph.RemoveVertex(id);
						else Console.WriteLine("вершины не существует");
						break;
					case "4":
						Console.WriteLine("введите вершины, между которыми удаляется ребро(v1 v2)");
						idList = Console.ReadLine().Split(" ").ToList();
						graph.RemoveEdge(idList[0], idList[1]);
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
						Console.WriteLine(graph.StrongConnections());
						break;
					case "9":
						if (graph.IsForest())
						{
							Console.WriteLine("граф является лесом");
							break;
						}
						if (!graph.IsCyclical())
						{
							Console.WriteLine("граф является деревом");
							break;
						}
						Console.WriteLine("граф не является ни деревом ни лесом");
						break;
					case "10":
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
				res += ($"{graph.AdjacencyList.ElementAt(i).Key} : ");
				res += (String.Join("  ", (graph.AdjacencyList[graph.AdjacencyList.ElementAt(i).Key].Select(x => (x.Item1).ToString() + "," + (x.Item2).ToString()).ToList())) + "\n");
				
			}
			Console.WriteLine(res);
		}
		static public void WriteAdjacencyListToFile(string path, Graph graph)
		{
			string res = "";
			for (int i = 0; i < graph.AdjacencyList.Count; i++)
			{
				res += ($"{graph.AdjacencyList.ElementAt(i).Key} : ");
				res += (String.Join("  ", (graph.AdjacencyList[graph.AdjacencyList.ElementAt(i).Key].Select(x => (x.Item1).ToString() + "," + (x.Item2).ToString()).ToList())) + "\n");
			}
			File.WriteAllText(path, res);
		}
	}
}
