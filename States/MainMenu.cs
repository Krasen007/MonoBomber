namespace MonoBomber.Objects
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    public class MainMenu
    {
        private StaticItem backgrTree;

        public MainMenu(ContentManager content, SpriteBatch spriteBatch, SpriteFont gameFont, GraphicsDeviceManager graphics)
        {
            this.LoadContent(content);
            this.Draw(spriteBatch, gameFont, graphics);
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont gameFont, GraphicsDeviceManager graphics)
        {
            graphics.GraphicsDevice.Clear(Color.DarkRed);
            spriteBatch.Begin();
            spriteBatch.DrawString(gameFont, "MAIN MENU", new Vector2(580, 30), Color.LightGoldenrodYellow);
            spriteBatch.Draw(this.backgrTree.SpriteTexture, new Vector2(1280 - this.backgrTree.SpriteTexture.Width, 300), Color.White);
            spriteBatch.Draw(this.backgrTree.SpriteTexture, new Vector2(0, 300), Color.White);
            spriteBatch.DrawString(
                gameFont,
                "\n Stay away from the enemies that lurk in the graveyard!" +
                "\n                  Find the key to open the door. " +
                "\n          If the game score reaches zero you lose!" +
                "\n" +
                "\n           Press enter or space to START GAME" +
                "\n                W,A,S,D to move the character" +
                "\n                Use P for PAUSE, Esc for Exit.",
                new Vector2(350, 325),
                Color.LightCoral);
            spriteBatch.End();
        }

        private void LoadContent(ContentManager content)
        {
            this.backgrTree = new StaticItem(new Vector2(125, 125), new Vector2(50f, 50f), new Vector2(0, 10));
            this.backgrTree.SpriteTexture = content.Load<Texture2D>("Tree");
        }
    }
}
