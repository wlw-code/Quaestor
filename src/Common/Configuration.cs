using System;
using Discord;

namespace Quaestor.Common
{
    public class Configuration
    {
        public static readonly string Prefix = ">",
            Version = "0.0.4",
            Game = $"{Prefix}help | v{Version}";

        public static Color GetRandomColor()
        {
            return Colors[Randomizer.Next(1, Colors.Length) - 1];
        }

        public static readonly Random Randomizer = new Random();

        public static readonly Color BanColor = new Color(255, 0, 0),
            KickColor = new Color(255, 140, 25),
            MuteColor = KickColor,
            UnmuteColor = new Color(85, 255, 0),
            ClearColor = UnmuteColor,
            WarnColor = KickColor,
            ErrorColor = BanColor;

        private static readonly Color[] Colors = {
            new Color(255, 38, 154),
            new Color(0, 255, 0),
            new Color(0, 232, 40),
            new Color(8, 248, 255),
            new Color(242, 38, 255),
            new Color(255, 28, 142),
            new Color(104, 255, 34),
            new Color(255, 190, 17),
            new Color(41, 84, 255),
            new Color(150, 36, 237),
            new Color(168, 237, 0)
        };
    }
}
