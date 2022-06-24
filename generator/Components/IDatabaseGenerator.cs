// OnionFruit API/Tooling Copyright DragonFruit Network
// Licensed under the MIT License. Please refer to the LICENSE file at the root of this project for details

using System.Collections.Generic;
using System.Linq;
using DragonFruit.OnionFruit.Api.Objects;

namespace DragonFruit.OnionFruit.Data.Components
{
    internal interface IDatabaseGenerator
    {
        string Name { get; }

        void Write(string dir, IReadOnlyCollection<IGrouping<string, TorRelayDetails>> countries);
    }
}
