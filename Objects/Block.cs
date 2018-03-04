namespace MonoContra.Objects
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using global::MonoContra.Utilities;

    public abstract class Block
    {
        // ~70px width
        public Block(ContentManager content, Vector2 position, bool health, int gameWidth, int gameHeight, double velocity, Vector2 scale)
        {
            this.Velocity = velocity;
            this.Health = true;
            this.Position = new Vector2(position.X, position.Y);
            this.CurrentAnimationKey = string.Empty;
            this.Animations = new Dictionary<string, Animation>();
            this.DestinationRectangle = new Rectangle((int)this.Position.X, (int)this.Position.Y, (int)this.Scale.X, (int)this.Scale.Y);
        }        

        public bool IsBreakable { get; protected set; }

        public bool Health { get; protected set; }

        public Vector2 Position { get; set; }

        public Rectangle DestinationRectangle { get; set; }

        protected Dictionary<string, Animation> Animations { get; set; }

        protected string CurrentAnimationKey { get; set; }

        protected double Velocity { get; set; }

        protected double Rotation { get; set; }

        protected Vector2 Scale { get; set; }

        public void Draw(SpriteBatch spriteBatch)
        {
            this.Animations[this.CurrentAnimationKey].Draw(spriteBatch, this.Rotation, this.Position, this.Scale);
        }

        public void TakeDamage()
        {
            if (this.IsBreakable == true)
            {
                this.Health = false;
            }
        }

        public bool IsAlive()
        {
            if (this.Health)
            {
                return true;
            }

            return false;
        }

        protected abstract void CreateAnimations(ContentManager content);
    }
}
