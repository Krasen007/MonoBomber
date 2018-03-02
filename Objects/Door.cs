namespace MonoContra.Objects
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Door : AnimatedSprite
    {
        private const int ANIMATION_SPEED = 9;
        private int animaSpeedIncrement;

        public Door(Texture2D texture, int rows, int cols, Vector2 spritePos) : base(texture, rows, cols, spritePos)
        {
        }

        public void Draw(SpriteBatch spriteBatch, double scaleX, double scaleY)
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

        public void Update(Player player)
        {
            if (player.DestinationRectangle.Intersects(this.DestinationRectangle))
            {
                this.HandleCollision(player);

                // If player contains key
                if (true) 
                {
                    // Exit game, go to next level
                    this.OpenDoorAnim();
                }
            }
        }

        private void OpenDoorAnim()
        {
            this.animaSpeedIncrement++;

            if (this.animaSpeedIncrement >= ANIMATION_SPEED)
            {
                this.CurrentFrame++;
                if (this.CurrentFrame >= this.TotalFrames)
                {
                    this.CurrentFrame = this.TotalFrames - 1;
                }

                this.animaSpeedIncrement = 0;
            }
        }

        private void HandleCollision(Player player)
        {
            return;
        }
    }
}
