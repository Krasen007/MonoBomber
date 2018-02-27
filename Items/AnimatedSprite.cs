namespace MonoContra.Items
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using Start.BackgroundItems;

    public abstract class AnimatedSprite : StaticItem
    {
        private Rectangle destinationRectangle;

        private int currentFrame;
        private int totalFrames;

        private int width1;
        private int height1;
        private int row1;
        private int column1;

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
            MoveLeft,
            MoveRight,
            MoveUp,
            MoveDown,
            DropBomb
        }

        public int Width { get => this.Width1; private set => this.Width1 = value; }

        public int Height { get => this.Height1; private set => this.Height1 = value; }

        public Rectangle DestinationRectangle { get => destinationRectangle; set => destinationRectangle = value; }

        protected int Rows { get; private set; }

        protected int Columns { get; private set; }

        protected int Row { get => this.Row1; private set => this.Row1 = value; }

        protected int Column { get => this.Column1; private set => this.Column1 = value; }

        protected int CurrentFrame { get => currentFrame; set => currentFrame = value; }

        protected int TotalFrames { get => totalFrames; set => totalFrames = value; }

        protected int Width1 { get => width1; set => width1 = value; }

        protected int Height1 { get => height1; set => height1 = value; }

        protected int Row1 { get => row1; set => row1 = value; }

        protected int Column1 { get => column1; set => column1 = value; }

        protected SpriteEffects FlipSpriteState { get => flipSpriteState; set => flipSpriteState = value; }

        public virtual void Update()
        {
            // Update method for your animated sprite
        }

        public void Draw(SpriteBatch spriteBatch, double scaleX, double scaleY, SpriteState state)
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
