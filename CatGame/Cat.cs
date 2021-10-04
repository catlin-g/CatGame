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
        public Vector2 currentPosition;

        public int animationFrame;
        public Rectangle currentAnimation;
        int frameCounter;

        Point pounceLeftAnimationFrame;
        Point pounceRightAnimationFrame;
        Point pounceUpAnimationFrame;
        Point pounceDownAnimationFrame;

        public Point frameSize;

        public DirectionState directionState;
        DirectionState previousDirectionState;

        public IActionState currentState;
        IActionState previousState;

        public int speed;
        int zoomies;

        public Cat()
        {
            currentPosition = new Vector2(200, 200);

            pounceLeftAnimationFrame = new Point(288, 0);
            pounceRightAnimationFrame = new Point(288, 64);
            pounceUpAnimationFrame = new Point(192, 96);
            pounceDownAnimationFrame = new Point(192, 0);

            frameSize = new Point(32, 32);
            speed = 4;
            zoomies = 1;

            currentState = new SitState();
            currentAnimation = new Rectangle(((SitState)currentState).DefaultAnimationFrame, frameSize);

            directionState = DirectionState.Down;
        }

        public void LoadContent(ContentManager content)
        {
            spriteSheet = content.Load<Texture2D>("Textures/CatSpriteSheet");
        }

        public void Update(GameTime gameTime)
        {
            var previousPos = currentPosition;
            previousState = currentState;
            previousDirectionState = directionState;

            var keyboard = Keyboard.GetState();

            currentState.HandleInput(this, keyboard);

            var frameCheck = 8 / zoomies;
            Animation(frameCheck, previousPos);
        }

        public void Animation(int frameCheck, Vector2 previousPos)
        {
            if (++frameCounter % frameCheck == 0 || previousState != currentState || previousDirectionState != directionState)
            {
                animationFrame = (animationFrame + 1) % 3;

                currentState.Update(this);
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
