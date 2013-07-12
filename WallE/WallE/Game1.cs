using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace WallE
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        List<CModel> models = new List<CModel>();
        Camera camera;
        InputManager Input = new InputManager();
        BoneModel boner;
        Tile tile, tile2;
        float test = 0.1f;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 800;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            models.Add(new CModel(Content.Load<Model>("freak"), new Vector3(40, 50, 50), Vector3.Zero, false, new Vector3(10), GraphicsDevice));

            models.Add(new CModel(Content.Load<Model>("new1"), Vector3.Zero, Vector3.Zero, false, new Vector3(200f), GraphicsDevice));

            camera = new ChaseCamera(new Vector3(0, 50, 300), new Vector3(0, 50, 0), Vector3.Zero, GraphicsDevice);
            ((ChaseCamera)camera).Move(models[0].Position, models[0].Rotation, new Vector3(0, 0, 0));

            boner = new BoneModel(Content.Load<Model>("freak3"));

            tile = new Tile(Content.Load<Texture2D>("brick_texture_map"), graphics, new Vector3(-1200,0,1000), new Vector3(400,200,10));
            tile2 = new Tile(Content.Load<Texture2D>("brick_texture_map"), graphics, new Vector3(-800, 0, 1000), new Vector3(400, 200, 10));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            Input.updateModel(gameTime, camera, models[0], graphics);
            Input.updateCamera(camera, models[0]);

            if (Keyboard.GetState().IsKeyDown(Keys.K))
            {
                test += 0.01f;
                boner.Update(gameTime, test);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.J))
            {
                test -= 0.01f;
                boner.Update(gameTime, test);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            foreach (CModel model in models)
                if (camera.BoundingVolumeIsInView(model.BoundingSphere))
                    model.Draw(camera.View, camera.Projection);

            boner.Draw(camera);
            
            tile.Draw(graphics, camera);
            tile2.Draw(graphics, camera);
            base.Draw(gameTime);
        }
    }
}
