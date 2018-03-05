namespace MonoContra.Levels
{
    using Microsoft.Xna.Framework.Input;
    using MonoContra.Enumerables;

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
