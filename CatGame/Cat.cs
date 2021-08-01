using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatGame
{
    public enum DirectionState
    {
        Left,
        Right,
        Up,
        Down,
    }

    public enum ActionState
    {
        Walk,
        Sit,
    }

    public class Cat
    {
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

        Point frameSize;

        DirectionState directionState;
        ActionState actionState;

        DirectionState previousDirectionState;
        ActionState previousActionState;
        int speed;
        int zoomies;

        public Cat()
        {
            position = new Vector2(200, 200);

            walkingLeftAnimationFrame = new Point(0, 32);
            walkingRightAnimationFrame = new Point(0, 64);
            walkingUpAnimationFrame = new Point(0, 96);
            walkingDownAnimationFrame = new Point(0, 0);

            sittingAnimationFrame = new Point(196, 128);

            frameSize = new Point(32, 32);
            speed = 4;
            zoomies = 1;
            
            currentAnimation = new Rectangle(sittingAnimationFrame, frameSize);

            directionState = DirectionState.Down;
            actionState = ActionState.Sit;
        }

        public void LoadContent(ContentManager content)
        {
            spriteSheet = content.Load<Texture2D>("Textures/CatSpriteSheet");
        }

        public void Update(GameTime gameTime)
        {
            var previousPos = position;
            previousActionState = actionState;
            previousDirectionState = directionState;

            // user input
            var keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.LeftShift))
            {
                zoomies = 2;
            }
            else
            {
                zoomies = 1;
            }

            if (keyboard.IsKeyDown(Keys.Left))
            {
                position.X -= speed * zoomies;
                directionState = DirectionState.Left;
            }
            if (keyboard.IsKeyDown(Keys.Right))
            {
                position.X += speed * zoomies;
                directionState = DirectionState.Right;
            }
            if (keyboard.IsKeyDown(Keys.Up))
            {
                position.Y -= speed * zoomies;
                directionState = DirectionState.Up;
            }
            if (keyboard.IsKeyDown(Keys.Down))
            {
                position.Y += speed * zoomies;
                directionState = DirectionState.Down;
            }

            // 
            if (previousPos == position)
            {
                actionState = ActionState.Sit;
            }
            else
            {
                actionState = ActionState.Walk;
            }

            int frameCheck = 8 / zoomies;

            // animation
            if (++frameCounter % frameCheck == 0 || previousActionState != actionState || previousDirectionState != directionState)
            {
                animationFrame = (animationFrame + 1) % 3;

                if (actionState == ActionState.Sit)
                {
                    currentAnimation.X = animationFrame * frameSize.X + sittingAnimationFrame.X;
                    currentAnimation.Y = sittingAnimationFrame.Y;
                }
                else if (actionState == ActionState.Walk)
                {
                    if (directionState == DirectionState.Left)
                    {
                            currentAnimation.X = animationFrame * frameSize.X + walkingLeftAnimationFrame.X;
                            currentAnimation.Y = walkingLeftAnimationFrame.Y;
                    }
                    if (directionState == DirectionState.Right)
                    {
                        currentAnimation.X = animationFrame * frameSize.X + walkingRightAnimationFrame.X;
                        currentAnimation.Y = walkingRightAnimationFrame.Y;
                    }
                    if (directionState == DirectionState.Up)
                    {
                        currentAnimation.X = animationFrame * frameSize.X + walkingUpAnimationFrame.X;
                        currentAnimation.Y = walkingUpAnimationFrame.Y;
                    }
                    if (directionState == DirectionState.Down)
                    {
                        currentAnimation.X = animationFrame * frameSize.X + walkingDownAnimationFrame.X;
                        currentAnimation.Y = walkingDownAnimationFrame.Y;
                    }
                }
            }
        }

        public void Draw(SpriteBatch sb)
        {
            //sb.Draw(texture, position, Color.White);
            //sb.Draw(texture, position, currentAnimation, Color.White);

            // debug
            //sb.Draw(spriteSheet, new Vector2(0, 0), Color.White);
            //sb.DrawRectangle(currentAnimation, Color.Red, 3);

            sb.Draw(spriteSheet, new Rectangle(position.ToPoint(), new Point(64, 64)), currentAnimation, Color.White);
        }
    }
}
