using Common;
using Xunit;
using Xunit.Abstractions;

namespace Day9;

public class Day9(ITestOutputHelper testOutputHelper)
{
    private long Part1Solution(string[] lines)
    {
        var points = lines.Select(x =>
        {
            var split = x.Split(',');
            return new Utils.Point(int.Parse(split[0]), int.Parse(split[1]), '.');
        }).ToList();

        long maxArea = 0;

        for (int i = 0; i < points.Count; i++)
        {
            for (int j = i + 1; j < points.Count; j++)
            {
                long dX = Math.Abs(points[i].X - points[j].X) + 1;
                long dY = Math.Abs(points[i].Y - points[j].Y) + 1;
                
                long dArea = dX * dY;

                if (dArea > maxArea)
                {
                    maxArea = dArea;
                }
            }
        }

        return maxArea;
    }
    
    [Fact]
    public void Part1_TestInput()
    {
        var lines = Utils.ReadLines("test.txt");
        long solution = Part1Solution(lines);
        Assert.Equal(50, solution);
    }

    [Fact]
    public void Part1_RealInput()
    {
        var lines = Utils.ReadLines("input.txt");
        long solution = Part1Solution(lines);
        testOutputHelper.WriteLine(solution.ToString());
    }
}