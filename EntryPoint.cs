namespace MonoContra
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using MonoContra.Objects;

    public class EntryPoint : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private KeyboardState oldKeyState;

        private SpriteFont debugFont;
        private bool tildePressed;

        private StaticItem topTree;
        private StaticItem rightTree;
        private Player player;
        private Enemy enemy;

        private StaticItem backgrTree;

        private bool loadOnce;

        private GameState gameState;

        public EntryPoint()
        {
            this.Window.Title = "CONTRA";
            this.graphics = new GraphicsDeviceManager(this);
            this.Content.RootDirectory = "Content";

            this.graphics.PreferredBackBufferWidth = 1280;
            this.graphics.PreferredBackBufferHeight = 720;
            this.graphics.ApplyChanges();

            ////this.graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            ////this.graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            ////graphics.IsFullScreen = true;
            ////graphics.ApplyChanges();

            this.IsMouseVisible = true;
        }

        private enum GameState
        {
            MainMenu,
            LevelOne,
            EndOfGame,
        }

        protected override void Initialize()
        {
            this.gameState = GameState.MainMenu;
            this.loadOnce = true;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(GraphicsDevice);
            this.debugFont = this.Content.Load<SpriteFont>("Debug");
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();

            if (keyState.IsKeyDown(Keys.Escape))
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

            // Update states
            switch (this.gameState)
            {
                case GameState.MainMenu:
                    this.UpdateMainMenu(gameTime, keyState);
                    break;
                case GameState.LevelOne:
                    this.UpdateGameplay(gameTime, keyState, mouseState);
                    break;
                case GameState.EndOfGame:
                    this.UpdateEndOfGame(gameTime);
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkRed);
            this.spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            // State draws, loading of items
            switch (this.gameState)
            {
                case GameState.MainMenu:
                    this.DrawMainMenu(gameTime);
                    break;
                case GameState.LevelOne:
                    this.DrawGameplay(gameTime);
                    break;
                case GameState.EndOfGame:
                    this.DrawEndOfGame(gameTime);
                    break;
            }

            this.spriteBatch.End();
            base.Draw(gameTime);
        }

        #region Update logic and states

        private void UpdateMainMenu(GameTime gameTime, KeyboardState keyState)
        {
            // Respond to user input for menu selections, etc
            if (this.oldKeyState.IsKeyDown(Keys.Enter) || this.oldKeyState.IsKeyDown(Keys.Space))
            {
                this.gameState = GameState.LevelOne;
                this.loadOnce = true;
            }
        }

        private void DrawMainMenu(GameTime gameTime)
        {
            // Draw the main menu, any active selections, etc
            if (this.loadOnce)
            {
                this.LoadMainMenu();
            }

            this.spriteBatch.DrawString(
                this.debugFont,
                "Press enter or space to START GAME \n" +
                "W,A,S,D to move character, Esc for Exit.",
                new Vector2(600, 400),
                Color.CadetBlue);
            this.spriteBatch.Draw(this.backgrTree.SpriteTexture, new Vector2(300, 300), Color.White);
        }

        private void UpdateGameplay(GameTime gameTime, KeyboardState keyState, MouseState mouseState)
        {
            // Respond to user actions in the game.
            // Update enemies
            // Handle collisions
            // if (true)//playerDied)
            //     _state = GameState.EndOfGame;
            this.player.Update(keyState, mouseState);
            this.enemy.Update(this.player);

            // Testing moving sprites
            this.MoveTree(gameTime);

            // When game over contition is met
            // this.loadOnce = true;
        }

        private void DrawGameplay(GameTime gameTime)
        {
            // Draw the background the level
            // Draw enemies
            // Draw the player
            // Draw particle effects, etc

            // Level one
            if (this.loadOnce)
            {
                this.LoadLevelOne();
            }

            this.spriteBatch.Draw(this.topTree.SpriteTexture, this.topTree.SpritePosition, Color.White);
            this.spriteBatch.Draw(this.rightTree.SpriteTexture, this.rightTree.SpritePosition, Color.White);

            this.player.Draw(this.spriteBatch, 1.25, 1.25, this.player.PlayerSate);
            this.enemy.Draw(this.spriteBatch, 0.25, 0.25, this.enemy.EnemyState);

            // Manage debug font
            if (this.tildePressed)
            {
                this.DebugInformation();
            }
        }

        private void UpdateEndOfGame(GameTime gameTime)
        {
            // Update scores
            // Do any animations, effects, etc for getting a high score
            // Respond to user input to restart level, or go back to main menu
            // if (true)//pushedMainMenuButton)
            //      _state = GameState.MainMenu;
            // else if (true)//pushedRestartLevelButton)
            // {
            //    //ResetLevel();
            //    _state = GameState.Gameplay;
            // }
        }

        private void DrawEndOfGame(GameTime gameTime)
        {
            // Draw text and scores
            // Draw menu for restarting level or going back to main menu
        }

        #endregion

        #region Loading        

        // Load textures and objects for level one
        private void LoadLevelOne()
        {
            this.topTree = new StaticItem(new Vector2(1300, 400), new Vector2(50f, 50f), new Vector2(0, 10));
            this.topTree.SpriteTexture = Content.Load<Texture2D>("Tree");

            this.rightTree = new StaticItem(new Vector2(1500, 250), new Vector2(50f, 50f), new Vector2(0, 10));
            this.rightTree.SpriteTexture = Content.Load<Texture2D>("Tree");

            Texture2D playerMoves = Content.Load<Texture2D>("bomberman");
            this.player = new Player(playerMoves, 4, 6, new Vector2(0, 330), new Vector2(10, 0), new Vector2(0, 10));

            Texture2D badGirl = Content.Load<Texture2D>("mele1");
            this.enemy = new Enemy(badGirl, 3, 3, new Vector2(500, 500), new Vector2(4, 0), new Vector2(0, 0));

            this.loadOnce = false;
        }

        private void LoadMainMenu()
        {
            this.backgrTree = new StaticItem(new Vector2(125, 125), new Vector2(50f, 50f), new Vector2(0, 10));
            this.backgrTree.SpriteTexture = Content.Load<Texture2D>("Tree");
            this.loadOnce = false;
        }

        #endregion

        // Replace Console WriteLine
        private void DebugInformation()
        {
            MouseState mouseState = Mouse.GetState();
            this.spriteBatch.DrawString(
                this.debugFont,
                "\n Debug info:" +
                "\n Mouse to vector: " + mouseState.Position.ToVector2() +
                "\n girl sprite: " + this.player.SpritePosition +
                "\n Current State: " + this.gameState +
                "\n current enemy destination rect: " + this.enemy.DestinationRectangle +
                "\n girl backgr tex: " + this.player.SpriteTexture.Width +
                "\n girl backgr tex width: " + this.player.Width,
                new Vector2(10, 10),
                Color.DarkGray);
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
