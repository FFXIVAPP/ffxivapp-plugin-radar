// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainViewModel.cs" company="SyndicatedLife">
//   Copyright© 2007 - 2021 Ryan Wilson &amp;lt;syndicated.life@gmail.com&amp;gt; (https://syndicated.life/)
//   Licensed under the MIT license. See LICENSE.md in the solution root for full license information.
// </copyright>
// <summary>
//   MainViewModel.cs Implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FFXIVAPP.Plugin.Radar.ViewModels {
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;

    using FFXIVAPP.Common.Models;
    using FFXIVAPP.Common.RegularExpressions;
    using FFXIVAPP.Common.Utilities;
    using FFXIVAPP.Common.ViewModelBase;
    using FFXIVAPP.Plugin.Radar.Models;
    using FFXIVAPP.Plugin.Radar.Properties;
    using FFXIVAPP.Plugin.Radar.Views;

    using NLog;

    internal sealed class MainViewModel : INotifyPropertyChanged {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private static Lazy<MainViewModel> _instance = new Lazy<MainViewModel>(() => new MainViewModel());

        public MainViewModel() {
            this.AddOrUpdateFilterCommand = new DelegateCommand(AddOrUpdateFilter);
            this.DeleteFilterCommand = new DelegateCommand(DeleteFilter);
            this.FilterSelectionCommand = new DelegateCommand(FilterSelection);
            this.ResetRadarWidgetCommand = new DelegateCommand(this.ResetRadarWidget);
            this.OpenRadarWidgetCommand = new DelegateCommand(this.OpenRadarWidget);
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public static MainViewModel Instance {
            get {
                return _instance.Value;
            }
        }

        public ICommand AddOrUpdateFilterCommand { get; private set; }

        public ICommand DeleteFilterCommand { get; private set; }

        public ICommand FilterSelectionCommand { get; private set; }

        public ICommand OpenRadarWidgetCommand { get; private set; }

        public ICommand ResetRadarWidgetCommand { get; private set; }

        public void OpenRadarWidget() {
            Settings.Default.ShowRadarWidgetOnLoad = true;
            Widgets.Instance.ShowRadarWidget();
        }

        public void ResetRadarWidget() {
            Settings.Default.RadarWidgetUIScale = Settings.Default.Properties["RadarWidgetUIScale"].DefaultValue.ToString();
            Settings.Default.RadarWidgetTop = int.Parse(Settings.Default.Properties["RadarWidgetTop"].DefaultValue.ToString());
            Settings.Default.RadarWidgetLeft = int.Parse(Settings.Default.Properties["RadarWidgetLeft"].DefaultValue.ToString());
            Settings.Default.RadarWidgetHeight = int.Parse(Settings.Default.Properties["RadarWidgetHeight"].DefaultValue.ToString());
            Settings.Default.RadarWidgetWidth = int.Parse(Settings.Default.Properties["RadarWidgetWidth"].DefaultValue.ToString());
        }

        /// <summary>
        /// </summary>
        private static void AddOrUpdateFilter() {
            var selectedKey = string.Empty;
            try {
                if (MainView.View.Filters.SelectedItems.Count == 1) {
                    selectedKey = GetValueBySelectedItem(MainView.View.Filters, "Key");
                }
            }
            catch (Exception ex) {
                Logging.Log(Logger, new LogItem(ex, true));
            }

            if (MainView.View.TKey.Text.Trim() == string.Empty || MainView.View.TLevel.Text.Trim() == string.Empty) {
                return;
            }

            var xKey = MainView.View.TKey.Text;
            if (!SharedRegEx.IsValidRegex(xKey)) {
                return;
            }

            var radarFilterItem = new RadarFilterItem(xKey);
            int level;
            if (int.TryParse(MainView.View.TLevel.Text, out level)) {
                radarFilterItem.Level = level;
            }

            radarFilterItem.Type = MainView.View.TType.Text;
            if (string.IsNullOrWhiteSpace(selectedKey)) {
                PluginViewModel.Instance.Filters.Add(radarFilterItem);
            }
            else {
                var index = PluginViewModel.Instance.Filters.TakeWhile(@event => @event.Key != selectedKey).Count();
                PluginViewModel.Instance.Filters[index] = radarFilterItem;
            }

            MainView.View.Filters.UnselectAll();
            MainView.View.TKey.Text = string.Empty;
        }

        /// <summary>
        /// </summary>
        private static void DeleteFilter() {
            string selectedKey;
            try {
                selectedKey = GetValueBySelectedItem(MainView.View.Filters, "Key");
            }
            catch (Exception ex) {
                Logging.Log(Logger, new LogItem(ex, true));
                return;
            }

            var index = PluginViewModel.Instance.Filters.TakeWhile(@event => @event.Key.ToString() != selectedKey).Count();
            PluginViewModel.Instance.Filters.RemoveAt(index);
        }

        /// <summary>
        /// </summary>
        private static void FilterSelection() {
            if (MainView.View.Filters.SelectedItems.Count != 1) {
                return;
            }

            if (MainView.View.Filters.SelectedIndex < 0) {
                return;
            }

            MainView.View.TKey.Text = GetValueBySelectedItem(MainView.View.Filters, "Key");
            MainView.View.TLevel.Text = GetValueBySelectedItem(MainView.View.Filters, "Level");
            MainView.View.TType.Text = GetValueBySelectedItem(MainView.View.Filters, "Type");
        }

        /// <summary>
        /// </summary>
        /// <param name="listView"> </param>
        /// <param name="key"> </param>
        private static string GetValueBySelectedItem(Selector listView, string key) {
            Type type = listView.SelectedItem.GetType();
            PropertyInfo property = type.GetProperty(key);
            return property.GetValue(listView.SelectedItem, null).ToString();
        }

        private void RaisePropertyChanged([CallerMemberName] string caller = "") {
            this.PropertyChanged(this, new PropertyChangedEventArgs(caller));
        }
    }
}