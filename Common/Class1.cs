namespace Common;

public static class Utils
{
    public static string[] ReadLines(string fileName)
    {
        string projectRoot = Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.Parent.FullName;
        string filePath = Path.Combine(projectRoot, fileName);
        return File.ReadAllLines(filePath);
    }
}