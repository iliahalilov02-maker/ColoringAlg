using System;
using System.Collections.Generic;

namespace PrimAlg;

public static class GreedyColoring
{
    public class Result
    {
        /// <summary>Цвет каждой вершины (0, 1, 2, ...).</summary>
        public int[] Colors { get; init; } = Array.Empty<int>();

        /// <summary>Число использованных цветов (хроматическое число по жадному алгоритму).</summary>
        public int ColorCount { get; init; }
    }

    /// <summary>
    /// Жадная раскраска по заданному порядку вершин.
    /// </summary>
    /// <param name="adjacency">Список смежности (неориентированный граф).</param>
    /// <param name="order">Порядок обхода вершин. null — естественный 0..V-1.</param>
    public static Result Colorize(List<List<int>> adjacency, IReadOnlyList<int>? order = null)
    {
        int n = adjacency.Count;
        order ??= Enumerable.Range(0, n).ToArray();

        var colors = new int[n];
        Array.Fill(colors, -1); // -1 = не раскрашена

        // seen[c] = true, если цвет c уже занят соседом текущей вершины
        var seen = new bool[n]; // цветов не больше V

        foreach (int v in order)
        {
            Array.Fill(seen, false);

            foreach (int neighbor in adjacency[v])
            {
                int c = colors[neighbor];
                if (c != -1)
                    seen[c] = true;
            }

            // Наименьший свободный цвет
            int chosen = 0;
            while (chosen < n && seen[chosen])
                chosen++;

            colors[v] = chosen;
        }

        int used = 0;
        for (int i = 0; i < n; i++)
            used = Math.Max(used, colors[i] + 1);

        return new Result { Colors = colors, ColorCount = used };
    }

    /// <summary>
    /// Порядок Уэлша-Пауэлла: сортировка вершин по убыванию степени.
    /// Обычно даёт меньше цветов, чем естественный порядок.
    /// </summary>
    public static int[] WelshPowellOrder(List<List<int>> adjacency)
    {
        int n = adjacency.Count;
        var order = new int[n];
        for (int i = 0; i < n; i++) order[i] = i;

        Array.Sort(order, (a, b) => adjacency[b].Count.CompareTo(adjacency[a].Count));
        return order;
    }
}