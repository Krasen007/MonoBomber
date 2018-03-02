namespace MonoContra.Objects
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using global::MonoContra.Utilities;

    public class Level
    {
        private StaticItem backgrTree;

        public Level(ContentManager content)
        {
            LoadContent(content);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.backgrTree.SpriteTexture, new Vector2(300, 300), Color.White);
        }

        private void LoadContent(ContentManager content)
        {
            this.backgrTree = new StaticItem(new Vector2(125, 125), new Vector2(50f, 50f), new Vector2(0, 10));
            this.backgrTree.SpriteTexture = content.Load<Texture2D>("Tree");
        }
    }
}
