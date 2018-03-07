namespace MonoBomber.Objects
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class StaticItem
    {
        public StaticItem(Vector2 spritePosition)
        {
            this.SpritePosition = spritePosition;
        }

        public StaticItem(Vector2 spritePosition, Vector2 spriteSpeedX, Vector2 spriteSpeedY)
        {
            this.SpritePosition = spritePosition;
            this.SpriteSpeedX = spriteSpeedX;
            this.SpriteSpeedY = spriteSpeedY;
        }

        public Texture2D SpriteTexture { get; set; }

        public Vector2 SpritePosition { get; set; }

        public Vector2 SpriteSpeedX { get; set; }

        public Vector2 SpriteSpeedY { get; set; }

        public void Draw(SpriteBatch spriteBatch, Texture2D spriteTexture, Vector2 spritePosition, Color color)
        {
            spriteBatch.Draw(spriteTexture, spritePosition, color);
        }

        /// <summary>
        /// Use ONLY for static items.
        /// </summary>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.SpriteTexture, this.SpritePosition, Color.White);
        }
    }
}