using Common;
using Xunit;
using Xunit.Abstractions;

namespace Day3;

public class Day3(ITestOutputHelper testOutputHelper)
{
    private int Part1Solution(string[] lines)
    {
        var total = 0;
        
        foreach (var line in lines)
        {
            var max = 0;
            
            for (int i = 0; i < line.Length - 1; i++)
            {
                var ch =  line[i];

                for (int j = i + 1; j < line.Length; j++)
                {
                    var ch2 =  line[j];

                    var comp = ch.ToString() + ch2.ToString();
                    
                    var compInt = Convert.ToInt32(comp);

                    if (compInt > max)
                    {
                        max = compInt;
                    }
                }
            }
            
            total += max;
        }

        return total;
    }

    
    [Fact]
    public void Part1_TestInput()
    {
        var lines = Utils.ReadLines("test.txt");
        long solution = Part1Solution(lines);
        Assert.Equal(357, solution);
    }

    [Fact]
    public void Part1_RealInput()
    {
        var lines = Utils.ReadLines("input.txt");
        long solution = Part1Solution(lines);
        testOutputHelper.WriteLine(solution.ToString());
    }


    private long Part2Solution(string[] lines)
    {
        return 0;
    }
    
    [Fact]
    public void Part2_TestInput()
    {
        var lines = Utils.ReadLines("test.txt");
        long solution = Part2Solution(lines);
        Assert.Equal(4174379265, solution);
    }

    [Fact]
    public void Part2_RealInput()
    {
        var lines = Utils.ReadLines("input.txt");
        long solution = Part2Solution(lines);
        testOutputHelper.WriteLine(solution.ToString());
    }
}