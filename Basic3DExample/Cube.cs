using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Basic3DExample
{
    public class Cube
    {
        /// <summary>
        /// The vertices of the cube
        /// </summary>
        VertexBuffer vertices;

        /// <summary>
        /// The vertex indices of the cube
        /// </summary>
        IndexBuffer indices;

        /// <summary>
        /// The effect to use rendering the cube
        /// </summary>
        BasicEffect effect;

        /// <summary>
        /// The game this cube belongs to 
        /// </summary>
        Game game;

        public Cube(Game1 game)
        {
            this.game = game;
            InitilizeVertices();
            InitilizeIndices();
            InitilizeEffect();
        }

        public void InitilizeVertices()
        {
            var vertexData = new VertexPositionColor[] {
            new VertexPositionColor() { Position = new Vector3(-3,  3, -3), Color = Color.Blue },
            new VertexPositionColor() { Position = new Vector3( 3,  3, -3), Color = Color.Green },
            new VertexPositionColor() { Position = new Vector3(-3, -3, -3), Color = Color.Red },
            new VertexPositionColor() { Position = new Vector3( 3, -3, -3), Color = Color.Cyan },
            new VertexPositionColor() { Position = new Vector3(-3,  3,  3), Color = Color.Blue },
            new VertexPositionColor() { Position = new Vector3( 3,  3,  3), Color = Color.Red },
            new VertexPositionColor() { Position = new Vector3(-3, -3,  3), Color = Color.Green },
            new VertexPositionColor() { Position = new Vector3( 3, -3,  3), Color = Color.Cyan }
            };

            vertices = new VertexBuffer(game.GraphicsDevice, typeof(VertexPositionColor), 8, BufferUsage.None);
            vertices.SetData<VertexPositionColor>(vertexData);
        }

        public void InitilizeIndices()
        {
            var indexData = new short[]
            {
                0, 1, 2, // Side 0
                2, 1, 3,
                4, 0, 6, // Side 1
                6, 0, 2,
                7, 5, 6, // Side 2
                6, 5, 4,
                3, 1, 7, // Side 3 
                7, 1, 5,
                4, 5, 0, // Side 4 
                0, 5, 1,
                3, 7, 2, // Side 5 
                2, 7, 6
            };
            indices = new IndexBuffer(game.GraphicsDevice, IndexElementSize.SixteenBits, 36, BufferUsage.None);
            indices.SetData<short>(indexData);
        }

        public void InitilizeEffect()
        {
            effect = new BasicEffect(game.GraphicsDevice);
            effect.World = Matrix.Identity;
            effect.View = Matrix.CreateLookAt(new Vector3(0, 0, 4), new Vector3(0, 0, 0), Vector3.Up);

            effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, game.GraphicsDevice.Viewport.AspectRatio, 0.1f, 100.0f);
            effect.VertexColorEnabled = true;
        }

        public void Update(GameTime gameTime)
        {
            float angle = (float)gameTime.TotalGameTime.TotalSeconds;

            effect.View = Matrix.CreateRotationY(angle) * Matrix.CreateLookAt(new Vector3(0, 5, -10), Vector3.Zero, Vector3.Up);
        }

        public void Draw()
        {
            effect.CurrentTechnique.Passes[0].Apply();

            game.GraphicsDevice.SetVertexBuffer(vertices);
            game.GraphicsDevice.Indices = indices;

            game.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 12);
        }
    }
}
