// FFXIVAPP.Plugin.Radar ~ RadarFilterHelper.cs
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

using System.Collections.Generic;
using FFXIVAPP.Memory.Core;
using FFXIVAPP.Memory.Core.Enums;
using FFXIVAPP.Plugin.Radar.Models;
using FFXIVAPP.Plugin.Radar.ViewModels;

namespace FFXIVAPP.Plugin.Radar.Helpers
{
    public static class RadarFilterHelper
    {
        public static List<ActorEntity> ResolveFilteredEntities(List<RadarFilterItem> filters, IEnumerable<ActorEntity> entities)
        {
            var filtered = new List<ActorEntity>();
            foreach (var actorEntity in entities)
            {
                foreach (var radarFilterItem in filters)
                {
                    if (radarFilterItem.RegEx.IsMatch(actorEntity.Name) && actorEntity.Level >= radarFilterItem.Level)
                    {
                        filtered.Add(actorEntity);
                    }
                }
            }
            return filtered;
        }

        public static List<ActorEntity> CleanupEntities(IEnumerable<ActorEntity> entities)
        {
            var filtered = new List<ActorEntity>();
            foreach (var actorEntity in entities)
            {
                var correctMap = XIVInfoViewModel.Instance.CurrentUser.MapIndex == actorEntity.MapIndex;
                var isDead = actorEntity.ActionStatus != Actor.ActionStatus.Dead;

                if (isDead && correctMap)
                {
                    filtered.Add(actorEntity);
                }
            }
            return filtered;
        }
    }
}
