namespace MonoContra
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using MonoContra.Enumarables;
<<<<<<< HEAD
    using MonoContra.Objects;
    using MonoContra.Utilities;

=======

    //draw
    //isAlive
    //isDestryctable
>>>>>>> 801a0072ba23e1e35df98fe763ae8cc36e78fbb7
    public class Wall : Block
    {
        private const string UNBREAKABLE_ANIMATION_KEY = "Tree";
        private const string BREAKABLE_ANIMATION_KEY = "wall";
<<<<<<< HEAD
        
=======

        public WallTypes WallType { get; set; }

>>>>>>> 801a0072ba23e1e35df98fe763ae8cc36e78fbb7
        public Wall(ContentManager content, bool health, WallTypes wallType, int gameWidth, int gameHeight, double velocity, Vector2 scale)
            : base(content, health, gameWidth, gameHeight, velocity, scale)
        {
            this.Position = new Vector2(100, 100);
            this.WallType = wallType;
            this.CurrentAnimationKey = UNBREAKABLE_ANIMATION_KEY;
        }

<<<<<<< HEAD
        public WallTypes WallType { get; set; }

        public void Update(GameTime gameTime, int gameWidth, int gameHeight)
        {
           foreach (var pair in this.Animations)
           {
=======
        public void Update(GameTime gameTime, int gameWidth, int gameHeight)
        {
            foreach (var pair in this.Animations)
            {
>>>>>>> 801a0072ba23e1e35df98fe763ae8cc36e78fbb7
                pair.Value.Update(gameTime);
           }
        }

        protected override void CreateAnimations(ContentManager content)
        {
            var unbreakableWall = content.Load<Texture2D>(UNBREAKABLE_ANIMATION_KEY);
            var breakableWall = content.Load<Texture2D>(BREAKABLE_ANIMATION_KEY);

            this.Animations.Add(UNBREAKABLE_ANIMATION_KEY, new Animation(unbreakableWall, 1, 1, 1, 239, 286)); // gameHeight / (wallPerRow * 2)
<<<<<<< HEAD
            // this.animations.Add(BREAKABLE_ANIMATION_KEY, new Animation(breakableWall, 1, 1, 1, 50, 50));
=======
            //this.animations.Add(BREAKABLE_ANIMATION_KEY, new Animation(breakableWall, 1, 1, 1, 50, 50));
>>>>>>> 801a0072ba23e1e35df98fe763ae8cc36e78fbb7
        }
    }
}
