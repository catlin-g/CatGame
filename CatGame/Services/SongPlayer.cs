using Microsoft.Xna.Framework.Media;

namespace CatGame.Services
{
	public class SongPlayer
	{
		public float Volume
		{
			get => MediaPlayer.Volume;
			set => MediaPlayer.Volume = value;
		}

		public void PlaySong(string songName)
		{
			if (GameServices.Songs.TryGetValue(songName, out var value))
			{
				MediaPlayer.Stop();
				MediaPlayer.Play(value);
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
			=> MediaPlayer.Stop();
	}
}
