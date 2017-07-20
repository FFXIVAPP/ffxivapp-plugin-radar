// FFXIVAPP.Plugin.Radar ~ Radar.xaml.cs
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
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using FFXIVAPP.Common.Models;
using FFXIVAPP.Common.Utilities;
using FFXIVAPP.Plugin.Radar.Enums;
using FFXIVAPP.Plugin.Radar.Helpers;
using FFXIVAPP.Plugin.Radar.Models;
using FFXIVAPP.Plugin.Radar.Properties;
using FFXIVAPP.Plugin.Radar.ViewModels;
using FFXIVAPP.ResourceFiles;
using NLog;
using Sharlayan.Core;
using Sharlayan.Core.Enums;

namespace FFXIVAPP.Plugin.Radar.Controls
{
    /// <summary>
    ///     Interaction logic for Radar.xaml
    /// </summary>
    public partial class Radar
    {
        #region Logger

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        #endregion

        public Radar View;

        public Radar()
        {
            View = this;
            InitializeComponent();
            if (IsRendered)
            {
                return;
            }
            IsRendered = true;
        }

        #region Radar Declarations

        public bool IsRendered { get; set; }

        #endregion

        public void Refresh()
        {
            InvalidateVisual();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            var bc = new BrushConverter();

            var user = XIVInfoViewModel.Instance.CurrentUser;
            if (user == null)
            {
                return;
            }

            var origin = new Coordinate
            {
                X = (float) (ActualWidth / 2),
                Y = (float) (ActualHeight / 2)
            };

            var scale = (float) (ActualHeight / 2.0f) / 125.0f;
            var angle = Math.Abs(user.Heading * (180 / Math.PI) - 180);

            if (Settings.Default.RadarCompassMode)
            {
                var drawingGroup = new DrawingGroup
                {
                    Transform = new RotateTransform
                    {
                        Angle = angle,
                        CenterX = origin.X,
                        CenterY = origin.Y
                    }
                };
                drawingGroup.Children.Add(new ImageDrawing(Game.RadarHeading, new Rect(origin.X - 64, origin.Y - 128, 128, 128)));
                drawingGroup.Children.Add(new ImageDrawing(Game.Player, new Rect(origin.X - 8, origin.Y - 21, 16, 21)));
                drawingContext.DrawDrawing(drawingGroup);
            }
            else
            {
                drawingContext.DrawImage(Game.RadarHeading, new Rect(new Point(origin.X - 64, origin.Y - 128), new Size(128, 128)));
            }

            var sb = new StringBuilder();

            var npcEntites = new List<ActorEntity>(XIVInfoViewModel.Instance.CurrentNPCs.Select(kvp => kvp.Value)
                                                                   .ToList());
            var monsterEntites = new List<ActorEntity>(XIVInfoViewModel.Instance.CurrentMonsters.Select(kvp => kvp.Value)
                                                                       .ToList());
            var pcEntites = new List<ActorEntity>(XIVInfoViewModel.Instance.CurrentPCs.Select(kvp => kvp.Value)
                                                                  .ToList());

            if (Settings.Default.FilterRadarItems)
            {
                var npcFilters = PluginViewModel.Instance.Filters.Where(filter => filter.Type != "PC" && filter.Type != "Monster")
                                                .ToList();
                if (npcFilters.Any())
                {
                    npcEntites = RadarFilterHelper.ResolveFilteredEntities(npcFilters, npcEntites);
                }

                var monsterFilters = PluginViewModel.Instance.Filters.Where(filter => filter.Type == "Monster")
                                                    .ToList();
                if (PluginViewModel.Instance.RankedFilters.Any())
                {
                    monsterFilters.AddRange(PluginViewModel.Instance.RankedFilters);
                }

                if (monsterFilters.Any())
                {
                    monsterEntites = RadarFilterHelper.ResolveFilteredEntities(monsterFilters, monsterEntites);
                }

                var pcFilters = PluginViewModel.Instance.Filters.Where(filter => filter.Type == "PC")
                                               .ToList();
                if (pcFilters.Any())
                {
                    pcEntites = RadarFilterHelper.ResolveFilteredEntities(pcFilters, pcEntites);
                }

                monsterEntites = RadarFilterHelper.CleanupEntities(monsterEntites);
            }

            #region Resolve PCs

            if (Settings.Default.PCShow)
            {
                foreach (var actorEntity in pcEntites)
                {
                    sb.Clear();
                    var fsModifier = 0;
                    drawingContext.PushOpacity(1);
                    try
                    {
                        if (!actorEntity.IsValid || user == null)
                        {
                            continue;
                        }
                        if (actorEntity.ID == user.ID)
                        {
                            continue;
                        }
                        Coordinate screen;
                        if (Settings.Default.RadarCompassMode)
                        {
                            var coord = user.Coordinate.Subtract(actorEntity.Coordinate)
                                            .Scale(scale);
                            screen = new Coordinate(-coord.X, 0, -coord.Y).Add(origin);
                        }
                        else
                        {
                            screen = user.Coordinate.Subtract(actorEntity.Coordinate)
                                         .Rotate2D(user.Heading)
                                         .Scale(scale)
                                         .Add(origin);
                        }
                        screen = screen.Add(-8, -8, 0);
                        if (Settings.Default.PCShowName)
                        {
                            sb.Append(actorEntity.Name);
                        }
                        if (Settings.Default.PCShowHPPercent)
                        {
                            sb.AppendFormat(" {0:P0}", actorEntity.HPPercent);
                        }
                        if (Settings.Default.PCShowDistance)
                        {
                            sb.AppendFormat(" {0:N2} {1}", user.GetDistanceTo(actorEntity), ResolveHeightVariance(user, actorEntity));
                        }
                        var useJob = Settings.Default.PCShowJob;
                        if (Settings.Default.PCShowJob)
                        {
                            #region Get Job Icons

                            switch (actorEntity.Job)
                            {
                                case Actor.Job.Unknown:
                                    if (actorEntity.OwnerID > 0 && actorEntity.OwnerID < 3758096384)
                                    {
                                        drawingContext.DrawImage(Game.Chocobo, new Rect(new Point(screen.X, screen.Y), new Size(16, 16)));
                                    }
                                    useJob = false;
                                    break;
                                default:
                                    drawingContext.DrawImage(Game.GetIconByName(actorEntity.Job.ToString()), new Rect(new Point(screen.X, screen.Y), new Size(16, 16)));
                                    break;
                            }

                            #endregion
                        }
                        if (!useJob)
                        {
                            var imageSource = actorEntity.HPCurrent > 0 ? Game.Player : Game.Unknown;
                            drawingContext.DrawImage(imageSource, new Rect(new Point(screen.X, screen.Y), new Size(16, 16)));
                        }
                        if (Settings.Default.PCShowName || Settings.Default.PCShowHPPercent)
                        {
                            var label = new FormattedText(sb.ToString(), _cultureInfo, _flowDirection, _typeface, Int32.Parse(Settings.Default.PCFontSize) + fsModifier, (SolidColorBrush) bc.ConvertFromString(Settings.Default.PCFontColor));
                            drawingContext.DrawText(label, new Point(screen.X + 20, screen.Y));
                        }
                    }
                    catch (Exception ex)
                    {
                        Logging.Log(Logger, new LogItem(ex, true));
                    }
                    drawingContext.Pop();
                }
            }

            #endregion

            #region Resolve Monsters

            if (Settings.Default.MonsterShow)
            {
                foreach (var actorEntity in monsterEntites)
                {
                    sb.Clear();
                    var fsModifier = 0;
                    var fontColor = Settings.Default.MonsterFontColor;

                    var RankB = LocaleHelper.GetRankedMonsters("B");
                    var RankA = LocaleHelper.GetRankedMonsters("A");
                    var RankS = LocaleHelper.GetRankedMonsters("S");

                    if (Settings.Default.MonsterShowRankColor)
                    {
                        if (RankA.Any(x => x.Equals(actorEntity.Name, StringComparison.InvariantCultureIgnoreCase)))
                        {
                            fontColor = Settings.Default.MonsterFontColorARank;
                            fsModifier += 2;
                        }
                        else if (RankS.Any(x => x.Equals(actorEntity.Name, StringComparison.InvariantCultureIgnoreCase)))
                        {
                            fontColor = Settings.Default.MonsterFontColorSRank;
                            fsModifier += 2;
                        }
                        else if (RankB.Any(x => x.Equals(actorEntity.Name, StringComparison.InvariantCultureIgnoreCase)))
                        {
                            fontColor = Settings.Default.MonsterFontColorBRank;
                            fsModifier += 2;
                        }
                    }

                    drawingContext.PushOpacity(1);

                    try
                    {
                        if (!actorEntity.IsValid || user == null)
                        {
                            continue;
                        }
                        if (actorEntity.ID == user.ID)
                        {
                            continue;
                        }
                        Coordinate screen;
                        if (Settings.Default.RadarCompassMode)
                        {
                            var coord = user.Coordinate.Subtract(actorEntity.Coordinate)
                                            .Scale(scale);
                            screen = new Coordinate(-coord.X, 0, -coord.Y).Add(origin);
                        }
                        else
                        {
                            screen = user.Coordinate.Subtract(actorEntity.Coordinate)
                                         .Rotate2D(user.Heading)
                                         .Scale(scale)
                                         .Add(origin);
                        }
                        screen = screen.Add(-8, -8, 0);
                        ImageSource actorIcon = null;
                        switch (actorEntity.IsFate)
                        {
                            case true:
                                actorIcon = Game.MobFate;
                                break;
                            case false:
                                if (actorEntity.OwnerID > 0 && actorEntity.OwnerID < 3758096384)
                                {
                                    actorIcon = Game.Chocobo;
                                }
                                else
                                {
                                    actorIcon = actorEntity.IsClaimed ? Game.MobClaimed : Game.MobUnclaimed;
                                }
                                if (RankA.Any(x => x.Equals(actorEntity.Name, StringComparison.InvariantCultureIgnoreCase)))
                                {
                                    actorIcon = Game.MobPassive4;
                                }
                                else if (RankS.Any(x => x.Equals(actorEntity.Name, StringComparison.InvariantCultureIgnoreCase)))
                                {
                                    actorIcon = Game.MobAggressive4;
                                }
                                else if (RankB.Any(x => x.Equals(actorEntity.Name, StringComparison.InvariantCultureIgnoreCase)))
                                {
                                    actorIcon = Game.MobAggressive4;
                                }
                                break;
                        }
                        if (actorEntity.HPCurrent > 0)
                        {
                            if (actorIcon != null)
                            {
                                drawingContext.DrawImage(actorIcon, new Rect(new Point(screen.X, screen.Y), new Size(16, 16)));
                            }
                        }
                        else
                        {
                            drawingContext.DrawImage(Game.Unknown, new Rect(new Point(screen.X, screen.Y), new Size(16, 16)));
                        }
                        if (Settings.Default.MonsterShowName)
                        {
                            sb.Append(actorEntity.Name);
                        }
                        if (Settings.Default.MonsterShowHPPercent)
                        {
                            sb.AppendFormat(" {0:P0}", actorEntity.HPPercent);
                        }
                        if (Settings.Default.MonsterShowDistance)
                        {
                            sb.AppendFormat(" {0:N2} {1}", user.GetDistanceTo(actorEntity), ResolveHeightVariance(user, actorEntity));
                        }
                        if (Settings.Default.MonsterShowName || Settings.Default.MonsterShowHPPercent)
                        {
                            var label = new FormattedText(sb.ToString(), _cultureInfo, _flowDirection, _typeface, Int32.Parse(Settings.Default.MonsterFontSize) + fsModifier, (SolidColorBrush) bc.ConvertFromString(fontColor));
                            drawingContext.DrawText(label, new Point(screen.X + 20, screen.Y));
                        }
                    }
                    catch (Exception ex)
                    {
                        Logging.Log(Logger, new LogItem(ex, true));
                    }
                    drawingContext.Pop();
                }
            }

            #endregion

            #region Resolve NPCs, Gathering & Other

            foreach (var actorEntity in npcEntites)
            {
                switch (actorEntity.Type)
                {
                    case Actor.Type.NPC:

                        #region Resolve NPCs

                        if (Settings.Default.NPCShow)
                        {
                            sb.Clear();
                            var fsModifier = 0;
                            drawingContext.PushOpacity(1);
                            try
                            {
                                if (!actorEntity.IsValid || user == null)
                                {
                                    continue;
                                }
                                if (actorEntity.ID == user.ID)
                                {
                                    continue;
                                }
                                Coordinate screen;
                                if (Settings.Default.RadarCompassMode)
                                {
                                    var coord = user.Coordinate.Subtract(actorEntity.Coordinate)
                                                    .Scale(scale);
                                    screen = new Coordinate(-coord.X, 0, -coord.Y).Add(origin);
                                }
                                else
                                {
                                    screen = user.Coordinate.Subtract(actorEntity.Coordinate)
                                                 .Rotate2D(user.Heading)
                                                 .Scale(scale)
                                                 .Add(origin);
                                }
                                screen = screen.Add(-8, -8, 0);
                                if (Settings.Default.NPCShowName)
                                {
                                    sb.Append(actorEntity.Name);
                                }
                                if (Settings.Default.NPCShowHPPercent)
                                {
                                    sb.AppendFormat(" {0:P0}", actorEntity.HPPercent);
                                }
                                if (Settings.Default.NPCShowDistance)
                                {
                                    sb.AppendFormat(" {0:N2} {1}", user.GetDistanceTo(actorEntity), ResolveHeightVariance(user, actorEntity));
                                }
                                var actorIcon = Game.Vendor;
                                if (actorEntity.HPCurrent > 0)
                                {
                                    drawingContext.DrawImage(actorIcon, new Rect(new Point(screen.X, screen.Y), new Size(16, 16)));
                                }
                                else
                                {
                                    drawingContext.DrawImage(Game.Unknown, new Rect(new Point(screen.X, screen.Y), new Size(16, 16)));
                                }
                                if (Settings.Default.NPCShowName || Settings.Default.NPCShowHPPercent)
                                {
                                    var label = new FormattedText(sb.ToString(), _cultureInfo, _flowDirection, _typeface, Int32.Parse(Settings.Default.NPCFontSize) + fsModifier, (SolidColorBrush) bc.ConvertFromString(Settings.Default.NPCFontColor));
                                    drawingContext.DrawText(label, new Point(screen.X + 20, screen.Y));
                                }
                            }
                            catch (Exception ex)
                            {
                                Logging.Log(Logger, new LogItem(ex, true));
                            }
                            drawingContext.Pop();
                        }

                        #endregion

                        break;
                    case Actor.Type.Gathering:

                        #region Resolve Gathering

                        if (Settings.Default.GatheringShow)
                        {
                            sb.Clear();
                            var fsModifier = 0;
                            drawingContext.PushOpacity(1);
                            try
                            {
                                if (!actorEntity.IsValid || user == null)
                                {
                                    continue;
                                }
                                if (actorEntity.ID == user.ID)
                                {
                                    continue;
                                }
                                if (actorEntity.GatheringInvisible != 0)
                                {
                                    continue;
                                }
                                Coordinate screen;
                                if (Settings.Default.RadarCompassMode)
                                {
                                    var coord = user.Coordinate.Subtract(actorEntity.Coordinate)
                                                    .Scale(scale);
                                    screen = new Coordinate(-coord.X, 0, -coord.Y).Add(origin);
                                }
                                else
                                {
                                    screen = user.Coordinate.Subtract(actorEntity.Coordinate)
                                                 .Rotate2D(user.Heading)
                                                 .Scale(scale)
                                                 .Add(origin);
                                }
                                screen = screen.Add(-8, -8, 0);
                                if (Settings.Default.GatheringShowName)
                                {
                                    sb.Append(actorEntity.Name);
                                }
                                if (Settings.Default.GatheringShowHPPercent)
                                {
                                    sb.AppendFormat(" {0:P0}", actorEntity.HPPercent);
                                }
                                if (Settings.Default.GatheringShowDistance)
                                {
                                    sb.AppendFormat(" {0:N2} {1}", user.GetDistanceTo(actorEntity), ResolveHeightVariance(user, actorEntity));
                                }

                                var actorIcon = Game.Gathering;
                                if (Constants.GatheringNodes.TryGetValue(user.Job.ToString(), out List<GatheringNode> node))
                                {
                                    var nodeMatch = node.FirstOrDefault(n => n.Localization.Matches(actorEntity.Name));
                                    if (nodeMatch != null)
                                    {
                                        switch (user.Job)
                                        {
                                            case Actor.Job.BTN:
                                                actorIcon = Game.Harvesting;
                                                switch (nodeMatch.Rarity)
                                                {
                                                    case GatheringRarity.Unspoiled:
                                                    case GatheringRarity.Ephemeral:
                                                    case GatheringRarity.Legendary:
                                                        actorIcon = Game.HarvestingSuper;
                                                        break;
                                                }
                                                break;
                                            case Actor.Job.FSH:
                                                actorIcon = Game.Fishing;
                                                break;
                                            case Actor.Job.MIN:
                                                actorIcon = Game.Mining;
                                                switch (nodeMatch.Rarity)
                                                {
                                                    case GatheringRarity.Unspoiled:
                                                    case GatheringRarity.Ephemeral:
                                                    case GatheringRarity.Legendary:
                                                        actorIcon = Game.MiningSuper;
                                                        break;
                                                }
                                                break;
                                        }
                                    }
                                }
                                drawingContext.DrawImage(actorIcon, new Rect(new Point(screen.X, screen.Y), new Size(16, 16)));
                                if (Settings.Default.GatheringShowName || Settings.Default.GatheringShowHPPercent)
                                {
                                    var label = new FormattedText(sb.ToString(), _cultureInfo, _flowDirection, _typeface, Int32.Parse(Settings.Default.GatheringFontSize) + fsModifier, (SolidColorBrush) bc.ConvertFromString(Settings.Default.GatheringFontColor));
                                    drawingContext.DrawText(label, new Point(screen.X + 20, screen.Y));
                                }
                            }
                            catch (Exception ex)
                            {
                                Logging.Log(Logger, new LogItem(ex, true));
                            }
                            drawingContext.Pop();
                        }

                        #endregion

                        break;
                    default:

                        #region Resolve Other

                        if (Settings.Default.OtherShow)
                        {
                            sb.Clear();
                            var fsModifier = 0;
                            drawingContext.PushOpacity(1);
                            try
                            {
                                if (!actorEntity.IsValid || user == null)
                                {
                                    continue;
                                }
                                if (actorEntity.ID == user.ID)
                                {
                                    continue;
                                }
                                Coordinate screen;
                                if (Settings.Default.RadarCompassMode)
                                {
                                    var coord = user.Coordinate.Subtract(actorEntity.Coordinate)
                                                    .Scale(scale);
                                    screen = new Coordinate(-coord.X, 0, -coord.Y).Add(origin);
                                }
                                else
                                {
                                    screen = user.Coordinate.Subtract(actorEntity.Coordinate)
                                                 .Rotate2D(user.Heading)
                                                 .Scale(scale)
                                                 .Add(origin);
                                }
                                screen = screen.Add(-8, -8, 0);
                                ImageSource actorIcon;
                                switch (actorEntity.Type)
                                {
                                    case Actor.Type.Aetheryte:
                                        actorIcon = Game.Aetheryte;
                                        break;
                                    case Actor.Type.Minion:
                                        actorIcon = Game.Avatar;
                                        break;
                                    default:
                                        actorIcon = Game.Vendor;
                                        break;
                                }
                                if (actorEntity.HPCurrent > 0 || actorEntity.Type == Actor.Type.Aetheryte)
                                {
                                    if (actorIcon != null)
                                    {
                                        drawingContext.DrawImage(actorIcon, new Rect(new Point(screen.X, screen.Y), new Size(16, 16)));
                                    }
                                }
                                else
                                {
                                    drawingContext.DrawImage(Game.Unknown, new Rect(new Point(screen.X, screen.Y), new Size(16, 16)));
                                }
                                if (Settings.Default.OtherShowName)
                                {
                                    sb.Append(actorEntity.Name);
                                }
                                if (Settings.Default.OtherShowHPPercent)
                                {
                                    sb.AppendFormat(" {0:P0}", actorEntity.HPPercent);
                                }
                                if (Settings.Default.OtherShowDistance)
                                {
                                    sb.AppendFormat(" {0:N2} {1}", user.GetDistanceTo(actorEntity), ResolveHeightVariance(user, actorEntity));
                                }
                                if (Settings.Default.OtherShowName || Settings.Default.OtherShowHPPercent)
                                {
                                    var label = new FormattedText(sb.ToString(), _cultureInfo, _flowDirection, _typeface, Int32.Parse(Settings.Default.OtherFontSize) + fsModifier, (SolidColorBrush) bc.ConvertFromString(Settings.Default.OtherFontColor));
                                    drawingContext.DrawText(label, new Point(screen.X + 20, screen.Y));
                                }
                            }
                            catch (Exception ex)
                            {
                                Logging.Log(Logger, new LogItem(ex, true));
                            }
                            drawingContext.Pop();
                        }

                        #endregion

                        break;
                }
            }

            #endregion
        }

        private string ResolveHeightVariance(ActorEntity user, ActorEntity actorEntity)
        {
            var modifier = string.Empty;
            if (user.Z < actorEntity.Z)
            {
                modifier = "+";
            }
            if (user.Z > actorEntity.Z)
            {
                modifier = "-";
            }
            return $"{modifier}{Math.Abs(ResolveHeightVarianceDecimal(user, actorEntity)):N2}";
        }

        private decimal ResolveHeightVarianceDecimal(ActorEntity user, ActorEntity actorEntity)
        {
            double variance = 0;
            if (user.Z < actorEntity.Z)
            {
                variance = user.Z - actorEntity.Z;
            }
            if (user.Z > actorEntity.Z)
            {
                variance = actorEntity.Z - user.Z;
            }
            return (decimal) variance;
        }

        #region DrawContext Declaratoins

        private CultureInfo _cultureInfo = CultureInfo.InvariantCulture;
        private FlowDirection _flowDirection = FlowDirection.LeftToRight;
        private Typeface _typeface = new Typeface("Verdana");

        #endregion
    }
}
