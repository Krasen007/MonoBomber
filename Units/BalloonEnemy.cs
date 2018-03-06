namespace MonoContra.Units
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using MonoContra.Objects;
    using MonoContra.Utils;

    public class BalloonEnemy : AnimatedSprite
    {
        private const int ANIMATION_SPEED = 5;
        private int animaSpeedIncrement;

        private Random randomDirection = new Random();
        private float elapsedTime = 0;
        private int direction;

        public BalloonEnemy(Texture2D texture, int rows, int cols, Vector2 spritePos, Vector2 spriteSpdX, Vector2 spriteSpdY) : base(texture, rows, cols, spritePos, spriteSpdX, spriteSpdY)
        {
            this.IsAlive = true;
        }

        public bool IsAlive { get; set; }

        public void Update(SpriteBatch spriteBatch, List<Wall> walls, GameTime gameTime, Player player)
        {
            this.Move(spriteBatch, gameTime);
            this.HandleWallCollision(walls);
            this.HandleSidesCollision();
            this.HandlePlayerCollision(player);
        }

        private void HandleWallCollision(List<Wall> walls)
        {
            foreach (var wall in walls)
            {
                if (CollisionHelper.CollideTop(this.DestinationRectangle, wall.DestinationRectangle))
                {
                    // this.CurrentFrame = 0;
                    this.SpritePosition -= this.SpriteSpeedY;
                }
                else if (CollisionHelper.CollideBottom(this.DestinationRectangle, wall.DestinationRectangle))
                {
                    // this.CurrentFrame = 13;
                    this.SpritePosition += this.SpriteSpeedY;
                }
                else if (CollisionHelper.CollideRight(this.DestinationRectangle, wall.DestinationRectangle))
                {
                    // this.CurrentFrame = 19;
                    this.SpritePosition += this.SpriteSpeedX;
                }
                else if (CollisionHelper.CollideLeft(this.DestinationRectangle, wall.DestinationRectangle))
                {
                    // this.CurrentFrame = 7;
                    this.SpritePosition -= this.SpriteSpeedX;
                }
            }
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
            else if (this.SpritePosition.Y >= 1440 - this.Width)
            {
                this.SpritePosition = new Vector2(this.SpritePosition.X, 1440 - this.Width);
            }
            else if (this.SpritePosition.X >= 2560 - this.Height)
            {
                this.SpritePosition = new Vector2(2560 - this.Height, this.SpritePosition.Y);
            }
        }

        private void HandlePlayerCollision(Player player)
        {
            if (this.DestinationRectangle.Intersects(player.DestinationRectangle))
            {
                player.IsAlive = false;
                player.NumberOfLives--;
            }
        }

        private void Move(SpriteBatch spriteBatch, GameTime gameTime)
        {
            this.elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            // Get a new random direction every 1 second
            if (this.elapsedTime > 1)
                {
                this.elapsedTime -= 1; // Subtract the 1 second we've already checked
                this.direction = this.randomDirection.Next(0, 4);
                }

            if (this.direction == 0)
            {
                this.SpritePosition += new Vector2(this.randomDirection.Next(0, 5), 0);
            }
            else if (this.direction == 1)
            {
                this.SpritePosition -= new Vector2(0, this.randomDirection.Next(0, 5));
            }
            else if (this.direction == 2)
            {
                this.SpritePosition += new Vector2(0, this.randomDirection.Next(0, 5));
            }
            else if (this.direction == 3)
            {
                this.SpritePosition -= new Vector2(this.randomDirection.Next(0, 5), 0);
            }

            this.UpdateAnimation();
        }

        private void UpdateAnimation()
        {
            this.animaSpeedIncrement++;

            if (this.animaSpeedIncrement >= ANIMATION_SPEED)
            {
                this.CurrentFrame++;
                if (this.CurrentFrame >= 8)
                {
                    this.CurrentFrame = 0;
                }

                this.animaSpeedIncrement = 0;
            }
        }
    }
}
