// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Chinese.cs" company="SyndicatedLife">
//   Copyright(c) 2018 Ryan Wilson &amp;lt;syndicated.life@gmail.com&amp;gt; (http://syndicated.life/)
//   Licensed under the MIT license. See LICENSE.md in the solution root for full license information.
// </copyright>
// <summary>
//   Chinese.cs Implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FFXIVAPP.Plugin.Radar.Localization {
    using System.Collections.Generic;
    using System.Windows;

    public abstract class Chinese {
        private static readonly ResourceDictionary Dictionary = new ResourceDictionary();

        private static readonly List<string> RankA = new List<string> {
            "Hellsclaw",
            "Unktehi",
            "Vogaal Ja",
            "Cornu",
            "Marberry",
            "Nahn",
            "Forneus",
            "Melt",
            "Girtab",
            "Ghede Ti Malice",
            "Marraco",
            "Sabotender Bailarina",
            "Maahes",
            "Dalvag's Final Flame",
            "Zanig'oh",
            "Alectyron",
            "Kurrea"
        };

        private static readonly List<string> RankB = new List<string> {
            "Albin the Ashen",
            "Barbastelle",
            "Bloody Mary",
            "Dark Helmet",
            "Flame Sergeant Dalvag",
            "Gatling",
            "Leech King",
            "Monarch Ogrefly",
            "Myradrosh",
            "Naul",
            "Ovjang",
            "Phecda",
            "Sewer Syrup",
            "Skogs Fru",
            "Stinging Sophie",
            "Vuokho",
            "White Joker"
        };

        private static readonly List<string> RankS = new List<string> {
            "Garlok",
            "Croakadile",
            "Croque-Mitaine",
            "Mahisha",
            "Nandi",
            "Bonnacon",
            "Laideronnette",
            "Wulgaru",
            "Thousand-cast Theda",
            "Mindflayer",
            "Safat",
            "Brontes",
            "Lampalagua",
            "Minhocao",
            "Nunyunuwi",
            "Zona Seeker",
            "Agrippa the Mighty"
        };

        /// <summary>
        /// </summary>
        /// <returns> </returns>
        public static ResourceDictionary Context() {
            Dictionary.Clear();
            Dictionary.Add("radar_", "*PH*");
            Dictionary.Add("radar_RadarWidgetHeader", "雷达小工具");
            Dictionary.Add("radar_OpenNowButtonText", "现在打开");
            Dictionary.Add("radar_ResetPositionButtonText", "重设设置");
            Dictionary.Add("radar_EnableClickThroughHeader", "在雷达上开启 Click-Through");
            Dictionary.Add("radar_WidgetOpacityHeader", "小工具不透明");
            Dictionary.Add("radar_ShowTitlesOnRadarHeader", "在雷达上显示注释");
            Dictionary.Add("radar_UIScaleHeader", "界面规模");
            Dictionary.Add("radar_RadarSettingsTabHeader", "雷达设置");
            Dictionary.Add("radar_PCShowHeader", "PC 显示");
            Dictionary.Add("radar_PCShowNameHeader", "PC 显示名称");
            Dictionary.Add("radar_PCShowHPPercentHeader", "PC 显示 HP Percent");
            Dictionary.Add("radar_PCShowJobHeader", "PC 显示工作");
            Dictionary.Add("radar_PCShowDistanceHeader", "PC 显示距离");
            Dictionary.Add("radar_NPCShowHeader", "NPC 显示");
            Dictionary.Add("radar_NPCShowNameHeader", "NPC 显示名称");
            Dictionary.Add("radar_NPCShowHPPercentHeader", "NPC 显示 HP Percent");
            Dictionary.Add("radar_NPCShowDistanceHeader", "NPC 显示距离");
            Dictionary.Add("radar_MonsterShowHeader", "Monster 显示");
            Dictionary.Add("radar_MonsterShowNameHeader", "Monster 显示名称");
            Dictionary.Add("radar_MonsterShowHPPercentHeader", "Monster 显示 HP Percent");
            Dictionary.Add("radar_MonsterShowDistanceHeader", "Monster 显示距离");
            Dictionary.Add("radar_GatheringShowHeader", "Gathering 显示");
            Dictionary.Add("radar_GatheringShowNameHeader", "Gathering 显示名称");
            Dictionary.Add("radar_GatheringShowHPPercentHeader", "Gathering 显示 HP Percent");
            Dictionary.Add("radar_GatheringShowDistanceHeader", "Gathering 显示距离");
            Dictionary.Add("radar_OtherShowHeader", "其他显示");
            Dictionary.Add("radar_OtherShowNameHeader", "其他显示名称");
            Dictionary.Add("radar_OtherShowHPPercentHeader", "其他显示 HP Percent");
            Dictionary.Add("radar_OtherShowDistanceHeader", "其他显示距离");
            Dictionary.Add("radar_GitHubButtonText", "打开项目源代码(GitHub)");
            Dictionary.Add("radar_PCFontSizeHeader", "PC Font Size");
            Dictionary.Add("radar_PCFontColorHeader", "PC Font Color");
            Dictionary.Add("radar_NPCFontSizeHeader", "NPC Font Size");
            Dictionary.Add("radar_NPCFontColorHeader", "NPC Font Color");
            Dictionary.Add("radar_MonsterFontSizeHeader", "Monster Font Size");
            Dictionary.Add("radar_MonsterFontColorHeader", "Monster Font Color");
            Dictionary.Add("radar_GatheringFontSizeHeader", "Gathering Font Size");
            Dictionary.Add("radar_GatheringFontColorHeader", "Gathering Font Color");
            Dictionary.Add("radar_OtherFontSizeHeader", "Other Font Size");
            Dictionary.Add("radar_OtherFontColorHeader", "Other Font Color");

            Dictionary.Add("radar_MonsterShowRankColorHeader", "Show Ranked Monsters In Color");
            Dictionary.Add("radar_MonsterShowBRankHeader", "Monster Show (B Rank)");
            Dictionary.Add("radar_MonsterShowARankHeader", "Monster Show (A Rank)");
            Dictionary.Add("radar_MonsterShowSRankHeader", "Monster Show (S Rank)");
            Dictionary.Add("radar_MonsterFontColorBRankHeader", "Monster Font Color (B Rank)");
            Dictionary.Add("radar_MonsterFontColorARankHeader", "Monster Font Color (A Rank)");
            Dictionary.Add("radar_MonsterFontColorSRankHeader", "Monster Font Color (S Rank)");
            Dictionary.Add("radar_FilterOptionsHeader", "Filter Options");
            Dictionary.Add("radar_KeyLabel", "Key:");
            Dictionary.Add("radar_LevelLabel", "Level:");
            Dictionary.Add("radar_TypeLabel", "Type:");
            Dictionary.Add("radar_DeleteFilterButton", " - ");
            Dictionary.Add("radar_AddorUpdateFilterButton", "Add Or Update Filter");
            Dictionary.Add("radar_SettingsRadarHeader", "Settings:Radar");
            Dictionary.Add("radar_CompasModeLabel", "Compass Mode");
            Dictionary.Add("radar_FilterRadarItemsLabel", "Filter Radar Items");
            Dictionary.Add("radar_ShowEntityDebugLabel", "Show Entity Debug Information");
            Dictionary.Add("radar_RadarUIScaleLabel", "UI Scale");
            Dictionary.Add("radar_SettingsRadarPCHeader", "Settings:Radar:PC");
            Dictionary.Add("radar_SettingsRadarNPCHeader", "Settings:Radar:NPC");
            Dictionary.Add("radar_SettingsRadarMonsterHeader", "Settings:Radar:Monster");
            Dictionary.Add("radar_SettingsRadarGatheringHeader", "Settings:Radar:Gathering");
            Dictionary.Add("radar_SettingsRadarOtherHeader", "Settings:Radar:Other");
            Dictionary.Add("radar_RadarTitleBar", "[RADAR]");

            return Dictionary;
        }

        public static List<string> GetRankedMonster(string name) {
            List<string> monsters;
            switch (name) {
                case "B":
                    monsters = RankB;
                    break;
                case "A":
                    monsters = RankA;
                    break;
                case "S":
                    monsters = RankS;
                    break;
                default:
                    monsters = new List<string>();
                    monsters.AddRange(RankB);
                    monsters.AddRange(RankA);
                    monsters.AddRange(RankS);
                    break;
            }

            return monsters;
        }
    }
}