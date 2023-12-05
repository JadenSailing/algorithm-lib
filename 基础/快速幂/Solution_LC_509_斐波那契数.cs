/*
 * 斐波那契数 （通常用 F(n) 表示）形成的序列称为 斐波那契数列 。该数列由 0 和 1 开始，后面的每一项数字都是前面两项数字的和。也就是：

F(0) = 0，F(1) = 1
F(n) = F(n - 1) + F(n - 2)，其中 n > 1
给定 n ，请计算 F(n) 。
*/

public class Solution_LC_509_斐波那契数
{
    public int Fib(int n)
    {
        if (n < 2)
        {
            return n;
        }
        int[][] q = new int[2][]
        {
            new int[2] { 1, 1 },
            new int[2] { 1, 0 },
        };
        //[f1, f0] * q = [f1 + f0, f1] = [f2, f1]
        int[][] res = Pow(q, n - 1);
        return res[0][0];
    }
    private int[][] Pow(int[][] mat, int p)
    {
        //单位矩阵
        int[][] ret = new int[2][]
        {
            new int[] { 1, 0 },
            new int[] { 0, 1 },
        };
        while (p > 0)
        {
            if ((p & 1) == 1)
            {
                ret = Mul(ret, mat);
            }
            p >>= 1;
            mat = Mul(mat, mat);
        }
        return ret;
    }

    private int[][] Mul(int[][] x, int[][] y)
    {
        int n = x.Length, p = x[0].Length, m = y[0].Length;
        int[][] res = new int[n][];
        for (int i = 0; i < n; i++) res[i] = new int[m];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                int v = 0;
                for (int k = 0; k < p; k++)
                {
                    v += x[i][k] * y[k][j];
                }
                res[i][j] = v;
            }
        }
        return res;
    }
}