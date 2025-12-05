using Common;
using Xunit;
using Xunit.Abstractions;

namespace Day5;
public class Day5(ITestOutputHelper testOutputHelper)
{
    private int Part1Solution(string[] lines)
    {
        var total = 0;
        var processIds = false;
        List<Utils.Range> ranges = [];

        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                processIds = true;
                continue;
            }

            if (processIds)
            {
                var productId = Convert.ToInt64(line);

                foreach (var range in ranges)
                {
                    if (range.Start <= productId && range.End >= productId)
                    {
                        total++;
                        break;
                    }
                }
            }
            else
            {
                var rangeVals = line.Split('-');
                ranges.Add(new Utils.Range(long.Parse(rangeVals[0]), long.Parse(rangeVals[1])));
            }
        }

        return total;
    }
    
    [Fact]
    public void Part1_TestInput()
    {
        var lines = Utils.ReadLines("test.txt");
        var result = Part1Solution(lines);
        Assert.Equal(3, result);
    }

    [Fact]
    public void Part1_RealInput()
    {
        var lines = Utils.ReadLines("input.txt");
        var result = Part1Solution(lines);
        testOutputHelper.WriteLine(result.ToString());
    }


    private int Part2Solution(string[] lines)
    {
        return 0;
    }
    
    [Fact]
    public void Part2_TestInput()
    {
        var lines = Utils.ReadLines("test.txt");
        var result = Part2Solution(lines);
        Assert.Equal(43, result);
    }

    [Fact]
    public void Part2_RealInput()
    {
        var lines = Utils.ReadLines("input.txt");
        var result = Part2Solution(lines);
        testOutputHelper.WriteLine(result.ToString());
    }
}
