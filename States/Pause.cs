namespace MonoBomber.States
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using MonoBomber.Utils;

    public class Pause
    {
        public Pause(SpriteBatch spriteBatch, SpriteFont gameFont)
        {
            this.Draw(spriteBatch, gameFont);
        }

        private void Draw(SpriteBatch spriteBatch, SpriteFont gameFont)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(
                gameFont,
                "\n Game paused! " +
                "\n Press P to resume.",
                new Vector2(600, 230), // this.player.SpritePosition.X, this.player.SpritePosition.Y
                Color.SandyBrown);
            spriteBatch.End();
        }
    }
}
