namespace MonoContra.Objects
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Bomb : AnimatedSprite
    {
        private const int ANIMATION_SPEED = 9;
        private int animaSpeedIncrement;

        public Bomb(Texture2D animatedTexture, int rows, int cols, Vector2 spritePos) : base(animatedTexture, rows, cols, spritePos)
        {
        }

        public void Update()
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
