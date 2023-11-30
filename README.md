







# AutumnMist's Algorithm Library
 ## C# Algorithm IO
 1. 可用宏区分ACM模式或核心代码模式
 2.  工程主类里处理了ACM模式下的常用输入输出   [Program](https://github.com/JadenSailing/algorithm-lib/blob/main/Program.cs)
 3. LeetCodeSolution目录下是核心代码模式的支持。得益于C#反射机制，做到了一键复制粘贴的支持。能处理各种情况的输入输出，自动调用Solution和Input [LeetCodeSolution](https://github.com/JadenSailing/algorithm-lib/tree/main/LeetCodeSolution)
 ## 基础
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
 $二维前缀和pre[i][j]定义为从左上角到(i,j)位置围成的矩形范围内的元素和$
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
//计算任意矩形元素和
public int SumRegion(int row1, int col1, int row2, int col2)
{
    return pre[row2 + 1][col2 + 1] - pre[row1][col2 + 1] - pre[row2 + 1][col1] + pre[row1][col1];
}
```
 
 [二维区域和-矩阵不可变](https://github.com/JadenSailing/algorithm-lib/blob/main/PrefixSum/Solution_LC_304_%E4%BA%8C%E7%BB%B4%E5%8C%BA%E5%9F%9F%E5%92%8C%E6%A3%80%E7%B4%A2%20-%20%E7%9F%A9%E9%98%B5%E4%B8%8D%E5%8F%AF%E5%8F%98.cs)
 
 ### 差分
 - 一维差分
 
- 二维差分
 
 ## 数组
 
 ## 哈希表

 ## 栈
 
 ## 队列
 
 ## 链表
 
 ## 堆
 
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

 ## 树
 
 ### 树状数组
 
- [基础模板](https://github.com/JadenSailing/algorithm-lib/blob/main/BinaryIndexedTree/BIT.cs)
 
- [区域和检索-数组可修改](https://github.com/JadenSailing/algorithm-lib/blob/main/BinaryIndexedTree/Solution_LC_307_%E5%8C%BA%E5%9F%9F%E5%92%8C%E6%A3%80%E7%B4%A2-%E6%95%B0%E7%BB%84%E5%8F%AF%E4%BF%AE%E6%94%B9.cs)
 
 ### 线段树
 
 ### 平衡树
 
- 红黑树
- Treap
 
 ## 图
 
 ## 并查集
 
- [基础模板](https://github.com/JadenSailing/algorithm-lib/blob/main/UnionFind/UnionFind.cs)
 
- [省份数量](https://github.com/JadenSailing/algorithm-lib/blob/main/UnionFind/Solution_LC_547_%E7%9C%81%E4%BB%BD%E6%95%B0%E9%87%8F.cs)
 
 ## 数论
 
 ## 位运算
 
 ## 动态规划
 
 ## 计算几何
 
 ## 其它
 
 
 