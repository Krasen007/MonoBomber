namespace MonoBomber.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Camera
    {
        private Matrix transform;
        private Vector2 center;
        private Viewport viewport;

        public Camera(Viewport viewport)
        {
            this.viewport = viewport;
        }

        public Matrix Transform
        {
            get { return this.transform; }
        }

        public void Update(Vector2 position, int xOffset, int yOffset)
        {
            if (position.X < this.viewport.Width / 2)
            {
                this.center.X = this.viewport.Width / 2;
            }
            else if (position.X > xOffset - (this.viewport.Width / 2))
            {
                this.center.X = xOffset - (this.viewport.Width / 2);
            }
            else
            {
                this.center.X = position.X;
            }

            if (position.Y < this.viewport.Height / 2)
            {
                this.center.Y = this.viewport.Height / 2;
            }
            else if (position.Y > yOffset - (this.viewport.Height / 2))
            {
                this.center.Y = yOffset - (this.viewport.Height / 2);
            }
            else
            {
                this.center.Y = position.Y;
            }

            this.transform = Matrix.CreateTranslation(new Vector3(-this.center.X + (this.viewport.Width / 2), -this.center.Y + (this.viewport.Height / 2), 0));
        }
    }
}
