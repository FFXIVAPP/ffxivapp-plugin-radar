// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Initializer.cs" company="SyndicatedLife">
//   Copyright© 2007 - 2021 Ryan Wilson &amp;lt;syndicated.life@gmail.com&amp;gt; (https://syndicated.life/)
//   Licensed under the MIT license. See LICENSE.md in the solution root for full license information.
// </copyright>
// <summary>
//   Initializer.cs Implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FFXIVAPP.Plugin.Radar {
    using System.Collections.Generic;
    using System.Globalization;
    using System.Xml.Linq;

    using FFXIVAPP.Common.RegularExpressions;
    using FFXIVAPP.Plugin.Radar.Enums;
    using FFXIVAPP.Plugin.Radar.Helpers;
    using FFXIVAPP.Plugin.Radar.Models;
    using FFXIVAPP.Plugin.Radar.Properties;

    internal static class Initializer {
        /// <summary>
        /// </summary>
        public static void LoadFilters() {
            if (Constants.XFilters != null) {
                PluginViewModel.Instance.Filters.Clear();
                foreach (XElement xElement in Constants.XFilters.Descendants().Elements("Filter")) {
                    var xKey = (string) xElement.Attribute("Key");
                    if (!SharedRegEx.IsValidRegex(xKey)) {
                        continue;
                    }

                    var xLevel = (string) xElement.Element("Level");
                    var xType = (string) xElement.Element("Type");
                    if (string.IsNullOrWhiteSpace(xKey) || string.IsNullOrWhiteSpace(xType)) {
                        return;
                    }

                    int level;
                    int.TryParse(xLevel, out level);
                    var radarFilterItem = new RadarFilterItem(xKey) {
                        Level = level,
                    };
                    radarFilterItem.Type = xType;
                    PluginViewModel.Instance.Filters.Add(radarFilterItem);
                }
            }
        }

        /// <summary>
        /// </summary>
        public static void LoadSettings() {
            if (Constants.XSettings != null) {
                Settings.Default.Reset();
                foreach (XElement xElement in Constants.XSettings.Descendants().Elements("Setting")) {
                    var xKey = (string) xElement.Attribute("Key");
                    var xValue = (string) xElement.Element("Value");
                    if (string.IsNullOrWhiteSpace(xKey) || string.IsNullOrWhiteSpace(xValue)) {
                        return;
                    }

                    if (Constants.Settings.Contains(xKey)) {
                        Settings.Default.SetValue(xKey, xValue, CultureInfo.InvariantCulture);
                    }
                    else {
                        Constants.Settings.Add(xKey);
                    }
                }
            }
        }

        public static void SetGatheringNodes() {
            List<GatheringNode> botanyNodes = new List<GatheringNode>();

            #region Normal

            botanyNodes.Add(
                new GatheringNode {
                    Type = GatheringType.MainHand,
                    Localization = new Sharlayan.Models.Localization {
                        English = "Mature Tree",
                    },
                });
            botanyNodes.Add(
                new GatheringNode {
                    Type = GatheringType.OffHand,
                    Localization = new Sharlayan.Models.Localization {
                        English = "Lush Vegetation Patch",
                    },
                });

            #endregion

            #region Unspoiled

            botanyNodes.Add(
                new GatheringNode {
                    Type = GatheringType.MainHand,
                    Rarity = GatheringRarity.Unspoiled,
                    Localization = new Sharlayan.Models.Localization {
                        English = "Unspoiled Mature Tree",
                    },
                });
            botanyNodes.Add(
                new GatheringNode {
                    Type = GatheringType.OffHand,
                    Rarity = GatheringRarity.Unspoiled,
                    Localization = new Sharlayan.Models.Localization {
                        English = "Unspoiled Lush Vegetation",
                    },
                });

            #endregion

            #region Ephemeral

            botanyNodes.Add(
                new GatheringNode {
                    Type = GatheringType.MainHand,
                    Rarity = GatheringRarity.Ephemeral,
                    Localization = new Sharlayan.Models.Localization {
                        English = "Ephemeral Mature Tree",
                    },
                });
            botanyNodes.Add(
                new GatheringNode {
                    Type = GatheringType.OffHand,
                    Rarity = GatheringRarity.Ephemeral,
                    Localization = new Sharlayan.Models.Localization {
                        English = "Ephemeral Lush Vegetation",
                    },
                });

            #endregion

            #region Legendary

            botanyNodes.Add(
                new GatheringNode {
                    Type = GatheringType.MainHand,
                    Rarity = GatheringRarity.Legendary,
                    Localization = new Sharlayan.Models.Localization {
                        English = "Legendary Mature Tree",
                    },
                });
            botanyNodes.Add(
                new GatheringNode {
                    Type = GatheringType.OffHand,
                    Rarity = GatheringRarity.Legendary,
                    Localization = new Sharlayan.Models.Localization {
                        English = "Legendary Lush Vegetation",
                    },
                });

            #endregion

            Constants.GatheringNodes.Add("BTN", botanyNodes);

            #region Fishing

            List<GatheringNode> fishingNodes = new List<GatheringNode>();

            #region Unspoiled

            #endregion

            #region Ephemeral

            #endregion

            #region Legendary

            fishingNodes.Add(
                new GatheringNode {
                    Type = GatheringType.MainHand,
                    Rarity = GatheringRarity.Legendary,
                    Localization = new Sharlayan.Models.Localization {
                        English = "Legendary Mature Tree",
                    },
                });
            fishingNodes.Add(
                new GatheringNode {
                    Type = GatheringType.OffHand,
                    Rarity = GatheringRarity.Legendary,
                    Localization = new Sharlayan.Models.Localization {
                        English = "Legendary Lush Vegetation",
                    },
                });

            #endregion

            #endregion

            Constants.GatheringNodes.Add("FSH", fishingNodes);

            #region Mining

            List<GatheringNode> miningNodes = new List<GatheringNode>();

            #region Normal

            miningNodes.Add(
                new GatheringNode {
                    Type = GatheringType.MainHand,
                    Localization = new Sharlayan.Models.Localization {
                        English = "Mineral Deposit",
                    },
                });
            miningNodes.Add(
                new GatheringNode {
                    Type = GatheringType.OffHand,
                    Localization = new Sharlayan.Models.Localization {
                        English = "Rocky Outcropping",
                    },
                });

            #endregion

            #region Unspoiled

            miningNodes.Add(
                new GatheringNode {
                    Type = GatheringType.MainHand,
                    Rarity = GatheringRarity.Unspoiled,
                    Localization = new Sharlayan.Models.Localization {
                        English = "Unspoiled Mineral Deposit",
                    },
                });
            miningNodes.Add(
                new GatheringNode {
                    Type = GatheringType.OffHand,
                    Rarity = GatheringRarity.Unspoiled,
                    Localization = new Sharlayan.Models.Localization {
                        English = "Unspoiled Rocky Outcropping",
                    },
                });

            #endregion

            #region Ephemeral

            miningNodes.Add(
                new GatheringNode {
                    Type = GatheringType.MainHand,
                    Rarity = GatheringRarity.Ephemeral,
                    Localization = new Sharlayan.Models.Localization {
                        English = "Ephemeral Mineral Deposit",
                    },
                });
            miningNodes.Add(
                new GatheringNode {
                    Type = GatheringType.OffHand,
                    Rarity = GatheringRarity.Ephemeral,
                    Localization = new Sharlayan.Models.Localization {
                        English = "Ephemeral Rocky Outcropping",
                    },
                });

            #endregion

            #region Legendary

            miningNodes.Add(
                new GatheringNode {
                    Type = GatheringType.MainHand,
                    Rarity = GatheringRarity.Legendary,
                    Localization = new Sharlayan.Models.Localization {
                        English = "Legendary Mineral Deposit",
                    },
                });
            miningNodes.Add(
                new GatheringNode {
                    Type = GatheringType.OffHand,
                    Rarity = GatheringRarity.Legendary,
                    Localization = new Sharlayan.Models.Localization {
                        English = "Legendary Rocky Outcropping",
                    },
                });

            #endregion

            #endregion

            Constants.GatheringNodes.Add("MIN", miningNodes);
        }

        public static void SetupWindowTopMost() {
            WidgetTopMostHelper.HookWidgetTopMost();
        }
    }
}