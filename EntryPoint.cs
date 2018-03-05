namespace MonoContra
{
    using System;
    using Enumerables;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using MonoContra.Levels;
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
        //private bool tildePressed;
        private bool loadOnce;
        private GameState gameState;



        // State Machine
        private StateMachine stateMachine;

        // GameState Intro
        private Intro intro;

        // GameState MainMenu
        private MainMenu mainMenu;

        // GameState GameStart
        private Level1 levelOne;

        // GameState Pause
        private Pause pause;

        public EntryPoint()
        {
            this.Window.Title = "MonoBomber";
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
            this.stateMachine = new StateMachine();

            this.gameState = GameState.Intro;
            this.loadOnce = true;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(GraphicsDevice);
            this.gameFont = this.Content.Load<SpriteFont>("Debug");

            // Intro State
            this.intro = new Intro(Content);

            // Menu State
            this.mainMenu = new MainMenu(Content);

            // GameStart State        
            this.levelOne = new Level1(Content, GraphicsDevice);

            this.pause = new Pause();
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
                    this.pause.Update(keyState, gameState);
                    break;
                case GameState.GameOver:
                    this.UpdateGameOver(keyState, mouseState);
                    break;
            }

            this.oldMouseState = mouseState;
            this.oldKeyState = keyState;

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
                    this.levelOne.Draw(gameTime, this.spriteBatch, this.Content);
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
    }
}
