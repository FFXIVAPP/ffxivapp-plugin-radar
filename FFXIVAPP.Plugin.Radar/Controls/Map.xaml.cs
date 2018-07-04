// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Map.xaml.cs" company="SyndicatedLife">
//   Copyright(c) 2018 Ryan Wilson &amp;lt;syndicated.life@gmail.com&amp;gt; (http://syndicated.life/)
//   Licensed under the MIT license. See LICENSE.md in the solution root for full license information.
// </copyright>
// <summary>
//   Map.xaml.cs Implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FFXIVAPP.Plugin.Radar.Controls {
    using System.Windows.Media;

    /// <summary>
    ///     Interaction logic for Radar.xaml
    /// </summary>
    public partial class Map {
        public Map View;

        public Map() {
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
        }
    }
}