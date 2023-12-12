namespace AdventOfCode.Day5;

public class Parser
{
    private readonly string[] lines;

    public Parser(string[] lines)
    {
        this.lines = lines;
    }

    public Maps Maps { get; set; }
    public string[] InitialSeeds { get; set; }

    public void Parse()
    {
        Maps = new Maps();
        InitialSeeds = ParseInitialSeeds(lines[0]);

        string currentSection = string.Empty;
        IDictionary<long,long> currentMap = new Dictionary<long,long>();

        for (int lineNo = 2; lineNo < lines.Length; lineNo++)
        {
            var currentLine = lines[lineNo];

            if (string.IsNullOrWhiteSpace(currentLine))
                continue;

            if (!char.IsDigit(currentLine[0]))
            {
                // save current section
                SaveCurrentMap(currentSection, currentMap);

                // new section
                currentMap = new Dictionary<long,long>();
                currentSection = currentLine.Replace(" map:", "");
                continue;
            }

            var splittedValues = currentLine.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            long destinationRangeStart = long.Parse(splittedValues[0]);
            long sourceRangeStart = long.Parse(splittedValues[1]);
            long rangeLength = long.Parse(splittedValues[2]);

            for (int i = 0; i < rangeLength; i++)
            {
                currentMap.Add(sourceRangeStart + i, destinationRangeStart + i);
            }
        }
    }

    private void SaveCurrentMap(string section, IDictionary<long,long> mapToSave)
    {
        switch (section)
        {
            case "seed-to-soil":
                Maps.SeedToSoil = mapToSave;
                return;
            case "soil-to-fertilizer":
                Maps.SoilToFertilizer = mapToSave;
                return;
            case "fertilizer-to-water":
                Maps.FertilizerToWater = mapToSave;
                return;
            case "water-to-light":
                Maps.WaterToLight = mapToSave;
                return;
            case "light-to-temperature":
                Maps.LightToTemperature = mapToSave;
                return;
            case "temperature-to-humidity":
                Maps.TemperatureToHumidity = mapToSave;
                return;
            case "humidity-to-location":
                Maps.HumidityToLocation = mapToSave;
                return;
        }
    }

    private string[] ParseInitialSeeds(string firstLine) => firstLine.Substring(6).Split(' ', StringSplitOptions.RemoveEmptyEntries).ToArray();
}

public class Maps
{
    public IDictionary<long,long> SeedToSoil { get; set; }
    public IDictionary<long,long> SoilToFertilizer { get; set; }
    public IDictionary<long,long> FertilizerToWater { get; set; }

    public IDictionary<long,long> WaterToLight { get; set; }

    public IDictionary<long,long> LightToTemperature { get; set; }

    public IDictionary<long,long> TemperatureToHumidity { get; set; }

    public IDictionary<long,long> HumidityToLocation { get; set; }
}

