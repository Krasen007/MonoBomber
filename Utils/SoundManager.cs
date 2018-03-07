namespace MonoBomber.Utils
{
    using Microsoft.Xna.Framework.Audio;
    using Microsoft.Xna.Framework.Content;

    public class SoundManager
    {
        private SoundEffect mainTheme;
        private SoundEffect gameWin;
        private SoundEffect gameLose;

        public SoundManager(ContentManager content)
        {
            this.LoadContent(content);
            this.MainThemeInstance = this.mainTheme.CreateInstance();
            this.GameWinInstance = this.gameWin.CreateInstance();
            this.GameLoseInstance = this.gameLose.CreateInstance();
        }

        public SoundEffectInstance MainThemeInstance { get; set; }

        public SoundEffectInstance GameWinInstance { get; set; }

        public SoundEffectInstance GameLoseInstance { get; set; }

        private void LoadContent(ContentManager content)
        {
            this.mainTheme = content.Load<SoundEffect>("Sounds/mainTheme");
            this.gameWin = content.Load<SoundEffect>("Sounds/gamewin");
            this.gameLose = content.Load<SoundEffect>("Sounds/gamelose");
        }
    }
}
