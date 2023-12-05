/*
 * 给定一个无向、连通的树。树中有 n 个标记为 0...n-1 的节点以及 n-1 条边 。

给定整数 n 和数组 edges ， edges[i] = [ai, bi]表示树中的节点 ai 和 bi 之间有一条边。

返回长度为 n 的数组 answer ，其中 answer[i] 是树中第 i 个节点与所有其他节点之间的距离之和。

*/

public class Solution_LC_834_树中距离之和
{
    int[] ans;
    //root=0时各节点的子树大小 包含自身
    int[] size;
    //root=0时各节点到子节点的距离和
    int[] dis;
    public int[] SumOfDistancesInTree(int n, int[][] edges)
    {
        var g = BuildGraph(n, edges);
        ans = new int[n];
        size = new int[n];
        dis = new int[n];
        DFS0(g, 0, -1);
        DFS(g, 0, -1, dis[0]);
        return ans;
    }

    private void DFS0(HashSet<int>[] g, int u, int p)
    {
        size[u] = 1;
        dis[u] = 0;
        foreach (int v in g[u])
        {
            if (v == p) continue;
            DFS0(g, v, u);
            //子节点数量等于各子节点数量之和
            size[u] += size[v];
            //子节点距离等于各子节点距离 + 各子节点个数(每个均增加了1)
            dis[u] += dis[v] + size[v];
        }
    }

    private void DFS(HashSet<int>[] g, int u, int p, int d)
    {
        int n = g.Length;
        ans[u] = d;
        foreach (int v in g[u])
        {
            if (v == p) continue;
            //从u向v换根过程中
            //距离和增加了(n-size[v]) 减少了size[v]
            DFS(g, v, u, d + (n - size[v]) - size[v]);
        }
    }

    private HashSet<int>[] BuildGraph(int n, int[][] edges)
    {
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