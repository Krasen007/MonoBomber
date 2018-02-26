namespace MonoContra.Objects
{
    using global::MonoContra.Utilities;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using System.Collections.Generic;

    class Block
    {
        protected Dictionary<string, Animation> animations;
        protected string currentAnimationKey;

        protected double velocity;
        protected Vector2 position;
        protected double rotation;
        protected Vector2 scale;

        public bool isBreakable { get; protected set; }
        public Vector2 Position
        {
            get
            {
                return this.position;
            }
        }

        public Block(ContentManager Content, int gameWidth, int gameHeight, double velocity, Vector2 scale)
        {
            this.velocity = velocity;
            this.scale = scale;
            this.currentAnimationKey = "";
            this.animations = new Dictionary<string, Animation>();

            this.CreateAnimations(Content);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            this.animations[this.currentAnimationKey].Draw(spriteBatch, this.rotation, this.position, this.scale);
        }

        protected abstract void CreateAnimations(ContentManager Content);
    }
}
