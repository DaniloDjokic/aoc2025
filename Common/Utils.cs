namespace Common;

public static class Utils
{
    public static string[] ReadLines(string fileName)
    {
        string projectRoot = Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.Parent.FullName;
        string filePath = Path.Combine(projectRoot, fileName);
        return File.ReadAllLines(filePath);
    }

    public static int CountDigits(long num)
    {
        return num == 0 ? 1 : (int)Math.Floor(Math.Log10(Math.Abs(num))) + 1;
    }
    
    public static int CountDigits(int num)
    {
         return num == 0 ? 1 : (int)Math.Floor(Math.Log10(Math.Abs(num))) + 1;
    }
}