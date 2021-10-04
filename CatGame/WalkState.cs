using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatGame
{
    class WalkState : IActionState
    {
        public WalkState()
        {
            walkingLeftAnimationFrame = new Point(0, 32);
            walkingRightAnimationFrame = new Point(0, 64);
            walkingUpAnimationFrame = new Point(0, 96);
            walkingDownAnimationFrame = new Point(0, 0);
        }

        public void HandleInput(Cat cat, KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyUp(Keys.Left)
                && keyboardState.IsKeyUp(Keys.Right)
                && keyboardState.IsKeyUp(Keys.Up)
                && keyboardState.IsKeyUp(Keys.Down))
            {
                cat.currentState = new SitState();
            }

            UpdateMovement(cat, keyboardState);
        }

        Point walkingLeftAnimationFrame;
        Point walkingRightAnimationFrame;
        Point walkingUpAnimationFrame;
        Point walkingDownAnimationFrame;

        public void Update(Cat cat)
        {
            UpdateAnimation(cat);
        }

        public void UpdateMovement(Cat cat, KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                cat.currentPosition.X -= cat.speed;
                cat.directionState = DirectionState.Left;
            }
            else if (keyboardState.IsKeyDown(Keys.Right))
            {
                cat.currentPosition.X += cat.speed;
                cat.directionState = DirectionState.Right;
            }
            else if (keyboardState.IsKeyDown(Keys.Up))
            {
                cat.currentPosition.Y -= cat.speed;
                cat.directionState = DirectionState.Up;
            }
            else if (keyboardState.IsKeyDown(Keys.Down))
            {
                cat.currentPosition.Y += cat.speed;
                cat.directionState = DirectionState.Down;
            }
        }

        public void UpdateAnimation(Cat cat)
        {
            DirectionalAnimation.Update(cat, walkingLeftAnimationFrame, walkingRightAnimationFrame, walkingUpAnimationFrame, walkingDownAnimationFrame);

        }
    }
}
