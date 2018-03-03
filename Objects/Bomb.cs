namespace MonoContra.Objects
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Bomb : AnimatedSprite
    {
        public Bomb(Texture2D animatedTexture, int rows, int cols, Vector2 spritePos) : base(animatedTexture, rows, cols, spritePos)
        {
        }

        public void Update()
        {
            this.CurrentFrame++;
            if (this.CurrentFrame >= this.TotalFrames)
            {
                this.CurrentFrame = this.TotalFrames - 1;
            }
        }
    }
}
