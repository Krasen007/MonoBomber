namespace MonoContra.Objects
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    class PowerUpMarioStar : AnimatedSprite
    {
        private const int ANIMATION_SPEED = 20;
        private int animaSpeedIncrement;

        public PowerUpMarioStar(Texture2D animatedTexture, int rows, int cols, Vector2 spritePos, Vector2 spriteSpdX, Vector2 spriteSpdY) : base(animatedTexture, rows, cols, spritePos, spriteSpdX, spriteSpdY)
        {
        }
        public void Update(Player player)
        {
            this.KeyAnimation();

            if (player.DestinationRectangle.Intersects(this.DestinationRectangle))
            {
                this.HandleCollision(player);
            }
        }

        private void KeyAnimation()
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
            // Hide powerup from map
            this.SpritePosition = new Vector2(-100, -100);
        }
    }
}
