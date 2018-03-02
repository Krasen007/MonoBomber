namespace MonoContra.Objects
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Enemy : AnimatedSprite
    {
        private const int ANIMATION_SPEED = 5;
        private int animaSpeedIncrement;

        public Enemy(Texture2D texture, int rows, int cols, Vector2 spritePos, Vector2 spriteSpdX, Vector2 spriteSpdY) : base(texture, rows, cols, spritePos, spriteSpdX, spriteSpdY)
        {
            this.FlipSpriteState = SpriteEffects.FlipHorizontally;
        }
        
        // TODO: Add animations for enemy
        public void Update(Player player)
        {
            this.animaSpeedIncrement++;

            if (this.animaSpeedIncrement >= ANIMATION_SPEED)
            {
                this.CurrentFrame++;
                if (this.CurrentFrame >= 7)
                {
                    this.CurrentFrame = 0;
                }

                this.animaSpeedIncrement = 0;
            }

            this.HandleCollision(player);
        }

        private void HandleCollision(AnimatedSprite player)
        {
            if (this.DestinationRectangle.Intersects(player.DestinationRectangle))
            {
                this.SpriteTexture.Dispose();
            }
        }
    }
}
