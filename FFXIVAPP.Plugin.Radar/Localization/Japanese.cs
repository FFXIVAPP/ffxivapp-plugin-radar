// FFXIVAPP.Plugin.Radar ~ Japanese.cs
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

using System.Collections.Generic;
using System.Windows;

namespace FFXIVAPP.Plugin.Radar.Localization
{
    public abstract class Japanese
    {
        private static readonly ResourceDictionary Dictionary = new ResourceDictionary();

        private static readonly List<string> RankB = new List<string>
        {
            "死灰のアルビン",
            "バーバステル",
            "ブラッディ・マリー",
            "ダークヘルメット",
            "不滅のフェランド闘軍曹",
            "ガトリングス",
            "リーチキング",
            "モナーク・オーガフライ",
            "ミラドロッシュ",
            "ナウル",
            "アヴゼン",
            "フェクダ",
            "スェアーシロップ",
            "スコッグ・フリュー",
            "スティンギング・ソフィー",
            "ヴオコー",
            "ホワイトジョーカー",
            // Stormblood Rank B
            "アスワング",
            "ブッカブー",
            "デイダラ",
            "剣豪ガウキ",
            "姑獲鳥",
            "グアス・ア・ニードル",
            "雷撃のギョライ",
            "キワ",
            "クールマ",
            "マネス",
            "オゼルム",
            "宵闇のヤミニ"
        };

        private static readonly List<string> RankA = new List<string>
        {
            "魔導ヘルズクロー",
            "ウンクテヒ",
            "醜男のヴォガージャ",
            "コンヌ",
            "マーベリー",
            "ナン",
            "ファルネウス",
            "メルティゼリー",
            "ギルタブ",
            "ゲーデ",
            "マラク",
            "サボテンダー・バイラリーナ",
            "マヘス",
            "ファイナルフレイム",
            "ザニゴ",
            "アレクトリオン",
            "クーレア",
            // Stormblood Rank A
            "アンガダ",
            "アクラブアメル",
            "アール",
            "船幽霊",
            "ガジャースラ",
            "ギリメカラ",
            "ルミナーレ",
            "マヒシャ",
            "オニユメミ",
            "オルクス",
            "ソム",
            "バックスタイン"
        };

        private static readonly List<string> RankS = new List<string>
        {
            "ガーロック",
            "ケロゲロス",
            "クロック・ミテーヌ",
            "チェルノボーグ",
            "ナンディ",
            "ボナコン",
            "レドロネット",
            "ウルガル",
            "サウザンドキャスト・セダ",
            "マインドフレア",
            "サファト",
            "ブロンテス",
            "バルウール",
            "ミニョーカオン",
            "ヌニュヌウィ",
            "ゾーナ・シーカー",
            "アグリッパ",
            // Stormblood Rank S
            "ガンマ",
            "オキナ",
            "オルガナ",
            "ソルト・アンド・ライト"
        };

        /// <summary>
        /// </summary>
        /// <returns> </returns>
        public static ResourceDictionary Context()
        {
            Dictionary.Clear();
            Dictionary.Add("radar_", "*PH*");
            Dictionary.Add("radar_RadarWidgetHeader", "Radar Widget");
            Dictionary.Add("radar_OpenNowButtonText", "ウィジェットを開く");
            Dictionary.Add("radar_ResetPositionButtonText", "位置をリセットする");
            Dictionary.Add("radar_EnableClickThroughHeader", "ウィジェット上のクリックを下に通す");
            Dictionary.Add("radar_WidgetOpacityHeader", "ウィジェット透過率");
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
            Dictionary.Add("radar_GitHubButtonText", "プロジェクトソースを開く(GitHub)");
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
            Dictionary.Add("radar_RadarUIScaleLabel", "UI Scale");
            Dictionary.Add("radar_SettingsRadarPCHeader", "Settings:Radar:PC");
            Dictionary.Add("radar_SettingsRadarNPCHeader", "Settings:Radar:NPC");
            Dictionary.Add("radar_SettingsRadarMonsterHeader", "Settings:Radar:Monster");
            Dictionary.Add("radar_SettingsRadarGatheringHeader", "Settings:Radar:Gathering");
            Dictionary.Add("radar_SettingsRadarOtherHeader", "Settings:Radar:Other");
            Dictionary.Add("radar_RadarTitleBar", "[RADAR]");

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
