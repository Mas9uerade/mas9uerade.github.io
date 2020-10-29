using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2
{
    public class BusStation
    {
        public class Vector2<T> 
        {
            public T X;
            public T Y;

            public Vector2(T x, T y )
            {
                X = x;
                Y = y;
            }
        }

        public int NumBusesToDestination(int[][] routes, int S, int T)
        {
            if (routes==null || routes.Length == 0)
            {
                return -1;
            }

            if (S==T)
            {
                return 0;
            }
            //邻接表
            List<List<int>> routeGraph = new List<List<int>>(routes.Length);
            for (int i = 0; i < routes.Length; ++i)
            {
                routeGraph.Add(new List<int>());
            }
            for (int i = 0; i <routes.Length; ++i)
            {
                for (int j = i+1; j < routes.Length; ++j )
                {
                    if (IsRouteIntersect(routes[i], routes[j]))
                    {
                        routeGraph[i].Add(j);
                        routeGraph[j].Add(i);
                    }
                }
            }

            HashSet<int> seen = new HashSet<int>();
            HashSet<int> target = new HashSet<int>();
            Queue<Vector2<int>> queue = new Queue<Vector2<int>>();

            for (int i =0; i < routes.Length; ++i)
            {
                bool match = BinarySearch(0, routes[i].Length-1, routes[i], S);
                if (match)
                {
                    seen.Add(i);
                    queue.Enqueue(new Vector2<int>(i, 0));
                }
                if (BinarySearch(0, routes[i].Length - 1, routes[i], T))
                {
                    target.Add(i);
                }
            }

            while (queue.Count>0)
            {
                Vector2<int> info = queue.Dequeue();
                int node = info.X, depth = info.Y;
                if (target.Contains(node))
                { 
                    return depth + 1;
                }
                foreach (int nei in routeGraph[node])
                {
                    if (!seen.Contains(nei))
                    {
                        seen.Add(nei);
                        queue.Enqueue(new Vector2<int>(nei, depth + 1));
                    }
                }
            }
            return -1;
        }

        //是否相交
        public bool IsRouteIntersect(int[] route1, int[] route2)
        {
            int i =0, j = 0;
            while (i < route1.Length && j < route2.Length)
            {
                if (route1[i] == route2[j])
                {
                    return true;
                }
                if (route1[i] > route2[j])
                {
                    j++;
                }
                else
                {
                    i++;
                }
            }
            return false;
        }


        public bool BinarySearch(int beginIndex, int endIndex, int[] data, int val)
        {
            if (data[endIndex] < val)
            {
                return false;
            }
            if (data[endIndex] == val || data[beginIndex] == val)
            {
                return true;
            }
            int mid;
            while (beginIndex < data.Length && endIndex >= 0 && data[beginIndex] <= data[endIndex])
            {
                mid = (beginIndex + endIndex) / 2;
                if (data[mid] < val)
                {
                    beginIndex = mid + 1;
                }
                else if (data[mid] > val)
                {
                    endIndex = mid - 1;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }
    }
}
