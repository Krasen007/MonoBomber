namespace MonoContra
{
    using System;
    using Enumerables;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using Objects;

    public class EntryPoint : Game
    {
        private const int GAME_WIDTH = 1280;
        private const int GAME_HEIGHT = 720;

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private KeyboardState oldKeyState;

        private SpriteFont debugFont;
        private bool tildePressed;

        private StaticItem background;
        private StaticItem topTree;
        private StaticItem rightTree;
        private Player player;
        private Enemy enemy;
        private Wall wall;
        private Wall wall1;

        private Wall rock;
        private StaticItem backgrTree;

        private bool loadOnce;

        private GameState gameState;

        public EntryPoint()
        {
            this.Window.Title = "CONTRA";
            this.graphics = new GraphicsDeviceManager(this);
            this.Content.RootDirectory = "Content";

            this.graphics.PreferredBackBufferWidth = GAME_WIDTH;
            this.graphics.PreferredBackBufferHeight = GAME_HEIGHT;
            this.graphics.ApplyChanges();

            ////this.graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            ////this.graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            ////graphics.IsFullScreen = true;
            ////graphics.ApplyChanges();

            this.IsMouseVisible = true;
        }

        private enum GameState
        {
            Intro,
            MainMenu,
            GameStart,
            PAUSE,
            GameOver,
        }

        protected override void Initialize()
        {
            this.gameState = GameState.MainMenu;
            this.loadOnce = true;

            this.wall = new Wall(Content, new Vector2(100, 100), true, WallTypes.Unbreakable, GAME_WIDTH, GAME_HEIGHT, 8, new Vector2(1f, 1f));
            this.wall1 = new Wall(Content, new Vector2(170, 100), true, WallTypes.Unbreakable, GAME_WIDTH, GAME_HEIGHT, 8, new Vector2(1f, 1f));

            this.rock = new Wall(Content, new Vector2(170, 170), true, WallTypes.Breakable, GAME_WIDTH, GAME_HEIGHT, 8, new Vector2(1f, 1f));

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

            if (this.oldKeyState.IsKeyDown(Keys.Escape) && keyState.IsKeyUp(Keys.Escape))
            {
                this.Exit();
            }
            
            // Update states
            switch (this.gameState)
            {
                case GameState.Intro:
                    // TODO: add this
                    this.gameState = GameState.MainMenu;
                    break;
                case GameState.MainMenu:
                    this.UpdateMainMenu(gameTime, keyState);
                    break;
                case GameState.GameStart:
                    this.UpdateGameStart(gameTime, keyState, mouseState);
                    break;
                case GameState.PAUSE:
                    if (this.oldKeyState.IsKeyDown(Keys.P) && keyState.IsKeyUp(Keys.P))
                    {
                        this.gameState = GameState.GameStart;
                    }

                    this.oldKeyState = keyState;
                    break;
                case GameState.GameOver:
                    this.UpdateGameOver(gameTime);
                    break;
            }

            this.wall.Update(gameTime, GAME_WIDTH, GAME_HEIGHT);
            this.wall1.Update(gameTime, GAME_WIDTH, GAME_HEIGHT);
            this.rock.Update(gameTime, GAME_WIDTH, GAME_HEIGHT);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {            
            switch (this.gameState)
            {
                case GameState.Intro:
                    this.gameState = GameState.MainMenu;
                    break;
                case GameState.MainMenu:
                    this.DrawMainMenu(gameTime);
                    break;
                case GameState.GameStart:
                    this.DrawGameStart(gameTime);
                    break;
                case GameState.GameOver:
                    this.DrawGameOver(gameTime);
                    break;
            }

            this.spriteBatch.Begin();
            this.wall.Draw(this.spriteBatch);
            this.wall1.Draw(this.spriteBatch);
            this.rock.Draw(this.spriteBatch);
            this.spriteBatch.End();
            base.Draw(gameTime);
        }

        #region Update logic and states

        private void UpdateMainMenu(GameTime gameTime, KeyboardState keyState)
        {
            // Respond to user input for menu selections, etc
            if (this.oldKeyState.IsKeyDown(Keys.Enter) || this.oldKeyState.IsKeyDown(Keys.Space))
            {
                this.gameState = GameState.GameStart;
                this.loadOnce = true;
            }

            this.oldKeyState = keyState;
        }

        private void DrawMainMenu(GameTime gameTime)
        {
            // Draw the main menu, any active selections, etc
            if (this.loadOnce)
            {
                this.LoadMainMenu();
            }

            this.graphics.GraphicsDevice.Clear(Color.DarkRed);
            this.spriteBatch.Begin();
            this.spriteBatch.DrawString(
                this.debugFont,
                "Press enter or space to START GAME \n" +
                "W,A,S,D to move character, P for PAUSE, Esc for Exit.",
                new Vector2(600, 400),
                Color.CadetBlue);
            this.spriteBatch.Draw(this.backgrTree.SpriteTexture, new Vector2(300, 300), Color.White);
            this.spriteBatch.End();
        }

        private void UpdateGameStart(GameTime gameTime, KeyboardState keyState, MouseState mouseState)
        {
            // Respond to user actions in the game.
            // Update enemies
            // Handle collisions
            if (this.oldKeyState.IsKeyDown(Keys.OemTilde) && keyState.IsKeyUp(Keys.OemTilde))
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

            if (this.oldKeyState.IsKeyDown(Keys.P) && keyState.IsKeyUp(Keys.P))
            {
                // TODO: Add more stuff
                //this.graphics.GraphicsDevice.Clear(Color.Green);
                this.gameState = GameState.PAUSE;
            }

            this.oldKeyState = keyState;

            this.player.Update(keyState, mouseState);
            this.enemy.Update(this.player);
            this.MoveTree(gameTime);

            // if (true)//playerDied)
            //     _state = GameState.EndOfGame;

            // When game over contition is met
            // this.loadOnce = true;
        }

        private void DrawGameStart(GameTime gameTime)
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

            this.spriteBatch.Begin();

            this.background.Draw(this.spriteBatch);

            this.topTree.Draw(this.spriteBatch, this.topTree.SpriteTexture, this.topTree.SpritePosition, Color.White);
            this.rightTree.Draw(this.spriteBatch, this.rightTree.SpriteTexture, this.rightTree.SpritePosition, Color.White);

            this.player.Draw(this.spriteBatch, 1.25, 1.25, this.player.PlayerSate);
            this.enemy.Draw(this.spriteBatch, 0.25, 0.25, this.enemy.EnemyState);

            // Manage debug font
            if (this.tildePressed)
            {
                this.DebugInformation();
            }

            this.spriteBatch.End();
        }

        private void UpdateGameOver(GameTime gameTime)
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

        private void DrawGameOver(GameTime gameTime)
        {
            // Draw text and scores
            // Draw menu for restarting level or going back to main menu
        }

        #endregion

        #region Loading        

        // Load textures and objects for level one
        private void LoadLevelOne()
        {
            this.background = new StaticItem(Vector2.Zero);
            this.background.SpriteTexture = Content.Load<Texture2D>("background2");

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

        // This will be removed soon
        private void MoveTree(GameTime gameTime)
        {
            // Testing moving sprites
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
