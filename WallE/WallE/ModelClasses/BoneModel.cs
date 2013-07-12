using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace WallE
{
    public class BoneModel
    {
        // Store the original transform matrix for each animating bone.
        Matrix boneTransform;

        // Our model
        private Model model;

        // Array holding all the bone transform matrices for the entire model.
        // We could just allocate this locally inside the Draw method, but it
        // is more efficient to reuse a single array, as this avoids creating
        // unnecessary garbage.
        Matrix[] _boneTransforms;

        public BoneModel(Model model)
        {
            this.model = model;
            
            // Allocate the transform matrix array.
            _boneTransforms = new Matrix[model.Bones.Count];
            // Store the original transform matrix for each animating bone.
            boneTransform = model.Bones[0].Transform;
            //rotate the model 90 degrees
            model.Root.Transform *= Matrix.CreateRotationY(MathHelper.ToRadians(90));
        }

        public void Update(GameTime gameTime, float move)
        {
            // Apply the transform to the Bone
            //model.Bones[0].Transform = Matrix.CreateTranslation((float)(sine), 0, (float)(cosine)) * boneTransform;
            //model.Bones[2].Transform *= Matrix.CreateRotationZ(move);
            //model.Bones[2].Transform = Matrix.CreateTranslation(new Vector3(0, -1, -2.5f)) * Matrix.CreateRotationY(MathHelper.ToRadians(90)) * Matrix.CreateRotationX(move);
            //model.Bones[1].Transform *= Matrix.CreateRotationX(move);
            model.Bones[2].Transform = Matrix.CreateRotationZ(move) * Matrix.CreateTranslation(new Vector3(0, 2.5f, 2.5f)) * Matrix.CreateRotationY(MathHelper.ToRadians(90));
            //model.Bones[1].Transform = Matrix.CreateRotationZ(-time) * Matrix.CreateTranslation(new Vector3(0, 2.5f, -5f)) * Matrix.CreateRotationY(MathHelper.ToRadians(90));
            //model.Bones[2].Transform *= Matrix.CreateRotationX(move);
            //model.Bones[3].Transform *= Matrix.CreateRotationY(move);
            //model.Bones[4].Transform *= Matrix.CreateRotationY(-move);
            //model.Bones[5].Transform *= Matrix.CreateRotationY(-move);
            //model.Bones[6].Transform *= Matrix.CreateRotationY(-move);
            //Matrix rotation = Matrix.CreateFromYawPitchRoll(model.Rotation.Y, 0, model.Rotation.Z);
            //model.Bones[1].Transform += Vector3.Transform(Vector3.Backward, rotation) * (float)gameTime.ElapsedGameTime.TotalMilliseconds * 0.2f;
        }

        public void Draw(Camera camera)
        {
            model.CopyAbsoluteBoneTransformsTo(_boneTransforms);

            // Draw the model.
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = _boneTransforms[mesh.ParentBone.Index] * Matrix.CreateScale(15f) * Matrix.CreateTranslation(-50,50,-50);
                    effect.View = camera.View;
                    effect.Projection = camera.Projection;
                    effect.EnableDefaultLighting();
                }
                mesh.Draw();
            }
        }
    }
}
