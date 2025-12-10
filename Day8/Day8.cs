using System.Runtime.ExceptionServices;
using Common;
using Xunit;
using Xunit.Abstractions;

namespace Day8;

public class Day8(ITestOutputHelper testOutputHelper)
{
    record JunctionBox(int X, int Y, int Z)
    {
        public double DistanceTo(JunctionBox other)
        {
            return Math.Sqrt(Math.Pow(X - other.X, 2) + Math.Pow(Y - other.Y, 2) + Math.Pow(Z - other.Z, 2));
        }
    }

    record JunctionDistance(JunctionBox First, JunctionBox Second, double Distance);
    
    private long Part1Solution(string[] lines, int connections)
    {
        List<JunctionBox> boxes = [];

        foreach (var line in lines)
        {
            var vals = line.Split(',');
            boxes.Add(new JunctionBox(int.Parse(vals[0]), int.Parse(vals[1]), int.Parse(vals[2])));
        }

        List<JunctionDistance> distances = [];

        for (int i = 0; i < boxes.Count; i++)
        {
            for (int j = i + 1; j < boxes.Count; j++)
            {
                distances.Add(new JunctionDistance(boxes[i], boxes[j], boxes[i].DistanceTo(boxes[j])));
            }
        }

        distances = distances.OrderBy(x => x.Distance).ToList();

        List<JunctionBox> processed = [];
        foreach (var distance in distances.Take(connections))
        {
            if (!processed.Contains(distance.First))
            {
                processed.Add(distance.First);
            }
        }

        List<List<JunctionBox>> finalCircuits = [];
        foreach (var box in processed)
        {
            var normalized = NormalizeCircuit(box, []);

            bool hasDuplicate = false;
            foreach (var finalCircuit in finalCircuits)
            {
                if (AreCircuitsTheSame(normalized, finalCircuit))
                {
                    hasDuplicate = true;
                    break;
                }
            }

            if (!hasDuplicate)
            {
                finalCircuits.Add(normalized);
            }
        }

        return finalCircuits
            .OrderByDescending(x => x.Count)
            .Select(x => x.Count)
            .Take(3)
            .Aggregate((acc, x) => acc * x);
    }

    private List<JunctionBox> NormalizeCircuit(JunctionBox box, List<JunctionBox> circuit)
    {
        if (!circuit.Contains(box))
        {
            circuit.Add(box);
        }
        
        return circuit;
    }

    private bool AreCircuitsTheSame(List<JunctionBox> first, List<JunctionBox> second)
    {
        var x = first.OrderBy(k => k.X).ThenBy(k => k.Y).ThenBy(k => k.Z).ToList();
        var y = second.OrderBy(k => k.X).ThenBy(k => k.Y).ThenBy(k => k.Z).ToList();
        
        if (x.Count != y.Count)
        {
            return false;
        }

        for (int i = 0; i < x.Count; i++)
        {
            if (x[i].X != y[i].X || x[i].Y != y[i].Y || x[i].Z != y[i].Z)
            {
                return false;
            }
        }

        return true;
    }
    
    [Fact]
    public void Part1_TestInput()
    {
        var lines = Utils.ReadLines("test.txt");
        var result = Part1Solution(lines, 10);
        Assert.Equal(40, result);
    }

    [Fact]
    public void Part1_RealInput()
    {
        var lines = Utils.ReadLines("input.txt");
        var result = Part1Solution(lines, 1000);
        testOutputHelper.WriteLine(result.ToString());
    }
    
    private long Part2Solution(string[] lines)
    {
        List<JunctionBox> boxes = [];

        foreach (var line in lines)
        {
            var vals = line.Split(',');
            var box = new JunctionBox(int.Parse(vals[0]), int.Parse(vals[1]), int.Parse(vals[2]));
            boxes.Add(box);
        }

        List<JunctionDistance> distances = [];

        for (int i = 0; i < boxes.Count; i++)
        {
            for (int j = i + 1; j < boxes.Count; j++)
            {
                distances.Add(new JunctionDistance(boxes[i], boxes[j], boxes[i].DistanceTo(boxes[j])));
            }
        }
        
        distances = distances.OrderBy(x => x.Distance).ToList();

        JunctionBox lastFirst = null;
        JunctionBox lastSecond = null;
        
        List<List<JunctionBox>> finalCircuits = [];
        foreach (var dist in distances)
        {
            var first = dist.First;
            var second = dist.Second;
            
            if (boxes.Contains(first))
                boxes.Remove(first);
            if (boxes.Contains(second))
                boxes.Remove(second);
            
            finalCircuits.Add([first, second]);
            finalCircuits = NormalizeCircuits(finalCircuits);

            if (boxes.Count == 0 && finalCircuits.Count == 1)
            {
                lastFirst = first;
                lastSecond = second;
                break;
            }
        }

        return lastFirst!.X * lastSecond!.X;
    }

    private List<List<JunctionBox>> NormalizeCircuits(List<List<JunctionBox>> circuits)
    {
        List<HashSet<JunctionBox>> groups = [];
        
        foreach (var circuit in circuits)
        {
            var listSet = new HashSet<JunctionBox>(circuit);
            var matchingGroups = 
                groups
                    .Where(g => g.Intersect(listSet).Any())
                    .ToList();

            if (matchingGroups.Any())
            {
                var mergedSet = new HashSet<JunctionBox>(listSet);

                foreach (var group in matchingGroups)
                {
                    mergedSet.UnionWith(group);
                    groups.Remove(group);
                }
                
                groups.Add(mergedSet);
            }
            else
            {
                groups.Add(listSet);
            }
        }
        
        return groups.Select(g => g.ToList()).ToList();
    }
    
    [Fact]
    public void Part2_TestInput()
    {
        var lines = Utils.ReadLines("test.txt");
        var result = Part2Solution(lines);
        Assert.Equal(25272, result);
    }

    [Fact]
    public void Part2_RealInput()
    {
        var lines = Utils.ReadLines("input.txt");
        var result = Part2Solution(lines);
        testOutputHelper.WriteLine(result.ToString());
    }
}