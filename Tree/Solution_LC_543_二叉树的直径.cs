
using LeetCode;
/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int val=0, TreeNode left=null, TreeNode right=null) {
 *         this.val = val;
 *         this.left = left;
 *         this.right = right;
 *     }
 * }
 */
public class Solution_LC_543_二叉树的直径
{
    private int ans = 0;
    public int DiameterOfBinaryTree(TreeNode root)
    {
        DFS(root);
        return ans;
    }

    //树形dp 返回子树向下的最大长度+1
    public int DFS(TreeNode node)
    {
        if (node == null) return 0;
        int left = 0, right = 0;
        if (node.left != null) left = DFS(node.left);
        if (node.right != null) right = DFS(node.right);
        ans = Math.Max(ans, left + right);
        //如果当前节点不为空 那么连接父节点后直径+1
        return Math.Max(left, right) + 1;
    }
}