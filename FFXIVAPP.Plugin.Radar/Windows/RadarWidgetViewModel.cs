// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RadarWidgetViewModel.cs" company="SyndicatedLife">
//   Copyright© 2007 - 2021 Ryan Wilson &amp;lt;syndicated.life@gmail.com&amp;gt; (https://syndicated.life/)
//   Licensed under the MIT license. See LICENSE.md in the solution root for full license information.
// </copyright>
// <summary>
//   RadarWidgetViewModel.cs Implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FFXIVAPP.Plugin.Radar.Windows {
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    internal sealed class RadarWidgetViewModel : INotifyPropertyChanged {
        private static Lazy<RadarWidgetViewModel> _instance = new Lazy<RadarWidgetViewModel>(() => new RadarWidgetViewModel());

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public static RadarWidgetViewModel Instance {
            get {
                return _instance.Value;
            }
        }

        private void RaisePropertyChanged([CallerMemberName] string caller = "") {
            this.PropertyChanged(this, new PropertyChangedEventArgs(caller));
        }
    }
}