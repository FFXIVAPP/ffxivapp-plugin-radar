// FFXIVAPP.Plugin.Radar
// French.cs
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
    public abstract class French
    {
        private static readonly ResourceDictionary Dictionary = new ResourceDictionary();

        private static readonly List<string> RankB = new List<string>()
        {
            "Albin Le Blafard",
            "Barbastelle",
            "Mary La Sanglante",
            "Casque Noir",
            "Sergent-major Dalvag",
            "Acanthor",
            "Roi Des Sangsues",
            "Agrion Ogre Monarque",
            "Myradrosh",
            "Naul",
            "Ovjang",
            "Phecda",
            "Syrop D'égout",
            "Skogs Fru",
            "Sophie La Dardante",
            "Vuokho",
            "Joker Blanc"
        };

        private static readonly List<string> RankA = new List<string>()
        {
            "Griffe Des Enfers Magitek",
            "Unktehi",
            "Vogaal Ja",
            "Cornu",
            "Marberry",
            "Nahn",
            "Forneus",
            "Fondu",
            "Girtab",
            "Guédé Ti-Malice",
            "Marraco",
            "Pampa Ballerine",
            "Maahes",
            "Dernière Flamme De Dalvag",
            "Zanig'oh",
            "Alectryon",
            "Kurrea"
        };

        private static readonly List<string> RankS = new List<string>()
        {
            "Garlok",
            "Croabéros",
            "Croque-Mitaine",
            "Mahisha",
            "Nandi",
            "Bonnacon",
            "Laideronnette",
            "Wulgaru",
            "Theda La Tripoteuse",
            "Flagelleur Mental",
            "Safat",
            "Brontes",
            "Balaur",
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
            Dictionary.Add("radar_RadarWidgetHeader", "Radar Widget");
            Dictionary.Add("radar_OpenNowButtonText", "Open Now");
            Dictionary.Add("radar_ResetPositionButtonText", "Reset Settings");
            Dictionary.Add("radar_EnableClickThroughHeader", "Enable Click-Through On Radar");
            Dictionary.Add("radar_WidgetOpacityHeader", "Widget Opacity");
            Dictionary.Add("radar_ShowTitlesOnRadarHeader", "Show Titles On Radar");
            Dictionary.Add("radar_UIScaleHeader", "UI Scale");
            Dictionary.Add("radar_RadarSettingsTabHeader", "Radar Settings");
            Dictionary.Add("radar_PCShowHeader", "PC Show");
            Dictionary.Add("radar_PCShowNameHeader", "PC Show Name");
            Dictionary.Add("radar_PCShowHPPercentHeader", "PC Show HP Percent");
            Dictionary.Add("radar_PCShowJobHeader", "PC Show Job");
            Dictionary.Add("radar_PCShowDistanceHeader", "PC Show Distance");
            Dictionary.Add("radar_NPCShowHeader", "NPC Show");
            Dictionary.Add("radar_NPCShowNameHeader", "NPC Show Name");
            Dictionary.Add("radar_NPCShowHPPercentHeader", "NPC Show HP Percent");
            Dictionary.Add("radar_NPCShowDistanceHeader", "NPC Show Distance");
            Dictionary.Add("radar_MonsterShowHeader", "Monster Show");
            Dictionary.Add("radar_MonsterShowNameHeader", "Monster Show Name");
            Dictionary.Add("radar_MonsterShowHPPercentHeader", "Monster Show HP Percent");
            Dictionary.Add("radar_MonsterShowDistanceHeader", "Monster Show Distance");
            Dictionary.Add("radar_GatheringShowHeader", "Gathering Show");
            Dictionary.Add("radar_GatheringShowNameHeader", "Gathering Show Name");
            Dictionary.Add("radar_GatheringShowHPPercentHeader", "Gathering Show HP Percent");
            Dictionary.Add("radar_GatheringShowDistanceHeader", "Gathering Show Distance");
            Dictionary.Add("radar_OtherShowHeader", "Other Show");
            Dictionary.Add("radar_OtherShowNameHeader", "Other Show Name");
            Dictionary.Add("radar_OtherShowHPPercentHeader", "Other Show HP Percent");
            Dictionary.Add("radar_OtherShowDistanceHeader", "Other Show Distance");
            Dictionary.Add("radar_GitHubButtonText", "Open Project Source (GitHub)");
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

        public static List<string> GetRankedMonster(string name)
        {
            List<string> monsters;
            switch (name)
            {
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
