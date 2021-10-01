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
    public class Cat
    {
        Texture2D spriteSheet;
        Vector2 currentPosition;

        int animationFrame;
        Rectangle currentAnimation;
        int frameCounter;

        Point walkingLeftAnimationFrame;
        Point walkingRightAnimationFrame;
        Point walkingUpAnimationFrame;
        Point walkingDownAnimationFrame;

        Point sittingLeftAnimationFrame;
        Point sittingRightAnimationFrame;
        Point sittingAwayAnimationFrame;
        Point sittingForwardAnimationFrame;

        Point pounceLeftAnimationFrame;
        Point pounceRightAnimationFrame;
        Point pounceUpAnimationFrame;
        Point pounceDownAnimationFrame;

        Point frameSize;

        DirectionState directionState;
        ActionState actionState;

        DirectionState previousDirectionState;
        ActionState previousActionState;
        int speed;
        int zoomies;

        public Cat()
        {
            currentPosition = new Vector2(200, 200);

            walkingLeftAnimationFrame = new Point(0, 32);
            walkingRightAnimationFrame = new Point(0, 64);
            walkingUpAnimationFrame = new Point(0, 96);
            walkingDownAnimationFrame = new Point(0, 0);

            sittingLeftAnimationFrame = new Point(100, 128);
            sittingRightAnimationFrame = new Point(100, 192);
            sittingAwayAnimationFrame = new Point(196, 160);
            sittingForwardAnimationFrame = new Point(196, 128);

            pounceLeftAnimationFrame = new Point(288, 0);
            pounceRightAnimationFrame = new Point(288, 64);
            pounceUpAnimationFrame = new Point(192, 96);
            pounceDownAnimationFrame = new Point(192, 0);

            frameSize = new Point(32, 32);
            speed = 4;
            zoomies = 1;

            currentAnimation = new Rectangle(sittingForwardAnimationFrame, frameSize);

            directionState = DirectionState.Down;
            actionState = ActionState.Sit;
        }

        public void LoadContent(ContentManager content)
        {
            spriteSheet = content.Load<Texture2D>("Textures/CatSpriteSheet");
        }

        public void Update(GameTime gameTime)
        {
            var previousPos = currentPosition;
            previousActionState = actionState;
            previousDirectionState = directionState;

            var keyboard = Keyboard.GetState();
            UserInput(keyboard);

            if (previousPos == currentPosition)
            {
                actionState = ActionState.Sit;
            }
            else
            {
                actionState = ActionState.Walk;
            }

            var frameCheck = 8 / zoomies;
            Animation(frameCheck, previousPos);
        }

        public void UserInput(KeyboardState keyboard)
        {
            zoomies = keyboard.IsKeyDown(Keys.LeftShift) ? 2 : 1;

            if (keyboard.IsKeyDown(Keys.Left))
            {
                currentPosition.X -= speed * zoomies;
                directionState = DirectionState.Left;
            }
            else if (keyboard.IsKeyDown(Keys.Right))
            {
                currentPosition.X += speed * zoomies;
                directionState = DirectionState.Right;
            }
            else if (keyboard.IsKeyDown(Keys.Up))
            {
                currentPosition.Y -= speed * zoomies;
                directionState = DirectionState.Up;
            }
            else if (keyboard.IsKeyDown(Keys.Down))
            {
                currentPosition.Y += speed * zoomies;
                directionState = DirectionState.Down;
            }

            if (keyboard.IsKeyDown(Keys.Space))
            {
                actionState = ActionState.Pounce;
            }
        }

        public void Animation(int frameCheck, Vector2 previousPos)
        {
            if (++frameCounter % frameCheck == 0 || previousActionState != actionState || previousDirectionState != directionState)
            {
                animationFrame = (animationFrame + 1) % 3;

                if (actionState == ActionState.Sit)
                {
                    if (directionState == DirectionState.Left)
                    {
                        currentAnimation.X = animationFrame * frameSize.X + sittingLeftAnimationFrame.X;
                        currentAnimation.Y = sittingLeftAnimationFrame.Y;
                    }
                    if (directionState == DirectionState.Right)
                    {
                        currentAnimation.X = animationFrame * frameSize.X + sittingRightAnimationFrame.X;
                        currentAnimation.Y = sittingRightAnimationFrame.Y;
                    }
                    if (directionState == DirectionState.Up)
                    {
                        currentAnimation.X = animationFrame * frameSize.X + sittingAwayAnimationFrame.X;
                        currentAnimation.Y = sittingAwayAnimationFrame.Y;
                    }
                    if (directionState == DirectionState.Down)
                    {
                        currentAnimation.X = animationFrame * frameSize.X + sittingForwardAnimationFrame.X;
                        currentAnimation.Y = sittingForwardAnimationFrame.Y;
                    }
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
                else if (actionState == ActionState.Pounce)
                {
                    if (directionState == DirectionState.Left)
                    {
                        currentAnimation.X = animationFrame * frameSize.X + pounceLeftAnimationFrame.X;
                        currentAnimation.Y = pounceLeftAnimationFrame.Y;
                    }
                    if (directionState == DirectionState.Right)
                    {
                        currentAnimation.X = animationFrame * frameSize.X + pounceRightAnimationFrame.X;
                        currentAnimation.Y = pounceRightAnimationFrame.Y;
                    }
                    if (directionState == DirectionState.Up)
                    {
                        currentAnimation.X = animationFrame * frameSize.X + pounceUpAnimationFrame.X;
                        currentAnimation.Y = pounceUpAnimationFrame.Y;
                    }
                    if (directionState == DirectionState.Down)
                    {
                        currentAnimation.X = animationFrame * frameSize.X + pounceDownAnimationFrame.X;
                        currentAnimation.Y = pounceDownAnimationFrame.Y;
                    }
                }
            }
        }

        public void Draw(SpriteBatch sb)
        {
            //sb.Draw(texture, position, Color.White);
            //sb.Draw(texture, position, currentAnimation, Color.White);

            // debug
            sb.Draw(spriteSheet, new Vector2(0, 0), Color.White);
            sb.DrawRectangle(currentAnimation, Color.Red, 3);

            sb.Draw(spriteSheet, new Rectangle(currentPosition.ToPoint(), new Point(64, 64)), currentAnimation, Color.White);
        }
    }
}
