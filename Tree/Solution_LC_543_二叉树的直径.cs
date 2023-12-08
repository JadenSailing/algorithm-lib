
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
public class Solution_LC_543_��������ֱ��
{
    private int ans = 0;
    public int DiameterOfBinaryTree(TreeNode root)
    {
        DFS(root);
        return ans;
    }

    //����dp �����������µ���󳤶�+1
    public int DFS(TreeNode node)
    {
        if (node == null) return 0;
        int left = 0, right = 0;
        if (node.left != null) left = DFS(node.left);
        if (node.right != null) right = DFS(node.right);
        ans = Math.Max(ans, left + right);
        //�����ǰ�ڵ㲻Ϊ�� ��ô���Ӹ��ڵ��ֱ��+1
        return Math.Max(left, right) + 1;
    }
}