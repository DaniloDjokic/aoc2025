using Common;
using Xunit;
using Xunit.Abstractions;

namespace Day6;

public class Day6(ITestOutputHelper testOutputHelper)
{
    private enum Operation
    {
        Add,
        Multiply
    };

    private ulong Part1Solution(string[] lines)
     {
         ulong total = 0;
         List<List<(ulong, int)>> allNums = [];
         List<Operation> operations = [];

         int idx = 0;
         int linesCounter = 1;
         
         foreach (var line in lines)
         {
             if (linesCounter == lines.Length)
             {
                 var ops = line.Split(' ').Where(x => !string.IsNullOrWhiteSpace(x));

                 foreach (var op in ops)
                 {
                     var opEnum = op switch
                     {
                         "+" => Operation.Add,
                         "*" => Operation.Multiply,
                         _ => throw new Exception("What?")
                     };
                     
                     operations.Add(opEnum);
                 }
             }
             else
             {
                 var numStrs = line.Split(' ').Where(x => !string.IsNullOrEmpty(x));
                 List<(ulong, int)> nums = [];
             
                 foreach (var numStr in numStrs)
                 {
                     var num = ulong.Parse(numStr);
                     nums.Add((num, idx));
                     idx++;
                 }
             
                 idx = 0;
                 allNums.Add(nums);
                 linesCounter++;
             }
         }

         for (int i = 0; i < allNums[0].Count ; i++)
         {
             List<ulong> numsToProcess = [];
             var op = operations[i];

             foreach (var nums in allNums)
             {
                 var i1 = i;
                 numsToProcess = [..numsToProcess, ..nums.Where(x => x.Item2 == i1).Select(x => x.Item1)];
             }
             
             ulong toAdd = op switch
             {
                 Operation.Add => numsToProcess.Aggregate((acc, x) => acc + x),
                 Operation.Multiply =>  numsToProcess.Aggregate((acc, x) => acc * x),
                 _ => throw new Exception("What?")
             };
             
             total += toAdd;
         }
         
         return total;
     }
    
    [Fact]
    public void Part1_TestInput()
    {
        var lines = Utils.ReadLines("test.txt");
        var result = Part1Solution(lines);
        Assert.Equal((ulong)4277556, result);
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
        var nums = ReadNumbers(lines);
        var ops = 
            lines[^1]
                .Split(" ")
                .Where(x => x != string.Empty)
                .Select(x => x switch
                {
                    "*" => Operation.Multiply,
                    "+" => Operation.Add,
                    _ => throw new Exception("What")
                })
                .ToList();
        
        Assert.Equal(nums.Count, ops.Count);

        long total = nums.Zip(ops, (numsToProcess, op) =>
            {
                return op switch
                {
                    Operation.Add => numsToProcess.Aggregate((acc, x) => acc + x),
                    Operation.Multiply => numsToProcess.Aggregate((acc, x) => acc * x),
                    _ => throw new Exception("What?")
                };
            })
            .Aggregate((acc, x) => acc + x);
        
        return total;
    }

    private List<List<long>> ReadNumbers(string[] lines)
    {
        List<List<long>> nums = [];
        
        var lastLine = lines[^1];

        var indices =
            lastLine
                .Select((el, idx) => new {el, idx})
                .Where(x => x.el is '+' or '*')
                .Select(x => x.idx)
                .ToList();
        
        indices.Add(lastLine.Length + 1);
        
        var numWidths = 
            indices.Zip(indices.Skip(1), (a,b) => b - a - 1).ToList();
        
        var pointer = 0;
        foreach (var numWidth in numWidths)
        {
            var start = pointer;
            var end =  pointer + numWidth;

            List<string> numsInWindowStr = [];
            for (int i = 0; i < lines.Length - 1; i++)
            {
                var line = lines[i];
                numsInWindowStr.Add(line[start..end]);
            }

            List<long> numsInWindow = [];

            for (int i = 0; i < end - start; i++)
            {
                string num = "";
                
                foreach (var numStr in numsInWindowStr)
                {
                    num += numStr[i].ToString();
                }
                
                numsInWindow.Add(int.Parse(num));
            }

            nums.Add(numsInWindow);
            pointer = end + 1;
        }

        return nums;
    }
    
    [Fact]
    public void Part2_TestInput()
    {
        var lines = Utils.ReadLines("test.txt");
        var result = Part2Solution(lines);
        Assert.Equal(3263827, result);
    }

    [Fact]
    public void Part2_RealInput()
    {
        var lines = Utils.ReadLines("input.txt");
        var result = Part2Solution(lines);
        testOutputHelper.WriteLine(result.ToString());
    }
}