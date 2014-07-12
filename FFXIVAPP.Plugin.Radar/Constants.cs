// FFXIVAPP.Plugin.Radar
// Constants.cs
// 
// Copyright © 2007 - 2014 Ryan Wilson - All Rights Reserved
// 
// Redistribution and use in source and binary forms, with or without 
// modification, are permitted provided that the following conditions are met: 
// 
//  * Redistributions of source code must retain the above copyright notice, 
//    this list of conditions and the following disclaimer. 
//  * Redistributions in binary form must reproduce the above copyright 
//    notice, this list of conditions and the following disclaimer in the 
//    documentation and/or other materials provided with the distribution. 
//  * Neither the name of SyndicatedLife nor the names of its contributors may 
//    be used to endorse or promote products derived from this software 
//    without specific prior written permission. 
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" 
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE 
// IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE 
// ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE 
// LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR 
// CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF 
// SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS 
// INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN 
// CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
// ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
// POSSIBILITY OF SUCH DAMAGE. 

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Xml.Linq;
using FFXIVAPP.Common.Helpers;
using FFXIVAPP.Plugin.Radar.Models;

namespace FFXIVAPP.Plugin.Radar
{
    public static class Constants
    {
        #region Declarations

        public const string LibraryPack = "pack://application:,,,/FFXIVAPP.Plugin.Radar;component/";

        public static readonly string[] Supported =
        {
            "ja", "fr", "en", "de"
        };

        public static string BaseDirectory
        {
            get
            {
                var appDirectory = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly()
                                                                         .CodeBase).LocalPath);
                return Path.Combine(appDirectory, "Plugins", Plugin.PName);
            }
        }

        #endregion

        #region Property Bindings

        private static XDocument _xSettings;
        private static List<string> _settings;
        private static XDocument _xFilters;
        private static List<RadarFilterItem> _filters;

        public static XDocument XSettings
        {
            get
            {
                var file = Path.Combine(Common.Constants.PluginsSettingsPath, "FFXIVAPP.Plugin.Radar.xml");
                var legacyFile = "./Plugins/FFXIVAPP.Plugin.Radar/Settings.xml";
                if (_xSettings != null)
                {
                    return _xSettings;
                }
                try
                {
                    var found = File.Exists(file);
                    if (found)
                    {
                        _xSettings = XDocument.Load(file);
                    }
                    else
                    {
                        found = File.Exists(legacyFile);
                        _xSettings = found ? XDocument.Load(legacyFile) : ResourceHelper.XDocResource(LibraryPack + "/Defaults/Settings.xml");
                    }
                }
                catch (Exception ex)
                {
                    _xSettings = ResourceHelper.XDocResource(LibraryPack + "/Defaults/Settings.xml");
                }
                return _xSettings;
            }
            set { _xSettings = value; }
        }

        public static List<string> Settings
        {
            get { return _settings ?? (_settings = new List<string>()); }
            set { _settings = value; }
        }

        public static XDocument XFilters
        {
            get
            {
                var file = Path.Combine(Common.Constants.PluginsSettingsPath, "FFXIVAPP.Plugin.Radar.Filters.xml");
                if (_xFilters != null)
                {
                    return _xFilters;
                }
                try
                {
                    var found = File.Exists(file);
                    _xFilters = found ? XDocument.Load(file) : ResourceHelper.XDocResource(LibraryPack + "/Defaults/Filters.xml");
                }
                catch (Exception ex)
                {
                    _xFilters = ResourceHelper.XDocResource(LibraryPack + "/Defaults/Filters.xml");
                }
                return _xFilters;
            }
            set { _xFilters = value; }
        }

        public static List<RadarFilterItem> Filters
        {
            get { return _filters ?? (_filters = new List<RadarFilterItem>()); }
            set { _filters = value; }
        }

        #endregion

        #region Property Bindings

        private static Dictionary<string, string> _autoTranslate;
        private static Dictionary<string, string> _chatCodes;
        private static Dictionary<string, string[]> _colors;
        private static CultureInfo _cultureInfo;

        public static Dictionary<string, string> AutoTranslate
        {
            get { return _autoTranslate ?? (_autoTranslate = new Dictionary<string, string>()); }
            set { _autoTranslate = value; }
        }

        public static Dictionary<string, string> ChatCodes
        {
            get { return _chatCodes ?? (_chatCodes = new Dictionary<string, string>()); }
            set { _chatCodes = value; }
        }

        public static string ChatCodesXml { get; set; }

        public static Dictionary<string, string[]> Colors
        {
            get { return _colors ?? (_colors = new Dictionary<string, string[]>()); }
            set { _colors = value; }
        }

        public static CultureInfo CultureInfo
        {
            get { return _cultureInfo ?? (_cultureInfo = new CultureInfo("en")); }
            set { _cultureInfo = value; }
        }

        #endregion

        #region Auto-Properties

        public static string CharacterName { get; set; }

        public static string ServerName { get; set; }

        public static string GameLanguage { get; set; }

        public static bool EnableHelpLabels { get; set; }

        public static string Theme { get; set; }

        #endregion
    }
}
