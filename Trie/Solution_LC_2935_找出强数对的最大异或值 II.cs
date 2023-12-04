/*
 给你一个下标从 0 开始的整数数组 nums 。如果一对整数 x 和 y 满足以下条件，则称其为 强数对 ：

|x - y| <= min(x, y)
你需要从 nums 中选出两个整数，且满足：这两个整数可以形成一个强数对，并且它们的按位异或（XOR）值是在该数组所有强数对中的 最大值 。

返回数组 nums 所有可能的强数对中的 最大 异或值。

注意，你可以选择同一个整数两次来形成一个强数对。
 * 
 */


public class Solution_LC_2935_找出强数对的最大异或值II
{
    class Trie
    {
        public int count = 0;
        public bool isEnd;
        public Trie[] next;
        public Trie()
        {
            this.isEnd = false;
            this.next = new Trie[2];
        }
    }
    public int MaximumStrongPairXor(int[] nums)
    {
        int n = nums.Length;
        Array.Sort(nums);
        Trie root = new Trie();
        int ans = 0;
        int left = 0;
        for (int i = 0; i < n; i++)
        {
            int v = nums[i];
            var node = root;
            //add
            for (int k = 20; k >= 0; k--)
            {
                int flag = (v >> k) & 1;
                if (node.next[flag] == null) node.next[flag] = new Trie();
                node = node.next[flag];
                node.count++;
            }
            node.isEnd = true;
            //remove
            while (nums[left] * 2 < v)
            {
                node = root;
                int vL = nums[left];
                for (int k = 20; k >= 0; k--)
                {
                    int flag = (vL >> k) & 1;
                    node = node.next[flag];
                    node.count--;
                }
                left++;
            }
            //check
            node = root;
            int res = 0;
            for (int k = 20; k >= 0; k--)
            {
                int cur = (v >> k) & 1;
                if (node.next[1 - cur] != null && node.next[1 - cur].count > 0)
                {
                    res |= 1 << k;
                    node = node.next[1 - cur];
                }
                else if (node.next[cur] != null && node.next[cur].count > 0)
                {
                    node = node.next[cur];
                }
                else
                {
                    res = 0;
                    break;
                }
            }
            ans = Math.Max(ans, res);
        }
        return ans;
    }
}