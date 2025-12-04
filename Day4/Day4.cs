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


    private int Part2Solution(Utils.Point[,] grid)
    {
        int total = 0;
        
        var removable = ProcessGrid(grid);

        while (removable.Count != 0)
        {
            total += removable.Count;
            var newGrid = ClearRemovables(grid, removable);
            removable = ProcessGrid(newGrid);
        }

        return total;
    }

    private List<(int,int)> ProcessGrid(Utils.Point[,] grid)
    {
        List<(int, int)> removable = [];
    
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                if (grid[i, j].Value == '@' && CanAccess(grid, i, j))
                {
                    removable.Add((i, j));
                }
            }
        }

        return removable;
    }

    private Utils.Point[,] ClearRemovables(Utils.Point[,] grid, List<(int, int)> removables)
    {
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                var comp = (grid[i, j].X, grid[i, j].Y);

                if (removables.Contains(comp))
                {
                    grid[i, j].Value = '.';
                }
            }
        }

        return grid;
    }
    
    [Fact]
    public void Part2_TestInput()
    {
        var grid = Utils.ReadGrid("test.txt");
        var result = Part2Solution(grid);
        Assert.Equal(43, result);
    }

    [Fact]
    public void Part2_RealInput()
    {
        var grid = Utils.ReadGrid("input.txt");
        var result = Part2Solution(grid);
        testOutputHelper.WriteLine(result.ToString());
    }
}
