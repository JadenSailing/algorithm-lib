/*
 * 这里有 n 个航班，它们分别从 1 到 n 进行编号。

有一份航班预订表 bookings ，表中第 i 条预订记录 bookings[i] = [firsti, lasti, seatsi] 意味着在从 firsti 到 lasti （包含 firsti 和 lasti ）的 每个航班 上预订了 seatsi 个座位。

请你返回一个长度为 n 的数组 answer，里面的元素是每个航班预定的座位总数。
*/

public class Solution_LC_1109_航班预订统计
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