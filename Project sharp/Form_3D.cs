using System;
using System.Drawing;
using System.Windows.Forms;

using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace Project_sharp
{
    public partial class Form_3D : Form
    {
        DiamondSquare terra;

        private Device device = null;
        private VertexBuffer vb = null;
        private IndexBuffer ib = null;

        private static int terWidth = 1024;
        private static int terLength = 1024;
        private float moveSpeed = 7f;
        private float turnSpeed = 0.05f;
        private float rotY = 0;
        private float tempY = 0;

        private float rotXZ = 0;
        private float tempXZ = 0;

        private static int vertCount = 0;
        private static int indCount = 0;

        private Vector3 camPosition, camLookAt, camUp;

        CustomVertex.PositionColored[] verts;

        bool isMiddleMouseDown = false;

        private Bitmap bitmap = null;

        private static int[] indices = null;

        private FillMode fillMode = FillMode.WireFrame;
        private Color backgroundColor = Color.Black;
        private bool invalidating = true;

        public Form_3D(DiamondSquare map)
        {
            terra = map;
            terWidth = terra.Size;
            terLength = terra.Size;
            indCount = (terWidth - 1) * (terLength - 1) * 6;
            vertCount = terWidth* terLength;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque, true);
            
            InitializeComponent();
            InitializeGraphics();

            InitializeEventHandler();
            
        }
        private void InitializeGraphics()
        {
            PresentParameters pp = new PresentParameters();
            pp.Windowed = true;
            pp.SwapEffect = SwapEffect.Discard;
            pp.EnableAutoDepthStencil = true;
            pp.AutoDepthStencilFormat = DepthFormat.D16;

            device = new Device(0, DeviceType.Hardware, this, CreateFlags.HardwareVertexProcessing, pp);

            GenerateVertex();
            GenerateIndex();

            vb = new VertexBuffer(typeof(CustomVertex.PositionColored), vertCount, device, Usage.Dynamic | Usage.WriteOnly, CustomVertex.PositionColored.Format, Pool.Default);
            OnVertexBufferCreate(vb, null);

            ib = new IndexBuffer(typeof(int), indCount, device, Usage.WriteOnly, Pool.Default);
            OnIndexBufferCreate(ib, null);

            // Initialize Camera position
            camPosition = new Vector3(2, 4.5f, -3.5f);
            camUp = new Vector3(0, 1, 0);
        }
        private void InitializeEventHandler()
        {
            vb.Created += new EventHandler(OnVertexBufferCreate);
            ib.Created += new EventHandler(OnIndexBufferCreate);

            this.KeyDown += new KeyEventHandler(OnKeyDown);
            this.MouseWheel += new MouseEventHandler(OnMouseScroll);

            this.MouseMove += new MouseEventHandler(OnMouseMove);
            this.MouseDown += new MouseEventHandler(OnMouseDown);
            this.MouseUp += new MouseEventHandler(OnMouseUp);

        }
        private void OnIndexBufferCreate(object sender, EventArgs e)
        {
            IndexBuffer buffer = (IndexBuffer)sender;
            buffer.SetData(indices, 0, LockFlags.None); // puts all index fron the int array into the index buffer
        }
        private void OnVertexBufferCreate(object sender, EventArgs e)
        {
            VertexBuffer buffer = (VertexBuffer)sender;
            buffer.SetData(verts, 0, LockFlags.None); // puts all vertices from the vertex array into the vertex buffer
        }
        private void SetUpCamera()
        {
            camLookAt.X = (float)Math.Sin(rotY) + camPosition.X + (float)(Math.Sin(rotXZ) * Math.Sin(rotY));       //
            camLookAt.Y = (float)Math.Sin(rotXZ) + camPosition.Y;                               // build the camera lookAt comehow with the camera position
            camLookAt.Z = (float)Math.Cos(rotY) + camPosition.Z + (float)(Math.Sin(rotXZ) * Math.Cos(rotY));       //

            device.Transform.Projection = Matrix.PerspectiveFovLH((float)Math.PI / 4, this.Width / this.Height, 1.0f, 10000.0f);
            device.Transform.View = Matrix.LookAtLH(camPosition, camLookAt, camUp);

            
            device.RenderState.Lighting = false;
            device.RenderState.CullMode = Cull.CounterClockwise;
            device.RenderState.FillMode = fillMode;
        }
        private void Form_3D_Paint(object sender, PaintEventArgs e) // here we kind of have a drawind loop
        {
            device.Clear(ClearFlags.Target | ClearFlags.ZBuffer, backgroundColor, 1, 0);
            SetUpCamera();
            device.BeginScene();

            device.VertexFormat = CustomVertex.PositionColored.Format;

            device.SetStreamSource(0, vb, 0); // tells the device where to receive the vertex information from
            device.Indices = ib; // tells the devvice where to receive the index information from

            device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, vertCount, 0, indCount/3);

            device.EndScene();
            device.Present();

            menuStrip2.Update();
            if (invalidating)
            {
                this.Invalidate();// this makes the form redraw itself over and over so that we have some kind of a loop 
            }
            
        }
        private void GenerateVertex()
        {
            verts = new CustomVertex.PositionColored[vertCount];
            int k = 0;
            DiamondSquare a = terra;

            for (int z = 0; z<terWidth;z++)
                for(int x = 0; x < terLength; x++)
                {
                    verts[k].Position = new Vector3(x, 60*(float)a.GetValue(x,z), z);
                    if(50*a.GetValue(x,z) < 0)
                    {
                        verts[k].Color = Color.Blue.ToArgb();
                    }
                    else if(50 * a.GetValue(x, z) >= 0 && 50 * a.GetValue(x, z) < 5)
                    {
                        verts[k].Color = Color.Yellow.ToArgb();
                    }
                    else if (50 * a.GetValue(x, z) >= 5 && 50 * a.GetValue(x, z) < 30)
                    {
                        verts[k].Color = Color.Green.ToArgb();
                    }
                    else if(50 * a.GetValue(x, z) >= 30 && 50 * a.GetValue(x, z) < 50)
                    {
                        verts[k].Color = Color.Gray.ToArgb();
                    }
                    else
                    {
                        verts[k].Color = Color.White.ToArgb();
                    }

                    k++;
                }
            
        }

        private void GenerateIndex()
        {
            indices = new int[indCount];
            int k = 0;
            int l = 0;
            for(int i = 0; i < indCount; i += 6)
            {
                indices[i] = k;
                indices[i + 1] = k + terLength;
                indices[i + 2] = k + terLength + 1;
                indices[i + 3] = k;
                indices[i + 4] = k + terLength + 1;
                indices[i + 5] = k + 1;
                k++;
                l++;
                if (l == terLength - 1)
                {
                    l = 0;
                    k++;
                }
            }
        }
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                //foward
                case (Keys.W):
                    {
                        camPosition.X += moveSpeed * (float)Math.Sin(rotY);
                        camPosition.Z += moveSpeed * (float)Math.Cos(rotY);
                        break;
                    }
                // backward
                case (Keys.S):
                    {
                        camPosition.X -= moveSpeed * (float)Math.Sin(rotY);
                        camPosition.Z -= moveSpeed * (float)Math.Cos(rotY);
                        break;
                    }
                // right
                case (Keys.D):
                    {
                        camPosition.X += moveSpeed * (float)Math.Sin(rotY + Math.PI/2);
                        camPosition.Z += moveSpeed * (float)Math.Cos(rotY + Math.PI/2);
                        break;
                    }
                //left
                case (Keys.A):
                    {
                        camPosition.X -= moveSpeed * (float)Math.Sin(rotY + Math.PI / 2);
                        camPosition.Z -= moveSpeed * (float)Math.Cos(rotY + Math.PI / 2);
                        break;
                    }
                // rotate left
                case (Keys.Q):
                    {
                        rotY -= turnSpeed;
                        break;
                    }
                //rotate right
                case (Keys.E):
                    {
                        rotY += turnSpeed;
                        break;
                    }
                case (Keys.Up):
                    {
                        if(rotXZ < Math.PI/2)
                            rotXZ += turnSpeed;
                        break;
                    }
                case (Keys.Down):
                    {
                        if (rotXZ > -Math.PI / 2)
                            rotXZ -= turnSpeed;
                        break;
                    }
            }
        }
        private void OnMouseScroll(object sender, MouseEventArgs e)
        {
            camPosition.Y -= e.Delta * 0.1f;
        }
        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (isMiddleMouseDown)
            {
                rotY = tempY + e.X * turnSpeed;
                float tmp = tempXZ + e.Y * turnSpeed / 4;
                if (tmp < Math.PI / 2 && tmp > -Math.PI / 2)
                {
                    rotXZ = tmp;
                }
            }
        }

        private void solidToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fillMode = FillMode.Solid;
            solidToolStripMenuItem.Checked = true;
            wireframeToolStripMenuItem.Checked = false;
            pointToolStripMenuItem.Checked = false;
        }

        private void wireframeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fillMode = FillMode.WireFrame;
            solidToolStripMenuItem.Checked = false;
            wireframeToolStripMenuItem.Checked = true;
            pointToolStripMenuItem.Checked = false;
        }

        private void pointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fillMode = FillMode.Point;
            solidToolStripMenuItem.Checked = false;
            wireframeToolStripMenuItem.Checked = false;
            pointToolStripMenuItem.Checked = true;
        }

        
        private void backgroundColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            invalidating = false;
            if(cd.ShowDialog(this) == DialogResult.OK)
            {
                backgroundColor = cd.Color;
            }
            invalidating = true;
            this.Invalidate();
        }
        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case (MouseButtons.Middle):
                    {
                        tempXZ = rotXZ + e.Y * turnSpeed / 4;
                        tempY = rotY - e.X * turnSpeed;
                        isMiddleMouseDown = true;
                        break;
                    }
            }
        }
        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case (MouseButtons.Middle):
                    {
                        isMiddleMouseDown = false;
                        break;
                    }
            }
        }
    }
}
