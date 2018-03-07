namespace MonoBomber.Utils
{
    using Microsoft.Xna.Framework;

    public static class CollisionHelper
    {
        public static bool CollideTop(this Rectangle r1, Rectangle r2)
        {
            return r1.Bottom >= r2.Top - 1 &&
                    r1.Bottom <= r2.Top + (r2.Height / 2) &&
                    r1.Right >= r2.Left + (r2.Width / 5) &&
                    r1.Left <= r2.Right - (r2.Width / 5);
        }

        public static bool CollideBottom(this Rectangle r1, Rectangle r2)
        {
            return r1.Top <= r2.Bottom + (r2.Height / 5) &&
                    r1.Top >= r2.Bottom - 1 &&
                    r1.Right >= r2.Left + (r2.Width / 5) &&
                    r1.Left <= r2.Right - (r2.Width / 2);
        }

        public static bool CollideLeft(this Rectangle r1, Rectangle r2)
        {
            return r1.Right <= r2.Right &&
                    r1.Right >= r2.Left - 5 &&
                    r1.Top <= r2.Bottom - (r2.Width / 4) &&
                    r1.Bottom >= r2.Top + (r2.Width / 4);
        }

        public static bool CollideRight(this Rectangle r1, Rectangle r2)
        {
            return r1.Left >= r2.Left &&
                    r1.Left <= r2.Right + 5 &&
                    r1.Top <= r2.Bottom - (r2.Width / 4) &&
                    r1.Bottom >= r2.Top + (r2.Width / 4);
        }
    }
}
