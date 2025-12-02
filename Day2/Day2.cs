using Common;
using Xunit;
using Xunit.Abstractions;

namespace Day2;

public class Day2(ITestOutputHelper testOutputHelper)
{
    private long Part1Solution(string[] lines)
    {
        long total = 0;
        
        foreach (var line in lines)
        {
            var ranges = line.Split(',');
            foreach (var range in ranges)
            {
                var rangeElements = range.Split('-');
                var start = long.Parse(rangeElements[0]);
                var end = long.Parse(rangeElements[1]);

                for (long i = start; i <= end; i++)
                {
                    long digitCount = Utils.CountDigits(i);

                    if (digitCount % 2 != 0)
                        continue;
                    
                    long pairSize = digitCount / 2;
                    
                    long divisor = (int)Math.Pow(10, digitCount - pairSize);
                    long modulo = (int)Math.Pow(10, pairSize);

                    var firstN = i / divisor;
                    var secondN = i % divisor;

                    if (firstN == secondN)
                    {
                        total += i;
                    }
                }
            }
        }

        return total;
    }
    
    [Fact]
    public void Part1_TestInput()
    {
        var lines = Utils.ReadLines("test.txt");
        long solution = Part1Solution(lines);
        Assert.Equal(1227775554, solution);
    }

    [Fact]
    public void Part1_RealInput()
    {
        var lines = Utils.ReadLines("input.txt");
        long solution = Part1Solution(lines);
        testOutputHelper.WriteLine(solution.ToString());
    }
}