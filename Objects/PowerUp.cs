namespace MonoContra.Objects
{
    using System;
    using Enumerables;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;

    public class PowerUp : Block
    {
        private PowerUpTypes powerUpType;
        
        private const string BOMBSATCHEL_ANIMATION_KEY = "bombSathel";
        private const string SONIC_ANIMATION_KEY = "sonic";
        private const string DRAGONLANCE_ANIMATION_KEY = "dragonLance";
        //add mario star 
        private const string MARIOSTAR_ANIMATION_KEY = "marioStar";

        public PowerUp(ContentManager content, Vector2 position, bool health, PowerUpTypes powerUpType, int gameWidth, int gameHeight, double velocity, Vector2 scale) : base(content, position, health, gameWidth, gameHeight, velocity, scale)
        {
            this.PowerUpType = powerUpType;
            switch (powerUpType)
            {
                // TODO : scale 
                case PowerUpTypes.BombSatchel:
                    this.CurrentAnimationKey = BOMBSATCHEL_ANIMATION_KEY;
                    break;
                case PowerUpTypes.Sonic:
                    this.CurrentAnimationKey =SONIC_ANIMATION_KEY;
                    break;
                case PowerUpTypes.DragonLance:
                    this.CurrentAnimationKey = DRAGONLANCE_ANIMATION_KEY;
                    break;
                case PowerUpTypes.MarioStrar:
                    this.CurrentAnimationKey = MARIOSTAR_ANIMATION_KEY;
                    break;
            }
            this.CreateAnimations(content);
        }

        public PowerUpTypes PowerUpType
        {
            get
            {
                return this.powerUpType;
            }
            set
            {
                switch (powerUpType)
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


        protected override void CreateAnimations(ContentManager content)
        {
            throw new NotImplementedException();
        }
    }
}