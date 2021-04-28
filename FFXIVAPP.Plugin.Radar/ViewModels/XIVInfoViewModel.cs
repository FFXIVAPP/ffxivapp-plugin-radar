// --------------------------------------------------------------------------------------------------------------------
// <copyright file="XIVInfoViewModel.cs" company="SyndicatedLife">
//   Copyright© 2007 - 2021 Ryan Wilson &amp;lt;syndicated.life@gmail.com&amp;gt; (https://syndicated.life/)
//   Licensed under the MIT license. See LICENSE.md in the solution root for full license information.
// </copyright>
// <summary>
//   XIVInfoViewModel.cs Implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FFXIVAPP.Plugin.Radar.ViewModels {
    using System;
    using System.Collections.Concurrent;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Timers;

    using Sharlayan.Core;

    public class XIVInfoViewModel : INotifyPropertyChanged {
        private static Lazy<XIVInfoViewModel> _instance = new Lazy<XIVInfoViewModel>(() => new XIVInfoViewModel());

        public readonly Timer InfoTimer = new Timer(100);

        private ConcurrentDictionary<uint, ActorItem> _currentMonsters;

        private ConcurrentDictionary<uint, ActorItem> _currentNPCs;

        private ConcurrentDictionary<uint, ActorItem> _currentPCs;

        public XIVInfoViewModel() {
            this.InfoTimer.Elapsed += this.InfoTimerOnElapsed;

            // InfoTimer.Start();
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public static XIVInfoViewModel Instance {
            get {
                return _instance.Value;
            }
        }

        public ConcurrentDictionary<uint, ActorItem> CurrentMonsters {
            get {
                return this._currentMonsters ?? (this._currentMonsters = new ConcurrentDictionary<uint, ActorItem>());
            }

            set {
                this._currentMonsters = value;
                this.RaisePropertyChanged();
            }
        }

        public ConcurrentDictionary<uint, ActorItem> CurrentNPCs {
            get {
                return this._currentNPCs ?? (this._currentNPCs = new ConcurrentDictionary<uint, ActorItem>());
            }

            set {
                this._currentNPCs = value;
                this.RaisePropertyChanged();
            }
        }

        public ConcurrentDictionary<uint, ActorItem> CurrentPCs {
            get {
                return this._currentPCs ?? (this._currentPCs = new ConcurrentDictionary<uint, ActorItem>());
            }

            set {
                this._currentPCs = value;
                this.RaisePropertyChanged();
            }
        }

        public ActorItem CurrentUser => ActorItem.CurrentUser;

        public bool IsProcessing { get; set; }

        private void InfoTimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs) {
            if (this.IsProcessing) {
                return;
            }

            this.IsProcessing = true;

            // do stuff if you have too
            this.IsProcessing = false;
        }

        private void RaisePropertyChanged([CallerMemberName] string caller = "") {
            this.PropertyChanged(this, new PropertyChangedEventArgs(caller));
        }
    }
}