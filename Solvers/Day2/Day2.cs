using System.Security.Cryptography.X509Certificates;
using AdventOfCode;

namespace AdventOfCode.Day2;

public class Day2Solver
{
    public void FirstPart()
    {
        string[] lines = GetPuzzleInput();
        var games = Parser.ParseLinesToGames(lines);

        int total =0;

        foreach(var game in games){
            // is this game possible? 12 red cubes, 13 green cubes and 14 blue cubes
            bool gamePossible = true;

            foreach(var draw in game.Draws){
                if(draw.NumberOfReds > 12){
                    gamePossible = false;
                    break;
                }
                if(draw.NumberOfGreens > 13){
                    gamePossible = false;
                    break;
                }
                if(draw.NumberOfBlues > 14){
                    gamePossible = false;
                    break;
                }
            }

            if(gamePossible){
                total += game.Id;
            }
        }

        Console.WriteLine("Total is " + total);
    }

    public void SecondPart()
    {
        string[] lines = GetPuzzleInput();
        var games = Parser.ParseLinesToGames(lines);

        int total = 0;

        foreach(var game in games){
            int maxRed = game.Draws.Max(x => x.NumberOfReds);
            int maxGreen = game.Draws.Max(x => x.NumberOfGreens);
            int maxBlue = game.Draws.Max(x => x.NumberOfBlues);

            int power = maxRed * maxBlue * maxGreen;
            total += power;
        }

        Console.WriteLine("Total is " + total);
    }

    private string[] GetPuzzleInput()
    {
        string[] input = File.ReadAllLines(".\\..\\..\\..\\Inputs\\day2.txt");
        return input;
    }
}