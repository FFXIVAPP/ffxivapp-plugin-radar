// FFXIVAPP.Plugin.Radar ~ AboutViewModel.cs
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
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using FFXIVAPP.Common.Models;
using FFXIVAPP.Common.ViewModelBase;

namespace FFXIVAPP.Plugin.Radar.ViewModels
{
    internal sealed class AboutViewModel : INotifyPropertyChanged
    {
        public AboutViewModel()
        {
            OpenWebSiteCommand = new DelegateCommand(OpenWebSite);
        }

        #region Declarations

        public DelegateCommand OpenWebSiteCommand { get; private set; }

        #endregion

        #region Command Bindings

        public void OpenWebSite()
        {
            try
            {
                Process.Start("https://github.com/icehunter/ffxivapp-plugin-radar");
            }
            catch (Exception ex)
            {
                var popupContent = new PopupContent();
                popupContent.Title = PluginViewModel.Instance.Locale["app_WarningMessage"];
                popupContent.Message = ex.Message;
                Plugin.PHost.PopupMessage(Plugin.PName, popupContent);
            }
        }

        #endregion

        #region Property Bindings

        private static AboutViewModel _instance;

        public static AboutViewModel Instance
        {
            get { return _instance ?? (_instance = new AboutViewModel()); }
        }

        #endregion

        #region Loading Functions

        #endregion

        #region Utility Functions

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
