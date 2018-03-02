namespace MonoContra.Objects
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Enemy : AnimatedSprite
    {
        private int animationSpeed = 0;

        private SpriteState enemyState;

        public Enemy(Texture2D texture, int rows, int cols, Vector2 spritePos, Vector2 spriteSpdX, Vector2 spriteSpdY) : base(texture, rows, cols, spritePos, spriteSpdX, spriteSpdY)
        {
            this.FlipSpriteState = SpriteEffects.FlipHorizontally;
        }

        public SpriteState EnemyState { get => this.enemyState; set => this.enemyState = value; }

        // TODO: Add animations for enemy
        public void Update(Player player)
        {
            this.animationSpeed++;

            if (this.animationSpeed >= 5)
            {
                this.CurrentFrame++;
                if (this.CurrentFrame >= 7)
                {
                    this.CurrentFrame = 0;
                }

                this.animationSpeed = 0;
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
