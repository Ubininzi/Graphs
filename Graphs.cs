using System.Linq;

namespace Graphs
{
	class Graph
	{
		internal Dictionary<Vertex, List<Vertex>> AdjacencyList = new();
		//List<Vertex> Vertices = new();
		//List<Edge> Edges = new();
		public Graph(Dictionary<Vertex, List<Vertex>> vertices)
		{
			AdjacencyList = vertices;
		}
		public Graph()
		{
			AdjacencyList = new Dictionary<Vertex, List<Vertex>>();
		}
		public Graph(string path) {
			int[][] IncidenceMatrix = File.ReadAllText(path).Split("\n").Select(str => str.Split(",").Select(c => Convert.ToInt32(c)).ToArray()).ToArray();
			//Vertices = Enumerable.Range(0,IncidenceMatrix.Length).Select(x => new Vertex(x.ToString())).ToList();

			//for (int i = 0; i < IncidenceMatrix.Length; i++) {
			//	for (int j = 0; j < IncidenceMatrix[i].Length; j++){
			//		int FirstEdge, SecEdge;
			//		if (IncidenceMatrix[i][j] == 1) {
			//		}
			//	}
			//}
			CreateAdjacencyList();
		}
		private void AddVertex(Vertex vertex) {
			AdjacencyList.Add(vertex, new List<Vertex>());
		}
		private void RemoveVertex(Vertex vertex) {
			foreach (var vertices in AdjacencyList.Values) vertices.Remove(vertex);
			AdjacencyList.Remove(vertex);
		}
		private void AddEdge(Vertex vertex1, Vertex vertex2) {
			if (vertex1 == vertex2) AdjacencyList[vertex1].Add(vertex1);
			else
			{
				AdjacencyList[vertex1].Add(vertex2);
				AdjacencyList[vertex2].Add(vertex1);
			}
		}
		private void RemoveEdge(Vertex vertex1, Vertex vertex2) {
			AdjacencyList[vertex1].Remove(vertex2);
			AdjacencyList[vertex2].Remove(vertex1);
		}
		private void CreateAdjacencyList() {
			CreateEdgeList();
		}
		private void CreateEdgeList() { }
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
						Console.WriteLine("введите вершины, между которыми создается ребро(v1,v2)");
						idList  = Console.ReadLine().Split(",").ToList();
						AddEdge(GetVertexById(idList[0]), GetVertexById(idList[1]));
						break;
					case "3":
						Console.WriteLine("введите название удаляемой вершины");
                        id = Console.ReadLine();
                        if (IsVertexExists(id)) RemoveVertex(GetVertexById(id));
                        else Console.WriteLine("вершины не существует");
                        break;
                    case "4":
						Console.WriteLine("введите вершины, между которыми удаляется ребро(v1,v2)");
                        idList = Console.ReadLine().Split(",").ToList();
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
