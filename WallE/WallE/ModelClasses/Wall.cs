using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace WallE
{
    public class Wall
    {
        VertexPositionTexture[] wall;
        VertexBuffer vertexBuffer;
        BasicEffect effect;
        Matrix world = Matrix.Identity;
        Texture2D texture;

        public void LoadContent(ContentManager content, GraphicsDeviceManager graphicsDeviceManager)
        {
            texture = content.Load<Texture2D>("brick_texture_map");

            wall = new VertexPositionTexture[22];
            wall[0] = new VertexPositionTexture(new Vector3(200, 0, 200), new Vector2(0, 0));
            wall[1] = new VertexPositionTexture(new Vector3(200, 200, 200), new Vector2(2, 0));
            wall[2] = new VertexPositionTexture(new Vector3(0, 0, 200), new Vector2(0, 2));
            wall[3] = new VertexPositionTexture(new Vector3(0, 200, 200), new Vector2(2, 2));
            wall[4] = new VertexPositionTexture(new Vector3(0, 0, 220), new Vector2(0, 0));
            wall[5] = new VertexPositionTexture(new Vector3(0, 200, 220), new Vector2(2, 0));
            wall[6] = new VertexPositionTexture(new Vector3(200, 0, 220), new Vector2(0, 2));
            wall[7] = new VertexPositionTexture(new Vector3(200, 200, 220), new Vector2(2, 2));
            wall[8] = new VertexPositionTexture(new Vector3(200, 0, 200), new Vector2(0, 0));
            wall[9] = new VertexPositionTexture(new Vector3(200, 200, 220), new Vector2(2, 0));
            wall[10] = new VertexPositionTexture(new Vector3(200, 200, 200), new Vector2(2, 2));
            wall[11] = new VertexPositionTexture(new Vector3(0, 200, 220), new Vector2(0, 2));
            wall[12] = new VertexPositionTexture(new Vector3(0, 200, 200), new Vector2(0, 0));

            // Set vertex data in VertexBuffer
            vertexBuffer = new VertexBuffer(graphicsDeviceManager.GraphicsDevice, typeof(VertexPositionTexture), wall.Length, BufferUsage.None);
            vertexBuffer.SetData(wall);

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
                (PrimitiveType.TriangleStrip, wall, 0, 11);
            }
        }
    }
}
