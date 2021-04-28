// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WinAPI.cs" company="SyndicatedLife">
//   Copyright© 2007 - 2021 Ryan Wilson &amp;lt;syndicated.life@gmail.com&amp;gt; (https://syndicated.life/)
//   Licensed under the MIT license. See LICENSE.md in the solution root for full license information.
// </copyright>
// <summary>
//   WinAPI.cs Implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FFXIVAPP.Plugin.Radar.Interop {
    using System;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows;
    using System.Windows.Interop;

    using FFXIVAPP.Common.Models;
    using FFXIVAPP.Common.Utilities;
    using FFXIVAPP.Plugin.Radar.Properties;

    using NLog;

    public static class WinAPI {
        public const uint EVENT_SYSTEM_FOREGROUND = 3;

        public const uint WINEVENT_OUTOFCONTEXT = 0;

        private const int GWL_EXSTYLE = -20;

        private const int WS_EX_TRANSPARENT = 0x00000020;

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public delegate void WinEventDelegate(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime);

        public static string GetActiveWindowTitle() {
            const int nChars = 256;
            IntPtr handle = IntPtr.Zero;
            var Buff = new StringBuilder(nChars);
            handle = GetForegroundWindow();
            return GetWindowText(handle, Buff, nChars) > 0
                       ? Buff.ToString()
                       : string.Empty;
        }

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("user32.dll")]
        public static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax, IntPtr hmodWinEventProc, WinEventDelegate lpfnWinEventProc, uint idProcess, uint idThread, uint dwFlags);

        public static void ToggleClickThrough(Window window) {
            try {
                IntPtr hWnd = new WindowInteropHelper(window).Handle;
                if (Settings.Default.WidgetClickThroughEnabled) {
                    SetWindowTransparent(hWnd);
                }
                else {
                    SetWindowLayered(hWnd);
                }
            }
            catch (Exception ex) {
                Logging.Log(Logger, new LogItem(ex, true));
            }
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hwnd, int index);

        private static void SetWindowLayered(IntPtr hwnd) {
            var extendedStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
            extendedStyle &= ~WS_EX_TRANSPARENT;
            SetWindowLong(hwnd, GWL_EXSTYLE, extendedStyle);
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int SetWindowLong(IntPtr hwnd, int index, int newStyle);

        private static void SetWindowTransparent(IntPtr hwnd) {
            var extendedStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
            SetWindowLong(hwnd, GWL_EXSTYLE, extendedStyle | WS_EX_TRANSPARENT);
        }
    }
}