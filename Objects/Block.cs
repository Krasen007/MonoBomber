namespace MonoContra.Objects
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using global::MonoContra.Utilities;

    public abstract class Block
    {
        private Dictionary<string, Animation> animations;
        private string currentAnimationKey;

        private double velocity;
        private double rotation;
        private Vector2 scale;

        public Block(ContentManager content, int gameWidth, int gameHeight, double velocity, Vector2 scale)
        {
            this.Velocity = velocity;
            this.Scale = scale;
            this.CurrentAnimationKey = string.Empty;
            this.Animations = new Dictionary<string, Animation>();

            this.CreateAnimations(content);
        }

        public bool IsBreakable { get; protected set; }

        public Vector2 Position { get; }

        protected Dictionary<string, Animation> Animations { get => this.animations; set => this.animations = value; }

        protected string CurrentAnimationKey { get => this.currentAnimationKey; set => this.currentAnimationKey = value; }

        protected double Velocity { get => this.velocity; set => this.velocity = value; }

        protected double Rotation { get => this.rotation; set => this.rotation = value; }

        protected Vector2 Scale { get => this.scale; set => this.scale = value; }

        public void Draw(SpriteBatch spriteBatch)
        {
            this.Animations[this.CurrentAnimationKey].Draw(spriteBatch, this.Rotation, this.Position, this.Scale);
        }

        protected abstract void CreateAnimations(ContentManager content);
    }
}
