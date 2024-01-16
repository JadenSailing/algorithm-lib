
/*
给你一个下标从 0 开始的字符串 s 、字符串 a 、字符串 b 和一个整数 k 。

如果下标 i 满足以下条件，则认为它是一个 美丽下标 ：

0 <= i <= s.length - a.length
s[i..(i + a.length - 1)] == a
存在下标 j 使得：
0 <= j <= s.length - b.length
s[j..(j + b.length - 1)] == b
|j - i| <= k
以数组形式按 从小到大排序 返回美丽下标。

 

示例 1：

输入：s = "isawsquirrelnearmysquirrelhouseohmy", a = "my", b = "squirrel", k = 15
输出：[16,33]
解释：存在 2 个美丽下标：[16,33]。
- 下标 16 是美丽下标，因为 s[16..17] == "my" ，且存在下标 4 ，满足 s[4..11] == "squirrel" 且 |16 - 4| <= 15 。
- 下标 33 是美丽下标，因为 s[33..34] == "my" ，且存在下标 18 ，满足 s[18..25] == "squirrel" 且 |33 - 18| <= 15 。
因此返回 [16,33] 作为结果。
示例 2：

输入：s = "abcd", a = "a", b = "a", k = 4
输出：[0]
解释：存在 1 个美丽下标：[0]。
- 下标 0 是美丽下标，因为 s[0..0] == "a" ，且存在下标 0 ，满足 s[0..0] == "a" 且 |0 - 0| <= 4 。
因此返回 [0] 作为结果。
 

提示：

1 <= k <= s.length <= 5 * 105
1 <= a.length, b.length <= 5 * 105
s、a、和 b 只包含小写英文字母。
*/

public class Solution_LC_3008_找出数组中的美丽下标II
{
    public IList<int> BeautifulIndices(string s, string a, string b, int k)
    {
        // 随机哈希基数和模数，防止被 hack
        Random rand = new Random(DateTime.Now.Millisecond);
        int BASE = 37 + rand.Next() % 107;
        int MOD = (int)1e9 + 7;

        // 求字符串 s 的哈希
        int n = s.Length;
        long[] P = new long[n + 1];
        P[0] = 1;
        for (int i = 1; i <= n; i++) P[i] = P[i - 1] * BASE % MOD;
        long[] h = new long[n + 1];
        h[0] = 0;
        for (int i = 1; i <= n; i++) h[i] = (h[i - 1] * BASE + s[i - 1] - 'a') % MOD;

        // 求子串 s[L..R] 的哈希值
        long Calc(int L, int R)
        {
            return (h[R] - h[L - 1] * P[R - L + 1] % MOD + MOD) % MOD;
        };

        // 用哈希找出 b 在 s 里出现的所有下标
        List<int> vec = new List<int>();
        long HB = 0;
        foreach (char c in b) HB = (HB * BASE + c - 'a') % MOD;
        for (int i = 1, j = b.Length; j <= n; i++, j++) if (Calc(i, j) == HB) vec.Add(i);

        List<int> ans = new List<int>();
        // 用哈希找出 a 在 s 里出现的所有下标
        long HA = 0;
        foreach (char c in a) HA = (HA * BASE + c - 'a') % MOD;
        for (int i = 1, j = a.Length; j <= n; i++, j++)
        {
            if (Calc(i, j) == HA)
            {
                // 在 vec 里二分，看是否存在范围内的数
                int low = 0, high = vec.Count - 1;
                while (low <= high)
                {
                    int mid = low + (high - low) / 2;
                    if (vec[mid] > i + k) high = mid - 1;
                    else low = mid + 1;
                }
                if (high >= 0 && vec[high] >= i - k) ans.Add(i - 1);
            }
        }
        return ans;
    }
}