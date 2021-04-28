// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Widgets.cs" company="SyndicatedLife">
//   Copyright© 2007 - 2021 Ryan Wilson &amp;lt;syndicated.life@gmail.com&amp;gt; (https://syndicated.life/)
//   Licensed under the MIT license. See LICENSE.md in the solution root for full license information.
// </copyright>
// <summary>
//   Widgets.cs Implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FFXIVAPP.Plugin.Radar {
    using System;

    using FFXIVAPP.Common.Models;
    using FFXIVAPP.Common.Utilities;
    using FFXIVAPP.Plugin.Radar.Windows;

    using NLog;

    public class Widgets {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private static Lazy<Widgets> _instance = new Lazy<Widgets>(() => new Widgets());

        private RadarWidget _radarWidget;

        public static Widgets Instance {
            get {
                return _instance.Value;
            }
        }

        public RadarWidget RadarWidget {
            get {
                return this._radarWidget ?? (this._radarWidget = new RadarWidget());
            }

            set {
                this._radarWidget = value;
            }
        }

        public void ShowRadarWidget() {
            try {
                this.RadarWidget.Show();
            }
            catch (Exception ex) {
                Logging.Log(Logger, new LogItem(ex, true));
            }
        }
    }
}