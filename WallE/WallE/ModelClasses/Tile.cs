using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace WallE
{
    public class Tile
    {
        VertexPositionTexture[] tile;
        VertexBuffer vertexBuffer;
        BasicEffect effect;
        Matrix world = Matrix.Identity;
        Texture2D texture;
        
        public Tile(Texture2D texture2D, GraphicsDeviceManager graphicsDeviceManager, Vector3 position, Vector3 dimension)
        {
            texture = texture2D;
            
            tile = new VertexPositionTexture[14];
            tile[0] = new VertexPositionTexture(new Vector3(position.X, position.Y, position.Z), new Vector2(0, 0));
            tile[1] = new VertexPositionTexture(new Vector3(position.X + dimension.X, position.Y, position.Z), new Vector2(1, 0));
            tile[2] = new VertexPositionTexture(new Vector3(position.X, position.Y + dimension.Y, position.Z), new Vector2(0, 1));
            tile[3] = new VertexPositionTexture(new Vector3(position.X + dimension.X, position.Y + dimension.Y, position.Z), new Vector2(1, 1));
            tile[4] = new VertexPositionTexture(new Vector3(position.X + dimension.X, position.Y + dimension.Y, position.Z + dimension.Z), new Vector2(1, 0));
            tile[5] = new VertexPositionTexture(new Vector3(position.X + dimension.X, position.Y, position.Z), new Vector2(0, 1));
            tile[6] = new VertexPositionTexture(new Vector3(position.X + dimension.X, position.Y, position.Z + dimension.Z), new Vector2(0, 0));
            tile[7] = new VertexPositionTexture(new Vector3(position.X, position.Y, position.Z), new Vector2(1, 1));
            tile[8] = new VertexPositionTexture(new Vector3(position.X, position.Y, position.Z + dimension.Z), new Vector2(1, 0));
            tile[9] = new VertexPositionTexture(new Vector3(position.X, position.Y + dimension.Y, position.Z), new Vector2(0, 1));
            tile[10] = new VertexPositionTexture(new Vector3(position.X, position.Y + dimension.Y, position.Z + dimension.Z), new Vector2(0, 0));
            tile[11] = new VertexPositionTexture(new Vector3(position.X + dimension.X, position.Y + dimension.Y, position.Z + dimension.Z), new Vector2(1, 0));
            tile[12] = new VertexPositionTexture(new Vector3(position.X, position.Y, position.Z + dimension.Z), new Vector2(0, 1));
            tile[13] = new VertexPositionTexture(new Vector3(position.X + dimension.X, position.Y, position.Z + dimension.Z), new Vector2(1, 1));

            // Set vertex data in VertexBuffer
            vertexBuffer = new VertexBuffer(graphicsDeviceManager.GraphicsDevice, typeof(VertexPositionTexture), tile.Length, BufferUsage.None);
            vertexBuffer.SetData(tile);

            // Initialize the BasicEffect
            effect = new BasicEffect(graphicsDeviceManager.GraphicsDevice);
        }

        public void Draw(GraphicsDeviceManager graphicsDeviceManager, Camera camera)
        {
            graphicsDeviceManager.GraphicsDevice.SetVertexBuffer(vertexBuffer);

            //Set object and camera info
            effect.World = world;
            effect.View = camera.View;
            effect.Projection = camera.Projection;
            effect.Texture = texture;
            effect.TextureEnabled = true;

            // Begin effect and draw for each pass
            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                graphicsDeviceManager.GraphicsDevice.DrawUserPrimitives<VertexPositionTexture>
                (PrimitiveType.TriangleStrip, tile, 0, 12);
            }
        }
    }
}
