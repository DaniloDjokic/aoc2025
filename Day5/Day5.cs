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


    private long Part2Solution(string[] lines)
    {
        List<Utils.Range> ranges = [];

        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                break;
            }
            
            var rangeVals = line.Split('-');
            ranges.Add(new Utils.Range(long.Parse(rangeVals[0]), long.Parse(rangeVals[1])));
        }

        List<Utils.Range> newRanges = ranges;

        do
        {
            ranges = newRanges;
            newRanges = [];
            
            for (int i = 0; i < ranges.Count; i++)
            {
                var currRange = ranges[i];

                for (int j = i + 1; j < ranges.Count; j++)
                {
                    var cmpRange = ranges[j];

                    var overlapResult = currRange.OvarlapsWith(cmpRange);

                    if (overlapResult is not null)
                    {
                        currRange = overlapResult;
                        ranges.Remove(cmpRange);
                    }
                }

                newRanges.Add(currRange);
            }
        } while (!RangeCollectionsAreTheSame(ranges, newRanges));

        long total = 0;

        foreach (var range in newRanges)
        {
            total += range.End - range.Start + 1;
        }


        return total;
    }

    private bool RangeCollectionsAreTheSame(List<Utils.Range> first, List<Utils.Range> second)
    {
        if (first.Count != second.Count)
            return false;

        for (int i = 0; i < first.Count; i++)
        {
            if (first[i].Start != second[i].Start || first[i].End != second[i].End)
            {
                return false;
            }   
        }

        return true;
    }
    
    [Fact]
    public void Part2_TestInput()
    {
        var lines = Utils.ReadLines("test.txt");
        var result = Part2Solution(lines);
        Assert.Equal(14, result);
    }

    [Fact]
    public void Part2_RealInput()
    {
        var lines = Utils.ReadLines("input.txt");
        var result = Part2Solution(lines);
        testOutputHelper.WriteLine(result.ToString());
    }

    [Fact]
    public void ComparisonWorks()
    {
        var rng1 = new Utils.Range(10, 15);
        var rng2 =  new Utils.Range(12, 20);
        var cmp = rng1.OvarlapsWith(rng2);
        Assert.Equal(cmp!.Start, rng1.Start);
        Assert.Equal(cmp!.End, rng2.End);
        
        rng1 = new Utils.Range(10, 15);
        rng2 = new Utils.Range(7, 12);
        cmp = rng1.OvarlapsWith(rng2);
        Assert.Equal(cmp!.Start, rng2.Start);
        Assert.Equal(cmp!.End, rng1.End);
        
        rng1 = new Utils.Range(10, 15);
        rng2 = new Utils.Range(10, 20);
        cmp = rng1.OvarlapsWith(rng2);
        Assert.Equal(cmp!.Start, rng1.Start);
        Assert.Equal(cmp!.End, rng2.End);
        
        rng1 = new Utils.Range(10, 15);
        rng2 = new Utils.Range(15, 20);
        cmp = rng1.OvarlapsWith(rng2);
        Assert.Equal(cmp!.Start, rng1.Start);
        Assert.Equal(cmp!.End, rng2.End);
        
        rng1 = new Utils.Range(10, 15);
        rng2 = new Utils.Range(7, 20);
        cmp = rng1.OvarlapsWith(rng2);
        Assert.Equal(cmp!.Start, rng2.Start);
        Assert.Equal(cmp!.End, rng2.End);
        
        rng1 = new Utils.Range(10, 15);
        rng2 = new Utils.Range(12, 14);
        cmp = rng1.OvarlapsWith(rng2);
        Assert.Equal(cmp!.Start, rng1.Start);
        Assert.Equal(cmp!.End, rng1.End);
        
        rng1 = new Utils.Range(10, 15);
        rng2 = new Utils.Range(15, 20);
        cmp = rng1.OvarlapsWith(rng2);
        Assert.Equal(cmp!.Start, rng1.Start);
        Assert.Equal(cmp!.End, rng2.End);
        
        rng1 = new Utils.Range(10, 15);
        rng2 = new Utils.Range(7,10);
        cmp = rng1.OvarlapsWith(rng2);
        Assert.Equal(cmp!.Start, rng2.Start);
        Assert.Equal(cmp!.End, rng1.End);
    }
}
