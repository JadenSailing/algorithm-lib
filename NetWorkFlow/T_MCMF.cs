namespace LeetCode
{
    public class MCMF_Dinic
    {
        class Edge
        {
            public int from, to;
            public long flow, cost;
            public Edge(int from, int to, long flow, long cost)
            {
                this.from = from;
                this.to = to;
                this.flow = flow;
                this.cost = cost;
            }
        }

        private int n;
        private List<Edge>[] g;
        private int[][] index;
        private long[] dis;
        private int[] now;
        private int[] vis;
        private int s, t;
        const int MX = int.MaxValue / 2;
        private long minCost;
        public MCMF_Dinic(int n)
        {
            this.n = n;
            g = new List<Edge>[n];
            index = new int[n][];
            dis = new long[n];
            now = new int[n];
            for (int i = 0; i < n; i++)
            {
                g[i] = new List<Edge>();
                index[i] = new int[n];
                Array.Fill(index[i], -1);
            }
        }

        public void AddEdge(int u, int v, long w, long c)
        {
            if (index[u][v] < 0)
            {
                index[u][v] = g[u].Count;
                g[u].Add(new Edge(u, v, w, c));
            }
            if (index[v][u] < 0)
            {
                index[v][u] = g[v].Count;
                g[v].Add(new Edge(v, u, 0, -c));
            }
        }

        private bool SPFA()
        {
            Array.Fill(now, 0);
            Array.Fill(dis, MX);
            int[] vis = new int[n];
            dis[s] = 0;
            vis[s] = 1;
            Queue<long> queue = new Queue<long>();
            queue.Enqueue(s);
            while (queue.Count > 0)
            {
                long u = queue.Dequeue();
                vis[u] = 0;
                for (int i = 0; i < g[u].Count; i++)
                {
                    var e = g[u][i];
                    long d = dis[u] + e.cost;
                    if (e.flow > 0 && dis[e.to] > d)
                    {
                        dis[e.to] = d;
                        if (vis[e.to] == 0)
                        {
                            queue.Enqueue(e.to);
                            vis[e.to] = 1;
                        }
                    }
                }
            }
            return dis[t] != MX;
        }

        private long DFS(int u, long limit)
        {
            if (u == t || limit == 0) return limit;
            long flow = 0;
            vis[u] = 1;
            for (int i = now[u]; i < g[u].Count; i++)
            {
                now[u] = i;
                var e = g[u][i];
                if (vis[e.to] == 0 && e.flow > 0 && dis[e.to] == dis[u] + e.cost)
                {
                    long k = DFS(e.to, Math.Min(limit, e.flow));
                    e.flow -= k;
                    int iv = index[e.to][u];
                    g[e.to][iv].flow += k;
                    flow += k;
                    limit -= k;
                    minCost += e.cost * k;
                }
            }
            vis[u] = 0;
            return flow;
        }

        public (long, long) Dinic(int s, int t)
        {
            this.s = s; this.t = t;
            this.minCost = 0;
            long maxFlow = 0;
            this.vis = new int[n];
            while (SPFA())
            {
                maxFlow += DFS(s, long.MaxValue);
            }
            return (maxFlow, minCost);
        }
    }
}
