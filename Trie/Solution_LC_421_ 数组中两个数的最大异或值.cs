/*
 * ����һ���������� nums ������ nums[i] XOR nums[j] ����������������� 0 �� i �� j < n ��
 * 
 */

public class Trie
{
    public int val;
    public Trie[] next = new Trie[2];
    public bool isEnd;
}

public class Solution_LC_421_��������������������ֵ
{

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