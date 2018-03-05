namespace MonoContra
{
    using System;
    using Enumerables;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using MonoContra.Utils;
    using Objects;

    public class EntryPoint : Game
    {
        private const int GAME_WIDTH = 1280;
        private const int GAME_HEIGHT = 720;

        private const int MAP_WIDTH = 2560;
        private const int MAP_HEIGHT = 1440;

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private KeyboardState oldKeyState;
        private MouseState oldMouseState;
        private SpriteFont gameFont;
        private bool tildePressed;
        private bool loadOnce;
        private GameState gameState;

        // GameState Intro
        private Intro intro;

        // GameState MainMenu
        private MainMenu mainMenu;

        // GameState GameStart

        private Level1 levelOne;

        //private StaticItem background;
        //private Player player;
        //private Enemy enemy;
        //private Map map;
        //private Camera camera;
        //private Door exitDoor;
        //private Key key;
        //private Bomb bomb;

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

        protected override void Initialize()
        {
            this.gameState = GameState.Intro;
            this.loadOnce = true;
            //this.map = new Map(Content, 35, 35);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Intro State
            this.intro = new Intro(Content);

            // Menu State
            this.mainMenu = new MainMenu(Content);

            // GameStart State
            this.spriteBatch = new SpriteBatch(GraphicsDevice);
            this.gameFont = this.Content.Load<SpriteFont>("Debug");
            //this.camera = new Camera(GraphicsDevice.Viewport);

            //gs
            this.levelOne = new Level1(Content, GraphicsDevice);
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
                    this.UpdateIntro(gameTime, keyState, mouseState);
                    break;
                case GameState.MainMenu:
                    this.UpdateMainMenu(gameTime, keyState, mouseState);
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
                    this.UpdateGameOver(keyState, mouseState);
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            switch (this.gameState)
            {
                case GameState.Intro:
                    this.DrawIntroScreen();
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

            base.Draw(gameTime);
        }
        #region Update logic and states

        private void UpdateIntro(GameTime gameTime, KeyboardState keyState, MouseState mouseState)
        {
            if (keyState.IsKeyUp(Keys.Enter) && this.oldKeyState.IsKeyDown(Keys.Enter))
            {
                this.gameState = GameState.MainMenu;
            }

            if (mouseState.LeftButton == ButtonState.Pressed && this.oldMouseState.LeftButton == ButtonState.Released)
            {
                this.gameState = GameState.MainMenu;
            }

            this.loadOnce = true;

            this.oldMouseState = mouseState;
            this.oldKeyState = keyState;
        }

        private void DrawIntroScreen()
        {
            if (this.loadOnce)
            {
                this.loadOnce = false;
            }

            this.graphics.GraphicsDevice.Clear(Color.White);
            this.intro.Draw(this.spriteBatch, GAME_WIDTH, GAME_HEIGHT);
        }

        private void UpdateMainMenu(GameTime gameTime, KeyboardState keyState, MouseState mouseState)
        {
            // Respond to user input for menu selections, etc
            if (keyState.IsKeyUp(Keys.Enter) && this.oldKeyState.IsKeyDown(Keys.Enter))
            {
                this.gameState = GameState.GameStart;
            }

            if (mouseState.LeftButton == ButtonState.Pressed && this.oldMouseState.LeftButton == ButtonState.Released)
            {
                this.gameState = GameState.GameStart;
            }

            this.loadOnce = true;
            this.oldMouseState = mouseState;
            this.oldKeyState = keyState;
        }

        private void DrawMainMenu(GameTime gameTime)
        {
            // Draw the main menu, any active selections, etc
            if (this.loadOnce)
            {
                this.loadOnce = false;
            }

            this.graphics.GraphicsDevice.Clear(Color.DarkRed);
            this.mainMenu.Draw(this.spriteBatch, this.gameFont);
        }

        private void UpdateGameStart(GameTime gameTime, KeyboardState keyState, MouseState mouseState)
        {
            this.levelOne.Update(gameTime, keyState, mouseState, spriteBatch, this.gameState);

            //// Respond to user actions in the game.
            //// Update enemies
            //// Handle collisions
            //if (this.oldKeyState.IsKeyDown(Keys.OemTilde) && keyState.IsKeyUp(Keys.OemTilde))
            //{
            //    if (!this.tildePressed)
            //    {
            //        this.tildePressed = true;
            //    }
            //    else
            //    {
            //        this.tildePressed = false;
            //    }
            //}

            //if (this.oldKeyState.IsKeyDown(Keys.P) && keyState.IsKeyUp(Keys.P))
            //{
            //    // TODO: Add more stuff maybe
            //    ////this.graphics.GraphicsDevice.Clear(Color.Green);
            //    this.gameState = GameState.PAUSE;
            //    this.spriteBatch.Begin();
            //    this.spriteBatch.DrawString(
            //        this.gameFont,
            //        "\n Game paused! " +
            //        "\n Press P to resume.",
            //        new Vector2(this.player.SpritePosition.X, this.player.SpritePosition.Y),
            //        Color.DarkBlue);
            //    this.spriteBatch.End();
            //}

            //this.oldKeyState = keyState;

            //this.player.Update(keyState, mouseState, this.key, this.spriteBatch, this.map.Walls);
            //if (!this.player.IsAlive)
            //{
            //    this.gameState = GameState.GameOver;
            //    this.player.IsAlive = true;
            //}

            //////this.player.Bomb.Update(); // does not work
            //this.bomb.Update();

            //this.enemy.Update(this.player);
            //this.exitDoor.Update(this.player);
            //this.key.Update(this.player);

            //this.map.Update(gameTime);
            //this.camera.Update(this.player.SpritePosition, MAP_WIDTH, MAP_HEIGHT);

            //// if (true)//playerDied)
            ////     _state = GameState.EndOfGame;

            //// When game over contition is met
            //// this.loadOnce = true;
        }

        private void DrawGameStart(GameTime gameTime)
        {
            this.levelOne.Draw(gameTime, spriteBatch, Content);
            //// Draw the background the level
            //// Draw enemies
            //// Draw the player
            //// Draw particle effects, etc
            //// Level one
            //if (this.loadOnce)
            //{
            //    this.LoadLevelOne();
            //    this.loadOnce = false;
            //}

            //this.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, this.camera.Transform);

            //this.background.Draw(this.spriteBatch);
            //this.map.Draw(this.spriteBatch);

            //// Manage debug font
            //if (this.tildePressed)
            //{
            //    this.DebugInformation();
            //}

            //this.player.Draw(this.spriteBatch, 0.90, 0.90);
            //////this.player.Bomb.Draw(this.spriteBatch, 0.75, 0.75); // does not work
            //this.bomb.Draw(this.spriteBatch, 0.55, 0.55);

            //this.enemy.Draw(this.spriteBatch, 0.13, 0.13);
            //this.exitDoor.Draw(this.spriteBatch, 0.15, 0.15);
            //this.key.Draw(this.spriteBatch, 1, 1);
            //this.spriteBatch.End();
        }

        private void UpdateGameOver(KeyboardState keyState, MouseState mouseState)
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

            // Respond to user input for menu selections, etc
            if (this.oldKeyState.IsKeyDown(Keys.Enter) || this.oldKeyState.IsKeyDown(Keys.Space))
            {
                this.gameState = GameState.GameStart;
                this.loadOnce = true;
            }

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                this.gameState = GameState.GameStart;
                this.loadOnce = true;
            }

            this.oldKeyState = keyState;
        }

        private void DrawGameOver(GameTime gameTime)
        {
            // Draw text and scores
            // Draw menu for restarting level or going back to main menu
            this.graphics.GraphicsDevice.Clear(Color.Teal);
            this.spriteBatch.Begin();
            this.spriteBatch.DrawString(
                this.gameFont,
                "\n You are dead! " +
                "\n Press Enter to restart.",
                new Vector2(0, 0), //this.player.SpritePosition.X, this.player.SpritePosition.Y),
                Color.DarkBlue);
            this.spriteBatch.End();
        }

        #endregion

        #region Loading        

        // Load textures and objects for level one
        //private void LoadLevelOne()
        //{
        //    this.background = new StaticItem(Vector2.Zero);
        //    this.background.SpriteTexture = Content.Load<Texture2D>("background3");

        //    Texture2D playerMoves = Content.Load<Texture2D>("bomberman");
        //    this.player = new Player(playerMoves, 4, 6, new Vector2(0, 310), new Vector2(10, 0), new Vector2(0, 10));

        //    Texture2D badGirl = Content.Load<Texture2D>("mele1");
        //    this.enemy = new Enemy(badGirl, 3, 3, new Vector2(520, 525), new Vector2(4, 0), new Vector2(0, 0));

        //    Texture2D doorLocked = Content.Load<Texture2D>("doorLocked");
        //    Texture2D doorOpen = Content.Load<Texture2D>("doorOpen");
        //    this.exitDoor = new Door(doorOpen, 1, 4, new Vector2(250, 245));

        //    Texture2D keyAnim = Content.Load<Texture2D>("key");
        //    this.key = new Key(keyAnim, 1, 3, new Vector2(666, 390));

        //    Texture2D bombAnim = Content.Load<Texture2D>("bombanimation");
        //    this.bomb = new Bomb(bombAnim, 1, 5, new Vector2(387, 530));
        //}

        #endregion

        // Replace Console WriteLine
        //private void DebugInformation()
        //{
        //    MouseState mouseState = Mouse.GetState();
        //    this.spriteBatch.DrawString(
        //        this.gameFont,
        //        "\n Debug info:" +
        //        "\n Mouse to vector: " + mouseState.Position.ToVector2() +
        //        "\n player sprite: " + this.player.SpritePosition +
        //        "\n player dest rect: " + this.player.DestinationRectangle +
        //        "\n door rect: " + this.exitDoor.DestinationRectangle +
        //        "\n enemy destination rect: " + this.enemy.DestinationRectangle,
        //        new Vector2(this.player.SpritePosition.X - 150, this.player.SpritePosition.Y - 150),
        //        Color.Orange);
        //}

    }
}
