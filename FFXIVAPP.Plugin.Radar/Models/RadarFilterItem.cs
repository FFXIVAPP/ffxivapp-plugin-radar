// FFXIVAPP.Plugin.Radar ~ RadarFilterItem.cs
// 
// Copyright © 2007 - 2015 Ryan Wilson - All Rights Reserved
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

using System.Text.RegularExpressions;
using FFXIVAPP.Common.RegularExpressions;
using FFXIVAPP.Memory.Core.Enums;

namespace FFXIVAPP.Plugin.Radar.Models
{
    public class RadarFilterItem
    {
        public RadarFilterItem(string key = "INVALID")
        {
            Key = key;
            Level = 0;
            Type = Actor.Type.Unknown;
            RegEx = new Regex(key, SharedRegEx.DefaultOptions | RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
        }

        public string Key { get; set; }
        public int Level { get; set; }
        public Actor.Type Type { get; set; }
        public Regex RegEx { get; set; }
    }
}
