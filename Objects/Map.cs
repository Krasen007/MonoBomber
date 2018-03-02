namespace MonoContra.Objects
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using MonoContra.Enumerables;

    public class Map
    {
        private const int WALL_WIDTH = 70;
        private List<Wall> walls = new List<Wall>();
        private int mapWidth, mapHeight;
        private int blocksInRow, blocksInColumn; // колко квадрата да има на ред/колона

        public Map(ContentManager content, int blocksInRow, int blocksInColumn)
        {
            this.blocksInRow = blocksInRow;
            this.blocksInColumn = blocksInColumn;
            this.mapWidth = WALL_WIDTH * blocksInRow;
            this.mapHeight = WALL_WIDTH * blocksInColumn;
            this.Generate(content);
        }

        public int MapWidth
        {
            get { return this.mapWidth; }
        }

        public int MapHeight
        {
            get { return this.mapHeight; }
        }
        
        public int BlocksInRow { get; }

        public int BlocksInColumn { get; }              

        public void Generate(ContentManager content)
        {
            for (int y = 0; y < this.blocksInColumn; y += 2)
            {
                int yCoord = y * WALL_WIDTH;
                for (int x = 1; x <= this.blocksInRow; x += 2)
                {
                    this.walls.Add(new Wall(content, new Vector2(x * WALL_WIDTH, yCoord), true, WallTypes.Unbreakable, this.mapWidth, this.mapHeight, 8, new Vector2(1f, 1f)));
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (var wall in this.walls)
            {
                wall.Update(gameTime, 0, 0);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var wall in this.walls)
            {
                wall.Draw(spriteBatch);
            }
        }
    }
}
