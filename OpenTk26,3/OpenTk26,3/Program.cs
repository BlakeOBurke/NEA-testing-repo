using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Drawing;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace OpenTk26_3
{
    internal class Program
    {

        class vortex :MY_vertex
        {
            public float w;
            public vortex(float x, float y, float z, Color Colors) : base(x, y, z, Colors)
            {
                this.w = 1;
            }
            public vortex(float x, float y, float z,float w, Color Colors) : base(x, y, z, Colors)
            {
                this.w = w;
            }
            public vortex(MY_vector4 a, Color Colors) : base(a.x, a.y, a.z, Colors)
            {
                this.w = a.w;
            }
        }

        public static camera player = new camera(0, 0, 0);
        static void Main(string[] args)
        {
            Game ga = new Game(1200,900);
            ga.Run(60,60);

            Console.WriteLine("hi");
        }

        public static MY_Matrix3 getTranslate(camera A)
        {
            return new MY_Matrix3(-A.pos.x, -A.pos.y, -A.pos.z);
        }
        public static uint[] getinds()
        {
            List<uint> a = new List<uint>();
            int cur = 0;
            for (int i = 0; i < Shape.Models.Count(); i++)
            {
                for (int j = 0; j < Shape.Models[i].triangle.Count(); j++)
                {
                    a.Add((uint)Shape.Models[i].triangle[j]);
                    //cur++;
                }
            }
            return a.ToArray();
        }

        public static Matrix4 modelMat(Shape shapee)
        {
            Matrix4 matrix = new Matrix4();

            MY_Matrix3 mov = MY_Matrix3.multi4(new MY_Matrix3(shapee.centre), new MY_Matrix3(true, shapee.angle));

            matrix[0,0] = mov.mat[0,0];
            matrix[0,1] = mov.mat[0,1];
            matrix[0,2] = mov.mat[0,2];
            matrix[0,3] = mov.mat[0,3];

            matrix[1, 0] = mov.mat[1, 0];
            matrix[1, 1] = mov.mat[1, 1];
            matrix[1, 2] = mov.mat[1, 2];
            matrix[1, 3] = mov.mat[1, 3];

            matrix[2, 0] = mov.mat[2, 0];
            matrix[2, 1] = mov.mat[2, 1];
            matrix[2, 2] = mov.mat[2, 2];
            matrix[2, 3] = mov.mat[2, 3];

            matrix[3, 0] = mov.mat[3, 0];
            matrix[3, 1] = mov.mat[3, 1];
            matrix[3, 2] = mov.mat[3, 2];
            matrix[3, 3] = mov.mat[3, 3];

            matrix.Transpose();

            return matrix;
        }

        public static void doLight(ref float[] vertices, uint[] inds)
        {
            float[] lighting = new float[vertices.Count() / 2];
            for (int i = 0; i < inds.Count()/3; i++)
            {
                MY_vector3 a = new MY_vector3(vertices[inds[i * 3 + 2] * 6] - vertices[inds[i * 3] * 6], vertices[inds[i * 3 + 2] * 6 + 1] - vertices[inds[i * 3] * 6 + 1], vertices[inds[i * 3 + 2] * 6 + 2] - vertices[inds[i * 3] * 6 + 2]);
                MY_vector3 b = new MY_vector3(vertices[inds[i * 3 + 1] * 6] - vertices[inds[i * 3] * 6], vertices[inds[i * 3 + 1] * 6 + 1] - vertices[inds[i * 3] * 6 + 1], vertices[inds[i * 3 + 1] * 6 + 2] - vertices[inds[i * 3] * 6 + 2]);

                MY_vector3 c = new MY_vector3(vertices[inds[i * 3 + 2] * 6] + vertices[inds[i * 3 + 1] * 6] + vertices[inds[i * 3] * 6], vertices[inds[i * 3 + 2] * 6 + 1] + vertices[inds[i * 3 + 1] * 6 + 1] + vertices[inds[i * 3] * 6 + 1], vertices[inds[i * 3 + 2] * 6 + 2] + vertices[inds[i * 3 + 1] * 6 + 2] + vertices[inds[i * 3] * 6 + 2]);
                c = new MY_vector3(c.x/3,c.y/3,c.z/3);

                a = new MY_vector3(a.x-c.x,a.y-c.y,a.z-c.z);
                b = new MY_vector3(b.x-c.x,b.y-c.y,b.z-c.z);

                MY_vector3 normal = MY_vector3.cross(a,b);

                //MY_vector3 playerPoint = player.camforward();
                //MY_vector3 playerLight = new MY_vector3(playerPoint.x + player.pos.x, playerPoint.y + player.pos.y, playerPoint.z + player.pos.z);

                float brightness = MY_vector3.dot(normal, /*new MY_vector3(0,0, -1)*/player.camforward().normalise())/(normal.magnitude());
                brightness = (float)Math.Cos(brightness);
                //float brightness = MY_vector3.dot(normal, new MY_vector3(0,0, -1))/normal.magnitude();

                //brightness = (float)Math.Cos(brightness);

                if(brightness< 0)
                {
                    brightness = 0;
                }

                brightness += 0.2f;

                if(brightness > 1)
                {
                    brightness = 1;
                }

                if(brightness > lighting[inds[i*3] *3] )
                {

                    lighting[inds[i * 3] * 3] = brightness;
                    lighting[inds[i * 3] * 3+1] = brightness;
                    lighting[inds[i * 3] * 3+2] = brightness;

                }

                if (brightness > lighting[inds[i * 3 + 1] * 3])
                {

                    lighting[inds[i * 3+1] * 3] = brightness;
                    lighting[inds[i * 3+1] * 3+1] = brightness;
                    lighting[inds[i * 3+1] * 3+2] = brightness;

                }

                if (brightness > lighting[inds[i * 3 + 2] * 3])
                {

                    lighting[inds[i * 3 + 2] * 3] = brightness;
                    lighting[inds[i * 3 + 2] * 3 + 1] = brightness;
                    lighting[inds[i * 3 + 2] * 3 + 2] = brightness;

                }
                /*vertices[inds[i * 3] * 6+3] *= brightness;
                vertices[inds[i * 3] * 6+4] *= brightness;
                vertices[inds[i * 3] * 6+5] *= brightness;


                vertices[inds[i * 3 + 1] * 6 + 3] *= brightness;
                vertices[inds[i * 3 + 1] * 6 + 4] *= brightness;
                vertices[inds[i * 3 + 1] * 6 + 5] *= brightness;


                vertices[inds[i * 3 + 2] * 6 + 3] *= brightness;
                vertices[inds[i * 3 + 2] * 6 + 4] *= brightness;
                vertices[inds[i * 3 + 2] * 6 + 5] *= brightness;*/

            }


            for (int i = 0; i < lighting.Count()/3; i++)
            {
                vertices[i*6 +3] *= lighting[i*3];
                vertices[i*6 + 4] *= lighting[i*3+1];
                vertices[i * 6 + 5] *= lighting[i*3 + 2];
            }

        }



        public static Matrix4 pro()
        {
            MY_vector3 forw = player.camforward();
            forw.x += player.pos.x; forw.y += player.pos.y;forw.z += player.pos.z;
            forw.normalise();
            //MY_vector3 right = MY_vector3.cross(forw, new MY_vector3(0,-1,0)).normalise();
            //MY_vector3 upp = MY_vector3.cross(right, forw);
            Matrix4 camer = Matrix4.LookAt(new Vector3(player.pos.x,player.pos.y,player.pos.z), new Vector3(forw.x, forw.y, forw.z), new Vector3(0,1,0));

            //camer.Transpose();
            if(player.zooooooom > Math.PI - 0.001f)
            {
                player.zooooooom = (float)Math.PI -0.001f;
            }
            else if(player.zooooooom < 0.0001f)
            {
                player.zooooooom = 0.0001f;
            }

            camer = camer * Matrix4.CreatePerspectiveFieldOfView(player.zooooooom, player.aspect, 0.01f, 20000f);
            return camer;
        }


        public static float[] projecte()
        {
            int e = Shape.shapes.Sum();
            vortex[] a = new vortex[e];
            int cur = 0;

            //Matrix4 camer = Matrix4.LookAt(new Vector3(player.pos.x, player.pos.y, player.pos.z), new Vector3(player.pos.x, player.pos.y, player.pos.z + 1), new Vector3(player.pos.x, player.pos.y - 1, player.pos.z));
            //camer.Transpose();

            //camer = camer * Matrix4.CreatePerspectiveFieldOfView(player.zooooooom, 1, 0.1f, 1000f);
            //camer.Transpose();

            /*MY_Matrix3 cam = new MY_Matrix3(new double[,]
            {


                {camer[0,0], camer[0,1],camer[0,2],camer[0,3] },
                {camer[1,0], camer[1,1],camer[1,2], camer[1,3] },
                {camer[2,0], camer[2,1], camer[2,2],  camer[2,3] },

                {camer[3,0], camer[3,1], camer[3,2], camer[3,3] },
                });*/


            //MY_Matrix3 spinna = MY_Matrix3.multi4(projection.GetProjectmat(), cam);

            for (int i = 0; i < Shape.Models.Count(); i++)
            { 
                MY_Matrix3 mov = MY_Matrix3.multi4(new MY_Matrix3(Shape.Models[i].centre), new MY_Matrix3(true,Shape.Models[i].angle));
                
                /*MY_Matrix3 moova = getTranslate(player);
                MY_Matrix3 spinna = getRotate(player);*/
                //spinna = MY_Matrix3.multi4(projection.GetProjectmat(), spinna);
                //spinna = MY_Matrix3.multi4(spinna, moova);




//              mov = MY_Matrix3.multi4(spinna, mov);
                for (int j = 0; j < Shape.Models[i].verts.Count(); j++)
                {
                    vortex ne = new vortex(mov.multiply(new MY_vector4(Shape.Models[i].verts[j].pos, 1)), Shape.Models[i].verts[j].color);
                    //ne = new vortex(mov.multiply(new MY_vector4(ne.pos, 1)), ne.color);
                    //Console.WriteLine(ne.pos.z/ne.w);
                    a[cur] = ne;
                    cur++;
                }
                //Console.WriteLine();
            }


            //MY_Matrix3 moova = getTranslate(player);
            //MY_Matrix3 spinna = getRotate (player);
            //spinna = MY_Matrix3.multi4(projection.GetProjectmat(), spinna);
            //spinna = MY_Matrix3.multi4(spinna, moova);


            

            float[] b = new float[a.Count()*6];
            for (int i = 0; i < a.Count(); i++)
            {
                b[i * 6] = a[i].pos.x;
                b[i * 6+1] = a[i].pos.y;
                b[i * 6+2] = a[i].pos.z;
                b[i * 6+3] = ((float)a[i].color.R/255); b[i * 6+4] = ((float)a[i].color.G / 255f); b[i * 6+5] = ((float)a[i].color.B / 255f);
            }
            //for (int i = 0; i < a.Count(); i++)
            //{
                //vortex c = new vortex(spinna.multiply(new MY_vector4(a[i].pos, 1)), a[i].color);
                //vortex c =new vortex(cam.multiply( new MY_vector4(a[i].pos, 1)), a[i].color);


                //b[i*7] = c.pos.x/c.w;
                //b[i*7+1] = c.pos.y/c.w;
                //b[i * 7 + 2] = c.pos.z / c.w;
                //if (c.pos.x < c.w && /*-c.pos.x > -c.w &&*/ c.pos.y < c.w /*&& -c.pos.y > -c.w */&& c.pos.z < c.w /*&& -c.pos.z > -c.w*/)
                /*{
                    
                }
                else
                {
                    b[i * 6] = 0;
                    b[i * 6 + 1] = 0;
                    b[i * 6 + 2] = 0;
                }*/
                //b[i * 7 + 3] = -c.w;

                //b[i*7+4] = ((float)c.color.R / 255); b[i*7+5] = ((float)c.color.G / 255); b[i*7+6] = ((float)c.color.B/255);

            //}

            return b;
        }
        class projection
        {
            static float near = 1f;
            static float far = 1000;
            static public float fovY = 0.0174533f*90;

            public static MY_Matrix3 perspective = new MY_Matrix3(new float[,]
            {/*
                {(1/((4/3f)*Math.Tan(fovY/2))),0,0,0},
                {0,(1/Math.Tan(fovY/2)),0,0 },
                {0,0,-(far+near)/(near-far),-(2*far*near)/(near-far)},
                {0,0,-1,0},*/                    
                    {(float)(1/((4F/4f)*Math.Tan(fovY/2))),0,0,0},
                    {0,(float)(1/Math.Tan(fovY/2)),0,0 },
                    {0,0,-(far+near)/(far-near),-(2*far*near)/(far-near)},
                    {0,0,1,0},
            });

            public static MY_Matrix3 GetProjectmat()
            {



                fovY = player.zooooooom;


                Matrix4 perspex = Matrix4.CreatePerspectiveFieldOfView(player.zooooooom, 1, 0.1f, 1000f);
                perspex.Transpose();

                perspective = new MY_Matrix3(new float[,]
                {

                    /*{2*near/(right-left),0,-(right+left)/(right-left),0},
                    {0,2*near/(bottom-top),-(bottom+top)/(bottom-top),0 },
                    {0,0,(far+near)/(far-near),(-2*far*near)/(far-near)},
                    {0,0,-1,0},*/


                    { perspex[0,0],perspex[0,1],perspex[0,2],perspex[0,3] },
                    { perspex[1,0],perspex[1,1],perspex[1,2],perspex[1,3] },
                    { perspex[2,0],perspex[2,1],perspex[2,2],perspex[2,3] },
                    { perspex[3,0],perspex[3,1],perspex[3,2],perspex[3,3] },

                    /* curr
                    {(1/Math.Tan(fovY/2)),0,0,0},
                    {0,(1/Math.Tan(fovY/2)),0,0 },
                    {0,0,far/(far-near),-(far*near)/(far-near)},
                    {0,0,-1,0},*/

                    /*
                    {1/Math.Tan(fovY/2),0,0,0},
                    {0,1/Math.Tan(fovY/2),0,0},
                    {0,0,(far+near)/(near-far),-1},
                    {0,0,(2*far*near)/(near-far),0},*/
                    /*
                    {Math.Tan(Math.PI * 0.5 - 0.5 * fovY)/(4/3), 0,0,0 },
                    {0,Math.Tan(Math.PI * 0.5 - 0.5 * fovY),0,0 },
                    {0,0,(near+far) * (1/(near - far)),near*far*1/(near-far) * 2 },
                    {0,0,-1,0 },*/

                    
                });
                return perspective;
            }


        }
        public static MY_Matrix3 getRotate(camera A)
        {
            MY_Matrix3 yaw = new MY_Matrix3(2, A.direction.z);
            yaw = MY_Matrix3.multi4(yaw, new MY_Matrix3(1, A.direction.y));
            yaw = MY_Matrix3.multi4(yaw, new MY_Matrix3(0, A.direction.x));
            //MY_Matrix3 yaw = new MY_Matrix3(0, A.pitch);
            /*yaw = MY_Matrix3.multi4(yaw, new MY_Matrix3(1, A.yaw));
            yaw = MY_Matrix3.multi4(yaw, new MY_Matrix3(2, A.roll));*/
            return yaw;
        }

    }

}
