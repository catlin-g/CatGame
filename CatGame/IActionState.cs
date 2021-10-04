using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatGame
{
    public interface IActionState
    {
        void HandleInput(Cat cat, KeyboardState keyboardState);

        void Update(Cat cat);
    }
}
