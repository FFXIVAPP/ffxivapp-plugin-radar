// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RadarWidget.xaml.cs" company="SyndicatedLife">
//   Copyright© 2007 - 2021 Ryan Wilson &amp;lt;syndicated.life@gmail.com&amp;gt; (https://syndicated.life/)
//   Licensed under the MIT license. See LICENSE.md in the solution root for full license information.
// </copyright>
// <summary>
//   RadarWidget.xaml.cs Implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FFXIVAPP.Plugin.Radar.Windows {
    using System;
    using System.ComponentModel;
    using System.Timers;
    using System.Windows;
    using System.Windows.Input;

    using FFXIVAPP.Common.Helpers;
    using FFXIVAPP.Plugin.Radar.Interop;
    using FFXIVAPP.Plugin.Radar.Properties;

    /// <summary>
    ///     Interaction logic for RadarWidget.xaml
    /// </summary>
    public partial class RadarWidget {
        public static RadarWidget View;

        private readonly Timer RefreshTimer = new Timer(100);

        public RadarWidget() {
            View = this;
            this.InitializeComponent();
            View.SourceInitialized += delegate {
                WinAPI.ToggleClickThrough(this);
            };
        }

        public bool IsRendered { get; set; }

        private void RadarWidget_OnClosing(object sender, CancelEventArgs e) {
            e.Cancel = true;
            this.Hide();
        }

        private void RadarWidget_OnLoaded(object sender, RoutedEventArgs e) {
            if (this.IsRendered) {
                return;
            }

            this.IsRendered = true;
            this.RefreshTimer.Elapsed += this.RefreshTimerTick;
            this.RefreshTimer.Start();
        }

        private void RefreshTimerTick(object sender, EventArgs e) {
            if (View.IsVisible) {
                DispatcherHelper.Invoke(() => View.RadarControl.Refresh());
            }
        }

        private void TitleBar_OnPreviewMouseDown(object sender, MouseButtonEventArgs e) {
            if (Mouse.LeftButton == MouseButtonState.Pressed) {
                this.DragMove();
            }
        }

        private void WidgetClose_OnClick(object sender, RoutedEventArgs e) {
            Settings.Default.ShowRadarWidgetOnLoad = false;
            this.Close();
        }
    }
}