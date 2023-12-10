namespace AdventOfCode.Day4;

public class Day4Solver
{

    public void FirstPart()
    {
        string[] input = GetPuzzleInput();

        Parser p = new Parser(input);
        var cards = p.Parse();

        var totalPoints = cards.Sum(x => x.Points);

        Console.WriteLine("Total points " + totalPoints);
    }



    private string[] GetPuzzleInput()
    {
        string[] input = File.ReadAllLines(".\\..\\..\\..\\Inputs\\day4.txt");
        return input;
    }
}
