// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Radar.xaml.cs" company="SyndicatedLife">
//   Copyright(c) 2018 Ryan Wilson &amp;lt;syndicated.life@gmail.com&amp;gt; (http://syndicated.life/)
//   Licensed under the MIT license. See LICENSE.md in the solution root for full license information.
// </copyright>
// <summary>
//   Radar.xaml.cs Implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FFXIVAPP.Plugin.Radar.Controls {
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

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

    /// <summary>
    ///     Interaction logic for Radar.xaml
    /// </summary>
    public partial class Radar {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public Radar View;

        private CultureInfo _cultureInfo = CultureInfo.InvariantCulture;

        private FlowDirection _flowDirection = FlowDirection.LeftToRight;

        private Typeface _typeface = new Typeface("Verdana");

        public Radar() {
            this.View = this;
            this.InitializeComponent();
            if (this.IsRendered) {
                return;
            }

            this.IsRendered = true;
        }

        public bool IsRendered { get; set; }

        public void Refresh() {
            this.InvalidateVisual();
        }

        protected override void OnRender(DrawingContext drawingContext) {
            base.OnRender(drawingContext);

            var bc = new BrushConverter();

            ActorItem user = XIVInfoViewModel.Instance.CurrentUser;
            if (user == null) {
                return;
            }

            var origin = new Coordinate {
                X = (float) (this.ActualWidth / 2),
                Y = (float) (this.ActualHeight / 2)
            };

            var scale = (float) (this.ActualHeight / 2.0f) / 125.0f;
            var angle = Math.Abs(user.Heading * (180 / Math.PI) - 180);

            var drawingGroup = new DrawingGroup();

            if (Settings.Default.RadarCompassMode) {
                drawingGroup.Transform = new RotateTransform {
                    Angle = angle,
                    CenterX = origin.X,
                    CenterY = origin.Y
                };
            }

            drawingGroup.Children.Add(new ImageDrawing(Game.RadarHeading, new Rect(origin.X - 64, origin.Y - 128, 128, 128)));
            drawingGroup.Children.Add(new ImageDrawing(Game.Player, new Rect(origin.X - 8, origin.Y - 16, 16, 21)));

            this.DrawDrawing(drawingContext, drawingGroup);

            var sb = new StringBuilder();

            List<ActorItem> npcEntites = new List<ActorItem>(XIVInfoViewModel.Instance.CurrentNPCs.Select(kvp => kvp.Value).ToList());
            List<ActorItem> monsterEntites = new List<ActorItem>(XIVInfoViewModel.Instance.CurrentMonsters.Select(kvp => kvp.Value).ToList());
            List<ActorItem> pcEntites = new List<ActorItem>(XIVInfoViewModel.Instance.CurrentPCs.Select(kvp => kvp.Value).ToList());

            

            if (Settings.Default.FilterRadarItems) {
                List<RadarFilterItem> npcFilters = PluginViewModel.Instance.Filters.Where(filter => filter.Type != "PC" && filter.Type != "Monster").ToList();
                if (npcFilters.Any()) {
                    npcEntites = RadarFilterHelper.ResolveFilteredEntities(npcFilters, npcEntites);
                }

                List<RadarFilterItem> monsterFilters = PluginViewModel.Instance.Filters.Where(filter => filter.Type == "Monster").ToList();
                if (PluginViewModel.Instance.RankedFilters.Any()) {
                    monsterFilters.AddRange(PluginViewModel.Instance.RankedFilters);
                }

                if (monsterFilters.Any()) {
                    monsterEntites = RadarFilterHelper.ResolveFilteredEntities(monsterFilters, monsterEntites);
                }

                List<RadarFilterItem> pcFilters = PluginViewModel.Instance.Filters.Where(filter => filter.Type == "PC").ToList();
                if (pcFilters.Any()) {
                    pcEntites = RadarFilterHelper.ResolveFilteredEntities(pcFilters, pcEntites);
                }

                monsterEntites = RadarFilterHelper.CleanupEntities(monsterEntites);
            }

            

            #region Resolve PCs

            if (Settings.Default.PCShow) {
                foreach (ActorItem actorEntity in pcEntites) {
                    sb.Clear();
                    var fontSizeModifier = 0;
                    drawingContext.PushOpacity(1);
                    try {
                        if (!actorEntity.IsValid || user == null) {
                            continue;
                        }

                        if (actorEntity.ID == user.ID) {
                            continue;
                        }

                        Coordinate screen;
                        if (Settings.Default.RadarCompassMode) {
                            Coordinate coord = user.Coordinate.Subtract(actorEntity.Coordinate).Scale(scale);
                            screen = new Coordinate(-coord.X, 0, -coord.Y).Add(origin);
                        }
                        else {
                            screen = user.Coordinate.Subtract(actorEntity.Coordinate).Rotate2D(user.Heading).Scale(scale).Add(origin);
                        }

                        screen = screen.Add(-8, -8, 0);
                        if (Settings.Default.PCShowName) {
                            sb.Append(actorEntity.Name);
                        }

                        if (Settings.Default.PCShowHPPercent) {
                            sb.AppendFormat(" {0:P0}", actorEntity.HPPercent);
                        }

                        if (Settings.Default.PCShowDistance) {
                            sb.AppendFormat(" {0:N2} {1}", user.GetDistanceTo(actorEntity), this.ResolveHeightVariance(user, actorEntity));
                        }

                        var useJob = Settings.Default.PCShowJob;
                        if (Settings.Default.PCShowJob) {
                            #region Get Job Icons

                            switch (actorEntity.Job) {
                                case Actor.Job.Unknown:
                                    if (actorEntity.OwnerID > 0 && actorEntity.OwnerID < 3758096384) {
                                        this.DrawImage(drawingContext, Game.Chocobo, new Rect(new Point(), new Size(16, 16)));
                                    }

                                    useJob = false;
                                    break;
                                default:
                                    this.DrawImage(drawingContext, Game.GetIconByName(actorEntity.Job.ToString()), new Rect(new Point(screen.X, screen.Y), new Size(16, 16)));
                                    break;
                            }

                            #endregion
                        }

                        if (!useJob) {
                            BitmapImage imageSource = actorEntity.HPCurrent > 0
                                                          ? Game.Player
                                                          : Game.Unknown;
                            this.DrawImage(drawingContext, imageSource, new Rect(new Point(screen.X, screen.Y), new Size(16, 16)));
                        }

                        this.RenderDebugInformation(actorEntity, ref sb);

                        if (Settings.Default.PCShowName || Settings.Default.PCShowHPPercent) {
                            var text = new FormattedText(sb.ToString(), this._cultureInfo, this._flowDirection, this._typeface, int.Parse(Settings.Default.PCFontSize) + fontSizeModifier, (SolidColorBrush) bc.ConvertFromString(Settings.Default.PCFontColor));
                            this.DrawText(drawingContext, text, screen.X + 20, screen.Y);
                        }
                    }
                    catch (Exception ex) {
                        Logging.Log(Logger, new LogItem(ex, true));
                    }

                    drawingContext.Pop();
                }
            }

            #endregion

            #region Resolve Monsters

            if (Settings.Default.MonsterShow) {
                foreach (ActorItem actorEntity in monsterEntites) {
                    sb.Clear();
                    var fontSizeModifier = 0;
                    var fontColor = Settings.Default.MonsterFontColor;

                    List<string> RankB = LocaleHelper.GetRankedMonsters("B");
                    List<string> RankA = LocaleHelper.GetRankedMonsters("A");
                    List<string> RankS = LocaleHelper.GetRankedMonsters("S");

                    var isRanked = false;

                    if (Settings.Default.MonsterShowRankColor) {
                        if (RankA.Any(x => x.Equals(actorEntity.Name, StringComparison.InvariantCultureIgnoreCase))) {
                            fontColor = Settings.Default.MonsterFontColorARank;
                            fontSizeModifier += 2;
                            isRanked = true;
                        }
                        else if (RankS.Any(x => x.Equals(actorEntity.Name, StringComparison.InvariantCultureIgnoreCase))) {
                            fontColor = Settings.Default.MonsterFontColorSRank;
                            fontSizeModifier += 2;
                            isRanked = true;
                        }
                        else if (RankB.Any(x => x.Equals(actorEntity.Name, StringComparison.InvariantCultureIgnoreCase))) {
                            fontColor = Settings.Default.MonsterFontColorBRank;
                            fontSizeModifier += 2;
                            isRanked = true;
                        }
                    }

                    drawingContext.PushOpacity(1);

                    try {
                        if (!actorEntity.IsValid || user == null) {
                            continue;
                        }

                        if (actorEntity.ID == user.ID) {
                            continue;
                        }

                        Coordinate screen;
                        if (Settings.Default.RadarCompassMode) {
                            Coordinate coord = user.Coordinate.Subtract(actorEntity.Coordinate).Scale(scale);
                            screen = new Coordinate(-coord.X, 0, -coord.Y).Add(origin);
                        }
                        else {
                            screen = user.Coordinate.Subtract(actorEntity.Coordinate).Rotate2D(user.Heading).Scale(scale).Add(origin);
                        }

                        screen = screen.Add(-8, -8, 0);
                        BitmapImage actorIcon;

                        if (actorEntity.IsFate) {
                            actorIcon = Game.MobFate;
                        }
                        else {
                            switch (actorEntity.DifficultyRank) {
                                case 6:
                                    actorIcon = actorEntity.IsAggressive
                                                    ? Game.MobAggressive5
                                                    : Game.MobPassive5;
                                    break;
                                case 4:
                                    actorIcon = actorEntity.IsAggressive
                                                    ? Game.MobAggressive3
                                                    : Game.MobPassive3;
                                    break;
                                case 3:
                                    actorIcon = actorEntity.IsAggressive
                                                    ? Game.MobAggressive2
                                                    : Game.MobPassive2;
                                    break;
                                case 2:
                                    actorIcon = actorEntity.IsAggressive
                                                    ? Game.MobAggressive6
                                                    : Game.MobPassive6;
                                    break;
                                case 1:
                                    actorIcon = actorEntity.IsAggressive
                                                    ? Game.MobAggressive4
                                                    : Game.MobPassive4;
                                    break;
                                default:
                                    actorIcon = actorEntity.IsAggressive
                                                    ? Game.MobAggressive1
                                                    : Game.MobPassive1;
                                    break;
                            }

                            if (actorEntity.OwnerID > 0 && actorEntity.OwnerID < 3758096384) {
                                actorIcon = Game.Chocobo;
                            }
                        }

                        var iconSize = new Size(16, 16);
                        var point = new Point(screen.X, screen.Y);

                        if (isRanked) {
                            iconSize = new Size(24, 24);
                            point = new Point(screen.X - 4, screen.Y - 4);
                        }

                        if (actorEntity.HPCurrent > 0) {
                            if (actorIcon != null) {
                                this.DrawImage(drawingContext, actorIcon, new Rect(point, iconSize));
                            }
                        }
                        else {
                            this.DrawImage(drawingContext, Game.Unknown, new Rect(point, iconSize));
                        }

                        if (Settings.Default.MonsterShowName) {
                            sb.Append(actorEntity.Name);
                        }

                        if (Settings.Default.MonsterShowHPPercent) {
                            sb.AppendFormat(" {0:P0}", actorEntity.HPPercent);
                        }

                        if (Settings.Default.MonsterShowDistance) {
                            sb.AppendFormat(" {0:N2} {1}", user.GetDistanceTo(actorEntity), this.ResolveHeightVariance(user, actorEntity));
                        }

                        this.RenderDebugInformation(actorEntity, ref sb);

                        if (Settings.Default.MonsterShowName || Settings.Default.MonsterShowHPPercent) {
                            var text = new FormattedText(sb.ToString(), this._cultureInfo, this._flowDirection, this._typeface, int.Parse(Settings.Default.MonsterFontSize) + fontSizeModifier, (SolidColorBrush) bc.ConvertFromString(fontColor));
                            this.DrawText(drawingContext, text, screen.X + 20, screen.Y);
                        }
                    }
                    catch (Exception ex) {
                        Logging.Log(Logger, new LogItem(ex, true));
                    }

                    drawingContext.Pop();
                }
            }

            #endregion

            #region Resolve NPCs, Gathering & Other

            foreach (ActorItem actorEntity in npcEntites) {
                sb.Clear();
                var fontSizeModifier = 0;
                drawingContext.PushOpacity(1);

                try {
                    if (!actorEntity.IsValid || user == null) {
                        continue;
                    }

                    if (actorEntity.ID == user.ID) {
                        continue;
                    }

                    Coordinate screen;
                    if (Settings.Default.RadarCompassMode) {
                        Coordinate coord = user.Coordinate.Subtract(actorEntity.Coordinate).Scale(scale);
                        screen = new Coordinate(-coord.X, 0, -coord.Y).Add(origin);
                    }
                    else {
                        screen = user.Coordinate.Subtract(actorEntity.Coordinate).Rotate2D(user.Heading).Scale(scale).Add(origin);
                    }

                    screen = screen.Add(-8, -8, 0);
                    BitmapImage actorIcon;

                    switch (actorEntity.Type) {
                        case Actor.Type.NPC:

                            #region Resolve NPCs

                            if (Settings.Default.NPCShow) {
                                try {
                                    if (Settings.Default.NPCShowName) {
                                        sb.Append(actorEntity.Name);
                                    }

                                    if (Settings.Default.NPCShowHPPercent) {
                                        sb.AppendFormat(" {0:P0}", actorEntity.HPPercent);
                                    }

                                    if (Settings.Default.NPCShowDistance) {
                                        sb.AppendFormat(" {0:N2} {1}", user.GetDistanceTo(actorEntity), this.ResolveHeightVariance(user, actorEntity));
                                    }

                                    actorIcon = actorEntity.HPCurrent > 0
                                                    ? Game.Vendor
                                                    : Game.Unknown;
                                    this.DrawImage(drawingContext, actorIcon, new Rect(new Point(screen.X, screen.Y), new Size(16, 16)));

                                    this.RenderDebugInformation(actorEntity, ref sb);

                                    if (Settings.Default.NPCShowName || Settings.Default.NPCShowHPPercent) {
                                        var text = new FormattedText(sb.ToString(), this._cultureInfo, this._flowDirection, this._typeface, int.Parse(Settings.Default.NPCFontSize) + fontSizeModifier, (SolidColorBrush) bc.ConvertFromString(Settings.Default.NPCFontColor));
                                        this.DrawText(drawingContext, text, screen.X + 20, screen.Y);
                                    }
                                }
                                catch (Exception ex) {
                                    Logging.Log(Logger, new LogItem(ex, true));
                                }

                                drawingContext.Pop();
                            }

                            #endregion

                            break;
                        case Actor.Type.Gathering:

                            #region Resolve Gathering

                            if (Settings.Default.GatheringShow && actorEntity.GatheringInvisible == 0) {
                                try {
                                    if (Settings.Default.GatheringShowName) {
                                        sb.Append(actorEntity.Name);
                                    }

                                    if (Settings.Default.GatheringShowHPPercent) {
                                        sb.AppendFormat(" {0:P0}", actorEntity.HPPercent);
                                    }

                                    if (Settings.Default.GatheringShowDistance) {
                                        sb.AppendFormat(" {0:N2} {1}", user.GetDistanceTo(actorEntity), this.ResolveHeightVariance(user, actorEntity));
                                    }

                                    actorIcon = Game.Gathering;
                                    if (Constants.GatheringNodes.TryGetValue(user.Job.ToString(), out List<GatheringNode> node)) {
                                        GatheringNode nodeMatch = node.FirstOrDefault(n => n.Localization.Matches(actorEntity.Name));
                                        if (nodeMatch != null) {
                                            switch (user.Job) {
                                                case Actor.Job.BTN:
                                                    actorIcon = Game.Harvesting;
                                                    switch (nodeMatch.Rarity) {
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
                                                    switch (nodeMatch.Rarity) {
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

                                    this.DrawImage(drawingContext, actorIcon, new Rect(new Point(screen.X, screen.Y), new Size(16, 16)));

                                    this.RenderDebugInformation(actorEntity, ref sb);

                                    if (Settings.Default.GatheringShowName || Settings.Default.GatheringShowHPPercent) {
                                        var text = new FormattedText(sb.ToString(), this._cultureInfo, this._flowDirection, this._typeface, int.Parse(Settings.Default.GatheringFontSize) + fontSizeModifier, (SolidColorBrush) bc.ConvertFromString(Settings.Default.GatheringFontColor));
                                        this.DrawText(drawingContext, text, screen.X + 20, screen.Y);
                                    }
                                }
                                catch (Exception ex) {
                                    Logging.Log(Logger, new LogItem(ex, true));
                                }

                                drawingContext.Pop();
                            }

                            #endregion

                            break;
                        default:

                            #region Resolve Other

                            if (Settings.Default.OtherShow) {
                                try {
                                    switch (actorEntity.Type) {
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

                                    if (actorEntity.HPCurrent > 0 || actorEntity.Type == Actor.Type.Aetheryte) {
                                        if (actorIcon != null) {
                                            this.DrawImage(drawingContext, actorIcon, new Rect(new Point(screen.X, screen.Y), new Size(16, 16)));
                                        }
                                    }
                                    else {
                                        this.DrawImage(drawingContext, Game.Unknown, new Rect(new Point(screen.X, screen.Y), new Size(16, 16)));
                                    }

                                    if (Settings.Default.OtherShowName) {
                                        sb.Append(actorEntity.Name);
                                    }

                                    if (Settings.Default.OtherShowHPPercent) {
                                        sb.AppendFormat(" {0:P0}", actorEntity.HPPercent);
                                    }

                                    if (Settings.Default.OtherShowDistance) {
                                        sb.AppendFormat(" {0:N2} {1}", user.GetDistanceTo(actorEntity), this.ResolveHeightVariance(user, actorEntity));
                                    }

                                    this.RenderDebugInformation(actorEntity, ref sb);

                                    if (Settings.Default.OtherShowName || Settings.Default.OtherShowHPPercent) {
                                        var text = new FormattedText(sb.ToString(), this._cultureInfo, this._flowDirection, this._typeface, int.Parse(Settings.Default.OtherFontSize) + fontSizeModifier, (SolidColorBrush) bc.ConvertFromString(Settings.Default.OtherFontColor));
                                        this.DrawText(drawingContext, text, screen.X + 20, screen.Y);
                                    }
                                }
                                catch (Exception ex) {
                                    Logging.Log(Logger, new LogItem(ex, true));
                                }

                                drawingContext.Pop();
                            }

                            #endregion

                            break;
                    }
                }
                catch (Exception ex) { }
            }

            #endregion
        }

        private void DrawDrawing(DrawingContext context, DrawingGroup group) {
            context.DrawDrawing(group);
        }

        private void DrawImage(DrawingContext context, BitmapImage image, double x, double y, double width, double height) {
            context.DrawImage(image, new Rect(new Point(x, y), new Size(width, height)));
        }

        private void DrawImage(DrawingContext context, BitmapImage image, Rect rect) {
            context.DrawImage(image, rect);
        }

        private void DrawText(DrawingContext context, FormattedText text, double x, double y) {
            context.DrawText(text, new Point(x, y));
        }

        private void RenderDebugInformation(ActorItem actorEntity, ref StringBuilder sb) {
            if (Settings.Default.ShowEntityDebug) {
                

                sb.Append($"{Environment.NewLine}NPCID1: {actorEntity.NPCID1:X}");
                sb.Append($"{Environment.NewLine}NPCID2: {actorEntity.NPCID2:X}");
                sb.Append($"{Environment.NewLine}ModelID: {actorEntity.ModelID:X}");

                
            }
        }

        private string ResolveHeightVariance(ActorItem user, ActorItem actorEntity) {
            var modifier = string.Empty;
            if (user.Z < actorEntity.Z) {
                modifier = "+";
            }

            if (user.Z > actorEntity.Z) {
                modifier = "-";
            }

            return $"{modifier}{Math.Abs(this.ResolveHeightVarianceDecimal(user, actorEntity)):N2}";
        }

        private decimal ResolveHeightVarianceDecimal(ActorItem user, ActorItem actorEntity) {
            double variance = 0;
            if (user.Z < actorEntity.Z) {
                variance = user.Z - actorEntity.Z;
            }

            if (user.Z > actorEntity.Z) {
                variance = actorEntity.Z - user.Z;
            }

            return (decimal) variance;
        }
    }
}