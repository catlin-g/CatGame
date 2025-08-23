using CatGame.CatDetails;
using CatGame.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended;
using System;

namespace CatGame
{
	public class CatGame : Game
	{
		readonly GraphicsDeviceManager _graphics;
		SpriteBatch _spriteBatch;
		readonly Cat cat;

		const int tilesize = 32;
		const int mapTilesetWidthInTiles = 24;
		const int mapTilesetHeightInTiles = 21;
		int[,] map;

		Rectangle mapBounds = new(0, 0, mapTilesetWidthInTiles * tilesize, mapTilesetHeightInTiles * tilesize);
		Rectangle tilesetBounds = new((mapTilesetWidthInTiles * tilesize) + 32, 0, mapTilesetWidthInTiles * tilesize, mapTilesetHeightInTiles * tilesize);

		int SelectedTile;

		public CatGame()
		{

			_graphics = new GraphicsDeviceManager(this);

			Window.AllowUserResizing = true;
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
			cat = new Cat();
		}

		protected override void Initialize()
		{
			_graphics.PreferredBackBufferWidth = 1920;
			_graphics.PreferredBackBufferHeight = 1080;
			_graphics.ApplyChanges();

			map = new int[mapTilesetWidthInTiles, mapTilesetHeightInTiles];
			for (var y = 0; y < mapTilesetHeightInTiles; ++y)
			{
				for (var x = 0; x < mapTilesetWidthInTiles; ++x)
				{
					map[x, y] = 179;
				}
			}

			base.Initialize();
		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);

			GameServices.Textures.Add("map", Content.Load<Texture2D>("Textures\\InteriorSpriteSheet"));

			GameServices.Songs.Add("song1", Content.Load<Song>("Songs\\FunnyCat"));
			GameServices.Songs.Add("song2", Content.Load<Song>("Songs\\MitchiriNekoMarch"));

			GameServices.Fonts.Add("Calibri8", Content.Load<SpriteFont>("Fonts\\Calibri8"));
			GameServices.Fonts.Add("Calibri12", Content.Load<SpriteFont>("Fonts\\Calibri12"));

			cat.LoadContent(Content);
		}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
			{
				Exit();
			}

			var songToPlay = Random.Shared.Next(2) == 0 ? "song1" : "song2";
			GameServices.SongPlayer.Volume = 0.05f;
			GameServices.SongPlayer.PlayNewSong(songToPlay);

			cat.Update(gameTime);

			var mouseState = Mouse.GetState();
			if (mouseState.LeftButton == ButtonState.Pressed)
			{
				// if tileset, set SelectedTile
				if (mapBounds.Contains(mouseState.Position))
				{
					var x = mouseState.Position.X / tilesize;
					var y = mouseState.Position.Y / tilesize;
					map[x, y] = SelectedTile;
				}

				// if click on map, set map tile to SelectedTile
				if (tilesetBounds.Contains(mouseState.Position))
				{
					var tilesetXY = new Point((mouseState.Position.X - tilesetBounds.X) / tilesize, (mouseState.Position.Y - tilesetBounds.Y) / tilesize);
					SelectedTile = tilesetXY.X + (tilesetXY.Y * mapTilesetWidthInTiles);
				}
			}

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.BlueViolet);

			_spriteBatch.Begin(samplerState: SamplerState.PointClamp);

			var mapTex = GameServices.Textures["map"];

			// draw map
			for (var y = 0; y < map.GetLength(1); ++y)
			{
				for (var x = 0; x < map.GetLength(0); ++x)
				{
					var tile = map[x, y];
					var tileInMapX = tile % mapTilesetWidthInTiles;
					var tileInMapY = tile / mapTilesetWidthInTiles;

					var srcRect = new Rectangle(tileInMapX * 32, tileInMapY * 32, 32, 32);
					var dstRect = new Rectangle(x * 32, y * 32, 32, 32);

					_spriteBatch.Draw(mapTex, dstRect, srcRect, Color.White);

					//_spriteBatch.DrawString(
					//	GameServices.Fonts["Calibri8"],
					//	$"{tile}",
					//	new Vector2(dstRect.X, dstRect.Y) + Vector2.One,
					//	Color.Black,
					//	0f,
					//	Vector2.Zero,
					//	1f,
					//	SpriteEffects.None,
					//	0);
				}
			}

			_spriteBatch.DrawRectangle(new RectangleF(0, 0, map.GetLength(0) * 32, map.GetLength(1) * 32), Color.Black, 1);

			// draw cat
			cat.Draw(_spriteBatch);

			// draw tileset
			_spriteBatch.Draw(mapTex, new Rectangle(tilesetBounds.X, 0, mapTex.Width, mapTex.Height), Color.White);

			// draw tileset grid
			var counter = 0;
			for (var y = 0; y < map.GetLength(1); ++y)
			{
				for (var x = 0; x < map.GetLength(0); ++x)
				{
					//var srcRect = new Rectangle(tileInMapX * 32, tileInMapY * 32, 32, 32);
					var dstRect = new Rectangle(tilesetBounds.X + (x * tilesize), tilesetBounds.Y + (y * tilesize), tilesize, tilesize);

					//_spriteBatch.Draw(mapTex, dstRect, srcRect, Color.White);

					_spriteBatch.DrawString(
						GameServices.Fonts["Calibri8"],
						$"{counter++}",
						new Vector2(dstRect.X, dstRect.Y) + Vector2.One,
						Color.Black,
						0f,
						Vector2.Zero,
						1f,
						SpriteEffects.None,
						0);

					_spriteBatch.DrawRectangle(dstRect, Color.Black);
				}
			}

			var srcRect2 = new Rectangle(SelectedTile / mapTilesetWidthInTiles * tilesize, SelectedTile % mapTilesetWidthInTiles * tilesize, tilesize, tilesize);
			var dstRect2 = new Rectangle(0, mapBounds.Height + tilesize, tilesize, tilesize);
			_spriteBatch.Draw(mapTex, dstRect2, srcRect2, Color.White);

			_spriteBatch.DrawString(GameServices.Fonts["Calibri8"], SelectedTile.ToString(), dstRect2.Location.ToVector2() + Vector2.One, Color.Black);

			_spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
