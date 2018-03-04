namespace MonoContra.Objects
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using MonoContra.Enumerables;

    public class Player : AnimatedSprite
    {
        private KeyboardState oldKeyState;
        private MouseState oldMouseState;

        private Texture2D bombAnim;        // not working 

        public Player(Texture2D texture, int rows, int cols, Vector2 spritePos, Vector2 spriteSpdX, Vector2 spriteSpdY) : base(texture, rows, cols, spritePos, spriteSpdX, spriteSpdY)
        {
            this.IsAlive = true;
        }

        public bool HasKey { get; private set; }

        public bool IsAlive { get; set; }

        public Bomb Bomb { get; private set; }

        public void Update(KeyboardState keyState, MouseState mouseState, Key key, ContentManager bombAnimation, SpriteBatch spriteBatch, List<Wall> walls)
        {
            this.MovePlayer(keyState, mouseState, bombAnimation, spriteBatch);

            this.HandleKeyCollision(key);
            this.HandleWallCollision(walls);
            this.HandleSidesCollision();

            ////if (!IsAlive)
            ////{
            ////    state = GameState.GameOver;
            ////   // return state;
            ////}
            ////else
            ////{
            ////    state = GameState.GameStart;
            ////   // return state;
            ////}
        }

        private void HandleSidesCollision()
        {
            if (this.SpritePosition.X <= 0)
            {
                this.SpritePosition = new Vector2(0, this.SpritePosition.Y);
            }
            else if (this.SpritePosition.Y <= 0)
            {
                this.SpritePosition = new Vector2(this.SpritePosition.X, 0);
            }
            else if (this.SpritePosition.Y >= 1440)
            {
                this.SpritePosition = new Vector2(this.SpritePosition.X, 1440-this.Width);
            }
            else if (this.SpritePosition.X >= 2560)
            {
                this.SpritePosition = new Vector2(2560-this.Height, this.SpritePosition.Y);
            }
        }

        private void DropBomb(SpriteBatch spriteBatch, ContentManager bombAnimation)
        {
            this.bombAnim = bombAnimation.Load<Texture2D>("bombanimation"); // not working
            this.Bomb = new Bomb(this.bombAnim, 1, 5, this.SpritePosition); // not working
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

        private void HandleWallCollision(List<Wall> walls)
        {
            foreach (var wall in walls)
            {
                if (this.DestinationRectangle.Intersects(wall.DestinationRectangle))
                {
                    // TODO: Fix collision
                    this.SpritePosition = new Vector2(this.SpritePosition.X - 2, this.SpritePosition.Y);
                }
            }
        }
    }
}