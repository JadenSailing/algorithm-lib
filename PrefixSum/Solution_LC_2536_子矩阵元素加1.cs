/*
 * 给你一个正整数 n ，表示最初有一个 n x n 、下标从 0 开始的整数矩阵 mat ，矩阵中填满了 0 。

另给你一个二维整数数组 query 。针对每个查询 query[i] = [row1i, col1i, row2i, col2i] ，请你执行下述操作：

找出 左上角 为 (row1i, col1i) 且 右下角 为 (row2i, col2i) 的子矩阵，将子矩阵中的 每个元素 加 1 。也就是给所有满足 row1i <= x <= row2i 和 col1i <= y <= col2i 的 mat[x][y] 加 1 。
返回执行完所有操作后得到的矩阵 mat 。
*/

public class Solution_LC_2536_子矩阵元素加1
{
    public int[][] RangeAddQueries(int n, int[][] queries)
    {
        int[][] diff = new int[n + 1][];
        for (int i = 0; i <= n; i++) diff[i] = new int[n + 1];
        for (int i = 0; i < queries.Length; i++)
        {
            int r1 = queries[i][0];
            int c1 = queries[i][1];
            int r2 = queries[i][2];
            int c2 = queries[i][3];
            diff[r1][c1]++;
            diff[r1][c2 + 1]--;
            diff[r2 + 1][c1]--;
            diff[r2 + 1][c2 + 1]++;
        }
        int[][] ans = new int[n][];
        for (int i = 0; i < n; i++) ans[i] = new int[n];

        //方式1
        /*
        for(int i = 0; i < n; i++)
        {
            for(int j = 0; j < n; j++)
            {
                int left = j > 0 ? diff[i][j - 1] : 0;
                int up = i > 0 ? diff[i - 1][j] : 0;
                int lu = i > 0 && j > 0 ? diff[i - 1][j - 1] : 0;
                diff[i][j] += left + up - lu;
                //一次遍历获取
                ans[i][j] = diff[i][j];
            }
        }
        */

        //方式2
        for (int i = 0; i < n; i++)
        {
            for (int j = 1; j < n; j++)
            {
                diff[i][j] += diff[i][j - 1];
            }
        }
        for (int i = 1; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                diff[i][j] += diff[i - 1][j];
            }
        }
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                ans[i][j] = diff[i][j];
            }
        }

        return ans;
    }
}