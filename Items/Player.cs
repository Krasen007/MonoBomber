namespace MonoContra.Items
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    public class Player : AnimatedSprite
    {
        private SpriteState playerSate;

        private KeyboardState oldKeyState;
        private MouseState oldMouseState;

        public Player(Texture2D texture, int rows, int cols, Vector2 spritePos, Vector2 spriteSpdX, Vector2 spriteSpdY) : base(texture, rows, cols, spritePos, spriteSpdX, spriteSpdY)
        {
        }

        public SpriteState PlayerSate { get => this.playerSate; set => this.playerSate = value; }

        public void Update(KeyboardState keyState, MouseState mouseState)
        {
            if (keyState.IsKeyDown(Keys.Right) || keyState.IsKeyDown(Keys.D))
            {
                this.PlayerSate = SpriteState.MoveRight;

                this.CurrentFrame++;
                if (this.CurrentFrame >= 12)
                {
                    this.CurrentFrame = 7;
                }

                this.SpritePosition += this.SpriteSpeedX;
            }

            if (keyState.IsKeyDown(Keys.Left) || keyState.IsKeyDown(Keys.A))
            {
                this.PlayerSate = SpriteState.MoveLeft;

                // Bug when going from right to left
                this.CurrentFrame++;

                if (this.CurrentFrame >= this.TotalFrames)
                {
                    this.CurrentFrame = 19;
                }

                this.SpritePosition -= this.SpriteSpeedX;
            }

            if (keyState.IsKeyDown(Keys.Up) || keyState.IsKeyDown(Keys.W))
            {
                this.PlayerSate = SpriteState.MoveUp;

                this.CurrentFrame++;
                if (this.CurrentFrame >= 18)
                {
                    this.CurrentFrame = 13;
                }

                this.SpritePosition -= this.SpriteSpeedY;
            }

            if (keyState.IsKeyDown(Keys.Down) || keyState.IsKeyDown(Keys.S))
            {
                this.PlayerSate = SpriteState.MoveDown;

                this.CurrentFrame++;
                if (this.CurrentFrame >= 6)
                {
                    this.CurrentFrame = 0;
                }

                this.SpritePosition += this.SpriteSpeedY;
            }

            if (mouseState.LeftButton == ButtonState.Pressed && this.oldMouseState.LeftButton == ButtonState.Released)
            {
                this.SpritePosition = new Vector2(mouseState.Position.X, mouseState.Position.Y);
            }

            this.oldKeyState = keyState;
            this.oldMouseState = mouseState;
        }
    }
}
