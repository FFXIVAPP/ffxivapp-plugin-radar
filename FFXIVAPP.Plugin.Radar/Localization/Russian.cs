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

using System.Collections.Generic;
using System.Windows;

namespace FFXIVAPP.Plugin.Radar.Localization
{
    public abstract class Russian
    {
        private static readonly ResourceDictionary Dictionary = new ResourceDictionary();

        private static readonly List<string> RankB = new List<string>
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

        private static readonly List<string> RankA = new List<string>
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

        private static readonly List<string> RankS = new List<string>
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
            Dictionary.Add("radar_RadarWidgetHeader", "Виждет Радара");
            Dictionary.Add("radar_OpenNowButtonText", "Открыть Сейчас");
            Dictionary.Add("radar_ResetPositionButtonText", "Сброс Настроек");
            Dictionary.Add("radar_EnableClickThroughHeader", "Заблокировать Перетаскивание Мышью");
            Dictionary.Add("radar_WidgetOpacityHeader", "Непрозрачность Виджета");
            Dictionary.Add("radar_ShowTitlesOnRadarHeader", "Показать Заголовок Радара");
            Dictionary.Add("radar_UIScaleHeader", "Размер Интерфейса");
            Dictionary.Add("radar_RadarSettingsTabHeader", "Настройки Радара");
            Dictionary.Add("radar_PCShowHeader", "Показывать Игроков");
            Dictionary.Add("radar_PCShowNameHeader", "Показывать Имена Игроков");
            Dictionary.Add("radar_PCShowHPPercentHeader", "Показывать % HP Игрока");
            Dictionary.Add("radar_PCShowJobHeader", "Показать Job Игроков");
            Dictionary.Add("radar_PCShowDistanceHeader", "Показывать Расстояние до Игроков");
            Dictionary.Add("radar_NPCShowHeader", "Показывать NPC");
            Dictionary.Add("radar_NPCShowNameHeader", "Показывать Имя NPC");
            Dictionary.Add("radar_NPCShowHPPercentHeader", "Показывать % HP NPC");
            Dictionary.Add("radar_NPCShowDistanceHeader", "Показывать Расстояние до NPC");
            Dictionary.Add("radar_MonsterShowHeader", "Показывать Монстров");
            Dictionary.Add("radar_MonsterShowNameHeader", "Показывать Имена Монстров");
            Dictionary.Add("radar_MonsterShowHPPercentHeader", "Показывать % HP Монстров");
            Dictionary.Add("radar_MonsterShowDistanceHeader", "Показывать Расстояние до Монстров");
            Dictionary.Add("radar_GatheringShowHeader", "Показывать Ресурсы");
            Dictionary.Add("radar_GatheringShowNameHeader", "Показывать Названия Ресурсов");
            Dictionary.Add("radar_GatheringShowHPPercentHeader", "Показывать % HP Ресурсов");
            Dictionary.Add("radar_GatheringShowDistanceHeader", "Показывать Расстояние до Ресурсов");
            Dictionary.Add("radar_OtherShowHeader", "Показывать Остальное");
            Dictionary.Add("radar_OtherShowNameHeader", "Показывать Название Остального");
            Dictionary.Add("radar_OtherShowHPPercentHeader", "Показывать % HP Остального");
            Dictionary.Add("radar_OtherShowDistanceHeader", "Показыать Расстояние до Остального");
            Dictionary.Add("radar_GitHubButtonText", "Страница Проекта (GitHub)");
            Dictionary.Add("radar_PCFontSizeHeader", "Размер Шрифта Имени Игроков");
            Dictionary.Add("radar_PCFontColorHeader", "Цвет Шрифта Имени Игроков");
            Dictionary.Add("radar_NPCFontSizeHeader", "Размер Шрифта NPC");
            Dictionary.Add("radar_NPCFontColorHeader", "Цвет Шрифта NPC");
            Dictionary.Add("radar_MonsterFontSizeHeader", "Размер Шрифта Монстров");
            Dictionary.Add("radar_MonsterFontColorHeader", "Цвет Шрифта Монстров");
            Dictionary.Add("radar_GatheringFontSizeHeader", "Размер Шрифта Gathering");
            Dictionary.Add("radar_GatheringFontColorHeader", "Цвет Шрифта Gathering");
            Dictionary.Add("radar_OtherFontSizeHeader", "Размер Шрифта Других");
            Dictionary.Add("radar_OtherFontColorHeader", "Цвет Шрифта Других");

            Dictionary.Add("radar_MonsterShowRankColorHeader", "Показывать Только Монстров с Рангом");
            Dictionary.Add("radar_MonsterShowBRankHeader", "Показывать Монстров (B Rank)");
            Dictionary.Add("radar_MonsterShowARankHeader", "Показывать Монстров (A Rank)");
            Dictionary.Add("radar_MonsterShowSRankHeader", "Показывать Монстров (S Rank)");
            Dictionary.Add("radar_MonsterFontColorBRankHeader", "Цвет Шрифта Монстров (B Rank)");
            Dictionary.Add("radar_MonsterFontColorARankHeader", "Цвет Шрифта Монстров (A Rank)");
            Dictionary.Add("radar_MonsterFontColorSRankHeader", "Цвет Шрифта Монстров (S Rank)");
            Dictionary.Add("radar_FilterOptionsHeader", "Опции Фильтра");
            Dictionary.Add("radar_KeyLabel", "Ключ:");
            Dictionary.Add("radar_LevelLabel", "Уровень:");
            Dictionary.Add("radar_TypeLabel", "Тип:");
            Dictionary.Add("radar_DeleteFilterButton", "Удалить Фильтр");
            Dictionary.Add("radar_AddorUpdateFilterButton", "Добавить или Обновить Фильтр");
            Dictionary.Add("radar_SettingsRadarHeader", "Настройки:Радар");
            Dictionary.Add("radar_CompassModeLabel", "Модификация Компас");
            Dictionary.Add("radar_FilterRadarItemsLabel", "Фильтровать Вещи Радара");
            Dictionary.Add("radar_RadarUIScaleLabel", "Размер UI");
            Dictionary.Add("radar_SettingsRadarPCHeader", "Настройки:Радар:Игрок");
            Dictionary.Add("radar_SettingsRadarNPCHeader", "Настройки:Радар:NPC");
            Dictionary.Add("radar_SettingsRadarMonsterHeader", "Настройки:Радар:Монстры");
            Dictionary.Add("radar_SettingsRadarGatheringHeader", "Настройки:Радар:Gathering");
            Dictionary.Add("radar_SettingsRadarOtherHeader", "Настройки:Радар:Другие");
            Dictionary.Add("radar_RadarTitleBar", "[РАДАР]");

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
