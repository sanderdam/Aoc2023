using System.Reflection.Metadata;

namespace AdventOfCode.Day2;

public class Parser
{
    public static IList<Game> ParseLinesToGames(string[] lines)
    {
        IList<Game> games = new List<Game>();
        foreach (var line in lines)
        {
            Game game = new Game();
            games.Add(game);

            int indexOfFirstSemicolon = line.IndexOf(':');
            string gameIdentifierPart = line.Substring(0, indexOfFirstSemicolon);
            game.Id = ParseGameId(gameIdentifierPart);

            game.Draws = ParseDraws(line.Substring(indexOfFirstSemicolon + 1).Trim());
        }

        return games;
    }

    private static IList<Draw> ParseDraws(string line)
    {
        string[] drawsArr = line.Split(';');
        IList<Draw> draws = new List<Draw>();

        foreach (string strDraw in drawsArr)
        {
            var draw = ParseDraw(strDraw);
            draws.Add(draw);
        }

        return draws;
    }

    private static Draw ParseDraw(string strDraw)
    {
        Draw draw = new Draw();
        string[] cubes = strDraw.Split(',');
        foreach (string strCube in cubes)
        {
            Cube cube = new Cube();
            string[] cubeParts = strCube.Trim().Split(' ');

            if (!int.TryParse(cubeParts[0], out int numberOfCubes))
            {
                throw new ApplicationException("Cannot parse draw");
            }
            cube.NumberOfCubes = numberOfCubes;

            cube.CubeColor = ParseCubeColor(cubeParts[1].Trim());

            switch (cube.CubeColor)
            {
                case CubeColors.Red:
                    draw.NumberOfReds += cube.NumberOfCubes;
                    break;
                case CubeColors.Green:
                    draw.NumberOfGreens += cube.NumberOfCubes;
                    break;
                case CubeColors.Blue:
                    draw.NumberOfBlues += cube.NumberOfCubes;
                    break;
            }

            draw.Cubes.Add(cube);
        }

        return draw;
    }

    private static int ParseGameId(string line)
    {
        int indexOfSpace = line.IndexOf(' ');
        string gameIdentifierInStr = line.Substring(indexOfSpace).Trim();

        if (!int.TryParse(gameIdentifierInStr, out int gameIdentifier))
        {
            throw new ApplicationException("Error when parsing game identifier");
        }

        return gameIdentifier;
    }

    private static CubeColors ParseCubeColor(string cubeColorInStr)
    {
        switch (cubeColorInStr)
        {
            case "red":
                return CubeColors.Red;
            case "green":
                return CubeColors.Green;
            case "blue":
                return CubeColors.Blue;
            default:
                throw new ApplicationException("Cannot parse cubecolor");
        }
    }
}

public class Game
{
    public int Id { get; set; }
    public IList<Draw> Draws { get; set; } = new List<Draw>();
}

public class Draw
{
    public int NumberOfReds { get; set; }
    public int NumberOfBlues { get; set; }
    public int NumberOfGreens { get; set; }
    public IList<Cube> Cubes { get; set; } = new List<Cube>();
}

public class Cube
{
    public int NumberOfCubes { get; set; }
    public CubeColors CubeColor { get; set; }
}

public enum CubeColors
{
    Red, Green, Blue
}