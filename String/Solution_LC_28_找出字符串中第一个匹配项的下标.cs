public class Solution_LC_28_找出字符串中第一个匹配项的下标
{
    public int StrStr(string haystack, string needle)
    {
        List<int> res = KMP(haystack, needle);
        return res.Count == 0 ? -1 : res[0];
    }

    private int[] PI(string s)
    {
        int n = s.Length;
        int[] res = new int[n];
        for (int i = 1; i < n; i++)
        {
            int j = res[i - 1];
            while (j > 0 && s[i] != s[j]) j = res[j - 1];
            if (s[i] == s[j]) j++;
            res[i] = j;
        }
        return res;
    }
    private List<int> KMP(string s, string t)
    {
        int n = s.Length, m = t.Length;
        int[] pi = PI(t);
        List<int> res = new List<int>();
        int j = 0;
        for (int i = 0; i < n; i++)
        {
            while (j > 0 && s[i] != t[j]) j = pi[j - 1];
            if (s[i] == t[j]) j++;
            if (j == m)
            {
                res.Add(i - m + 1);
                j = pi[j - 1];
            }
        }

        return res;
    }
}