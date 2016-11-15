// FFXIVAPP.Plugin.Radar ~ Radar.xaml.cs
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
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using FFXIVAPP.Common.Utilities;
using FFXIVAPP.Memory.Core;
using FFXIVAPP.Memory.Core.Enums;
using FFXIVAPP.Plugin.Radar.Helpers;
using FFXIVAPP.Plugin.Radar.Properties;
using FFXIVAPP.Plugin.Radar.ViewModels;
using NLog;

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

            var scale = ((float) (ActualHeight / 2.0f) / 125.0f);
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
                drawingGroup.Children.Add(new ImageDrawing(RadarIconHelper.RadarHeading, new Rect(origin.X - 64, origin.Y - 128, 128, 128)));
                drawingContext.DrawDrawing(drawingGroup);
            }
            else
            {
                drawingContext.DrawImage(RadarIconHelper.RadarHeading, new Rect(new Point(origin.X - 64, origin.Y - 128), new Size(128, 128)));
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
                var npcFilters = PluginViewModel.Instance.Filters.Where(filter => filter.Type != Actor.Type.PC && filter.Type != Actor.Type.Monster)
                                                .ToList();
                if (npcFilters.Any())
                {
                    npcEntites = RadarFilterHelper.ResolveFilteredEntities(npcFilters, npcEntites);
                }

                var monsterFilters = PluginViewModel.Instance.Filters.Where(filter => filter.Type == Actor.Type.Monster)
                                                    .ToList();
                if (PluginViewModel.Instance.RankedFilters.Any())
                {
                    monsterFilters.AddRange(PluginViewModel.Instance.RankedFilters);
                }

                if (monsterFilters.Any())
                {
                    monsterEntites = RadarFilterHelper.ResolveFilteredEntities(monsterFilters, monsterEntites);
                }
                
                var pcFilters = PluginViewModel.Instance.Filters.Where(filter => filter.Type == Actor.Type.PC)
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
                                case Actor.Job.ACN:
                                    drawingContext.DrawImage(RadarIconHelper.Arcanist, new Rect(new Point(screen.X, screen.Y), new Size(16, 16)));
                                    break;
                                case Actor.Job.ALC:
                                    drawingContext.DrawImage(RadarIconHelper.Alchemist, new Rect(new Point(screen.X, screen.Y), new Size(16, 16)));
                                    break;
                                case Actor.Job.ARC:
                                    drawingContext.DrawImage(RadarIconHelper.Archer, new Rect(new Point(screen.X, screen.Y), new Size(16, 16)));
                                    break;
                                case Actor.Job.ARM:
                                    drawingContext.DrawImage(RadarIconHelper.Armorer, new Rect(new Point(screen.X, screen.Y), new Size(16, 16)));
                                    break;
                                case Actor.Job.AST:
                                    drawingContext.DrawImage(RadarIconHelper.Astrologian, new Rect(new Point(screen.X, screen.Y), new Size(16, 16)));
                                    break;
                                case Actor.Job.BLM:
                                    drawingContext.DrawImage(RadarIconHelper.Blackmage, new Rect(new Point(screen.X, screen.Y), new Size(16, 16)));
                                    break;
                                case Actor.Job.BRD:
                                    drawingContext.DrawImage(RadarIconHelper.Bard, new Rect(new Point(screen.X, screen.Y), new Size(16, 16)));
                                    break;
                                case Actor.Job.BSM:
                                    drawingContext.DrawImage(RadarIconHelper.Blacksmith, new Rect(new Point(screen.X, screen.Y), new Size(16, 16)));
                                    break;
                                case Actor.Job.BTN:
                                    drawingContext.DrawImage(RadarIconHelper.Botanist, new Rect(new Point(screen.X, screen.Y), new Size(16, 16)));
                                    break;
                                case Actor.Job.CNJ:
                                    drawingContext.DrawImage(RadarIconHelper.Conjurer, new Rect(new Point(screen.X, screen.Y), new Size(16, 16)));
                                    break;
                                case Actor.Job.CPT:
                                    drawingContext.DrawImage(RadarIconHelper.Carpenter, new Rect(new Point(screen.X, screen.Y), new Size(16, 16)));
                                    break;
                                case Actor.Job.CUL:
                                    drawingContext.DrawImage(RadarIconHelper.Culinarian, new Rect(new Point(screen.X, screen.Y), new Size(16, 16)));
                                    break;
                                case Actor.Job.DRG:
                                    drawingContext.DrawImage(RadarIconHelper.Dragoon, new Rect(new Point(screen.X, screen.Y), new Size(16, 16)));
                                    break;
                                case Actor.Job.DRK:
                                    drawingContext.DrawImage(RadarIconHelper.DarkKnight, new Rect(new Point(screen.X, screen.Y), new Size(16, 16)));
                                    break;
                                case Actor.Job.FSH:
                                    drawingContext.DrawImage(RadarIconHelper.Fisher, new Rect(new Point(screen.X, screen.Y), new Size(16, 16)));
                                    break;
                                case Actor.Job.GLD:
                                    drawingContext.DrawImage(RadarIconHelper.Gladiator, new Rect(new Point(screen.X, screen.Y), new Size(16, 16)));
                                    break;
                                case Actor.Job.GSM:
                                    drawingContext.DrawImage(RadarIconHelper.Goldsmith, new Rect(new Point(screen.X, screen.Y), new Size(16, 16)));
                                    break;
                                case Actor.Job.LNC:
                                    drawingContext.DrawImage(RadarIconHelper.Leatherworker, new Rect(new Point(screen.X, screen.Y), new Size(16, 16)));
                                    break;
                                case Actor.Job.LTW:
                                    drawingContext.DrawImage(RadarIconHelper.Leatherworker, new Rect(new Point(screen.X, screen.Y), new Size(16, 16)));
                                    break;
                                case Actor.Job.MCH:
                                    drawingContext.DrawImage(RadarIconHelper.Machinist, new Rect(new Point(screen.X, screen.Y), new Size(16, 16)));
                                    break;
                                case Actor.Job.MIN:
                                    drawingContext.DrawImage(RadarIconHelper.Miner, new Rect(new Point(screen.X, screen.Y), new Size(16, 16)));
                                    break;
                                case Actor.Job.MNK:
                                    drawingContext.DrawImage(RadarIconHelper.Monk, new Rect(new Point(screen.X, screen.Y), new Size(16, 16)));
                                    break;
                                case Actor.Job.MRD:
                                    drawingContext.DrawImage(RadarIconHelper.Marauder, new Rect(new Point(screen.X, screen.Y), new Size(16, 16)));
                                    break;
                                case Actor.Job.NIN:
                                    drawingContext.DrawImage(RadarIconHelper.Ninja, new Rect(new Point(screen.X, screen.Y), new Size(16, 16)));
                                    break;
                                case Actor.Job.PGL:
                                    drawingContext.DrawImage(RadarIconHelper.Pugilist, new Rect(new Point(screen.X, screen.Y), new Size(16, 16)));
                                    break;
                                case Actor.Job.PLD:
                                    drawingContext.DrawImage(RadarIconHelper.Paladin, new Rect(new Point(screen.X, screen.Y), new Size(16, 16)));
                                    break;
                                case Actor.Job.ROG:
                                    drawingContext.DrawImage(RadarIconHelper.Rogue, new Rect(new Point(screen.X, screen.Y), new Size(16, 16)));
                                    break;
                                case Actor.Job.SCH:
                                    drawingContext.DrawImage(RadarIconHelper.Scholar, new Rect(new Point(screen.X, screen.Y), new Size(16, 16)));
                                    break;
                                case Actor.Job.Unknown:
                                    if (actorEntity.OwnerID > 0 && actorEntity.OwnerID < 3758096384)
                                    {
                                        drawingContext.DrawImage(RadarIconHelper.Chocobo, new Rect(new Point(screen.X, screen.Y), new Size(16, 16)));
                                    }
                                    useJob = false;
                                    break;
                                case Actor.Job.WAR:
                                    drawingContext.DrawImage(RadarIconHelper.Warrior, new Rect(new Point(screen.X, screen.Y), new Size(16, 16)));
                                    break;
                                case Actor.Job.WHM:
                                    drawingContext.DrawImage(RadarIconHelper.Whitemage, new Rect(new Point(screen.X, screen.Y), new Size(16, 16)));
                                    break;
                                case Actor.Job.WVR:
                                    drawingContext.DrawImage(RadarIconHelper.Weaver, new Rect(new Point(screen.X, screen.Y), new Size(16, 16)));
                                    break;
                            }

                            #endregion
                        }
                        if (!useJob)
                        {
                            var imageSource = actorEntity.HPCurrent > 0 ? RadarIconHelper.Player : RadarIconHelper.Skull;
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
                        Logging.Log(Logger, ex.Message);
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
                                actorIcon = RadarIconHelper.Fate;
                                break;
                            case false:
                                if (actorEntity.OwnerID > 0 && actorEntity.OwnerID < 3758096384)
                                {
                                    actorIcon = RadarIconHelper.ChocoboPersonal;
                                }
                                else
                                {
                                    actorIcon = actorEntity.IsClaimed ? RadarIconHelper.MonsterClaimed : RadarIconHelper.Monster;
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
                            drawingContext.DrawImage(RadarIconHelper.Skull, new Rect(new Point(screen.X, screen.Y), new Size(16, 16)));
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
                        Logging.Log(Logger, ex.Message);
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
                                var actorIcon = RadarIconHelper.NPC;
                                if (actorEntity.HPCurrent > 0)
                                {
                                    drawingContext.DrawImage(actorIcon, new Rect(new Point(screen.X, screen.Y), new Size(16, 16)));
                                }
                                else
                                {
                                    drawingContext.DrawImage(RadarIconHelper.Skull, new Rect(new Point(screen.X, screen.Y), new Size(16, 16)));
                                }
                                if (Settings.Default.NPCShowName || Settings.Default.NPCShowHPPercent)
                                {
                                    var label = new FormattedText(sb.ToString(), _cultureInfo, _flowDirection, _typeface, Int32.Parse(Settings.Default.NPCFontSize) + fsModifier, (SolidColorBrush) bc.ConvertFromString(Settings.Default.NPCFontColor));
                                    drawingContext.DrawText(label, new Point(screen.X + 20, screen.Y));
                                }
                            }
                            catch (Exception ex)
                            {
                                Logging.Log(Logger, ex.Message);
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
                                var actorIcon = RadarIconHelper.Wood;
                                if (actorEntity.HPCurrent > 0)
                                {
                                    drawingContext.DrawImage(actorIcon, new Rect(new Point(screen.X, screen.Y), new Size(16, 16)));
                                }
                                else
                                {
                                    drawingContext.DrawImage(RadarIconHelper.Skull, new Rect(new Point(screen.X, screen.Y), new Size(16, 16)));
                                }
                                if (Settings.Default.GatheringShowName || Settings.Default.GatheringShowHPPercent)
                                {
                                    var label = new FormattedText(sb.ToString(), _cultureInfo, _flowDirection, _typeface, Int32.Parse(Settings.Default.GatheringFontSize) + fsModifier, (SolidColorBrush) bc.ConvertFromString(Settings.Default.GatheringFontColor));
                                    drawingContext.DrawText(label, new Point(screen.X + 20, screen.Y));
                                }
                            }
                            catch (Exception ex)
                            {
                                Logging.Log(Logger, ex.Message);
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
                                                 .Scale(scale);
                                }
                                screen = screen.Add(-8, -8, 0);
                                ImageSource actorIcon;
                                switch (actorEntity.Type)
                                {
                                    case Actor.Type.Aetheryte:
                                        actorIcon = RadarIconHelper.Crystal;
                                        break;
                                    case Actor.Type.Minion:
                                        actorIcon = RadarIconHelper.Sheep;
                                        break;
                                    default:
                                        actorIcon = RadarIconHelper.NPC;
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
                                    drawingContext.DrawImage(RadarIconHelper.Skull, new Rect(new Point(screen.X, screen.Y), new Size(16, 16)));
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
                                Logging.Log(Logger, ex.Message);
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
            var modifier = "";
            if (user.Z < actorEntity.Z)
            {
                modifier = "+";
            }
            if (user.Z > actorEntity.Z)
            {
                modifier = "-";
            }
            return String.Format("{0}{1:N2}", modifier, Math.Abs(ResolveHeightVarianceDecimal(user, actorEntity)));
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
