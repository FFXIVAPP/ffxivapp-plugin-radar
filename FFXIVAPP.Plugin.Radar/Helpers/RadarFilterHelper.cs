// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RadarFilterHelper.cs" company="SyndicatedLife">
//   Copyright(c) 2018 Ryan Wilson &amp;lt;syndicated.life@gmail.com&amp;gt; (http://syndicated.life/)
//   Licensed under the MIT license. See LICENSE.md in the solution root for full license information.
// </copyright>
// <summary>
//   RadarFilterHelper.cs Implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FFXIVAPP.Plugin.Radar.Helpers {
    using System.Collections.Generic;

    using FFXIVAPP.Plugin.Radar.Models;
    using FFXIVAPP.Plugin.Radar.ViewModels;

    using Sharlayan.Core;
    using Sharlayan.Core.Enums;

    public static class RadarFilterHelper {
        public static List<ActorItem> CleanupEntities(IEnumerable<ActorItem> entities) {
            List<ActorItem> filtered = new List<ActorItem>();
            foreach (ActorItem actorEntity in entities) {
                var correctMap = XIVInfoViewModel.Instance.CurrentUser.MapIndex == actorEntity.MapIndex;
                var isDead = actorEntity.ActionStatus != Actor.ActionStatus.Dead;

                if (isDead && correctMap) {
                    filtered.Add(actorEntity);
                }
            }

            return filtered;
        }

        public static List<ActorItem> ResolveFilteredEntities(List<RadarFilterItem> filters, IEnumerable<ActorItem> entities) {
            List<ActorItem> filtered = new List<ActorItem>();
            foreach (ActorItem actorEntity in entities) {
                foreach (RadarFilterItem radarFilterItem in filters) {
                    if (radarFilterItem.RegEx.IsMatch(actorEntity.Name) && actorEntity.Level >= radarFilterItem.Level) {
                        filtered.Add(actorEntity);
                    }
                }
            }

            return filtered;
        }
    }
}