// FFXIVAPP.Plugin.Radar
// Initializer.cs
// 
// Copyright © 2007 - 2014 Ryan Wilson - All Rights Reserved
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
using System.Globalization;
using System.Xml.Linq;
using FFXIVAPP.Common.Core.Memory.Enums;
using FFXIVAPP.Plugin.Radar.Helpers;
using FFXIVAPP.Plugin.Radar.Models;
using FFXIVAPP.Plugin.Radar.Properties;

namespace FFXIVAPP.Plugin.Radar
{
    internal static class Initializer
    {
        #region Declarations

        #endregion

        /// <summary>
        /// </summary>
        public static void LoadSettings()
        {
            if (Constants.XSettings != null)
            {
                Settings.Default.Reset();
                foreach (var xElement in Constants.XSettings.Descendants()
                                                  .Elements("Setting"))
                {
                    var xKey = (string) xElement.Attribute("Key");
                    var xValue = (string) xElement.Element("Value");
                    if (String.IsNullOrWhiteSpace(xKey) || String.IsNullOrWhiteSpace(xValue))
                    {
                        return;
                    }
                    if (Constants.Settings.Contains(xKey))
                    {
                        Settings.Default.SetValue(xKey, xValue, CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        Constants.Settings.Add(xKey);
                    }
                }
            }
        }

        /// <summary>
        /// </summary>
        public static void LoadFilters()
        {
            if (Constants.XFilters != null)
            {
                foreach (var xElement in Constants.XFilters.Descendants()
                                                  .Elements("Filter"))
                {
                    var xKey = (string) xElement.Attribute("Key");
                    var xLevel = (string) xElement.Element("Level");
                    var xType = (string) xElement.Element("Type");
                    if (String.IsNullOrWhiteSpace(xKey) || String.IsNullOrWhiteSpace(xType))
                    {
                        return;
                    }
                    int level;
                    Int32.TryParse(xLevel, out level);
                    var radarFilterItem = new RadarFilterItem
                    {
                        Key = xKey,
                        Level = level
                    };
                    switch (xType)
                    {
                        case "PC":
                            radarFilterItem.Type = Actor.Type.PC;
                            break;
                        case "Monster":
                            radarFilterItem.Type = Actor.Type.Monster;
                            break;
                        case "NPC":
                            radarFilterItem.Type = Actor.Type.NPC;
                            break;
                        case "Aetheryte":
                            radarFilterItem.Type = Actor.Type.Aetheryte;
                            break;
                        case "Gathering":
                            radarFilterItem.Type = Actor.Type.Gathering;
                            break;
                        case "Minion":
                            radarFilterItem.Type = Actor.Type.Minion;
                            break;
                    }
                    Constants.Filters.Add(radarFilterItem);
                }
            }
        }

        public static void SetupWindowTopMost()
        {
            WidgetTopMostHelper.HookWidgetTopMost();
        }
    }
}
