namespace MonoContra
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using global::MonoContra.Utilities;
    using global::MonoContra.Objects;
    using MonoContra.Enumarables;
    //draw
    //isAlive
    //isDestryctable
    public class Wall : Block
    {

        private const string UNBREAKABLE_ANIMATION_KEY = "Tree";
        private const string BREAKABLE_ANIMATION_KEY = "wall";

        public WallTypes WallType { get; set; }
        public Wall(ContentManager content,bool health, WallTypes wallType, int gameWidth, int gameHeight, double velocity, Vector2 scale)
            :base(content, health, gameWidth, gameHeight, velocity, scale)
        {
            this.Position = new Vector2(100, 100);
            this.WallType = wallType;
            this.CurrentAnimationKey = UNBREAKABLE_ANIMATION_KEY;
        }
        public void Update(GameTime gameTime, int gameWidth, int gameHeight)
        {
           foreach(var pair in this.animations)
            {
                pair.Value.Update(gameTime);
            }
        }

        protected override void CreateAnimations(ContentManager content)
        {
            var unbreakableWall = content.Load<Texture2D>(UNBREAKABLE_ANIMATION_KEY);
            var breakableWall = content.Load<Texture2D>(BREAKABLE_ANIMATION_KEY);


            this.animations.Add(UNBREAKABLE_ANIMATION_KEY, new Animation(unbreakableWall,1, 1,1,239, 286)); //gameHeight / (wallPerRow * 2)
            //this.animations.Add(BREAKABLE_ANIMATION_KEY, new Animation(breakableWall, 1, 1, 1, 50, 50));
        }
    }
}
