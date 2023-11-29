namespace AutumnMist
{
    //树状数组基础模板
    //下标从1开始
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
            while(i > 0)
            {
                res += tree[i];
                i -= LowBit(i);
            }
            return res;
        }

        public void Update(int i, int x)
        {
            while(i <= n)
            {
                tree[i] += x;
                i += LowBit(i);
            }
        }
    }
}