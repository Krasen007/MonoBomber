namespace MonoBomber.Objects
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using MonoBomber.Enumerables;
    using MonoBomber.States;
    using MonoBomber.Units;
    using MonoBomber.Utils;

    public class Level1
    {
        private const int MAP_WIDTH = 2560;
        private const int MAP_HEIGHT = 1440;
        private float timeSinceLastShot = 0f;

        private SpriteFont gameFont;
        private KeyboardState oldKeyState;
        private bool tildePressed = false;
        private bool loadOnce = true;

        private StaticItem background;
        private Player player;
        private Enemy enemy;
        private Map map;
        private Camera camera;
        private Door exitDoor;
        private Key key;
        private Bomb bomb;

        private List<Explosion> explosions = new List<Explosion>();

        private SoundManager soundManager;

        private DrawScore score;
        private double gameScore = 5000;

        private PowerUpMoreBombs moreBombs;

        private List<BalloonEnemy> balloonEnemys = new List<BalloonEnemy>();

        public Level1(ContentManager content, GraphicsDevice viewport)
        {
            this.map = new Map(content, 35, 35);
            this.camera = new Camera(viewport.Viewport);
            this.gameFont = content.Load<SpriteFont>("Debug");
            this.soundManager = new SoundManager(content);
        }

        public bool GamePause { get; set; }

        public void Update(GameTime gameTime, KeyboardState keyState, MouseState mouseState, SpriteBatch spriteBatch, GameState gameState, SpriteFont gameFont)
        {
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
                if (!this.GamePause)
                {
                    this.GamePause = true;
                }
            }

            this.oldKeyState = keyState;

            if (this.oldKeyState.IsKeyDown(Keys.Space))
            {
                this.bomb.SpritePosition = new Vector2(this.player.SpritePosition.X + 15, this.player.SpritePosition.Y + 10);
                this.timeSinceLastShot = 0;
                this.bomb.Health = true;
            }

            this.oldKeyState = keyState;

            if (this.bomb.Health == true)
            {
                this.timeSinceLastShot += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (this.timeSinceLastShot <= 5)
                {
                    this.bomb.UpdateAnimation();
                }
            }

            this.player.Update(keyState, mouseState, this.key, spriteBatch, this.map.Walls);
            if (this.score.GameScore <= 0)
            {

                this.player.IsAlive = false;
            }

            this.enemy.Update(this.player);
            this.exitDoor.Update(this.player);
            this.key.Update(this.player);

            this.moreBombs.Update(this.player);

            foreach (BalloonEnemy balloonEnemy in this.balloonEnemys)
            {
                balloonEnemy.Update(spriteBatch, this.map.Walls, gameTime, this.player);
            }

            if (this.timeSinceLastShot > 4 && this.timeSinceLastShot < 6)
            {
                foreach (Explosion explosion in explosions)
                {
                    explosion.Update(gameTime);
                }
            }

            this.map.Update(gameTime);
            this.camera.Update(this.player.SpritePosition, MAP_WIDTH, MAP_HEIGHT);

            if ((int)gameTime.TotalGameTime.Seconds % 2 == 0 && this.bomb.Health == true)
            {
                foreach (Explosion explosion in explosions)
                {
                    explosion.Health = false;
                }
            }
            this.PlayMusic();
        }

        public bool IsPlayerAlive()
        {
            return this.player.IsAlive;
        }

        public bool Pause()
        {
            return this.GamePause;
        }

        public int NumberOfLives()
        {
            return this.player.NumberOfLives;
        }

        public bool IsLevelCompleted()
        {
            return this.exitDoor.LevelComplete;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, ContentManager content)
        {
            if (this.loadOnce)
            {
                this.score = new DrawScore(spriteBatch, this.gameFont, this.gameScore);
                this.LoadLevelOne(content);
                this.loadOnce = false;
            }

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, this.camera.Transform);

            this.background.Draw(spriteBatch);

            this.exitDoor.Draw(spriteBatch, 0.15, 0.15);
            this.key.Draw(spriteBatch, 1, 1);
            this.map.Draw(spriteBatch);

            if (this.tildePressed)
            {
                this.DebugInformation(spriteBatch);
            }

            if (this.bomb.Health == true && this.timeSinceLastShot <= 5)
            {
                this.bomb.Draw(spriteBatch, 0.55, 0.55);
            }

            if (this.timeSinceLastShot > 4)
            {
                Texture2D explosionAnimation = content.Load<Texture2D>("explosion");
                explosions.Add(new Explosion(content, new Vector2(this.bomb.SpritePosition.X + 30, this.bomb.SpritePosition.Y + 30), true, 8, new Vector2(1f, 1f)));
                int bombAnimationWidth = 70;
                for (int i = 1; i <= 2; i++)
                {
                    explosions.Add(new Explosion(content, new Vector2(this.bomb.SpritePosition.X - bombAnimationWidth * i + 30, this.bomb.SpritePosition.Y + 30) , true, 8, new Vector2(1f, 1f)));
                    explosions.Add(new Explosion(content, new Vector2(this.bomb.SpritePosition.X + bombAnimationWidth * i + 30, this.bomb.SpritePosition.Y + 30), true, 8, new Vector2(1f, 1f)));
                    
                    explosions.Add(new Explosion(content, new Vector2(this.bomb.SpritePosition.X + 30, this.bomb.SpritePosition.Y - bombAnimationWidth * i + 30), true, 8, new Vector2(1f, 1f)));
                    explosions.Add(new Explosion(content, new Vector2(this.bomb.SpritePosition.X + 30, this.bomb.SpritePosition.Y + bombAnimationWidth * i + 30), true, 8, new Vector2(1f, 1f)));
                }
                foreach (Explosion explosion in explosions)
                {
                    explosion.Draw(spriteBatch);
                }
            }

            if (this.timeSinceLastShot > 4 && this.timeSinceLastShot < 6)
            {
                foreach (Explosion explosion in explosions)
                {
                    explosion.Draw(spriteBatch);
                }
            }

            this.player.Draw(spriteBatch, 0.90, 0.90);
            this.enemy.Draw(spriteBatch, 0.13, 0.13);

            this.moreBombs.Draw(spriteBatch, 1, 1);

            foreach (BalloonEnemy balloonEnenemy in this.balloonEnemys)
            {
                balloonEnenemy.Draw(spriteBatch, 1.1, 1.1);
            }

            this.score.Draw(gameTime, spriteBatch, this.gameFont, this.camera.CamUITopLeft(this.player.SpritePosition, MAP_HEIGHT));

            spriteBatch.End();
        }

        private void PlayMusic()
        {
            if (this.player.IsAlive && this.exitDoor.LevelComplete == false)
            {
                this.soundManager.MainThemeInstance.IsLooped = true;
                this.soundManager.MainThemeInstance.Play();
            }
            else if (this.player.IsAlive && this.exitDoor.LevelComplete == true)
            {
                this.soundManager.MainThemeInstance.Stop();
                this.soundManager.GameWinInstance.Play();
            }
            else if (!this.player.IsAlive)
            {
                this.soundManager.MainThemeInstance.Stop();
                this.soundManager.GameLoseInstance.Play();
            }
            else if (this.GamePause)
            {
                this.soundManager.MainThemeInstance.Pause();
            }
            else if (!this.GamePause)
            {
                this.soundManager.MainThemeInstance.Resume();
            }
            else
            {
                this.soundManager.MainThemeInstance.Stop();
            }
        }

        private void LoadLevelOne(ContentManager content)
        {
            this.background = new StaticItem(Vector2.Zero);
            this.background.SpriteTexture = content.Load<Texture2D>("background3");

            Texture2D playerMoves = content.Load<Texture2D>("bomberman");
            this.player = new Player(playerMoves, 4, 6, new Vector2(0, 310), new Vector2(10, 0), new Vector2(0, 10));
            this.player.NumberOfLives = 3;

            Texture2D badGirl = content.Load<Texture2D>("mele1");
            this.enemy = new Enemy(badGirl, 3, 3, new Vector2(520, 525), new Vector2(4, 0), new Vector2(0, 0));

            Texture2D doorLocked = content.Load<Texture2D>("doorLocked");
            Texture2D doorOpen = content.Load<Texture2D>("doorOpen");
            this.exitDoor = new Door(doorOpen, 1, 4, new Vector2(250, 245));

            Texture2D keyAnim = content.Load<Texture2D>("key");
            this.key = new Key(keyAnim, 1, 3, new Vector2(666, 390));

            Texture2D bombAnim = content.Load<Texture2D>("bombanimation");
            this.bomb = new Bomb(bombAnim, 1, 5, new Vector2(100, 100));

            Texture2D moreBombsAnim = content.Load<Texture2D>("bombSathel");
            this.moreBombs = new PowerUpMoreBombs(moreBombsAnim, 1, 1, new Vector2(388, 180));
            
            Texture2D balloonEnemyAnim = content.Load<Texture2D>("HeartStripBalloon");
            this.balloonEnemys.Add(new BalloonEnemy(balloonEnemyAnim, 4, 4, new Vector2(300, 182), new Vector2(2, 0), new Vector2(0, 2))); // SpeedX,Y - this number must be bigger than the speed of the sprite for collision to work
            this.balloonEnemys.Add(new BalloonEnemy(balloonEnemyAnim, 4, 4, new Vector2(500, 462), new Vector2(2, 0), new Vector2(0, 2)));
        }

        private void DebugInformation(SpriteBatch spriteBatch)
        {
            MouseState mouseState = Mouse.GetState();
            spriteBatch.DrawString(
                this.gameFont,
                "\n Debug info:" +
                "\n Mouse to vector: " + mouseState.Position.ToVector2() +
                "\n levelcomplete door: " + this.exitDoor.LevelComplete +
                "\n player dest rect: " + this.player.DestinationRectangle +
                "\n player lives: " + this.player.NumberOfLives +
                "\n enemy destination rect: " + this.enemy.DestinationRectangle,
                new Vector2(this.player.SpritePosition.X - 150, this.player.SpritePosition.Y - 150),
                Color.Orange);
        }
    }
}