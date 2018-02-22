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
            this.graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            ////this.graphics.IsFullScreen = true;
            this.graphics.PreferredBackBufferWidth = 1280;
            this.graphics.PreferredBackBufferHeight = 720;
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
                this.DebugInformation();
            }

            // Level one
            this.spriteBatch.Draw(this.topTree.BackgrItemTexture, this.topTree.SpritePosition, Color.White);
            this.spriteBatch.Draw(this.rightTree.BackgrItemTexture, this.rightTree.SpritePosition, Color.White);
            this.girlCharacter.Draw(this.spriteBatch, 0.5, 0.5);
            this.spriteBatch.End();

            base.Draw(gameTime);
        }

        // Replace Console WriteLine
        private void DebugInformation()
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
                "\n Tree Width: " + 
                this.topTree.BackgrItemTexture.Width +
                "\n Tree Height: " + 
                this.topTree.BackgrItemTexture.Height +
                "\n Tree position: " + 
                this.topTree.SpritePosition,
                new Vector2(10, 10), 
                Color.DarkGray);
        }

        // Load textures and objects for level one
        private void LoadLevelOne()
        {
            this.topTree = new StaticItem(new Vector2(1300, 400), new Vector2(50f, 50f));
            this.topTree.BackgrItemTexture = Content.Load<Texture2D>("Tree");

            this.rightTree = new StaticItem(new Vector2(1500, 250), new Vector2(50f, 50f));
            this.rightTree.BackgrItemTexture = Content.Load<Texture2D>("Tree");

            Texture2D girlMoveAnim = Content.Load<Texture2D>("girlMove1");
            this.girlCharacter = new AnimatedSprite(girlMoveAnim, 3, 3, new Vector2(0, 330), new Vector2(10, 0));
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
                this.graphics.GraphicsDevice.Viewport.Width - this.topTree.BackgrItemTexture.Width;
            int maxY =
                this.graphics.GraphicsDevice.Viewport.Height - this.topTree.BackgrItemTexture.Height;

            if (this.topTree.SpritePosition.X <= 0 - this.topTree.BackgrItemTexture.Width)
            {
                this.topTree.SpritePosition = new Vector2(radomTree.Next(1500, 2500), radomTree.Next(0, 450));
            }

            if (this.rightTree.SpritePosition.X <= 0 - this.topTree.BackgrItemTexture.Width)
            {
                this.rightTree.SpritePosition = new Vector2(radomTree.Next(1500, 2500), radomTree.Next(0, 450));
            }
        }
    }
}
