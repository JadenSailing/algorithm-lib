/*
 * ����һ��������ͨ������������ n �����Ϊ 0...n-1 �Ľڵ��Լ� n-1 ���� ��

�������� n ������ edges �� edges[i] = [ai, bi]��ʾ���еĽڵ� ai �� bi ֮����һ���ߡ�

���س���Ϊ n ������ answer ������ answer[i] �����е� i ���ڵ������������ڵ�֮��ľ���֮�͡�

*/

public class Solution_LC_834_���о���֮��
{
    int[] ans;
    //root=0ʱ���ڵ��������С ��������
    int[] size;
    //root=0ʱ���ڵ㵽�ӽڵ�ľ����
    int[] dis;
    public int[] SumOfDistancesInTree(int n, int[][] edges)
    {
        var g = BuildGraph(n, edges);
        ans = new int[n];
        size = new int[n];
        dis = new int[n];
        DFS0(g, 0, -1);
        DFS(g, 0, -1, dis[0]);
        return ans;
    }

    private void DFS0(HashSet<int>[] g, int u, int p)
    {
        size[u] = 1;
        dis[u] = 0;
        foreach (int v in g[u])
        {
            if (v == p) continue;
            DFS0(g, v, u);
            //�ӽڵ��������ڸ��ӽڵ�����֮��
            size[u] += size[v];
            //�ӽڵ������ڸ��ӽڵ���� + ���ӽڵ����(ÿ����������1)
            dis[u] += dis[v] + size[v];
        }
    }

    private void DFS(HashSet<int>[] g, int u, int p, int d)
    {
        int n = g.Length;
        ans[u] = d;
        foreach (int v in g[u])
        {
            if (v == p) continue;
            //��u��v����������
            //�����������(n-size[v]) ������size[v]
            DFS(g, v, u, d + (n - size[v]) - size[v]);
        }
    }

    private HashSet<int>[] BuildGraph(int n, int[][] edges)
    {
        HashSet<int>[] g = new HashSet<int>[n];
        for (int i = 0; i < n; i++) g[i] = new HashSet<int>();
        for (int i = 0; i < edges.Length; i++)
        {
            int L = edges[i][0], R = edges[i][1];
            g[L].Add(R);
            g[R].Add(L);
        }
        return g;
    }
}