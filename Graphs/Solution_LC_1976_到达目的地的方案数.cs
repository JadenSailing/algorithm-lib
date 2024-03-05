public class Solution_LC_1976_到达目的地的方案数
{
    //注意mod溢出问题
    int mod = (int)1e9 + 7;
    public int CountPaths(int n, int[][] roads)
    {
        //带权邻接表图
        Dictionary<int, int>[] g = new Dictionary<int, int>[n];
        //初始化每个节点
        for (int i = 0; i < n; i++) g[i] = new Dictionary<int, int>();
        foreach (int[] v in roads)
        {
            //无向图标准建图 有重复边可以取最小
            g[v[0]][v[1]] = v[2];
            g[v[1]][v[0]] = v[2];
        }
        //表示到达dp[u]的最小值+方案数 注意元组的使用
        (long, int)[] dp = new (long, int)[n];
        //由于数据范围最短路长度声明为long类型，初始化为long.MaxValue
        Array.Fill(dp, (long.MaxValue, 0));
        //起点最小值为0 方案数为1
        dp[0] = (0, 1);
        //基于优先队列的dijkstra求最短路
        PriorityQueue<(int, long), long> pq = new PriorityQueue<(int, long), long>();
        pq.Enqueue((0, 0), 0);
        while (pq.Count > 0)
        {
            //w代表入队时的路线长度 注意不可取当前最小值
            int u; long w; (u, w) = pq.Dequeue();
            foreach (int v in g[u].Keys)
            {
                //从当前路线到达v的长度
                long d = w + g[u][v];
                if (d > dp[v].Item1) continue;
                //相等时累加
                if (d == dp[v].Item1) dp[v].Item2 = (dp[v].Item2 + dp[u].Item2) % mod;
                //更优时重置
                if (d < dp[v].Item1)
                {
                    dp[v] = (d, dp[u].Item2);
                    pq.Enqueue((v, d), dp[v].Item1);
                }
            }
        }
        return dp[n - 1].Item2;
    }
}