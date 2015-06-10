// FFXIVAPP.Plugin.Radar
// Chinese.cs
// 
// Copyright © 2007 - 2015 Ryan Wilson - All Rights Reserved
// 
// Redistribution and use in source and binary forms, with or without 
// modification, are permitted provided that the following conditions are met: 
// 
//  * Redistributions of source code must retain the above copyright notice, 
//    this list of conditions and the following disclaimer. 
//  * Redistributions in binary form must reproduce the above copyright 
//    notice, this list of conditions and the following disclaimer in the 
//    documentation and/or other materials provided with the distribution. 
//  * Neither the name of SyndicatedLife nor the names of its contributors may 
//    be used to endorse or promote products derived from this software 
//    without specific prior written permission. 
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" 
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE 
// IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE 
// ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE 
// LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR 
// CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF 
// SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS 
// INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN 
// CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
// ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
// POSSIBILITY OF SUCH DAMAGE. 

using System.Collections.Generic;
using System.Windows;

namespace FFXIVAPP.Plugin.Radar.Localization
{
    public abstract class Chinese
    {
        private static readonly ResourceDictionary Dictionary = new ResourceDictionary();

        private static readonly List<string> RankB = new List<string>()
        {
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

        private static readonly List<string> RankA = new List<string>()
        {
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

        private static readonly List<string> RankS = new List<string>()
        {
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
        public static ResourceDictionary Context()
        {
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

            Dictionary.Add("radar_MonsterShowOnlyRankHeader", "Show Ranked Monester Only");
            Dictionary.Add("radar_MonsterShowBRankHeader", "Monster Show (B Rank)");
            Dictionary.Add("radar_MonsterShowARankHeader", "Monster Show (A Rank)");
            Dictionary.Add("radar_MonsterShowSRankHeader", "Monster Show (S Rank)");
            Dictionary.Add("radar_MonsterFontColorBRankHeader", "Monster Font Color (B Rank)");
            Dictionary.Add("radar_MonsterFontColorARankHeader", "Monster Font Color (A Rank)");
            Dictionary.Add("radar_MonsterFontColorSRankHeader", "Monster Font Color (S Rank)");

            return Dictionary;
        }

        public static List<string> getRankedMob(string name) {
            List<string> _mobs;
            switch(name) {
                case "A":
                    _mobs = RankA;
                    break;
                case "S":
                    _mobs = RankS;
                    break;
                default:
                    _mobs = RankB;
                    break;
            }

            return _mobs;
        }
    }
}
