public class Solution_LC_547_省份数量
{
    public int FindCircleNum(int[][] isConnected)
    {
        int n = isConnected.Length;
        UnionFind uf = new UnionFind(n);
        for(int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (isConnected[i][j] == 1) uf.Connect(i, j);
            }
        }
        return uf.setCount;
    }
}

public class UnionFind
{
    public int n = 0;
    private int[] fa;
    public int setCount = 0;
    public UnionFind(int n)
    {
        this.n = n;
        this.setCount = n;
        this.fa = new int[n];
        for (int i = 0; i < n; i++) this.fa[i] = i;
    }

    public int Find(int x)
    {
        return fa[x] == x ? x : (fa[x] = Find(fa[x]));
    }

    public bool IsConnected(int x, int y)
    {
        return Find(x) == Find(y);
    }

    public bool Connect(int x, int y)
    {
        if (IsConnected(x, y)) return false;
        fa[Find(x)] = Find(y);
        setCount--;
        return true;
    }
}