using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class1
{
    public class Solver
    {
        // Основной метод для вызова из Python - принимает простые массивы
        public static int[] FindMaxSetA1_Simple(int[] graphArray,int[] neighborKeys,int[][] neighborArrays)
        {
            // Преобразуем массивы в нужные типы внутри C#
            var graph = graphArray.ToList();
            var neighbors = new Dictionary<int, List<int>>();

            for (int i = 0; i < neighborKeys.Length; i++)
            {
                neighbors[neighborKeys[i]] = neighborArrays[i].ToList();
            }

            // Вызываем основной алгоритм
            var result = FindMaxSetA1(graph, neighbors, new List<int>());
            return result.ToArray();
        }
        public static int[] FindMaxSetA2_Simple(int[] neighborKeys,int[][] neighborArrays)
        {
            var neighbors = new Dictionary<int, List<int>>();

            for (int i = 0; i < neighborKeys.Length; i++)
            {
                neighbors[neighborKeys[i]] = neighborArrays[i].ToList();
            }

            var result = FindMaxSetA2(neighbors);
            return result.ToArray();
        }

        //  алгоритм A1
        public static List<int> FindMaxSetA1(List<int> graph, Dictionary<int, List<int>> neighbors, List<int> curSet)
        {
            if (graph.Count == 0)
            {
                return new List<int>(curSet);
            }
            int ver = graph[0];
            List<int> newGraph1 = new List<int>();
            foreach (int i in graph)
            {
                if ((i != ver) && (!neighbors[ver].Contains(i)))
                {
                    newGraph1.Add(i);
                }
            }
            List<int> newCurSet1 = new List<int>(curSet) { ver };
            List<int> result1 = FindMaxSetA1(newGraph1, neighbors, newCurSet1);
            List<int> newGraph2 = new List<int>(graph);
            newGraph2.Remove(ver);
            var result2 = FindMaxSetA1(newGraph2, neighbors, curSet);
            if (result1.Count >= result2.Count)
            {
                return result1;
            }
            else
            {
                return result2;
            }
        }

        // алгоритм A2 
        public static List<int> FindMaxSetA2(Dictionary<int, List<int>> neighbors)
        {
            var neighborsCopy = new Dictionary<int, List<int>>(neighbors);
            var sortedVer = neighbors.Keys.OrderBy(v => neighbors[v].Count).ToList();
            List<int> maxSet = new List<int>();

            foreach (int i in sortedVer)
            {
                if (!neighborsCopy.ContainsKey(i))
                    continue;

                maxSet.Add(i);
                var neighborsToRemove = neighborsCopy[i].ToList();
                neighborsCopy.Remove(i);
                foreach (int neighbor in neighborsToRemove)
                {
                    neighborsCopy.Remove(neighbor);
                }
            }
            return maxSet;
        }
    }
}
