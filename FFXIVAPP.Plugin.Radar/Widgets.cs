// FFXIVAPP.Plugin.Radar
// Widgets.cs
// 
// Created by Ryan Wilson.
// 
// Copyright © 2014 - 2014 Ryan Wilson - All Rights Reserved

using System;
using FFXIVAPP.Plugin.Radar.Windows;

namespace FFXIVAPP.Plugin.Radar
{
    public class Widgets
    {
        private static Widgets _instance;
        private RadarWidget _radarWidget;

        public static Widgets Instance
        {
            get { return _instance ?? (_instance = new Widgets()); }
            set { _instance = value; }
        }

        public RadarWidget RadarWidget
        {
            get { return _radarWidget ?? (_radarWidget = new RadarWidget()); }
            set { _radarWidget = value; }
        }

        public void ShowRadarWidget()
        {
            try
            {
                RadarWidget.Show();
            }
            catch (Exception ex)
            {
            }
        }
    }
}
