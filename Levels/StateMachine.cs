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
        // Init
        private bool loadOnce;
        private GameState gameState;

        // Load
        private SpriteBatch spriteBatch;
        private SpriteFont gameFont;

        // GameState Intro
        private Intro intro;

        // GameState MainMenu
        private MainMenu mainMenu;

        // GameState GameStart
        private Level1 levelOne;

        // GameState Pause
        private Pause pause;
        
        // Update
        private KeyboardState oldKeyState;
        private MouseState oldMouseState;

        public StateMachine()
        {
            this.Initialize();
        }

        public void Initialize()
        {
            this.gameState = GameState.Intro;
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

            this.pause = new Pause();
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();

            if (this.oldKeyState.IsKeyDown(Keys.Escape) && keyState.IsKeyUp(Keys.Escape))
            {
                // this.Exit();
            }

            switch (this.gameState)
            {
                case GameState.Intro:
                    this.UpdateIntro(gameTime, keyState, mouseState);
                    break;
                case GameState.MainMenu:
                    this.UpdateMainMenu(gameTime, keyState, mouseState);
                    break;
                case GameState.GameStart:
                    this.levelOne.Update(gameTime, keyState, mouseState, this.spriteBatch, this.gameState, this.gameFont);
                    break;
                case GameState.PAUSE:
                    this.pause.Update(keyState, this.gameState);
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
            switch (this.gameState)
            {
                case GameState.Intro:
                    this.DrawIntroScreen(graphics, gameWidth, gameHeight);
                    break;
                case GameState.MainMenu:
                    this.DrawMainMenu(gameTime, graphics);
                    break;
                case GameState.GameStart:
                    this.levelOne.Draw(gameTime, this.spriteBatch, content);
                    break;
                case GameState.GameOver:
                    this.DrawGameOver(gameTime, graphics);
                    break;
            }
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
            if (this.oldKeyState.IsKeyDown(Keys.Enter) || this.oldKeyState.IsKeyDown(Keys.Space))
            {
                this.gameState = GameState.GameStart;
            }

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                this.gameState = GameState.GameStart;                
            }

            this.loadOnce = true;
            this.oldKeyState = keyState;
        }

        private void DrawGameOver(GameTime gameTime, GraphicsDeviceManager graphics)
        {
            // Draw text and scores
            // Draw menu for restarting level or going back to main menu
            graphics.GraphicsDevice.Clear(Color.Teal);
            this.spriteBatch.Begin();
            this.spriteBatch.DrawString(
                this.gameFont,
                "\n You are dead! " +
                "\n Press Enter to restart.",
                new Vector2(0, 0), // this.player.SpritePosition.X, this.player.SpritePosition.Y),
                Color.DarkBlue);
            this.spriteBatch.End();
        }

        #endregion  
    }
}
