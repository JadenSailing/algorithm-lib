namespace LeetCode
{
    public class SegmentTreePersistent
    {
        public class Node
        {
            public bool valid = false; //是否是有效区间
            public Node left, right;
            public long sum, max, min;
            public bool flag = false; //延迟标记
            public long val; //延迟数据
        }
        private long n = (long)1e9; //一般不会超过这个上界
        private Node root = new Node();
        private Dictionary<int, Node> roots = new Dictionary<int, Node>();
        public bool isAdd = true; //默认是区间增加的形式，否则是区间覆盖
        public SegmentTreePersistent(bool isAdd = true) { this.isAdd = isAdd; roots[0] = root; }
        public bool IsValid(int version, long L, long R) { return IsValid(roots[version], 0, n, L, R); }
        public long Sum(int version, long L, long R) { return Sum(roots[version], 0, n, L, R); }
        public long Min(int version, long L, long R) { return Min(roots[version], 0, n, L, R); }
        public long Max(int version, long L, long R) { return Max(roots[version], 0, n, L, R); }
        public void Update(int version, long L, long R, long val) { Update(roots[version], 0, n, L, R, val, false); }
        public void AddVersion(int pre, int now) { roots[now] = CopyNode(roots[pre]); }

        private bool IsValid(Node node, long start, long end, long l, long r)
        {
            if (l <= start && end <= r) return node.valid;
            long mid = (start + end) >> 1;
            PushDown(node, mid - start + 1, end - mid);
            bool res = false;
            if (l <= mid) res |= IsValid(node.left, start, mid, l, r);
            if (r > mid) res |= IsValid(node.right, mid + 1, end, l, r);
            return res;
        }

        private Node CopyNode(Node srcNode)
        {
            Node node = new Node();
            node.valid = srcNode.valid;
            node.sum = srcNode.sum;
            node.left = srcNode.left;
            node.right = srcNode.right;
            node.val = srcNode.val;
            return node;
        }

        private Node Update(Node node, long start, long end, long l, long r, long val, bool copy = true)
        {
            if (copy) node = CopyNode(node);
            if (l <= start && end <= r)
            {
                if (isAdd)
                {
                    node.sum += (end - start + 1) * val;
                    node.val += val;
                    if (!node.valid)
                    {
                        node.max = node.min = val;
                    }
                    else
                    {
                        node.max += val;
                        node.min += val;
                    }
                }
                else
                {
                    node.sum = (end - start + 1) * val;
                    node.val = val;
                    node.max = node.min = val;
                }
                node.valid = true;
                node.flag = true;
                return node;
            }
            long mid = (start + end) >> 1;
            PushDown(node, mid - start + 1, end - mid);
            if (l <= mid) node.left = Update(node.left, start, mid, l, r, val);
            if (r > mid) node.right = Update(node.right, mid + 1, end, l, r, val);
            PushUp(node);
            return node;
        }
        private long Sum(Node node, long start, long end, long l, long r)
        {
            if (l <= start && end <= r) return node.sum;
            long mid = (start + end) >> 1, ans = 0;
            PushDown(node, mid - start + 1, end - mid);
            if (l <= mid) ans += Sum(node.left, start, mid, l, r);
            if (r > mid) ans += Sum(node.right, mid + 1, end, l, r);
            return ans;
        }

        private long Max(Node node, long start, long end, long l, long r)
        {
            if (l <= start && end <= r) return node.max;
            long mid = (start + end) >> 1, ans = long.MinValue;
            PushDown(node, mid - start + 1, end - mid);
            if (l <= mid && node.left.valid) ans = Math.Max(ans, Max(node.left, start, mid, l, r));
            if (r > mid && node.right.valid) ans = Math.Max(ans, Max(node.right, mid + 1, end, l, r));
            return ans;
        }

        private long Min(Node node, long start, long end, long l, long r)
        {
            if (l <= start && end <= r) return node.min;
            long mid = (start + end) >> 1, ans = long.MaxValue;
            PushDown(node, mid - start + 1, end - mid);
            if (l <= mid && node.left.valid) ans = Math.Min(ans, Min(node.left, start, mid, l, r));
            if (r > mid && node.right.valid) ans = Math.Min(ans, Min(node.right, mid + 1, end, l, r));
            return ans;
        }

        private void PushUp(Node node)
        {
            node.sum = node.left.sum + node.right.sum;
            node.max = long.MinValue;
            node.min = long.MaxValue;
            if (node.left.valid)
            {
                node.min = Math.Min(node.min, node.left.min);
                node.max = Math.Max(node.max, node.left.max);
            }
            if (node.right.valid)
            {
                node.min = Math.Min(node.min, node.right.min);
                node.max = Math.Max(node.max, node.right.max);
            }
            node.valid = node.valid || node.left.valid;
            node.valid = node.valid || node.right.valid;
        }
        private void PushDown(Node node, long leftNum, long rightNum)
        {
            if (node.left == null) node.left = new Node();
            if (node.right == null) node.right = new Node();
            if (!node.flag) return;
            if (isAdd)
            {
                node.left.sum += node.val * leftNum;
                node.right.sum += node.val * rightNum;
                if (node.left.valid)
                {
                    node.left.max += node.val;
                    node.left.min += node.val;
                }
                else
                {
                    node.left.max = node.val;
                    node.left.min = node.val;
                }
                if (node.right.valid)
                {
                    node.right.max += node.val;
                    node.right.min += node.val;
                }
                else
                {
                    node.right.max = node.val;
                    node.right.min = node.val;
                }
                node.left.val += node.val;
                node.right.val += node.val;
            }
            else
            {
                node.left.sum = node.val * leftNum;
                node.right.sum = node.val * rightNum;
                node.left.max = node.val;
                node.left.min = node.val;
                node.right.max = node.val;
                node.right.min = node.val;
                node.left.val = node.right.val = node.val;
            }
            node.left.valid = node.right.valid = true;
            node.left.flag = node.right.flag = true;
            node.val = 0;
            node.flag = false;
        }
    }
}