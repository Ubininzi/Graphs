using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Graphs;

//Graph graph1 = new("D:\\Users\\st315-09\\source\\repos\\GRPHS\\AdjacencyMatrix2.csv");
//Graph graph2 = new("D:\\Users\\st315-09\\source\\repos\\GRPHS\\TestAdjacencyMatrix.csv");
Graph graph1 = new("C:\\Users\\Максим\\source\\repos\\App1\\Graphs\\TestAdjacencyMatrix.csv");
//Graph graph2 = new("D:\\Users\\st306-06\\source\\repos\\graphs\\TestAdjacencyMatrix.csv");

GraphUI.UI(graph1);
//GraphUI.UI(Graph.ComplementGraph(graph1));
//Console.WriteLine(graph2.StrongConnections());
//GraphUI.UI(graph2);
//GraphUI.UI(Graph.ComplementGraph(graph1));
//Graph graph2 = new Graph();
//GraphUI.UI(graph2);

Graph Kgraph = Graph.Kruskal(graph1);
if (Kgraph != null)
    GraphUI.UI(Kgraph);
