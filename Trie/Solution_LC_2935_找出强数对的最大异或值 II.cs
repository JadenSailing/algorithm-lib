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
        public int val;
        public Trie[] next = new Trie[2];
        public bool isEnd;
    }
    public int FindMaximumXOR(int[] nums)
    {
        Trie root = new Trie();
        int ans = 0;
        foreach(int v in nums)
        {
            //check
            int res = 0;
            Trie node = root;
            for(int k = 31; k >= 0; k--)
            {
                int cur = (v >> k) & 1;
                if (node.next[1 - cur] != null)
                {
                    res |= 1 << k;
                    node = node.next[1 - cur];
                }
                else node = node.next[cur];
                if (node == null) break;
            }
            ans = Math.Max(ans, res);

            //add
            node = root;
            for(int k = 31; k >= 0; k--)
            {
                int cur = (v >> k) & 1;
                if (node.next[cur] == null)
                {
                    node.next[cur] = new Trie() { val = cur };
                }
                node = node.next[cur];
            }
            node.isEnd = true;
        }
        return ans;
    }
}