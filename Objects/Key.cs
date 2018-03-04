namespace MonoContra.Objects
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Key : AnimatedSprite
    {
        private const int ANIMATION_SPEED = 20;
        private int animaSpeedIncrement;
        private bool pastOne = false;

        public Key(Texture2D animatedTexture, int rows, int cols, Vector2 spritePos) : base(animatedTexture, rows, cols, spritePos)
        {
        }

        public void Update(Player player)
        {
            this.KeyAnimation();

            if (player.DestinationRectangle.Intersects(this.DestinationRectangle) && player.HasKey)
            {
                this.HandleCollision(player);
            }
        }

        private void KeyAnimation()
        {
            this.animaSpeedIncrement++;

            if (this.animaSpeedIncrement >= ANIMATION_SPEED)
            {
                if (!this.pastOne)
                {
                    this.CurrentFrame++;
                    if (this.CurrentFrame >= this.TotalFrames)
                    {
                        this.CurrentFrame = 1;
                        this.pastOne = true;
                    }
                }
                else if (this.pastOne)
                {
                    this.CurrentFrame = 0;
                    this.pastOne = false;
                }

                this.animaSpeedIncrement = 0;
            }
        }

        private void HandleCollision(Player player)
        {
            // Hide key from map
            this.SpritePosition = new Vector2(-100, -100);
        }
    }
}
