namespace MonoContra.Items
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Start.BackgroundItems;

    public class AnimatedSprite
    {
        private int currentFrame;
        private int totalFrames;

        public AnimatedSprite(Texture2D texture, int rows, int columns)
        {
            this.Texture = texture;
            this.Rows = rows;
            this.Columns = columns;
            this.currentFrame = 0;
            this.totalFrames = this.Rows * this.Columns;
        }

        public Texture2D Texture { get; set; }

        public int Rows { get; set; }

        public int Columns { get; set; }

        public void Update()
        {
            this.currentFrame++;
            if (this.currentFrame == this.totalFrames)
            {
                this.currentFrame = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            int width = this.Texture.Width / this.Columns;
            int height = this.Texture.Height / this.Rows;
            int row = (int)((float)this.currentFrame / (float)this.Columns);
            int column = this.currentFrame % this.Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, width, height);
            
            spriteBatch.Draw(this.Texture, destinationRectangle, sourceRectangle, Color.White);
        }
    }
}
