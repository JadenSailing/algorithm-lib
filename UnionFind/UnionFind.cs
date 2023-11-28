namespace AutumnMist
{
    public class UnionFind
    {
        public int n = 0;
        private int[] fa;
        public int setCount = 0;
        public UnionFind(int n)
        {
            this.n = n;
            this.setCount = n;
            this.fa = new int[n];
            for (int i = 0; i < n; i++) this.fa[i] = i;
        }

        public int Find(int x)
        {
            return fa[x] == x ? x : (fa[x] = Find(fa[x]));
        }

        public bool IsConnected(int x, int y)
        {
            return Find(x) == Find(y);
        }

        public bool Connect(int x, int y)
        {
            if (IsConnected(x, y)) return false;
            fa[Find(x)] = Find(y);
            setCount--;
            return true;
        }
    }
}