// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventSubscriber.cs" company="SyndicatedLife">
//   Copyright© 2007 - 2021 Ryan Wilson &amp;lt;syndicated.life@gmail.com&amp;gt; (https://syndicated.life/)
//   Licensed under the MIT license. See LICENSE.md in the solution root for full license information.
// </copyright>
// <summary>
//   EventSubscriber.cs Implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FFXIVAPP.Plugin.Radar {
    using System.Collections.Concurrent;

    using FFXIVAPP.Common.Core.Constant;
    using FFXIVAPP.IPluginInterface.Events;
    using FFXIVAPP.Plugin.Radar.ViewModels;

    using Sharlayan.Core;

    public static class EventSubscriber {
        public static void Subscribe() {
            Plugin.PHost.ConstantsUpdated += OnConstantsUpdated;

            // Plugin.PHost.NewChatLogEntry += OnNewChatLogEntry;
            Plugin.PHost.MonsterItemsUpdated += OnMonsterItemsUpdated;
            Plugin.PHost.NPCItemsUpdated += OnNPCItemsUpdated;
            Plugin.PHost.PCItemsUpdated += OnPCItemsUpdated;
            Plugin.PHost.CurrentUserUpdated += OnCurrentUserUpdated;

            // Plugin.PHost.NewPlayerEntity += OnNewPlayerEntity;
            // Plugin.PHost.NewTargetEntity += OnNewTargetEntity;
            // Plugin.PHost.NewPartyEntries += OnNewPartyEntries;
        }

        public static void UnSubscribe() {
            Plugin.PHost.ConstantsUpdated -= OnConstantsUpdated;

            // Plugin.PHost.NewChatLogEntry -= OnNewChatLogEntry;
            Plugin.PHost.MonsterItemsUpdated -= OnMonsterItemsUpdated;
            Plugin.PHost.NPCItemsUpdated -= OnNPCItemsUpdated;
            Plugin.PHost.PCItemsUpdated -= OnPCItemsUpdated;
            Plugin.PHost.CurrentUserUpdated -= OnCurrentUserUpdated;

            // Plugin.PHost.NewPlayerEntity -= OnNewPlayerEntity;
            // Plugin.PHost.NewTargetEntity -= OnNewTargetEntity;
            // Plugin.PHost.NewPartyEntries -= OnNewPartyEntries;
        }

        private static void OnConstantsUpdated(object sender, ConstantsEntityEvent constantsEntityEvent) {
            // delegate event from constants, not required to subsribe, but recommended as it gives you app settings
            if (sender == null) {
                return;
            }

            ConstantsEntity constantsEntity = constantsEntityEvent.ConstantsEntity;
            Constants.AutoTranslate = constantsEntity.AutoTranslate;
            Constants.ChatCodes = constantsEntity.ChatCodes;
            Constants.Colors = constantsEntity.Colors;
            Constants.CultureInfo = constantsEntity.CultureInfo;
            Constants.CharacterName = constantsEntity.CharacterName;
            Constants.ServerName = constantsEntity.ServerName;
            Constants.GameLanguage = constantsEntity.GameLanguage;
            Constants.EnableHelpLabels = constantsEntity.EnableHelpLabels;
            Constants.Theme = constantsEntity.Theme;
            PluginViewModel.Instance.EnableHelpLabels = Constants.EnableHelpLabels;
        }

        private static void OnCurrentUserUpdated(object sender, CurrentUserEvent currentUserEvent) {
            // delegate event from player entities from ram, not required to subsribe
            // this updates 10x a second and only sends data if the items are found in ram
            // currently there no change/new/removed event handling (looking into it)
            if (sender == null) {
                return;
            }

            XIVInfoViewModel.Instance.CurrentUser = currentUserEvent.CurrentUser;
        }

        private static void OnMonsterItemsUpdated(object sender, ActorItemsEvent actorItemsEvent) {
            // delegate event from monster entities from ram, not required to subsribe
            // this updates 10x a second and only sends data if the items are found in ram
            // currently there no change/new/removed event handling (looking into it)
            if (sender == null) {
                return;
            }

            ConcurrentDictionary<uint, ActorItem> actorItems = actorItemsEvent.ActorItems;
            XIVInfoViewModel.Instance.CurrentMonsters = actorItems;
        }

        private static void OnNPCItemsUpdated(object sender, ActorItemsEvent actorItemsEvent) {
            // delegate event from npc entities from ram, not required to subsribe
            // this list includes anything that is not a player or monster
            // this updates 10x a second and only sends data if the items are found in ram
            // currently there no change/new/removed event handling (looking into it)
            if (sender == null) {
                return;
            }

            ConcurrentDictionary<uint, ActorItem> actorItems = actorItemsEvent.ActorItems;
            XIVInfoViewModel.Instance.CurrentNPCs = actorItems;
        }

        private static void OnPCItemsUpdated(object sender, ActorItemsEvent actorItemsEvent) {
            // delegate event from player entities from ram, not required to subsribe
            // this updates 10x a second and only sends data if the items are found in ram
            // currently there no change/new/removed event handling (looking into it)
            if (sender == null) {
                return;
            }

            ConcurrentDictionary<uint, ActorItem> actorItems = actorItemsEvent.ActorItems;
            XIVInfoViewModel.Instance.CurrentPCs = actorItems;
        }
    }
}