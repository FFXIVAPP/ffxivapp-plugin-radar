// FFXIVAPP.Plugin.Radar ~ Initializer.cs
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

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Linq;
using FFXIVAPP.Common.RegularExpressions;
using FFXIVAPP.Plugin.Radar.Enums;
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
                    radarFilterItem.Type = xType;
                    PluginViewModel.Instance.Filters.Add(radarFilterItem);
                }
            }
        }

        public static void SetGatheringNodes()
        {
            #region Botany

            var botanyNodes = new List<GatheringNode>();

            #region Normal

            botanyNodes.Add(new GatheringNode
            {
                Type = GatheringType.MainHand,
                Localization = new Sharlayan.Models.Localization
                {
                    English = "Mature Tree"
                }
            });
            botanyNodes.Add(new GatheringNode
            {
                Type = GatheringType.OffHand,
                Localization = new Sharlayan.Models.Localization
                {
                    English = "Lush Vegetation Patch"
                }
            });

            #endregion

            #region Unspoiled

            botanyNodes.Add(new GatheringNode
            {
                Type = GatheringType.MainHand,
                Rarity = GatheringRarity.Unspoiled,
                Localization = new Sharlayan.Models.Localization
                {
                    English = "Unspoiled Mature Tree"
                }
            });
            botanyNodes.Add(new GatheringNode
            {
                Type = GatheringType.OffHand,
                Rarity = GatheringRarity.Unspoiled,
                Localization = new Sharlayan.Models.Localization
                {
                    English = "Unspoiled Lush Vegetation"
                }
            });

            #endregion

            #region Ephemeral

            botanyNodes.Add(new GatheringNode
            {
                Type = GatheringType.MainHand,
                Rarity = GatheringRarity.Ephemeral,
                Localization = new Sharlayan.Models.Localization
                {
                    English = "Ephemeral Mature Tree"
                }
            });
            botanyNodes.Add(new GatheringNode
            {
                Type = GatheringType.OffHand,
                Rarity = GatheringRarity.Ephemeral,
                Localization = new Sharlayan.Models.Localization
                {
                    English = "Ephemeral Lush Vegetation"
                }
            });

            #endregion

            #region Legendary

            botanyNodes.Add(new GatheringNode
            {
                Type = GatheringType.MainHand,
                Rarity = GatheringRarity.Legendary,
                Localization = new Sharlayan.Models.Localization
                {
                    English = "Legendary Mature Tree"
                }
            });
            botanyNodes.Add(new GatheringNode
            {
                Type = GatheringType.OffHand,
                Rarity = GatheringRarity.Legendary,
                Localization = new Sharlayan.Models.Localization
                {
                    English = "Legendary Lush Vegetation"
                }
            });

            #endregion

            #endregion

            Constants.GatheringNodes.Add("BTN", botanyNodes);

            #region Fishing

            var fishingNodes = new List<GatheringNode>();

            #region Normal

            #endregion

            #region Unspoiled

            #endregion

            #region Ephemeral

            #endregion

            #region Legendary

            fishingNodes.Add(new GatheringNode
            {
                Type = GatheringType.MainHand,
                Rarity = GatheringRarity.Legendary,
                Localization = new Sharlayan.Models.Localization
                {
                    English = "Legendary Mature Tree"
                }
            });
            fishingNodes.Add(new GatheringNode
            {
                Type = GatheringType.OffHand,
                Rarity = GatheringRarity.Legendary,
                Localization = new Sharlayan.Models.Localization
                {
                    English = "Legendary Lush Vegetation"
                }
            });

            #endregion

            #endregion

            Constants.GatheringNodes.Add("FSH", fishingNodes);

            #region Mining

            var miningNodes = new List<GatheringNode>();

            #region Normal

            miningNodes.Add(new GatheringNode
            {
                Type = GatheringType.MainHand,
                Localization = new Sharlayan.Models.Localization
                {
                    English = "Mineral Deposit"
                }
            });
            miningNodes.Add(new GatheringNode
            {
                Type = GatheringType.OffHand,
                Localization = new Sharlayan.Models.Localization
                {
                    English = "Rocky Outcropping"
                }
            });

            #endregion

            #region Unspoiled

            miningNodes.Add(new GatheringNode
            {
                Type = GatheringType.MainHand,
                Rarity = GatheringRarity.Unspoiled,
                Localization = new Sharlayan.Models.Localization
                {
                    English = "Unspoiled Mineral Deposit"
                }
            });
            miningNodes.Add(new GatheringNode
            {
                Type = GatheringType.OffHand,
                Rarity = GatheringRarity.Unspoiled,
                Localization = new Sharlayan.Models.Localization
                {
                    English = "Unspoiled Rocky Outcropping"
                }
            });

            #endregion

            #region Ephemeral

            miningNodes.Add(new GatheringNode
            {
                Type = GatheringType.MainHand,
                Rarity = GatheringRarity.Ephemeral,
                Localization = new Sharlayan.Models.Localization
                {
                    English = "Ephemeral Mineral Deposit"
                }
            });
            miningNodes.Add(new GatheringNode
            {
                Type = GatheringType.OffHand,
                Rarity = GatheringRarity.Ephemeral,
                Localization = new Sharlayan.Models.Localization
                {
                    English = "Ephemeral Rocky Outcropping"
                }
            });

            #endregion

            #region Legendary

            miningNodes.Add(new GatheringNode
            {
                Type = GatheringType.MainHand,
                Rarity = GatheringRarity.Legendary,
                Localization = new Sharlayan.Models.Localization
                {
                    English = "Legendary Mineral Deposit"
                }
            });
            miningNodes.Add(new GatheringNode
            {
                Type = GatheringType.OffHand,
                Rarity = GatheringRarity.Legendary,
                Localization = new Sharlayan.Models.Localization
                {
                    English = "Legendary Rocky Outcropping"
                }
            });

            #endregion

            #endregion

            Constants.GatheringNodes.Add("MIN", miningNodes);
        }

        public static void SetupWindowTopMost()
        {
            WidgetTopMostHelper.HookWidgetTopMost();
        }

        #region Declarations

        #endregion
    }
}
