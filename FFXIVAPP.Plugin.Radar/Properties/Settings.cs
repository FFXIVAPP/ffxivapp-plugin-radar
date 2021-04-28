// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Settings.cs" company="SyndicatedLife">
//   Copyright© 2007 - 2021 Ryan Wilson &amp;lt;syndicated.life@gmail.com&amp;gt; (https://syndicated.life/)
//   Licensed under the MIT license. See LICENSE.md in the solution root for full license information.
// </copyright>
// <summary>
//   Settings.cs Implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FFXIVAPP.Plugin.Radar.Properties {
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Configuration;
    using System.Diagnostics;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows.Media;
    using System.Xml.Linq;

    using FFXIVAPP.Common.Helpers;
    using FFXIVAPP.Common.Models;
    using FFXIVAPP.Common.Utilities;
    using FFXIVAPP.Plugin.Radar.Models;

    using NLog;

    using ColorConverter = System.Windows.Media.ColorConverter;
    using FontFamily = System.Drawing.FontFamily;

    internal class Settings : ApplicationSettingsBase, INotifyPropertyChanged {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private static Settings _default;

        public new event PropertyChangedEventHandler PropertyChanged = delegate { };

        public static Settings Default {
            get {
                return _default ?? (_default = (Settings) Synchronized(new Settings()));
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("False")]
        public bool FilterRadarItems {
            get {
                return (bool) this["FilterRadarItems"];
            }

            set {
                this["FilterRadarItems"] = value;
                this.RaisePropertyChanged();
            }
        }

        [ApplicationScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue(
            @"<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfString xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <string>8</string>
  <string>9</string>
  <string>10</string>
  <string>11</string>
  <string>12</string>
  <string>13</string>
  <string>14</string>
  <string>15</string>
  <string>16</string>
  <string>17</string>
  <string>18</string>
  <string>19</string>
  <string>20</string>
  <string>21</string>
  <string>22</string>
  <string>23</string>
  <string>24</string>
</ArrayOfString>")]
        public StringCollection FontSizeList {
            get {
                return (StringCollection) this["FontSizeList"];
            }

            set {
                this["FontSizeList"] = value;
                this.RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("Orange")]
        public string GatheringFontColor {
            get {
                return (string) this["GatheringFontColor"];
            }

            set {
                this["GatheringFontColor"] = value;
                this.RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("12")]
        public string GatheringFontSize {
            get {
                return (string) this["GatheringFontSize"];
            }

            set {
                this["GatheringFontSize"] = value;
                this.RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("True")]
        public bool GatheringShow {
            get {
                return (bool) this["GatheringShow"];
            }

            set {
                this["GatheringShow"] = value;
                this.RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("False")]
        public bool GatheringShowDistance {
            get {
                return (bool) this["GatheringShowDistance"];
            }

            set {
                this["GatheringShowDistance"] = value;
                this.RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("False")]
        public bool GatheringShowHPPercent {
            get {
                return (bool) this["GatheringShowHPPercent"];
            }

            set {
                this["GatheringShowHPPercent"] = value;
                this.RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("True")]
        public bool GatheringShowName {
            get {
                return (bool) this["GatheringShowName"];
            }

            set {
                this["GatheringShowName"] = value;
                this.RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("Red")]
        public string MonsterFontColor {
            get {
                return (string) this["MonsterFontColor"];
            }

            set {
                this["MonsterFontColor"] = value;
                this.RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("Silver")]
        public string MonsterFontColorARank {
            get {
                return (string) this["MonsterFontColorARank"];
            }

            set {
                this["MonsterFontColorARank"] = value;
                this.RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("Plum")]
        public string MonsterFontColorBRank {
            get {
                return (string) this["MonsterFontColorBRank"];
            }

            set {
                this["MonsterFontColorBRank"] = value;
                this.RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("SkyBlue")]
        public string MonsterFontColorSRank {
            get {
                return (string) this["MonsterFontColorSRank"];
            }

            set {
                this["MonsterFontColorSRank"] = value;
                this.RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("12")]
        public string MonsterFontSize {
            get {
                return (string) this["MonsterFontSize"];
            }

            set {
                this["MonsterFontSize"] = value;
                this.RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("True")]
        public bool MonsterShow {
            get {
                return (bool) this["MonsterShow"];
            }

            set {
                this["MonsterShow"] = value;
                this.RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("False")]
        public bool MonsterShowDistance {
            get {
                return (bool) this["MonsterShowDistance"];
            }

            set {
                this["MonsterShowDistance"] = value;
                this.RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("False")]
        public bool MonsterShowHPPercent {
            get {
                return (bool) this["MonsterShowHPPercent"];
            }

            set {
                this["MonsterShowHPPercent"] = value;
                this.RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("True")]
        public bool MonsterShowName {
            get {
                return (bool) this["MonsterShowName"];
            }

            set {
                this["MonsterShowName"] = value;
                this.RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("False")]
        public bool MonsterShowRankColor {
            get {
                return (bool) this["MonsterShowRankColor"];
            }

            set {
                this["MonsterShowRankColor"] = value;
                this.RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("LimeGreen")]
        public string NPCFontColor {
            get {
                return (string) this["NPCFontColor"];
            }

            set {
                this["NPCFontColor"] = value;
                this.RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("12")]
        public string NPCFontSize {
            get {
                return (string) this["NPCFontSize"];
            }

            set {
                this["NPCFontSize"] = value;
                this.RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("False")]
        public bool NPCShow {
            get {
                return (bool) this["NPCShow"];
            }

            set {
                this["NPCShow"] = value;
                this.RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("False")]
        public bool NPCShowDistance {
            get {
                return (bool) this["NPCShowDistance"];
            }

            set {
                this["NPCShowDistance"] = value;
                this.RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("False")]
        public bool NPCShowHPPercent {
            get {
                return (bool) this["NPCShowHPPercent"];
            }

            set {
                this["NPCShowHPPercent"] = value;
                this.RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("False")]
        public bool NPCShowName {
            get {
                return (bool) this["NPCShowName"];
            }

            set {
                this["NPCShowName"] = value;
                this.RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("Yellow")]
        public string OtherFontColor {
            get {
                return (string) this["OtherFontColor"];
            }

            set {
                this["OtherFontColor"] = value;
                this.RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("12")]
        public string OtherFontSize {
            get {
                return (string) this["OtherFontSize"];
            }

            set {
                this["OtherFontSize"] = value;
                this.RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("True")]
        public bool OtherShow {
            get {
                return (bool) this["OtherShow"];
            }

            set {
                this["OtherShow"] = value;
                this.RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("False")]
        public bool OtherShowDistance {
            get {
                return (bool) this["OtherShowDistance"];
            }

            set {
                this["OtherShowDistance"] = value;
                this.RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("False")]
        public bool OtherShowHPPercent {
            get {
                return (bool) this["OtherShowHPPercent"];
            }

            set {
                this["OtherShowHPPercent"] = value;
                this.RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("True")]
        public bool OtherShowName {
            get {
                return (bool) this["OtherShowName"];
            }

            set {
                this["OtherShowName"] = value;
                this.RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("White")]
        public string PCFontColor {
            get {
                return (string) this["PCFontColor"];
            }

            set {
                this["PCFontColor"] = value;
                this.RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("12")]
        public string PCFontSize {
            get {
                return (string) this["PCFontSize"];
            }

            set {
                this["PCFontSize"] = value;
                this.RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("True")]
        public bool PCShow {
            get {
                return (bool) this["PCShow"];
            }

            set {
                this["PCShow"] = value;
                this.RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("False")]
        public bool PCShowDistance {
            get {
                return (bool) this["PCShowDistance"];
            }

            set {
                this["PCShowDistance"] = value;
                this.RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("False")]
        public bool PCShowHPPercent {
            get {
                return (bool) this["PCShowHPPercent"];
            }

            set {
                this["PCShowHPPercent"] = value;
                this.RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("True")]
        public bool PCShowJob {
            get {
                return (bool) this["PCShowJob"];
            }

            set {
                this["PCShowJob"] = value;
                this.RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("False")]
        public bool PCShowName {
            get {
                return (bool) this["PCShowName"];
            }

            set {
                this["PCShowName"] = value;
                this.RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("True")]
        public bool RadarCompassMode {
            get {
                return (bool) this["RadarCompassMode"];
            }

            set {
                this["RadarCompassMode"] = value;
                this.RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("600")]
        public int RadarWidgetHeight {
            get {
                return (int) this["RadarWidgetHeight"];
            }

            set {
                this["RadarWidgetHeight"] = value;
                this.RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("100")]
        public int RadarWidgetLeft {
            get {
                return (int) this["RadarWidgetLeft"];
            }

            set {
                this["RadarWidgetLeft"] = value;
                this.RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("100")]
        public int RadarWidgetTop {
            get {
                return (int) this["RadarWidgetTop"];
            }

            set {
                this["RadarWidgetTop"] = value;
                this.RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("1.0")]
        public string RadarWidgetUIScale {
            get {
                return (string) this["RadarWidgetUIScale"];
            }

            set {
                this["RadarWidgetUIScale"] = value;
                this.RaisePropertyChanged();
            }
        }

        [ApplicationScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue(
            @"<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfString xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <string>0.8</string>
  <string>0.9</string>
  <string>1.0</string>
  <string>1.1</string>
  <string>1.2</string>
  <string>1.3</string>
  <string>1.4</string>
  <string>1.5</string>
</ArrayOfString>")]
        public StringCollection RadarWidgetUIScaleList {
            get {
                return (StringCollection) this["RadarWidgetUIScaleList"];
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("600")]
        public int RadarWidgetWidth {
            get {
                return (int) this["RadarWidgetWidth"];
            }

            set {
                this["RadarWidgetWidth"] = value;
                this.RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("False")]
        public bool ShowEntityDebug {
            get {
                return (bool) this["ShowEntityDebug"];
            }

            set {
                this["ShowEntityDebug"] = value;
                this.RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("True")]
        public bool ShowRadarWidgetOnLoad {
            get {
                return (bool) this["ShowRadarWidgetOnLoad"];
            }

            set {
                this["ShowRadarWidgetOnLoad"] = value;
                this.RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("True")]
        public bool ShowTitleOnWidgets {
            get {
                return (bool) this["ShowTitleOnWidgets"];
            }

            set {
                this["ShowTitleOnWidgets"] = value;
                this.RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("False")]
        public bool WidgetClickThroughEnabled {
            get {
                return (bool) this["WidgetClickThroughEnabled"];
            }

            set {
                this["WidgetClickThroughEnabled"] = value;
                this.RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("0.7")]
        public string WidgetOpacity {
            get {
                return (string) this["WidgetOpacity"];
            }

            set {
                this["WidgetOpacity"] = value;
                this.RaisePropertyChanged();
            }
        }

        [ApplicationScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue(
            @"<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfString xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <string>0.5</string>
  <string>0.6</string>
  <string>0.7</string>
  <string>0.8</string>
  <string>0.9</string>
  <string>1.0</string>
</ArrayOfString>")]
        public StringCollection WidgetOpacityList {
            get {
                return (StringCollection) this["WidgetOpacityList"];
            }

            set {
                this["WidgetOpacityList"] = value;
                this.RaisePropertyChanged();
            }
        }

        public new void Reset() {
            this.DefaultSettings();
            foreach (var key in Constants.Settings) {
                SettingsProperty settingsProperty = Default.Properties[key];
                if (settingsProperty == null) {
                    continue;
                }

                var value = settingsProperty.DefaultValue.ToString();
                this.SetValue(key, value, CultureInfo.InvariantCulture);
            }
        }

        public override void Save() {
            // this call to default settings only ensures we keep the settings we want and delete the ones we don't (old)
            this.DefaultSettings();
            this.SaveSettingsNode();
            this.SaveFilters();

            // I would make a function for each node itself; other examples such as log/event would showcase this
            Constants.XSettings.Save(Path.Combine(Common.Constants.PluginsSettingsPath, "FFXIVAPP.Plugin.Radar.xml"));
        }

        public void SetValue(string key, string value, CultureInfo cultureInfo) {
            try {
                var type = Default[key].GetType().Name;
                switch (type) {
                    case "Boolean":
                        Default[key] = bool.Parse(value);
                        break;
                    case "Color":
                        var cc = new ColorConverter();
                        object color = cc.ConvertFrom(value);
                        Default[key] = color ?? Colors.Black;
                        break;
                    case "Double":
                        Default[key] = double.Parse(value, cultureInfo);
                        break;
                    case "Font":
                        var fc = new FontConverter();
                        object font = fc.ConvertFromString(value);
                        Default[key] = font ?? new Font(new FontFamily("Microsoft Sans Serif"), 12);
                        break;
                    case "Int32":
                        Default[key] = int.Parse(value, cultureInfo);
                        break;
                    default:
                        Default[key] = value;
                        break;
                }
            }
            catch (Exception ex) {
                Logging.Log(Logger, new LogItem(ex, true));
            }

            this.RaisePropertyChanged(key);
        }

        private void DefaultSettings() {
            Constants.Settings.Clear();
            Constants.Settings.Add("RadarWidgetWidth");
            Constants.Settings.Add("RadarWidgetHeight");
            Constants.Settings.Add("RadarWidgetUIScale");
            Constants.Settings.Add("ShowRadarWidgetOnLoad");
            Constants.Settings.Add("RadarWidgetTop");
            Constants.Settings.Add("RadarWidgetLeft");
            Constants.Settings.Add("WidgetClickThroughEnabled");
            Constants.Settings.Add("ShowTitleOnWidgets");
            Constants.Settings.Add("WidgetOpacity");
            Constants.Settings.Add("RadarCompassMode");
            Constants.Settings.Add("FilterRadarItems");
            Constants.Settings.Add("ShowEntityDebug");

            Constants.Settings.Add("PCShow");
            Constants.Settings.Add("PCShowName");
            Constants.Settings.Add("PCShowHPPercent");
            Constants.Settings.Add("PCShowJob");
            Constants.Settings.Add("PCShowDistance");
            Constants.Settings.Add("PCFontSize");
            Constants.Settings.Add("PCFontColor");

            #region NPC Options

            Constants.Settings.Add("NPCShow");
            Constants.Settings.Add("NPCShowName");
            Constants.Settings.Add("NPCShowHPPercent");
            Constants.Settings.Add("NPCShowDistance");
            Constants.Settings.Add("NPCFontSize");
            Constants.Settings.Add("NPCFontColor");

            #endregion

            #region Monster Options

            Constants.Settings.Add("MonsterShow");
            Constants.Settings.Add("MonsterShowName");
            Constants.Settings.Add("MonsterShowHPPercent");
            Constants.Settings.Add("MonsterShowDistance");
            Constants.Settings.Add("MonsterFontSize");
            Constants.Settings.Add("MonsterFontColor");

            Constants.Settings.Add("MonsterShowRankColor");
            Constants.Settings.Add("MonsterFontColorBRank");
            Constants.Settings.Add("MonsterFontColorARank");
            Constants.Settings.Add("MonsterFontColorSRank");

            #endregion

            #region Gathering Options

            Constants.Settings.Add("GatheringShow");
            Constants.Settings.Add("GatheringShowName");
            Constants.Settings.Add("GatheringShowHPPercent");
            Constants.Settings.Add("GatheringShowDistance");
            Constants.Settings.Add("GatheringFontSize");
            Constants.Settings.Add("GatheringFontColor");

            #endregion

            #region Other Options

            Constants.Settings.Add("OtherShow");
            Constants.Settings.Add("OtherShowName");
            Constants.Settings.Add("OtherShowHPPercent");
            Constants.Settings.Add("OtherShowDistance");
            Constants.Settings.Add("OtherFontSize");
            Constants.Settings.Add("OtherFontColor");

            #endregion
        }

        private void RaisePropertyChanged([CallerMemberName] string caller = "") {
            this.PropertyChanged(this, new PropertyChangedEventArgs(caller));
        }

        private void SaveFilters() {
            if (Constants.XFilters == null) {
                return;
            }

            Constants.XFilters.Descendants("Filter").Where(node => PluginViewModel.Instance.Filters.All(e => e.Key.ToString() != node.Attribute("Key").Value)).Remove();
            IEnumerable<XElement> xElements = Constants.XFilters.Descendants().Elements("Filter");
            XElement[] enumerable = xElements as XElement[] ?? xElements.ToArray();
            foreach (RadarFilterItem filter in PluginViewModel.Instance.Filters) {
                var xKey = filter.Key;
                var xLevel = filter.Level.ToString();
                var xType = filter.Type;
                List<XValuePair> keyPairList = new List<XValuePair> {
                    new XValuePair {
                        Key = "Level",
                        Value = xLevel,
                    },
                    new XValuePair {
                        Key = "Type",
                        Value = xType,
                    },
                };
                XElement element = enumerable.FirstOrDefault(e => e.Attribute("Key").Value == filter.Key);
                if (element == null) {
                    XmlHelper.SaveXmlNode(Constants.XFilters, "Filters", "Filter", xKey, keyPairList);
                }
                else {
                    XElement xKeyElement = element.Element("Key");
                    if (xKeyElement != null) {
                        xKeyElement.Value = xKey;
                    }

                    XElement xLevelElement = element.Element("Level");
                    if (xLevelElement != null) {
                        xLevelElement.Value = xLevel;
                    }

                    XElement xTypeElement = element.Element("Type");
                    if (xTypeElement != null) {
                        xTypeElement.Value = xType;
                    }
                }
            }

            Constants.XFilters.Save(Path.Combine(Common.Constants.PluginsSettingsPath, "FFXIVAPP.Plugin.Radar.Filters.xml"));
        }

        private void SaveSettingsNode() {
            if (Constants.XSettings == null) {
                return;
            }

            IEnumerable<XElement> xElements = Constants.XSettings.Descendants().Elements("Setting");
            XElement[] enumerable = xElements as XElement[] ?? xElements.ToArray();
            foreach (var setting in Constants.Settings) {
                XElement element = enumerable.FirstOrDefault(e => e.Attribute("Key").Value == setting);
                var xKey = setting;
                if (Default[xKey] == null) {
                    continue;
                }

                if (element == null) {
                    var xValue = Default[xKey].ToString();
                    List<XValuePair> keyPairList = new List<XValuePair> {
                        new XValuePair {
                            Key = "Value",
                            Value = xValue,
                        },
                    };
                    XmlHelper.SaveXmlNode(Constants.XSettings, "Settings", "Setting", xKey, keyPairList);
                }
                else {
                    XElement xElement = element.Element("Value");
                    if (xElement != null) {
                        xElement.Value = Default[setting].ToString();
                    }
                }
            }
        }
    }
}