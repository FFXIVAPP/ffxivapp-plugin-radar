// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GatheringNode.cs" company="SyndicatedLife">
//   Copyright(c) 2018 Ryan Wilson &amp;lt;syndicated.life@gmail.com&amp;gt; (http://syndicated.life/)
//   Licensed under the MIT license. See LICENSE.md in the solution root for full license information.
// </copyright>
// <summary>
//   GatheringNode.cs Implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FFXIVAPP.Plugin.Radar.Models {
    using FFXIVAPP.Plugin.Radar.Enums;

    using Sharlayan.Models;

    public class GatheringNode {
        public GatheringNode() {
            this.Localization = new Localization();
            this.Type = GatheringType.Unknown;
            this.Rarity = GatheringRarity.Normal;
        }

        public Localization Localization { get; set; }

        public GatheringRarity Rarity { get; set; }

        public GatheringType Type { get; set; }
    }
}