namespace MonoBomber.Objects
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Bomb : AnimatedSprite
    {
        private const int ANIMATION_SPEED = 9;
        private const int BOMB_EXPLOSION_IN = 4000;

        private int animaSpeedIncrement;

         private bool health;
        public Bomb(Texture2D animatedTexture, int rows, int cols, Vector2 spritePos) : base(animatedTexture, rows, cols, spritePos)
        {
            this.health = true;
        }

        public bool Health { get; set; }       

        public void Update(Player player)
        {
            this.UpdateAnimation();

            if (player.DestinationRectangle.Intersects(this.DestinationRectangle)) // && player.HasKey)
            {
                this.HandlePlayerCollision(player);
            }
        }

        private void HandlePlayerCollision(Player player)
        {
            return;
        }

        public void UpdateAnimation()
        {
            this.animaSpeedIncrement++;

            if (this.animaSpeedIncrement >= ANIMATION_SPEED)
            {
                this.CurrentFrame++;
                if (this.CurrentFrame >= this.TotalFrames)
                {
                    this.CurrentFrame = 0;
                }

                this.animaSpeedIncrement = 0;
            }
        }
    }
}
