// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PluginException.cs" company="SyndicatedLife">
//   Copyright(c) 2018 Ryan Wilson &amp;lt;syndicated.life@gmail.com&amp;gt; (http://syndicated.life/)
//   Licensed under the MIT license. See LICENSE.md in the solution root for full license information.
// </copyright>
// <summary>
//   PluginException.cs Implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FFXIVAPP.Plugin.Radar {
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class PluginException : Exception {
        public PluginException() { }

        public PluginException(string message)
            : base(message) { }

        public PluginException(string message, Exception inner)
            : base(message, inner) { }

        protected PluginException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}