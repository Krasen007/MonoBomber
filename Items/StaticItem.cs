namespace Start.BackgroundItems
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class StaticItem
    {
        private Texture2D backgrItemTexture;
        private Vector2 spritePosition;
        private Vector2 spriteSpeed;

        public StaticItem(Vector2 spritePos, Vector2 spriteSpd)
        {
            this.spritePosition = spritePos;
            this.spriteSpeed = spriteSpd;
        }

        public Texture2D BackgrItemTexture { get => this.backgrItemTexture; set => this.backgrItemTexture = value; }

        public Vector2 SpritePosition { get => this.spritePosition; set => this.spritePosition = value; }

        public Vector2 SpriteSpeed { get => this.spriteSpeed; set => this.spriteSpeed = value; }
    }
}