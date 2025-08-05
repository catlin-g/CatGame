using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace CatGame
{
	public enum DirectionUpDown
	{
		Up,
		None,
		Down,
	}

	public enum DirectionLeftRight
	{
		Left,
		None,
		Right,
	}

	public enum ActionState
	{
		Walk,
		Sit,
		Lie
	}

	public class Cat
	{
		SpriteFont font;
		Texture2D spriteSheet;
		Vector2 position;

		int animationFrame;
		Rectangle currentAnimation;
		int frameCounter;

		Point walkingLeftAnimationFrame;
		Point walkingRightAnimationFrame;
		Point walkingUpAnimationFrame;
		Point walkingDownAnimationFrame;
		Point sittingAnimationFrame;
		Point lyingAnimationFrame;

		TimeSpan timeSitting = TimeSpan.Zero;

		Point frameSize;

		DirectionUpDown directionUpDownState;
		DirectionLeftRight directionLeftRightState;
		ActionState actionState;

		DirectionUpDown previousDirectionUpDownState;
		DirectionLeftRight previousDirectionLeftRightState;
		ActionState previousActionState;
		int previousZoomies = 1;

		int baseSpeed;
		int zoomieSpeed;

		public Cat()
		{
			position = new Vector2(200, 200);

			walkingLeftAnimationFrame = new Point(0, 32);
			walkingRightAnimationFrame = new Point(0, 64);
			walkingUpAnimationFrame = new Point(0, 96);
			walkingDownAnimationFrame = new Point(0, 0);
			sittingAnimationFrame = new Point(196, 128);
			lyingAnimationFrame = new Point(0, 160);

			frameSize = new Point(32, 32);
			baseSpeed = 2;
			zoomieSpeed = 1;

			currentAnimation = new Rectangle(sittingAnimationFrame, frameSize);

			directionUpDownState = DirectionUpDown.Down;
			directionLeftRightState = DirectionLeftRight.Left;
			actionState = ActionState.Sit;
		}

		public void LoadContent(ContentManager content)
		{
			spriteSheet = GameServices.Textures["cat"];
			font = GameServices.Fonts["Calibri12"];
		}

		public void Update(GameTime gameTime)
		{
			var previousPos = position;
			previousActionState = actionState;

			previousDirectionUpDownState = directionUpDownState;
			previousDirectionLeftRightState = directionLeftRightState;

			previousZoomies = zoomieSpeed;

			// user input
			var keyboard = Keyboard.GetState();

			if (keyboard.IsKeyDown(Keys.LeftShift) || keyboard.IsKeyDown(Keys.RightShift))
			{
				zoomieSpeed = 2;

				if (previousZoomies != zoomieSpeed)
				{
					GameServices.SoundEffectPlayer.PlaySound("cat1");
				}
			}
			else
			{
				zoomieSpeed = 1;
				if (previousZoomies != zoomieSpeed)
				{
					//GameServices.SoundEffectPlayer.PlaySound("cat2");
				}
			}

			directionUpDownState = DirectionUpDown.None;
			directionLeftRightState = DirectionLeftRight.None;

			var positionDiff = Vector2.Zero;
			if (keyboard.IsKeyDown(Keys.Up))
			{
				positionDiff.Y -= Vector2.One.Y;
				directionUpDownState = DirectionUpDown.Up;
			}
			else if (keyboard.IsKeyDown(Keys.Down))
			{
				positionDiff.Y += Vector2.One.Y;
				directionUpDownState = DirectionUpDown.Down;
			}

			if (keyboard.IsKeyDown(Keys.Left))
			{
				positionDiff.X -= Vector2.One.X;
				directionLeftRightState = DirectionLeftRight.Left;
			}
			else if (keyboard.IsKeyDown(Keys.Right))
			{
				positionDiff.X += Vector2.One.X;
				directionLeftRightState = DirectionLeftRight.Right;
			}

			if (positionDiff != Vector2.Zero)
			{
				positionDiff.Normalize();
				position += positionDiff * zoomieSpeed * baseSpeed;
			}

			//
			if (previousPos == position)
			{
				actionState = ActionState.Sit;
				timeSitting += gameTime.ElapsedGameTime;

				if (timeSitting.TotalSeconds > 3) // lie down after 3 seconds
				{
					actionState = ActionState.Lie;
				}

				if (previousActionState != actionState)
				{
					GameServices.SoundEffectPlayer.PlaySound("cat3");
				}
			}
			else
			{
				timeSitting = TimeSpan.Zero;
				actionState = ActionState.Walk;
			}

			var frameCheck = 8 / zoomieSpeed;

			var isNewDirection = previousDirectionUpDownState != directionUpDownState || previousDirectionLeftRightState != directionLeftRightState;

			// animation
			if (++frameCounter % frameCheck == 0 || previousActionState != actionState || isNewDirection)
			{
				animationFrame = (animationFrame + 1) % 3;

				if (actionState == ActionState.Sit)
				{
					currentAnimation.X = animationFrame * frameSize.X + sittingAnimationFrame.X;
					currentAnimation.Y = sittingAnimationFrame.Y;
				}
				else if (actionState == ActionState.Walk)
				{
					if (directionUpDownState == DirectionUpDown.Up)
					{
						currentAnimation.X = animationFrame * frameSize.X + walkingUpAnimationFrame.X;
						currentAnimation.Y = walkingUpAnimationFrame.Y;

						if (directionLeftRightState == DirectionLeftRight.Left)
						{
							currentAnimation.X += frameSize.X * 3;
							currentAnimation.Y -= frameSize.Y * 2;
						}
						if (directionLeftRightState == DirectionLeftRight.Right)
						{
							currentAnimation.X += frameSize.X * 3;
							currentAnimation.Y -= frameSize.Y * 0;
						}

					}
					else if (directionUpDownState == DirectionUpDown.Down)
					{
						currentAnimation.X = animationFrame * frameSize.X + walkingDownAnimationFrame.X;
						currentAnimation.Y = walkingDownAnimationFrame.Y;

						if (directionLeftRightState == DirectionLeftRight.Left)
						{
							currentAnimation.X += frameSize.X * 3;
							currentAnimation.Y -= frameSize.Y * 0;
						}
						if (directionLeftRightState == DirectionLeftRight.Right)
						{
							currentAnimation.X += frameSize.X * 3;
							currentAnimation.Y += frameSize.Y * 2;
						}
					}
					else if (directionLeftRightState == DirectionLeftRight.Left)
					{
						currentAnimation.X = animationFrame * frameSize.X + walkingLeftAnimationFrame.X;
						currentAnimation.Y = walkingLeftAnimationFrame.Y;
					}
					else if (directionLeftRightState == DirectionLeftRight.Right)
					{
						currentAnimation.X = animationFrame * frameSize.X + walkingRightAnimationFrame.X;
						currentAnimation.Y = walkingRightAnimationFrame.Y;
					}
				}
				else if (actionState == ActionState.Lie)
				{
					currentAnimation.X = animationFrame * frameSize.X + lyingAnimationFrame.X;
					currentAnimation.Y = ((frameCounter / 30) % 2) * frameSize.Y + lyingAnimationFrame.Y;
				}
			}
		}

		public void Draw(SpriteBatch sb)
		{
			sb.Draw(spriteSheet, new Rectangle(position.ToPoint(), new Point(64, 64)), currentAnimation, Color.White);

			DrawString(sb, $"BaseSpeed: {baseSpeed}", new Vector2(10, 10));
			DrawString(sb, $"ZoomieSpeed: {zoomieSpeed}", new Vector2(10, 30));
			DrawString(sb, $"ActionState: {actionState}", new Vector2(10, 50));
			DrawString(sb, $"AnimationFrame: {animationFrame}", new Vector2(10, 70));
			DrawString(sb, $"Position: {position}", new Vector2(10, 90));
			DrawString(sb, $"DirectionUpDownState: {directionUpDownState}", new Vector2(10, 110));
			DrawString(sb, $"DirectionLeftRightState: {directionLeftRightState}", new Vector2(10, 130));
		}

		void DrawString(SpriteBatch sb, string str, Vector2 pos)
		{
			sb.DrawString(font, str, pos + Vector2.One, Color.Black);
			sb.DrawString(font, str, pos, Color.White);
		}
	}
}
