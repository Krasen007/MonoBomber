namespace Start
{
    using System;
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

        private KeyboardState oldKeyState;

        private SpriteFont debugFont;
        private bool tildePressed = false;

        private StaticItem topTree;
        private StaticItem rightTree;
        private AnimatedSprite girlCharacter;

        public EntryPoint()
        {
            this.Window.Title = "CONTRA";
            this.graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            this.graphics.PreferredBackBufferWidth = 1280;
            this.graphics.PreferredBackBufferHeight = 720;
            this.graphics.ApplyChanges();

            ////this.graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            ////this.graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            ////graphics.IsFullScreen = true;
            ////graphics.ApplyChanges();

            this.IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here    
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            this.spriteBatch = new SpriteBatch(GraphicsDevice);
            this.debugFont = this.Content.Load<SpriteFont>("Debug");
            this.LoadLevelOne();
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            KeyboardState keyState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || keyState.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            if (this.oldKeyState.IsKeyUp(Keys.OemTilde) && keyState.IsKeyDown(Keys.OemTilde))
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

            this.oldKeyState = keyState;

            // testing moving sprites
            this.MoveTree(gameTime);
            this.girlCharacter.Update(keyState, mouseState);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here
            GraphicsDevice.Clear(Color.DarkRed);

            this.spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            // Manage Fonts
            if (this.tildePressed)
            {
                float frameRate = 1 / (float)gameTime.ElapsedGameTime.TotalSeconds;
                this.DebugInformation(frameRate);
            }

            // Level one
            this.spriteBatch.Draw(this.topTree.SpriteTexture, this.topTree.SpritePosition, Color.White);
            this.spriteBatch.Draw(this.rightTree.SpriteTexture, this.rightTree.SpritePosition, Color.White);
            this.girlCharacter.Draw(this.spriteBatch, 1.5, 1.5, "up");
            this.spriteBatch.End();

            base.Draw(gameTime);
        }

        // Replace Console WriteLine
        private void DebugInformation(float frameRate)
        {
            MouseState mouseState = Mouse.GetState();
            this.spriteBatch
                .DrawString(
                this.debugFont,
                "\n Debug info:" +
                "\n Mouse to vector: " +
                mouseState.Position.ToVector2() +
                "\n girl sprite: " +
                this.girlCharacter.SpritePosition +
                ////"\n Gametime elasped: " + gameTime.ElapsedGameTime.TotalSeconds +
                ////"\n current girl frame: " +
                ////this.girlCharacter.currentFrame +
                "\n girl backgr tex: " +
                this.girlCharacter.SpriteTexture.Width +
                "\n girl backgr tex width: " +
                this.girlCharacter.Width,
                new Vector2(10, 10),
                Color.DarkGray);
        }

        // Load textures and objects for level one
        private void LoadLevelOne()
        {
            this.topTree = new StaticItem(new Vector2(1300, 400), new Vector2(50f, 50f), new Vector2(0, 10));
            this.topTree.SpriteTexture = Content.Load<Texture2D>("Tree");

            this.rightTree = new StaticItem(new Vector2(1500, 250), new Vector2(50f, 50f), new Vector2(0, 10));
            this.rightTree.SpriteTexture = Content.Load<Texture2D>("Tree");

            Texture2D girlMoveAnim = Content.Load<Texture2D>("bomberman");
            Texture2D girlMeleAnim = Content.Load<Texture2D>("mele1");
            this.girlCharacter = new AnimatedSprite(girlMoveAnim, 4, 6, new Vector2(0, 330), new Vector2(10, 0), new Vector2(0, 10));
        }

        // Just testing moving sprites
        private void MoveTree(GameTime gameTime)
        {
            Random radomTree = new Random();

            // direction velociti etc.
            this.topTree.SpritePosition -= new Vector2(radomTree.Next(150, 300), 0f) * (float)gameTime.ElapsedGameTime.TotalSeconds;

            this.rightTree.SpritePosition -= new Vector2(radomTree.Next(150, 500), 0f) * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Check for bounce.
            int maxX =
                this.graphics.GraphicsDevice.Viewport.Width - this.topTree.SpriteTexture.Width;
            int maxY =
                this.graphics.GraphicsDevice.Viewport.Height - this.topTree.SpriteTexture.Height;

            if (this.topTree.SpritePosition.X <= 0 - this.topTree.SpriteTexture.Width)
            {
                this.topTree.SpritePosition = new Vector2(radomTree.Next(1500, 2500), radomTree.Next(0, 450));
            }

            if (this.rightTree.SpritePosition.X <= 0 - this.topTree.SpriteTexture.Width)
            {
                this.rightTree.SpritePosition = new Vector2(radomTree.Next(1500, 2500), radomTree.Next(0, 450));
            }
        }
    }
}
