// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LogPublisher.cs" company="SyndicatedLife">
//   Copyright© 2007 - 2021 Ryan Wilson &amp;lt;syndicated.life@gmail.com&amp;gt; (https://syndicated.life/)
//   Licensed under the MIT license. See LICENSE.md in the solution root for full license information.
// </copyright>
// <summary>
//   LogPublisher.cs Implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FFXIVAPP.Plugin.Radar.Utilities {
    using System;

    using FFXIVAPP.Common.Models;
    using FFXIVAPP.Common.Utilities;

    using NLog;

    using Sharlayan.Core;

    public static class LogPublisher {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static void HandleCommands(ChatLogItem chatLogItem) { }

        public static void Process(ChatLogItem chatLogItem) {
            try { }
            catch (Exception ex) {
                Logging.Log(Logger, new LogItem(ex, true));
            }
        }
    }
}