using System.Linq;

namespace Graphs
{
	class Graph
	{
		internal Dictionary<Vertex, List<(Vertex,double)>> AdjacencyList = new();
		internal bool wheighted;
		internal bool oriented;
		public Graph(Dictionary<Vertex, List<(Vertex,double)>> vertices, bool wheighted, bool oriented)
		{
			this.AdjacencyList = vertices;
			this.wheighted = wheighted;
			this.oriented = oriented;
		}
		public Graph(Graph graph) {
			this.AdjacencyList = graph.AdjacencyList;
			this.oriented = graph.oriented;
			this.wheighted = graph.wheighted;
		}
		public Graph()
		{
			AdjacencyList = new Dictionary<Vertex, List<(Vertex,double)>>();
			wheighted = false;
			oriented = false;
		}
		public Graph(string path) {
			//int[,] AdjacencyMatrix = File.ReadAllText(path).Split("\n").Select(str => str.Split(",").Select(c => Convert.ToInt32(c)).ToArray()).ToArray();
			//Vertices = Enumerable.Range(0,IncidenceMatrix.Length).Select(x => new Vertex(x.ToString())).ToList();
			//for (int i = 0; i < IncidenceMatrix.Length; i++) {
			//	for (int j = 0; j < IncidenceMatrix[i].Length; j++){
			//		int FirstEdge, SecEdge;
			//		if (IncidenceMatrix[i][j] == 1) {
			//		}
			//	}
			//}
			
		}
		private void AddVertex(Vertex vertex) {
			AdjacencyList.Add(vertex, new List<(Vertex,double)>());
		}
		private void RemoveVertex(Vertex vertex) {
			if (wheighted)
			{
				foreach (var vertices in AdjacencyList.Values) {
					try
					{
						vertices.Remove(vertices.First(x => (x.Item1 == vertex)));
					}
					catch { }
				}
			}
			else
				foreach (var vertices in AdjacencyList.Values) vertices.Remove((vertex, 1));
			AdjacencyList.Remove(vertex);
		}
		private void AddEdge(Vertex vertex1, Vertex vertex2) {
			if (vertex1 == vertex2) AdjacencyList[vertex1].Add((vertex1,1));
			else
			{
				if (oriented)	AdjacencyList[vertex1].Add((vertex2, 1));
				else 
				{ 
					AdjacencyList[vertex1].Add((vertex2, 1));
					AdjacencyList[vertex2].Add((vertex1, 1));
				}
			}
		}
		private void AddEdge(Vertex vertex1, Vertex vertex2, double weight)
		{
			if (vertex1 == vertex2) AdjacencyList[vertex1].Add((vertex1,weight));
			else
			{
                if (oriented) AdjacencyList[vertex1].Add((vertex2, weight));
                else
                {
                    AdjacencyList[vertex1].Add((vertex2, weight));
                    AdjacencyList[vertex2].Add((vertex1, weight));
                }
            }
		}

		private void RemoveEdge(Vertex vertex1, Vertex vertex2) {
			AdjacencyList[vertex1].Remove(AdjacencyList[vertex1].First(x => x.Item1 == vertex2));
			AdjacencyList[vertex2].Remove(AdjacencyList[vertex2].First(x => x.Item1 == vertex1));
		}
		private int[,] CreateAdjacencyList() {
			int[,] matrix = new int[AdjacencyList.Keys.Count,AdjacencyList.Keys.Count];	//каждоой вершине индекс - и позжен добавлять 1 и 0 уже для каждого индекса
			for (int i = 0; i < AdjacencyList.Keys.Count; i++) {
				//AdjacencyList.ElementAt(i).Key;
			}

			return matrix;
		}
		private Vertex GetVertexById(string id) {
			return AdjacencyList.Keys.First(x => x.Id == id);
		}
		private bool IsVertexExists(string id) {
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
		public void UI() {
			Console.WriteLine(
				"1. Добавить вершину\n" +
				"2. Добавить ребро\n" +
				"3. Удалить вершину\n" +
				"4. Удалить ребро\n" +
				"5. Вывести список смежности\n" +
				"6. Сохранить граф в файл\n" +
				"7. Выход");
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
						if (!IsVertexExists(id)) AddVertex(new Vertex(id));
						else Console.WriteLine("вершина уже существует");
						break;
					case "2":
						if (wheighted)
						{
							Console.WriteLine("введите вершины, между которыми создается ребро и вес ребра(v1 v2 wheight)");
							idList  = Console.ReadLine().Split(" ").ToList();
							AddEdge(GetVertexById(idList[0]), GetVertexById(idList[1]),Convert.ToDouble(idList[2]));
						}
						else { 
							Console.WriteLine("введите вершины, между которыми создается ребро(v1 v2)");// вес ребра
							idList = Console.ReadLine().Split(" ").ToList();
							AddEdge(GetVertexById(idList[0]), GetVertexById(idList[1]));
						}
						break;
					case "3":
						Console.WriteLine("введите название удаляемой вершины");
						id = Console.ReadLine();
						if (IsVertexExists(id)) RemoveVertex(GetVertexById(id));
						else Console.WriteLine("вершины не существует");
						break;
					case "4":
						Console.WriteLine("введите вершины, между которыми удаляется ребро(v1 v2)");
						idList = Console.ReadLine().Split(" ").ToList();
						RemoveEdge(GetVertexById(idList[0]), GetVertexById(idList[1]));
						break;
					case "5"://список смежности
					case "6"://граф в файл
						break;
					case "7":
						return;
					default:
						Console.WriteLine("Выбрана несущесвующая операция");
						break;
				}
			}
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

	//class Edge
	//{
	//	internal int Id;
	//	internal KeyValuePair<Vertex, Vertex> Link;
	//  internal int weight;
	//	public Edge(int id, KeyValuePair<Vertex, Vertex> link)
	//	{
	//		Id = id;
	//		Link = link;
	//	}
	//}
}
