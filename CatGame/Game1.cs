using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace CatGame
{
	public class Game1 : Game
	{
		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;
		private Cat cat;
		public Game1()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
			cat = new Cat();
		}

		protected override void Initialize()
		{
			// TODO: Add your initialization logic here

			base.Initialize();
		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);

			GameServices.Songs.Add("song1", Content.Load<Song>("Songs\\FunnyCat"));
			GameServices.Songs.Add("song2", Content.Load<Song>("Songs\\MitchiriNekoMarch"));

			GameServices.SoundEffects.Add("cat1", Content.Load<SoundEffect>("SFX\\cathungrymeow45"));
			GameServices.SoundEffects.Add("cat2", Content.Load<SoundEffect>("SFX\\catpainmeow87"));
			GameServices.SoundEffects.Add("cat3", Content.Load<SoundEffect>("SFX\\kittymeow93"));

			cat.LoadContent(Content);
		}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
			{
				Exit();
			}

			GameServices.SongPlayer.PlayNewSong("song2");

			// TODO: Add your update logic here
			cat.Update(gameTime);

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.BlueViolet);

			_spriteBatch.Begin(samplerState: SamplerState.PointClamp);
			// TODO: Add your drawing code here
			cat.Draw(_spriteBatch);

			_spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
