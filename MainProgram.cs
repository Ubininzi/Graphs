using Graphs;

Graph graph1 = new("C:\\Users\\Максим\\source\\repos\\App1\\Graphs\\AdjacencyMatrix2.csv");
Graph graph2 = new("C:\\Users\\Максим\\source\\repos\\App1\\Graphs\\TestAdjacencyMatrix.csv");
//Graph graph1 = new("D:\\Users\\st306-06\\source\\repos\\graphs\\TestAdjacencyMatrix.csv");
//Graph graph2 = new("D:\\Users\\st306-06\\source\\repos\\graphs\\TestAdjacencyMatrix.csv");
Console.WriteLine(graph2.StrongConnections());
GraphUI.UI(graph2);
GraphUI.UI(Graph.ComplementGraph(graph1));


//Graph Kgraph = Graph.ComplementGraph(graph1);
//Graph Ugraph = Graph.IntersectionGraph(graph1, graph2);
//if (Ugraph != null)
//    GraphUI.UI(Ugraph);
