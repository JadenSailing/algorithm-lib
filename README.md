



















# AutumnMist's Algorithm Library
 ## C# Algorithm IO
 1. 可用宏区分ACM模式或核心代码模式
 2.  工程主类里处理了ACM模式下的常用输入输出   [Program](https://github.com/JadenSailing/algorithm-lib/blob/main/Program.cs)
 3. LeetCodeSolution目录下是核心代码模式的支持。得益于C#反射机制，做到了一键复制粘贴的支持。能处理各种情况的输入输出，自动调用Solution和Input [LeetCodeSolution](https://github.com/JadenSailing/algorithm-lib/tree/main/LeetCodeSolution)
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


 ### 前缀和
 - 一维前缀和 
 ```
 int[] pre = new int[n + 1];
 for(int i = 0; i < n; i++) pre[i + 1] = pre[i] + nums[i];
 ```
 ```
 //区间和
 [L, R](0 <= L, R < n) = pre[R + 1] - pre[L];
 ```
 [区域和检索-数组不可变](https://github.com/JadenSailing/algorithm-lib/blob/main/PrefixSum/Solution_LC_303_%E5%8C%BA%E5%9F%9F%E5%92%8C%E6%A3%80%E7%B4%A2%20-%20%E6%95%B0%E7%BB%84%E4%B8%8D%E5%8F%AF%E5%8F%98.cs)
 
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
		pre[i + 1][j + 1] = pre[i + 1][j] + pre[i][j + 1] - pre[i][j] + matrix[i][j];
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
    return pre[row2 + 1][col2 + 1] - pre[row1][col2 + 1] - pre[row2 + 1][col1] + pre[row1][col1];
}
```
 
 [二维区域和-矩阵不可变](https://github.com/JadenSailing/algorithm-lib/blob/main/PrefixSum/Solution_LC_304_%E4%BA%8C%E7%BB%B4%E5%8C%BA%E5%9F%9F%E5%92%8C%E6%A3%80%E7%B4%A2%20-%20%E7%9F%A9%E9%98%B5%E4%B8%8D%E5%8F%AF%E5%8F%98.cs)
 
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
	diff[i] += diff[i - 1];
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
 ## 数组
 
 ## 哈希表

 ## 栈
 
 ## 队列
 
 ## 链表
 
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
        if (size == list.Count - 1)
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
	- Heapify from n / 2 -> 1
	- Pop & Swap(1, Size--)
	
 
 ## 字符串
 
 ## 排序

 ## 二分
 - 基础模板 [搜索插入位置](https://github.com/JadenSailing/algorithm-lib/blob/main/BinarySearch/Solution_LC_35_%E6%90%9C%E7%B4%A2%E6%8F%92%E5%85%A5%E4%BD%8D%E7%BD%AE.cs)
 ```
int low = 0, high = n - 1;
while(low <= high)
{
	int mid = low + (high - low) / 2;
	if(Check()) high = mid - 1
	else low = mid + 1
}
return low
```

 ## 双指针

 ## 贪心

 ## 树
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
     fa[v][i] = fa[fa[v][i - 1]][i - 1];
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
 ### 字典树
- 基本字典树模型
 [数组中两个数的最大异或值](https://github.com/JadenSailing/algorithm-lib/blob/main/Trie/Solution_LC_421_%20%E6%95%B0%E7%BB%84%E4%B8%AD%E4%B8%A4%E4%B8%AA%E6%95%B0%E7%9A%84%E6%9C%80%E5%A4%A7%E5%BC%82%E6%88%96%E5%80%BC.cs)
 - 可删除字典树
 [找出强数对的最大异或值II](https://github.com/JadenSailing/algorithm-lib/blob/main/Trie/Solution_LC_2935_%E6%89%BE%E5%87%BA%E5%BC%BA%E6%95%B0%E5%AF%B9%E7%9A%84%E6%9C%80%E5%A4%A7%E5%BC%82%E6%88%96%E5%80%BC%20II.cs)
 ### 平衡树
 
- 红黑树
- Treap
 
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
 - A*
 
 ### 最小生成树
 
 ### Tarjan
 
 ## 并查集
 
- [基础模板](https://github.com/JadenSailing/algorithm-lib/blob/main/UnionFind/UnionFind.cs)
 
- [省份数量](https://github.com/JadenSailing/algorithm-lib/blob/main/UnionFind/Solution_LC_547_%E7%9C%81%E4%BB%BD%E6%95%B0%E9%87%8F.cs)
 
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
全选巨无霸则`T=C*4`，全选小皇堡则`T=C*2`，所以`TC`只能在这之间，又番茄的最小单位为2，故`T`必须是偶数。注意实现时不要除零的情况，以及TC=0是合法的

 ## 位运算
 
 ### 状态压缩
 ### 枚举子集
 
 ## 动态规划
 
 ### 带权区间调度
 
 ### 树形dp
 自底向上处理数据，习惯用数组返回所需的各种数据，简单的可以用单值返回

 [到达首都的最少油耗](https://github.com/JadenSailing/algorithm-lib/blob/main/DP/%E6%A0%91%E5%BD%A2DP/Solution_LC_2477_%E5%88%B0%E8%BE%BE%E9%A6%96%E9%83%BD%E7%9A%84%E6%9C%80%E5%B0%91%E6%B2%B9%E8%80%97.cs)
 ### 换根dp
两边DFS，第一遍获取各节点数据和根节点的结果，第二遍换根转移递推其它节点结果。

[树中距离之和](https://github.com/JadenSailing/algorithm-lib/blob/main/DP/%E6%8D%A2%E6%A0%B9DP/Solution_LC_834_%E6%A0%91%E4%B8%AD%E8%B7%9D%E7%A6%BB%E4%B9%8B%E5%92%8C.cs)

 ### 数位dp

 ### dp优化

 ## 计算几何
 
 
 ## 其它
 
 
 