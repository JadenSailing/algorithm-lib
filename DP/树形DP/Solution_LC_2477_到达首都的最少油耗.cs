/*
 给你一棵 n 个节点的树（一个无向、连通、无环图），每个节点表示一个城市，编号从 0 到 n - 1 ，且恰好有 n - 1 条路。0 是首都。给你一个二维整数数组 roads ，其中 roads[i] = [ai, bi] ，表示城市 ai 和 bi 之间有一条 双向路 。

每个城市里有一个代表，他们都要去首都参加一个会议。

每座城市里有一辆车。给你一个整数 seats 表示每辆车里面座位的数目。

城市里的代表可以选择乘坐所在城市的车，或者乘坐其他城市的车。相邻城市之间一辆车的油耗是一升汽油。

请你返回到达首都最少需要多少升汽油。
*/

public class Solution_LC_2477_到达首都的最少油耗
{
    public long MinimumFuelCost(int[][] roads, int seats)
    {
        return DFS(BuildGraph(roads), 0, -1, seats)[1];
    }
    private long[] DFS(HashSet<int>[] g, int u, int p, int seats)
    {
        long pCount = 1;
        long cost = 0;
        //依次处理子节点
        foreach (int v in g[u])
        {
            //跳过
            if (v == p) continue;
            long[] child = DFS(g, v, u, seats);
            pCount += child[0];
            cost += child[1] + (child[0] + seats - 1) / seats;
        }
        //返回当前节点的最终数据
        return new long[2] { pCount, cost };
    }
    //标准建图
    private HashSet<int>[] BuildGraph(int[][] edges)
    {
        int n = edges.Length + 1;
        HashSet<int>[] g = new HashSet<int>[n];
        for (int i = 0; i < n; i++) g[i] = new HashSet<int>();
        for (int i = 0; i < edges.Length; i++)
        {
            int L = edges[i][0], R = edges[i][1];
            g[L].Add(R);
            g[R].Add(L);
        }
        return g;
    }
}