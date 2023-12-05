/*
 ����һ�� n ���ڵ������һ��������ͨ���޻�ͼ����ÿ���ڵ��ʾһ�����У���Ŵ� 0 �� n - 1 ����ǡ���� n - 1 ��·��0 ���׶�������һ����ά�������� roads ������ roads[i] = [ai, bi] ����ʾ���� ai �� bi ֮����һ�� ˫��· ��

ÿ����������һ���������Ƕ�Ҫȥ�׶��μ�һ�����顣

ÿ����������һ����������һ������ seats ��ʾÿ����������λ����Ŀ��

������Ĵ������ѡ��������ڳ��еĳ������߳����������еĳ������ڳ���֮��һ�������ͺ���һ�����͡�

���㷵�ص����׶�������Ҫ���������͡�
*/

public class Solution_LC_2477_�����׶��������ͺ�
{
    public long MinimumFuelCost(int[][] roads, int seats)
    {
        return DFS(BuildGraph(roads), 0, -1, seats)[1];
    }
    private long[] DFS(HashSet<int>[] g, int u, int p, int seats)
    {
        long pCount = 1;
        long cost = 0;
        //���δ����ӽڵ�
        foreach (int v in g[u])
        {
            //����
            if (v == p) continue;
            long[] child = DFS(g, v, u, seats);
            pCount += child[0];
            cost += child[1] + (child[0] + seats - 1) / seats;
        }
        //���ص�ǰ�ڵ����������
        return new long[2] { pCount, cost };
    }
    //��׼��ͼ
    private HashSet<int>[] BuildGraph(int[][] edges)
    {
        int n = edges.Length + 1;
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