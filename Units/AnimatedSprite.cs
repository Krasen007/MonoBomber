namespace MonoContra.Objects
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public abstract class AnimatedSprite : StaticItem
    {
        private Rectangle destinationRectangle;

        private int currentFrame;
        private int totalFrames;

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
            this.CurrentFrame = 0;
            this.TotalFrames = this.Rows * this.Columns;
            this.FlipSpriteState = SpriteEffects.None;     
        }

        public enum SpriteState
        {
            Idle,
            MoveLeft,
            MoveRight,
            MoveUp,
            MoveDown,
            DropBomb,
            Shoot
        }

        public int Width { get; set; }

        public int Height { get; set; }

        public Rectangle DestinationRectangle { get => this.destinationRectangle; set => this.destinationRectangle = value; }

        protected int Rows { get; private set; }

        protected int Columns { get; private set; }

        protected int Row { get; set; }

        protected int Column { get; set; }

        protected int CurrentFrame { get => this.currentFrame; set => this.currentFrame = value; }

        protected int TotalFrames { get => this.totalFrames; set => this.totalFrames = value; }

        protected SpriteEffects FlipSpriteState { get => this.flipSpriteState; set => this.flipSpriteState = value; }

        public virtual void Update()
        {
            // Update method for your animated sprite
        }

        public virtual void Draw(SpriteBatch spriteBatch, double scaleX, double scaleY, SpriteState state)
        {
            if (state == SpriteState.MoveLeft)
            {
                this.Animate(spriteBatch, scaleX, scaleY);
            }
            else if (state == SpriteState.MoveRight)
            {
                this.Animate(spriteBatch, scaleX, scaleY);
            }
            else if (state == SpriteState.MoveUp)
            {
                this.Animate(spriteBatch, scaleX, scaleY);
            }
            else if (state == SpriteState.MoveDown)
            {
                this.Animate(spriteBatch, scaleX, scaleY);
            }
            else
            {
                this.Animate(spriteBatch, scaleX, scaleY);
            }
        }

        private void Animate(SpriteBatch spriteBatch, double scaleX, double scaleY)
        {
            this.Width = this.SpriteTexture.Width / this.Columns;
            this.Height = this.SpriteTexture.Height / this.Rows;
            this.Row = (int)((float)this.CurrentFrame / (float)this.Columns);
            this.Column = this.CurrentFrame % this.Columns;

            scaleX = scaleX * this.Width;
            scaleY = scaleY * this.Height;

            Rectangle sourceRectangle = new Rectangle(this.Width * this.Column, this.Height * this.Row, this.Width, this.Height); // this is what texture is used
            this.DestinationRectangle = new Rectangle((int)SpritePosition.X, (int)SpritePosition.Y, (int)scaleX, (int)scaleY); // this is how is drawn

            spriteBatch.Draw(this.SpriteTexture, this.DestinationRectangle, sourceRectangle, Color.White, 0, Vector2.Zero, this.FlipSpriteState, 0);
        }
    }
}
