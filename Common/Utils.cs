namespace Common;

public static class Extensions
{
    extension(Utils.Point[,] grid)
    {
        public List<Utils.Point> FilterGrid(Func<Utils.Point, bool> predicate)
        {
            List<Utils.Point> points = [];

            foreach (var point in grid)
            {
                if (predicate(point))
                {
                    points.Add(point);
                }
            }
        
            return points;
        }

        public Utils.Point GetSpecificPoint(Func<Utils.Point, bool> predicate)
        {
            List<Utils.Point> points = [];

            foreach (var point in grid)
            {
                if (predicate(point))
                {
                    points.Add(point);
                }
            }

            if (points.Count != 1)
            {
                throw new ArgumentException("There are two points with the same value.");
            }
        
            return points[0];
        }
    }
}

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

    public struct Point(int x, int y, char value) : IEquatable<Point>
    {
        public int X { get; init; } = x;
        public int Y { get; init; } = y;
        public char Value { get; set; } = value;

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

        public bool Equals(Point other)
        {
            return this.X == other.X && this.Y == other.Y;
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

    public static List<Point?> GetAllAdjacent(Point[,] grid, Point point)
    {
        List<Point?> points = [];
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

    public class Range(long start, long end)
    {
        public long Start { get; init; } = start;
        public long End { get; init; } = end;

        public Range? OvarlapsWith(Range other)
        {
            //Case 0: equal
            if (this.Start == other.Start && this.End == other.End)
            {
                return this;
            }
            
            //Case 1: overlaps on left
            if (this.Start <= other.Start && 
                this.End >= other.Start && this.End <= other.End)
            {
                return new Range(this.Start, other.End);
            }
            
            //Case 2: overlaps on right
            if (this.Start >= other.Start && this.Start <= other.End
                                          && this.End >= other.End)
            {
                return new Range(other.Start, this.End);
            }
            
            //Case 3: this overlaps other
            if (this.Start <= other.Start && this.End >= other.End)
            {
                return this;
            }
            
            //Case 4: other overlaps this
            if (other.Start <= this.Start && other.End >= this.End)
            {
                return other;
            }
            
            //Case 5: doesn't overlap
            return null;
        }
    }
}