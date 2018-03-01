namespace MonoContra.Objects
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;

    class PowerUp : Block
    {
        public PowerUp(ContentManager content, Vector2 position, bool health, int gameWidth, int gameHeight, double velocity, Vector2 scale) : base(content, position, health, gameWidth, gameHeight, velocity, scale)
        {

        }

        protected override void CreateAnimations(ContentManager content)
        {
            throw new NotImplementedException();
        }
    }
}