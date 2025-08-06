using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;

namespace CatGame.Services
{
	public class SoundEffectPlayer
	{
		Dictionary<string, SoundEffectInstance> Playing = [];

		public void PlaySound(string soundName, float volume = 0.2f, float pitch = 0f, float pan = 0.5f, bool allowOverlap = true)
		{
			if (GameServices.SoundEffects.TryGetValue(soundName, out var value))
			{
				if (Playing.TryGetValue(soundName, out var instance))
				{
					// If the sound is already playing and overlap is not allowed, return
					if (!allowOverlap && instance.State == SoundState.Playing)
					{
						return;
					}
				}

				// Create a new instance if it doesn't exist
				instance = value.CreateInstance();
				Playing[soundName] = instance;

				instance.Volume = volume;
				instance.Pitch = pitch;
				instance.Pan = pan;
				instance.Play();
			}
		}
	}
}
