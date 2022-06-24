// OnionFruit API/Tooling Copyright DragonFruit Network
// Licensed under the MIT License. Please refer to the LICENSE file at the root of this project for details

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Bia.Countries.Iso3166;
using DragonFruit.Data;
using DragonFruit.Data.Serializers.Newtonsoft;
using DragonFruit.OnionFruit.Api.Enums;
using DragonFruit.OnionFruit.Api.Extensions;
using DragonFruit.OnionFruit.Api.Objects;
using DragonFruit.OnionFruit.Data.Components;

namespace DragonFruit.OnionFruit.Data
{
    internal static class Program
    {
        private static readonly ApiClient Client = new ApiClient<ApiJsonSerializer>();

        private static async Task Main(string[] args)
        {
            Console.WriteLine("Fetching data...");
            
            var data = await Client.GetTorDetailsAsync(type: TorNodeType.Relay).ConfigureAwait(false);
            var countries = data.Relays.GroupBy(x => x.CountryCode?.ToUpper()).Where(x => Countries.GetCountryByAlpha2(x.Key) is not null).ToArray();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"Downloaded {data.Relays.Length} relay metadata packets over {countries.Length} countries");

            var stopwatch = new Stopwatch();
            var directory = Environment.GetEnvironmentVariable("OUTPUT_DIR") ?? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "onionfruit-data");
            var generators = Assembly.GetExecutingAssembly().GetTypes().Where(x => typeof(IDatabaseGenerator).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract).Select(Activator.CreateInstance).Cast<IDatabaseGenerator>();

            Directory.CreateDirectory(directory);
            foreach (var generator in generators)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"Running generator: {generator.Name}...");
                
                stopwatch.Restart();
                generator.Write(directory, countries);
                stopwatch.Stop();
                
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($" Completed in {stopwatch.Elapsed.TotalMilliseconds} milliseconds");
            }
            
            Console.WriteLine("All generators complete.");
            
        }

        private static IEnumerable<string> GetCountriesWithFlag(this IEnumerable<IGrouping<string, TorRelayDetails>> info, TorNodeFlags flag)
        {
            return info.Where(x => x.Any(y => y.Flags.HasFlag(flag))).Select(x => x.Key);
        }
    }
}
