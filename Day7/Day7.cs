using System.Diagnostics.CodeAnalysis;
using Common;
using Xunit;
using Xunit.Abstractions;

namespace Day7;

public class Day7(ITestOutputHelper testOutputHelper)
{
    class Splitter(Utils.Point point) : IComparable<Splitter>, IEquatable<Splitter>
    {
        public Utils.Point Point { get; } = point;
        
        public int CompareTo(Splitter? other)
        {
            if (other != null) return this.Point.Y.CompareTo(other.Point.Y);
            return -1;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as  Splitter);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Point.X, Point.Y);
        }

        public bool Equals(Splitter? other)
        {
            return other != null && this.Point.X == other.Point.X && this.Point.Y == other.Point.Y;
        }
    }
    
    private long Part1Solution(Utils.Point[,] grid)
    {
        var splitters = grid.FilterGrid(x => x.Value == '^').Select(x => new Splitter(x)).ToList();
        var start = grid.GetSpecificPoint(x => x.Value == 'S');
        
        return ProcessBeam(start, splitters, grid.GetLength(0));
    }

    private int ProcessBeam(Utils.Point start, List<Splitter> splitters, int gridHeight)
    {
        int totalBeamCount = 0;

        List<Utils.Point> processed = [];
        
        Queue<Utils.Point> beamStarts = [];
        beamStarts.Enqueue(start);

        while (beamStarts.Count > 0)
        {
            var beamStart = beamStarts.Dequeue();
            var nextSplitter = GetNextSplitter(beamStart, splitters);

            bool alreadyProcessed = false;

            var xEnd = nextSplitter is not null ? nextSplitter.Point.X : gridHeight;
            for (int i = beamStart.X; i <= xEnd; i++)
            {
                if (processed.Contains(new Utils.Point(i, beamStart.Y, '^'))) alreadyProcessed = true;
            }
            
            if (alreadyProcessed)
                continue;
            
            if (nextSplitter is not null)
            {
                beamStarts.Enqueue(new Utils.Point(nextSplitter.Point.X, nextSplitter.Point.Y + 1, '^'));
                beamStarts.Enqueue(new Utils.Point(nextSplitter.Point.X, nextSplitter.Point.Y - 1, '^'));

                totalBeamCount++;
                
                for (int i = beamStart.X; i < nextSplitter.Point.X; i++)
                {
                    processed.Add(new Utils.Point(i, beamStart.Y, '^'));
                }
            }
            else
            {
                for (int i = beamStart.X; i < gridHeight; i++)
                {
                    processed.Add(new Utils.Point(i, beamStart.Y, '^'));
                }
            }
        }
        
        return totalBeamCount;
    }

    private Splitter? GetNextSplitter(Utils.Point from, List<Splitter> splitters)
    {
        return splitters.Where(x => x.Point.Y == from.Y && x.Point.X > from.X).Min();
    }
    
    [Fact]
    public void Part1_TestInput()
    {
        var points = Utils.ReadGrid("test.txt");
        var result = Part1Solution(points);
        Assert.Equal(21, result);
    }

    [Fact]
    public void Part1_RealInput()
    {
        var points = Utils.ReadGrid("input.txt");
        var result = Part1Solution(points);
        testOutputHelper.WriteLine(result.ToString());
    }

    private long Part2Solution(Utils.Point[,] grid)
    {
        var splitters = grid.FilterGrid(x => x.Value == '^').Select(x => new Splitter(x)).ToList();
        var start = grid.GetSpecificPoint(x => x.Value == 'S');

        return ProcessBeam2(start, splitters, []);
    }

    private long ProcessBeam2(Utils.Point start, List<Splitter> splitters, Dictionary<Splitter, long> memo)
    {
        long totalTimelines = 0;
        
        var nextSplitter = GetNextSplitter(start, splitters);

        if (nextSplitter is not null)
        {
            if (memo.TryGetValue(nextSplitter, out var value))
            {
                totalTimelines += value;
            }
            else
            {
                var left = new Utils.Point(nextSplitter.Point.X, nextSplitter.Point.Y - 1, '^');
                var right = new Utils.Point(nextSplitter.Point.X, nextSplitter.Point.Y + 1, '^');
                
                totalTimelines += ProcessBeam2(left, splitters, memo);
                totalTimelines += ProcessBeam2(right, splitters, memo);
                
                memo[nextSplitter] = totalTimelines;
            }
        }
        else
        {
            totalTimelines = 1;
        }

        return totalTimelines;
    }
    
    [Fact]
    public void Part2_TestInput()
    {
        var points = Utils.ReadGrid("test.txt");
        var result = Part2Solution(points);
        Assert.Equal(40, result);
    }

    [Fact]
    public void Part2_RealInput()
    {
        var points = Utils.ReadGrid("input.txt");
        var result = Part2Solution(points);
        testOutputHelper.WriteLine(result.ToString());
    }
}