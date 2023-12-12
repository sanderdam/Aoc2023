using System.Runtime.InteropServices;

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

    public void SecondPart()
    {
        string[] input = GetPuzzleInput();

        Parser p = new Parser(input);
        var originalCards = p.Parse();

        List<Card> copies = new List<Card>();

        for (int currentOriginalCardIndex = 0; currentOriginalCardIndex < originalCards.Count; currentOriginalCardIndex++)
        {
            var currentOriginalCard = originalCards[currentOriginalCardIndex];
            int numberOfCopiesOfThisCard = copies.Count(x => x.CardNumber == currentOriginalCard.CardNumber);
            int startIndexForCopyingCards = currentOriginalCardIndex + 1;

            for (int cardIndexToCopy = startIndexForCopyingCards; cardIndexToCopy < (startIndexForCopyingCards + currentOriginalCard.NumberOfMatches); cardIndexToCopy++)
            {
                var cardToCopy = originalCards[cardIndexToCopy];
                copies.Add(cardToCopy);
                for (int copyToCreate = 0; copyToCreate < numberOfCopiesOfThisCard; copyToCreate++)
                {
                    copies.Add(cardToCopy);
                }                
            }
        }

        Console.WriteLine("Total points " + (originalCards.Count + copies.Count));
    }



    private string[] GetPuzzleInput()
    {
        string[] input = File.ReadAllLines(".\\..\\..\\..\\Inputs\\day4.txt");
        return input;
    }
}
