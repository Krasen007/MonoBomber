namespace MonoContra.Objects
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using MonoContra.Objects;
    using MonoContra.Enumarables;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework.Content;

    class Map
    {
        protected List<Wall> walls = new List<Wall>();
        private int WALL_WIDTH = 70;
        private int mapWidth, mapHeight;
        private int blocksInRow, blocksInColumn; // колко квадрата да има на ред/колона
        public int MapWidth
        {
            get { return mapWidth; }
        }

        public int MapHeight
        {
            get { return mapHeight; }
        }
        
        public int BlocksInRow { get;  }

        public int BlocksInColumn { get; }

        public Map(ContentManager Content, int blocksInRow, int blocksInColumn)
        {
            this.blocksInRow = blocksInRow;
            this.blocksInColumn = blocksInColumn;
            this.mapWidth = WALL_WIDTH * blocksInRow;
            this.mapHeight = WALL_WIDTH * blocksInColumn;
            Generate(Content);
        }

        public void Generate(ContentManager Content)
        {
            for(int y = 0; y < this.blocksInColumn; y+=2)
            {
                int yCoord = y * WALL_WIDTH;
                for (int x = 1; x <= this.blocksInRow ; x+=2)
                {
                    walls.Add(new Wall(Content, new Vector2(x * WALL_WIDTH, yCoord), true, WallTypes.Unbreakable, this.mapWidth, this.mapHeight, 8, new Vector2(1f, 1f)));
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (var wall in walls)
            {
                wall.Update(gameTime, 0, 0);
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(var wall in walls)
            {
                wall.Draw(spriteBatch);
            }
        }
    }
}
