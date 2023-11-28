using LeetCode;
using System;
using System.Text;

public class Input
{
    public static string[] ReadStringArray(string str)
    {
        int index = 0;
        List<string> ans = new List<string>();
        if (str[index] != '[') return ans.ToArray();
        index++;
        if (str[index] == ']') return ans.ToArray();
        while (true)
        {
            index++;
            StringBuilder sb = new StringBuilder();
            int flag = 1;
            while (str[index] != '\"')
            {
                sb.Append(str[index]);
                index++;
            }
            ans.Add(sb.ToString());
            index++;
            if (str[index] == ']') break;
            index++;
        }
        return ans.ToArray();
    }
    public static int[] ReadIntArray(string str)
    {
        int index = 0;
        return ReadIntArray(str, ref index);
    }
    public static int[] ReadIntArray(string str, ref int index)
    {
        //[1,5,5,4,11]
        List<int> ans = new List<int>();
        if(str[index] != '[') return null;
        index++;
        while(true)
        {
            string num = "";
            int flag = 1;
            if(str[index] == '-')
            {
                flag = -1;
                index++;
            }
            while(str[index] >= '0' && str[index] <= '9')
            {
                num += str[index].ToString();
                index++;
            }
            if(num.Length > 0) ans.Add((int)(long.Parse(num) * flag));
            if(str[index++] == ']') break;
        }
        return ans.ToArray();
    }
    public static int[][] ReadIntArray2D(string str)
    {
        //[[0,1],[1,2],[1,3],[3,4]]
        List<int[]> ans = new List<int[]>();
        int index = 0;
        if(str[index] != '[') return null;
        index++; //[
        while(true)
        {
            int[] arr = ReadIntArray(str, ref index);
            ans.Add(arr);
            if(str[index++] == ']') break;
        }
        //index++; //]
        return ans.ToArray();
    }
}