# AutumnMist's Algorithm Library
[分类题单](List.md)
## C# Algorithm Contest IO Project
- 可用宏区分ACM模式或核心代码模式
- 工程主类里处理了ACM模式下的常用输入输出   [Program](https://github.com/JadenSailing/algorithm-lib/blob/main/Program.cs)
- LeetCodeSolution目录下是核心代码模式处理(主要用于LeetCode比赛)
  - 基于C#反射，处理各种情况的输入输出 [LeetCodeSolution](https://github.com/JadenSailing/algorithm-lib/tree/main/LeetCodeSolution)
  - 可直接全选复制粘贴运行
## 基础
### GCD&LCM
最大公约数和最小公倍数
```
public static long GCD(long a, long b) { return b == 0 ? a : GCD(b, a % b); }
public static long LCM(long a, long b) { return a / GCD(a, b) * b; }
```
### 快速幂
- 快速幂模板
```
public static long Power(long x, long y, long p)
{
    long res = 1L;
    x %= p; //注意
    while(y > 0)
    {
        if (y % 2 == 1) res = res * x % p;
        y /= 2;
        x = x * x % p;
    }
    return res;
}
```

- 矩阵快速幂
[3337. 字符串转换后的长度 II](https://leetcode.cn/problems/total-characters-in-string-after-transformations-ii/)
[2912. 在网格上移动到目的地的方法数](https://leetcode.cn/problems/number-of-ways-to-reach-destination-in-the-grid/)
```
public class Solution
{
    int mod = (int)1e9 + 7;
    public int NumberOfWays(int n, int m, int k, int[] source, int[] dest)
    {
        //与终点是否是 相同点/同行/同列/均不同 的方案数
        long[][] f = new long[][] { new long[4]};
        //初值
        bool fx = source[0] == dest[0];
        bool fy = source[1] == dest[1];
        if (fx && fy) f[0][0] = 1;
        else if (fx && !fy) f[0][1] = 1;
        else if (!fx && fy) f[0][2] = 1;
        else f[0][3] = 1;
        long[][] mat = new long[4][]
        {
            new long[] {0, m - 1, n - 1, 0 },
            new long[] {1, m - 2, 0, n - 1 }, 
            new long[] {1, 0, n - 2, m - 1 }, 
            new long[] {0, 1, 1, (n - 2) + (m - 2) }, 
        };
        f = Mul(f, Power(mat, k, mod), mod);
        return (int)f[0][3];
    }

    private long[][] Power(long[][] mat, long p, long mod)
    {
        p %= mod;
        int size = mat.Length;
        //单位矩阵
        long[][] ret = new long[size][];
        for (int i = 0; i < size; i++)
        {
            ret[i] = new long[size];
            ret[i][i] = 1;
        }
        while (p > 0)
        {
            if ((p & 1) == 1)
            {
                ret = Mul(ret, mat, mod);
            }
            p >>= 1;
            mat = Mul(mat, mat, mod);
        }
        return ret;
    }
    private long[][] Mul(long[][] x, long[][] y, long mod)
    {
        int n = x.Length, p = x[0].Length, m = y[0].Length;
        long[][] res = new long[n][];
        for (int i = 0; i < n; i++) res[i] = new long[m];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                long v = 0;
                for (int k = 0; k < p; k++)
                {
                    v += (1L * x[i][k] * y[k][j] % mod);
                    v %= mod;
                }
                res[i][j] = v;
            }
        }
        return res;
    }
    public static long Power(long x, long y, long p)
    {
        long res = 1L;
        while (y > 0)
        {
            if (y % 2 == 1) res = res * x % p;
            y /= 2;
            x = x * x % p;
        }
        return res;
    }
}
```
矩阵快速幂求[斐波那契数](https://github.com/JadenSailing/algorithm-lib/blob/main/%E5%9F%BA%E7%A1%80/%E5%BF%AB%E9%80%9F%E5%B9%82/Solution_LC_509_%E6%96%90%E6%B3%A2%E9%82%A3%E5%A5%91%E6%95%B0.cs)

### 逆元
费马小定理 a^p ≡ a (mod p)
```
Power(a, p, p) = a % p
Power(a, p - 1, p) = 1
Power(a, p - 2, p) = (1 / a) % p
a / b % p= a * Power(b, p - 2, p) % p
```
### 高精度
- 高精度乘法
```
//高精度乘法 AB均为非负整数
public static string Mul(string A, string B)
{
    const int MX = (int)1e5;
    int[] listA = new int[MX], listB = new int[MX], listC = new int[MX];
    int nA = A.Length, nB = B.Length;
    //A = "123", listA = [0 3 2 1 0 ...]
    for (int i = nA - 1; i >= 0; i--) listA[nA - i] = A[i] - '0';
    for (int i = nB - 1; i >= 0; i--) listB[nB - i] = B[i] - '0';
    //Ai * Bj = Ci+j-1
    for (int i = 1; i <= nA; i++) for (int j = 1; j <= nB; j++) listC[i + j - 1] += listA[i] * listB[j];
    //进位
    for (int i = 1; i <= nA + nB; i++)
    {
        listC[i + 1] += listC[i] / 10;
        listC[i] %= 10;
    }
    StringBuilder s = new StringBuilder();
    if (listC[nA + nB] > 0) s.Append(listC[nA + nB]);//判断第i+j位上的数字是不是0
    for (int i = nA + nB - 1; i >= 1; i--) s.Append(listC[i]);
    return s.ToString();
}
```
- 高精度加法
```
//高精度加法 AB均为非负整数
public static string Sum(string A, string B)
{
    const int MX = (int)1e5;
    int[] listA = new int[MX], listB = new int[MX], listC = new int[MX];
    int nA = A.Length, nB = B.Length;
    for (int i = nA - 1; i >= 0; i--) listA[nA - i] = A[i] - '0';
    for (int i = nB - 1; i >= 0; i--) listB[nB - i] = B[i] - '0';
    int n = Math.Max(nA, nB) + 1;
    for (int i = 1; i < n; i++)
    {
        listC[i] += listA[i] + listB[i]; //注意不是=
        listC[i + 1] += listC[i] / 10;
        listC[i] %= 10;
    }
    StringBuilder s = new StringBuilder();
    if (listC[n] > 0) s.Append(listC[n]);
    for (int i = n - 1; i >= 1; i--) s.Append(listC[i]);
    return s.ToString();
}
```

### 区间交集
```
//区间[a,b]和[c,d] (a<=b, c<=d)的交集
int overlapping = 1
r = Math.Max((Math.Min(b, d) - Math.Max(a, c)) + overlapping, 0);
```
### 前缀和
- 一维前缀和 
```
int[] pre = new int[n + 1];
for(int i = 0; i < n; i++) pre[i + 1] = pre[i] + nums[i];
```
```
//区间和
[L, R](0 <= L, R < n) = pre[R + 1]- pre[L];
```
[区域和检索-数组不可变](https://github.com/JadenSailing/algorithm-lib/blob/main/PrefixSum/Solution_LC_303_%E5%8C%BA%E5%9F%9F%E5%92%8C%E6%A3%80%E7%B4%A2%20-%20%E6%95%B0%E7%BB%84%E4%B8%8D%E5%8F%AF%E5%8F%98.cs)

[462. 最小操作次数使数组元素相等 II](https://leetcode.cn/problems/minimum-moves-to-equal-array-elements-ii/)
标准做法中位数贪心即可 这里提供枚举所有下标的做法
```
//前缀和枚举下标做法
long ans = long.MaxValue;
for (int i = 0; i < n; i++)
{
    long v = nums[i];
    long L = v * i- sum[i]; //i之前差额
    long R = sum[n]- sum[i]- v * (n- i); //i之后(含i)差额
    ans = Math.Min(ans, L + R);
}
```
```
//迭代枚举
long pre = nums[0];
long res = total- 1L * nums[0] * n; //注意此处int溢出
long ans = res;
for (int i = 1; i < n; i++)
{
    long v = nums[i];
    long L = i * (v- pre);
    long R = (n- i) * (v- pre);
    res = res + L- R;
    ans = Math.Min(ans, res);
    pre = v;
}
```
 
- 二维前缀和

 $二维前缀和pre[i][j]定义为从(0,0)到(i,j)位置围成的矩形范围内的元素和$
 
```
//构建前缀和
int[][] pre = new int[n + 1][];
for (int i = 0; i <= n; i++) pre[i] = new int[m + 1];
for (int i = 0; i < n; i++)
{
	for (int j = 0; j < m; j++)
	{
		pre[i + 1][j + 1] = pre[i + 1][j] + pre[i][j + 1]- pre[i][j] + matrix[i][j];
	}
}
```
```
//另外一种构建方式
int[][] pre = new int[n + 1][];
for(int i = 0; i <= n; i++) pre[i] = new int[m + 1];
//初始化为对应位置的元素
for(int i = 0; i < n; i++)
{
	for(int j = 0; j < m; j++)
	{
		pre[i + 1][j + 1] = matrix[i][j];
	}
}
//从左向右累加
for(int i = 0; i < n; i++)
{
	for(int j = 0; j < m; j++)
	{
		pre[i + 1][j + 1] += pre[i + 1][j];
	}
}
//从上向下累加
for(int i = 0; i < n; i++)
{
	for(int j = 0; j < m; j++)
	{
		pre[i + 1][j + 1] += pre[i][j + 1];
	}
}
```
```
//计算任意矩形元素和
public int SumRegion(int row1, int col1, int row2, int col2)
{
    return pre[row2 + 1][col2 + 1]- pre[row1][col2 + 1]- pre[row2 + 1][col1] + pre[row1][col1];
}
```
 
 [二维区域和-矩阵不可变](https://github.com/JadenSailing/algorithm-lib/blob/main/PrefixSum/Solution_LC_304_%E4%BA%8C%E7%BB%B4%E5%8C%BA%E5%9F%9F%E5%92%8C%E6%A3%80%E7%B4%A2%20-%20%E7%9F%A9%E9%98%B5%E4%B8%8D%E5%8F%AF%E5%8F%98.cs)
 
- 前后缀分解
 预处理前缀和后缀信息 然后枚举每一项

[42. 接雨水](https://leetcode.cn/problems/trapping-rain-water/)
```
//L[i]和R[i]表示i之前和之后的最大值
for(int i = 0; i < n; i++)
{
	//注意是两侧高度的较小值-当前格子的高度 大于0的部分才可以接雨水
	ans += Math.Max(0, Math.Min(L[i], R[i])- height[i]);
}
```
 
 
### 差分
- 一维差分
```
 int[] diff = new int[n + 1]; //防越界
//[L,R](0 <= L <= R < n)
diff[L] += d;
diff[R + 1] -= d;
for(int i = 1; i < n; i++)
{
	//还原原数组
	diff[i] += diff[i- 1];
}
```
 [航班预订统计](https://github.com/JadenSailing/algorithm-lib/blob/main/PrefixSum/Solution_LC_1109_%E8%88%AA%E7%8F%AD%E9%A2%84%E8%AE%A2%E7%BB%9F%E8%AE%A1.cs)
 
- 二维差分
```
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
```
 [子矩阵元素加1](https://github.com/JadenSailing/algorithm-lib/blob/main/PrefixSum/Solution_LC_2536_%E5%AD%90%E7%9F%A9%E9%98%B5%E5%85%83%E7%B4%A0%E5%8A%A01.cs)
### 离散化
用于压缩稀疏数据
```
List<int> list = new List<int>(new HashSet<int>(nums));
list.Sort();
Dictionary<int, int> raw = new Dictionary<int, int>();
for(int i = 0; i < list.Count; i++)
{
    raw[list[i]] = i;
}
```
### 正难则反
一种解题思路 正向可能会非常难的情况下 反向可能会很简单 
- 比如出现`至少x个xxx`一类的描述

[猴子碰撞的方法数](https://leetcode.cn/problems/count-collisions-of-monkeys-on-a-polygon/)
```
public int MonkeyMove(int n) {
	//特别注意对mod执行±运算后 需要再次mod
    long res = Power(2, n, mod)- 2;
    res = (res + mod) % mod;
    return (int)res;
}
```
[至少有1位重复的数字](https://leetcode.cn/problems/numbers-with-repeated-digits/)

- 或者带有`覆盖`性质的题目

[查询后矩阵的和](https://leetcode.cn/problems/sum-of-matrix-after-queries/)

[C. Balanced Stone Heaps](https://codeforces.com/problemset/problem/1623/C) 
数组从i=3遍历到n 每次选择a[i]，取出任意3k，a[i-1] += k， a[i-2] += k*2 求min(a)最大值
[题解](https://codeforces.com/contest/1623/submission/254784442)


## 数组
 
## 哈希表

## 栈
### 单调栈
 
 
## 队列
 
## 链表
 [206. 反转链表](https://leetcode.cn/problems/reverse-linked-list/)
```
public ListNode ReverseList(ListNode head) 
{
	ListNode pre = null;
	while(head != null)
	{
		(pre, head, head.next) = (head, head.next, pre);
	}
	return pre;
}
```
## 堆
- 基础模板
```
public class Heap
{
    private bool mIsMinHeap = false;
    private List<int> list = new List<int>() { 0 };
    private int size = 0;
    public int Pop()
    {
        int res = list[1];
        //移除堆顶 把最后一个元素置顶 然后下滤
        list[1] = list[size];
        size--;
        SiftDown(1);
        return res;
    }
    public void Push(int x)
    {
        if (size == list.Count- 1)
        {
            list.Add(x);
            size++;
        }
        else list[size] = x;
        //新增元素放在末尾 然后上浮
        SiftUp(size);
    }
    private void SiftUp(int i)
    {
        while (i / 2 > 0)
        {
            int p = i / 2;
            if (list[i] <= list[p]) break;
            (list[i], list[p]) = (list[p], list[i]);
            i /= 2;
        }
    }
    private void SiftDown(int i)
    {
        while (i * 2 <= size)
        {
            int c = i * 2;
            if (c + 1 <= size && list[c + 1] > list[c]) c++;
            if (list[i] >= list[c]) break;
            (list[i], list[c]) = (list[c], list[i]);
            i = c;
        }
    }
}
```
[数组第K大元素](https://github.com/JadenSailing/algorithm-lib/blob/main/Heap/Solution_LC_215_%E6%95%B0%E7%BB%84%E4%B8%AD%E7%9A%84%E7%AC%ACK%E4%B8%AA%E6%9C%80%E5%A4%A7%E5%85%83%E7%B4%A0.cs)
- 堆排序的处理
	- 原地修改
	- Heapify from n / 2 -> 0
	- Pop & Swap(1, Size--)

- 对顶堆
[数据流的中位数](https://github.com/JadenSailing/algorithm-lib/blob/main/Heap/Solution_LC_295_%E6%95%B0%E6%8D%AE%E6%B5%81%E7%9A%84%E4%B8%AD%E4%BD%8D%E6%95%B0.cs)

- 对顶Map 
[3321. 计算子数组的 x-sum II](https://leetcode.cn/problems/find-x-sum-of-all-k-long-subarrays-ii/)
```
public class MinMaxHeap<T> where T : IComparable, IEquatable<T>
{
    private Map<T> kHeap = new Map<T>();
    private Map<T> oHeap = new Map<T>();
    private long sum = 0;
    private int k = 1;
    private Func<T, long> calc;
    public MinMaxHeap(int k, Func<T, long> calcSum = null)
    {
        this.k = k;
        this.calc = calcSum;
    }
    private void Refresh()
    {
        while(kHeap.Count > k)
        {
            var node = kHeap.Kth(0);
            kHeap.Delete(node);
            if (calc != null) sum -= calc(node);
            oHeap.Insert(node);
        }
        while (kHeap.Count < k && oHeap.Count > 0)
        {
            var node = oHeap.Kth(oHeap.Count - 1);
            oHeap.Delete(node);
            kHeap.Insert(node);
            if(calc != null) sum += calc(node);
        }

    }
    public long Sum() { return sum; }
    public T TopK() { return kHeap.Kth(0); }

    public void Insert(T node)
    {
        if (kHeap.Count < k || kHeap.Kth(0).CompareTo(node) < 0)
        {
            kHeap.Insert(node);
            if (calc != null) sum += calc(node);
        }
        else oHeap.Insert(node);
        Refresh();
    }
    public void Delete(T node)
    {
        if(kHeap.Contains(node))
        {
            kHeap.Delete(node);
            if (calc != null) sum -= calc(node);
        }
        if (oHeap.Contains(node)) oHeap.Delete(node);
        Refresh();
    }
}
```

- 延迟删除
队列元素额外存储标记数据 检测是额外处理标记是否合法。
[3092. 最高频率的 ID](https://leetcode.cn/problems/most-frequent-ids/)
```
for (int i = 0; i < n; i++)
{
    int idx = nums[i];
    if (!dict.ContainsKey(idx)) dict[idx] = 0;
    dict[idx] += freq[i];
    pq.Enqueue((idx, dict[idx]), -dict[idx]);
    //延迟标记的处理 延迟删除复杂度跟加入队列次数相关
    //仅校验个数有可能出现错误的元素使用正确的数据 本题不影响
    //可以使用时间戳严格区分有效元素
    while (pq.Count > 0 && pq.Peek().Item2 != dict[pq.Peek().Item1]) pq.Dequeue();
    if (pq.Count > 0) ans[i] = pq.Peek().Item2;
}
```
[F. Kirill and Mushrooms](https://codeforces.com/contest/1945/problem/F)
[题解](https://codeforces.com/contest/1945/submission/252407275)

 
## 字符串
### 字符串哈希
多项式的哈希算法，双哈希优化 
标准模板如下，注意：
保持BASE和MOD全局统一
每个需要Hash的字符串创建一个StringHash实例
`MOD` 单hash下数据量较大时2e5+有case无法通过，所以改成双哈希。
```
public class StringHash
{
    long MOD = (long)1e9 + 7;
    CalcHash h1, h2;
    public StringHash(string s)
    {
        h1 = new CalcHash(s, 0);
        h2 = new CalcHash(s, 1);
    }

    // 求子串 s[L..R] 的哈希值 L,R ∈ [1,n]
    public long Hash(int L, int R)
    {
        return h1.Hash(L, R) * MOD + h2.Hash(L, R);
    }
    private class CalcHash
    {
        static Random rand;
        static long[] BASES, MODS;
        long[] P, h;
        long BASE, MOD;
        static CalcHash()
        {
            // 随机哈希基数和模数，防止被 hack
            rand = new Random(DateTime.Now.Millisecond);
            BASES = new long[2];
            BASES[0] = 37 + rand.Next() % 107;
            BASES[1] = 37 + rand.Next() % 107;
            MODS = new long[2];
            MODS[0] = (long)1e9 + 7;
            MODS[1] = 998244353;
        }

        public CalcHash(string s, int idx)
        {
            BASE = BASES[idx];
            MOD = MODS[idx];
            // 求字符串 s 的哈希
            int n = s.Length;
            P = new long[n + 1];
            P[0] = 1;
            for (int i = 1; i <= n; i++) P[i] = P[i - 1] * BASE % MOD;
            h = new long[n + 1];
            h[0] = 0;
            for (int i = 1; i <= n; i++) h[i] = (h[i - 1] * BASE + s[i - 1] - 'a' + 1) % MOD;
        }

        public long Hash(int L, int R)
        {
            return (h[R] - h[L - 1] * P[R - L + 1] % MOD + MOD) % MOD;
        }
    }
}
```
[找出数组中的美丽下标 II](https://github.com/JadenSailing/algorithm-lib/blob/main/String/Solution_LC_3008_%E6%89%BE%E5%87%BA%E6%95%B0%E7%BB%84%E4%B8%AD%E7%9A%84%E7%BE%8E%E4%B8%BD%E4%B8%8B%E6%A0%87II.cs)

### KMP
复杂度`O(n+m)`
```
//计算next数组
private int[] PI(string s)
{
    int n = s.Length;
    int[] res = new int[n];
    for (int i = 1; i < n; i++)
    {
        int j = res[i- 1];
        while (j > 0 && s[i] != s[j]) j = res[j- 1];
        if (s[i] == s[j]) j++;
        res[i] = j;
    }
    return res;
}
//KMP
private List<int> KMP(string s, string t)
{
    int n = s.Length, m = t.Length;
    int[] pi = PI(t);
    List<int> res = new List<int>();
    int j = 0;
    for (int i = 0; i < n; i++)
    {
        while (j > 0 && s[i] != t[j]) j = pi[j- 1];
        if (s[i] == t[j]) j++;
        if (j == m)
        {
            res.Add(i- m + 1);
            j = pi[j- 1];
        }
    }
    return res;
}
```
[找出字符串中第一个匹配项的下标](https://leetcode.cn/problems/find-the-index-of-the-first-occurrence-in-a-string/)

```
public int StrStr(string haystack, string needle)
{
    List<int> res = KMP(haystack, needle);
    return res.Count == 0 ? -1 : res[0];
}
```
### 最小表示法
循环同构字符串中返回字典序最小的位置
```
private static int GetMinIndex(string s)
{
    int n = s.Length;
    char[] chs = s.ToCharArray();
    int k = 0, i = 0, j = 1;
    while (k < n && i < n && j < n)
    {
        if (chs[(i + k) % n] == chs[(j + k) % n])
        {
            k++;
        }
        else
        {
            if (chs[(i + k) % n] > chs[(j + k) % n]) i = i + k + 1;
            else j = j + k + 1;
            if (i == j) i++;
            k = 0;
        }
    }
    return Math.Min(i, j);
}
```
[899. 有序队列](https://leetcode.cn/problems/orderly-queue/)

## 排序
### 冒泡排序
 原理：每轮遍历中，逐个向后比较，将更大的数向后冒泡，持续N轮

```
int n = nums.Length;
for(int i = 0; i < n; i++)
{
    for(int j = 0; j < n- i- 1; j++)
    {
        if (nums[j] > nums[j + 1]) (nums[j], nums[j + 1]) = (nums[j + 1], nums[j]);
    }
}
```
```
 //优化
 int last = n- 1;
 while(last > 0)
 {
     int now = 0;
     for(int j = 0; j < last; j++)
     {
         if (nums[j] > nums[j + 1])
         {
             now = j + 1;
             (nums[j], nums[j + 1]) = (nums[j + 1], nums[j]);
         }
     }
     last = now;
 }
```
 
### 选择排序
 原理：每轮选择最小的下标与当前首位交换
```
 int n = nums.Length;
for(int i = 0; i < n; i++)
{
    int min = i;
    for(int j = i + 1; j < n; j++)
    {
        if (nums[j] < nums[min]) min = j;
    }
    (nums[i], nums[min]) = (nums[min], nums[i]);
}
```
>冒泡和选择排序的比较
都是把当前剩余的最值添加到已排序的末尾
区别是冒泡排序每轮交换N次，而选择排序只是记录最值下标
### 插入排序
原理：逐个选择未排序的元素向已排序列表中插入
```
int n = nums.Length;
for(int i = 1; i < n; i++)
{
    for(int j = i; j > 0; j--)
    {
        if (nums[j] < nums[j- 1]) (nums[j- 1], nums[j]) = (nums[j], nums[j- 1]);
        else break;
    }
}
```
### 快速排序
- 标准模板(中位数)
```
private void QuickSort(int[] nums, int L, int R)
{
    if (L >= R) return;
    int M = Partition(nums, L, R);
    QuickSort(nums, L, M - 1);
    QuickSort(nums, M + 1, R);
}
private int Partition(int[] nums, int L, int R)
{
    int m = Medium(nums, L, R);
    (nums[L], nums[m]) = (nums[m], nums[L]);
    int i = L, j = R, v = nums[L];
    while (i < j)
    {
        //下面两行顺序不能修改
        while (i < j && nums[j] >= v) j--;
        while (i < j && nums[i] <= v) i++;
        (nums[i], nums[j]) = (nums[j], nums[i]);
    }
    (nums[L], nums[i]) = (nums[i], nums[L]);
    return i;
}
private int Medium(int[] nums, int L, int R)
{
    int M = L + (R - L) / 2;
    if (nums[L] <= nums[M] && nums[L] >= nums[R]) return L;
    if (nums[R] <= nums[M] && nums[R] >= nums[L]) return R;
    return M;
}
```
- acwing标准模板
```
private void QuickSort(int[] nums, int L, int R)
{
    if (L >= R) return;
    int i = L- 1, j = R + 1, x = nums[(L + R) / 2];
    while (i < j)
    {
        do i++; while (nums[i] < x);
        do j--; while (nums[j] > x);
        if (i < j) (nums[i], nums[j]) = (nums[j], nums[i]);
    }
    QuickSort(nums, L, j);
    QuickSort(nums, j + 1, R);
}
```
- 快速选择 
均摊复杂度O(n)
```
//第k小元素 k∈[0, n- 1]
public int QuickSelect(int[] nums, int L, int R, int k)
{
    if (L == R) return nums[k];
    int x = nums[L], i = L- 1, j = R + 1;
    while (i < j)
    {
        do i++; while (nums[i] < x);
        do j--; while (nums[j] > x);
        if (i < j) (nums[i], nums[j]) = (nums[j], nums[i]);
    }
    if (k <= j) return QuickSelect(nums, L, j, k);
    else return QuickSelect(nums, j + 1, R, k);
}
```

### 堆排序
 参见堆的介绍
### 归并排序
```
//int n = nums.Length;
//MergeSort(nums, new int[n], 0, n- 1);
private void MergeSort(int[] nums, int[] tmp, int L, int R)
{
    if (L >= R) return;
    int mid = L + (R- L) / 2;
    MergeSort(nums, tmp, L, mid);
    MergeSort(nums, tmp, mid + 1, R);
    int i = L, j = mid + 1, t = L;
    while (i <= mid && j <= R)
    {
        if (nums[i] <= nums[j]) tmp[t++] = nums[i++];
        else tmp[t++] = nums[j++];
    }
    while (i <= mid) tmp[t++] = nums[i++];
    while (j <= R) tmp[t++] = nums[j++];
    //for (int k = L; k <= R; k++) nums[k] = tmp[k];
    Array.Copy(tmp, L, nums, L, R- L + 1);
}
```
### 基数排序
### 桶排序

## 二分
- 基础模板 [搜索插入位置](https://github.com/JadenSailing/algorithm-lib/blob/main/BinarySearch/Solution_LC_35_%E6%90%9C%E7%B4%A2%E6%8F%92%E5%85%A5%E4%BD%8D%E7%BD%AE.cs)
```
int low = 0, high = n- 1;
while(low <= high)
{
	int mid = low + (high- low) / 2;
	if(Check()) high = mid- 1
	else low = mid + 1
}
return low
```

## 双指针
 某些情况称为滑动窗口
 [无重复字符的最长子串](https://github.com/JadenSailing/algorithm-lib/blob/main/TwoPointers/Solution_LC_3_%E6%97%A0%E9%87%8D%E5%A4%8D%E5%AD%97%E7%AC%A6%E7%9A%84%E6%9C%80%E9%95%BF%E5%AD%90%E4%B8%B2.cs)
 

## 分治

### 根号分治
### 四叉树

## 贪心

### 反悔贪心
- 反悔堆
[课程表III](https://github.com/JadenSailing/algorithm-lib/blob/main/Greedy/Solution_LC_630_%E8%AF%BE%E7%A8%8B%E8%A1%A8III.cs)

## 树

### 遍历
树的前序中序和后序遍历
经典问题 [105. 从前序与中序遍历序列构造二叉树](https://leetcode.cn/problems/construct-binary-tree-from-preorder-and-inorder-traversal/)

前序遍历第一个位置是根节点，找到中序中根节点位置，按照个数对前序和中序分别分段，递归处理。
注意：所有节点值必须唯一，另外从前序和后序是无法唯一还原的
```
public TreeNode BuildTree(int[] preorder, int[] inorder)
{
    int n = preorder.Length;
    Dictionary<int, int> indexDict = new Dictionary<int, int>();
    for (int i = 0; i < n; i++) indexDict[inorder[i]] = i;
    return DFS(preorder, inorder, indexDict, 0, n- 1, 0, n- 1);
}

private TreeNode DFS(int[] preorder, int[] inorder, Dictionary<int, int> indexDict, int pL, int pR, int iL, int iR)
{
    int val = preorder[pL];
    TreeNode root = new TreeNode(val);
    int mid = indexDict[val];
    if (mid > iL) root.left = DFS(preorder, inorder, indexDict, pL + 1, pL + 1 + mid- iL- 1, iL, mid- 1);
    if (mid < iR) root.right = DFS(preorder, inorder, indexDict, pL + 1 + mid- iL, pR, mid + 1, iR);
    return root;
}
```
[889. 根据前序和后序遍历构造二叉树](https://leetcode.cn/problems/construct-binary-tree-from-preorder-and-postorder-traversal/)
```
public TreeNode ConstructFromPrePost(int[] preorder, int[] postorder)
{
    int n = preorder.Length;
    Dictionary<int, int> indexDict = new Dictionary<int, int>();
    for (int i = 0; i < n; i++) indexDict[postorder[i]] = i;
    return DFS(preorder, postorder, indexDict, 0, n- 1, 0, n- 1);
}

private TreeNode DFS(int[] preorder, int[] postorder, Dictionary<int, int> indexDict, int prL, int prR, int poL, int poR)
{
    TreeNode root = new TreeNode(preorder[prL]);
    if (prR > prL)
    {
        int idx = indexDict[preorder[prL + 1]];
        root.left = DFS(preorder, postorder, indexDict, prL + 1, prL + 1 + idx- poL, poL, idx);
        if (idx < poR- 1) root.right = DFS(preorder, postorder, indexDict, prL + 1 + idx- poL + 1, prR, idx + 1, poR- 1);
    }
    return root;
}
```

### 树的直径
- 两边DFS 任意点出发最远端是直径上一点，无法处理负边
- 树形dp 计算所有节点的左右子树长度和
 
 [树的直径](https://github.com/JadenSailing/algorithm-lib/blob/main/Tree/Solution_LC_543_%E4%BA%8C%E5%8F%89%E6%A0%91%E7%9A%84%E7%9B%B4%E5%BE%84.cs)
### 树的重心
树的重心如果不唯一，则至多有两个，且这两个重心相邻。
以树的重心为根时，所有子树的大小都不超过整棵树大小的一半。
简单树形DP即可
```
//sz[u]表示根节点为0时，u节点的子树大小+1(自身)
private static void DFS(int u, int p)
{
	sz[u] = 1;
	int max = 0;
	foreach (int v in g[u])
	{
		if (v == p) continue;
		DFS(v, u);
		max = Math.Max(max, sz[v]);
		sz[u] += sz[v];
	}
	max = Math.Max(max, n - sz[u]);
	if (max <= n / 2) ans = max;
}
```

 
### LCA
- 倍增计算节点的第K个父节点
```
 //更新子节点的fa数据 倍增更新
 //如i的第4个父节点=i的第2个节点的第2个父节点
 fa[v][0] = u;
 for (int i = 1; i <= LOG; i++)
 {
     fa[v][i] = fa[fa[v][i- 1]][i- 1];
 }
```
```
 //计算第k个父节点
 public int GetKthAncestor(int node, int k)
 {
     if (k > depth[node]) return -1;
     for (int i = LOG; i >= 0; i--)
     {
         if (((k >> i) & 1) == 1)
         {
             node = fa[node][i];
         }
     }
     return node;
 }
```
 [树节点的第 K 个祖先](https://github.com/JadenSailing/algorithm-lib/blob/main/Tree/LCA/Solution_LC_1483_%E6%A0%91%E8%8A%82%E7%82%B9%E7%9A%84%E7%AC%AC%20K%20%E4%B8%AA%E7%A5%96%E5%85%88.cs)
- LCA
计算x y的 lca(最近公共祖先)，先把更深的节点向上跳到跟另一节点相同深度，再一起向上跳到最近的公共节点
```
private int LCA(int x, int y)
{
    //x总是更深的节点
    if (depth[x] < depth[y]) (x, y) = (y, x);
    for (int k = LOG; k >= 0; k--)
    {
        //如果比y深度更大 则从此节点继续向上跳
        if (depth[fa[x][k]] >= depth[y])
        {
            x = fa[x][k];
        }
    }
    //y在x的子树中会导致x==y
    if (x == y) return x;

    for (int k = 15; k >= 0; k--)
    {
        if (fa[x][k] != fa[y][k])
        {
            x = fa[x][k];
            y = fa[y][k];
        }
    }
    //x y此时是某个节点的直接子节点
    return fa[x][0];
}
```
 
### 树状数组
 
- [基础模板]
```
//树状数组基础模板
//下标从1开始
public class BIT
{
	public int n = 0;
	private int[] tree;
	public BIT(int n)
	{
		this.n = n;
		tree = new int[n + 1];
	}

	private int LowBit(int x)
	{
		return x & (-x);
	}

	public int Query(int i)
	{
		i++;
		int res = 0;
		while (i > 0)
		{
			res += tree[i];
			i -= LowBit(i);
		}
		return res;
	}

	public void Update(int i, int x)
	{
		i++;
		while (i <= n)
		{
			tree[i] += x;
			i += LowBit(i);
		}
	}
}
```
 
- [区域和检索-数组可修改](https://github.com/JadenSailing/algorithm-lib/blob/main/BinaryIndexedTree/Solution_LC_307_%E5%8C%BA%E5%9F%9F%E5%92%8C%E6%A3%80%E7%B4%A2-%E6%95%B0%E7%BB%84%E5%8F%AF%E4%BF%AE%E6%94%B9.cs)
 
 - 二维树状数组 $O(logn*logm)$
```
public class BIT2D
{
    int n, m;
    int[][] tree;
    public BIT2D(int n, int m)
    {
        this.n = n; this.m = m;
        tree = new int[n + 1][];
        for (int i = 0; i <= n; i++) tree[i] = new int[m + 1];
    }
    private int LowBIT(int x) { return x & -x; }
    public void Add(int row, int col, int v)
    {
        for (int i = row + 1; i <= n; i += LowBIT(i))
        {
            for (int j = col + 1; j <= m; j += LowBIT(j))
            {
                tree[i][j] += v;
            }
        }
    }
    public int Sum(int row, int col)
    {
        int res = 0;
        for (int i = row + 1; i > 0; i -= LowBIT(i))
        {
            for (int j = col + 1; j > 0; j -= LowBIT(j))
            {
                res += tree[i][j];
            }
        }
        return res;
    }
}
```
 
### 线段树
 
- 线段树模板 支持区间更新/增加，区间求和，区间极值
- 动态开点，延迟标记
```
public class SegmentTree
    {
        public class Node
        {
            public bool valid = false; //是否是有效区间
            public Node left, right;
            public long sum, max, min;
            public bool flag = false; //延迟标记
            public long val; //延迟数据
        }
        private long n = (long)1e9 + 10; //一般不会超过这个上界
        private Node root = new Node();
        public bool isAdd = true; //默认是区间增加的形式，否则是区间覆盖
        public SegmentTree(bool isAdd = true) { this.isAdd = isAdd; }
        public bool IsValid(long L, long R) { return IsValid(root, 0, n, L, R); }
        public long Sum(long L, long R) { return Sum(root, 0, n, L, R); }
        public long Min(long L, long R) { return Min(root, 0, n, L, R); }
        public long Max(long L, long R) { return Max(root, 0, n, L, R); }
        public void Update(long L, long R, long val) { Update(root, 0, n, L, R, val); }

        private bool IsValid(Node node, long start, long end, long l, long r)
        {
            if (l <= start && end <= r) return node.valid;
            long mid = (start + end) >> 1;
            PushDown(node, mid - start + 1, end - mid);
            bool res = false;
            if (l <= mid) res |= IsValid(node.left, start, mid, l, r);
            if (r > mid) res |= IsValid(node.right, mid + 1, end, l, r);
            return res;
        }

        private void Update(Node node, long start, long end, long l, long r, long val)
        {
            if (l <= start && end <= r)
            {
                if (isAdd)
                {
                    node.sum += (end - start + 1) * val;
                    node.val += val;
                    if (!node.valid)
                    {
                        node.max = node.min = val;
                    }
                    else
                    {
                        node.max += val;
                        node.min += val;
                    }
                }
                else
                {
                    node.sum = (end - start + 1) * val;
                    node.val = val;
                    node.max = node.min = val;
                }
                node.valid = true;
                node.flag = true;
                return;
            }
            long mid = (start + end) >> 1;
            PushDown(node, mid - start + 1, end - mid);
            if (l <= mid) Update(node.left, start, mid, l, r, val);
            if (r > mid) Update(node.right, mid + 1, end, l, r, val);
            PushUp(node);
        }
        private long Sum(Node node, long start, long end, long l, long r)
        {
            if (l <= start && end <= r) return node.sum;
            long mid = (start + end) >> 1, ans = 0;
            PushDown(node, mid - start + 1, end - mid);
            if (l <= mid) ans += Sum(node.left, start, mid, l, r);
            if (r > mid) ans += Sum(node.right, mid + 1, end, l, r);
            return ans;
        }

        private long Max(Node node, long start, long end, long l, long r)
        {
            if (l <= start && end <= r) return node.max;
            long mid = (start + end) >> 1, ans = long.MinValue;
            PushDown(node, mid - start + 1, end - mid);
            if (l <= mid && node.left.valid) ans = Math.Max(ans, Max(node.left, start, mid, l, r));
            if (r > mid && node.right.valid) ans = Math.Max(ans, Max(node.right, mid + 1, end, l, r));
            return ans;
        }

        private long Min(Node node, long start, long end, long l, long r)
        {
            if (l <= start && end <= r) return node.min;
            long mid = (start + end) >> 1, ans = long.MaxValue;
            PushDown(node, mid - start + 1, end - mid);
            if (l <= mid && node.left.valid) ans = Math.Min(ans, Min(node.left, start, mid, l, r));
            if (r > mid && node.right.valid) ans = Math.Min(ans, Min(node.right, mid + 1, end, l, r));
            return ans;
        }

        private void PushUp(Node node)
        {
            node.sum = node.left.sum + node.right.sum;
            node.max = long.MinValue;
            node.min = long.MaxValue;
            if (node.left.valid)
            {
                node.min = Math.Min(node.min, node.left.min);
                node.max = Math.Max(node.max, node.left.max);
            }
            if (node.right.valid)
            {
                node.min = Math.Min(node.min, node.right.min);
                node.max = Math.Max(node.max, node.right.max);
            }
            node.valid = node.valid || node.left.valid;
            node.valid = node.valid || node.right.valid;
        }
        private void PushDown(Node node, long leftNum, long rightNum)
        {
            if (node.left == null) node.left = new Node();
            if (node.right == null) node.right = new Node();
            if (!node.flag) return;
            if (isAdd)
            {
                node.left.sum += node.val * leftNum;
                node.right.sum += node.val * rightNum;
                if (node.left.valid)
                {
                    node.left.max += node.val;
                    node.left.min += node.val;
                }
                else
                {
                    node.left.max = node.val;
                    node.left.min = node.val;
                }
                if (node.right.valid)
                {
                    node.right.max += node.val;
                    node.right.min += node.val;
                }
                else
                {
                    node.right.max = node.val;
                    node.right.min = node.val;
                }
                node.left.val += node.val;
                node.right.val += node.val;
            }
            else
            {
                node.left.sum = node.val * leftNum;
                node.right.sum = node.val * rightNum;
                node.left.max = node.val;
                node.left.min = node.val;
                node.right.max = node.val;
                node.right.min = node.val;
                node.left.val = node.right.val = node.val;
            }
            node.left.valid = node.right.valid = true;
            node.left.flag = node.right.flag = true;
            node.val = 0;
            node.flag = false;
        }
    }
```
 - 可持久化线段树[模板](https://github.com/JadenSailing/algorithm-lib/blob/main/SegmentTree/T_SegmentTreePersistent.cs)
 
### 字典树
- 基本字典树模型
 [数组中两个数的最大异或值](https://github.com/JadenSailing/algorithm-lib/blob/main/Trie/Solution_LC_421_%20%E6%95%B0%E7%BB%84%E4%B8%AD%E4%B8%A4%E4%B8%AA%E6%95%B0%E7%9A%84%E6%9C%80%E5%A4%A7%E5%BC%82%E6%88%96%E5%80%BC.cs)
- 可删除字典树
 [找出强数对的最大异或值II](https://github.com/JadenSailing/algorithm-lib/blob/main/Trie/Solution_LC_2935_%E6%89%BE%E5%87%BA%E5%BC%BA%E6%95%B0%E5%AF%B9%E7%9A%84%E6%9C%80%E5%A4%A7%E5%BC%82%E6%88%96%E5%80%BC%20II.cs)
### 平衡树
 
- 红黑树
- Treap
```
//标准平衡树(有序集合)实现
public class Map<T> where T : IComparable, IEquatable<T>
{
    class Node<TKey> where TKey : IComparable
    {
        public Node<TKey> left;
        public Node<TKey> right;
        public TKey key;
        public int priority;
        public int size;
        public int count;
    }
    private Node<T> root = null;

    private Random randGenterator = new Random(new DateTime().Millisecond);

    public bool Insert(T x)
    {
        if (x == null) return false;
        root = Insert(root, x);
        return true;
    }
    public bool Delete(T x)
    {
        root = Delete(root, x);
        return true;
    }
    public int UpperBound(T x)
    {
        return UpperBound(root, x);
    }
    public int LowerBound(T x)
    {
        return LowerBound(root, x);
    }

    public bool Contains(T x)
    {
        int k = LowerBound(x);
        if (k < Count && Kth(k).Equals(x)) return true;
        return false;
    }
    public T Kth(int k)
    {
        return Kth(root, k);
    }
    public int Count { get { return root == null ? 0 : root.size; } }

    private int RandomPriority() { return randGenterator.Next(); }
    private Node<T> NewNode(T x)
    {
        Node<T> node = new Node<T>() { size = 1, count = 1, key = x, priority = RandomPriority() };
        return node;
    }
    private void Update(Node<T> u)
    {
        int size = u.count;
        if (u.left != null) size += u.left.size;
        if (u.right != null) size += u.right.size;
        u.size = size;
    }
    private Node<T> Rotate(Node<T> o, bool leftRotate)
    {
        Node<T> k;
        if (leftRotate)
        {
            k = o.right;
            o.right = k.left;
            k.left = o;
        }
        else
        {
            k = o.left;
            o.left = k.right;
            k.right = o;
        }
        Update(o);
        Update(k);
        return k;
    }
    private Node<T> Insert(Node<T> node, T x)
    {
        if (node == null)
        {
            node = NewNode(x);
            return node;
        }
        int compare = x.CompareTo(node.key);
        if (compare == 0)
        {
            node.count++;
            Update(node);
        }
        else if (compare > 0) node.right = Insert(node.right, x);
        else node.left = Insert(node.left, x);
        if (node.left != null && node.priority > node.left.priority) node = Rotate(node, false);
        if (node.right != null && node.priority > node.right.priority) node = Rotate(node, true);
        Update(node);
        return node;
    }

    private Node<T> Delete(Node<T> node, T x)
    {
        if (node.key.Equals(x))
        {
            if (node.count > 1)
            {
                node.count--;
                Update(node);
                return node;
            }
            if (node.left == null && node.right == null) return null;
            if (node.left == null) return node.right;
            if (node.right == null) return node.left;
            if (node.left.priority < node.right.priority)
            {
                node = Rotate(node, false);
                node.right = Delete(node.right, x);
                Update(node);
                return node;
            }
            else
            {
                node = Rotate(node, true);
                node.left = Delete(node.left, x);
                Update(node);
                return node;
            }
        }
        if (node.key.CompareTo(x) > 0) node.left = Delete(node.left, x);
        else node.right = Delete(node.right, x);
        Update(node);
        return node;
    }

    private int UpperBound(Node<T> u, T x)
    {
        if (u == null) return 0;
        int compare = x.CompareTo(u.key);
        int lSize = (u.left == null ? 0 : u.left.size);
        if (compare >= 0) return lSize + u.count + UpperBound(u.right, x);
        return UpperBound(u.left, x);
    }
    private int LowerBound(Node<T> u, T x)
    {
        if (u == null) return 0;
        int compare = x.CompareTo(u.key);
        int lSize = (u.left == null ? 0 : u.left.size);
        if (compare == 0) return lSize;
        if (compare > 0) return lSize + u.count + LowerBound(u.right, x);
        return LowerBound(u.left, x);
    }

    private T Kth(Node<T> u, int k)
    {
        if (k < 0 || k >= Count) return default(T);
        int lSize = (u.left == null ? 0 : u.left.size);
        if (lSize > k) return Kth(u.left, k);
        if (lSize + u.count > k) return u.key;
        return Kth(u.right, k - lSize - u.count);
    }
}
```
	
  - [避免洪水泛滥](https://github.com/JadenSailing/algorithm-lib/blob/main/%E5%B9%B3%E8%A1%A1%E6%A0%91/Solution_LC_1488_%E9%81%BF%E5%85%8D%E6%B4%AA%E6%B0%B4%E6%B3%9B%E6%BB%A5.cs)
    

  - [序列顺序查询](https://leetcode.cn/problems/sequentially-ordinal-rank-tracker/) [[代码]](https://github.com/JadenSailing/algorithm-lib/blob/main/%E5%B9%B3%E8%A1%A1%E6%A0%91/Solution_LC_2102_%E5%BA%8F%E5%88%97%E9%A1%BA%E5%BA%8F%E6%9F%A5%E8%AF%A2.cs)
  - [683. K 个关闭的灯泡](https://leetcode.cn/problems/k-empty-slots/)  自定义类型
```
class Node : IComparable, IEquatable<Node>
{
    public int L, R;
    public Node(int L, int R) { this.L = L; this.R = R; }
    public int CompareTo(object other)
    {
        var target = other as Node;
        return R - target.R;
    }
    public bool Equals(Node other)
    {
        return L == other.L && R == other.R;
    }
}
public int KEmptySlots(int[] bulbs, int k)
{
    int n = bulbs.Length;
    Map<Node> map = new Map<Node>();
    map.Insert(new Node(0, n - 1));
    for (int i = 0; i < n; i++)
    {
        int v = bulbs[i] - 1;
        int kth = map.LowerBound(new Node(v, v));
        var pre = map.Kth(kth);
        if (v - pre.L == k && pre.L != 0) return i + 1;
        if (pre.R - v == k && pre.R != n - 1) return i + 1;
        map.Delete(pre);
        if (v > pre.L) map.Insert(new Node(pre.L, v - 1));
        if (pre.R > v) map.Insert(new Node(v + 1, pre.R));
    }
    return -1;
}
```
 
## 图论
 
### 基础
- 建图
	 
   - 基于边
   - 基于父节点
   - 带边权
 
- 遍历
 
### 拓扑排序
 
### 最短路
- Floyd
```
//处理无负权回路的任意两点最短路
//f[i][j]初始化为邻接矩阵 表示i-j的距离 如果无直接连接边 则为INF
for (int k = 0; k < n; k++)
{
    for (int i = 0; i < n; i++)
    {
        for (int j = 0; j < n; j++)
        {
            f[i][j] = Math.Min(f[i][j], f[i][k] + f[k][j]);
        }
    }
}
```
- Dijkstra
    
    - 无负权 
	- 基于节点 复杂度O(n^2)
   ```
	public int ShortestPath(int node1, int node2)
    {
        int[] dis = new int[n];
        Array.Fill(dis, int.MaxValue);
        dis[node1] = 0;
        int[] vis = new int[n];
        while (true)
        {
            int min = int.MaxValue;
            int u = -1;
            for (int i = 0; i < n; i++)
            {
                if (vis[i] == 0 && dis[i] < min)
                {
                    min = dis[i];
                    u = i;
                }
            }
            if (u == -1 || min == int.MaxValue) break; //全部处理完
            vis[u] = 1;
            foreach (int v in g[u].Keys)
            {
                dis[v] = Math.Min(dis[v], dis[u] + g[u][v]);
            }
        }
        return dis[node2] == int.MaxValue ? -1 : dis[node2];
    }
   ```

	- 基于优先队列优化 复杂度O(m*logm)

```
//双纺锤形图会卡遍历 所以要用vis标记
int[] dis = new int[n];
int[] vis = new int[n];
Array.Fill(dis, int.MaxValue);
PriorityQueue<int, int> pq = new PriorityQueue<int, int>();
dis[node1] = 0;
pq.Enqueue(node1, 0);
while (pq.Count > 0)
{
    int u = pq.Dequeue();
    if (vis[u] == 1) continue;
    vis[u] = 1;
    foreach (int v in g[u].Keys)
    {
        int d = g[u][v] + dis[u];
        if (d < dis[v])
        {
            dis[v] = d;
            pq.Enqueue(v, d);
        }
    }
}
return dis[node2] == int.MaxValue ? -1 : dis[node2];
```

另外有基于二叉堆/斐波那契堆等优化方案
[1976. 到达目的地的方案数](https://leetcode.cn/problems/number-of-ways-to-arrive-at-destination/)
[>题解](https://github.com/JadenSailing/algorithm-lib/blob/main/Graphs/Solution_LC_1976_%E5%88%B0%E8%BE%BE%E7%9B%AE%E7%9A%84%E5%9C%B0%E7%9A%84%E6%96%B9%E6%A1%88%E6%95%B0.cs)

- A*
 
### 最小生成树
- Kruskal
排序所有边，并查集记录合并信息，如果最后连通分量=1 表示一颗完整最小生成树，自动处理重边
```
Array.Sort(connections, (A, B) => (A[2] - B[2]));
UnionFind uf = new UnionFind(n);
int ans = 0;
foreach (int[] v in connections)
{
    int L, R, W; (L, R, W) = (v[0], v[1], v[2]);
    if (uf.Connect(L - 1, R - 1)) ans += W;
}
return uf.setCount == 1 ? ans : -1;
```
- Prim
任选起点，类似dijkstra逐个添加，但是dis[v]记录是整体到v的距离 而非从起点到v的距离，注意建图重边问题
```
int MX = int.MaxValue / 2;
int[] dis = new int[n];
int[] vis = new int[n];
Array.Fill(dis, MX);
dis[0] = 0;
PriorityQueue<int, int> pq = new PriorityQueue<int, int>();
pq.Enqueue(0, dis[0]);
int ans = 0;
while (pq.Count > 0)
{
    int u = pq.Dequeue();
    if (vis[u] == 1) continue;
    vis[u] = 1;
    ans += dis[u];
    foreach (int v in g[u].Keys)
    {
        int d = g[u][v]; //note: dijkstra d = dis[u] + g[u][v]
        if (d < dis[v])
        {
            dis[v] = d;
            pq.Enqueue(v, d);
        }
    }
}
if (vis.Sum() < n) return -1;
return ans;
```
### Tarjan

- 割点
```
int time = 0;
int[] dfn = new int[n];
int[] low = new int[n];
void Tarjan(int u, int fa)
{
	low[u] = dfn[u] = ++time;
	int count = 0; //需要记录子节点个数 用于根节点的判定
    foreach(var v in g[u])
	{
		if(v == fa) continue;
		if(dfn[v] == 0)
		{
			Tarjan(v, u);
            low[u] = Math.Min(low[u], low[v]);
			count++;
			if(u == 0 && count >= 2 //如果是根节点且子图>=2
			|| u > 0 && low[v] >= dfn[u]) //如果是非根节点且low[v]>=dfn[u]
				flag[u] = true; //标记割点
		}
        else low[u] = Math.Min(low[u], dfn[v]);
	}
}
Tarjan(0, -1);
```
- 割边
```
int time = 0;
int[] dfn = new int[n];
int[] low = new int[n];
void Tarjan(int u, int fa)
{
    low[u] = dfn[u] = ++time;
    foreach(var v in g[u])
	{
        if (v == fa) continue;
        if (dfn[v] == 0)
        {
            Tarjan(v, u);
            low[u] = Math.Min(low[u], low[v]);
            //只需要下面这一句
            if (low[v] > dfn[u])
                Console.WriteLine(u + "-" + v);//无向边u-v是割边
        }
        else low[u] = Math.Min(low[u], dfn[v]);
    }
}
Tarjan(0, -1);
```
[1192. 查找集群内的关键连接](https://leetcode.cn/problems/critical-connections-in-a-network/) 

- 点双连通
```
//tarjan
bool[] flag = new bool[n]; //是否是割点
int time = 0;
int[] low = new int[n];
int[] dfn = new int[n];
Stack<int> st = new Stack<int>();
List<List<int>> vbcc = new List<List<int>>(); //所有点双连通分量
void Tarjan(int u, int p)
{
    low[u] = dfn[u] = ++time;
    st.Push(u);
    int count = 0;
    foreach (var v in g[u])
    {
        if (v == p) continue;
        if (dfn[v] == 0)
        {
            count++;
            Tarjan(v, u);
            low[u] = Math.Min(low[u], low[v]);
            if (low[v] >= dfn[u])
            {
                if (u > 0) flag[u] = true;
                List<int> list = new List<int>();
                while (st.Peek() != v)
                {
                    list.Add(st.Pop());
                }
                list.Add(st.Pop());
                list.Add(u);
                vbcc.Add(list);
            }
        }
        else low[u] = Math.Min(low[u], dfn[v]);
    }
    if (count >= 2 && u == 0) flag[u] = true;
}
Tarjan(0, -1);
```
举个例子，如图：

最终的点双连通分量是

[LCP 54. 夺回据点](https://leetcode.cn/problems/s5kipK/) 点双连通分量，近似模板题。

- 边双连通

```
//tarjan
bool[] flag = new bool[n]; //是否是割点
int time = 0;
int[] low = new int[n];
int[] dfn = new int[n];
Stack<int> st = new Stack<int>();
List<List<int>> ebcc = new List<List<int>>(); //所有点双连通分量
void Tarjan(int u, int p)
{
    low[u] = dfn[u] = ++time;
    st.Push(u);
    int count = 0;
    foreach (var v in g[u])
    {
        if (v == p) continue;
        if (dfn[v] == 0)
        {
            count++;
            Tarjan(v, u);
            low[u] = Math.Min(low[u], low[v]);
        }
        else low[u] = Math.Min(low[u], dfn[v]);
    }
    if (low[u] == dfn[u])
    {
        List<int> list = new List<int>();
        while (st.Peek() != u) list.Add(st.Pop());
        list.Add(st.Pop());
        ebcc.Add(list);
    }
}
Tarjan(0, -1);
```

[CF923-div3-F.Microcycle-求带权无向图中最短边权重最小的简单环](https://codeforces.com/contest/1927/submission/245749383)

本题解包含了Tarjan求环 Dijkstra求最短路 记录path等

### 网络流
[Dinic模板](https://github.com/JadenSailing/algorithm-lib/blob/main/NetWorkFlow/T_Graph.cs)
[最大流模板题](https://www.luogu.com.cn/problem/P3376)
[奶牛的电信](https://www.luogu.com.cn/problem/P1345) 拆点最小割

[最小费用最大流Dinic模板](https://github.com/JadenSailing/algorithm-lib/blob/main/NetWorkFlow/T_MCMF.cs)
[3276. 选择矩阵中单元格的最大得分](https://leetcode.cn/problems/select-cells-in-grid-with-maximum-score/)
```
//最大费用最小流 费用取反转换为最小费用最大流
public int MaxScore(IList<IList<int>> grid)
{
    int n = grid.Count, m = grid[0].Count;
    int mv = 100;
    MCMF_Dinic g = new MCMF_Dinic(mv + n + 2);
    int s = 0, t = mv + n + 1;
    //超级源点->每个值 容量1 费用 取反
    //这保证了每个值只能使用一次
    for (int i = 1; i <= mv; i++) g.AddEdge(s, i, 1, -i);
    //每一行->超级汇点 容量1 费用 0
    //这保证了每一行只能使用一次
    for (int i = 0; i < n; i++) g.AddEdge(i + mv + 1, t, 1, 0);
    //每个值->对应行 容量1 费用 0
    for (int i = 0; i < n; i++) for (int j = 0; j < m; j++) g.AddEdge(grid[i][j], i + mv + 1, 1, 0);
    return -(int)(g.Dinic(s, t).Item2);
}
```
 
## 并查集
 
- [基础模板](https://github.com/JadenSailing/algorithm-lib/blob/main/UnionFind/UnionFind.cs)
```
public class UnionFind
{
    public int n = 0;
    private int[] fa;
    public int setCount = 0;
    public UnionFind(int n)
    {
        this.n = n;
        this.setCount = n;
        this.fa = new int[n];
        for (int i = 0; i < n; i++) this.fa[i] = i;
    }

    public int Find(int x)
    {
        return fa[x] == x ? x : (fa[x] = Find(fa[x]));
    }

    public bool IsConnected(int x, int y)
    {
        return Find(x) == Find(y);
    }

    public bool Connect(int x, int y)
    {
        if (IsConnected(x, y)) return false;
        fa[Find(x)] = Find(y);
        setCount--;
        return true;
    }
}
```
 
- [省份数量](https://github.com/JadenSailing/algorithm-lib/blob/main/UnionFind/Solution_LC_547_%E7%9C%81%E4%BB%BD%E6%95%B0%E9%87%8F.cs)
- 带权并查集
下面是一个字典类型，倍数权值的模板
比较难理解的两点
  *Find时，(fa[x], wt[x]) = (Find(fa[x]), wt[x] \* wt[fa[x]])，这里是向根合并倍数，跳过不同的根时每次都要把倍数累计相乘。*

  *Connect时，wt[px] \*= w \* wt[y] / wt[x]，这里当合并x和y时，需要合并各自的根px,py，注意w[px] w[py]此时可能均不为1，都需要考虑进去，举个简单例子：
  假设x,px权值分别为2,3, y,py权值分别为4,5, 如果此时需要连接的是x/y=3，那么最终px会向py合并，px权重应该是多少？有意思的是跟py的权重无关。如果以具体数值距离，比如x = 60, px = 30, y = 20, py = 5, px的权重为30/5=6=3\*4/2=w\*w[y]/w[x]。*
```
public class UnionFind
{
    public Dictionary<string, string> fa = new Dictionary<string, string>();
    public Dictionary<string, double> wt = new Dictionary<string, double>();

    private void Add(string x)
    {
        if (!fa.ContainsKey(x))
        {
            fa[x] = x;
            wt[x] = 1;
        }
    }
    public string Find(string x)
    {
        Add(x);
        if (fa[x] != x)
        {
            (fa[x], wt[x]) = (Find(fa[x]), wt[x] * wt[fa[x]]);
        }
        return fa[x];
    }

    public double Weight(string x)
    {
        Find(x);
        return wt[x];
    }

    public bool Connect(string x, string y, double w)
    {
        Add(x); Add(y);
        if (IsConnected(x, y)) return false;
        var px = Find(x);
        var py = Find(y);
        fa[px] = py;
        wt[px] *= w * wt[y] / wt[x];
        return true;
    }
    public bool IsConnected(string x, string y)
    {
        Add(x); Add(y);
        return Find(x) == Find(y);
    }
}
```
 
## 位运算
 
### 状态压缩
 
### 枚举子集
 
## 动态规划
 
### 线性DP
- LIS(Longest Increasing Subsequence) 最长上升子序列

[300. 最长递增子序列](https://leetcode.cn/problems/longest-increasing-subsequence/)
```
//普通解
public int LengthOfLIS(int[] nums)
{
	int n = nums.Length;
	//dp[i]表示以nums[i]结尾的lis长度
	int[] dp = new int[n];
	Array.Fill(dp, 1); //只有自身时长度为1
	for(int i = 1; i < n; i++)
	{
		for(int j = 0; j < i; j++)
		{
			if (nums[j] < nums[i]) dp[i] = Math.Max(dp[i], dp[j] + 1);
		}
	}
	return dp.Max(); //返回最大值
}
```
```
//二分优化
public int LengthOfLIS(int[] nums)
{
	int n = nums.Length;
	int[] lis = new int[n];
	lis[0] = nums[0];
	int len = 1;
	for(int i = 1; i < n; i++)
	{
		int v = nums[i];
		int low = 0, high = len- 1;
		while(low <= high)
		{
			int mid = low + (high- low) / 2;
			if(lis[mid] < v) low = mid + 1;
			else high = mid- 1;
		}
		lis[low] = v;
		len = Math.Max(len, low + 1);
	}
	return len;
}
```
- LCS(Longest Common Subsequence)最长公共子序列
```
int[][] dp = new int[n1 + 1][];
for (int i = 0; i <= n1; i++) dp[i] = new int[n2 + 1];
for (int i = 1; i <= n1; i++)
{
    for (int j = 1; j <= n2; j++)
    {
        if (text1[i- 1] == text2[j- 1])
        {
            dp[i][j] = dp[i- 1][j- 1] + 1;
        }
        else
        {
            dp[i][j] = Math.Max(dp[i- 1][j], dp[i][j- 1]);
        }
    }
}
return dp[n1][n2];
```
空间优化版
```
int[] dp = new int[m + 1];
for (int i = 1; i <= n; i++)
{
	int upLeft = 0;
	for (int j = 1; j <= m; j++)
	{
		(upLeft, dp[j]) = (dp[j], text1[i- 1] == text2[j- 1] ? upLeft + 1 : Math.Max(dp[j], dp[j- 1]));
	}
}
return dp[m];
```
 
- 编辑距离
 
### 区间DP
 
### 背包DP
 
### 带权区间调度
 [weighted Interval Scheduling 问题-草莓奶昔](https://leetcode.cn/problems/maximize-the-profit-as-the-salesman/solutions/2398862/python-weightedintervalscheduling-wen-ti-t253/)
```
/*
    给定 n 个闭区间 [left_i,right_i,score_i].
    请你在数轴上选择若干区间,使得选中的区间之间互不相交.
    返回可选取区间的最大权值和.

    Args:
        intervals: 区间列表,每个区间为[left,right,score].
        overlapping: 是否允许选择的区间端点重合.默认为False.
    */
    private int WeightedIntervalScheduling(List<int[]> intervals, bool overlapping = false)
    {
        int n = intervals.Count;
        intervals.Sort((A, B) => (A[1]- B[1]));
        int[] dp = new int[n + 1];
        for (int i = 0; i < n; i++)
        {
            int low = 0, high = n- 1;
            while (low <= high)
            {
                int mid = low + (high- low) / 2;
                if (overlapping && intervals[mid][1] > intervals[i][0]
                 || !overlapping && intervals[mid][1] >= intervals[i][0])
                {
                    high = mid- 1;
                }
                else
                {
                    low = mid + 1;
                }
            }
            dp[i + 1] = Math.Max(dp[i], dp[low] + intervals[i][2]);
        }
        return dp[n];
    }
```
[无重叠区间](https://leetcode.cn/problems/non-overlapping-intervals/)
```
 public int EraseOverlapIntervals(int[][] intervals)
    {
        int n = intervals.Length;
        List<int[]> list = new List<int[]>();
        for (int i = 0; i < n; i++) list.Add(new int[] { intervals[i][0], intervals[i][1], 1 });
        return n- WeightedIntervalScheduling(list, true);
    }
```
 [规划兼职工作](https://leetcode.cn/problems/maximum-profit-in-job-scheduling/)
```
public int JobScheduling(int[] startTime, int[] endTime, int[] profit)
{
    List<int[]> list = new List<int[]>();
    for (int i = 0; i < startTime.Length; i++)
    {
        list.Add(new int[] { startTime[i], endTime[i], profit[i] });
    }
    return WeightedIntervalScheduling(list, true);
}
```
 
### 树形dp
 自底向上处理数据，习惯用数组返回所需的各种数据，简单的可以用单值返回

 [到达首都的最少油耗](https://github.com/JadenSailing/algorithm-lib/blob/main/DP/%E6%A0%91%E5%BD%A2DP/Solution_LC_2477_%E5%88%B0%E8%BE%BE%E9%A6%96%E9%83%BD%E7%9A%84%E6%9C%80%E5%B0%91%E6%B2%B9%E8%80%97.cs)
### 换根dp
两边DFS，第一遍获取各节点数据和根节点的结果，第二遍换根转移递推其它节点结果。

[树中距离之和](https://github.com/JadenSailing/algorithm-lib/blob/main/DP/%E6%8D%A2%E6%A0%B9DP/Solution_LC_834_%E6%A0%91%E4%B8%AD%E8%B7%9D%E7%A6%BB%E4%B9%8B%E5%92%8C.cs)

### 数位dp
 标准模板 以[至少1位重复的数字](https://leetcode.cn/problems/numbers-with-repeated-digits)为例
 
 模板熟练后，题目的关键点在于找到递归数据如何维护 维护什么和重复子问题的界定和缓存
```
//至少有一位重复数字的个数[1,n]
public int NumDupDigitsAtMostN(int n)
{
    //计算没有重复数字的个数 正难则反
    int diff = DFS(n.ToString(), 0, 0, true, false, Cache(10, 1024, -1));
    return n- diff;
}

//标准模板
//s n的字符串形式
//cur 当前遍历到的位[0, s.Length- 1]
//mask 题目所需的数据 此处是10个字母是否有重复的标记
//isLimit 当前位是否受n当前位的约束 当不受约束时 则会记录cache 
//isNum 表示是否是合法数字 即不全是前导0
//vis 记忆化数据

private int DFS(string s, int cur, int mask, bool isLimit, bool isNum, int[][] vis)
{
    //遍历完s长度后的退出处理
    if (cur == s.Length) return isNum ? 1 : 0;
    //如果无约束且有缓存 则返回缓存数据
    if (!isLimit && isNum && vis[cur][mask] >= 0) return vis[cur][mask];
    int res = 0;
    //如果需要处理前导零 特殊处理
    if (!isNum) res += DFS(s, cur + 1, mask, false, false, vis);
    //上界
    int upper = isLimit ? s[cur]- '0' : 9;
    //上面处理前导0 就从1开始 否则还是从0开始
    int lower = isNum ? 0 : 1;
    for (int i = lower; i <= upper; i++)
    {
        //无重复的情况下才需要向下计算
        if ((mask >> i & 1) == 0)
        {
            //mask数据修改
            //isLimit更新
            //isNum更新
            //注意只修改当前i
            res += DFS(s, cur + 1, mask | (1 << i), isLimit && i == upper, isNum || i > 0, vis);
        }
    }
    //只有无约束的情况下存缓存
    if (!isLimit && isNum) vis[cur][mask] = res;
    return res;
}
```
### dp优化

## 数论
### 质数
- 埃氏筛
```
int N = (int)1e6 + 5;
List<int> primes = new List<int>();
//是否是非质数的标志
bool[] flag = new bool[N];
//0 1特殊处理非质数
flag[0] = flag[1] = true;
for(int i = 2; i < N; i++)
{
    if (flag[i] == false)
    {
        for(int j = i * 2; j < N; j += i)
        {
            flag[j] = true;
        }
    }
}
for(int i = 2; i < N; i++)
{
    if (flag[i] == false) primes.Add(i);
}
```
- 分解质因数
```
Dictionary<int, int> count = new Dictionary<int, int>();
void Divide(int x)
{
    for (int i = 2; i <= x / i; i++)
    {
        while (x % i == 0)
        {
            if (!count.ContainsKey(i)) count[i] = 0;
            count[i]++;
            x /= i;
        }
    }
    //重要
    if (x > 1)
    {
        if (!count.ContainsKey(x)) count[x] = 0;
        count[x]++;
    }
}
```

[范围内最接近的两个质数](https://github.com/JadenSailing/algorithm-lib/blob/main/%E6%95%B0%E8%AE%BA/%E8%B4%A8%E6%95%B0/Solution_LC_2523_%E8%8C%83%E5%9B%B4%E5%86%85%E6%9C%80%E6%8E%A5%E8%BF%91%E7%9A%84%E4%B8%A4%E4%B8%AA%E8%B4%A8%E6%95%B0.cs)

- 线性筛

### 数列
等差数列前n项和
`S = (a1 + an) * n / 2`
[2834. 找出美丽数组的最小和](https://leetcode.cn/problems/find-the-minimum-possible-sum-of-a-beautiful-array/)
```
long ans = 0;
long m = Math.Min(n, k / 2);
//1L * int 防溢出
ans += 1L * m * (m + 1) / 2 % mod;
//a1 = k, an = k + (n - m - 1), N = (n - m)
//0L + int 防溢出
ans += 1L * (n - m) * (0L + k + k + n - m - 1) / 2 % mod;
return (int)(ans % mod);
```

### 组合数学
- 排列组合
  - 排列
  $A_n^m=\frac{n!}{(n-m)!}$
  - 组合
  $C_n^m=\frac{n!}{(n-m)!m!}=\frac{A_n^m}{A_m^m}$
```
public static long fp = (long)1e9 + 7;
public static long[] fMem = new long[(int)1e6 + 5];
public static void InitFact(long n = (long)1e6, long mod = (long)1e9 + 7) 
{
	fp = mod;
	fMem[0] = 1; 
	long res = 1; 
	for (int i = 1; i <= n; i++)
	{
		res = (res * i) % fp; 
		fMem[i] = res;
	}
}
public static long Fact(long x) { return fMem[x]; }
public static long Anm(long n, long m) 
{
	long x = Fact(n); 
	long y = Fact(n - m); 
	return x * Power(y, fp - 2, fp) % fp; 
}
public static long Cnm(long n, long m) 
{ 
	long x = Fact(n); 
	long y = Fact(m) * Fact(n - m) % fp;
	return x * Power(y, fp - 2, fp) % fp; 
}
public static long Power(long x, long y, long p) 
{ 
	long res = 1L; 
	while (y > 0) 
	{
		if (y % 2 == 1) res = res * x % p;
		y /= 2; 
		x = x * x % p; 
	} 
	return res; 
}
```
- 枚举排列
```
private static bool NextPermutation(int[] p)
{
    int n = p.Length, t = -1;
    for (int i = 0; i < n - 1; i++) if (p[i + 1] - p[i] > 0) t = i;
    if (t == -1) return false;
    for (int i = n - 1; ; i--)
    {
        if (p[i] > p[t])
        {
            (p[i], p[t]) = (p[t], p[i]);
            break;
        }
    }
    Array.Sort(p, t + 1, n - t - 1);
    return true;
}
```
```
//使用方式
int[] p = new int[4] { 0, 1, 2, 3 };
do { Print(p); } while (NextPermutation(p));
```
[3154. 到达第 K 级台阶的方案数](https://leetcode.cn/problems/find-number-of-ways-to-reach-the-k-th-stair/)
```
long ans = 0;
InitFact(100);
for (int i = 0; i < 32; i++)
{
    for (int j = 0; j <= i + 1; j++)
    {
        int target = (1 << i) - j;
        if (target == k)
        {
            ans += Cnm(i + 1, j, mod);
        }
    }
}
return (int)ans;
```

- 容斥原理
```
//计算<=x所有数中至少是coins之一倍数的个数
private static long Calc(int[] coins, long x)
{
    int n = coins.Length; //n不超过20
    long res = 0;
    for (int i = 1; i < 1 << n; i++) //枚举子集 注意不包括空集
    {
        int mul = -1;
        long r = 1;
        for (int k = 0; k < 30; k++)
        {
            if (((i >> k) & 1) == 1)
            {
                mul *= -1;
                r = LCM(r, coins[k]); //拥有c个属性的个数需要计算lcm
            }
        }
        res += mul * (x / r);
    }
    return res;
}
```

[D. Exam in MAC](https://codeforces.com/contest/1935/problem/D)

已知集合`S` 整数`c`，计算数对`(x,y)`的个数，要求`0<=x<=y<=c`，且`(x+y)∉S (y-x)∉S`
```
//正难则反
long ans = 1L * (c + 1) * (c + 2) / 2;
int odd = 0, even = 0;
for (int i = 0; i < n; i++)
{
    long v = nums[i];
    //x+y∈S
    ans -= (v / 2) + 1;
    //y-x∈S
    ans -= c - v + 1;
    if (v % 2 == 0) even++;
    else odd++;
}
//重合部分 (x+y)=a (y-x)=b => a + b为偶数
//特别注意int溢出问题(1L * list.Count * list.Count)
ans += 1L * odd * (odd + 1) / 2;
ans += 1L * even * (even + 1) / 2;
Print(ans);
```

### 方程
- 二元一次方程
 [1276. 不浪费原料的汉堡制作方案](https://leetcode.cn/problems/number-of-burgers-with-no-waste-of-ingredients/)
 给你两个整数 `T` 和 `C`，分别表示番茄片和奶酪片的数目。不同汉堡的原料搭配如下：
 `巨无霸汉堡`：4 片番茄和 1 片奶酪
`小皇堡`：2 片番茄和 1 片奶酪 如何使得剩余原材料为零

	- 标准二元一次方程：
`4x+2y=T,x+y=C`
解方程：
`x=T/2-C,y=C*2-T/2`
注意隐藏条件`x,y`均为非负整数
则有`T>=C*2` 和 `T<=C*4` 且 `T`为偶数
	- 思维
全选巨无霸则`T=C*4`，全选小皇堡则`T=C*2`，所以`TC`只能在这之间，又番茄的最小单位为2，故`T`必须是偶数。注意实现时不要除零的情况，以及`TC`=0是合法的

### 博弈论

## 计算几何
 
 
## 其它
 
### 约瑟夫环
 [lc1823.找出游戏的获胜者](https://leetcode.cn/problems/find-the-winner-of-the-circular-game/)

核心函数`f(n, k) = (f(n- 1, k) + k) % n`
```
public int FindTheWinner(int n, int k) {
	int ans = 0;
	for(int i = 2; i <= n; i++) ans = (ans + k) % i;
	return ans + 1;
}
```
递归写法(如果下标从0开始，否则需要额外写个DFS函数)
```
public int FindTheWinner(int n, int k) {
	if(n == 1) return 0;
	return (DFS(n- 1, k) + k) % n;
}
```
### LogTrick
数组中以任意i为右端点，左侧不同的`或`/`与`/`gcd`/不超过$logU$个
```
//res[i]表示以i为右端点 左侧不同and值的区间列表
private Dictionary<int, int[]>[] LogTrick(int[] nums)
{
    int n = nums.Length;
    Dictionary<int, int[]>[] res = new Dictionary<int, int[]>[n];
    res[0] = new Dictionary<int, int[]>();
    res[0][nums[0]] = new int[] { 0, 0 };
    for (int i = 1; i < n; i++)
    {
        res[i] = new Dictionary<int, int[]>();
        foreach (int v in res[i - 1].Keys)
        {
            int vAnd = v & nums[i];
            if (!res[i].ContainsKey(vAnd))
            {
                res[i][vAnd] = new int[] { res[i - 1][v][0], res[i - 1][v][1] };
            }
            else
            {
                res[i][vAnd][0] = Math.Min(res[i][vAnd][0], res[i - 1][v][0]);
                res[i][vAnd][1] = Math.Max(res[i][vAnd][1], res[i - 1][v][1]);
            }
        }
        if (!res[i].ContainsKey(nums[i]))
        {
            res[i][nums[i]] = new int[] { i, i };
        }
        else
        {
            res[i][nums[i]][1] = i;
        }
    }
    return res;
}
```
### 珂朵莉树
链表实现的[珂朵莉树](https://github.com/JadenSailing/algorithm-lib/blob/main/Other/T_ODT.cs)

[模板题](https://codeforces.com/contest/896/problem/C) [题解](https://codeforces.com/contest/896/submission/267813264)

### 莫队
[3261. 统计满足 K 约束的子字符串数量 II](https://leetcode.cn/problems/count-substrings-that-satisfy-k-constraint-ii/)
```
public class Solution
{
    public long[] CountKConstraintSubstrings(string s, int k, int[][] queries)
    {
        int[] left = CalcLeft(s, k);
        int[] right = CalcRight(s, k);
        MoAlgo mo = new MoAlgo(s.Length, queries, (int L, int R) => (Math.Min(right[L], R) - L + 1), (int L, int R) => (R - Math.Max(left[R], L) + 1));
        return mo.Solve();
    }

    //计算每个位置i左侧最远的合法位置
    private int[] CalcLeft(string s, int k)
    {
    }
    //计算每个位置i右侧最远的合法位置
    private int[] CalcRight(string s, int k)
    {
    }
}
public class MoAlgo
{
    int n;
    int[][] queries;
    int m;
    int[][] comb;
    Func<int, int, long> moveL;
    Func<int, int, long> moveR;

    public MoAlgo(int n, int[][] queries,
        Func<int, int, long> moveL,
        Func<int, int, long> moveR)
    {
        this.n = n;
        this.queries = queries;
        this.moveL = moveL;
        this.moveR = moveR;
        this.m = queries.Length;
        int bSize = (int)(Math.Ceiling(Math.Sqrt(n)));
        comb = new int[m][];
        for (int i = 0; i < m; i++) comb[i] = new int[] { i, queries[i][0], queries[i][1] };
        Array.Sort(comb, (A, B) =>
        {
            if (A[1] / bSize != B[1] / bSize)
            {
                return A[1] / bSize - B[1] / bSize;
            }
            return A[2] - B[2];
        });
    }

    public long[] Solve()
    {
        int L = 0, R = -1;
        long total = 0;
        long[] ans = new long[m];
        for (int i = 0; i < m; ++i)
        {
            int[] q = comb[i];
            while (L > q[1])
            {
                L--;
                total += moveL(L, R);
            }
            while (R < q[2])
            {
                R++;
                total += moveR(L, R);
            }
            while (L < q[1])
            {
                total -= moveL(L, R);
                L++;
            }
            while (R > q[2])
            {
                total -= moveR(L, R);
                R--;
            }
            ans[q[0]] = total;
        }
        return ans;
    }
}
```

## C#语法
### PriorityQueue用法
 主要是简单Compare无法解决的情况下 需要构造比较器实例
 比如排序字符串，长度优先->字典序次之
```
public static void Solve()
{
    string[] arr = new string[] { "abc", "ab", "ac", "a", "aa", "aaaa" };
    PriorityQueue<string, string> pq = new PriorityQueue<string, string>(new Comparer());
    for (int i = 0; i < arr.Length; i++) pq.Enqueue(arr[i], arr[i]);
    List<string> res = new List<string>();
    while(pq.Count > 0) res.Add(pq.Dequeue());
    Print(res.ToArray());
    //["a","aa","ab","ac","abc","aaaa"]
}
class Comparer : IComparer<string>
{ 
    public int Compare(string A, string B)
    {
        if (A.Length != B.Length) return A.Length- B.Length;
        return A.CompareTo(B);
    }
}
```
### 递归过深问题
- 尾调用优化
- 子线程增大栈空间
```
Thread thread = new Thread(Solve, 100 * 1024 * 1024);
thread.Start();
thread.Join();
```
	
### 元组使用
- 声明 赋值 返回值等
```
int a, b; 
(a, b) = (2, 3);
(a, b) = (b, a);
```
- 作为组合Key Value 
```
Dictionary<(int, int), int> dict = new Dictionary<(int, int), int>();
dict[(2, 3)] = 10;
foreach((int, int) key in dict.Keys)
{
    Print(key.Item1, key.Item2, dict[key]);
}
```
### 形形色色爆int
- 可能出现的异常提示
`Unhandled exception. System.OverflowException: Arithmetic operation resulted in an overflow.`
 - 除零
 - new负长度数组
 - int相加 `a + b > int.MaxValue` 特别注意 `a + b + 0L`无效，必须是`0L + a + b`
 - int相乘 `a * b > int.MaxValue` 特别注意 `a * b * 1L`无效，必须是`1L * a * b`
 - 多int连加/乘 比如三项相加必须是`((r1 + r2) % mod + r3) % mod`
 - 总之 **可能爆int的地方一定会爆int**
```
v += (int)(1L * x[i][k] * y[k][j] % mod);
v %= mod;
```
 - 意外的较快增长
```
count[v] += count[v - 1]; //v重复时指数增长
```