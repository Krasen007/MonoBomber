namespace MonoBomber.Objects
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using MonoBomber.Enumerables;

    public class Map
    {
        private const int WALL_WIDTH = 70;

        private List<Wall> walls = new List<Wall>();
        private int mapWidth, mapHeight;
        private int blocksInRow, blocksInColumn;
        private Random rng = new Random();
        private int chanceToSpawnWall;

        public Map(ContentManager content, int blocksInRow, int blocksInColumn)
        {
            this.blocksInRow = blocksInRow;
            this.blocksInColumn = blocksInColumn;
            this.mapWidth = WALL_WIDTH * blocksInRow;
            this.mapHeight = WALL_WIDTH * blocksInColumn;
            this.GenerateUnbreakableWalls(content);
            this.GenerateRandomBreakableWalls(content);
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

        public List<Wall> Walls { get => this.walls; set => this.walls = value; }

        public void GenerateUnbreakableWalls(ContentManager content)
        {
            for (int y = 0; y < this.blocksInColumn; y += 2)
            {
                int yCoord = y * WALL_WIDTH;
                for (int x = 1; x <= this.blocksInRow; x += 2)
                {
                    this.Walls.Add(new Wall(content, new Vector2(x * WALL_WIDTH, yCoord), true, WallTypes.Unbreakable, 8, new Vector2(1f, 1f)));
                }
            }
        }

        public void GenerateRandomBreakableWalls(ContentManager content)
        {
            for (int y = 0; y < this.blocksInColumn; y++)
            {
                int yCoord = y * WALL_WIDTH;
                if (y % 2 == 0)
                {
                    for (int x = 0; x <= this.blocksInRow; x += 2)
                    {
                        this.chanceToSpawnWall = this.rng.Next(0, 100);

                        if (this.chanceToSpawnWall < 30)
                        {
                            this.Walls.Add(new Wall(content, new Vector2(x * WALL_WIDTH, yCoord), true, WallTypes.Breakable, 8, new Vector2(1f, 1f)));
                        }
                    }
                }
                else
                {
                    for (int x = 1; x <= this.blocksInRow; x++)
                    {
                        this.chanceToSpawnWall = this.rng.Next(0, 100);

                        if (this.chanceToSpawnWall < 1)
                        {
                            this.Walls.Add(new Wall(content, new Vector2(x * WALL_WIDTH, yCoord), true, WallTypes.Breakable, 8, new Vector2(1f, 1f)));
                        }
                    }
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (var wall in this.Walls)
            {
                wall.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var wall in this.Walls)
            {
                wall.Draw(spriteBatch);
            }
        }
    }
}
