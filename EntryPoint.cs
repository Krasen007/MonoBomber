namespace MonoContra
{
    using System;
    using Enumerables;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
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

        // State Machine
        private StateMachine stateMachine;

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

            base.Initialize();
        }

        protected override void LoadContent()
        {
            this.stateMachine.LoadContent(this.Content, this.GraphicsDevice);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            this.stateMachine.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            this.stateMachine.Draw(gameTime, this.Content, this.graphics, GAME_WIDTH, GAME_HEIGHT);

            base.Draw(gameTime);
        }              
    }
}
