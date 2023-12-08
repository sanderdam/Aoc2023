namespace AdventOfCode.Day3;

public class Parser
{
    private readonly string[] _lines;

    public IList<Symbol> Symbols = new List<Symbol>();
    public IList<Number> Numbers = new List<Number>();

    public Parser(string[] lines)
    {
        _lines = lines;
    }


    public void Parse()
    {
        for (int lineNo = 0; lineNo < _lines.Length; lineNo++)
        {
            string line = _lines[lineNo];
            string tempStr = "";
            for (int positionNo = 0; positionNo < line.Length; positionNo++)
            {
                char currentChar = line[positionNo];

                if (char.IsDigit(currentChar))
                {
                    tempStr += currentChar;

                    if (positionNo == line.Length -1) // this line has ended and so will this number
                    {
                        int endIndex = positionNo - 1; // the previous position
                        int startIndex = endIndex - (tempStr.Length - 1);
                        Numbers.Add(new Number(lineNo + 1, startIndex, endIndex, int.Parse(tempStr)));

                        tempStr = ""; // reset the tempStr to start a new part
                    }
                    continue;
                }

                if (!char.IsDigit(currentChar) && tempStr != "")
                {
                    // A part has completed
                    int endIndex = positionNo - 1; // the previous position
                    int startIndex = endIndex - (tempStr.Length - 1);
                    Numbers.Add(new Number(lineNo + 1, startIndex, endIndex, int.Parse(tempStr)));

                    tempStr = ""; // reset the tempStr to start a new part
                }

                if (!char.IsDigit(currentChar) && currentChar != '.')
                {
                    // it must be a symbol
                    Symbols.Add(new Symbol(lineNo + 1, positionNo, currentChar));
                }
            }
        }

        ;
    }
}

public record Symbol(int Line, int Position, char Character);

public record Number(int Line, int StartIndex, int EndIndex, int No);
