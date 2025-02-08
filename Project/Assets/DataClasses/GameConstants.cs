using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Assets.DataClasses
{
	public static class GameConstants
	{
		public const int GameSpaceId = 1;
		public const int GameSpaceWidth = 1920;
		public const int GameSpaceHeight = 1064;
		public const int UIUpdateIntervalMs = 6;
		public const int TimeUpdateIntervalSec = 1;

		public const double DefaultCharacterPositionX = 960;
		public const double DefaultCharacterPositionY = 532;

		public const double EasyMultiplier = 2.0;
		public const double NormalMultiplier = 1.0;
		public const double HardMultiplier = 0.5;

		public const string EasyBackgroundUri = "pack://application:,,,/Assets/Textures/background-easy.png";
		public const string NormalBackgroundUri = "pack://application:,,,/Assets/Textures/background-normal.png";
		public const string HardBackgroundUri = "pack://application:,,,/Assets/Textures/background-hard.png";
	}
}
