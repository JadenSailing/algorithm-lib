


# AutumnMist's Algorithm Library(C#)
 ## C# Algorithm IO
 1. 可用宏区分ACM模式或核心代码模式
 2.  工程主类里处理了ACM模式下的常用输入输出   [Program](https://github.com/JadenSailing/algorithm-lib/blob/main/Program.cs)
 3. LeetCodeSolution目录下是核心代码模式的支持。得益于C#强大的反射机制，能处理各种情况的输入输出，自动调用Solution和Input  [LeetCodeSolution](https://github.com/JadenSailing/algorithm-lib/tree/main/LeetCodeSolution)
 ## 基础
 ### 前缀和
 ### 差分
 ## 数组
 ## 哈希表
 ## 栈
 ## 队列
 ## 链表
 ## 堆
 ## 字符串
 ## 排序
 ## 二分
 1. 基础模板 [搜索插入位置](https://github.com/JadenSailing/algorithm-lib/blob/main/BinarySearch/Solution_LC_35_%E6%90%9C%E7%B4%A2%E6%8F%92%E5%85%A5%E4%BD%8D%E7%BD%AE.cs)
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
 ## 图
 ## 并查集
 1. [基础模板](https://github.com/JadenSailing/algorithm-lib/blob/main/UnionFind/UnionFind.cs)
 ## 数论
 ## 位运算
 ## 动态规划
 ## 计算几何
 ## 其它
 
 