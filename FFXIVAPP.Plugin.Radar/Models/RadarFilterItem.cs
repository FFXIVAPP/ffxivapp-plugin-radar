// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RadarFilterItem.cs" company="SyndicatedLife">
//   Copyright(c) 2018 Ryan Wilson &amp;lt;syndicated.life@gmail.com&amp;gt; (http://syndicated.life/)
//   Licensed under the MIT license. See LICENSE.md in the solution root for full license information.
// </copyright>
// <summary>
//   RadarFilterItem.cs Implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FFXIVAPP.Plugin.Radar.Models {
    using System.Text.RegularExpressions;

    using FFXIVAPP.Common.RegularExpressions;

    public class RadarFilterItem {
        public RadarFilterItem(string key = "INVALID") {
            this.Key = key;
            this.Level = 0;
            this.Type = "Unknown";
            this.RegEx = new Regex(key, SharedRegEx.DefaultOptions | RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
        }

        public string Key { get; set; }

        public int Level { get; set; }

        public Regex RegEx { get; set; }

        public string Type { get; set; }
    }
}