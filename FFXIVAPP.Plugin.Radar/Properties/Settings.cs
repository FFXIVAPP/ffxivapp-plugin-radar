// FFXIVAPP.Plugin.Radar
// Settings.cs
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
using NLog;
using ColorConverter = System.Windows.Media.ColorConverter;
using FontFamily = System.Drawing.FontFamily;

namespace FFXIVAPP.Plugin.Radar.Properties
{
    internal class Settings : ApplicationSettingsBase, INotifyPropertyChanged
    {
        private static Settings _default;

        public static Settings Default
        {
            get { return _default ?? (_default = ((Settings) (Synchronized(new Settings())))); }
        }

        public override void Save()
        {
            // this call to default settings only ensures we keep the settings we want and delete the ones we don't (old)
            DefaultSettings();
            SaveSettingsNode();
            SaveFilters();
            // I would make a function for each node itself; other examples such as log/event would showcase this
            Constants.XSettings.Save(Path.Combine(Common.Constants.PluginsSettingsPath, "FFXIVAPP.Plugin.Radar.xml"));
        }

        private void DefaultSettings()
        {
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

            #region PC Options

            Constants.Settings.Add("PCShow");
            Constants.Settings.Add("PCShowName");
            Constants.Settings.Add("PCShowHPPercent");
            Constants.Settings.Add("PCShowJob");
            Constants.Settings.Add("PCShowDistance");
            Constants.Settings.Add("PCFontSize");
            Constants.Settings.Add("PCFontColor");

            #endregion

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

            Constants.Settings.Add("MonsterShowRankOnly");
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

        public new void Reset()
        {
            DefaultSettings();
            foreach (var key in Constants.Settings)
            {
                var settingsProperty = Default.Properties[key];
                if (settingsProperty == null)
                {
                    continue;
                }
                var value = settingsProperty.DefaultValue.ToString();
                SetValue(key, value, CultureInfo.InvariantCulture);
            }
        }

        public void SetValue(string key, string value, CultureInfo cultureInfo)
        {
            try
            {
                var type = Default[key].GetType()
                                       .Name;
                switch (type)
                {
                    case "Boolean":
                        Default[key] = Boolean.Parse(value);
                        break;
                    case "Color":
                        var cc = new ColorConverter();
                        var color = cc.ConvertFrom(value);
                        Default[key] = color ?? Colors.Black;
                        break;
                    case "Double":
                        Default[key] = Double.Parse(value, cultureInfo);
                        break;
                    case "Font":
                        var fc = new FontConverter();
                        var font = fc.ConvertFromString(value);
                        Default[key] = font ?? new Font(new FontFamily("Microsoft Sans Serif"), 12);
                        break;
                    case "Int32":
                        Default[key] = Int32.Parse(value, cultureInfo);
                        break;
                    default:
                        Default[key] = value;
                        break;
                }
            }
            catch (Exception ex)
            {
                Logging.Log(LogManager.GetCurrentClassLogger(), "", ex);
            }
            RaisePropertyChanged(key);
        }

        #region Property Bindings (Settings)

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("0.7")]
        public string WidgetOpacity
        {
            get { return ((string) (this["WidgetOpacity"])); }
            set
            {
                this["WidgetOpacity"] = value;
                RaisePropertyChanged();
            }
        }

        [ApplicationScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue(@"<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfString xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <string>0.5</string>
  <string>0.6</string>
  <string>0.7</string>
  <string>0.8</string>
  <string>0.9</string>
  <string>1.0</string>
</ArrayOfString>")]
        public StringCollection WidgetOpacityList
        {
            get { return ((StringCollection) (this["WidgetOpacityList"])); }
            set
            {
                this["WidgetOpacityList"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("False")]
        public bool WidgetClickThroughEnabled
        {
            get { return ((bool) (this["WidgetClickThroughEnabled"])); }
            set
            {
                this["WidgetClickThroughEnabled"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("True")]
        public bool ShowTitleOnWidgets
        {
            get { return ((bool) (this["ShowTitleOnWidgets"])); }
            set
            {
                this["ShowTitleOnWidgets"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("True")]
        public bool RadarCompassMode
        {
            get { return ((bool) (this["RadarCompassMode"])); }
            set
            {
                this["RadarCompassMode"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("False")]
        public bool FilterRadarItems
        {
            get { return ((bool) (this["FilterRadarItems"])); }
            set
            {
                this["FilterRadarItems"] = value;
                RaisePropertyChanged();
            }
        }

        [ApplicationScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue(@"<?xml version=""1.0"" encoding=""utf-16""?>
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
        public StringCollection FontSizeList
        {
            get { return ((StringCollection) (this["FontSizeList"])); }
            set
            {
                this["FontSizeList"] = value;
                RaisePropertyChanged();
            }
        }

        #region Radar Widget Settings

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("600")]
        public int RadarWidgetWidth
        {
            get { return ((int) (this["RadarWidgetWidth"])); }
            set
            {
                this["RadarWidgetWidth"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("600")]
        public int RadarWidgetHeight
        {
            get { return ((int) (this["RadarWidgetHeight"])); }
            set
            {
                this["RadarWidgetHeight"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("True")]
        public bool ShowRadarWidgetOnLoad
        {
            get { return ((bool) (this["ShowRadarWidgetOnLoad"])); }
            set
            {
                this["ShowRadarWidgetOnLoad"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("100")]
        public int RadarWidgetTop
        {
            get { return ((int) (this["RadarWidgetTop"])); }
            set
            {
                this["RadarWidgetTop"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("100")]
        public int RadarWidgetLeft
        {
            get { return ((int) (this["RadarWidgetLeft"])); }
            set
            {
                this["RadarWidgetLeft"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("1.0")]
        public string RadarWidgetUIScale
        {
            get { return ((string) (this["RadarWidgetUIScale"])); }
            set
            {
                this["RadarWidgetUIScale"] = value;
                RaisePropertyChanged();
            }
        }

        [ApplicationScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue(@"<?xml version=""1.0"" encoding=""utf-16""?>
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
        public StringCollection RadarWidgetUIScaleList
        {
            get { return ((StringCollection) (this["RadarWidgetUIScaleList"])); }
        }

        #endregion

        #region PC Options

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("True")]
        public bool PCShow
        {
            get { return ((bool) (this["PCShow"])); }
            set
            {
                this["PCShow"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("False")]
        public bool PCShowName
        {
            get { return ((bool) (this["PCShowName"])); }
            set
            {
                this["PCShowName"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("False")]
        public bool PCShowHPPercent
        {
            get { return ((bool) (this["PCShowHPPercent"])); }
            set
            {
                this["PCShowHPPercent"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("True")]
        public bool PCShowJob
        {
            get { return ((bool) (this["PCShowJob"])); }
            set
            {
                this["PCShowJob"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("False")]
        public bool PCShowDistance
        {
            get { return ((bool) (this["PCShowDistance"])); }
            set
            {
                this["PCShowDistance"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("12")]
        public string PCFontSize
        {
            get { return ((string) (this["PCFontSize"])); }
            set
            {
                this["PCFontSize"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("White")]
        public string PCFontColor
        {
            get { return ((string) (this["PCFontColor"])); }
            set
            {
                this["PCFontColor"] = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region NPC Options

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("False")]
        public bool NPCShow
        {
            get { return ((bool) (this["NPCShow"])); }
            set
            {
                this["NPCShow"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("False")]
        public bool NPCShowName
        {
            get { return ((bool) (this["NPCShowName"])); }
            set
            {
                this["NPCShowName"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("False")]
        public bool NPCShowHPPercent
        {
            get { return ((bool) (this["NPCShowHPPercent"])); }
            set
            {
                this["NPCShowHPPercent"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("False")]
        public bool NPCShowDistance
        {
            get { return ((bool) (this["NPCShowDistance"])); }
            set
            {
                this["NPCShowDistance"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("12")]
        public string NPCFontSize
        {
            get { return ((string) (this["NPCFontSize"])); }
            set
            {
                this["NPCFontSize"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("LimeGreen")]
        public string NPCFontColor
        {
            get { return ((string) (this["NPCFontColor"])); }
            set
            {
                this["NPCFontColor"] = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Monster Options

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("True")]
        public bool MonsterShow
        {
            get { return ((bool) (this["MonsterShow"])); }
            set
            {
                this["MonsterShow"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("True")]
        public bool MonsterShowName
        {
            get { return ((bool) (this["MonsterShowName"])); }
            set
            {
                this["MonsterShowName"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("False")]
        public bool MonsterShowHPPercent
        {
            get { return ((bool) (this["MonsterShowHPPercent"])); }
            set
            {
                this["MonsterShowHPPercent"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("False")]
        public bool MonsterShowDistance
        {
            get { return ((bool) (this["MonsterShowDistance"])); }
            set
            {
                this["MonsterShowDistance"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("12")]
        public string MonsterFontSize
        {
            get { return ((string) (this["MonsterFontSize"])); }
            set
            {
                this["MonsterFontSize"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("Red")]
        public string MonsterFontColor
        {
            get { return ((string) (this["MonsterFontColor"])); }
            set
            {
                this["MonsterFontColor"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("True")]
        public bool MonsterShowRankOnly
        {
            get { return ((bool) (this["MonsterShowRankOnly"])); }
            set
            {
                this["MonsterShowRankOnly"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("Red")]
        public string MonsterFontColorBRank
        {
            get { return ((string) (this["MonsterFontColorBRank"])); }
            set
            {
                this["MonsterFontColorBRank"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("Red")]
        public string MonsterFontColorARank
        {
            get { return ((string) (this["MonsterFontColorARank"])); }
            set
            {
                this["MonsterFontColorARank"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("Red")]
        public string MonsterFontColorSRank
        {
            get { return ((string) (this["MonsterFontColorSRank"])); }
            set
            {
                this["MonsterFontColorSRank"] = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Gathering Options

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("True")]
        public bool GatheringShow
        {
            get { return ((bool) (this["GatheringShow"])); }
            set
            {
                this["GatheringShow"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("True")]
        public bool GatheringShowName
        {
            get { return ((bool) (this["GatheringShowName"])); }
            set
            {
                this["GatheringShowName"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("False")]
        public bool GatheringShowHPPercent
        {
            get { return ((bool) (this["GatheringShowHPPercent"])); }
            set
            {
                this["GatheringShowHPPercent"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("False")]
        public bool GatheringShowDistance
        {
            get { return ((bool) (this["GatheringShowDistance"])); }
            set
            {
                this["GatheringShowDistance"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("12")]
        public string GatheringFontSize
        {
            get { return ((string) (this["GatheringFontSize"])); }
            set
            {
                this["GatheringFontSize"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("Orange")]
        public string GatheringFontColor
        {
            get { return ((string) (this["GatheringFontColor"])); }
            set
            {
                this["GatheringFontColor"] = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Other Options

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("True")]
        public bool OtherShow
        {
            get { return ((bool) (this["OtherShow"])); }
            set
            {
                this["OtherShow"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("True")]
        public bool OtherShowName
        {
            get { return ((bool) (this["OtherShowName"])); }
            set
            {
                this["OtherShowName"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("False")]
        public bool OtherShowHPPercent
        {
            get { return ((bool) (this["OtherShowHPPercent"])); }
            set
            {
                this["OtherShowHPPercent"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("False")]
        public bool OtherShowDistance
        {
            get { return ((bool) (this["OtherShowDistance"])); }
            set
            {
                this["OtherShowDistance"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("12")]
        public string OtherFontSize
        {
            get { return ((string) (this["OtherFontSize"])); }
            set
            {
                this["OtherFontSize"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("Yellow")]
        public string OtherFontColor
        {
            get { return ((string) (this["OtherFontColor"])); }
            set
            {
                this["OtherFontColor"] = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #endregion

        #region Implementation of INotifyPropertyChanged

        public new event PropertyChangedEventHandler PropertyChanged = delegate { };

        private void RaisePropertyChanged([CallerMemberName] string caller = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(caller));
        }

        #endregion

        #region Iterative Settings Saving

        private void SaveSettingsNode()
        {
            if (Constants.XSettings == null)
            {
                return;
            }
            var xElements = Constants.XSettings.Descendants()
                                     .Elements("Setting");
            var enumerable = xElements as XElement[] ?? xElements.ToArray();
            foreach (var setting in Constants.Settings)
            {
                var element = enumerable.FirstOrDefault(e => e.Attribute("Key")
                                                              .Value == setting);
                var xKey = setting;
                if (Default[xKey] == null)
                {
                    continue;
                }
                if (element == null)
                {
                    var xValue = Default[xKey].ToString();
                    var keyPairList = new List<XValuePair>
                    {
                        new XValuePair
                        {
                            Key = "Value",
                            Value = xValue
                        }
                    };
                    XmlHelper.SaveXmlNode(Constants.XSettings, "Settings", "Setting", xKey, keyPairList);
                }
                else
                {
                    var xElement = element.Element("Value");
                    if (xElement != null)
                    {
                        xElement.Value = Default[setting].ToString();
                    }
                }
            }
        }

        private void SaveFilters()
        {
            if (Constants.XFilters == null)
            {
                return;
            }
            Constants.XFilters.Descendants("Filter")
                     .Where(node => PluginViewModel.Instance.Filters.All(e => e.Key.ToString() != node.Attribute("Key")
                                                                                                      .Value))
                     .Remove();
            var xElements = Constants.XFilters.Descendants()
                                     .Elements("Filter");
            var enumerable = xElements as XElement[] ?? xElements.ToArray();
            foreach (var filter in PluginViewModel.Instance.Filters)
            {
                var xKey = filter.Key;
                var xLevel = filter.Level.ToString();
                var xType = filter.Type.ToString();
                var keyPairList = new List<XValuePair>
                {
                    new XValuePair
                    {
                        Key = "Level",
                        Value = xLevel
                    },
                    new XValuePair
                    {
                        Key = "Type",
                        Value = xType
                    }
                };
                var element = enumerable.FirstOrDefault(e => e.Attribute("Key")
                                                              .Value == filter.Key);
                if (element == null)
                {
                    XmlHelper.SaveXmlNode(Constants.XFilters, "Filters", "Filter", xKey, keyPairList);
                }
                else
                {
                    var xKeyElement = element.Element("Key");
                    if (xKeyElement != null)
                    {
                        xKeyElement.Value = xKey;
                    }
                    var xLevelElement = element.Element("Level");
                    if (xLevelElement != null)
                    {
                        xLevelElement.Value = xLevel;
                    }
                    var xTypeElement = element.Element("Type");
                    if (xTypeElement != null)
                    {
                        xTypeElement.Value = xType;
                    }
                }
            }
            Constants.XFilters.Save(Path.Combine(Common.Constants.PluginsSettingsPath, "FFXIVAPP.Plugin.Radar.Filters.xml"));
        }

        #endregion
    }
}
