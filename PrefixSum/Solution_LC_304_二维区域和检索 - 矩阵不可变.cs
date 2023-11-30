
/*
 * ����һ����ά���� matrix���������͵Ķ������

�������Ӿ��η�Χ��Ԫ�ص��ܺͣ����Ӿ���� ���Ͻ� Ϊ (row1, col1) �����½� Ϊ (row2, col2) ��
ʵ�� NumMatrix �ࣺ

NumMatrix(int[][] matrix) ������������ matrix ���г�ʼ��
int sumRegion(int row1, int col1, int row2, int col2) ���� ���Ͻ� (row1, col1) �����½� (row2, col2) ���������Ӿ����Ԫ�� �ܺ� ��
*/

public class NumMatrix_304
{
    int[][] pre;
    public NumMatrix_304(int[][] matrix)
    {
        int n = matrix.Length, m = matrix[0].Length;
        pre = new int[n + 1][];
        for (int i = 0; i <= n; i++) pre[i] = new int[m + 1];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                pre[i + 1][j + 1] = pre[i + 1][j] + pre[i][j + 1] - pre[i][j] + matrix[i][j];
            }
        }
    }

    public int SumRegion(int row1, int col1, int row2, int col2)
    {
        return pre[row2 + 1][col2 + 1] - pre[row1][col2 + 1] - pre[row2 + 1][col1] + pre[row1][col1];
    }
}

/**
 * Your NumMatrix object will be instantiated and called as such:
 * NumMatrix obj = new NumMatrix(matrix);
 * int param_1 = obj.SumRegion(row1,col1,row2,col2);
 */