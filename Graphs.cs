namespace Graphs
{
	class Graph
	{
		protected Dictionary<Vertex, List<Vertex>> AdjacencyList = new();
		protected List<Vertex> Vertices = new();
		protected List<Edge> Edges = new();

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
			Vertices = Enumerable.Range(0,IncidenceMatrix.Length).Select(x => new Vertex(x)).ToList();

			//for (int i = 0; i < IncidenceMatrix.Length; i++) {
			//	for (int j = 0; j < IncidenceMatrix[i].Length; j++){
			//		int FirstEdge, SecEdge;
			//		if (IncidenceMatrix[i][j] == 1) {
			//		}
			//	}
			//}
			createAdjacencyList();
		}
		private void createAdjacencyList() { 
			
		}
	}

	class Vertex
	{
		protected int Id;
		public Vertex(int Id)
		{
			this.Id = Id;
		}
	}

	class Edge
	{
		protected int Id;
		protected KeyValuePair<Vertex, Vertex> Link;
		public Edge(int id, KeyValuePair<Vertex, Vertex> link)
		{
			Id = id;
			Link = link;
		}
	}
}
