namespace MonoContra.Objects
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    public class Intro
    {
        private StaticItem title;

        public Intro(ContentManager content, SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            this.LoadContent(content);
            this.Draw(spriteBatch, graphics);
        }

        private void LoadContent(ContentManager content)
        {
            this.title = new StaticItem(Vector2.Zero);
            this.title.SpriteTexture = content.Load<Texture2D>("monobomber");
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            graphics.GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin();
            spriteBatch.Draw(this.title.SpriteTexture, new Vector2(1280 - this.title.SpriteTexture.Width - 60, (720 / 2) - this.title.SpriteTexture.Height + 20), Color.White);
            spriteBatch.End();
        }
    }
}
