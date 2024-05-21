using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.CodeDom;
using OpenTK.Graphics;
using OpenTK.Input;
using System.IO;
using System.Threading;
using System.ComponentModel;
using System.Globalization;
using System.Media;
using Silk.NET.Core;
using System.Timers;
using System.Diagnostics;
using System.Security.Policy;

namespace OpenTk26_3
{

   
    public class Game : GameWindow
    {
        public static Random rnd = new Random();
        static float[] vertices = {};
        static uint[] indices = {};
        public int VertexBufferObject;
        public int ElementBufferObject;
        public int VertexArrayObject;
        public static int MOUSEX,MOUSEY;
        public static bool quickMov = false;
        public static Stopwatch stopstopstop = new Stopwatch();
        public static float terrX=0;
        public static float terrY=0;

        Shader shader;

        public Game (int width, int height) : base (width, height,GraphicsMode.Default, "game")
        {

        }

        static MY_vertex[] infromFile(string path)
        {
            string[] infile = File.ReadAllLines(path);
                //int counter = 0;
                List<MY_vertex> vert = new List<MY_vertex>();
                //int count2 = 0;

                MY_vector3 col = new MY_vector3(rnd.Next(150, 205), rnd.Next(150, 205), rnd.Next(1, 205));

                for (int i = 0; i < infile.Count(); i++)
                {
                    if (infile[i].Contains("vertex"))
                    {
                        string[] invertex = infile[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        //int a = rnd.Next(150, 255);

                        //vert.Add(new MY_vertex(new MY_vector3(float.Parse(invertex[1]), float.Parse(invertex[2]), float.Parse(invertex[3])), Color.FromArgb(50, (((int)col.x + rnd.Next(1, 50))/255)*255, (((int)col.y + rnd.Next(1, 50)) / 255) * 255, (((int)col.z + rnd.Next(1, 50)) / 255) * 255)));
                        vert.Add(new MY_vertex(new MY_vector3(float.Parse(invertex[1]), float.Parse(invertex[2]), float.Parse(invertex[3])), Color.FromArgb(50, ((int)col.x + rnd.Next(1, 50)), ((int)col.y + rnd.Next(1, 50)), ((int)col.z + rnd.Next(1, 50)))));
                    }
                }

                //addverts(vert.ToArray());
                return vert.ToArray();

        }

        public static Car Vroooom;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            stopstopstop.Start();

            GL.ClearColor(Color.LightSkyBlue);

            //terrain stuff  DONT DELETE
            {
                Shape FLOOR = new Shape("256_good.obj");
                Shape.Models.Add(FLOOR);

                Terrain floor = new Terrain(FLOOR,Shape.Models.IndexOf(FLOOR),terrX,terrY,false);
                //Terrain floor = new Terrain(FLOOR,Shape.Models.IndexOf(FLOOR),320,320);
                //Shape.Models.Add(new Shape("cube.obj"));
                float X1 = FLOOR.verts[0].pos.x;
                float Z1 = FLOOR.verts[0].pos.z;
                float X2 = FLOOR.verts.Last().pos.x;
                float Z2 = FLOOR.verts.Last().pos.z;
                X1 -= X2;
                Z1 -= Z2;
                X1 = Math.Abs(X1);
                Z1 = Math.Abs(Z1);
                X1 /= 255f;
                Z1 /= 255f;




                //Shape.Models.Last().scala(new MY_vector3(X1, 0.1f, Z1));
                MY_Matrix3 moveTerr = new MY_Matrix3(10*X1,1,10*Z1,false);
                moveTerr = MY_Matrix3.multi4(moveTerr, new MY_Matrix3(new MY_vector3(0,-10,0)));
                for (int i = 0; i < Shape.Models[0].verts.Count();i++)
                {
                    Shape.Models[0].verts[i].pos = moveTerr.multiply(new MY_vector4(Shape.Models[0].verts[i].pos, 1f));
                }

            }
            //terrain stuff DONT DELETE


            //Shape.Models.Last().mooova(new MY_vector3(0,-100,0), new MY_vector3(0, 0,0));

            Vroooom = new Car();

            VertexBufferObject = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject); 
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StreamDraw);


            ElementBufferObject = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StreamDraw);


            shader = new Shader("shader.vert.txt", "shader.frag.txt");

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);


            

            VertexArrayObject = GL.GenVertexArray();

            GL.BindVertexArray(VertexArrayObject);


            //GL.Enable(EnableCap.)

            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.LineSmooth);

            MOUSEX = 0; MOUSEY = 0;

            Vroooom.YY();
        }
        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
            shader.Dispose();
        }
        public void freeCamKeys(KeyboardState input)
        {
            if (input.IsKeyDown(Key.W))
            {
                MY_vector3 mov = Program.player.camforward();

                if (quickMov)
                {
                    mov = new MY_vector3(mov.x * 10, mov.y * 10, mov.z * 10);
                }


                Program.player.pos = Program.player.pos.sum(new MY_vector3(mov.x, mov.y, mov.z));
                //Shape.Models[0].mooova(new MY_vector3(0, 1, 0), new MY_vector3(0, 0, 0));
                Console.WriteLine(Program.player.pos.x + "x, " + Program.player.pos.y + "y, " + Program.player.pos.z + "z, " + Shape.Models[0].centre.x + " " + Shape.Models[0].centre.y + " " + Shape.Models[0].centre.z + " " + Program.player.direction.y);
            }
            if (input.IsKeyDown(Key.S))
            {
                MY_vector3 mov = Program.player.camforward();

                if (quickMov)
                {
                    mov = new MY_vector3(mov.x * 10, mov.y * 10, mov.z * 10);
                }

                Program.player.pos = Program.player.pos.sum(new MY_vector3(-mov.x, -mov.y, -mov.z));
                //Shape.Models[0].mooova(new MY_vector3(0, -1, 0), new MY_vector3(0, 0, 0));
                Console.WriteLine(Program.player.pos.x + "x, " + Program.player.pos.y + "y, " + Program.player.pos.z + "z, " + Shape.Models[0].centre.x + " " + Shape.Models[0].centre.y + " " + Shape.Models[0].centre.z + " " + Program.player.direction.y);

            }
            if (input.IsKeyDown(Key.A))
            {
                MY_vector3 mov = MY_vector3.cross(Program.player.camforward(), new MY_vector3(0, -1, 0));

                if (quickMov)
                {
                    mov = new MY_vector3(mov.x * 10, mov.y * 10, mov.z * 10);
                }

                Program.player.pos = Program.player.pos.sum(new MY_vector3(mov.x, mov.y, mov.z));
                Console.WriteLine(Program.player.pos.x + "x, " + Program.player.pos.y + "y, " + Program.player.pos.z + "z, " + Shape.Models[0].centre.x + " " + Shape.Models[0].centre.y + " " + Shape.Models[0].centre.z + " " + Program.player.direction.y);

            }
            if (input.IsKeyDown(Key.D))
            {
                MY_vector3 mov = MY_vector3.cross(Program.player.camforward(), new MY_vector3(0, -1, 0));

                if (quickMov)
                {
                    mov = new MY_vector3(mov.x * 10, mov.y * 10, mov.z * 10);
                }

                Program.player.pos = Program.player.pos.sum(new MY_vector3(-mov.x, -mov.y, -mov.z));
                Console.WriteLine(Program.player.pos.x + "x, " + Program.player.pos.y + "y, " + Program.player.pos.z + "z, " + Shape.Models[0].centre.x + " " + Shape.Models[0].centre.y + " " + Shape.Models[0].centre.z + " " + Program.player.direction.y);
            }

            if (input.IsKeyDown(Key.ShiftLeft))
            {
                quickMov = true;
            }
            if (input.IsKeyUp(Key.ShiftLeft))
            {
                quickMov = false;
            }
        }
        public void FreeMouse()
        {
            MouseState moose = Mouse.GetState();
            Program.player.direction.y -= (moose.X - MOUSEX) * 0.001f;
            MOUSEX = moose.X;

            Program.player.direction.x += (moose.Y - MOUSEY) * 0.001f;
            MOUSEY = moose.Y;

            if (Program.player.direction.x > Math.PI / 2 - 0.05f)
            {
                Program.player.direction.x = (float)Math.PI / 2 - 0.05f;
            }
            else if (Program.player.direction.x < -Math.PI / 2 + 0.05f)
            {
                Program.player.direction.x = (float)-Math.PI / 2 + 0.05f;
            }
        }

        public void collide(Car Drive)
        {
            int X = (int)Math.Floor(0.1f*Drive.Kart.centre.x + 127.5f);//bottom left
            int Z = (int)Math.Floor(0.1f* Drive.Kart.centre.z + 127.5f);

            int Xc = (int)Math.Ceiling(Drive.Kart.centre.x + 127.5f);//top right
            int Zc = (int)Math.Ceiling(Drive.Kart.centre.z + 127.5f);

            if (X < 0) X = 0;
            if (Z < 0) Z = 0;
            if (X > 255) X = 255;
            if (Z > 255) Z = 255;

            float disX1 = Drive.Kart.centre.x +127.5f - X;
            float disZ1 = Drive.Kart.centre.z +127.5f - Z;
            float a = (float)Math.Sqrt(disX1*disX1 + disZ1* disZ1);
            float disX2 = Drive.Kart.centre.x + 127.5f - Xc;
            float disZ2 = Drive.Kart.centre.z + 127.5f - Zc;
            float b = (float)Math.Sqrt(disX2 * disX2 + disZ2 * disZ2);
            float TERRY;
            try
            {
                if (a > b)
                {// top right
                    TERRY = (Shape.Models[0].verts[X * 256 + Z].pos.y + Shape.Models[0].verts[(X + 1) * 256 + Z].pos.y + Shape.Models[0].verts[X * 256 + (Z + 1)].pos.y) / 3f;
                }
                else
                {// bottom left
                    TERRY = (Shape.Models[0].verts[Xc * 256 + Zc].pos.y + Shape.Models[0].verts[(X + 1) * 256 + Z].pos.y + Shape.Models[0].verts[X * 256 + (Z + 1)].pos.y) / 3f;
                }
            }
            catch (Exception ex)
            {
                TERRY = Shape.Models[0].verts[X * 256 + Z].pos.y;
            }


            //float TERRY = Shape.Models[0].verts[X * 256 + Z].pos.y;

            if (Drive.Kart.centre.y < TERRY + 1f)
            {
                Drive.Kart.centre.y = TERRY + 1f;
            }
            else Drive.Kart.centre.y -= 9.81f * 0.01f;
        }
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            KeyboardState input = Keyboard.GetState();

            if (input.IsKeyDown(Key.Escape))
            {
                Exit();
            }

            //freeCamKeys(input);
            FreeMouse();

            {//car movement function
                

                if (input.IsKeyDown(Key.W))
                {
                    Vroooom.acc = Vroooom.acc.sum(Vroooom.carForward().Times(0.1f));
                }
                else if(input.IsKeyUp(Key.W))
                {
                    if(Vroooom.acc.magnitude() < 0.5f)
                    {
                        Vroooom.acc = new MY_vector3(0, 0, 0);
                        Vroooom.velocity = Vroooom.velocity.Times(0.05f);
                    }
                    else
                    {
                        Vroooom.acc = Vroooom.acc.Times(0.4f);
                        Vroooom.velocity = Vroooom.velocity.Times(0.05f);
                    }
                }

                if (input.IsKeyDown(Key.D))
                {
                    if (Vroooom.velocity.magnitude() > 0.2f)
                    {
                        Vroooom.acc = Vroooom.acc.sum(MY_vector3.cross(Vroooom.carForward(), new MY_vector3(0, 1, 0)).Times(0.5f));
                    }
                }
                if (input.IsKeyDown(Key.A))
                {
                    if(Vroooom.velocity.magnitude() > 0.2f)
                    {
                        Vroooom.acc = Vroooom.acc.sum(MY_vector3.cross(Vroooom.carForward(), new MY_vector3(0, 1, 0)).Times(-0.5f));
                    }
                }

                if (Vroooom.velocity.magnitude() > 1)
                {
                    Vroooom.velocity = Vroooom.velocity.Times(1 / Vroooom.velocity.magnitude());
                }
                if (Vroooom.acc.magnitude() > 1)
                {
                    Vroooom.acc = Vroooom.acc.Times(1 / Vroooom.acc.magnitude());
                }



                Vroooom.velocity = Vroooom.velocity.sum(Vroooom.acc);

                Vroooom.Kart.mooova(Vroooom.velocity.sum(new MY_vector3(0,-1,0)), new MY_vector3(0, 0, 0));

                collide(Vroooom);

                Vroooom.Kart.angle = new MY_vector3(Vroooom.Kart.angle.x, (float)Math.Atan2(Vroooom.velocity.x,Vroooom.velocity.z), Vroooom.Kart.angle.z);

                Console.WriteLine((Vroooom.BL().x - Vroooom.TR().x).ToString() + " " + (Vroooom.BL().y - Vroooom.TR().y).ToString() + " " + (Vroooom.BL().z - Vroooom.TR().z).ToString());

                Program.player.pos = Vroooom.Kart.centre.sum(new MY_vector3(-Program.player.camforward().x * 0.6f, -Program.player.camforward().y * 0.5f + 0.1f, -Program.player.camforward().z * 0.66f));


            }



            if (input.IsKeyDown(Key.Space))
            {

                //Shape.Models.Add(new Shape(infromFile("cube_a.ast")));
                /*int a = rnd.Next(1, 4);
                switch (rnd.Next(1, 5))
                {
                    case 1:
                        Shape.Models.Add(new Shape(infromFile("d20_a.ast")));
                        Shape.Models.Last().scala(new MY_vector3(a,a,a));
                        Shape.Models.Last().mooova(new MY_vector3((rnd.Next(0, 4000) - 2000) * .5f, (rnd.Next(10, 500)) * .5f, (rnd.Next(0, 4000) - 2000) * .5f), new MY_vector3(0, 0, 0));
                        break;
                    case 2:
                        Shape.Models.Add(new Shape(infromFile("cube_a.ast")));
                        Shape.Models.Last().scala(new MY_vector3(a, a, a));
                        Shape.Models.Last().mooova(new MY_vector3((rnd.Next(0, 4000) - 2000) * .5f, (rnd.Next(10, 500)) * .5f, (rnd.Next(0, 4000) - 2000) * .5f), new MY_vector3(0, 0, 0));
                        break;
                    case 3:
                        Shape.Models.Add(new Shape("cube.obj"));
                        Shape.Models.Last().scala(new MY_vector3(3*a,3*a,3*a));
                        Shape.Models.Last().mooova(new MY_vector3((rnd.Next(0, 4000) - 2000) * .5f, (rnd.Next(10, 500)) * .5f, (rnd.Next(0, 4000) - 2000) * .5f), new MY_vector3(0, 0, 0));
                        break;
                    case 4:
                        Shape.Models.Add(new Shape("icosahedron.obj"));
                        Shape.Models.Last().scala(new MY_vector3(3*a, 3*a, 3*a));
                        Shape.Models.Last().mooova(new MY_vector3((rnd.Next(0, 4000) - 2000) * .5f, (rnd.Next(10, 500)) * .5f, (rnd.Next(0, 4000) - 2000) * .5f), new MY_vector3(0, 0, 0));
                        break;

                }*/

                /*Shape.Models.Add(new Shape("cube.obj"));
                Shape.Models.Last().scala(new MY_vector3(5, 5, 5));
                Shape.Models.Last().mooova(new MY_vector3((rnd.Next(0, 4000) - 2000) * .5f, (rnd.Next(10, 500)) * .5f, (rnd.Next(0, 4000) - 2000) * .5f), new MY_vector3(0, 0, 0));*/

                
                Shape.Models.Add(new Shape(infromFile("low-poly-pikachu.ast")/*,new MY_vector3(Game.rnd.Next(50, 255), Game.rnd.Next(50, 255), Game.rnd.Next(50, 255))*/)); 
                Shape.Models.Last().scala(new MY_vector3(.5f, .5f, .5f));
                Shape.Models.Last().mooova(new MY_vector3((rnd.Next(0, 1000) - 500) * .5f, (rnd.Next(50, 500)) * .5f, (rnd.Next(0, 1000) -  500) * .5f), new MY_vector3((float)-Math.PI/2, 0, 0));
                

                /*Shape.Models.Add(new Shape(infromFile("cube_a.ast")));
                Shape.Models.Last().mooova(new MY_vector3((rnd.Next(0, 4000) - 2000) * .5f, (-rnd.Next(10, 500)) * .5f, (rnd.Next(0, 4000) - 2000) * .5f), new MY_vector3(0, 0, 0));*/

                //Thread.Sleep(1);
            }
            /*(if (input.IsKeyDown(Key.Up))
            {
                Program.player.pos.z += 1f;
            }
            if (input.IsKeyDown(Key.Down))
            {
                Program.player.pos.z -= 1f;
            }*/
            if (input.IsKeyDown(Key.T))
            {
                Program.player.zooooooom -= 0.00174533f*4;
            }
            if (input.IsKeyDown(Key.G))
            {
                Program.player.zooooooom += 0.00174533f*4;
            }
            if (input.IsKeyDown(Key.R))
            {
                for (int i = 2; i < Shape.Models.Count; i++)
                {
                    Shape.Models[i].mooova(new MY_vector3(-Shape.Models[i].centre.x * 0.01f, -Shape.Models[i].centre.y * 0.01f, -Shape.Models[i].centre.z * 0.01f), new MY_vector3(0, 0, 0));
                }
            }
            if (input.IsKeyDown(Key.F))
            {
                Shape FLOOR = Shape.Models[0];
                Terrain floor = new Terrain(FLOOR, Shape.Models.IndexOf(FLOOR), terrX, terrY, false);
                //terrX+=0.1f;
                terrY+=0.05f;
            }

            if(input .IsKeyDown(Key.E))
            {
                Vroooom.Kart.centre.y += 5;
            }


            for (int i = 1; i < Shape.Models.Count(); i++)
            {
                Shape.Models[i].mooova(new MY_vector3(0, 0, 0), new MY_vector3(0.0177f * 0.4f, 0.0177f * 0.5f, 0.0177f * 0.6f));

            }


            //MY_vector3 CtoP = Program.player.pos.sub(Shape.Models[1].centre);
            //CtoP.y -= 5f;
            //Shape.Models[1].mooova(CtoP, new MY_vector3(0,0,0));


        }
        public class Car
        {
            public Shape Kart;
            public MY_vector3 velocity;
            public MY_vector3 acc;
            public MY_vector3 forward = new MY_vector3(0, 0, 1);
            public float Y;

            public Car()
            {
                Kart = new Shape("KART.obj"); 
                Kart.scala(new MY_vector3(0.1f, 0.1f, 0.1f));
                this.velocity = new MY_vector3(0, 0, 0);
                this.acc = new MY_vector3(0, -9.81f, 0);
                
            }
            public void YY()
            {
                Y = Math.Abs(BL().y - TR().y);
            }
            public MY_vector3 carForward()
            {
                MY_Matrix3 rotation = new MY_Matrix3(true, true, true, this.Kart.angle);
                MY_vector4 ouut = rotation.multiply(new MY_vector4(forward, 1));
                return new MY_vector3(ouut);
            }
            public MY_vector3 BL()
            {
                MY_vector3 a = new MY_vector3(float.MaxValue, float.MaxValue, float.MaxValue);
                for (int i = 0; i < Kart.verts.Count();i++)
                {
                    if (Kart.verts[i].pos.x < a.x)
                    {
                        a.x = Kart.verts[i].pos.x;
                    }
                    if (Kart.verts[i].pos.y < a.y)
                    {
                        a.y = Kart.verts[i].pos.y;
                    }
                    if (Kart.verts[i].pos.z < a.z)
                    {
                        a.z = Kart.verts[i].pos.z;
                    }
                }
                return a;
            }
            public MY_vector3 TR()
            {
                MY_vector3 a = new MY_vector3(-float.MaxValue, -float.MaxValue, -float.MaxValue);
                for (int i = 0; i < Kart.verts.Count(); i++)
                {
                    if (Kart.verts[i].pos.x > a.x)
                    {
                        a.x = Kart.verts[i].pos.x;
                    }
                    if (Kart.verts[i].pos.y > a.y)
                    {
                        a.y = Kart.verts[i].pos.y;
                    }
                    if (Kart.verts[i].pos.z > a.z)
                    {
                        a.z = Kart.verts[i].pos.z;
                    }
                }
                return a;
            }

        }

        public void DrawStuff(List<Shape> a)
        {
            for (int i = 0; i < a.Count(); i++)
            {
                Matrix4 proj = Program.pro();


                Matrix4 model = Program.modelMat(a[i]);

                //GL.BindVertexArray(VertexArrayObject);


                //GL.Uniform1(location:(proj), 1);
                proj = model * proj;

                int uniID = GL.GetUniformLocation(3, "projection");

                GL.UniformMatrix4(uniID, true, ref proj);

                vertices = a[i].GetFloat();
                indices = a[i].triangle;

                //Program.doLight(ref vertices, indices);
                //indices = Program.getinds();

                //Program.doLight(ref vertices,indices);

                GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
                GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.DynamicDraw);
                //shader = new Shader("shader.vert.txt", "shader.frag.txt");


                GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);
                GL.EnableVertexAttribArray(0);


                GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));
                GL.EnableVertexAttribArray(1);



                GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
                GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.DynamicDraw);


                shader.Use();

                //GL.UseProgram();
                //GL.BindVertexArray(VertexArrayObject);
                //GL.CullFace(CullFaceMode.Back);





                GL.BindVertexArray(VertexArrayObject);

                GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);


            }
        }
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);



            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            DrawStuff(new List<Shape> { Vroooom.Kart });
            DrawStuff(Shape.Models);
            

            //Console.WriteLine(Shape.shapes.Sum() + " " + Shape.Models.Count());


            shader.Dispose();

            this.SwapBuffers();
            
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, this.Width, this.Height);
            Program.player.aspect = (float)Width/(float)Height;  
        }

    }



    
}
