// FFXIVAPP.Plugin.Radar
// MainViewModel.cs
// 
// Copyright © 2007 - 2015 Ryan Wilson - All Rights Reserved
// 
// Redistribution and use in source and binary forms, with or without 
// modification, are permitted provided that the following conditions are met: 
// 
//  * Redistributions of source code must retain the above copyright notice, 
//    this list of conditions and the following disclaimer. 
//  * Redistributions in binary form must reproduce the above copyright 
//    notice, this list of conditions and the following disclaimer in the 
//    documentation and/or other materials provided with the distribution. 
//  * Neither the name of SyndicatedLife nor the names of its contributors may 
//    be used to endorse or promote products derived from this software 
//    without specific prior written permission. 
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" 
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE 
// IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE 
// ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE 
// LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR 
// CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF 
// SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS 
// INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN 
// CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
// ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
// POSSIBILITY OF SUCH DAMAGE. 

using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using FFXIVAPP.Common.Core.Memory.Enums;
using FFXIVAPP.Common.RegularExpressions;
using FFXIVAPP.Common.Utilities;
using FFXIVAPP.Common.ViewModelBase;
using FFXIVAPP.Plugin.Radar.Models;
using FFXIVAPP.Plugin.Radar.Properties;
using FFXIVAPP.Plugin.Radar.Views;
using NLog;

namespace FFXIVAPP.Plugin.Radar.ViewModels
{
    internal sealed class MainViewModel : INotifyPropertyChanged
    {
        #region Property Bindings

        private static MainViewModel _instance;

        public static MainViewModel Instance
        {
            get { return _instance ?? (_instance = new MainViewModel()); }
        }

        #endregion

        #region Declarations

        public ICommand AddOrUpdateFilterCommand { get; private set; }
        public ICommand DeleteFilterCommand { get; private set; }
        public ICommand FilterSelectionCommand { get; private set; }
        public ICommand ResetRadarWidgetCommand { get; private set; }
        public ICommand OpenRadarWidgetCommand { get; private set; }

        #endregion

        #region Loading Functions

        #endregion

        public MainViewModel()
        {
            AddOrUpdateFilterCommand = new DelegateCommand(AddOrUpdateFilter);
            DeleteFilterCommand = new DelegateCommand(DeleteFilter);
            FilterSelectionCommand = new DelegateCommand(FilterSelection);
            ResetRadarWidgetCommand = new DelegateCommand(ResetRadarWidget);
            OpenRadarWidgetCommand = new DelegateCommand(OpenRadarWidget);
        }

        #region Utility Functions

        /// <summary>
        /// </summary>
        /// <param name="listView"> </param>
        /// <param name="key"> </param>
        private static string GetValueBySelectedItem(Selector listView, string key)
        {
            var type = listView.SelectedItem.GetType();
            var property = type.GetProperty(key);
            return property.GetValue(listView.SelectedItem, null)
                           .ToString();
        }

        #endregion

        #region Command Bindings

        /// <summary>
        /// </summary>
        private static void AddOrUpdateFilter()
        {
            var selectedKey = "";
            try
            {
                if (MainView.View.Filters.SelectedItems.Count == 1)
                {
                    selectedKey = GetValueBySelectedItem(MainView.View.Filters, "Key");
                }
            }
            catch (Exception ex)
            {
                Logging.Log(LogManager.GetCurrentClassLogger(), "", ex);
            }
            if (MainView.View.TKey.Text.Trim() == "" || MainView.View.TLevel.Text.Trim() == "")
            {
                return;
            }
            var xKey = MainView.View.TKey.Text;
            if (!SharedRegEx.IsValidRegex(xKey))
            {
                return;
            }
            var radarFilterItem = new RadarFilterItem(xKey);
            int level;
            if (Int32.TryParse(MainView.View.TLevel.Text, out level))
            {
                radarFilterItem.Level = level;
            }
            switch (MainView.View.TType.Text)
            {
                case "PC":
                    radarFilterItem.Type = Actor.Type.PC;
                    break;
                case "Monster":
                    radarFilterItem.Type = Actor.Type.Monster;
                    break;
                case "NPC":
                    radarFilterItem.Type = Actor.Type.NPC;
                    break;
                case "Aetheryte":
                    radarFilterItem.Type = Actor.Type.Aetheryte;
                    break;
                case "Gathering":
                    radarFilterItem.Type = Actor.Type.Gathering;
                    break;
                case "Minion":
                    radarFilterItem.Type = Actor.Type.Minion;
                    break;
            }
            if (String.IsNullOrWhiteSpace(selectedKey))
            {
                PluginViewModel.Instance.Filters.Add(radarFilterItem);
            }
            else
            {
                var index = PluginViewModel.Instance.Filters.TakeWhile(@event => @event.Key != selectedKey)
                                           .Count();
                PluginViewModel.Instance.Filters[index] = radarFilterItem;
            }
            MainView.View.Filters.UnselectAll();
            MainView.View.TKey.Text = "";
        }

        /// <summary>
        /// </summary>
        private static void DeleteFilter()
        {
            string selectedKey;
            try
            {
                selectedKey = GetValueBySelectedItem(MainView.View.Filters, "Key");
            }
            catch (Exception ex)
            {
                Logging.Log(LogManager.GetCurrentClassLogger(), "", ex);
                return;
            }
            var index = PluginViewModel.Instance.Filters.TakeWhile(@event => @event.Key.ToString() != selectedKey)
                                       .Count();
            PluginViewModel.Instance.Filters.RemoveAt(index);
        }

        /// <summary>
        /// </summary>
        private static void FilterSelection()
        {
            if (MainView.View.Filters.SelectedItems.Count != 1)
            {
                return;
            }
            if (MainView.View.Filters.SelectedIndex < 0)
            {
                return;
            }
            MainView.View.TKey.Text = GetValueBySelectedItem(MainView.View.Filters, "Key");
            MainView.View.TLevel.Text = GetValueBySelectedItem(MainView.View.Filters, "Level");
            MainView.View.TType.Text = GetValueBySelectedItem(MainView.View.Filters, "Type");
        }

        public void ResetRadarWidget()
        {
            Settings.Default.RadarWidgetUIScale = Settings.Default.Properties["RadarWidgetUIScale"].DefaultValue.ToString();
            Settings.Default.RadarWidgetTop = Int32.Parse(Settings.Default.Properties["RadarWidgetTop"].DefaultValue.ToString());
            Settings.Default.RadarWidgetLeft = Int32.Parse(Settings.Default.Properties["RadarWidgetLeft"].DefaultValue.ToString());
            Settings.Default.RadarWidgetHeight = Int32.Parse(Settings.Default.Properties["RadarWidgetHeight"].DefaultValue.ToString());
            Settings.Default.RadarWidgetWidth = Int32.Parse(Settings.Default.Properties["RadarWidgetWidth"].DefaultValue.ToString());
        }

        public void OpenRadarWidget()
        {
            Settings.Default.ShowRadarWidgetOnLoad = true;
            Widgets.Instance.ShowRadarWidget();
        }

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
