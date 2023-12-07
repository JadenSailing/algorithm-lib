public class TreeAncestor
{
    //ͨ�õ�����Ǳ�������Ľڵ�ֵ��0��ʼ�������ظ���������Ҫ��ӳ��
    //CF��������1��ʼ

    //��¼�ڵ����� ���ڵ�Ϊ0
    int[] depth;

    //fa[i][j]��ʾ�ڵ�i�ĵ�(1<<j)�����ڵ�
    //����fa[3][0]�ͱ�ʾ�ڵ�3��ֱ�Ӹ��ڵ�
    //���ϳ������ڵ��Ϊ0
    int[][] fa;

    //��ʾ�������ϼ�¼(1<<LOG)��
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

    //DFSԤ����
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

    //��ջ���������BFS
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

    //���ڸ��ڵ㹹����
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