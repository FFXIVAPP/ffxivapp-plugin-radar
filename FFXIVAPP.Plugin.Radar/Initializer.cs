// FFXIVAPP.Plugin.Radar
// FFXIVAPP & Related Plugins/Modules
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

using System;
using System.Globalization;
using System.Xml.Linq;
using FFXIVAPP.Common.RegularExpressions;
using FFXIVAPP.Memory.Core.Enums;
using FFXIVAPP.Plugin.Radar.Helpers;
using FFXIVAPP.Plugin.Radar.Models;
using FFXIVAPP.Plugin.Radar.Properties;

namespace FFXIVAPP.Plugin.Radar
{
    internal static class Initializer
    {
        /// <summary>
        /// </summary>
        public static void LoadSettings()
        {
            if (Constants.XSettings != null)
            {
                Settings.Default.Reset();
                foreach (var xElement in Constants.XSettings.Descendants()
                                                  .Elements("Setting"))
                {
                    var xKey = (string) xElement.Attribute("Key");
                    var xValue = (string) xElement.Element("Value");
                    if (String.IsNullOrWhiteSpace(xKey) || String.IsNullOrWhiteSpace(xValue))
                    {
                        return;
                    }
                    if (Constants.Settings.Contains(xKey))
                    {
                        Settings.Default.SetValue(xKey, xValue, CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        Constants.Settings.Add(xKey);
                    }
                }
            }
        }

        /// <summary>
        /// </summary>
        public static void LoadFilters()
        {
            if (Constants.XFilters != null)
            {
                PluginViewModel.Instance.Filters.Clear();
                foreach (var xElement in Constants.XFilters.Descendants()
                                                  .Elements("Filter"))
                {
                    var xKey = (string) xElement.Attribute("Key");
                    if (!SharedRegEx.IsValidRegex(xKey))
                    {
                        continue;
                    }
                    var xLevel = (string) xElement.Element("Level");
                    var xType = (string) xElement.Element("Type");
                    if (String.IsNullOrWhiteSpace(xKey) || String.IsNullOrWhiteSpace(xType))
                    {
                        return;
                    }
                    int level;
                    Int32.TryParse(xLevel, out level);
                    var radarFilterItem = new RadarFilterItem(xKey)
                    {
                        Level = level
                    };
                    switch (xType)
                    {
                        case "PC":
                            radarFilterItem.Type = Actor.Type.PC;
                            break;
                        case "Monster":
                            radarFilterItem.Type = Actor.Type.Monster;
                            break;
                        case "NPC":
                            radarFilterItem.Type = Actor.Type.NPC;
                            break;
                        case "Aetheryte":
                            radarFilterItem.Type = Actor.Type.Aetheryte;
                            break;
                        case "Gathering":
                            radarFilterItem.Type = Actor.Type.Gathering;
                            break;
                        case "Minion":
                            radarFilterItem.Type = Actor.Type.Minion;
                            break;
                    }
                    PluginViewModel.Instance.Filters.Add(radarFilterItem);
                }
            }
        }

        public static void SetupWindowTopMost()
        {
            WidgetTopMostHelper.HookWidgetTopMost();
        }

        #region Declarations

        #endregion
    }
}
