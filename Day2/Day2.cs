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


    private long Part2Solution(string[] lines)
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
                    int digitCount = Utils.CountDigits(i);
                    int maxRepeatablePair = digitCount / 2;

                    for (long j = 1; j <= maxRepeatablePair; j++)
                    {
                        int divisor = (int)Math.Pow(10, j);
                        int digitCollection = (int)(i % divisor);
                        if (DigitCollectionsAreTheSame(i, digitCollection))
                        {
                            total += i;
                            break;
                        }
                    }
                }
            }
        }

        return total;
    }
    
    private bool DigitCollectionsAreTheSame(long number, int digitCollection)
    {
        int digitCollectionCount = Utils.CountDigits(digitCollection);
        int divisor = (int)Math.Pow(10, digitCollectionCount);

        int firstN = (int)(number % divisor);

        while (number > 0)
        {
            if (number % divisor != firstN)
                return false;
            
            number /= divisor;
        }

        return true;
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