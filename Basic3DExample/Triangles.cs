using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Basic3DExample
{
    public class Triangles
    {

        private VertexPositionColor[] vertices;
        BasicEffect effect;
        Game game;


        public Triangles(Game game)
        {
            this.game = game;
            InitilizeVertices();
            InitalizeEffect();
        }

        void InitilizeVertices()
        {
            vertices = new VertexPositionColor[3];

            vertices[0].Position = new Vector3(0, 1, 0);
            vertices[0].Color = Color.Red;

            vertices[1].Position = new Vector3(1, 1, 0);
            vertices[1].Color = Color.Green;

            vertices[2].Position = new Vector3(1, 0, 0);
            vertices[2].Color = Color.Blue;
          
        }

        void InitalizeEffect()
        {
            effect = new BasicEffect(game.GraphicsDevice);
            effect.World = Matrix.Identity;
            effect.View = Matrix.CreateLookAt(
                new Vector3(0, 0, 4), // The camera position
                new Vector3(0, 0, 0), // The camera target,
                Vector3.Up            // The camera up vector
            );
            effect.Projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver4,                         // The field-of-view 
                game.GraphicsDevice.Viewport.AspectRatio,   // The aspect ratio
                0.1f, // The near plane distance 
                100.0f // The far plane distance
            );
            effect.VertexColorEnabled = true;
        }

        public void Update(GameTime gametime)
        {
            float angle = (float)gametime.TotalGameTime.TotalSeconds;
            effect.World = Matrix.CreateRotationY(angle);
        }

        public void Draw()
        {
            RasterizerState oldState = game.GraphicsDevice.RasterizerState;

            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            game.GraphicsDevice.RasterizerState = rasterizerState;


            effect.CurrentTechnique.Passes[0].Apply();

            game.GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleList, vertices, 0, 1);


            game.GraphicsDevice.RasterizerState = oldState;
        }
    }
}
