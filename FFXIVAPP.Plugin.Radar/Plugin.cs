// FFXIVAPP.Plugin.Radar ~ Plugin.cs
// 
// Copyright © 2007 - 2016 Ryan Wilson - All Rights Reserved
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
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using FFXIVAPP.Common.Events;
using FFXIVAPP.Common.Helpers;
using FFXIVAPP.Common.Utilities;
using FFXIVAPP.IPluginInterface;
using FFXIVAPP.Memory.Core.Enums;
using FFXIVAPP.Plugin.Radar.Helpers;
using FFXIVAPP.Plugin.Radar.Models;
using FFXIVAPP.Plugin.Radar.Properties;
using NLog;

namespace FFXIVAPP.Plugin.Radar
{
    [Export(typeof (IPlugin))]
    public class Plugin : IPlugin, INotifyPropertyChanged
    {
        private IPluginHost _host;
        private Dictionary<string, string> _locale;
        private string _name;
        private MessageBoxResult _popupResult;

        public IPluginHost Host
        {
            get { return _host; }
            set { PHost = _host = value; }
        }

        public MessageBoxResult PopupResult
        {
            get { return _popupResult; }
            set
            {
                _popupResult = value;
                PluginViewModel.Instance.OnPopupResultChanged(new PopupResultEvent(value));
            }
        }

        public Dictionary<string, string> Locale
        {
            get { return _locale ?? (_locale = new Dictionary<string, string>()); }
            set
            {
                _locale = value;
                var locale = LocaleHelper.Update(Constants.CultureInfo);
                foreach (var resource in locale)
                {
                    try
                    {
                        if (_locale.ContainsKey(resource.Key))
                        {
                            _locale[resource.Key] = resource.Value;
                        }
                        else
                        {
                            _locale.Add(resource.Key, resource.Value);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logging.Log(LogManager.GetCurrentClassLogger(), "", ex);
                    }
                }
                PluginViewModel.Instance.Locale = _locale;
                PluginViewModel.Instance.RankedFilters.Clear();
                foreach (var rankedMonster in LocaleHelper.GetRankedMonsters())
                {
                    PluginViewModel.Instance.RankedFilters.Add(new RadarFilterItem(rankedMonster)
                    {
                        Level = 0,
                        Type = Actor.Type.Monster
                    });
                }
                RaisePropertyChanged();
            }
        }

        public string FriendlyName { get; set; }

        public string Name
        {
            get { return _name; }
            private set { PName = _name = value; }
        }

        public string Icon { get; private set; }
        public string Description { get; private set; }
        public string Copyright { get; private set; }
        public string Version { get; private set; }
        public string Notice { get; private set; }
        public Exception Trace { get; private set; }

        public void Initialize(IPluginHost pluginHost)
        {
            Host = pluginHost;
            FriendlyName = "Radar";
            Name = AssemblyHelper.Name;
            Icon = "Logo.png";
            Description = AssemblyHelper.Description;
            Copyright = AssemblyHelper.Copyright;
            Version = AssemblyHelper.Version.ToString();
            Notice = "";
        }

        public void Dispose(bool isUpdating = false)
        {
            EventSubscriber.UnSubscribe();
            /*
             * If the isUpdating is true it means the application will be force closing/killed.
             * You wil have to choose what you want to do in this case.
             * By default the settings class clears the settings object and recreates it; but if killed untimely it will not save.
             * 
             * Suggested use is to not save settings if updating. Other disposing events could happen based on your needs.
             */
            if (isUpdating)
            {
                return;
            }
            Settings.Default.Save();
        }

        public TabItem CreateTab()
        {
            Locale = LocaleHelper.Update(Constants.CultureInfo);
            var content = new ShellView();
            content.Loaded += ShellViewModel.Loaded;
            var tabItem = new TabItem
            {
                Header = Name,
                Content = content
            };
            //do your gui stuff here
            EventSubscriber.Subscribe();
            //content gives you access to the base xaml
            return tabItem;
        }

        #region Property Bindings

        public static IPluginHost PHost { get; private set; }
        public static string PName { get; private set; }

        #endregion

        #region Declarations

        #endregion

        #region Implementation of INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private void RaisePropertyChanged([CallerMemberName] string caller = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(caller));
        }

        #endregion
    }
}
