using System.Collections.Generic;

namespace L5aStrat_Earth.Entities
{
    public static class References
    {
        public static readonly string coordinatesLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public static readonly string[] FormationList = 
        { 
            "Choki", 
            "Guu", 
            "Paa" 
        };

        public static readonly string[] UnitNamesList =
        {
            "Groupe",
            "Bataillon",
            "Brigade",
            "Régiment",
            "Escadron",
            "Section"
        };

        public static readonly string[] ArmyNamesList =
        {
            "Rokugan",
            "Empire",
            "Hégémonie",
            "Domination",
            "Conquête",
            "Percée",
            "Fracas",
            "Choc",
            "Katana",
            "Wakizashi",
            "Naginata",
            "Kabuto",
            "Yumi",
            "Tanto",
            "Yari",
            "Honneur",
            "Volonté",
            "Loyauté",
            "Rectitude",
            "Sincérité",
            "Bushido"
        };

        public static readonly Clan[] ClansList =
        {
            new Clan()
            {
                Name = "Crabe",
                Colors = new List<string>() { "blue", "#35a" },
                FamilyNames = new List<string>() { "Hida", "Hiruma", "Kuni", "Kaiu", "Yasuki", "Toritaka" },
                Symbol = "Crab-mon"
            },
            new Clan()
            {
                Name = "Dragon",
                Colors = new List<string>() { "green", "#484" },
                FamilyNames = new List<string>() { "Togashi", "Mirumoto", "Kitsuki", "Tamori" },
                Symbol = "Dragon-mon"
            },
            new Clan()
            {
                Name = "Grue",
                Colors = new List<string>() { "cyan", "#0cf" },
                FamilyNames = new List<string>() { "Doji", "Kakita", "Daidoji", "Asahina" },
                Symbol = "Crane-mon"
            },
            new Clan()
            {
                Name = "Licorne",
                Colors = new List<string>() { "mediumpurple", "mediumorchid" },
                FamilyNames = new List<string>() { "Shinjo", "Moto", "Ide", "Iuchi", "Utaku" },
                Symbol = "Unicorn-mon"
            },
            new Clan()
            {
                Name = "Lion",
                Colors = new List<string>() { "yellow", "gold" },
                FamilyNames = new List<string>() { "Akodo", "Matsu", "Kitsu", "Ikoma" },
                Symbol = "Lion-mon"
            },
            new Clan()
            {
                Name = "Phénix",
                Colors = new List<string>() { "darkorange", "orangered" },
                FamilyNames = new List<string>() { "Isawa", "Shiba", "Agasha", "Asako" },
                Symbol = "Mantis-mon"
            },
            new Clan()
            {
                Name = "Scorpion",
                Colors = new List<string>() { "red", "crimson" },
                FamilyNames = new List<string>() { "Bayushi", "Shosuro", "Soshi", "Yogo" },
                Symbol = "Scorpion-mon"
            },
            new Clan()
            {
                Name = "Mante",
                Colors = new List<string>() { "lime", "greenyellow" },
                FamilyNames = new List<string>() { "Yoritomo", "Tsuruchi", "Moshi", "Kitsune" },
                Symbol = "Mantis-mon"
            },
            new Clan()
            {
                Name = "Autres",
                Colors = new List<string>() { "indigo", "saddlebrown", "olive", "indianred", "teal", "rosybrown" },
                FamilyNames = new List<string>() { "Ichiro", "Kasuga", "Morito", "Suzume", "Tonbo", "Toku", "Usagi", "Daigotsu", "Chuda", "Goju", "[Moine/Ronin]" },
                Symbol = ""
            },
        };

        public static readonly string[] FirstNamesList =
        {
            "Akaboshi",
            "Atsuyori",
            "Bunzaburo",
            "Chonosuke",
            "Etsuji",
            "Fujima",
            "Fujimoto",
            "Fuyu",
            "Harufumi",
            "Hidetsugu",
            "Hirabayashi",
            "Hitoyo",
            "Hiyoriko",
            "Ichikawa",
            "Ikino",
            "Isomura",
            "Itsuto",
            "Kanekawa",
            "Katsuyo",
            "Kawamura",
            "Kitami",
            "Komuro",
            "Matsuno",
            "Mitsumori",
            "Mitsutaro",
            "Monzaburo",
            "Murakami",
            "Nagahashi",
            "Naoka",
            "Nishiya",
            "Okura",
            "Otara",
            "Ozaki",
            "Saijo",
            "Sakairi",
            "Shigeyori",
            "Sugimura",
            "Tadane",
            "Taizen",
            "Takamiya",
            "Takayori",
            "Tateno",
            "Tatsumaru",
            "Tomomitsu",
            "Tomonari",
            "Tsuji",
            "Urabe",
            "Utsuko",
            "Yokosuka",
            "Yoneda"
        };
    }
}
