﻿namespace MonoContra.Objects
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    public class Level1
    {
        //private StaticItem title;

        public Level1(ContentManager content)
        {
            this.LoadContent(content);
        }

        public void Draw(SpriteBatch spriteBatch, int gameWidth, int gameHeight)
        {
            //spriteBatch.Begin();
            //spriteBatch.Draw(this.title.SpriteTexture, new Vector2(gameWidth - this.title.SpriteTexture.Width - 60, (gameHeight / 2) - this.title.SpriteTexture.Height + 20), Color.White);
            //spriteBatch.End();
        }

        private void LoadContent(ContentManager content)
        {
            //this.title = new StaticItem(Vector2.Zero);
            //this.title.SpriteTexture = content.Load<Texture2D>("monobomber");
        }
    }
}