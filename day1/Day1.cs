using Common;
using Xunit;
using Xunit.Abstractions;

namespace day1;

public class Day1(ITestOutputHelper testOutputHelper)
{
    private int Part1Solution(string[] lines)
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
    public void Part1_TestInput()
    {
        var lines = Utils.ReadLines("test.txt");
        int password = Part1Solution(lines);
        Assert.Equal(3, password);
    }

    [Fact]
    public void Part1_RealInput()
    {
        var lines = Utils.ReadLines("input.txt");
        int password = Part1Solution(lines);
        testOutputHelper.WriteLine(password.ToString());
    }

    private int Part2Solution(string[] lines)
    {
        int password = 0;
        int dial = 50;
        
        foreach (var line in lines)
        {
            var dir = line[0];
            var val = int.Parse(line[1..]);
            int rotations = 0;
            
            if (dir == 'R')
            {
                rotations = val / 100;
                if (rotations > 0)
                {
                    password += rotations;
                }

                val %= 100;
                dial += val;

                if (dial > 100)
                    password++;
                
                dial  %= 100;
            }
            else if (dir == 'L')
            {
                rotations = val / 100;

                if (rotations > 0)
                {
                    password += rotations;
                }
                
                val %= 100;
                
                var oldDial = dial;
                dial -= val;
                
                if (dial < 0 && oldDial != 0)
                    password++;
                
                dial %= 100;
                
                if (dial < 0)
                    dial = 100 + dial;
            }

            if (dial == 0)
            {
                password++;
            }
        }

        return password;
    }
    
    
    [Fact]
    public void Part2_TestInput()
    {
        var lines = Utils.ReadLines("test.txt");
        int password = Part2Solution(lines);
        Assert.Equal(6, password);
    }

    [Fact]
    public void Part2_RealInput()
    {
        var lines = Utils.ReadLines("input.txt");
        int password = Part2Solution(lines);
        testOutputHelper.WriteLine(password.ToString());
    }
}