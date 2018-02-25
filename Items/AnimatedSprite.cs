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

        private string direction;

        private KeyboardState oldKeyState;
        private MouseState oldMouseState;

        private SpriteEffects flipSpriteState;

        /// <param name="texture">Texture/spritesheet for the sprite</param>
        /// <param name="rows">Rows of the spritesheet</param>
        /// <param name="cols">Rows of the spritesheet</param>
        /// <param name="spritePos">Position of the map</param>
        /// <param name="spriteSpdX">Movement by X</param>
        /// <param name="spriteSpdY">Movement by Y</param>
        public AnimatedSprite(Texture2D texture, int rows, int cols, Vector2 spritePos, Vector2 spriteSpdX, Vector2 spriteSpdY) : base(spritePos, spriteSpdX, spriteSpdY)
        {
            // Those are used for drawing of the sprite
            this.SpriteTexture = texture;
            this.Rows = rows;
            this.Columns = cols;
            this.currentFrame = 0;
            this.totalFrames = this.Rows * this.Columns;
            this.flipSpriteState = SpriteEffects.None;
        }

        public int Width { get => this.width; private set => this.width = value; }

        public int Height { get => this.height; private set => this.height = value; }

        public string Direction { get => this.direction; set => this.direction = value; }

        protected int Rows { get; private set; }

        protected int Columns { get; private set; }

        protected int Row { get => this.row; private set => this.row = value; }

        protected int Column { get => this.column; private set => this.column = value; }

        public void Update(KeyboardState keyState, MouseState mouseState)
        {
            if (keyState.IsKeyDown(Keys.Right) || keyState.IsKeyDown(Keys.D))
            {
                this.Direction = "right";

                this.currentFrame++;
                if (this.currentFrame >= 12)
                {
                    this.currentFrame = 7;
                }

                // this.flipSpriteState = SpriteEffects.None;
                this.SpritePosition += this.SpriteSpeedX;
            }

            if (keyState.IsKeyDown(Keys.Left) || keyState.IsKeyDown(Keys.A))
            {
                this.Direction = "left";

                // Bug when going from right to left
                this.currentFrame++;

                if (this.currentFrame >= this.totalFrames)
                {
                    this.currentFrame = 19;
                }

                // this.flipSpriteState = SpriteEffects.FlipHorizontally;
                this.SpritePosition -= this.SpriteSpeedX;
            }

            if (keyState.IsKeyDown(Keys.Up) || keyState.IsKeyDown(Keys.W))
            {
                this.Direction = "up";

                this.currentFrame++;
                if (this.currentFrame >= 18)
                {
                    this.currentFrame = 13;
                }

                this.SpritePosition -= this.SpriteSpeedY;
            }

            if (keyState.IsKeyDown(Keys.Down) || keyState.IsKeyDown(Keys.S))
            {
                this.Direction = "down";

                this.currentFrame++;
                if (this.currentFrame >= 6)
                {
                    this.currentFrame = 0;
                }

                this.SpritePosition += this.SpriteSpeedY;
            }

            if (mouseState.LeftButton == ButtonState.Pressed && this.oldMouseState.LeftButton == ButtonState.Released)
            {
                this.SpritePosition = new Vector2(mouseState.Position.X, mouseState.Position.Y);
            }

            this.oldKeyState = keyState;
            this.oldMouseState = mouseState;
        }

        public void Draw(SpriteBatch spriteBatch, double scaleX, double scaleY, string direct)
        {
            direct = this.Direction;

            if (direct == "left")
            {
                this.Animate(spriteBatch, scaleX, scaleY);
            }
            else if (direct == "right")
            {
                this.Animate(spriteBatch, scaleX, scaleY);
            }
            else if (direct == "up")
            {
                this.Animate(spriteBatch, scaleX, scaleY);
            }
            else if (direct == "down")
            {
                this.Animate(spriteBatch, scaleX, scaleY);
            }
        }

        private void Animate(SpriteBatch spriteBatch, double scaleX, double scaleY)
        {
            this.Width = this.SpriteTexture.Width / this.Columns;
            this.Height = this.SpriteTexture.Height / this.Rows;
            this.Row = (int)((float)this.currentFrame / (float)this.Columns);
            this.Column = this.currentFrame % this.Columns;

            scaleX = scaleX * this.Width;
            scaleY = scaleY * this.Height;

            Rectangle sourceRectangle = new Rectangle(this.Width * this.Column, this.Height * this.Row, this.Width, this.Height); // this is what texture is used
            Rectangle destinationRectangle = new Rectangle((int)SpritePosition.X, (int)SpritePosition.Y, (int)scaleX, (int)scaleY); // this is how is drawn

            spriteBatch.Draw(this.SpriteTexture, destinationRectangle, sourceRectangle, Color.White, 0, Vector2.Zero, this.flipSpriteState, 0);
        }
    }
}
