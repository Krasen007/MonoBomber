namespace MonoBomber.Objects
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using MonoBomber.Enumerables;
    using MonoBomber.Objects;
    using MonoBomber.Utilities;
    using MonoBomber.Utils;

    public class Explosion : Block
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

        public bool DestroyWalls(List<Wall> walls)
        {
            bool result = true;

            for (int i = 0; i < walls.Count; i++)
            {
                if (walls[i].WallType == WallTypes.Unbreakable && (CollisionHelper.CollideBottom(this.DestinationRectangle, walls[i].DestinationRectangle) ||
                    CollisionHelper.CollideLeft(this.DestinationRectangle, walls[i].DestinationRectangle) ||
                    CollisionHelper.CollideRight(this.DestinationRectangle, walls[i].DestinationRectangle) ||
                    CollisionHelper.CollideTop(this.DestinationRectangle, walls[i].DestinationRectangle) ||
                    this.DestinationRectangle.Intersects(walls[i].DestinationRectangle)))
                {
                    this.Health = false;
                    result = false;
                }
                else if (walls[i].WallType == WallTypes.Breakable && (CollisionHelper.CollideBottom(this.DestinationRectangle, walls[i].DestinationRectangle) ||
                    CollisionHelper.CollideLeft(this.DestinationRectangle, walls[i].DestinationRectangle) ||
                    CollisionHelper.CollideRight(this.DestinationRectangle, walls[i].DestinationRectangle) ||
                    CollisionHelper.CollideTop(this.DestinationRectangle, walls[i].DestinationRectangle) ||
                    this.DestinationRectangle.Intersects(walls[i].DestinationRectangle)))
                {
                    walls[i].Health = false;
                    walls.Remove(walls[i]);
                    i--;
                }
            }

            return result;
        }

        protected override void CreateAnimations(ContentManager content)
        {
            Texture2D explosionAnimation = content.Load<Texture2D>("explosion");
            this.Animations.Add(EXPLOSION_ANIMATION_KEY, new Animation(explosionAnimation, 1, 1, 1, 150, 150)); // gameHeight / (wallPerRow * 2)
        }
    }
}
