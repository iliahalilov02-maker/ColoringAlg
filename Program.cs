using System;
using System.Collections.Generic;
using PrimAlg;

class Program
{
    static void Main()
    {
        // Пример: граф из 6 вершин
        //   0 --- 1
        //   | \   |
        //   |  \  |
        //   3 --- 2
        //   |
        //   4 --- 5
        //
        // Рёбра: 0-1, 0-2, 0-3, 1-2, 2-3, 3-4, 4-5
        int n = 6;
        var adjacency = new List<List<int>>();
        for (int i = 0; i < n; i++) adjacency.Add(new List<int>());

        void AddEdge(int u, int v)
        {
            adjacency[u].Add(v);
            adjacency[v].Add(u);
        }

        AddEdge(0, 1);
        AddEdge(0, 2);
        AddEdge(0, 3);
        AddEdge(1, 2);
        AddEdge(2, 3);
        AddEdge(3, 4);
        AddEdge(4, 5);

        // 1) Жадная раскраска в естественном порядке
        var natural = GreedyColoring.Colorize(adjacency);
        PrintResult("Естественный порядок", natural);

        // 2) Жадная раскраска в порядке Уэлша-Пауэлла (по убыванию степени)
        var wpOrder = GreedyColoring.WelshPowellOrder(adjacency);
        var wp = GreedyColoring.Colorize(adjacency, wpOrder);
        PrintResult("Порядок Уэлша-Пауэлла", wp);
    }

    static void PrintResult(string title, GreedyColoring.Result r)
    {
        Console.WriteLine($"=== {title} ===");
        Console.WriteLine($"Использовано цветов: {r.ColorCount}");
        for (int v = 0; v < r.Colors.Length; v++)
            Console.WriteLine($"  Вершина {v}: цвет {r.Colors[v]}");
        Console.WriteLine();
    }
}