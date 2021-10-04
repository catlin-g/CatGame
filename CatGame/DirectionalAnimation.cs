using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatGame
{
    public class DirectionalAnimation
    {
        public static void Update(Cat cat, Point leftFrame, Point rightFrame, Point upFrame, Point downFrame)
        {
            if (cat.directionState == DirectionState.Left)
            {
                cat.currentAnimation.X = cat.animationFrame * cat.frameSize.X + leftFrame.X;
                cat.currentAnimation.Y = leftFrame.Y;
            }
            if (cat.directionState == DirectionState.Right)
            {
                cat.currentAnimation.X = cat.animationFrame * cat.frameSize.X + rightFrame.X;
                cat.currentAnimation.Y = rightFrame.Y;
            }
            if (cat.directionState == DirectionState.Up)
            {
                cat.currentAnimation.X = cat.animationFrame * cat.frameSize.X + upFrame.X;
                cat.currentAnimation.Y = upFrame.Y;
            }
            if (cat.directionState == DirectionState.Down)
            {
                cat.currentAnimation.X = cat.animationFrame * cat.frameSize.X + downFrame.X;
                cat.currentAnimation.Y = downFrame.Y;
            }
        }
    }
}
