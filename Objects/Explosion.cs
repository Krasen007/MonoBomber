namespace MonoBomber.Objects
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using MonoBomber.Enumerables;
    using MonoBomber.Objects;
    using MonoBomber.Utilities;
    class Explosion : Block
    {
        private const string EXPLOSION_ANIMATION_KEY = "explosion";

        public Explosion(ContentManager content, Vector2 position, bool health, double velocity, Vector2 scale)
                : base(content, position, health, velocity, scale)
        {
            this.CurrentAnimationKey = EXPLOSION_ANIMATION_KEY;
            this.Health = health;
            this.Scale = scale * 0.5f;
            this.CreateAnimations(content);
        }

        public void Update(GameTime gameTime)
        {
            foreach (var pair in this.Animations)
            {
                pair.Value.Update(gameTime);
            }
        }

        protected override void CreateAnimations(ContentManager content)
        {
            Texture2D explosionAnimation = content.Load<Texture2D>("explosion");
            this.Animations.Add(EXPLOSION_ANIMATION_KEY, new Animation(explosionAnimation, 1, 1, 1, 150, 150)); // gameHeight / (wallPerRow * 2)
        }
    }
}
