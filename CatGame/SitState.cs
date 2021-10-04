using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatGame
{
    class SitState : IActionState
    {
        public SitState()
        {
            sittingLeftAnimationFrame = new Point(100, 128);
            sittingRightAnimationFrame = new Point(100, 192);
            sittingUpAnimationFrame = new Point(196, 160);
            sittingDownAnimationFrame = new Point(196, 128);
        }

        public Point DefaultAnimationFrame => sittingDownAnimationFrame;

        public void HandleInput(Cat cat, KeyboardState keyboardState)
        {
            if (   keyboardState.IsKeyDown(Keys.Left) 
                || keyboardState.IsKeyDown(Keys.Right) 
                || keyboardState.IsKeyDown(Keys.Up) 
                || keyboardState.IsKeyDown(Keys.Down))
            {
                cat.currentState = new WalkState();
            }
        }

        Point sittingLeftAnimationFrame;
        Point sittingRightAnimationFrame;
        Point sittingUpAnimationFrame;
        Point sittingDownAnimationFrame;

        public void Update(Cat cat)
        {
            DirectionalAnimation.Update(cat, sittingLeftAnimationFrame, sittingRightAnimationFrame, sittingUpAnimationFrame, sittingDownAnimationFrame);
        }
    }
}