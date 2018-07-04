// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Plugin.cs" company="SyndicatedLife">
//   Copyright(c) 2018 Ryan Wilson &amp;lt;syndicated.life@gmail.com&amp;gt; (http://syndicated.life/)
//   Licensed under the MIT license. See LICENSE.md in the solution root for full license information.
// </copyright>
// <summary>
//   Plugin.cs Implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FFXIVAPP.Plugin.Radar {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.Composition;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    using FFXIVAPP.Common.Events;
    using FFXIVAPP.Common.Helpers;
    using FFXIVAPP.Common.Models;
    using FFXIVAPP.Common.Utilities;
    using FFXIVAPP.IPluginInterface;
    using FFXIVAPP.Plugin.Radar.Helpers;
    using FFXIVAPP.Plugin.Radar.Models;
    using FFXIVAPP.Plugin.Radar.Properties;

    using NLog;

    [Export(typeof(IPlugin))]
    public class Plugin : IPlugin, INotifyPropertyChanged {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private IPluginHost _host;

        private Dictionary<string, string> _locale;

        private string _name;

        private MessageBoxResult _popupResult;

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public static IPluginHost PHost { get; private set; }

        public static string PName { get; private set; }

        public string Copyright { get; private set; }

        public string Description { get; private set; }

        public string FriendlyName { get; set; }

        public IPluginHost Host {
            get {
                return this._host;
            }

            set {
                PHost = this._host = value;
            }
        }

        public string Icon { get; private set; }

        public Dictionary<string, string> Locale {
            get {
                return this._locale ?? (this._locale = new Dictionary<string, string>());
            }

            set {
                this._locale = value;
                Dictionary<string, string> locale = LocaleHelper.Update(Constants.CultureInfo);
                foreach (KeyValuePair<string, string> resource in locale) {
                    try {
                        if (this._locale.ContainsKey(resource.Key)) {
                            this._locale[resource.Key] = resource.Value;
                        }
                        else {
                            this._locale.Add(resource.Key, resource.Value);
                        }
                    }
                    catch (Exception ex) {
                        Logging.Log(Logger, new LogItem(ex, true));
                    }
                }

                PluginViewModel.Instance.Locale = this._locale;
                PluginViewModel.Instance.RankedFilters.Clear();
                foreach (var rankedMonster in LocaleHelper.GetRankedMonsters()) {
                    PluginViewModel.Instance.RankedFilters.Add(
                        new RadarFilterItem(rankedMonster) {
                            Level = 0,
                            Type = "Monster"
                        });
                }

                this.RaisePropertyChanged();
            }
        }

        public string Name {
            get {
                return this._name;
            }

            private set {
                PName = this._name = value;
            }
        }

        public string Notice { get; private set; }

        public MessageBoxResult PopupResult {
            get {
                return this._popupResult;
            }

            set {
                this._popupResult = value;
                PluginViewModel.Instance.OnPopupResultChanged(new PopupResultEvent(value));
            }
        }

        public Exception Trace { get; private set; }

        public string Version { get; private set; }

        public TabItem CreateTab() {
            this.Locale = LocaleHelper.Update(Constants.CultureInfo);
            var content = new ShellView();
            content.Loaded += ShellViewModel.Loaded;
            var tabItem = new TabItem {
                Header = this.Name,
                Content = content
            };

            // do your gui stuff here
            EventSubscriber.Subscribe();

            // content gives you access to the base xaml
            return tabItem;
        }

        public void Dispose(bool isUpdating = false) {
            EventSubscriber.UnSubscribe();

            /*
                         * If the isUpdating is true it means the application will be force closing/killed.
                         * You wil have to choose what you want to do in this case.
                         * By default the settings class clears the settings object and recreates it; but if killed untimely it will not save.
                         * 
                         * Suggested use is to not save settings if updating. Other disposing events could happen based on your needs.
                         */
            if (isUpdating) {
                return;
            }

            Settings.Default.Save();
        }

        public void Initialize(IPluginHost pluginHost) {
            this.Host = pluginHost;
            this.FriendlyName = "Radar";
            this.Name = AssemblyHelper.Name;
            this.Icon = "Logo.png";
            this.Description = AssemblyHelper.Description;
            this.Copyright = AssemblyHelper.Copyright;
            this.Version = AssemblyHelper.Version.ToString();
            this.Notice = string.Empty;
        }

        private void RaisePropertyChanged([CallerMemberName] string caller = "") {
            this.PropertyChanged(this, new PropertyChangedEventArgs(caller));
        }
    }
}