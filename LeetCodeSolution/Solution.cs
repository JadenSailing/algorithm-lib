public class Solution
{
    public int[] TwoSum(int[] nums, int target)
    {
        int n = nums.Length;
        Dictionary<int, int> dict = new Dictionary<int, int>();
        for(int i = 0; i < n; i++)
        {
            int v = nums[i];
            if (dict.ContainsKey(target - v)) return new int[] { dict[target - v], i };
            dict[v] = i;
        }
        return new int[] { -1, -1 };
    }
}