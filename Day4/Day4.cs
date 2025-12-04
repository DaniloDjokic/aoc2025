using System.Numerics;
using Common;
using Xunit;
using Xunit.Abstractions;

namespace Day4;
public class Day4(ITestOutputHelper testOutputHelper)
{
    private int Part1Solution(Utils.Point[,] grid)
    {
        int total = 0;

        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                if (grid[i, j].Value == '@' && CanAccess(grid, i, j))
                {
                    total++;
                }
            }
        }

        return total;
    }

    private bool CanAccess(Utils.Point[,] grid, int row, int col)
    {
        var point = grid[row, col];
        
        var adjacent = Utils.GetAllAdjacent(grid, point);
        if (adjacent.Count(x => x.Value == '@') <= 3)
        {
            return true;
        }

        return false;
    }

    
    [Fact]
    public void Part1_TestInput()
    {
        var grid = Utils.ReadGrid("test.txt");
        var result = Part1Solution(grid);
        Assert.Equal(13, result);
    }

    [Fact]
    public void Part1_RealInput()
    {
        var grid = Utils.ReadGrid("input.txt");
        var result = Part1Solution(grid);
        testOutputHelper.WriteLine(result.ToString());
    }


    private int Part2Solution(string[] lines)
    {
        return 0;
    }
    
    [Fact]
    public void Part2_TestInput()
    {

    }

    [Fact]
    public void Part2_RealInput()
    {

    }
}
