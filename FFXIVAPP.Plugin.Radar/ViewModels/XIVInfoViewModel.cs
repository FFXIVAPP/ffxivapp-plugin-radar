// FFXIVAPP.Plugin.Radar ~ XIVInfoViewModel.cs
// 
// Copyright © 2007 - 2017 Ryan Wilson - All Rights Reserved
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
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Timers;
using Sharlayan.Core;

namespace FFXIVAPP.Plugin.Radar.ViewModels
{
    public class XIVInfoViewModel : INotifyPropertyChanged
    {
        public XIVInfoViewModel()
        {
            InfoTimer.Elapsed += InfoTimerOnElapsed;
            //InfoTimer.Start();
        }

        private void InfoTimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            if (IsProcessing)
            {
                return;
            }
            IsProcessing = true;
            // do stuff if you have too
            IsProcessing = false;
        }

        #region Property Bindings

        private static Lazy<XIVInfoViewModel> _instance = new Lazy<XIVInfoViewModel>(() => new XIVInfoViewModel());
        private ConcurrentDictionary<UInt32, ActorEntity> _currentMonsters;
        private ConcurrentDictionary<uint, ActorEntity> _currentNPCs;
        private ConcurrentDictionary<uint, ActorEntity> _currentPCs;

        public static XIVInfoViewModel Instance
        {
            get { return _instance.Value; }
        }

        public ActorEntity CurrentUser => ActorEntity.CurrentUser;

        public ConcurrentDictionary<uint, ActorEntity> CurrentNPCs
        {
            get { return _currentNPCs ?? (_currentNPCs = new ConcurrentDictionary<uint, ActorEntity>()); }
            set
            {
                _currentNPCs = value;
                RaisePropertyChanged();
            }
        }

        public ConcurrentDictionary<UInt32, ActorEntity> CurrentMonsters
        {
            get { return _currentMonsters ?? (_currentMonsters = new ConcurrentDictionary<uint, ActorEntity>()); }
            set
            {
                _currentMonsters = value;
                RaisePropertyChanged();
            }
        }

        public ConcurrentDictionary<uint, ActorEntity> CurrentPCs
        {
            get { return _currentPCs ?? (_currentPCs = new ConcurrentDictionary<uint, ActorEntity>()); }
            set
            {
                _currentPCs = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Declarations

        public readonly Timer InfoTimer = new Timer(100);

        public bool IsProcessing { get; set; }

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
