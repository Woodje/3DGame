using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace WallE
{
    public class InputManager
    {
        MouseState lastMouseState;
        Vector3 cameraFreeRotation = new Vector3(0, 0, 0), cameraUpDownLook = new Vector3(0, 0, 0);

        float scrollDelta = 500f;

        public void Initialize(GraphicsDeviceManager graphics)
        {
            // Set mouse position and do initial get state
            Mouse.SetPosition(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2);
            lastMouseState = Mouse.GetState();
        }

        public void updateModel(GameTime gameTime, Camera camera, CModel model, GraphicsDeviceManager graphics)
        {
            Vector3 rotChange = new Vector3(0, 0, 0);
            Vector3 freeRotation = new Vector3(0, 0, 0);
            Vector3 upDownLook = new Vector3(0, 0, 0);

            // Determine how much the camera should turn
            float deltaX = (float)lastMouseState.X - (float)Mouse.GetState().X;
            float deltaY = (float)lastMouseState.Y - (float)Mouse.GetState().Y;

            // Calculate scroll wheel movement
            scrollDelta = (float)lastMouseState.ScrollWheelValue - (float)Mouse.GetState().ScrollWheelValue;
            ((ChaseCamera)camera).UpdateScroolDelta(scrollDelta);

            // Move model with mouse
            if (Mouse.GetState().RightButton == ButtonState.Pressed)
            {
                rotChange = new Vector3(0, deltaX * 0.1f, 0);
                upDownLook = new Vector3(deltaY * 0.1f, 0, 0);
            }
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                freeRotation = new Vector3(deltaY * 0.1f, deltaX * 0.1f, 0);
            }

            // Rotate model with keyboard
            if (Keyboard.GetState().IsKeyDown(Keys.A) && Mouse.GetState().RightButton == ButtonState.Released)
                rotChange = new Vector3(0, 1, 0);
            if (Keyboard.GetState().IsKeyDown(Keys.D) && Mouse.GetState().RightButton == ButtonState.Released)
                rotChange = new Vector3(0, -1, 0);

            // Set speed of model and camera
            model.Rotation += rotChange * .025f;
            cameraFreeRotation += freeRotation * .025f;
            cameraUpDownLook += upDownLook * .025f;
            
            // Determine what direction to move in
            Matrix rotation = Matrix.CreateFromYawPitchRoll(model.Rotation.Y, 0, model.Rotation.Z);

            // Move in the direction dictated by our rotation matrix
            if (Keyboard.GetState().IsKeyDown(Keys.W) || Mouse.GetState().RightButton == ButtonState.Pressed && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                model.Model.Root.Transform = Matrix.CreateRotationY(0);
                model.Model.Bones[1].Transform *= Matrix.CreateRotationX(-0.2f);
                model.Model.Bones[2].Transform *= Matrix.CreateRotationX(-0.2f);
                model.Model.Bones[3].Transform = Matrix.CreateTranslation(new Vector3(0, 5.235f, -5.235f)) * Matrix.CreateRotationY(MathHelper.ToRadians(90));
                model.Position += Vector3.Transform(Vector3.Forward, rotation) * (float)gameTime.ElapsedGameTime.TotalMilliseconds * 0.2f;
            }
            
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                model.Model.Root.Transform = Matrix.CreateRotationY(0);
                model.Model.Bones[1].Transform *= Matrix.CreateRotationX(0.2f);
                model.Model.Bones[2].Transform *= Matrix.CreateRotationX(0.2f);
                model.Model.Bones[3].Transform = Matrix.CreateTranslation(new Vector3(0, 5.235f, -5.235f)) * Matrix.CreateRotationY(MathHelper.ToRadians(90));
                model.Position += Vector3.Transform(Vector3.Backward, rotation) * (float)gameTime.ElapsedGameTime.TotalMilliseconds * 0.2f;
            }

            // Move model left and right
            if (Keyboard.GetState().IsKeyDown(Keys.D) && Mouse.GetState().RightButton == ButtonState.Pressed)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.W) || Mouse.GetState().RightButton == ButtonState.Pressed && Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    model.Model.Root.Transform = Matrix.CreateRotationY(-0.5f);
                    model.Model.Bones[3].Transform = Matrix.CreateTranslation(new Vector3(0, 5.235f, -5.235f)) * Matrix.CreateRotationY(MathHelper.ToRadians(135));
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    model.Model.Root.Transform = Matrix.CreateRotationY(0.5f);
                    model.Model.Bones[3].Transform = Matrix.CreateTranslation(new Vector3(0, 5.235f, -5.235f)) * Matrix.CreateRotationY(MathHelper.ToRadians(90));
                }
                else
                {
                    model.Model.Root.Transform = Matrix.CreateRotationY(MathHelper.ToRadians(-90));
                    model.Model.Bones[1].Transform *= Matrix.CreateRotationX(-0.2f);
                    model.Model.Bones[2].Transform *= Matrix.CreateRotationX(-0.2f);
                    model.Model.Bones[3].Transform = Matrix.CreateTranslation(new Vector3(0, 5.235f, -5.235f)) * Matrix.CreateRotationY(MathHelper.ToRadians(90));
                }

                model.Position += Vector3.Transform(Vector3.Right, rotation) * (float)gameTime.ElapsedGameTime.TotalMilliseconds * 0.2f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A) && Mouse.GetState().RightButton == ButtonState.Pressed)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.W) || Mouse.GetState().RightButton == ButtonState.Pressed && Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    model.Model.Root.Transform = Matrix.CreateRotationY(0.5f);
                    model.Model.Bones[3].Transform = Matrix.CreateTranslation(new Vector3(0, 5.235f, -5.235f)) * Matrix.CreateRotationY(MathHelper.ToRadians(45));
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    model.Model.Root.Transform = Matrix.CreateRotationY(-0.5f);
                    model.Model.Bones[3].Transform = Matrix.CreateTranslation(new Vector3(0, 5.235f, -5.235f)) * Matrix.CreateRotationY(MathHelper.ToRadians(90));
                }
                else
                {
                    model.Model.Root.Transform = Matrix.CreateRotationY(MathHelper.ToRadians(90));
                    model.Model.Bones[1].Transform *= Matrix.CreateRotationX(-0.2f);
                    model.Model.Bones[2].Transform *= Matrix.CreateRotationX(-0.2f);
                    model.Model.Bones[3].Transform = Matrix.CreateTranslation(new Vector3(0, 5.235f, -5.235f)) * Matrix.CreateRotationY(MathHelper.ToRadians(90));
                }

                model.Position += Vector3.Transform(Vector3.Left, rotation) * (float)gameTime.ElapsedGameTime.TotalMilliseconds * 0.2f;
            }

            // Set mouse position and do initial get state
            Mouse.SetPosition(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2);

            // Update the mousestate
            lastMouseState = Mouse.GetState();
        }

        public void updateCamera(Camera camera, CModel model)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W) || Mouse.GetState().RightButton == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.D))
            {
                // Move the camera to the new model's position and orientation
                ((ChaseCamera)camera).Move(model.Position, model.Rotation, cameraUpDownLook);
                cameraFreeRotation = model.Rotation;
            }

            if (Mouse.GetState().LeftButton == ButtonState.Pressed && Mouse.GetState().RightButton == ButtonState.Released)
                ((ChaseCamera)camera).Move(model.Position, cameraFreeRotation, cameraUpDownLook);

            // Update the camera
            camera.Update();
        }
    }
}
