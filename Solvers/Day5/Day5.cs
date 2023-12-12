using System.Runtime.InteropServices;

namespace AdventOfCode.Day5;

public class Day5Solver
{

    public void FirstPart()
    {
        string[] input = GetPuzzleInput();

        Parser p = new Parser(input);
        p.Parse();

        long lowestLocation = long.MaxValue;
        foreach (var seed in p.InitialSeeds)
        {
            long location = p.Maps.FindLocationForSeed(seed);
            if (lowestLocation > location)
            {
                lowestLocation = location;
            }
        }

        Console.WriteLine("Lowest location is " + lowestLocation);
    }

    public void SecondPart()
    {
        string[] input = GetPuzzleInput();

        Parser p = new Parser(input);
        p.Parse();

        long lowestLocation = long.MaxValue;
        foreach (long[] seedRange in p.InitialSeedRanges)
        {
            long seedStart = seedRange[0];
            long seedEnd = seedRange[0] + seedRange[1];

            for (long seed = seedRange[0]; seed < seedEnd; seed++)
            {
                long location = p.Maps.FindLocationForSeed(seed);
                if (lowestLocation > location)
                {
                    lowestLocation = location;
                }
            }
        }

        Console.WriteLine("Lowest location is " + lowestLocation);
    }



    private string[] GetPuzzleInput()
    {
        string[] input = File.ReadAllLines(".\\..\\..\\..\\Inputs\\day5.txt");
        return input;
    }
}
