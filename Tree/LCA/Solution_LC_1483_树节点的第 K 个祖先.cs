public class TreeAncestor
{
    //通用的树的潜规则，树的节点值从0开始连续无重复，否则需要建映射
    //CF里往往是1开始

    //记录节点的深度 根节点为0
    int[] depth;

    //fa[i][j]表示节点i的第(1<<j)个父节点
    //比如fa[3][0]就表示节点3的直接父节点
    //向上超过根节点的为0
    int[][] fa;

    //表示至多向上记录(1<<LOG)层
    const int LOG = 20;
    public TreeAncestor(int n, int[] parent)
    {
        var g = BuildGraph(n, parent);
        depth = new int[n];
        fa = new int[n][];
        for (int i = 0; i < n; i++) fa[i] = new int[LOG + 1];
        //DFS(g, 0, -1);
        BFS(g, parent);
    }

    public int GetKthAncestor(int node, int k)
    {
        if (k > depth[node]) return -1;
        for (int i = LOG; i >= 0; i--)
        {
            if (((k >> i) & 1) == 1)
            {
                node = fa[node][i];
            }
        }
        return node;
    }

    //DFS预处理
    private void DFS(HashSet<int>[] g, int u, int p)
    {
        foreach (int v in g[u])
        {
            if (v == p) continue;
            depth[v] = depth[u] + 1;
            fa[v][0] = u;
            for (int i = 1; i <= LOG; i++)
            {
                fa[v][i] = fa[fa[v][i - 1]][i - 1];
            }
            DFS(g, v, u);
        }
    }

    //防栈溢出可以用BFS
    private void BFS(HashSet<int>[] g, int[] parent)
    {
        int[] vis = new int[g.Length];
        Queue<int> queue = new Queue<int>();
        queue.Enqueue(0);
        vis[0] = 1;
        int d = 0;
        while (queue.Count > 0)
        {
            int count = queue.Count;
            while (count-- > 0)
            {
                int u = queue.Dequeue();
                depth[u] = d;
                if (parent[u] >= 0)
                {
                    fa[u][0] = parent[u];
                    for (int i = 1; i <= LOG; i++)
                    {
                        fa[u][i] = fa[fa[u][i - 1]][i - 1];
                    }
                }
                foreach (int v in g[u])
                {
                    if (vis[v] == 0)
                    {
                        vis[v] = 1;
                        queue.Enqueue(v);
                    }
                }
            }
            d++;
        }
    }

    //基于父节点构建树
    private HashSet<int>[] BuildGraph(int n, int[] parent)
    {
        HashSet<int>[] g = new HashSet<int>[n];
        for (int i = 0; i < n; i++)
        {
            g[i] = new HashSet<int>();
        }
        for (int i = 0; i < parent.Length; i++)
        {
            if (parent[i] >= 0)
            {
                g[i].Add(parent[i]);
                g[parent[i]].Add(i);
            }
        }
        return g;
    }
}

/**
 * Your TreeAncestor object will be instantiated and called as such:
 * TreeAncestor obj = new TreeAncestor(n, parent);
 * int param_1 = obj.GetKthAncestor(node,k);
 */