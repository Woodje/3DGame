using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace WallE
{
    public class MapManager
    {
        public List<BoundingBox> bBList = new List<BoundingBox>();
        List<Tile> tiles = new List<Tile>();

        int point1 = -4, point2 = 1, point3 = 1, point4 = 0;

        public void LoadContent(GraphicsDeviceManager graphicsDeviceManager, ContentManager content)
        {
            for (int i = point1; i <= 2; i++)
            {
                tiles.Add(new Tile(content.Load<Texture2D>("brick_texture_map"), graphicsDeviceManager, new Vector3(400 * i, 0, 1000), new Vector3(400, 200, 10)));
                bBList.Add(new BoundingBox(new Vector3(400 * i, 0, 1000), new Vector3((400 * i) + 400, 200, 1010)));
                point1 = 400 * i + 400;
            }

            for (int i = point2; i < 6; i++)
            {
                tiles.Add(new Tile(content.Load<Texture2D>("brick_texture_map"), graphicsDeviceManager, new Vector3(point1, 0, 1000 + (-400 * i)), new Vector3(10, 200, 400)));
                point2 = 1000 + (-400 * i);
            }

            for (int i = point3; i < 8; i++)
            {
                tiles.Add(new Tile(content.Load<Texture2D>("brick_texture_map"), graphicsDeviceManager, new Vector3(point1 + (-400 * i), 0, point2), new Vector3(400, 200, 10)));
                point3 = point1 + (-400 * i);
            }

            for (int i = point4; i < 5; i++)
            {
                tiles.Add(new Tile(content.Load<Texture2D>("brick_texture_map"), graphicsDeviceManager, new Vector3(point3, 0, point2 + (400 * i)), new Vector3(10, 200, 400)));
            }
        }

        public void Draw(GraphicsDeviceManager graphicsDeviceManager, Camera camera)
        {
            foreach (Tile tile in tiles)
                tile.Draw(graphicsDeviceManager, camera);
        }
    }
}
