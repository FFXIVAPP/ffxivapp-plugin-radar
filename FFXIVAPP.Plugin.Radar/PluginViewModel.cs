// FFXIVAPP.Plugin.Radar ~ PluginViewModel.cs
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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FFXIVAPP.Common.Events;
using FFXIVAPP.Common.Helpers;
using FFXIVAPP.Plugin.Radar.Models;

namespace FFXIVAPP.Plugin.Radar
{
    internal sealed class PluginViewModel : INotifyPropertyChanged
    {
        //used for global static properties

        public event EventHandler<PopupResultEvent> PopupResultChanged = delegate { };

        public void OnPopupResultChanged(PopupResultEvent e)
        {
            PopupResultChanged(this, e);
        }

        #region Property Bindings

        private static PluginViewModel _instance;
        public List<RadarFilterItem> RankedFilters = new List<RadarFilterItem>();
        private bool _enableHelpLabels;
        private ObservableCollection<RadarFilterItem> _filters;
        private Dictionary<string, string> _locale;

        public static PluginViewModel Instance
        {
            get { return _instance ?? (_instance = new PluginViewModel()); }
        }

        public Dictionary<string, string> Locale
        {
            get { return _locale ?? (_locale = new Dictionary<string, string>()); }
            set
            {
                _locale = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<RadarFilterItem> Filters
        {
            get { return _filters ?? (_filters = new ObservableCollection<RadarFilterItem>()); }
            set
            {
                if (_filters == null)
                {
                    _filters = new ObservableCollection<RadarFilterItem>();
                }
                _filters = value;
                RaisePropertyChanged();
            }
        }

        public static Dictionary<string, string> PluginInfo
        {
            get
            {
                var pluginInfo = new Dictionary<string, string>();
                pluginInfo.Add("Icon", "Logo.png");
                pluginInfo.Add("Name", AssemblyHelper.Name);
                pluginInfo.Add("Description", AssemblyHelper.Description);
                pluginInfo.Add("Copyright", AssemblyHelper.Copyright);
                pluginInfo.Add("Version", AssemblyHelper.Version.ToString());
                return pluginInfo;
            }
        }

        public bool EnableHelpLabels
        {
            get { return _enableHelpLabels; }
            set
            {
                _enableHelpLabels = value;
                RaisePropertyChanged();
            }
        }

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
