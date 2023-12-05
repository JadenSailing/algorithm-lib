/*
 * 给你两个正整数 left 和 right ，请你找到两个整数 num1 和 num2 ，它们满足：

left <= nums1 < nums2 <= right  。
nums1 和 nums2 都是 质数 。
nums2 - nums1 是满足上述条件的质数对中的 最小值 。
请你返回正整数数组 ans = [nums1, nums2] 。如果有多个整数对满足上述条件，请你返回 nums1 最小的质数对。如果不存在符合题意的质数对，请你返回 [-1, -1] 。

如果一个整数大于 1 ，且只能被 1 和它自己整除，那么它是一个质数。
*/

public class Solution_LC_2523_范围内最接近的两个质数
{
    public int[] ClosestPrimes(int left, int right)
    {
        List<int> primes = new List<int>();
        //是否是非质数的标志
        bool[] flag = new bool[right + 1];
        //0 1特殊处理非质数
        flag[0] = flag[1] = true;
        for (int i = 2; i <= right; i++)
        {
            if (flag[i] == false)
            {
                for (int j = i * 2; j <= right; j += i)
                {
                    flag[j] = true;
                }
            }
        }
        for (int i = 2; i <= right; i++)
        {
            if (flag[i] == false) primes.Add(i);
        }
        int max = int.MaxValue;
        int[] ans = new int[] { -1, -1 };
        for (int i = 0; i < primes.Count - 1; i++)
        {
            if (primes[i] >= left && primes[i + 1] <= right)
            {
                int d = primes[i + 1] - primes[i];
                if (d < max)
                {
                    max = d;
                    ans[0] = primes[i];
                    ans[1] = primes[i + 1];
                }
            }
        }
        return ans;
    }
}