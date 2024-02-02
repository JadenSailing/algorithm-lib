/*
 给定一个字符串 s ，请你找出其中不含有重复字符的 最长子串 的长度。
*/

public class Solution_LC_3_无重复字符的最长子串
{
    public int LengthOfLongestSubstring(string s)
    {
        int n = s.Length;
        int ans = 0;
        HashSet<char> set = new HashSet<char>();
        //枚举右指针,左指针移动
        /*
        int L = 0;
        for (int i = 0; i < n; i++)
        {
            char v = s[i];
            while (set.Contains(v)) set.Remove(s[L++]);
            set.Add(v);
            ans = Math.Max(ans, i - L + 1); //set.Count
        }
        */

        //枚举左指针,右指针移动
        int R = 0;
        for (int i = 0; i < n; i++)
        {
            while (R < n && !set.Contains(s[R]))
            {
                set.Add(s[R++]);
            }
            ans = Math.Max(ans, R - i); //set.Count
            set.Remove(s[i]);
        }

        return ans;
    }
}