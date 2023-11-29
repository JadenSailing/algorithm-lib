
/*
 * 有 n 个城市，其中一些彼此相连，另一些没有相连。如果城市 a 与城市 b 直接相连，且城市 b 与城市 c 直接相连，那么城市 a 与城市 c 间接相连。

省份 是一组直接或间接相连的城市，组内不含其他没有相连的城市。

给你一个 n x n 的矩阵 isConnected ，其中 isConnected[i][j] = 1 表示第 i 个城市和第 j 个城市直接相连，而 isConnected[i][j] = 0 表示二者不直接相连。

返回矩阵中 省份 的数量。
*/

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