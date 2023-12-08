using System.Security.Cryptography.X509Certificates;

namespace AdventOfCode.Day3;

public class Day3Solver
{

    public void FirstPart()
    {
        string[] input = GetPuzzleInput();
        Parser p = new Parser(input);

        int total = 0;

        p.Parse();

        foreach (var number in p.Numbers)
        {
            var symbolsOnAdjendanctLines = p.Symbols.Where(x => x.Line >= (number.Line - 1) && x.Line <= (number.Line + 1));
            var adjendanctSymbols = symbolsOnAdjendanctLines.Where(s => s.Position >= (number.StartIndex - 1) && s.Position <= (number.EndIndex + 1));
            if (adjendanctSymbols.Count() > 0)
            {
                total += number.No;
            }
            else
            {
                ;
            }
        }

        Console.WriteLine("Total is " + total);
    }


    private string[] GetPuzzleInput()
    {
        string[] input = File.ReadAllLines(".\\..\\..\\..\\Inputs\\day3.txt");
        return input;
    }
}
