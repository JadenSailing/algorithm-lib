/*
 这里有 n 门不同的在线课程，按从 1 到 n 编号。给你一个数组 courses ，其中 courses[i] = [durationi, lastDayi] 表示第 i 门课将会 持续 上 durationi 天课，并且必须在不晚于 lastDayi 的时候完成。

你的学期从第 1 天开始。且不能同时修读两门及两门以上的课程。

返回你最多可以修读的课程数目。
*/

public class Solution_LC_630_课程表III
{
    public int ScheduleCourse(int[][] courses)
    {
        int n = courses.Length;
        Array.Sort(courses, (A, B) => (A[1] - B[1]));
        PriorityQueue<int, int> pq = new PriorityQueue<int, int>();
        int day = 0;
        for(int i = 0; i < n; i++)
        {
            int[] v = courses[i];
            if(day + v[0] <= v[1])
            {
                day += v[0];
                pq.Enqueue(v[0], -v[0]);
            }
            else
            {
                if(pq.Count > 0 && pq.Peek() > v[0])
                {
                    int mx = pq.Dequeue();
                    pq.Enqueue(v[0], -v[0]);
                    day -= mx;
                    day += v[0];
                }
            }
        }
        return pq.Count;
    }
}