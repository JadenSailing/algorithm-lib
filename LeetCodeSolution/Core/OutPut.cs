public class OutPut
{
    public static void Line(object value)
    {
        Console.WriteLine(value);
    } 

    public static void LineArray(Array arr)
    {
        Console.WriteLine(GetLine(arr));
    }

    public static string GetLine(Array? arr)
    {
        int n = arr.Length;
        string line = "[";
        for(int i = 0; i < n; i++)
        {
            object obj = arr.GetValue(i);
            if(obj is Array)
            {
                line += GetLine(obj as Array);
            }
            else
            {
                line += obj.ToString();
            }
            if(i != n - 1) line += ",";
        }
        line += "]";
        return line;
    }

    private static string GetLine<T>(IList<T> list)
    {
        int n = list.Count;
        string line = "[";
        for(int i = 0; i < n; i++)
        {
            object obj = list[i];
            if(obj is IList<int>)
            {
                line += GetLine(obj as IList<int>);
            }
            else if(obj is Array)
            {
                line += GetLine(obj as Array);
            }
            else
            {
                line += obj.ToString();
            }
            if(i != n - 1) line += ",";
        }
        line += "]";
        return line;
    }

    public static void Line<T>(IList<T> list)
    {
        Console.WriteLine(GetLine<T>(list));
    }

    public static void Line<T1, T2>(IDictionary<T1, T2> dict)
    {
        string line = "[";
        int count = dict.Count;
        int index = 0;
        foreach(T2 obj in dict.Values)
        {
            if(obj is Array)
            {
                line += GetLine(obj as Array);
            }
            else
            {
                line += obj.ToString();
            }
            if(index != count - 1) line += ",";
            index++;
        }
        line += "]";
        Console.WriteLine(line);
    }

}