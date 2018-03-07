namespace MonoBomber
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using MonoBomber.Enumerables;
    using MonoBomber.Objects;
    using MonoBomber.Utilities;

    public class Wall : Block
    {
        private const string UNBREAKABLE_ANIMATION_KEY = "grave";
        private const string BREAKABLE_ANIMATION_KEY = "rock";
        private WallTypes wallType;

        public Wall(ContentManager content, Vector2 position, bool health, WallTypes wallType, double velocity, Vector2 scale)
            : base(content, position, health, velocity, scale)
        {
            this.WallType = wallType;
            if (wallType == WallTypes.Unbreakable)
            {
                this.CurrentAnimationKey = UNBREAKABLE_ANIMATION_KEY;
                this.Scale = scale * 0.15f;
            }
            else
            {
                this.CurrentAnimationKey = BREAKABLE_ANIMATION_KEY;
                this.Scale = scale * 0.50f;
            }

            this.CreateAnimations(content);
        }

        public WallTypes WallType
        {
            get
            {
                return this.wallType;
            }

            set
            {
                if (value == WallTypes.Unbreakable)
                {
                    this.Scale = this.Scale * 0.15f;
                    this.wallType = value;
                }
                else
                {
                    this.Scale = this.Scale * 0.5f;
                    this.wallType = value;
                }
            }
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
            if (this.WallType == WallTypes.Breakable)
            {
                var breakableWall = content.Load<Texture2D>(BREAKABLE_ANIMATION_KEY);
                this.Animations.Add(BREAKABLE_ANIMATION_KEY, new Animation(breakableWall, 1, 1, 1, 128, 128));
                return;
            }

            var unbreakableWall = content.Load<Texture2D>(UNBREAKABLE_ANIMATION_KEY);
            this.Animations.Add(UNBREAKABLE_ANIMATION_KEY, new Animation(unbreakableWall, 1, 1, 1, 512, 512)); // gameHeight / (wallPerRow * 2)
        }
    }
}
