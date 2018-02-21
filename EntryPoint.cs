namespace Start
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using MonoContra.Items;
    using Start.BackgroundItems;

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class EntryPoint : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private SpriteFont debugFont;
        private bool tildePressed = false;

        private StaticItem topTree;
        private StaticItem rightTree;
        private AnimatedSprite girlCharacter;

        public EntryPoint()
        {
            this.graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            ////this.graphics.IsFullScreen = true;
            this.graphics.PreferredBackBufferWidth = 1280;
            this.graphics.PreferredBackBufferHeight = 720;
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
            this.debugFont = this.Content.Load<SpriteFont>("Debug");
            this.LoadLevelOne();
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

            if (Keyboard.GetState().IsKeyDown(Keys.OemTilde))
            {
                if (!this.tildePressed)
                {
                    this.tildePressed = true;
                }
                else
                {
                    this.tildePressed = false;
                }
            }

            // testing moving sprites
            this.MoveTree(gameTime);
            this.girlCharacter.Update();

            // TODO: Add your update logic here
            base.Update(gameTime);
        }
        
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkRed);

            this.spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            // Manage Fonts
            if (this.tildePressed)
            {
                this.DebugInformation();
            }

            // Level one
            this.spriteBatch.Draw(this.topTree.BackgrItemTexture, this.topTree.SpritePosition, Color.White);
            this.spriteBatch.Draw(this.rightTree.BackgrItemTexture, this.rightTree.SpritePosition, Color.White);
            this.girlCharacter.Draw(this.spriteBatch, new Vector2(4, 2));
            this.spriteBatch.End();

            // TODO: Add your drawing code here
            base.Draw(gameTime);
        }

        // Replace Console WriteLine
        private void DebugInformation()
        {
            this.spriteBatch.DrawString(
                this.debugFont,
                "\n Debug info:" +
                "\n Buffer Width: " + this.graphics.PreferredBackBufferWidth +
                "\n Buffer Height: " + this.graphics.PreferredBackBufferHeight +
                ////"\n Gametime elasped: " + gameTime.ElapsedGameTime.TotalSeconds +
                "\n Tree Width: " + this.topTree.BackgrItemTexture.Width +
                "\n Tree Height: " + this.topTree.BackgrItemTexture.Height +
                "\n Tree position: " + this.topTree.SpritePosition,
                new Vector2(100, 100), Color.DarkGray);
        }

        // Load textures and objects for level one
        private void LoadLevelOne()
        {
            this.topTree = new StaticItem(new Vector2(100f, 250f), new Vector2(50f, 50f));
            this.topTree.BackgrItemTexture = Content.Load<Texture2D>("Tree");

            this.rightTree = new StaticItem(new Vector2(300f, 250f), new Vector2(50f, 50f));
            this.rightTree.BackgrItemTexture = Content.Load<Texture2D>("Tree");

            Texture2D girlMoveAnim = Content.Load<Texture2D>("girlMove1");
            this.girlCharacter = new AnimatedSprite(girlMoveAnim, 3, 3);
        }

        // Just testing moving sprites
        private void MoveTree(GameTime gameTime)
        {
            // direction velociti etc.
            this.topTree.SpritePosition += new Vector2(100f, 0f) * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Check for bounce.
            int maxX =
                this.graphics.GraphicsDevice.Viewport.Width - this.topTree.BackgrItemTexture.Width;
            int maxY =
                this.graphics.GraphicsDevice.Viewport.Height - this.topTree.BackgrItemTexture.Height;

            if (this.topTree.SpritePosition.X > maxX)
            {
                this.topTree.SpritePosition = new Vector2(250f, 100f);
            }
        }
    }
}
