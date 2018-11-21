// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Japanese.cs" company="SyndicatedLife">
//   Copyright(c) 2018 Ryan Wilson &amp;lt;syndicated.life@gmail.com&amp;gt; (http://syndicated.life/)
//   Licensed under the MIT license. See LICENSE.md in the solution root for full license information.
// </copyright>
// <summary>
//   Japanese.cs Implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FFXIVAPP.Plugin.Radar.Localization {
    using System.Collections.Generic;
    using System.Windows;

    public abstract class Japanese {
        private static readonly ResourceDictionary Dictionary = new ResourceDictionary();

        private static readonly List<string> RankA = new List<string> {
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

            // Heavensward Rank A
            "ミルカ",
            "リューバ",
            "ブネ",
            "アガトス",
            "パイルラスタ",
            "ワイバーンロード",
            "機兵のスリップキンクス",
            "ストラス",
            "キャムパクティ",
            "センチブロッサム"
            "エンケドラス",
            "シシウトゥル",

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

        private static readonly List<string> RankB = new List<string> {
            "アシエン・アルビン",
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

            // Heavensward Rank B
            "アルティック",
            "ギガントピテクス",
            "グナース・コメットドローン",
            "クルーゼ",
            "リュキダス",
            "オムニ",
            "プテリゴトゥス",
            "舞手のサヌバリ",
            "スキタリス",
            "スクオンク",
            "スケアクロウ",
            "テクスタ",

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

        private static readonly List<string> RankS = new List<string> {
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
            
            // Heavensward Rank S
            "カイザーベヒーモス",
            "ガンダルヴァ",
            "セーンムルウ",
            "ペイルライダー",
            "レウクロッタ"
            "極楽鳥",

            // Stormblood Rank S
            "ボーンクローラー",
            "ガンマ",
            "オキナ",
            "オルガナ",
            "ソルト・アンド・ライト",
            "ウドンゲ"
        };

        /// <summary>
        /// </summary>
        /// <returns> </returns>
        public static ResourceDictionary Context() {
            Dictionary.Clear();
            Dictionary.Add("radar_", "*PH*");
            Dictionary.Add("radar_RadarWidgetHeader", "レーダーウィジェット");
            Dictionary.Add("radar_OpenNowButtonText", "開く");
            Dictionary.Add("radar_ResetPositionButtonText", "設定を初期化");
            Dictionary.Add("radar_EnableClickThroughHeader", "Enable Click-Through On Radar");
            Dictionary.Add("radar_WidgetOpacityHeader", "ウィジェットの透過度");
            Dictionary.Add("radar_ShowTitlesOnRadarHeader", "レーダーにタイトルを表示");
            Dictionary.Add("radar_UIScaleHeader", "UI拡大率");
            Dictionary.Add("radar_RadarSettingsTabHeader", "レーダー設定");
            Dictionary.Add("radar_PCShowHeader", "プレイヤーの表示");
            Dictionary.Add("radar_PCShowNameHeader", "プレイヤーの名前を表示する");
            Dictionary.Add("radar_PCShowHPPercentHeader", "プレイヤーのHPバーを表示する");
            Dictionary.Add("radar_PCShowJobHeader", "プレイヤーのジョブを表示する");
            Dictionary.Add("radar_PCShowDistanceHeader", "プレイヤーを表示する距離");
            Dictionary.Add("radar_NPCShowHeader", "NPCの表示");
            Dictionary.Add("radar_NPCShowNameHeader", "NPCの名前を表示");
            Dictionary.Add("radar_NPCShowHPPercentHeader", "NPCのHP（％）を表示");
            Dictionary.Add("radar_NPCShowDistanceHeader", "NPCを表示する距離");
            Dictionary.Add("radar_MonsterShowHeader", "モンスター表示");
            Dictionary.Add("radar_MonsterShowNameHeader", "モンスターの名前を表示");
            Dictionary.Add("radar_MonsterShowHPPercentHeader", "モンスターのHP（％）を表示");
            Dictionary.Add("radar_MonsterShowDistanceHeader", "モンスターの表示距離");
            Dictionary.Add("radar_GatheringShowHeader", "採集（ギャザリング）の表示");
            Dictionary.Add("radar_GatheringShowNameHeader", "採集場所の名前");
            Dictionary.Add("radar_GatheringShowHPPercentHeader", "採集場所のHP（％）を表示");
            Dictionary.Add("radar_GatheringShowDistanceHeader", "採集場所を表示する距離");
            Dictionary.Add("radar_OtherShowHeader", "その他の表示");
            Dictionary.Add("radar_OtherShowNameHeader", "その他の名前を表示する");
            Dictionary.Add("radar_OtherShowHPPercentHeader", "その他のHP（％）を表示");
            Dictionary.Add("radar_OtherShowDistanceHeader", "その他を表示する距離");
            Dictionary.Add("radar_GitHubButtonText", "プロジェクトのソースを開く（GitHub）");
            Dictionary.Add("radar_PCFontSizeHeader", "プレイヤーの文字サイズ");
            Dictionary.Add("radar_PCFontColorHeader", "プレイヤーの文字色");
            Dictionary.Add("radar_NPCFontSizeHeader", "NPCの文字サイズ");
            Dictionary.Add("radar_NPCFontColorHeader", "NPCの文字色");
            Dictionary.Add("radar_MonsterFontSizeHeader", "モンスターの文字サイズ");
            Dictionary.Add("radar_MonsterFontColorHeader", "モンスターの文字色");
            Dictionary.Add("radar_GatheringFontSizeHeader", "採集場所の文字サイズ");
            Dictionary.Add("radar_GatheringFontColorHeader", "採集場所の文字色");
            Dictionary.Add("radar_OtherFontSizeHeader", "その他の文字サイズ");
            Dictionary.Add("radar_OtherFontColorHeader", "その他の文字色");

            Dictionary.Add("radar_MonsterShowRankColorHeader", "ランク付きモンスターを色別に表示");
            Dictionary.Add("radar_MonsterShowBRankHeader", "モンスターの表示（Bランク）");
            Dictionary.Add("radar_MonsterShowARankHeader", "モンスターの表示（Aランク）");
            Dictionary.Add("radar_MonsterShowSRankHeader", "モンスターの表示（Sランク）");
            Dictionary.Add("radar_MonsterFontColorBRankHeader", "モンスターの文字色（Bランク）");
            Dictionary.Add("radar_MonsterFontColorARankHeader", "モンスターの文字色（Aランク）");
            Dictionary.Add("radar_MonsterFontColorSRankHeader", "モンスターの文字色（Sランク）");
            Dictionary.Add("radar_FilterOptionsHeader", "フィルタオプション");
            Dictionary.Add("radar_KeyLabel", "キーワード：");
            Dictionary.Add("radar_LevelLabel", "レベル：");
            Dictionary.Add("radar_TypeLabel", "種類：");
            Dictionary.Add("radar_DeleteFilterButton", " - ");
            Dictionary.Add("radar_AddorUpdateFilterButton", "フィルタの追加もしくは更新");
            Dictionary.Add("radar_SettingsRadarHeader", "設定：レーダーr");
            Dictionary.Add("radar_CompasModeLabel", "コンパスモード");
            Dictionary.Add("radar_FilterRadarItemsLabel", "レーダー項目のフィルタ");
            Dictionary.Add("radar_ShowEntityDebugLabel", "エンティティのデバッグ情報を表示");
            Dictionary.Add("radar_RadarUIScaleLabel", "UI拡大率");
            Dictionary.Add("radar_SettingsRadarPCHeader", "設定：レーダー：プレイヤー");
            Dictionary.Add("radar_SettingsRadarNPCHeader", "設定：レーダー：NPC");
            Dictionary.Add("radar_SettingsRadarMonsterHeader", "設定：レーダー：モンスター");
            Dictionary.Add("radar_SettingsRadarGatheringHeader", "設定：レーダー：採集場所");
            Dictionary.Add("radar_SettingsRadarOtherHeader", "設定」レーダー：その他");
            Dictionary.Add("radar_RadarTitleBar", "[レーダー]");

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