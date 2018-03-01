namespace MonoContra.Utilities
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Animation
    {
        private const int DEFAULT_DURATION = 20;

        private Texture2D texture;
        private int framesCount;
        private int currentFrameRow;
        private int currentFrameCol;
        private int currentFrame;
        private int rows;
        private int cols;
        private double frameDuration;
        private double timeSinceLastChange;

        // width and height per frame
        private int width;
        private int height;

        public Animation(Texture2D spriteSheet, int frames, int rows, int cols, int width, int height)
        {
            this.texture = spriteSheet;
            this.framesCount = frames;
            ////this.FrameBounds = new Rectangle(0, 0, width, height);

            this.currentFrameRow = 0;
            this.currentFrameCol = 0;
            this.currentFrame = 0;
            this.frameDuration = DEFAULT_DURATION;
            this.timeSinceLastChange = 0;
            this.rows = rows;
            this.cols = cols;
            this.width = width;
            this.height = height;
        }

        public Rectangle FrameBounds { get; private set; }

        public void Update(GameTime gameTime)
        {
            this.timeSinceLastChange += gameTime.ElapsedGameTime.Milliseconds;

            if (this.timeSinceLastChange >= this.frameDuration)
            {
                this.timeSinceLastChange = 0;

                this.currentFrameCol = (this.currentFrameCol + 1) % this.cols;
                if (this.currentFrameCol == 0)
                {
                    this.currentFrameRow = (this.currentFrameRow + 1) % this.rows;
                }

                this.currentFrame = (this.currentFrame + 1) % this.framesCount;

                if (this.currentFrame == 0)
                {
                    this.currentFrameRow = 0;
                    this.currentFrameCol = 0;
                }

                this.FrameBounds = new Rectangle(
                    this.width * 
                    this.currentFrameCol,
                    this.height * 
                    this.currentFrameRow, 
                    this.width, 
                    this.height);
            }
        }

        public void Draw(SpriteBatch spriteBatch, double rotation, Vector2 position, Vector2 scale)
        {
            Vector2 center = new Vector2(this.width / 2, this.height / 2);
            spriteBatch.Draw(this.texture, position, sourceRectangle: this.FrameBounds, origin: center, scale: scale, rotation: (float)rotation);
        }
    }
}
