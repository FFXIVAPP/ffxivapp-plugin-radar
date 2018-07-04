// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ShellViewModel.cs" company="SyndicatedLife">
//   Copyright(c) 2018 Ryan Wilson &amp;lt;syndicated.life@gmail.com&amp;gt; (http://syndicated.life/)
//   Licensed under the MIT license. See LICENSE.md in the solution root for full license information.
// </copyright>
// <summary>
//   ShellViewModel.cs Implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FFXIVAPP.Plugin.Radar {
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;

    using FFXIVAPP.Plugin.Radar.Interop;
    using FFXIVAPP.Plugin.Radar.Properties;

    public sealed class ShellViewModel : INotifyPropertyChanged {
        private static Lazy<ShellViewModel> _instance = new Lazy<ShellViewModel>(() => new ShellViewModel());

        public ShellViewModel() {
            Initializer.LoadSettings();
            Initializer.LoadFilters();
            Initializer.SetGatheringNodes();
            Initializer.SetupWindowTopMost();
            Settings.Default.PropertyChanged += DefaultOnPropertyChanged;
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public static ShellViewModel Instance {
            get {
                return _instance.Value;
            }
        }

        internal static void Loaded(object sender, RoutedEventArgs e) {
            ShellView.View.Loaded -= Loaded;
        }

        private static void DefaultOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs) {
            var propertyName = propertyChangedEventArgs.PropertyName;
            switch (propertyName) {
                case "WidgetClickThroughEnabled":
                    WinAPI.ToggleClickThrough(Widgets.Instance.RadarWidget);
                    break;
                case "RadarWidgetUIScale":
                    try {
                        Settings.Default.RadarWidgetWidth = (int) (600 * double.Parse(Settings.Default.RadarWidgetUIScale));
                        Settings.Default.RadarWidgetHeight = (int) (600 * double.Parse(Settings.Default.RadarWidgetUIScale));
                    }
                    catch (Exception) {
                        Settings.Default.RadarWidgetWidth = 600;
                        Settings.Default.RadarWidgetHeight = 600;
                    }

                    break;
            }
        }

        private void RaisePropertyChanged([CallerMemberName] string caller = "") {
            this.PropertyChanged(this, new PropertyChangedEventArgs(caller));
        }
    }
}