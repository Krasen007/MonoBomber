namespace MonoContra.Objects
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

        private void LoadContent(ContentManager content)
        {
            this.backgrTree = new StaticItem(new Vector2(125, 125), new Vector2(50f, 50f), new Vector2(0, 10));
            this.backgrTree.SpriteTexture = content.Load<Texture2D>("Tree");
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont gameFont, GraphicsDeviceManager graphics)
        {
            graphics.GraphicsDevice.Clear(Color.DarkRed);
            spriteBatch.Begin();
            spriteBatch.Draw(this.backgrTree.SpriteTexture, new Vector2(300, 300), Color.White);
            spriteBatch.DrawString(
                gameFont,
                "Press enter or space to START GAME \n" +
                "W,A,S,D to move character, P for PAUSE, Esc for Exit.",
                new Vector2(600, 325), // GAME_WIDTH / 2 - 20, GAME_HEIGHT / 2 - 30),
                Color.CadetBlue);
            spriteBatch.End();
        }
    }
}
