// FFXIVAPP.Plugin.Radar
// MainViewModel.cs
// 
// Copyright © 2007 - 2014 Ryan Wilson - All Rights Reserved
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
using System.Runtime.CompilerServices;
using System.Windows.Input;
using FFXIVAPP.Common.ViewModelBase;
using FFXIVAPP.Plugin.Radar.Properties;

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

        public ICommand ResetRadarWidgetCommand { get; private set; }
        public ICommand OpenRadarWidgetCommand { get; private set; }

        #endregion

        #region Loading Functions

        #endregion

        public MainViewModel()
        {
            ResetRadarWidgetCommand = new DelegateCommand(ResetRadarWidget);
            OpenRadarWidgetCommand = new DelegateCommand(OpenRadarWidget);
        }

        #region Utility Functions

        #endregion

        #region Command Bindings

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
