/*
 ����һ���±�� 0 ��ʼ���������� nums �����һ������ x �� y �������������������Ϊ ǿ���� ��

|x - y| <= min(x, y)
����Ҫ�� nums ��ѡ�����������������㣺���������������γ�һ��ǿ���ԣ��������ǵİ�λ���XOR��ֵ���ڸ���������ǿ�����е� ���ֵ ��

�������� nums ���п��ܵ�ǿ�����е� ��� ���ֵ��

ע�⣬�����ѡ��ͬһ�������������γ�һ��ǿ���ԡ�
 * 
 */

public class Solution_LC_2935_�ҳ�ǿ���Ե�������ֵII
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