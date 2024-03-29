
# AutumnMist's Algorithm Library
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
```
private int[][] Pow(int[][] mat, int p)
{
    //单位矩阵
    int[][] ret = new int[2][]
    {
        new int[] { 1, 0 },
        new int[] { 0, 1 },
    };
    while (p > 0)
    {
        if ((p & 1) == 1)
        {
            ret = Mul(ret, mat);
        }
        p >>= 1;
        mat = Mul(mat, mat);
    }
    return ret;
}

private int[][] Mul(int[][] x, int[][] y)
{
    int n = x.Length, p = x[0].Length, m = y[0].Length;
    int[][] res = new int[n][];
    for (int i = 0; i < n; i++) res[i] = new int[m];
    for (int i = 0; i < n; i++)
    {
        for (int j = 0; j < m; j++)
        {
            int v = 0;
            for (int k = 0; k < p; k++)
            {
                v += x[i][k] * y[k][j];
            }
            res[i][j] = v;
        }
    }
    return res;
}
```
矩阵快速幂求[斐波那契数](https://github.com/JadenSailing/algorithm-lib/blob/main/%E5%9F%BA%E7%A1%80/%E5%BF%AB%E9%80%9F%E5%B9%82/Solution_LC_509_%E6%96%90%E6%B3%A2%E9%82%A3%E5%A5%91%E6%95%B0.cs)

### 逆元
费马小定理 a^p ≡ a (mod p)
```
Power(a, p, mod) = a % mod
Power(a, p- 1, mod) = 1
Power(a, p- 2, mod) = (1 / a) % mod
a / b % mod = a * Power(b, mod- 2, mod) % mod
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

- 双堆结构
[数据流的中位数](https://github.com/JadenSailing/algorithm-lib/blob/main/Heap/Solution_LC_295_%E6%95%B0%E6%8D%AE%E6%B5%81%E7%9A%84%E4%B8%AD%E4%BD%8D%E6%95%B0.cs)

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
`MOD` 1e6时有case无法通过
```
public class StringHash
{
	static Random rand;
	static long BASE, MOD;
	long[] P, h;
	static StringHash()
	{
		// 随机哈希基数和模数，防止被 hack
		rand = new Random(DateTime.Now.Millisecond);
		BASE = 37 + rand.Next() % 107;
		MOD = (long)1e18 + 3;
	}

	public StringHash(string s)
	{
		// 求字符串 s 的哈希
		int n = s.Length;
		P = new long[n + 1];
		P[0] = 1;
		for (int i = 1; i <= n; i++) P[i] = P[i- 1] * BASE % MOD;
		h = new long[n + 1];
		h[0] = 0;
		for (int i = 1; i <= n; i++) h[i] = (h[i- 1] * BASE + s[i- 1]- 'a' + 1) % MOD;
	}

	// 求子串 s[L..R] 的哈希值 L,R ∈ [1,n]
	public long Hash(int L, int R)
	{
		return (h[R]- h[L- 1] * P[R- L + 1] % MOD + MOD) % MOD;
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
 
- [基础模板](https://github.com/JadenSailing/algorithm-lib/blob/main/BinaryIndexedTree/BIT.cs)
 
- [区域和检索-数组可修改](https://github.com/JadenSailing/algorithm-lib/blob/main/BinaryIndexedTree/Solution_LC_307_%E5%8C%BA%E5%9F%9F%E5%92%8C%E6%A3%80%E7%B4%A2-%E6%95%B0%E7%BB%84%E5%8F%AF%E4%BF%AE%E6%94%B9.cs)
 
### 线段树
 
- 基本线段树模板 区间更新, 查询区间和
```
public class SegmentTree
{
    public class Node
    {
        public Node left, right;
        public long sum;
        public bool flag = false;
        public long data;
    }
    private long n = (long)1e9 + 5;
    private Node root = new Node();
    public SegmentTree(long n = (long)1e9 + 5) { this.n = n; }
    public long Sum(long L, long R) { return Sum(root, 0, n, L, R); }
    public void Update(long L, long R, long val) { Update(root, 0, n, L, R, val); }

    private void Update(Node node, long start, long end, long l, long r, long val)
    {
        if (l <= start && r >= end)
        {
            node.sum = (end- start + 1) * val;
            node.data = val;
            node.flag = true;
            return;
        }
        long mid = start + (end- start) / 2;
        PushDown(node, mid- start + 1, end- mid);
        if (l <= mid) Update(node.left, start, mid, l, r, val);
        if (r > mid) Update(node.right, mid + 1, end, l, r, val);
        PushUp(node);
    }
    private long Sum(Node node, long start, long end, long l, long r)
    {
        if (l <= start && end <= r) return node.sum;
        long mid = (start + end) >> 1, ans = 0;
        PushDown(node, mid- start + 1, end- mid);
        if (l <= mid) ans += Sum(node.left, start, mid, l, r);
        if (r > mid) ans += Sum(node.right, mid + 1, end, l, r);
        return ans;
    }

    private void PushUp(Node node)
    {
        node.sum = node.left.sum + node.right.sum;
    }
    private void PushDown(Node node, long leftNum, long rightNum)
    {
        if (node.left == null) node.left = new Node();
        if (node.right == null) node.right = new Node();
        if (!node.flag) return;
        node.left.sum = node.data * leftNum;
        node.right.sum = node.data * rightNum;
        node.left.data = node.right.data = node.data;
        node.left.flag = node.right.flag = true;
        node.flag = false;
    }
} 
```
 
 
### 字典树
- 基本字典树模型
 [数组中两个数的最大异或值](https://github.com/JadenSailing/algorithm-lib/blob/main/Trie/Solution_LC_421_%20%E6%95%B0%E7%BB%84%E4%B8%AD%E4%B8%A4%E4%B8%AA%E6%95%B0%E7%9A%84%E6%9C%80%E5%A4%A7%E5%BC%82%E6%88%96%E5%80%BC.cs)
- 可删除字典树
 [找出强数对的最大异或值II](https://github.com/JadenSailing/algorithm-lib/blob/main/Trie/Solution_LC_2935_%E6%89%BE%E5%87%BA%E5%BC%BA%E6%95%B0%E5%AF%B9%E7%9A%84%E6%9C%80%E5%A4%A7%E5%BC%82%E6%88%96%E5%80%BC%20II.cs)
### 平衡树
 
- 红黑树
- Treap

  - [Treap实现的Map模板](https://github.com/JadenSailing/algorithm-lib/blob/main/%E5%B9%B3%E8%A1%A1%E6%A0%91/T_Map.cs)
	
	>平衡树全接口支持Insert/Delete/Kth/Rank/LowerBound/UpperBound
	
  - [避免洪水泛滥](https://github.com/JadenSailing/algorithm-lib/blob/main/%E5%B9%B3%E8%A1%A1%E6%A0%91/Solution_LC_1488_%E9%81%BF%E5%85%8D%E6%B4%AA%E6%B0%B4%E6%B3%9B%E6%BB%A5.cs)
    

  - [序列顺序查询](https://leetcode.cn/problems/sequentially-ordinal-rank-tracker/) [[代码]](https://github.com/JadenSailing/algorithm-lib/blob/main/%E5%B9%B3%E8%A1%A1%E6%A0%91/Solution_LC_2102_%E5%BA%8F%E5%88%97%E9%A1%BA%E5%BA%8F%E6%9F%A5%E8%AF%A2.cs)
 
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
- Dijkstra
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
	public int ShortestPath(int node1, int node2)
    {
        int[] dis = new int[n];
        Array.Fill(dis, int.MaxValue);
        PriorityQueue<int, int> pq = new PriorityQueue<int, int>();
        dis[node1] = 0;
        pq.Enqueue(node1, 0);
        while (pq.Count > 0)
        {
            int u = pq.Dequeue();
            foreach(int v in g[u].Keys)
            {
                int d = g[u][v] + dis[u];
                if(d < dis[v])
                {
                    dis[v] = d;
                    pq.Enqueue(v, d);
                }
            }
        }
        return dis[node2] == int.MaxValue ? -1 : dis[node2];
    }
   ```
	另外有基于二叉堆/斐波那契堆等优化方案
	[1976. 到达目的地的方案数](https://leetcode.cn/problems/number-of-ways-to-arrive-at-destination/)
	[>题解](https://github.com/JadenSailing/algorithm-lib/blob/main/Graphs/Solution_LC_1976_%E5%88%B0%E8%BE%BE%E7%9B%AE%E7%9A%84%E5%9C%B0%E7%9A%84%E6%96%B9%E6%A1%88%E6%95%B0.cs)

- A*
 
### 最小生成树
 
### Tarjan
- 求割边(桥)
```
private void Tarjan(HashSet<int>[] g, int u, int fa, int time)
{
	dfn[u] = low[u] = time++;
	foreach(int v in g[u])
	{
		if(v == fa) continue;
		if(dfn[v] == 0)
		{
			Tarjan(g, v, u, time);
			low[u] = Math.Min(low[u], low[v]);
			if(low[v] > dfn[u])
			{
				bridges.Add(new List<int>(){u, v});
			}
		}
		else
		{
			low[u] = Math.Min(low[u], dfn[v]);
		}
	}
}
```
- 完整模板 包括记录环个数和分组
[CF923-div3-F.Microcycle-求带权无向图中最短边权重最小的简单环](https://codeforces.com/contest/1927/submission/245749383)

本题解包含了Tarjan求环 Dijkstra求最短路 记录path等
 
## 并查集
 
- [基础模板](https://github.com/JadenSailing/algorithm-lib/blob/main/UnionFind/UnionFind.cs)
 
- [省份数量](https://github.com/JadenSailing/algorithm-lib/blob/main/UnionFind/Solution_LC_547_%E7%9C%81%E4%BB%BD%E6%95%B0%E9%87%8F.cs)
 
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
- 容斥原理
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
 
### 灵神八题
 
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