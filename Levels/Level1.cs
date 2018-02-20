/*

namespace Start.Levels
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using Start.BackgroundItems;
    using Start.Levels;

    public class Level1
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private StaticItem topTree;
        private StaticItem rightTree;

        public StaticItem TopTree { get => topTree; set => topTree = value; }
        public StaticItem RightTree { get => rightTree; set => rightTree = value; }

        // private StaticItem topTree;
        public Level1(SpriteBatch sB, GraphicsDeviceManager gR)
        {
            this.spriteBatch = sB;
            this.graphics = gR;
        }

        public void LoadLevelOne()
        {
            this.TopTree = new StaticItem(new Vector2(100f, 250f), new Vector2(50f, 50f));
            this.RightTree = new StaticItem(new Vector2(300f, 250f), new Vector2(50f, 50f));
        }

        private void UpdateSprite(GameTime gameTime)
        {
            // topTree.SpritePosition += topTree.SpriteSpeed 

            // direction velociti etc.
            this.TopTree.SpritePosition += new Vector2(100f, 0f) * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Check for bounce.
            int MaxX =
                this.graphics.GraphicsDevice.Viewport.Width - TopTree.BackgrItemTexture.Width;
            // int MinX = 0;
            int MaxY =
                this.graphics.GraphicsDevice.Viewport.Height - TopTree.BackgrItemTexture.Height;
            // int MinY = 0;

            if (TopTree.SpritePosition.X > MaxX)
            {
                TopTree.SpritePosition -= new Vector2(100f, 0f);
            }
        }

        public void Draw()
        {
            this.spriteBatch.Draw(this.TopTree.BackgrItemTexture, this.TopTree.SpritePosition, Color.White);
            this.spriteBatch.Draw(this.RightTree.BackgrItemTexture, this.RightTree.SpritePosition, Color.White);
        }
    }
}

*/