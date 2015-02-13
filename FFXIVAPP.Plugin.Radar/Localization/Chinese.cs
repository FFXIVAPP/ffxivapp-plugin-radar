// FFXIVAPP.Plugin.Radar
// Chinese.cs
// 
// Copyright © 2007 - 2015 Ryan Wilson - All Rights Reserved
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

using System.Windows;

namespace FFXIVAPP.Plugin.Radar.Localization
{
    public abstract class Chinese
    {
        private static readonly ResourceDictionary Dictionary = new ResourceDictionary();

        /// <summary>
        /// </summary>
        /// <returns> </returns>
        public static ResourceDictionary Context()
        {
            Dictionary.Clear();
            Dictionary.Add("radar_", "*PH*");
            Dictionary.Add("radar_RadarWidgetHeader", "雷达小工具");
            Dictionary.Add("radar_OpenNowButtonText", "现在打开");
            Dictionary.Add("radar_ResetPositionButtonText", "重设设置");
            Dictionary.Add("radar_EnableClickThroughHeader", "在雷达上开启 Click-Through");
            Dictionary.Add("radar_WidgetOpacityHeader", "小工具不透明");
            Dictionary.Add("radar_ShowTitlesOnRadarHeader", "在雷达上显示注释");
            Dictionary.Add("radar_UIScaleHeader", "界面规模");
            Dictionary.Add("radar_RadarSettingsTabHeader", "雷达设置");
            Dictionary.Add("radar_PCShowHeader", "PC 显示");
            Dictionary.Add("radar_PCShowNameHeader", "PC 显示名称");
            Dictionary.Add("radar_PCShowHPPercentHeader", "PC 显示 HP Percent");
            Dictionary.Add("radar_PCShowJobHeader", "PC 显示工作");
            Dictionary.Add("radar_PCShowDistanceHeader", "PC 显示距离");
            Dictionary.Add("radar_NPCShowHeader", "NPC 显示");
            Dictionary.Add("radar_NPCShowNameHeader", "NPC 显示名称");
            Dictionary.Add("radar_NPCShowHPPercentHeader", "NPC 显示 HP Percent");
            Dictionary.Add("radar_NPCShowDistanceHeader", "NPC 显示距离");
            Dictionary.Add("radar_MonsterShowHeader", "Monster 显示");
            Dictionary.Add("radar_MonsterShowNameHeader", "Monster 显示名称");
            Dictionary.Add("radar_MonsterShowHPPercentHeader", "Monster 显示 HP Percent");
            Dictionary.Add("radar_MonsterShowDistanceHeader", "Monster 显示距离");
            Dictionary.Add("radar_GatheringShowHeader", "Gathering 显示");
            Dictionary.Add("radar_GatheringShowNameHeader", "Gathering 显示名称");
            Dictionary.Add("radar_GatheringShowHPPercentHeader", "Gathering 显示 HP Percent");
            Dictionary.Add("radar_GatheringShowDistanceHeader", "Gathering 显示距离");
            Dictionary.Add("radar_OtherShowHeader", "其他显示");
            Dictionary.Add("radar_OtherShowNameHeader", "其他显示名称");
            Dictionary.Add("radar_OtherShowHPPercentHeader", "其他显示 HP Percent");
            Dictionary.Add("radar_OtherShowDistanceHeader", "其他显示距离");
            Dictionary.Add("radar_GitHubButtonText", "打开项目源代码(GitHub)");
            return Dictionary;
        }
    }
}
