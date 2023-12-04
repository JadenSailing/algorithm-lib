/*
 * ����һ���������� nums ������ nums[i] XOR nums[j] ����������������� 0 �� i �� j < n ��
 * 
 */


public class Solution_LC_421_��������������������ֵ
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
        Trie node;
        foreach (int v in nums)
        {
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

            //check
            int res = 0;
            node = root;
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
        }
        return ans;
    }
}