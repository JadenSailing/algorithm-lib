/*
 * ����һ�������� n ����ʾ�����һ�� n x n ���±�� 0 ��ʼ���������� mat �������������� 0 ��

�����һ����ά�������� query �����ÿ����ѯ query[i] = [row1i, col1i, row2i, col2i] ������ִ������������

�ҳ� ���Ͻ� Ϊ (row1i, col1i) �� ���½� Ϊ (row2i, col2i) ���Ӿ��󣬽��Ӿ����е� ÿ��Ԫ�� �� 1 ��Ҳ���Ǹ��������� row1i <= x <= row2i �� col1i <= y <= col2i �� mat[x][y] �� 1 ��
����ִ�������в�����õ��ľ��� mat ��
*/

public class Solution_LC_2536_�Ӿ���Ԫ�ؼ�1
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

        //��ʽ1
        /*
        for(int i = 0; i < n; i++)
        {
            for(int j = 0; j < n; j++)
            {
                int left = j > 0 ? diff[i][j - 1] : 0;
                int up = i > 0 ? diff[i - 1][j] : 0;
                int lu = i > 0 && j > 0 ? diff[i - 1][j - 1] : 0;
                diff[i][j] += left + up - lu;
                //һ�α�����ȡ
                ans[i][j] = diff[i][j];
            }
        }
        */

        //��ʽ2
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