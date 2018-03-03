namespace MonoContra.Utils
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    public class Button
    {
        private Dictionary<States, Texture2D> textures;

        public Button(Rectangle rectangle, Texture2D noneTexture, Texture2D hoverTexture, Texture2D pressedTexture)
        {
            this.DestinationRectangle = rectangle;
            this.textures = new Dictionary<States, Texture2D>
            {
                { States.None, noneTexture },
                { States.Hover, hoverTexture },
                { States.Pressed, pressedTexture }
            };
        }

        public enum States
        {
            None,
            Pressed,
            Hover,
            Released
        }

        public Rectangle DestinationRectangle { get; set; }

        public States State { get; set; }

        public void Update(MouseState mouseState)
        {
            if (this.DestinationRectangle.Contains(mouseState.X, mouseState.Y))
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    this.State = States.Pressed;
                }
                else
                {
                    this.State = this.State == States.Pressed ? States.Released : States.Hover;
                }
            }
            else
            {
                this.State = States.None;
            }
        }

        // Make sure Begin is called on s before you call this function
        public void Draw(SpriteBatch s)
        {
            // TODO: make it work
            // s.Draw(_textures[State], _rectangle);
        }
    }
}