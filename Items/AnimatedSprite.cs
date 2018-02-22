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

        private int width;
        private int height;
        private int row;
        private int column;

        private KeyboardState oldKeyState;
        private MouseState oldMouseState;

        private SpriteEffects flipSpriteState;

        public AnimatedSprite(Vector2 spritePos, Vector2 spriteSpd) : base(spritePos, spriteSpd)
        {
        }

        public AnimatedSprite(Texture2D texture, int rows, int columns, Vector2 spritePos, Vector2 spriteSpd) : base(spritePos, spriteSpd)
        {
            // Those are used for drawing of the sprite
            this.Texture = texture;
            this.Rows = rows;
            this.Columns = columns;
            this.currentFrame = 0;
            this.totalFrames = this.Rows * this.Columns;
        }

        public Texture2D Texture { get; private set; }

        public int Rows { get; private set; }

        public int Columns { get; private set; }

        public int Width { get => this.width; private set => this.width = value; }

        public int Height { get => this.height; private set => this.height = value; }

        public int Row { get => this.row; private set => this.row = value; }

        public int Column { get => this.column; private set => this.column = value; }

        public void Update(KeyboardState keyState, MouseState mouseState)
        {
            this.currentFrame++;
            if (this.currentFrame == this.totalFrames)
            {
                this.currentFrame = 0;
            }

            if (keyState.IsKeyDown(Keys.Right) || keyState.IsKeyDown(Keys.D))
            {
                this.flipSpriteState = SpriteEffects.None;
                this.SpritePosition += this.SpriteSpeed;
            }

            if (keyState.IsKeyDown(Keys.Left) || keyState.IsKeyDown(Keys.A))
            {
                this.flipSpriteState = SpriteEffects.FlipHorizontally;
                this.SpritePosition -= this.SpriteSpeed;
            }

            if (mouseState.LeftButton == ButtonState.Pressed && this.oldMouseState.LeftButton == ButtonState.Released)
            {
                this.SpritePosition = new Vector2(mouseState.Position.X, mouseState.Position.Y);
            }

            this.oldKeyState = keyState;
            this.oldMouseState = mouseState;
        }

        public void Draw(SpriteBatch spriteBatch, double scaleX, double scaleY)
        {
            this.Width = this.Texture.Width / this.Columns;
            this.Height = this.Texture.Height / this.Rows;
            this.Row = (int)((float)this.currentFrame / (float)this.Columns);
            this.Column = this.currentFrame % this.Columns;

            scaleX = scaleX * this.Width;
            scaleY = scaleY * this.Height;

            Rectangle sourceRectangle = new Rectangle(this.Width * this.Column, this.Height * this.Row, this.Width, this.Height); // this is what texture is used
            Rectangle destinationRectangle = new Rectangle((int)SpritePosition.X, (int)SpritePosition.Y, (int)scaleX, (int)scaleY); // this is how is drawn

            spriteBatch.Draw(this.Texture, destinationRectangle, sourceRectangle, Color.White, 0, Vector2.Zero, this.flipSpriteState, 0);
        }
    }
}
