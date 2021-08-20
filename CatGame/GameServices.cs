using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace CatGame
{
	public static class GameServices
	{
		public static SongPlayer SongPlayer = new ();
		public static SoundEffectPlayer SoundEffectPlayer = new();

		public static Dictionary<string, SpriteFont> Fonts = new();
		public static Dictionary<string, Texture2D> Textures = new();
		public static Dictionary<string, SoundEffect> SoundEffects = new();
		public static Dictionary<string, Song> Songs = new();
	}

	public class SoundEffectPlayer
	{
		public void PlaySound(string soundName, float volume = 0.2f, float pitch = 0f, float pan = 0.5f)
		{
			if (GameServices.SoundEffects.ContainsKey(soundName))
			{
				GameServices.SoundEffects[soundName].Play(volume, pitch, pan);
			}
		}
	}

	public class SongPlayer
	{
		public void PlaySong(string songName)
		{
			if (GameServices.Songs.ContainsKey(songName))
			{
				MediaPlayer.Stop();
				MediaPlayer.Play(GameServices.Songs[songName]);
				MediaPlayer.Volume = 0.5f;
			}
		}

		public void PlayNewSong(string songName)
		{

			if (MediaPlayer.State != MediaState.Playing)
			{
				PlaySong(songName);
			}
		}

		public void StopSong()
		{
			MediaPlayer.Stop();
		}
	}
}
