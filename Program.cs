#define ACM
#undef ACM
#define MULTI_CASE
//#undef MULTI_CASE
#region import
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
#endregion

public class Program
{
    public static void Main(string[] args)
    {
#if ACM
#if MULTI_CASE
        int count = ReadInt();
#else
        int count = 1;
#endif
        CB();
        while (count-- > 0) Solve();
        CE();
#else
        string[] lines = File.ReadAllLines("Input.txt");
        Util.Call<Solution>(new Solution(), lines);
#endif
    }

    public static void Solve()
    {
    }

    #region static

    public static long mod = (long)1e9 + 7;
    public static long mod2 = 998244353;
    public static string YES { get { return "YES"; } }
    public static string NO { get { return "NO"; } }
    public static string Yes { get { return "Yes"; } }
    public static string No { get { return "No"; } }
    public static void Print(object t) { Console.WriteLine(t); }
    public static void Print(params object[] objs) { StringBuilder sb = new StringBuilder(); for (int i = 0; i < objs.Length; i++) { sb.Append(objs[i]); sb.Append(' '); } Print(sb); }
    public static void Print(Array arr) { StringBuilder sb = new StringBuilder(); for (int i = 0; i < arr.Length; i++) { sb.Append(arr.GetValue(i)); sb.Append(' '); } Print(sb); }
    public static void CB() { Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false }); }
    public static void CE() { Console.Out.Flush(); }
    public static string ReadLine() { return Console.ReadLine().Trim() ?? ""; }
    public static int ReadInt() { return int.Parse(ReadLine()); }
    public static int[] ReadIntArray() { return ReadLine().Split().Select(int.Parse).ToArray(); }
    public static (int, int) ReadInt2() { var tmp = ReadIntArray(); return (tmp[0], tmp[1]); }
    public static (int, int, int) ReadInt3() { var tmp = ReadIntArray(); return (tmp[0], tmp[1], tmp[2]); }
    public static (int, int, int, int) ReadInt4() { var tmp = ReadIntArray(); return (tmp[0], tmp[1], tmp[2], tmp[3]); }
    public static long ReadLong() { return long.Parse(ReadLine()); }
    public static long[] ReadLongArray() { return ReadLine().Split().Select(long.Parse).ToArray(); }
    public static (long, long) ReadLong2() { var tmp = ReadLongArray(); return (tmp[0], tmp[1]); }
    public static (long, long, long) ReadLong3() { var tmp = ReadLongArray(); return (tmp[0], tmp[1], tmp[2]); }
    public static (long, long, long, long) ReadLong4() { var tmp = ReadLongArray(); return (tmp[0], tmp[1], tmp[2], tmp[3]); }

    #endregion
}