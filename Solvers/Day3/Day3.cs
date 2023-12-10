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

    public void SecondPart()
    {
        string[] input = GetPuzzleInput();
        Parser p = new Parser(input);

        int total = 0;

        p.Parse();

        // find parts
        List<Number> parts = new List<Number>();
        foreach (var number in p.Numbers)
        {
            var symbolsOnAdjendanctLines = p.Symbols.Where(x => x.Line >= (number.Line - 1) && x.Line <= (number.Line + 1));
            var adjendanctSymbols = symbolsOnAdjendanctLines.Where(s => s.Position >= (number.StartIndex - 1) && s.Position <= (number.EndIndex + 1));
            if (adjendanctSymbols.Count() > 0)
            {
                parts.Add(number);
            }
        }

        var possibleGears = p.Symbols.Where(x => x.Character == '*');
        foreach (var gear in possibleGears)
        {
            var partNumbersOnAdjendanctLines = parts.Where( x=> x.Line >= (gear.Line -1) && x.Line <= (gear.Line +1));
            var adjendactPartNumbers = partNumbersOnAdjendanctLines.Where( x=> gear.Position >= (x.StartIndex -1) && gear.Position <= (x.EndIndex +1));
            if(adjendactPartNumbers.Count() == 2){
                // this a gear
                total += adjendactPartNumbers.Select(x => x.No).Aggregate((a,x) => a * x);
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
