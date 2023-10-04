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
		public void AddVertex() {
			//проверка на дубликат
			Console.WriteLine("Введите название вершины");
			string id = Console.ReadLine();
			AdjacencyList.Add(new Vertex(id), new List<Vertex>());
			Console.WriteLine($"вы создали вершину {id}");
		}
		public void RemoveVertex(string id) {
			//AdjacencyList.Values.Select(x => x.Remove(GetVertexId(id)));
			foreach (var vertices in AdjacencyList.Values) vertices.Remove(GetVertexId(id));
			AdjacencyList.Remove(GetVertexId(id));
		}
		public void AddEdge(string id1, string id2) {
			if (id1 == id2) AdjacencyList[GetVertexId(id1)].Add(GetVertexId(id2));
			else
			{
				AdjacencyList[GetVertexId(id1)].Add(GetVertexId(id2));
				AdjacencyList[GetVertexId(id2)].Add(GetVertexId(id1));
			}
		}
		public void RemoveEdge(string id1, string id2) {
			AdjacencyList[GetVertexId(id2)].Remove(GetVertexId(id1));
			AdjacencyList[GetVertexId(id1)].Remove(GetVertexId(id2));
        }
        private void CreateAdjacencyList() {
			CreateEdgeList();
		}
		private void CreateEdgeList() { }
		private Vertex GetVertexId(string id) {
			return AdjacencyList.Keys.First(x => x.Id == id);
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

	class Edge
	{
		internal int Id;
		internal KeyValuePair<Vertex, Vertex> Link;
		public Edge(int id, KeyValuePair<Vertex, Vertex> link)
		{
			Id = id;
			Link = link;
		}
	}

}
