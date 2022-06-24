// OnionFruit API/Tooling Copyright DragonFruit Network
// Licensed under the MIT License. Please refer to the LICENSE file at the root of this project for details

using System.Collections.Generic;
using System.IO;
using System.Linq;
using DragonFruit.Data.Serializers.Newtonsoft;
using DragonFruit.OnionFruit.Api.Enums;
using DragonFruit.OnionFruit.Api.Objects;

namespace DragonFruit.OnionFruit.Data.Components
{
    internal class ClientCountryDatabaseGenerator : IDatabaseGenerator
    {
        public string Name => "Client Database";
        
        public void Write(string dir, IReadOnlyCollection<IGrouping<string, TorRelayDetails>> countries)
        {
            // onionfruit clients accept a dictionary of key -> countrycode[]
            var clientData = new Dictionary<string, IEnumerable<string>>
            {
                ["in"] = CountriesWithFlag(countries, TorNodeFlags.Guard),
                ["out"] = CountriesWithFlag(countries , TorNodeFlags.Exit)
            };

            FileServices.WriteFile(Path.Combine(dir, "countries.json"), clientData);
        }

        private static IEnumerable<string> CountriesWithFlag(IEnumerable<IGrouping<string, TorRelayDetails>> info, TorNodeFlags flag)
        {
            return info.Where(x => x.Any(y => y.Flags.HasFlag(flag))).Select(x => x.Key);
        }
    }
}
