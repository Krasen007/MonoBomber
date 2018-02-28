namespace MonoContra.Objects
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class StaticItem
    {
        private Texture2D spriteTexture;
        private Vector2 spritePosition;
        private Vector2 spriteSpeedX;
        private Vector2 spriteSpeedY;

        public StaticItem(Vector2 spritePos, Vector2 spriteSpdX, Vector2 spriteSpdY)
        {
            this.spritePosition = spritePos;
            this.spriteSpeedX = spriteSpdX;
            this.spriteSpeedY = spriteSpdY;
        }

        public Texture2D SpriteTexture { get => this.spriteTexture; set => this.spriteTexture = value; }

        public Vector2 SpritePosition { get => this.spritePosition; set => this.spritePosition = value; }

        public Vector2 SpriteSpeedX { get => this.spriteSpeedX; protected set => this.spriteSpeedX = value; }

        public Vector2 SpriteSpeedY { get => this.spriteSpeedY; protected set => this.spriteSpeedY = value; }
    }
}