/*
 * ������ n �����࣬���Ƿֱ�� 1 �� n ���б�š�

��һ�ݺ���Ԥ���� bookings �����е� i ��Ԥ����¼ bookings[i] = [firsti, lasti, seatsi] ��ζ���ڴ� firsti �� lasti ������ firsti �� lasti ���� ÿ������ ��Ԥ���� seatsi ����λ��

���㷵��һ������Ϊ n ������ answer�������Ԫ����ÿ������Ԥ������λ������
*/

public class Solution_LC_1109_����Ԥ��ͳ��
{
    public int[] CorpFlightBookings(int[][] bookings, int n)
    {
        int[] diff = new int[n + 1];
        for (int i = 0; i < bookings.Length; i++)
        {
            int L = bookings[i][0] - 1;
            int R = bookings[i][1] - 1;
            int d = bookings[i][2];
            diff[L] += d;
            diff[R + 1] -= d;
        }
        for (int i = 1; i < n; i++)
        {
            diff[i] += diff[i - 1];
        }
        int[] ans = new int[n];
        for (int i = 0; i < n; i++) ans[i] = diff[i];
        return ans;
    }
}