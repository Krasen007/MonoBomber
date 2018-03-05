using Microsoft.Xna.Framework.Input;
using MonoContra.Enumerables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoContra.Levels
{  
    public class Pause
    {
        private KeyboardState oldKeyState;



        public void Update(KeyboardState keyState, GameState gameState)
        {
            if (this.oldKeyState.IsKeyDown(Keys.P) && keyState.IsKeyUp(Keys.P))
            {
                gameState = GameState.GameStart;
                
            }

            this.oldKeyState = keyState;
        }
    }    
}
