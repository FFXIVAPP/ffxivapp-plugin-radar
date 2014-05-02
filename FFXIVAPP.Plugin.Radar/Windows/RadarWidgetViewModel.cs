// FFXIVAPP.Plugin.Radar
// RadarWidgetViewModel.cs
// 
// Created by Ryan Wilson.
// 
// Copyright © 2014 - 2014 Ryan Wilson - All Rights Reserved

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FFXIVAPP.Plugin.Radar.Windows
{
    internal sealed class RadarWidgetViewModel : INotifyPropertyChanged
    {
        #region Property Bindings

        private static RadarWidgetViewModel _instance;

        public static RadarWidgetViewModel Instance
        {
            get { return _instance ?? (_instance = new RadarWidgetViewModel()); }
        }

        #endregion

        #region Declarations

        #endregion

        #region Loading Functions

        #endregion

        #region Utility Functions

        #endregion

        #region Command Bindings

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
