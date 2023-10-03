namespace Graphs
{
    class Graph
    {
        Dictionary<Node, List<Node>> Vertices = new Dictionary<Node, List<Node>>();

        public Graph(Dictionary<Node, List<Node>> vertices)
        {
            Vertices = vertices;
        }
        public Graph()
        {
            Vertices = new Dictionary<Node, List<Node>>();
        }
    }

    class Node
    {
        protected int Id;
        public Node(int Id)
        {
            this.Id = Id;
        }
    }

    class Edge
    {
        protected int Id;
        KeyValuePair<Node, Node> Link;
        public Edge(int id, KeyValuePair<Node, Node> link)
        {
            Id = id;
            Link = link;
        }
    }
}
