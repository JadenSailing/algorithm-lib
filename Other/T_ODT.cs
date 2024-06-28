namespace LeetCode
{
    public class ODT
    {
        public class Block
        {
            public int L, R;
            public long val;
            public Block next;
            public Block(int L, int R, long val)
            {
                this.L = L;
                this.R = R;
                this.val = val;
            }
        }
        Block root, bL, bR;
        public ODT(long[] nums)
        {
            int n = nums.Length;
            root = new Block(0, 0, nums[0]);
            var t = root;
            for (int i = 1; i < n; i++)
            {
                Block b = new Block(i, i, nums[i]);
                t.next = b;
                t = b;
            }
            bL = bR = null;
        }

        //返回以mid为起点的区间
        public Block Split(int mid)
        {
            var node = root;
            while (node != null)
            {
                if (node.L == mid) return node;
                if (node.L < mid && node.R >= mid)
                {
                    Block r = new Block(mid, node.R, node.val);
                    node.R = mid - 1;
                    r.next = node.next;
                    node.next = r;
                    return r;
                }
                node = node.next;
            }
            return null;
        }

        private void Prepare(int L, int R)
        {
            bL = Split(L);
            bR = Split(R + 1);
        }

        public void Add(int L, int R, long val)
        {
            Prepare(L, R);
            for (var node = bL; node != bR; node = node.next)
            {
                node.val += val;
            }
        }
        public void Merge(int L, int R, long val)
        {
            Prepare(L, R);
            bL.R = R;
            bL.next = bR;
            bL.val = val;
        }
        public long Kth(int L, int R, int k)
        {
            Prepare(L, R);
            List<Block> list = new List<Block>();
            for (var node = bL; node != bR; node = node.next)
            {
                list.Add(node);
            }
            list.Sort((A, B) => Math.Sign(A.val - B.val));
            foreach (var node in list)
            {
                int count = node.R - node.L + 1;
                if (count >= k) return node.val;
                k -= count;
            }
            return 0;
        }
        private long Power(long x, long y, long p)
        {
            long r = 1;
            x %= p;
            while (y > 0)
            {
                if (y % 2 == 1) r = r * x % p;
                y /= 2;
                x = x * x % p;
            }
            return r;
        }
        public long PowerSum(int L, int R, long x, long y)
        {
            Prepare(L, R);
            long res = 0;
            for (var node = bL; node != bR; node = node.next)
            {
                res += (node.R - node.L + 1) * Power(node.val, x, y);
            }
            return res % y;
        }
    }
}
