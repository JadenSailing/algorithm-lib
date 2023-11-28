using LeetCode;
using System.Reflection;
using System.Reflection.Metadata;

public class Util
{
    private static HashSet<string> methodBlackList = new HashSet<string>()
    {
        "GetType", "ToString", "Equals", "GetHashCode",
    };
    public static void Call<T>(T solution, string[] lines)
    {
        try
        {
            Type type = typeof(T);
            var methods = type.GetMethods();
            MethodInfo target = null;
            int mCount = 0;
            foreach (var method in methods)
            {
                if (method.IsAbstract) continue;
                if (method.IsConstructor) continue;
                if (!methodBlackList.Contains(method.Name))
                {
                    target = method;
                    mCount++;
                }
            }
            if (target == null)
            {
                Log("No public method found");
                return;
            }
            if(mCount > 1)
            {
                Log("Over 1 method found");
                return;
            }
            ParameterInfo[] paramInfos = target.GetParameters();
            int paramLen = paramInfos.Length;
            int index = 0;
            while (index < lines.Length)
            {
                object[] param = new object[paramInfos.Length];
                for (int i = 0; i < paramInfos.Length; i++)
                {
                    param[i] = GetParam(paramInfos[i], lines[i + index]);
                }
                var res = target.Invoke(solution, param);
                FormatOutPut(target.ReturnParameter, res);
                index += paramLen;
            }
        }
        catch (System.Exception e)
        {
            Log(e);
        }
    }

    private static void FormatOutPut(ParameterInfo param, object res)
    {
        Type t = param.ParameterType;
        if (t == typeof(string))
        {
            Log(res);
            return;
        }
        if(t.Name.Contains("Void"))
        {
            Log("Return Void...");
            return;
        }
        if (t.IsValueType)
        {
            Log(res);
            return;
        }
        if (t.IsArray)
        {
            var eType = t.GetElementType();
            OutPut.LineArray(res as Array);
            return;
        }
        if (res is TreeNode)
        {
            OutPut.Line(DataStructure.SerializeTree(res as TreeNode));
            return;
        }
        if(res is ListNode)
        {
            OutPut.Line(DataStructure.SerializeList(res as ListNode));
            return;
        }
        OutPut.Line(res);
    }

    public static object? GetParam(ParameterInfo param, string line)
    {
        Type t = param.ParameterType;
        if (t == typeof(string)) return line.Trim('\"');
        if (t == typeof(int)) return int.Parse(line);
        if (t == typeof(double)) return double.Parse(line);
        if (t == typeof(float)) return float.Parse(line);
        if (t == typeof(char)) return line[0];
        if (t == typeof(long)) return long.Parse(line);
        if (t.IsArray)
        {
            var eType = t.GetElementType();
            if (eType == typeof(string)) return Input.ReadStringArray(line);
            if (eType == typeof(int)) return Input.ReadIntArray(line);
            if (eType == typeof(int[])) return Input.ReadIntArray2D(line);
        }
        if (t.IsArray)
        {
            var eType = t.GetElementType();
            if (eType == typeof(string)) return Input.ReadStringArray(line);
            if (eType == typeof(int)) return Input.ReadIntArray(line);
            if (eType == typeof(int[])) return Input.ReadIntArray2D(line);
        }
        if(t.FullName.Contains("IList"))
        {
            if(t.FullName.IndexOf("IList") != t.FullName.LastIndexOf("IList"))
            {
                var arr = Input.ReadIntArray2D(line);
                IList<IList<int>> res = new List<IList<int>>();
                foreach (var a in arr)
                {
                    var list = new List<int>();
                    foreach (int k in a) list.Add(k);
                    res.Add(list);
                }
                return res;
            }
            else
            {
                var arr = Input.ReadIntArray(line);
                var list = new List<int>();
                foreach (int k in arr) list.Add(k);
                return list;
            }
        }
        if (t == typeof(TreeNode))
        {
            return DataStructure.DeserializeTree(line);
        }
        if (t == typeof(ListNode))
        {
            return DataStructure.DeserializeList(line);
        }
        if(t.IsGenericType && t.Name.Contains("IList"))
        {
            var eType = t.GenericTypeArguments[0];
            if(eType == typeof(string))
            {
                var arr = Input.ReadStringArray(line);
                var res = new List<string>();
                for (int i = 0; i < arr.Length; i++) res.Add(arr[i]);
                return res;
            }
            else if(eType == typeof(int))
            {
                var arr = Input.ReadIntArray(line);
                var res = new List<int>();
                for (int i = 0; i < arr.Length; i++) res.Add(arr[i]);
                return res;
            }
        }
        return null;
    }

    public static void Log(object obj)
    {
        Console.WriteLine(obj.ToString());
    }
}
