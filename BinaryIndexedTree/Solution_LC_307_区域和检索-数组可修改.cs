/*
给你一个数组 nums ，请你完成两类查询。
其中一类查询要求 更新 数组 nums 下标对应的值
另一类查询要求返回数组 nums 中索引 left 和索引 right 之间（ 包含 ）的nums元素的 和 ，其中 left <= right
实现 NumArray 类：

NumArray(int[] nums) 用整数数组 nums 初始化对象
void update(int index, int val) 将 nums[index] 的值 更新 为 val
int sumRange(int left, int right) 返回数组 nums 中索引 left 和索引 right 之间（ 包含 ）的nums元素的 和 （即，nums[left] + nums[left + 1], ..., nums[right]）
*/

public class NumArray_307
{
    private BIT tree;
    public NumArray_307(int[] nums)
    {
        tree = new BIT(nums.Length);
        for (int i = 0; i < nums.Length; i++) tree.Update(i + 1, nums[i]);
    }

    public void Update(int index, int val)
    {
        //不保留原数组
        int old = SumRange(index, index);
        this.tree.Update(index + 1, val - old);
    }

    public int SumRange(int left, int right)
    {
        return tree.Query(right + 1) - tree.Query(left);
    }
}
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
        while (i <= n)
        {
            tree[i] += x;
            i += LowBit(i);
        }
    }
}