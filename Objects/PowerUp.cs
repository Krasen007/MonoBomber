namespace MonoContra.Objects
{  
    using Enumerables;
    using Microsoft.Xna.Framework;    
    using Microsoft.Xna.Framework.Graphics;

    public class PowerUp : AnimatedSprite
    {
        public const string BOMBSATCHEL_ANIMATION_KEY = "bombSathel";
        public const string SONIC_ANIMATION_KEY = "sonic";
        public const string DRAGONLANCE_ANIMATION_KEY = "dragonLance";        
        //private const string MARIOSTAR_ANIMATION_KEY = "marioStar";

        protected PowerUpTypes powerUpType;
        protected const int ANIMATION_SPEED = 20;
        protected int animaSpeedIncrement;

        public PowerUp(Texture2D animatedTexture, int rows, int cols, Vector2 spritePos, Vector2 spriteSpdX, Vector2 spriteSpdY) : base(animatedTexture, rows, cols, spritePos, spriteSpdX, spriteSpdY)
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

        public void KeyAnimation()
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

        public void HandleCollision(Player player)
        {
            this.SpritePosition = new Vector2(-100, -100);
        }
        public PowerUpTypes PowerUpType
        {
            get
            {
                return this.powerUpType;
            }

            set
            {
                switch (this.powerUpType)
                {
                    // TODO : scale 
                    case PowerUpTypes.BombSatchel:
                    case PowerUpTypes.DragonLance:
                        this.powerUpType = value;
                        break;
                    case PowerUpTypes.Sonic:
                        this.powerUpType = value;
                        break;
                    case PowerUpTypes.MarioStrar:
                        this.powerUpType = value;
                        break;
                }
            }
        }        
    }
}