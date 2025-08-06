using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace CatGame.Services
{
	public static class GameServices
	{
		public static SongPlayer SongPlayer = new();
		public static SoundEffectPlayer SoundEffectPlayer = new();

		public static Dictionary<string, SpriteFont> Fonts = [];
		public static Dictionary<string, Texture2D> Textures = [];
		public static Dictionary<string, SoundEffect> SoundEffects = [];
		public static Dictionary<string, Song> Songs = [];
	}
}
