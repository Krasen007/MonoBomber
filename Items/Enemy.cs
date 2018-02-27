namespace MonoContra.Items
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Enemy : AnimatedSprite
    {
        private int currentFrame;

        private int width;
        private int height;
        private int row;
        private int column;

        private int animationSpeed = 0;

        private SpriteEffects flipSpriteState;

        public Enemy(Texture2D texture, int rows, int cols, Vector2 spritePos, Vector2 spriteSpdX, Vector2 spriteSpdY) : base(texture, rows, cols, spritePos, spriteSpdX, spriteSpdY)
        {
            this.SpriteTexture = texture;
            this.Rows = rows;
            this.Columns = cols;
            this.currentFrame = 0;
            ////this.totalFrames = this.Rows * this.Columns;
            this.flipSpriteState = SpriteEffects.FlipHorizontally;
        }

        public new int Width { get => this.width; private set => this.width = value; }

        public new int Height { get => this.height; private set => this.height = value; }

        protected new int Rows { get; private set; }

        protected new int Columns { get; private set; }

        protected new int Row { get => this.row; private set => this.row = value; }

        protected new int Column { get => this.column; private set => this.column = value; }

        // TODO: Add animations for enemy
        public void Update(AnimatedSprite player)
        {
            this.animationSpeed++;

            if (this.animationSpeed >= 5)
            {
                this.currentFrame++;
                if (this.currentFrame >= 7)
                {
                    this.currentFrame = 0;
                }

                this.animationSpeed = 0;
            }

            this.HandleCollision(player);
        }

        public void Draw(SpriteBatch spriteBatch, double scaleX, double scaleY)
        {
            this.Width = this.SpriteTexture.Width / this.Columns;
            this.Height = this.SpriteTexture.Height / this.Rows;
            this.Row = (int)((float)this.currentFrame / (float)this.Columns);
            this.Column = this.currentFrame % this.Columns;

            scaleX = scaleX * this.Width;
            scaleY = scaleY * this.Height;

            Rectangle sourceRectangle = new Rectangle(this.Width * this.Column, this.Height * this.Row, this.Width, this.Height); // this is what texture is used
            this.DestinationRectangle = new Rectangle((int)SpritePosition.X, (int)SpritePosition.Y, (int)scaleX, (int)scaleY); // this is how is drawn

            spriteBatch.Draw(this.SpriteTexture, this.DestinationRectangle, sourceRectangle, Color.White, 0, Vector2.Zero, this.flipSpriteState, 0);
        }

        private void HandleCollision(AnimatedSprite player)
        {
            if (this.DestinationRectangle.Intersects(player.DestinationRectangle))
            {
                this.SpriteTexture.Dispose();
            }
        }
    }
}
