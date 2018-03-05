namespace MonoContra.Objects
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using MonoContra.Enumerables;
    using MonoContra.Levels;
    using MonoContra.Utils;

    public class Level1
    {
        private const int MAP_WIDTH = 2560;
        private const int MAP_HEIGHT = 1440;

        // private GameState gameState;
        private SpriteFont gameFont;
        private KeyboardState oldKeyState;

        // private MouseState oldMouseState;
        private bool tildePressed = false;
        private bool gamePaused = false;
        private bool loadOnce = true;

        private StaticItem background;
        private Player player;
        private Enemy enemy;
        private Map map;
        private Camera camera;
        private Door exitDoor;
        private Key key;
        private Bomb bomb;

        public Level1(ContentManager content, GraphicsDevice viewport)
        {
            this.map = new Map(content, 35, 35);
            this.camera = new Camera(viewport.Viewport);
            this.gameFont = content.Load<SpriteFont>("Debug");
        }        

        public void Update(GameTime gameTime, KeyboardState keyState, MouseState mouseState, SpriteBatch spriteBatch, GameState gameState, SpriteFont gameFont)
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
                if (!this.gamePaused)
                {
                    this.gamePaused = true;
                    ////new Pause();
                    ////gameState = GameState.PAUSE;
                }
                else
                {
                    this.gamePaused = false;
                }
            }

            this.oldKeyState = keyState;

            this.player.Update(keyState, mouseState, this.key, spriteBatch, this.map.Walls);
            if (!this.player.IsAlive)
            {
                this.player.IsAlive = true;
            }

            ////this.player.Bomb.Update(); // does not work
            this.bomb.Update();

            this.enemy.Update(this.player);
            this.exitDoor.Update(this.player);
            this.key.Update(this.player);

            this.map.Update(gameTime);
            this.camera.Update(this.player.SpritePosition, MAP_WIDTH, MAP_HEIGHT);
        }
        public bool IsPlayerAlive()
        {
            return player.IsAlive;
        }        

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, ContentManager content)
        {
            // Draw the background the level
            // Draw enemies
            // Draw the player
            // Draw particle effects, etc
            // Level one
            if (this.loadOnce)
            {
                this.LoadLevelOne(content);
                this.loadOnce = false;
            }

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, this.camera.Transform);

            this.background.Draw(spriteBatch);
            this.map.Draw(spriteBatch);

            // Manage debug font
            if (this.tildePressed)
            {
                this.DebugInformation(spriteBatch);
            }

            if (this.gamePaused)
            {
                this.GamePaused(spriteBatch);
            }
                        
            this.player.Draw(spriteBatch, 0.90, 0.90);
            ////this.player.Bomb.Draw(this.spriteBatch, 0.75, 0.75); // does not work
            this.bomb.Draw(spriteBatch, 0.55, 0.55);

            this.enemy.Draw(spriteBatch, 0.13, 0.13);
            this.exitDoor.Draw(spriteBatch, 0.15, 0.15);
            this.key.Draw(spriteBatch, 1, 1);
            spriteBatch.End();
        }

        private void GamePaused(SpriteBatch spriteBatch)
        {
            // TODO: Add more stuff maybe
            ////this.graphics.GraphicsDevice.Clear(Color.Green);
            ////spriteBatch.Begin();
            spriteBatch.DrawString(
                this.gameFont,
                "\n Game paused! " +
                "\n Press P to resume.",
                new Vector2(this.player.SpritePosition.X, this.player.SpritePosition.Y),
                Color.DarkBlue);
            ////spriteBatch.End();
        }

        private void LoadLevelOne(ContentManager content)
        {
            this.background = new StaticItem(Vector2.Zero);
            this.background.SpriteTexture = content.Load<Texture2D>("background3");

            Texture2D playerMoves = content.Load<Texture2D>("bomberman");
            this.player = new Player(playerMoves, 4, 6, new Vector2(0, 310), new Vector2(10, 0), new Vector2(0, 10));

            Texture2D badGirl = content.Load<Texture2D>("mele1");
            this.enemy = new Enemy(badGirl, 3, 3, new Vector2(520, 525), new Vector2(4, 0), new Vector2(0, 0));

            Texture2D doorLocked = content.Load<Texture2D>("doorLocked");
            Texture2D doorOpen = content.Load<Texture2D>("doorOpen");
            this.exitDoor = new Door(doorOpen, 1, 4, new Vector2(250, 245));

            Texture2D keyAnim = content.Load<Texture2D>("key");
            this.key = new Key(keyAnim, 1, 3, new Vector2(666, 390));

            Texture2D bombAnim = content.Load<Texture2D>("bombanimation");
            this.bomb = new Bomb(bombAnim, 1, 5, new Vector2(387, 530));
        }

        private void DebugInformation(SpriteBatch spriteBatch)
        {
            MouseState mouseState = Mouse.GetState();
            spriteBatch.DrawString(
                this.gameFont,
                "\n Debug info:" +
                "\n Mouse to vector: " + mouseState.Position.ToVector2() +
                "\n player sprite: " + this.player.SpritePosition +
                "\n player dest rect: " + this.player.DestinationRectangle +
                "\n door rect: " + this.exitDoor.DestinationRectangle +
                "\n enemy destination rect: " + this.enemy.DestinationRectangle,
                new Vector2(this.player.SpritePosition.X - 150, this.player.SpritePosition.Y - 150),
                Color.Orange);
        }
    }
}