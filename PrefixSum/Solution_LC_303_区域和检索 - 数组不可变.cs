public class NumArray_303
{
    int[] pre;
    public NumArray_303(int[] nums)
    {
        int n = nums.Length;
        pre = new int[n + 1];
        for (int i = 0; i < n; i++) pre[i + 1] = pre[i] + nums[i];
    }

    public int SumRange(int left, int right)
    {
        return pre[right + 1] - pre[left];
    }
}

/**
 * Your NumArray object will be instantiated and called as such:
 * NumArray obj = new NumArray(nums);
 * int param_1 = obj.SumRange(left,right);
 */