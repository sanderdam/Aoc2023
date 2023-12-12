namespace AdventOfCode.Day5;

public class Parser
{
    private readonly string[] lines;

    public Parser(string[] lines)
    {
        this.lines = lines;
    }

    public Maps Maps { get; set; }
    public long[] InitialSeeds { get; set; }
    public long[][] InitialSeedRanges { get; set; }

    public void Parse()
    {
        Maps = new Maps();
        InitialSeeds = ParseInitialSeeds(lines[0]);
        InitialSeedRanges = ParseInitialSeedRanges(lines[0]);

        string currentSection = string.Empty;
        IList<(long sourceStart, long destinationStart, long range)> currentMap = new List<(long sourceStart, long destinationStart, long range)>();

        for (int lineNo = 2; lineNo < lines.Length; lineNo++)
        {
            var currentLine = lines[lineNo];

            if (string.IsNullOrWhiteSpace(currentLine))
                continue;

            if (!char.IsDigit(currentLine[0]))
            {
                // when no section is selected yet... this happens when line 3 is hitted
                if (!string.IsNullOrWhiteSpace(currentSection) && currentMap.Count > 0)
                {
                    // save current section
                    SaveCurrentMap(currentSection, currentMap);
                }

                // new section
                currentMap = new List<(long sourceStart, long destinationStart, long range)>();
                currentSection = currentLine.Replace(" map:", "");
                continue;
            }

            var splittedValues = currentLine.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            long destinationRangeStart = long.Parse(splittedValues[0]);
            long sourceRangeStart = long.Parse(splittedValues[1]);
            long rangeLength = long.Parse(splittedValues[2]);

            currentMap.Add((sourceRangeStart, destinationRangeStart, rangeLength));
        }

        // Save the last map that was in progress
        SaveCurrentMap(currentSection, currentMap);
    }

    private void SaveCurrentMap(string section, IList<(long sourceStart, long destinationStart, long range)> mapToSave)
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

    private long[] ParseInitialSeeds(string firstLine) => firstLine.Substring(6).Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => long.Parse(x)).ToArray();
    private long[][] ParseInitialSeedRanges(string firstLine)
    {
        var rangeOfSeedsArray =  firstLine.Substring(6).Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => long.Parse(x)).ToArray();
        var inPairs = rangeOfSeedsArray.Select((value,index) => new {value,index}).GroupBy(x => x.index / 2, x => x.value).Select(x => x.ToArray()).ToArray();
        return inPairs;
    }
}

public class Maps
{
    public long FindLocationForSeed(long seed)
    {
        long soil = CalculateDestinationForSourceInGivenMap(seed, SeedToSoil);
        long fertilizer = CalculateDestinationForSourceInGivenMap(soil, SoilToFertilizer);
        long water = CalculateDestinationForSourceInGivenMap(fertilizer, FertilizerToWater);
        long light = CalculateDestinationForSourceInGivenMap(water, WaterToLight);
        long temperature = CalculateDestinationForSourceInGivenMap(light, LightToTemperature);
        long humidity = CalculateDestinationForSourceInGivenMap(temperature, TemperatureToHumidity);
        long location = CalculateDestinationForSourceInGivenMap(humidity, HumidityToLocation);

        return location;
    }

    private long CalculateDestinationForSourceInGivenMap(long source, IList<(long sourceStart, long destinationStart, long range)> map)
    {
        foreach (var mapItem in map)
        {
            // when the source is in the range of the mapItem
            if (source >= mapItem.sourceStart && source < (mapItem.sourceStart + mapItem.range))
            {
                // calculate the destination
                long offsetFromStartIndex = source - mapItem.sourceStart;
                return mapItem.destinationStart + offsetFromStartIndex;
            }
        }

        // when no map is found, the destination is the same as source
        return source;
    }

    public IList<(long sourceStart, long destinationStart, long range)> SeedToSoil { get; set; }
    public IList<(long sourceStart, long destinationStart, long range)> SoilToFertilizer { get; set; }
    public IList<(long sourceStart, long destinationStart, long range)> FertilizerToWater { get; set; }

    public IList<(long sourceStart, long destinationStart, long range)> WaterToLight { get; set; }

    public IList<(long sourceStart, long destinationStart, long range)> LightToTemperature { get; set; }

    public IList<(long sourceStart, long destinationStart, long range)> TemperatureToHumidity { get; set; }

    public IList<(long sourceStart, long destinationStart, long range)> HumidityToLocation { get; set; }
}

