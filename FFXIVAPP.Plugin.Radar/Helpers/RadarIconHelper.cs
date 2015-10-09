﻿// FFXIVAPP.Plugin.Radar
// RadarIconHelper.cs
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

using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FFXIVAPP.Plugin.Radar.Helpers
{
    public static class RadarIconHelper
    {
        public static ImageSource Alchemist = new BitmapImage(new Uri(Constants.LibraryPack + "/Media/RadarIcons/alchemist.png"));
        public static ImageSource Arcanist = new BitmapImage(new Uri(Constants.LibraryPack + "/Media/RadarIcons/arcanist.png"));
        public static ImageSource Archer = new BitmapImage(new Uri(Constants.LibraryPack + "/Media/RadarIcons/archer.png"));
        public static ImageSource Armorer = new BitmapImage(new Uri(Constants.LibraryPack + "/Media/RadarIcons/armorer.png"));
        public static ImageSource Astrologian = new BitmapImage(new Uri(Constants.LibraryPack + "/Media/RadarIcons/astrologian.png"));
        public static ImageSource Bard = new BitmapImage(new Uri(Constants.LibraryPack + "/Media/RadarIcons/bard.png"));
        public static ImageSource Blackmage = new BitmapImage(new Uri(Constants.LibraryPack + "/Media/RadarIcons/blackmage.png"));
        public static ImageSource Blacksmith = new BitmapImage(new Uri(Constants.LibraryPack + "/Media/RadarIcons/blacksmith.png"));
        public static ImageSource Boat = new BitmapImage(new Uri(Constants.LibraryPack + "/Media/RadarIcons/boat.png"));
        public static ImageSource Boss = new BitmapImage(new Uri(Constants.LibraryPack + "/Media/RadarIcons/boss.png"));
        public static ImageSource Botanist = new BitmapImage(new Uri(Constants.LibraryPack + "/Media/RadarIcons/botanist.png"));
        public static ImageSource BronzeChest = new BitmapImage(new Uri(Constants.LibraryPack + "/Media/RadarIcons/bronze_chest.png"));
        public static ImageSource Carpenter = new BitmapImage(new Uri(Constants.LibraryPack + "/Media/RadarIcons/carpenter.png"));
        public static ImageSource Chocobo = new BitmapImage(new Uri(Constants.LibraryPack + "/Media/RadarIcons/chocobo.png"));
        public static ImageSource ChocoboPersonal = new BitmapImage(new Uri(Constants.LibraryPack + "/Media/RadarIcons/chocobo_personal.png"));
        public static ImageSource Conjurer = new BitmapImage(new Uri(Constants.LibraryPack + "/Media/RadarIcons/conjurer.png"));
        public static ImageSource Crystal = new BitmapImage(new Uri(Constants.LibraryPack + "/Media/RadarIcons/crystal.png"));
        public static ImageSource Culinarian = new BitmapImage(new Uri(Constants.LibraryPack + "/Media/RadarIcons/culinarian.png"));
        public static ImageSource Dragoon = new BitmapImage(new Uri(Constants.LibraryPack + "/Media/RadarIcons/dragoon.png"));
        public static ImageSource DarkKnight = new BitmapImage(new Uri(Constants.LibraryPack + "/Media/RadarIcons/darkknight.png"));
        public static ImageSource Dungeon = new BitmapImage(new Uri(Constants.LibraryPack + "/Media/RadarIcons/dungeon.png"));
        public static ImageSource Fate = new BitmapImage(new Uri(Constants.LibraryPack + "/Media/RadarIcons/fate.png"));
        public static ImageSource Fate2 = new BitmapImage(new Uri(Constants.LibraryPack + "/Media/RadarIcons/fate2.png"));
        public static ImageSource Fate3 = new BitmapImage(new Uri(Constants.LibraryPack + "/Media/RadarIcons/fate3.png"));
        public static ImageSource Field = new BitmapImage(new Uri(Constants.LibraryPack + "/Media/RadarIcons/field.png"));
        public static ImageSource Field2 = new BitmapImage(new Uri(Constants.LibraryPack + "/Media/RadarIcons/field2.png"));
        public static ImageSource Fisher = new BitmapImage(new Uri(Constants.LibraryPack + "/Media/RadarIcons/fisher.png"));
        public static ImageSource Gladiator = new BitmapImage(new Uri(Constants.LibraryPack + "/Media/RadarIcons/gladiator.png"));
        public static ImageSource Goldsmith = new BitmapImage(new Uri(Constants.LibraryPack + "/Media/RadarIcons/goldsmith.png"));
        public static ImageSource GoldChest = new BitmapImage(new Uri(Constants.LibraryPack + "/Media/RadarIcons/gold_chest.png"));
        public static ImageSource Lancer = new BitmapImage(new Uri(Constants.LibraryPack + "/Media/RadarIcons/lancer.png"));
        public static ImageSource Leatherworker = new BitmapImage(new Uri(Constants.LibraryPack + "/Media/RadarIcons/leatherworker.png"));
        public static ImageSource Mail = new BitmapImage(new Uri(Constants.LibraryPack + "/Media/RadarIcons/mail.png"));
        public static ImageSource Machinist = new BitmapImage(new Uri(Constants.LibraryPack + "/Media/RadarIcons/machinist.png"));
        public static ImageSource Marauder = new BitmapImage(new Uri(Constants.LibraryPack + "/Media/RadarIcons/marauder.png"));
        public static ImageSource Merchant = new BitmapImage(new Uri(Constants.LibraryPack + "/Media/RadarIcons/merchant.png"));
        public static ImageSource Merchant2 = new BitmapImage(new Uri(Constants.LibraryPack + "/Media/RadarIcons/merchant2.png"));
        public static ImageSource Miner = new BitmapImage(new Uri(Constants.LibraryPack + "/Media/RadarIcons/miner.png"));
        public static ImageSource Mining = new BitmapImage(new Uri(Constants.LibraryPack + "/Media/RadarIcons/mining.png"));
        public static ImageSource Monk = new BitmapImage(new Uri(Constants.LibraryPack + "/Media/RadarIcons/monk.png"));
        public static ImageSource Monster = new BitmapImage(new Uri(Constants.LibraryPack + "/Media/RadarIcons/monster.png"));
        public static ImageSource MonsterClaimed = new BitmapImage(new Uri(Constants.LibraryPack + "/Media/RadarIcons/monster_claimed.png"));
        public static ImageSource NPC = new BitmapImage(new Uri(Constants.LibraryPack + "/Media/RadarIcons/npc.png"));
        public static ImageSource Ninja = new BitmapImage(new Uri(Constants.LibraryPack + "/Media/RadarIcons/ninja.png"));
        public static ImageSource Paladin = new BitmapImage(new Uri(Constants.LibraryPack + "/Media/RadarIcons/paladin.png"));
        public static ImageSource Player = new BitmapImage(new Uri(Constants.LibraryPack + "/Media/RadarIcons/player.png"));
        public static ImageSource Pugilist = new BitmapImage(new Uri(Constants.LibraryPack + "/Media/RadarIcons/pugilist.png"));
        public static ImageSource RadarHeading = new BitmapImage(new Uri(Constants.LibraryPack + "/Media/RadarIcons/radar_heading.png"));
        public static ImageSource Rogue = new BitmapImage(new Uri(Constants.LibraryPack + "/Media/RadarIcons/rogue.png"));
        public static ImageSource Scholar = new BitmapImage(new Uri(Constants.LibraryPack + "/Media/RadarIcons/scholar.png"));
        public static ImageSource Sheep = new BitmapImage(new Uri(Constants.LibraryPack + "/Media/RadarIcons/sheep.png"));
        public static ImageSource SilverChest = new BitmapImage(new Uri(Constants.LibraryPack + "/Media/RadarIcons/silver_chest.png"));
        public static ImageSource Skull = new BitmapImage(new Uri(Constants.LibraryPack + "/Media/RadarIcons/skull.png"));
        public static ImageSource Summoner = new BitmapImage(new Uri(Constants.LibraryPack + "/Media/RadarIcons/summoner.png"));
        public static ImageSource Thaumaturge = new BitmapImage(new Uri(Constants.LibraryPack + "/Media/RadarIcons/thaumaturge.png"));
        public static ImageSource Town = new BitmapImage(new Uri(Constants.LibraryPack + "/Media/RadarIcons/town.png"));
        public static ImageSource Warrior = new BitmapImage(new Uri(Constants.LibraryPack + "/Media/RadarIcons/warrior.png"));
        public static ImageSource Weaver = new BitmapImage(new Uri(Constants.LibraryPack + "/Media/RadarIcons/weaver.png"));
        public static ImageSource Whitemage = new BitmapImage(new Uri(Constants.LibraryPack + "/Media/RadarIcons/whitemage.png"));
        public static ImageSource Wood = new BitmapImage(new Uri(Constants.LibraryPack + "/Media/RadarIcons/wood.png"));
        public static ImageSource Zone = new BitmapImage(new Uri(Constants.LibraryPack + "/Media/RadarIcons/zone.png"));
    }
}
