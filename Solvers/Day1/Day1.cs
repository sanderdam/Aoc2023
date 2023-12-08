namespace AdventOfCode.Day1;

public class Day1Solver
{
    public void FirstPart()
    {
        string[] input = GetPuzzleInput();

        int total = 0;
        foreach (var line in input)
        {
            char firstDigit = line.First(x => char.IsDigit(x));
            char secondDigit = line.Reverse().First(x => char.IsDigit(x));
            char[] digitCombined = { firstDigit, secondDigit };
            string twoDigitNumber = new string(digitCombined);

            int number = int.Parse(twoDigitNumber);
            total += number;
        }

        Console.WriteLine("Total is " + total);
    }

    public void SecondPart()
    {
        string[] input = GetPuzzleInput();

        int total = 0;
        foreach (string line in input)
        {
            string lineReplaced = ReplaceNumberTextInLine(line);
            char firstDigit = lineReplaced.First(x => char.IsDigit(x));
            char secondDigit = lineReplaced.Reverse().First(x => char.IsDigit(x));
            char[] digitCombined = { firstDigit, secondDigit };
            string twoDigitNumber = new string(digitCombined);

            int number = int.Parse(twoDigitNumber);
            total += number;
        }

        Console.WriteLine("Total is " + total);
    }

    private IDictionary<string,string> TextToNumbersMap = new Dictionary<string,string>(){
            { "one","o1e"},
            { "two","t2o"},
            { "three","t3ree"},
            { "four","f4ur"},
            { "five","f5ve"},
            { "six","s6x"},
            { "seven","s7ven"},
            { "eight","e8ght"},
            { "nine","n9ne"},            
            { "zero","z0ro"}
    };

    private string ReplaceNumberTextInLine(string line) {
        
        foreach( var map in TextToNumbersMap){
            line = line.Replace(map.Key, map.Value);
        }
        
        return line;
    }

    private string[] GetPuzzleInput()
    {
        string[] input = File.ReadAllLines(".\\..\\..\\..\\Inputs\\day1.txt");
        return input;
    }
}