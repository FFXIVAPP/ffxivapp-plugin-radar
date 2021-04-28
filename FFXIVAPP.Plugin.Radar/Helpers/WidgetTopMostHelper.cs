// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WidgetTopMostHelper.cs" company="SyndicatedLife">
//   Copyright© 2007 - 2021 Ryan Wilson &amp;lt;syndicated.life@gmail.com&amp;gt; (https://syndicated.life/)
//   Licensed under the MIT license. See LICENSE.md in the solution root for full license information.
// </copyright>
// <summary>
//   WidgetTopMostHelper.cs Implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FFXIVAPP.Plugin.Radar.Helpers {
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Timers;
    using System.Windows;
    using System.Windows.Interop;
    using System.Windows.Threading;

    using FFXIVAPP.Common.Helpers;
    using FFXIVAPP.Common.Models;
    using FFXIVAPP.Common.RegularExpressions;
    using FFXIVAPP.Common.Utilities;
    using FFXIVAPP.Plugin.Radar.Interop;
    using FFXIVAPP.Plugin.Radar.Properties;

    using NLog;

    public static class WidgetTopMostHelper {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private static WinAPI.WinEventDelegate _delegate;

        private static IntPtr _mainHandleHook;

        private static WindowInteropHelper _radarWidgetInteropHelper;

        private static WindowInteropHelper RadarWidgetInteropHelper {
            get {
                return _radarWidgetInteropHelper ?? (_radarWidgetInteropHelper = new WindowInteropHelper(Widgets.Instance.RadarWidget));
            }
        }

        private static Timer SetWindowTimer { get; set; }

        public static void HookWidgetTopMost() {
            try {
                _delegate = BringWidgetsIntoFocus;
                _mainHandleHook = WinAPI.SetWinEventHook(WinAPI.EVENT_SYSTEM_FOREGROUND, WinAPI.EVENT_SYSTEM_FOREGROUND, IntPtr.Zero, _delegate, 0, 0, WinAPI.WINEVENT_OUTOFCONTEXT);
            }
            catch (Exception ex) {
                Logging.Log(Logger, new LogItem(ex, true));
            }

            SetWindowTimer = new Timer(1000);
            SetWindowTimer.Elapsed += SetWindowTimerOnElapsed;
            SetWindowTimer.Start();
        }

        private static void BringWidgetsIntoFocus(IntPtr hwineventhook, uint eventtype, IntPtr hwnd, int idobject, int idchild, uint dweventthread, uint dwmseventtime) {
            BringWidgetsIntoFocus(true);
        }

        private static void BringWidgetsIntoFocus(bool force = false) {
            try {
                IntPtr handle = WinAPI.GetForegroundWindow();
                var activeTitle = WinAPI.GetActiveWindowTitle();

                var stayOnTop = Application.Current.Windows.OfType<Window>().Any(w => w.Title == activeTitle) || Regex.IsMatch(activeTitle.ToUpper(), @"^(FINAL FANTASY |最终幻想)XIV", SharedRegEx.DefaultOptions);

                // If any of the windows are focused, don't try to hide any of them, or it'll prevent us from moving/closing them
                if (handle == RadarWidgetInteropHelper.Handle) {
                    return;
                }

                if (Settings.Default.ShowRadarWidgetOnLoad) {
                    ToggleTopMost(Widgets.Instance.RadarWidget, stayOnTop, force);
                }
            }
            catch (Exception ex) {
                Logging.Log(Logger, new LogItem(ex, true));
            }
        }

        private static void SetWindowTimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs) {
            DispatcherHelper.Invoke(() => BringWidgetsIntoFocus(), DispatcherPriority.Normal);
        }

        /// <summary>
        /// </summary>
        /// <param name="window"></param>
        /// <param name="stayOnTop"></param>
        /// <param name="force"></param>
        private static void ToggleTopMost(Window window, bool stayOnTop, bool force) {
            if (window.Topmost && stayOnTop && !force) {
                return;
            }

            window.Topmost = false;
            if (!stayOnTop) {
                if (window.IsVisible) {
                    window.Hide();
                }

                return;
            }

            window.Topmost = true;
            if (!window.IsVisible) {
                window.Show();
            }
        }
    }
}