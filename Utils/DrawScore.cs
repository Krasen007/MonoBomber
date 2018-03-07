namespace MonoBomber.States
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class DrawScore
    {
        private float elapsedTime = 0;

        public DrawScore(SpriteBatch spriteBatch, SpriteFont gameFont, double timeLeft)
        {
            this.GameScore = timeLeft;
        }

        public double GameScore { get; set; }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, SpriteFont gameFont, Vector2 uiPosition)
        {
            this.elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (this.elapsedTime > 1f)
            {
                this.elapsedTime -= 1f;

                this.GameScore--;
            }

            spriteBatch.DrawString(gameFont,"            Score: " +
                this.GameScore, uiPosition, Color.Yellow);
        }
    }
}