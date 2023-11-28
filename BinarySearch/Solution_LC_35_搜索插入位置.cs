/*
35. 搜索插入位置
简单

给定一个排序数组和一个目标值，在数组中找到目标值，并返回其索引。如果目标值不存在于数组中，返回它将会被按顺序插入的位置。

请必须使用时间复杂度为 O(log n) 的算法。
*/

public class Solution_LC_35
{
    public int SearchInsert(int[] nums, int target)
    {
        //low high均为数组【有效】下标
        int low = 0, high = nums.Length - 1;
        //使用 <= 判断，退出时 low = high + 1
        while (low <= high)
        {
            //以下写法防越界
            int mid = low + (high - low) / 2;
            //Check函数根据需求设置
            // >= 目标值的被排除，故最终high是有效值的左侧第一位
            //如果改为>有问题，因为【<target】和【=target】结果不统一
            if (nums[mid] >= target) high = mid - 1;
            //故low是有效值下标
            else low = mid + 1;
        }
        return low;
    }
}