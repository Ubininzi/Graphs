using Graphs;

Graph graph = new Graph("C:\\Users\\Максим\\source\\repos\\App1\\Graphs\\TestIncidenceMatrix.csv");
graph.AddVertex();

graph.RemoveEdge("v8", "v8");

graph.RemoveVertex("v8");
graph.AddVertex();
