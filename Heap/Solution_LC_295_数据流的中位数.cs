
/*
 * 中位数是有序整数列表中的中间值。如果列表的大小是偶数，则没有中间值，中位数是两个中间值的平均值。

例如 arr = [2,3,4] 的中位数是 3 。
例如 arr = [2,3] 的中位数是 (2 + 3) / 2 = 2.5 。
实现 MedianFinder 类:

MedianFinder() 初始化 MedianFinder 对象。

void addNum(int num) 将数据流中的整数 num 添加到数据结构中。

double findMedian() 返回到目前为止所有元素的中位数。与实际答案相差 10-5 以内的答案将被接受。
*/

public class MedianFinder
{
    private PriorityQueue<int, int> pqL;
    private PriorityQueue<int, int> pqR;
    public MedianFinder()
    {
        pqL = new PriorityQueue<int, int>();
        pqR = new PriorityQueue<int, int>();
    }

    public void AddNum(int num)
    {
        if (pqL.Count == pqR.Count) pqL.Enqueue(num, -num);
        else pqR.Enqueue(num, num);
        //交换堆顶
        if (pqL.Count > 0 && pqR.Count > 0)
        {
            int vL = pqL.Peek();
            int vR = pqR.Peek();
            if (vL > vR)
            {
                pqL.Dequeue();
                pqR.Dequeue();
                pqL.Enqueue(vR, -vR);
                pqR.Enqueue(vL, vL);
            }
        }
    }

    public double FindMedian()
    {
        if (pqL.Count == pqR.Count) return (pqL.Peek() + pqR.Peek()) / 2.0;
        else return pqL.Peek();
    }
}

/**
 * Your MedianFinder object will be instantiated and called as such:
 * MedianFinder obj = new MedianFinder();
 * obj.AddNum(num);
 * double param_2 = obj.FindMedian();
 */