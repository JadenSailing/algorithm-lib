/*
35. ��������λ��
��

����һ�����������һ��Ŀ��ֵ�����������ҵ�Ŀ��ֵ�������������������Ŀ��ֵ�������������У����������ᱻ��˳������λ�á�

�����ʹ��ʱ�临�Ӷ�Ϊ O(log n) ���㷨��
*/

public class Solution_LC_35
{
    public int SearchInsert(int[] nums, int target)
    {
        //low high��Ϊ���顾��Ч���±�
        int low = 0, high = nums.Length - 1;
        //ʹ�� <= �жϣ��˳�ʱ low = high + 1
        while (low <= high)
        {
            //����д����Խ��
            int mid = low + (high - low) / 2;
            //Check����������������
            // >= Ŀ��ֵ�ı��ų���������high����Чֵ������һλ
            //�����Ϊ>�����⣬��Ϊ��<target���͡�=target�������ͳһ
            if (nums[mid] >= target) high = mid - 1;
            //��low����Чֵ�±�
            else low = mid + 1;
        }
        return low;
    }
}