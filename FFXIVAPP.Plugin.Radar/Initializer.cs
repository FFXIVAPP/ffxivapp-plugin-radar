// FFXIVAPP.Plugin.Radar
// Initializer.cs
// 
// Created by Ryan Wilson.
// 
// Copyright © 2014 - 2014 Ryan Wilson - All Rights Reserved

using System;
using System.Globalization;
using System.Xml.Linq;
using FFXIVAPP.Plugin.Radar.Helpers;
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

        public static void SetupWindowTopMost()
        {
            WidgetTopMostHelper.HookWidgetTopMost();
        }
    }
}
