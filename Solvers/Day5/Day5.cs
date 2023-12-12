using System.Runtime.InteropServices;

namespace AdventOfCode.Day5;

public class Day5Solver
{

    public void FirstPart()
    {
        string[] input = GetPuzzleInput();

        Parser p = new Parser(input);

        p.Parse();

        Console.WriteLine("Total points ");
    }

    public void SecondPart()
    {
        string[] input = GetPuzzleInput();

        Console.WriteLine("Total points ");
    }



    private string[] GetPuzzleInput()
    {
        string[] input = File.ReadAllLines(".\\..\\..\\..\\Inputs\\day5.txt");
        return input;
    }
}
