namespace LeetCode
{
    public class Graph_Dinic
    {
        private int n;
        private List<(int, long)>[] g;
        private int[][] index;
        private int[] depth;
        private int[] now;
        private int s, t;
        public Graph_Dinic(int n)
        {
            this.n = n;
            g = new List<(int, long)>[n];
            index = new int[n][];
            depth = new int[n];
            now = new int[n];
            for (int i = 0; i < n; i++)
            {
                g[i] = new List<(int, long)>();
                index[i] = new int[n];
                Array.Fill(index[i], -1);
            }
        }

        public void AddEdge(int u, int v, long w)
        {
            if (index[u][v] < 0)
            {
                index[u][v] = g[u].Count;
                g[u].Add((v, w));
            }
            else
            {
                g[u][index[u][v]] = (v, g[u][index[u][v]].Item2 + w);
            }
            if (index[v][u] < 0)
            {
                index[v][u] = g[v].Count;
                g[v].Add((u, 0));
            }
        }

        private bool BFS()
        {
            Array.Fill(now, 0);
            Array.Fill(depth, -1);
            depth[s] = 0;
            Queue<long> queue = new Queue<long>();
            queue.Enqueue(s);
            while (queue.Count > 0)
            {
                long u = queue.Dequeue();
                if (u == t) return true;
                for (int i = 0; i < g[u].Count; i++)
                {
                    long v = g[u][i].Item1;
                    long w = g[u][i].Item2;
                    if (w > 0 && depth[v] < 0)
                    {
                        queue.Enqueue(v);
                        depth[v] = depth[u] + 1;
                    }
                }
            }
            return false;
        }

        private long DFS(int u, long limit)
        {
            if (u == t || limit == 0) return limit;
            long flow = 0;
            for (int i = now[u]; i < g[u].Count; i++)
            {
                now[u] = i;
                int v = g[u][i].Item1;
                long w = g[u][i].Item2;
                if (w > 0 && depth[v] == depth[u] + 1)
                {
                    long k = DFS(v, Math.Min(limit, w));
                    g[u][i] = (v, w - k);
                    int iv = index[v][u];
                    g[v][iv] = (u, g[v][iv].Item2 + k);
                    flow += k;
                    limit -= k;
                }
            }
            return flow;
        }

        public long Dinic(int s, int t)
        {
            this.s = s; this.t = t;
            long maxFlow = 0;
            while (BFS())
            {
                maxFlow += DFS(s, long.MaxValue);
            }
            return maxFlow;
        }
    }
}
