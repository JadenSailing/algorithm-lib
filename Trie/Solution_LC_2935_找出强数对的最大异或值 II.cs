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