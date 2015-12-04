// FFXIVAPP.Plugin.Radar ~ RadarWidget.xaml.cs
// 
// Copyright © 2007 - 2015 Ryan Wilson - All Rights Reserved
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.ComponentModel;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using FFXIVAPP.Common.Helpers;
using FFXIVAPP.Plugin.Radar.Interop;
using FFXIVAPP.Plugin.Radar.Properties;

namespace FFXIVAPP.Plugin.Radar.Windows
{
    /// <summary>
    ///     Interaction logic for RadarWidget.xaml
    /// </summary>
    public partial class RadarWidget
    {
        public static RadarWidget View;

        public RadarWidget()
        {
            View = this;
            InitializeComponent();
            View.SourceInitialized += delegate { WinAPI.ToggleClickThrough(this); };
        }

        private void RadarWidget_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (IsRendered)
            {
                return;
            }
            IsRendered = true;
            RefreshTimer.Elapsed += RefreshTimerTick;
            RefreshTimer.Start();
        }

        private void RefreshTimerTick(object sender, EventArgs e)
        {
            if (View.IsVisible)
            {
                DispatcherHelper.Invoke(() => View.RadarControl.Refresh());
            }
        }

        private void TitleBar_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void WidgetClose_OnClick(object sender, RoutedEventArgs e)
        {
            Settings.Default.ShowRadarWidgetOnLoad = false;
            Close();
        }

        private void RadarWidget_OnClosing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        #region Radar Declarations

        private readonly Timer RefreshTimer = new Timer(100);
        public bool IsRendered { get; set; }

        #endregion
    }
}
