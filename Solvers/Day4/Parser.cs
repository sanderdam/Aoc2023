namespace AdventOfCode.Day4;

public class Parser
{
    private readonly string[] lines;

    public Parser(string[] lines)
    {
        this.lines = lines;
    }

    public List<Card> Parse()
    {
        List<Card> cards = new List<Card>();

        foreach (var line in lines)
        {
            string[] cardParts = line.Split(':');
            string cardIdentifierPart = cardParts[0];
            string cardNumbersPart = cardParts[1];

            int cardNumber = GetCardNumber(cardIdentifierPart);
            (int[] winningNumbers, int[] numbersyouHave) = ParseNumbers(cardNumbersPart.Trim());

            cards.Add(new Card(cardNumber, winningNumbers, numbersyouHave));
        }

        return cards;
    }

    private int GetCardNumber(string cardIdentifierPart)
    {
        string intInStr = cardIdentifierPart.Substring(4).Trim();
        if (false == int.TryParse(intInStr, out int cardNumber))
        {
            throw new ApplicationException("Error during parsing card identifier");
        }

        return cardNumber;
    }

    private (int[] winningNumbers, int[] numbersyouHave) ParseNumbers(string numbers)
    {
        string[] numberParts = numbers.Split('|');
        int[] winningNumbers = numberParts[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
        int[] numbersYouHave = numberParts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();

        return (winningNumbers, numbersYouHave);
    }
}

public record Card(int CardNumber, int[] WinningNumbers, int[] NumbersYouHave)
{
    public int Points
    {
        get
        {
            var matchingNumbers = NumbersYouHave.Where(x => WinningNumbers.Contains(x));
            
            if(matchingNumbers.Count() == 0) 
                return 0;

            int points = 1;
            for (int i = 1; i < matchingNumbers.Count(); i++)
            {
                points = points * 2;
            }
            return points;
        }
    }
}

