namespace MonoBomber.Objects
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    public class GameOver
    {
        public GameOver(SpriteBatch spriteBatch, SpriteFont gameFont, ContentManager content, GraphicsDeviceManager graphics)
        {
            this.Draw(spriteBatch, gameFont, content, graphics);
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont gameFont, ContentManager content, GraphicsDeviceManager graphics)
        {
            StaticItem gameOverScreen = new StaticItem(Vector2.Zero);
            gameOverScreen.SpriteTexture = content.Load<Texture2D>("gameover");
            
            graphics.GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            spriteBatch.Draw(gameOverScreen.SpriteTexture, new Vector2(280, 300), Color.White);
            spriteBatch.DrawString(
                gameFont,
                "\n You are dead! " +
                "\n Press Enter to restart.",
                new Vector2(500, 250),
                Color.DarkSeaGreen);
            spriteBatch.End();
        }
    }
}
