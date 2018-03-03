namespace MonoContra.Objects
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using MonoContra.Utilities;

    public class Player : AnimatedSprite
    {
        private KeyboardState oldKeyState;
        private MouseState oldMouseState;

        private Texture2D bombAnim;
        private Bomb bomb;

        public Player(Texture2D texture, int rows, int cols, Vector2 spritePos, Vector2 spriteSpdX, Vector2 spriteSpdY) : base(texture, rows, cols, spritePos, spriteSpdX, spriteSpdY)
        {
        }

        public bool HasKey { get; private set; }

        public void Update(KeyboardState keyState, MouseState mouseState, Key key, ContentManager bombAnimation, SpriteBatch spriteBatch)
        {
            this.MovePlayer(keyState, mouseState, bombAnimation, spriteBatch);

            this.HandleKeyCollision(key);
            ////this.HandleWallCollision(wall);
        }

        private void DropBomb(SpriteBatch spriteBatch, ContentManager bombAnimation)
        {
            this.bombAnim = bombAnimation.Load<Texture2D>("bombanimation");
            this.bomb = new Bomb(this.bombAnim, 1, 5, this.SpritePosition);
            spriteBatch.Begin();
            this.bomb.Draw(spriteBatch, 1, 1);
            this.bomb.Update();
            spriteBatch.End();
        }

        private void MovePlayer(KeyboardState keyState, MouseState mouseState, ContentManager bombAnimation, SpriteBatch spriteBatch)
        {
            if (keyState.IsKeyDown(Keys.Right) || keyState.IsKeyDown(Keys.D))
            {
                this.CurrentFrame++;
                if (this.CurrentFrame >= 12)
                {
                    this.CurrentFrame = 7;
                }

                this.SpritePosition += this.SpriteSpeedX;
            }
            else if (keyState.IsKeyDown(Keys.Left) || keyState.IsKeyDown(Keys.A))
            {
                // Bug when going from right to left
                this.CurrentFrame++;

                if (this.CurrentFrame >= this.TotalFrames)
                {
                    this.CurrentFrame = 19;
                }

                this.SpritePosition -= this.SpriteSpeedX;
            }
            else if (keyState.IsKeyDown(Keys.Up) || keyState.IsKeyDown(Keys.W))
            {
                this.CurrentFrame++;
                if (this.CurrentFrame >= 18)
                {
                    this.CurrentFrame = 13;
                }

                this.SpritePosition -= this.SpriteSpeedY;
            }
            else if (keyState.IsKeyDown(Keys.Down) || keyState.IsKeyDown(Keys.S))
            {
                this.CurrentFrame++;
                if (this.CurrentFrame >= 6)
                {
                    this.CurrentFrame = 0;
                }

                this.SpritePosition += this.SpriteSpeedY;
            }
            else if (keyState.IsKeyDown(Keys.Space) || keyState.IsKeyDown(Keys.Enter))
            {
                this.DropBomb(spriteBatch, bombAnimation);                
            }

            this.oldKeyState = keyState;

            /*             
            if (mouseState.LeftButton == ButtonState.Pressed && this.oldMouseState.LeftButton == ButtonState.Released)
            {
                this.SpritePosition = new Vector2(mouseState.Position.X, mouseState.Position.Y);
            }
            */

            this.oldKeyState = keyState;
            this.oldMouseState = mouseState;
        }

        private void HandleKeyCollision(Key key)
        {
            if (this.DestinationRectangle.Intersects(key.DestinationRectangle))
            {
                this.HasKey = true;
            }
        }

        private void HandleWallCollision(Animation anim)
        {
            ////if (this.SpriteTexture.Bounds.Intersects(anim.Texture.Bounds))
            ////{
            ////    this.SpritePosition += new Vector2(0,0);
            ////}
        }
    }
}
