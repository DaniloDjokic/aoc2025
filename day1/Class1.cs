using Common;
using Xunit;

namespace day1;

public class Day1
{
    private int RunSolution(string[] lines)
    {
        int password = 0;
        int dial = 50;
        
        foreach (var line in lines)
        {
            var dir = line[0];
            var val = int.Parse(line[1..]);
            
            if (dir == 'R')
            {
                dial += val;
                dial %= 100;
            }
            else if (dir == 'L')
            {
                dial += (100 - val);
                dial %= 100;
            }

            if (dial == 0)
            {
                password++;
            }
        }
        
        return password;
    }
    
    [Fact]
    public void RunSolution_TestInput()
    {
        var lines = Utils.ReadLines("test.txt");
        int password = RunSolution(lines);
        Assert.Equal(3, password);
    }

    [Fact]
    public void RunSolution_RealInput()
    {
        var lines = Utils.ReadLines("input.txt");
        int password = RunSolution(lines);
        Console.WriteLine(password);
    }
}