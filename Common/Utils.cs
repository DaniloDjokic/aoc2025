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

    public class Point(int x, int y, char value)
    {
        private int X { get; init; } = x;
        private int Y { get; init; } = y;
        public char Value { get; init; } = value;

        private Point FromPoint(Point point)
        {
            return new Point(point.X,  point.Y, point.Value);
        }
        
        public Point? ApplyOffset((int x, int y) offset, Point[,] grid)
        {
            (int newX, int newY) newPos = (X + offset.x, Y + offset.y);

            if (IsOutOfBounds(grid, newPos))
            {
                return null;
            }
            
            return FromPoint(grid[newPos.newX, newPos.newY]);
        }
    }

    public static Point[,] ReadGrid(string fileName)
    {
        string projectRoot = Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.Parent.FullName;
        string filePath = Path.Combine(projectRoot, fileName);

        var lines = File.ReadAllLines(filePath);

        Point[,] grid = new Point[lines.Length, lines[0].Length];

        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[i].Length; j++)
            {
                grid[i, j] = new Point(i, j, lines[i][j]);
            }
        }

        return grid;
    }

    public static bool IsOutOfBounds(Point[,] grid, (int x, int y) offset)
    {
        return offset.x < 0 || offset.x >= grid.GetLength(0) || offset.y < 0 || offset.y >= grid.GetLength(1);
    }

    public static List<Point> GetAllAdjacent(Point[,] grid, Point point)
    {
        List<Point> points = [];
        (int x, int y)[] offsets =
        [
            (-1, -1), //Top left
            (-1, 0), //Top
            (-1, 1), //Top Right
            (0, -1), //Left
            (0, 1), //Right
            (1, -1), //Bot Left
            (1, 0), //Bot
            (1, 1) //Bot Right
        ];

        foreach (var offset in  offsets)
        {
            var newPoint = point.ApplyOffset(offset, grid);

            if (newPoint is not null)
            {
                points.Add(newPoint);
            }
        }

        return points;
    }
}