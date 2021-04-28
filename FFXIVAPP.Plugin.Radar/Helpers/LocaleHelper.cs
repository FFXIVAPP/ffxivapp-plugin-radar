// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LocaleHelper.cs" company="SyndicatedLife">
//   Copyright© 2007 - 2021 Ryan Wilson &amp;lt;syndicated.life@gmail.com&amp;gt; (https://syndicated.life/)
//   Licensed under the MIT license. See LICENSE.md in the solution root for full license information.
// </copyright>
// <summary>
//   LocaleHelper.cs Implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FFXIVAPP.Plugin.Radar.Helpers {
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Windows;

    using FFXIVAPP.Plugin.Radar.Localization;

    internal static class LocaleHelper {
        public static List<string> GetRankedMonsters(string name = "ALL") {
            List<string> monsters;
            var culture = Constants.CultureInfo.TwoLetterISOLanguageName;

            switch (culture) {
                case "fr":
                    monsters = French.GetRankedMonster(name);
                    break;
                case "ja":
                    monsters = Japanese.GetRankedMonster(name);
                    break;
                case "de":
                    monsters = German.GetRankedMonster(name);
                    break;
                case "zh":
                    monsters = Chinese.GetRankedMonster(name);
                    break;
                default:
                    monsters = English.GetRankedMonster(name);
                    break;
            }

            return monsters;
        }

        /// <summary>
        /// </summary>
        /// <param name="cultureInfo"> </param>
        public static Dictionary<string, string> Update(CultureInfo cultureInfo) {
            var culture = cultureInfo.TwoLetterISOLanguageName;
            ResourceDictionary dictionary;
            if (Constants.Supported.Contains(culture)) {
                switch (culture) {
                    case "fr":
                        dictionary = French.Context();
                        break;
                    case "ja":
                        dictionary = Japanese.Context();
                        break;
                    case "de":
                        dictionary = German.Context();
                        break;
                    case "zh":
                        dictionary = Chinese.Context();
                        break;
                    case "ru":
                        dictionary = Russian.Context();
                        break;
                    default:
                        dictionary = English.Context();
                        break;
                }
            }
            else {
                dictionary = English.Context();
            }

            return dictionary.Cast<DictionaryEntry>().ToDictionary(item => (string) item.Key, item => (string) item.Value);
        }
    }
}