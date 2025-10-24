using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Basic3DExample
{
    public class Quad
    {
        /// <summary>
        /// The vertices of the quad
        /// </summary>
        VertexPositionTexture[] vertices;

        /// <summary>
        /// The vertex indices of the quad
        /// </summary>
        short[] indices;

        /// <summary>
        /// The effect to use rendering the triangle
        /// </summary>
        BasicEffect effect;

        /// <summary>
        /// The game this cube belongs to 
        /// </summary>
        Game game;

        public Quad(Game game)
        {
            this.game = game;
            InitializeVertices();
            InitializeIndices();
            InitializeEffect();
        }

        public void InitializeVertices()
        {
            vertices = new VertexPositionTexture[4];
            // Define vertex 0 (top left)
            vertices[0].Position = new Vector3(-1, 1, 0);
            vertices[0].TextureCoordinate = new Vector2(0, -1);
            // Define vertex 1 (top right)
            vertices[1].Position = new Vector3(1, 1, 0);
            vertices[1].TextureCoordinate = new Vector2(1, -1);
            // define vertex 2 (bottom right)
            vertices[2].Position = new Vector3(1, -1, 0);
            vertices[2].TextureCoordinate = new Vector2(1, 0);
            // define vertex 3 (bottom left) 
            vertices[3].Position = new Vector3(-1, -1, 0);
            vertices[3].TextureCoordinate = new Vector2(0, 0);
        }

        public void InitializeIndices()
        {
            indices = new short[6];

            // Define triangle 0 
            indices[0] = 0;
            indices[1] = 1;
            indices[2] = 2;
            // define triangle 1
            indices[3] = 2;
            indices[4] = 3;
            indices[5] = 0;
        }

        public void InitializeEffect()
        {
            effect = new BasicEffect(game.GraphicsDevice);
            effect.World = Matrix.Identity;
            effect.View = Matrix.CreateLookAt(
                new Vector3(0, 0, 4), 
                new Vector3(0, 0, 0), 
                Vector3.Up        
            );
            effect.Projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver4,                        
                game.GraphicsDevice.Viewport.AspectRatio,  
                0.1f,
                100.0f 
            );
            effect.TextureEnabled = true;
            effect.Texture = game.Content.Load<Texture2D>("monogame-logo");
        }

        public void Draw()
        {
            BlendState oldBlendState = game.GraphicsDevice.BlendState;
            
            game.GraphicsDevice.BlendState =  BlendState.AlphaBlend;

            effect.CurrentTechnique.Passes[0].Apply();

            game.GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionTexture>(PrimitiveType.TriangleList, vertices, 0, 4, indices, 0, 2);

            game.GraphicsDevice.BlendState = oldBlendState;
        }
    }
}
