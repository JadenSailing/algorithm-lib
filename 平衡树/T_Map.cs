
namespace LeetCode
{
    //标准平衡树(有序集合)实现
    public class Map<T> where T : IComparable, IEquatable<T>
    {
        class Node<TKey> where TKey : IComparable
        {
            public Node<TKey> left;
            public Node<TKey> right;
            public TKey key;
            public int priority;
            public int size;
            public int count;
        }
        private Node<T> root = null;

        private Random randGenterator = new Random(new DateTime().Millisecond);

        public bool Insert(T x)
        {
            if (x == null) return false;
            root = Insert(root, x);
            return true;
        }
        public bool Delete(T x)
        {
            root = Delete(root, x);
            return true;
        }
        public int UpperBound(T x)
        {
            return UpperBound(root, x);
        }
        public int LowerBound(T x)
        {
            return LowerBound(root, x);
        }
        //返回第k个元素 从0开始
        public T Kth(int k)
        {
            return Kth(root, k);
        }
        public int Count { get { return root == null ? 0 : root.size; } }

        private int RandomPriority() { return randGenterator.Next(); }
        private Node<T> NewNode(T x)
        {
            Node<T> node = new Node<T>() { size = 1, count = 1, key = x, priority = RandomPriority() };
            return node;
        }
        private void Update(Node<T> u)
        {
            int size = u.count;
            if (u.left != null) size += u.left.size;
            if (u.right != null) size += u.right.size;
            u.size = size;
        }
        private Node<T> Rotate(Node<T> o, bool leftRotate)
        {
            Node<T> k;
            if (leftRotate)
            {
                k = o.right;
                o.right = k.left;
                k.left = o;
            }
            else
            {
                k = o.left;
                o.left = k.right;
                k.right = o;
            }
            Update(o);
            Update(k);
            return k;
        }
        private Node<T> Insert(Node<T> node, T x)
        {
            if (node == null)
            {
                node = NewNode(x);
                return node;
            }
            int compare = x.CompareTo(node.key);
            if (compare == 0)
            {
                node.count++;
                Update(node);
            }
            else if (compare > 0) node.right = Insert(node.right, x);
            else node.left = Insert(node.left, x);
            if (node.left != null && node.priority > node.left.priority) node = Rotate(node, false);
            if (node.right != null && node.priority > node.right.priority) node = Rotate(node, true);
            Update(node);
            return node;
        }

        private Node<T> Delete(Node<T> node, T x)
        {
            if (node.key.Equals(x))
            {
                if (node.count > 1)
                {
                    node.count--;
                    Update(node);
                    return node;
                }
                if (node.left == null && node.right == null) return null;
                if (node.left == null) return node.right;
                if (node.right == null) return node.left;
                if (node.left.priority < node.right.priority)
                {
                    node = Rotate(node, false);
                    node.right = Delete(node.right, x);
                    Update(node);
                    return node;
                }
                else
                {
                    node = Rotate(node, true);
                    node.left = Delete(node.left, x);
                    Update(node);
                    return node;
                }
            }
            if (node.key.CompareTo(x) > 0) node.left = Delete(node.left, x);
            else node.right = Delete(node.right, x);
            Update(node);
            return node;
        }

        private int UpperBound(Node<T> u, T x)
        {
            if (u == null) return 0;
            int compare = x.CompareTo(u.key);
            int lSize = (u.left == null ? 0 : u.left.size);
            if (compare >= 0) return lSize + u.count + UpperBound(u.right, x);
            return UpperBound(u.left, x);
        }
        private int LowerBound(Node<T> u, T x)
        {
            if (u == null) return 0;
            int compare = x.CompareTo(u.key);
            int lSize = (u.left == null ? 0 : u.left.size);
            if (compare == 0) return lSize;
            if (compare > 0) return lSize + u.count + LowerBound(u.right, x);
            return LowerBound(u.left, x);
        }

        private T Kth(Node<T> u, int k)
        {
            if (k < 0 || k >= Count) return default(T);
            int lSize = (u.left == null ? 0 : u.left.size);
            if (lSize > k) return Kth(u.left, k);
            if (lSize + u.count > k) return u.key;
            return Kth(u.right, k - lSize - u.count);
        }
    }
}
