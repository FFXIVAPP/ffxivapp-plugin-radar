// FFXIVAPP.Plugin.Radar
// PluginException.cs
// 
// Created by Ryan Wilson.
// 
// Copyright © 2014 - 2014 Ryan Wilson - All Rights Reserved

using System;
using System.Runtime.Serialization;

namespace FFXIVAPP.Plugin.Radar
{
    [Serializable]
    public class PluginException : Exception
    {
        public PluginException()
        {
        }

        public PluginException(string message) : base(message)
        {
        }

        public PluginException(string message, Exception inner) : base(message, inner)
        {
        }

        protected PluginException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
