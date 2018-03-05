namespace MonoContra.Levels
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using MonoContra.Enumerables;
    using MonoContra.Objects;

    public class StateMachine
    {
        private bool loadOnce;        
        
        private SpriteBatch spriteBatch;
        private SpriteFont gameFont;
        
        private Intro intro;
        private MainMenu mainMenu;
        private Level1 levelOne;
        
        private KeyboardState oldKeyState;
        private MouseState oldMouseState;

        public StateMachine()
        {
            this.Initialize();
        }

        public GameState GameState { get; set; }

        public void Initialize()
        {
            this.GameState = GameState.Intro;
            this.loadOnce = true;
        }

        public void LoadContent(ContentManager content, GraphicsDevice graphicsDevice)
        {
            this.spriteBatch = new SpriteBatch(graphicsDevice);
            this.gameFont = content.Load<SpriteFont>("Debug");

            // Intro State
            this.intro = new Intro(content);

            // Menu State
            this.mainMenu = new MainMenu(content);

            // GameStart State        
            this.levelOne = new Level1(content, graphicsDevice);
        }

        public void Update(GameTime gameTime, ContentManager content, GraphicsDevice graphicsDevice)
        {
            KeyboardState keyState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();

            if (this.oldKeyState.IsKeyDown(Keys.Escape) && keyState.IsKeyUp(Keys.Escape))
            {
                Run.Game.Exit();
            }

            switch (this.GameState)
            {
                case GameState.Intro:
                    this.UpdateIntro(gameTime, keyState, mouseState);
                    break;
                case GameState.MainMenu:
                    this.UpdateMainMenu(gameTime, keyState, mouseState);
                    break;
                case GameState.GameStart:
                    this.UpdateGameStart(gameTime, keyState, mouseState, content, graphicsDevice);
                    break;
                case GameState.PAUSE:
                    if (this.oldKeyState.IsKeyDown(Keys.P) && keyState.IsKeyUp(Keys.P))
                    {
                        GameState = GameState.GameStart;
                        this.levelOne.GamePause = false;
                    }

                    this.oldKeyState = keyState;
                    break;
                case GameState.GameOver:
                    this.UpdateGameOver(keyState, mouseState);
                    break;
            }

            this.oldMouseState = mouseState;
            this.oldKeyState = keyState;
        }

        public void Draw(GameTime gameTime, ContentManager content, GraphicsDeviceManager graphics, int gameWidth, int gameHeight)
        {
            switch (this.GameState)
            {
                case GameState.Intro:
                    this.DrawIntroScreen(graphics, gameWidth, gameHeight);
                    break;
                case GameState.MainMenu:
                    this.DrawMainMenu(gameTime, graphics);
                    break;
                case GameState.GameStart:
                    this.DrawGameStart(gameTime, content);
                    break;
                case GameState.GameOver:
                    this.DrawGameOver(gameTime, graphics, content);
                    break;
            }
        }

        #region Update logic and states

        private void UpdateIntro(GameTime gameTime, KeyboardState keyState, MouseState mouseState)
        {
            if (keyState.IsKeyUp(Keys.Enter) && this.oldKeyState.IsKeyDown(Keys.Enter))
            {
                GameState = GameState.MainMenu;
            }

            if (mouseState.LeftButton == ButtonState.Pressed && this.oldMouseState.LeftButton == ButtonState.Released)
            {
                GameState = GameState.MainMenu;
            }

            this.loadOnce = true;

            this.oldMouseState = mouseState;
            this.oldKeyState = keyState;
        }

        private void DrawIntroScreen(GraphicsDeviceManager graphics, int gameWidth, int gameHeight)
        {
            if (this.loadOnce)
            {
                this.loadOnce = false;
            }

            graphics.GraphicsDevice.Clear(Color.White);
            this.intro.Draw(this.spriteBatch, gameWidth, gameHeight);
        }

        private void UpdateMainMenu(GameTime gameTime, KeyboardState keyState, MouseState mouseState)
        {
            // Respond to user input for menu selections, etc
            if (keyState.IsKeyUp(Keys.Enter) && this.oldKeyState.IsKeyDown(Keys.Enter))
            {
                this.GameState = GameState.GameStart;
            }

            if (mouseState.LeftButton == ButtonState.Pressed && this.oldMouseState.LeftButton == ButtonState.Released)
            {
                this.GameState = GameState.GameStart;
            }

            this.loadOnce = true;
            this.oldMouseState = mouseState;
            this.oldKeyState = keyState;
        }

        private void DrawMainMenu(GameTime gameTime, GraphicsDeviceManager graphics)
        {
            // Draw the main menu, any active selections, etc
            if (this.loadOnce)
            {
                this.loadOnce = false;
            }

            graphics.GraphicsDevice.Clear(Color.DarkRed);
            this.mainMenu.Draw(this.spriteBatch, this.gameFont);
        }

        private void UpdateGameStart(GameTime gameTime, KeyboardState keyState, MouseState mouseState, ContentManager content, GraphicsDevice graphicsDevice)
        {
            this.levelOne.Update(gameTime, keyState, mouseState, this.spriteBatch, this.GameState, this.gameFont);
            if (!this.levelOne.IsPlayerAlive())
            {
                if (this.levelOne.NumberOfLives() <= 0)
                {
                    this.levelOne = new Level1(content, graphicsDevice);
                    this.GameState = GameState.GameOver;
                }
                else
                {
                    // lower lives ???
                    this.levelOne = new Level1(content, graphicsDevice);                    
                    this.GameState = GameState.GameOver;
                }
            }

            if (this.levelOne.Pause())
            {
                this.GameState = GameState.PAUSE;
            }
        }

        private void DrawGameStart(GameTime gameTime, ContentManager content)
        {
            this.levelOne.Draw(gameTime, this.spriteBatch, content);
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
            if (keyState.IsKeyUp(Keys.Enter) && this.oldKeyState.IsKeyDown(Keys.Enter))
            {
                this.GameState = GameState.GameStart;
            }

            if (mouseState.LeftButton == ButtonState.Pressed && this.oldMouseState.LeftButton == ButtonState.Released)
            {
                this.GameState = GameState.GameStart;
            }

            this.loadOnce = true;
            this.oldKeyState = keyState;
        }

        private void DrawGameOver(GameTime gameTime, GraphicsDeviceManager graphics, ContentManager content)
        {
            StaticItem gameOverScreen = new StaticItem(Vector2.Zero);
            gameOverScreen.SpriteTexture = content.Load<Texture2D>("gameover");

            // Draw text and scores
            // Draw menu for restarting level or going back to main menu
            graphics.GraphicsDevice.Clear(Color.Black);
            this.spriteBatch.Begin();
            this.spriteBatch.Draw(gameOverScreen.SpriteTexture, new Vector2(280,300), Color.White);
            this.spriteBatch.DrawString(
                this.gameFont,
                "\n You are dead! " +
                "\n Press Enter to restart.",
                new Vector2(500, 250), // this.player.SpritePosition.X, this.player.SpritePosition.Y),
                Color.DarkSeaGreen);
            this.spriteBatch.End();
        }

        #endregion  
    }
}
