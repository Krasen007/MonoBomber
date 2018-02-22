namespace MonoContra.Items
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using Start.BackgroundItems;

    public class AnimatedSprite : StaticItem
    {
        private int currentFrame;
        private int totalFrames;

        private KeyboardState oldKeyState;

        private SpriteEffects flipSpriteState;
        ////Vector2 origin; 

        public AnimatedSprite(Vector2 spritePos, Vector2 spriteSpd) : base(spritePos, spriteSpd)
        {
            this.SpritePosition = spritePos;
            this.SpriteSpeed = spriteSpd;
        }

        public AnimatedSprite(Texture2D texture, int rows, int columns, Vector2 spritePos, Vector2 spriteSpd) : base(spritePos, spriteSpd)
        {
            // Those are used for drawing of the sprite
            this.Texture = texture;
            this.Rows = rows;
            this.Columns = columns;
            this.currentFrame = 0;
            this.totalFrames = this.Rows * this.Columns;

            this.SpritePosition = spritePos;
            this.SpriteSpeed = spriteSpd;
        }

        public Texture2D Texture { get; set; }

        public int Rows { get; set; }

        public int Columns { get; set; }        

        public void Update(KeyboardState keyState, MouseState mouseState)
        {
            this.currentFrame++;
            if (this.currentFrame == this.totalFrames)
            {
                this.currentFrame = 0;
            }

            if (keyState.IsKeyDown(Keys.Right))
            {
                this.flipSpriteState = SpriteEffects.None;
                this.SpritePosition += new Vector2(10, 0);
            }

            if (keyState.IsKeyDown(Keys.Left))
            {
                this.flipSpriteState = SpriteEffects.FlipHorizontally;
                this.SpritePosition -= new Vector2(10, 0);
            }

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                // TODO: fix position with mouse
                this.SpritePosition = new Vector2(mouseState.Position.X, mouseState.Position.Y);
            }

            this.oldKeyState = keyState;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int width = this.Texture.Width / this.Columns;
            int height = this.Texture.Height / this.Rows;
            int row = (int)((float)this.currentFrame / (float)this.Columns);
            int column = this.currentFrame % this.Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)SpritePosition.X, (int)SpritePosition.Y, width, height);
            
            spriteBatch.Draw(this.Texture, destinationRectangle, sourceRectangle, Color.White, 0, Vector2.Zero, this.flipSpriteState, 0);
        }
    }
}
