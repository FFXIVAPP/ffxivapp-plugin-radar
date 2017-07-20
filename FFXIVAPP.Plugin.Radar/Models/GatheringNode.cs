// FFXIVAPP.Plugin.Radar ~ GatheringNode.cs
// 
// Copyright © 2007 - 2017 Ryan Wilson - All Rights Reserved
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using FFXIVAPP.Plugin.Radar.Enums;

namespace FFXIVAPP.Plugin.Radar.Models
{
    public class GatheringNode
    {
        public GatheringNode()
        {
            Localization = new Sharlayan.Models.Localization();
            Type = GatheringType.Unknown;
            Rarity = GatheringRarity.Normal;
        }

        public Sharlayan.Models.Localization Localization { get; set; }
        public GatheringType Type { get; set; }
        public GatheringRarity Rarity { get; set; }
    }
}
