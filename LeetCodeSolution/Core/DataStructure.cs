namespace LeetCode
{
    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int x, TreeNode left = null, TreeNode right = null)
        {
            val = x;
            this.left = left;
            this.right = right;
        }
    }
    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int val = 0, ListNode next = null)
        {
            this.val = val;
            this.next = next;
        }
    }

    public static class DataStructure
    {
        public static string SerializeTree(TreeNode? root)
        {
            string ans = "[";
            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(root);
            int nullCount = 0;
            while (queue.Count > 0)
            {
                TreeNode node = queue.Dequeue();
                if (node == null) nullCount++;
                else
                {
                    if (nullCount > 0)
                    {
                        for (int i = 0; i < nullCount; i++)
                        {
                            if (ans.Length == 1) ans += "null";
                            else ans += ",null";
                        }
                        nullCount = 0;
                    }
                    if (ans.Length == 1) ans += node.val;
                    else ans += "," + node.val;
                    queue.Enqueue(node.left);
                    queue.Enqueue(node.right);
                }
            }
            ans += "]";
            return ans;
        }

        // Decodes your encoded data to tree.
        public static TreeNode DeserializeTree(string data)
        {
            if (data == "[]") return null;
            data = data.Substring(1, data.Length - 2);
            string[] arr = data.Split(',');
            TreeNode root = GetNode(arr, 0);
            Queue<TreeNode> queue = new Queue<TreeNode>();
            TreeNode parent = root;
            int index = 1;
            while (index < arr.Length)
            {
                parent.left = GetNode(arr, index);
                index++;
                parent.right = GetNode(arr, index);
                index++;
                if (parent.left != null) queue.Enqueue(parent.left);
                if (parent.right != null) queue.Enqueue(parent.right);
                parent = queue.Dequeue();
            }
            return root;
        }

        private static TreeNode GetNode(string[] arr, int index)
        {
            if (index >= arr.Length) return null;
            string v = arr[index];
            if (v == "null") return null;
            return new TreeNode(int.Parse(v));
        }

        public static string SerializeList(ListNode node)
        {
            if (node == null) return "[]";
            string ans = "[";
            while(node != null)
            {
                ans += node.val;
                if (node.next != null) ans += ",";
                node = node.next;
            }
            ans += "]";
            return ans;
        }
        public static ListNode DeserializeList(string data)
        {
            if (data == "[]") return null;
            data = data.Substring(1, data.Length - 2);
            string[] arr = data.Split(',');
            ListNode head = new ListNode();
            var tmp = head;
            for(int i = 0; i < arr.Length; i++)
            {
                tmp.next = new ListNode(int.Parse(arr[i]));
                tmp = tmp.next;
            }
            return head.next;
        }
    }
}