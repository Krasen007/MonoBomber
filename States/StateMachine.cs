namespace MonoBomber.Levels
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using MonoBomber.Enumerables;
    using MonoBomber.Objects;
    using MonoBomber.States;

    public class StateMachine
    {
        private bool loadOnce;

        private SpriteBatch spriteBatch;
        private SpriteFont gameFont;

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
                    this.UpdatePause(keyState);
                    break;
                case GameState.GameOver:
                    this.UpdateGameOver(keyState, mouseState);
                    break;
                case GameState.GameWin:
                    this.UpdateGameWin(keyState, mouseState, content, graphicsDevice);
                    break;
            }

            this.oldMouseState = mouseState;
            this.oldKeyState = keyState;
        }

        public void Draw(GameTime gameTime, ContentManager content, GraphicsDeviceManager graphics, GraphicsDevice graphicsDevice, int gameWidth, int gameHeight)
        {
            switch (this.GameState)
            {
                case GameState.Intro:
                    this.DrawIntroScreen(content, graphics, gameWidth, gameHeight);
                    break;
                case GameState.MainMenu:
                    this.DrawMainMenu(content, gameTime, graphics);
                    break;
                case GameState.GameStart:
                    this.DrawGameStart(content, gameTime, graphicsDevice);
                    break;
                case GameState.PAUSE:
                    this.DrawPause();
                    break;                    
                case GameState.GameOver:
                    this.DrawGameOver(graphics, this.spriteBatch, this.gameFont, content);
                    break;
                case GameState.GameWin:
                    this.DrawGameWin(gameTime, graphics, content);
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

        private void DrawIntroScreen(ContentManager content, GraphicsDeviceManager graphics, int gameWidth, int gameHeight)
        {
            if (this.loadOnce)
            {
                Intro intro = new Intro(content, this.spriteBatch, graphics);
                this.loadOnce = false;
            }
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

        private void DrawMainMenu(ContentManager content, GameTime gameTime, GraphicsDeviceManager graphics)
        {
            // Draw the main menu, any active selections, etc
            if (this.loadOnce)
            {
                MainMenu mainMenu = new MainMenu(content, this.spriteBatch, this.gameFont, graphics);
                this.loadOnce = false;
            }
        }

        private void UpdateGameStart(GameTime gameTime, KeyboardState keyState, MouseState mouseState, ContentManager content, GraphicsDevice graphicsDevice)
        {
            this.levelOne.Update(gameTime, keyState, mouseState, this.spriteBatch, this.GameState, this.gameFont);

            if (this.levelOne.IsPlayerAlive() && this.levelOne.IsLevelCompleted())
            {
                this.levelOne = new Level1(content, graphicsDevice);
                this.GameState = GameState.GameWin;
            }
            else if (!this.levelOne.IsPlayerAlive())
            {
                if (this.levelOne.NumberOfLives() <= 0)
                {
                    // Start new game
                    this.levelOne = new Level1(content, graphicsDevice);
                    this.GameState = GameState.GameOver;
                }
                else
                {
                    // TODO: Lower lives and continue
                    this.levelOne = new Level1(content, graphicsDevice);
                    this.GameState = GameState.GameOver;
                }
            }

            if (this.levelOne.Pause())
            {
                this.GameState = GameState.PAUSE;
            }
        }

        private void DrawGameStart(ContentManager content, GameTime gameTime, GraphicsDevice graphicsDevice)
        {
            if (this.loadOnce)
            {
                this.levelOne = new Level1(content, graphicsDevice);
                this.loadOnce = false;
            }

            this.levelOne.Draw(gameTime, this.spriteBatch, content);
        }

        private void UpdatePause(KeyboardState keyState)
        {
            if (this.oldKeyState.IsKeyDown(Keys.P) && keyState.IsKeyUp(Keys.P))
            {
                GameState = GameState.GameStart;
                this.levelOne.GamePause = false;
            }

            this.oldKeyState = keyState;
        }

        private void DrawPause()
        {
            Pause pause = new Pause(this.spriteBatch, this.gameFont);         
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

        private void DrawGameOver(GraphicsDeviceManager graphics, SpriteBatch spriteBatch, SpriteFont gameFont, ContentManager content)
        {
            GameOver gameOver = new GameOver(spriteBatch, gameFont, content, graphics);
        }

        private void UpdateGameWin(KeyboardState keyState, MouseState mouseState, ContentManager content, GraphicsDevice graphicsDevice)
        {
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

        private void DrawGameWin(GameTime gameTime, GraphicsDeviceManager graphics, ContentManager content)
        {
            GameWin gameWin = new GameWin(this.spriteBatch, this.gameFont, content, graphics);
        }

        #endregion  
    }
}
