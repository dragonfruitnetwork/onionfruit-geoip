// OnionFruit API/Tooling Copyright DragonFruit Network
// Licensed under the MIT License. Please refer to the LICENSE file at the root of this project for details

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DragonFruit.Data.Serializers.Newtonsoft;
using DragonFruit.OnionFruit.Api.Enums;
using DragonFruit.OnionFruit.Api.Objects;
using Newtonsoft.Json;

namespace DragonFruit.OnionFruit.Data.Components
{
    internal class WebsiteCountryDatabaseGenerator : IDatabaseGenerator
    {
        public string Name => "Website listing";
        
        public void Write(string dir, IReadOnlyCollection<IGrouping<string, TorRelayDetails>> countries)
        {
            var metrics = new LinkedList<CountryInfo>();
            var globalNodeCount = countries.Sum(x => x.Count());

            foreach (var country in countries)
            {
                var allNodes = country.ToArray();
                var countryInfo = new CountryInfo(country.Key);

                foreach (var nodeFlag in country.SelectMany(x => x.RawFlags))
                {
                    switch (nodeFlag)
                    {
                        case nameof(TorNodeFlags.Exit):
                            countryInfo.ExitNodeCount++;
                            break;

                        case nameof(TorNodeFlags.Fast):
                            countryInfo.FastNodeCount++;
                            break;

                        case nameof(TorNodeFlags.Guard):
                            countryInfo.EntryNodeCount++;
                            break;

                        case nameof(TorNodeFlags.Running):
                            countryInfo.OnlineNodeCount++;
                            break;
                            
                        case nameof(TorNodeFlags.Stable):
                            countryInfo.StableNodeCount++;
                            break;
                    }
                }

                countryInfo.TotalNodeCount = allNodes.Length;
                countryInfo.GlobalShare = (decimal)allNodes.Length / globalNodeCount;
                
                metrics.AddLast(countryInfo);
            }

            FileServices.WriteFile(Path.Combine(dir, "country-details.json"), metrics);
        }
    }

    [Serializable]
    internal class CountryInfo
    {
        public CountryInfo(string countryCode)
        {
            CountryCode = countryCode;
        }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("node_count")]
        public int TotalNodeCount { get; set; }

        [JsonProperty("node_count_exit")]
        public int ExitNodeCount { get; set; }

        [JsonProperty("node_count_entry")]
        public int EntryNodeCount { get; set; }

        [JsonProperty("node_count_online")]
        public int OnlineNodeCount { get; set; }
        
        [JsonProperty("node_count_stable")]
        public int StableNodeCount { get; set; }
        
        [JsonProperty("node_count_fast")]
        public int FastNodeCount { get; set; }
        
        [JsonProperty("node_share")]
        public decimal GlobalShare { get; set; }
    }
}