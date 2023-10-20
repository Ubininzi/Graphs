using Graphs;

//Graph graph1 = new("D:\\Users\\st315-09\\source\\repos\\GRPHS\\AdjacencyMatrix2.csv");
//Graph graph2 = new("D:\\Users\\st315-09\\source\\repos\\GRPHS\\TestAdjacencyMatrix.csv");
Graph graph1 = new("C:\\Users\\Максим\\source\\repos\\App1\\Graphs\\AdjacencyMatrix2.csv");
//Graph graph2 = new("D:\\Users\\st306-06\\source\\repos\\graphs\\TestAdjacencyMatrix.csv");


Graph graph2 = new(false, false);
graph2.AddVertex("C1");
graph2.AddVertex("C2");
graph2.AddVertex("C3");
graph2.AddEdge("C1", "C2");
graph2.AddEdge("C2", "C3");

GraphUI.UI(Graph.ComplementGraph(graph1));
GraphUI.UI(Graph.CompleteGraph(graph2));
GraphUI.UI(Graph.IntersectionGraph(graph1,graph2));
GraphUI.UI(Graph.UnionGraph(graph1,graph2));

Graph Kgraph = Graph.Kruskal(graph1);
if (Kgraph != null)
    GraphUI.UI(Kgraph);
