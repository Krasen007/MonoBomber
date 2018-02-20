namespace Start
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using Start.BackgroundItems;

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class EntryPoint : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private StaticItem topTree;
        private StaticItem rightTree;

        public EntryPoint()
        {
            this.graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here    
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        /// 
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            this.spriteBatch = new SpriteBatch(GraphicsDevice);
            this.LoadLevelOne();

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            // testing moving sprites
            // UpdateSprite(gameTime);
            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        private void UpdateSprite(GameTime gameTime)
        {
            // topTree.SpritePosition += topTree.SpriteSpeed 

            // direction velociti etc.
            this.topTree.SpritePosition += new Vector2(100f, 0f) * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        
            // Check for bounce.
            int MaxX =
                this.graphics.GraphicsDevice.Viewport.Width - topTree.BackgrItemTexture.Width;
            // int MinX = 0;
            int MaxY =
                this.graphics.GraphicsDevice.Viewport.Height - topTree.BackgrItemTexture.Height;
            // int MinY = 0;

            if (topTree.SpritePosition.X > MaxX)
            {
                topTree.SpritePosition -= new Vector2(100f, 0f);
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkRed);

            this.spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            this.spriteBatch.Draw(this.topTree.BackgrItemTexture, this.topTree.SpritePosition, Color.White);
            this.spriteBatch.Draw(this.rightTree.BackgrItemTexture, this.rightTree.SpritePosition, Color.White);

            this.spriteBatch.End();

            // TODO: Add your drawing code here
            base.Draw(gameTime);
        }

        private void LoadLevelOne()
        {
            this.topTree = new StaticItem(new Vector2(100f, 250f), new Vector2(50f, 50f));
            this.topTree.BackgrItemTexture = Content.Load<Texture2D>("Tree");

            this.rightTree = new StaticItem(new Vector2(300f, 250f), new Vector2(50f, 50f));
            this.rightTree.BackgrItemTexture = Content.Load<Texture2D>("Tree");
        }
    }
}
