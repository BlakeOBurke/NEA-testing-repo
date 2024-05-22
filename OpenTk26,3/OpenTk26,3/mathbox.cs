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
using System.Diagnostics;
using System.Numerics;
using System.Security.Permissions;
//using static System.Net.WebRequestMethods;

namespace OpenTk26_3
{
    internal class mathbox
    {
    }
    public class MY_vertex
    {
        public MY_vector3 pos;
        public Color color;
        public MY_vertex(float x, float y, float z, Color Colors)
        {
            this.pos = new MY_vector3(x, y, z);
            this.color = Colors;
        }
        public MY_vertex(MY_vector3 inp, Color Colors)
        {
            this.pos = inp;
            this.color = Colors;
        }

        public MY_vertex(MY_vector4 a, Color b)
        {
            this.pos = new MY_vector3(a);
            this.color = b;
        }

    }
    public class MY_Matrix3
    {
        public float[,] mat;

        public MY_vector4 multiply(MY_vector4 point)
        {
            if (this.mat.GetLength(0) == 4)
            {
                MY_vector4 result = new MY_vector4();
                result.x = (float)(this.mat[0, 0] * point.x + this.mat[0, 1] * point.y + this.mat[0, 2] * point.z + this.mat[0, 3] * point.w);
                result.y = (float)(this.mat[1, 0] * point.x + this.mat[1, 1] * point.y + this.mat[1, 2] * point.z + this.mat[1, 3] * point.w);
                result.z = (float)(this.mat[2, 0] * point.x + this.mat[2, 1] * point.y + this.mat[2, 2] * point.z + this.mat[2, 3] * point.w);
                result.w = (float)(this.mat[3, 0] * point.x + this.mat[3, 1] * point.y + this.mat[3, 2] * point.z + this.mat[3, 3] * point.w);
                return result;
            }
            else
            {
                MY_vector4 result = new MY_vector4();
                result.x = (float)(this.mat[0, 0] * point.x + this.mat[0, 1] * point.y + this.mat[0, 2] * point.z);
                result.y = (float)(this.mat[1, 0] * point.x + this.mat[1, 1] * point.y + this.mat[1, 2] * point.z);
                result.z = (float)(this.mat[2, 0] * point.x + this.mat[2, 1] * point.y + this.mat[2, 2] * point.z);
                result.w = 1;
                return result;
            }
        }

        public static MY_Matrix3 multi4(MY_Matrix3 first, MY_Matrix3 second)
        {
            MY_Matrix3 result = new MY_Matrix3();
            result.mat = new float[4, 4];
            result.mat[0, 0] = first.mat[0, 0] * second.mat[0, 0] + first.mat[0, 1] * second.mat[1, 0] + first.mat[0, 2] * second.mat[2, 0] + first.mat[0, 3] * second.mat[3, 0];
            result.mat[0, 1] = first.mat[0, 0] * second.mat[0, 1] + first.mat[0, 1] * second.mat[1, 1] + first.mat[0, 2] * second.mat[2, 1] + first.mat[0, 3] * second.mat[3, 1];
            result.mat[0, 2] = first.mat[0, 0] * second.mat[0, 2] + first.mat[0, 1] * second.mat[1, 2] + first.mat[0, 2] * second.mat[2, 2] + first.mat[0, 3] * second.mat[3, 2];
            result.mat[0, 3] = first.mat[0, 0] * second.mat[0, 3] + first.mat[0, 1] * second.mat[1, 3] + first.mat[0, 2] * second.mat[2, 3] + first.mat[0, 3] * second.mat[3, 3];

            result.mat[1, 0] = first.mat[1, 0] * second.mat[0, 0] + first.mat[1, 1] * second.mat[1, 0] + first.mat[1, 2] * second.mat[2, 0] + first.mat[1, 3] * second.mat[3, 0];
            result.mat[1, 1] = first.mat[1, 0] * second.mat[0, 1] + first.mat[1, 1] * second.mat[1, 1] + first.mat[1, 2] * second.mat[2, 1] + first.mat[1, 3] * second.mat[3, 1];
            result.mat[1, 2] = first.mat[1, 0] * second.mat[0, 2] + first.mat[1, 1] * second.mat[1, 2] + first.mat[1, 2] * second.mat[2, 2] + first.mat[1, 3] * second.mat[3, 2];
            result.mat[1, 3] = first.mat[1, 0] * second.mat[0, 3] + first.mat[1, 1] * second.mat[1, 3] + first.mat[1, 2] * second.mat[2, 3] + first.mat[1, 3] * second.mat[3, 3];

            result.mat[2, 0] = first.mat[2, 0] * second.mat[0, 0] + first.mat[2, 1] * second.mat[1, 0] + first.mat[2, 2] * second.mat[2, 0] + first.mat[2, 3] * second.mat[3, 0];
            result.mat[2, 1] = first.mat[2, 0] * second.mat[0, 1] + first.mat[2, 1] * second.mat[1, 1] + first.mat[2, 2] * second.mat[2, 1] + first.mat[2, 3] * second.mat[3, 1];
            result.mat[2, 2] = first.mat[2, 0] * second.mat[0, 2] + first.mat[2, 1] * second.mat[1, 2] + first.mat[2, 2] * second.mat[2, 2] + first.mat[2, 3] * second.mat[3, 2];
            result.mat[2, 3] = first.mat[2, 0] * second.mat[0, 3] + first.mat[2, 1] * second.mat[1, 3] + first.mat[2, 2] * second.mat[2, 3] + first.mat[2, 3] * second.mat[3, 3];

            result.mat[3, 0] = first.mat[3, 0] * second.mat[0, 0] + first.mat[3, 1] * second.mat[1, 0] + first.mat[3, 2] * second.mat[2, 0] + first.mat[3, 3] * second.mat[3, 0];
            result.mat[3, 1] = first.mat[3, 0] * second.mat[0, 1] + first.mat[3, 1] * second.mat[1, 1] + first.mat[3, 2] * second.mat[2, 1] + first.mat[3, 3] * second.mat[3, 1];
            result.mat[3, 2] = first.mat[3, 0] * second.mat[0, 2] + first.mat[3, 1] * second.mat[1, 2] + first.mat[3, 2] * second.mat[2, 2] + first.mat[3, 3] * second.mat[3, 2];
            result.mat[3, 3] = first.mat[3, 0] * second.mat[0, 3] + first.mat[3, 1] * second.mat[1, 3] + first.mat[3, 2] * second.mat[2, 3] + first.mat[3, 3] * second.mat[3, 3];
            return result;
        }
        public MY_Matrix3()
        {

        }
        /// <summary>
        /// make a matrix based on a 2d array
        /// </summary>
        /// <param name="AAAA"></param>
        public MY_Matrix3(float[,] AAAA)
        {
            this.mat = AAAA;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dimension">0,1,2 x,y,z</param>
        /// <param name="angle">angle in radians to rotate by</param>
        public MY_Matrix3(int dimension, double angle)
        {
            if (dimension == 0)
            {
                //x
                this.mat = new float[,]{
                        {1,0,0,0 },
                        {0,(float)Math.Cos(angle),(float)-Math.Sin(angle),0 },
                        {0,(float)Math.Sin(angle),(float) Math.Cos(angle),0 },
                        {0,0,0,1 }
                    };
            }
            else if (dimension == 1)
            {
                //y
                this.mat = new float[,]
                {
                        {(float) Math.Cos(angle),0,(float) Math.Sin(angle),0 },
                        {0,1,0,0 },
                        {(float) -Math.Sin(angle),0,(float) Math.Cos(angle),0},
                        {0,0,0,1 }
                };
            }
            else
            {
                //z
                this.mat = new float[,]
                {
                        {(float) Math.Cos(angle),(float) -Math.Sin(angle),0, 0 },
                        {(float) Math.Sin(angle), (float) Math.Cos(angle),0,0 },
                        {0,0,1,0 },
                        {0,0,0,1}
                };

            }
        }
        /// <summary>
        /// translation matrix from 3 floats
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public MY_Matrix3(float x, float y, float z)
        {
            this.mat = new float[,] { { 1, 0, 0, x }, { 0, 1, 0, y }, { 0, 0, 1, z }, { 0, 0, 0, 1 } };
        }
        /// <summary>
        /// translation matrix from a vector3
        /// </summary>
        /// <param name="a"></param>
        public MY_Matrix3(MY_vector3 a)
        {
            this.mat = new float[,] { { 1, 0, 0, a.x }, { 0, 1, 0, a.y }, { 0, 0, 1, a.z }, { 0, 0, 0, 1 } };
        }
        /// <summary>
        /// negative translation matrix from a vector3
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public MY_Matrix3(MY_vector3 a, bool b)
        {
            this.mat = new float[,] { { 1, 0, 0, -a.x }, { 0, 1, 0, -a.y }, { 0, 0, 1, -a.z }, { 0, 0, 0, 1 } };
        }
        /// <summary>
        /// rotation based on a vector3
        /// </summary>
        /// <param name="b"></param>
        /// <param name="a"></param>
        public MY_Matrix3(bool b, MY_vector3 a)
        {
            /*this.mat = new double[,] {
                { Math.Cos(a.y)*Math.Cos(a.z), Math.Sin(a.x)*Math.Sin(a.y)*Math.Cos(a.z) - Math.Cos(a.x)*Math.Sin(a.z), Math.Cos(a.x)*Math.Sin(a.y)*Math.Cos(a.z) + Math.Sin(a.x)*Math.Sin(a.z), 0},
                {Math.Cos(a.y)*Math.Cos(a.z), Math.Sin(a.x)*Math.Sin(a.y)*Math.Sin(a.z) + Math.Cos(a.x)*Math.Cos(a.z), Math.Cos(a.x)*Math.Sin(a.y)*Math.Sin(a.z) - Math.Sin(a.x)*Math.Cos(a.z),0},
                {-Math.Sin(a.y), Math.Sin(a.x)*Math.Cos(a.y),Math.Cos(a.x)*Math.Cos(a.y),0  },
                {0,0,0,1 }
            };*/
            MY_Matrix3 c = new MY_Matrix3(0, a.x);
            c = MY_Matrix3.multi4(c, new MY_Matrix3(1, a.y));
            c = MY_Matrix3.multi4(c, new MY_Matrix3(2, a.z));
            this.mat = c.mat;

        }
        /// <summary>
        /// negative rotation based on a vector3
        /// </summary>
        /// <param name="b"></param>
        /// <param name="d"></param>
        /// <param name="a"></param>
        public MY_Matrix3(bool b, bool d, MY_vector3 a)
        {
            /*a = new MY_vector3(-a.x,-a.y, -a.z);
            this.mat = new double[,] {
                { Math.Cos(a.y)*Math.Cos(a.z), Math.Sin(a.x)*Math.Sin(a.y)*Math.Cos(a.z) - Math.Cos(a.x)*Math.Sin(a.z), Math.Cos(a.x)*Math.Sin(a.y)*Math.Cos(a.z) + Math.Sin(a.x)*Math.Sin(a.z), 0},
                {Math.Cos(a.y)*Math.Cos(a.z), Math.Sin(a.x)*Math.Sin(a.y)*Math.Sin(a.z) + Math.Cos(a.x)*Math.Cos(a.z), Math.Cos(a.x)*Math.Sin(a.y)*Math.Sin(a.z) - Math.Sin(a.x)*Math.Cos(a.z),0},
                {-Math.Sin(a.y), Math.Sin(a.x)*Math.Cos(a.y),Math.Cos(a.x)*Math.Cos(a.y),0  },
                {0,0,0,1 }
            };*/
            MY_Matrix3 c = new MY_Matrix3(0, -a.x);
            c = MY_Matrix3.multi4(c, new MY_Matrix3(1, -a.y));
            c = MY_Matrix3.multi4(c, new MY_Matrix3(2, -a.z));
            this.mat = c.mat;

        }
        public MY_Matrix3(bool b,bool zz,bool zzz, MY_vector3 a)
        {
            /*this.mat = new double[,] {
                { Math.Cos(a.y)*Math.Cos(a.z), Math.Sin(a.x)*Math.Sin(a.y)*Math.Cos(a.z) - Math.Cos(a.x)*Math.Sin(a.z), Math.Cos(a.x)*Math.Sin(a.y)*Math.Cos(a.z) + Math.Sin(a.x)*Math.Sin(a.z), 0},
                {Math.Cos(a.y)*Math.Cos(a.z), Math.Sin(a.x)*Math.Sin(a.y)*Math.Sin(a.z) + Math.Cos(a.x)*Math.Cos(a.z), Math.Cos(a.x)*Math.Sin(a.y)*Math.Sin(a.z) - Math.Sin(a.x)*Math.Cos(a.z),0},
                {-Math.Sin(a.y), Math.Sin(a.x)*Math.Cos(a.y),Math.Cos(a.x)*Math.Cos(a.y),0  },
                {0,0,0,1 }
            };*/
            MY_Matrix3 c = new MY_Matrix3(2, a.z);
            c = MY_Matrix3.multi4(c, new MY_Matrix3(1, a.y));
            c = MY_Matrix3.multi4(c, new MY_Matrix3(0, a.x));
            this.mat = c.mat;

        }
        /// <summary>
        /// create a scale matrix
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="a"></param>
        public MY_Matrix3(float x, float y, float z, bool a)
        {
            mat = new float[,] { { x, 0, 0, 0 }, { 0, y, 0, 0 }, { 0, 0, z, 0 }, { 0, 0, 0, 1 } };
        }




    }

    public class MY_vector3
    {
        public float x, y, z;
        public MY_vector3(float X, float Y, float Z)
        {
            this.x = X;
            this.y = Y;
            this.z = Z;
        }
        public MY_vector3()
        {
            /*this.x = 0;
            this.y = 0;
            this.z = 0;*/
        }

        public MY_vector3(MY_vector4 a)
        {
            this.x = a.x;
            this.y = a.y;
            this.z = a.z;
        }
        public MY_vector3 normalise()
        {
            double m = Math.Sqrt(z * z + x * x + y * y);
            return new MY_vector3((float)(x / m), (float)(y / m), (float)(z / m));
        }

        public static float dot(MY_vector3 a, MY_vector3 b)
        {
            return a.x * b.x + a.y + b.y + a.z + b.z;
        }
        public static MY_vector3 cross(MY_vector3 a, MY_vector3 b)
        {
            return new MY_vector3(a.y * b.z - b.y * a.z, a.z * b.x - a.x * b.z, a.x * b.y - b.x * a.y);
        }
        public MY_vector3 sum(MY_vector3 a)
        {
            return new MY_vector3(x + a.x, y + a.y, z + a.z);
        }

        public float magnitude()
        {
            return (float)Math.Sqrt(x * x + y * y + z * z);
        }
        public MY_vector3 Times(float a)
        {
            return new MY_vector3(x*a, y*a, z*a);
        }
        public MY_vector3 sub(MY_vector3 a)
        {
            return new MY_vector3(x-a.x, y-a.y, z-a.z);
        }
    }

    public class MY_vector4 : MY_vector3
    {
        public float w;
        public MY_vector4(float x, float y, float z, float w) : base(x, y, z)
        {
            this.w = w;
        }
        public MY_vector4()
        {

        }
        public MY_vector4(MY_vector3 a, float w)
        {
            this.x = a.x;
            this.y = a.y;
            this.z = a.z;
            this.w = w;
        }
        /*public vector4 normalises()
        {

        }*/
    }

    public class camera
    {
        public MY_vector3 pos;
        public MY_vector3 direction;
        //public MY_vector3 right;
        //public MY_vector3 up;
        public MY_vector3 forward;
        public float aspect;
        public float zooooooom;
        public float View_distance;
        public camera(float x, float y, float z)
        {
            this.pos = new MY_vector3 (x, y, z);
            this.direction = new MY_vector3(0, 0, 0); 
            this.zooooooom = (float)(0.0174533 * 90);
            forward = new MY_vector3(0, 0, 1);
            aspect = 1.33333f;
            View_distance = 1f;
        }
        /*public MY_Matrix3 getcam(camera A)
        {
            right = MY_vector3.cross(forward, up).normalise();
            MY_vector3 upp = MY_vector3.cross(right,forward);


            return new MY_Matrix3(new double[,]
            {
                    {A.right.x,upp.x,-A.forward.x,A.pos.x },
                    {A.right.y,upp.y,-A.forward.y,A.pos.y },
                    {A.right.z,upp.z,-A.forward.z,A.pos.z },
                    {0,0,0,1 }  
            });
        }*/
        public MY_vector3 camforward()
        {
            MY_Matrix3 rotation = new MY_Matrix3(true,true,true,this.direction);
            MY_vector4 ouut = rotation.multiply(new MY_vector4(forward,1));
            return new MY_vector3(ouut);
        }
    }
    public interface IShape
    {

    }
    public class Shape
    {
        public MY_vector3 angle;
        public int count;

        public MY_vertex[] verts;
        public uint[] triangle;

        public MY_vector3 centre;

        public static List<int> shapes = new List<int>();
        public static List<Shape> Models = new List<Shape>();

        /// <summary>
        /// make a shape with a .ast file format
        /// </summary>
        /// <param name="verts"></param>
        public Shape(MY_vertex[] verts)
        {
            //this.index = Game..Count();
            this.verts = verts /*new MY_vertex[] { new MY_vertex(new MY_vector3(1, 1, 4), Color.Red) ,new MY_vertex(new MY_vector3(-1, -1, 1), Color.Blue), new MY_vertex(new MY_vector3(0, 1, 1), Color.Green) }*/;
            this.triangle = new uint[this.verts.Length];

            for (int i = 0; i < this.verts.Length; i++)
            {
                triangle[i] = (uint) (i);
            }
            this.centre = avgPos();
            this.count = this.verts.Count();
            //addverts(verts);
            this.angle = new MY_vector3(0, 0, 0);

            //shapes.Add(index);

            mooov2(centre, angle);

            this.centre = avgPos();
            shapes.Add(count);
        }

        /// <summary>
        /// create a shape from a .obj file
        /// </summary>
        /// <param name="path"></param>
        public Shape(string path)
        {

            string[] inp = File.ReadAllLines(path);

            List<MY_vertex> ver = new List<MY_vertex>();

            List<uint> tria = new List<uint>();


            //MY_vector3 col = new MY_vector3(Game.rnd.Next(100, 205), Game.rnd.Next(100, 205), Game.rnd.Next(100, 205));
            MY_vector3 col = new MY_vector3(Game.rnd.Next(50, 205), Game.rnd.Next(50, 205), Game.rnd.Next(50, 205));
            //MY_vector3 col = new MY_vector3(50, 205, 205);

            for (int i = 0; i < inp.Count(); i++)
            {
                if (inp[i].Substring(0, 2) == "v ")
                {
                    string[] point = inp[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    ver.Add(new MY_vertex(new MY_vector3(float.Parse(point[1]), float.Parse(point[2]), float.Parse(point[3])), Color.FromArgb(50, ((int)col.x + Game.rnd.Next(-50, 50)), ((int)col.y + Game.rnd.Next(-50, 50)), ((int)col.z + Game.rnd.Next(-50, 50)))));
                    //ver.Add(new MY_vertex(new MY_vector3(float.Parse(point[1]), float.Parse(point[2]), float.Parse(point[3])), Color.FromArgb(50, (int)col.x, (int)col.y, (int)col.z)));
                }
                else if (inp[i].Substring(0,2) == "f ")
                {
                    string[] point = inp[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    tria.Add((uint)(int.Parse(point[1].Split('/')[0]) -1));
                    tria.Add((uint)(int.Parse(point[2].Split('/')[0]) -1));
                    tria.Add((uint)(int.Parse(point[3].Split('/')[0]) -1));
                }
            }

            //this.index = Game..Count();
            this.verts = ver.ToArray();
            this.triangle = tria.ToArray();


            this.centre = avgPos();
            this.count = this.verts.Count();
            //addverts(verts);
            this.angle = new MY_vector3(0, 0, 0);

            //shapes.Add(index);

            mooov2(centre, angle);

            this.centre = avgPos();
            shapes.Add(count);
        }

        public Shape(string path, MY_vector3 color)
        {

            string[] inp = File.ReadAllLines(path);

            List<MY_vertex> ver = new List<MY_vertex>();

            List<uint> tria = new List<uint>();


            //MY_vector3 col = new MY_vector3(Game.rnd.Next(100, 205), Game.rnd.Next(100, 205), Game.rnd.Next(100, 205));
            MY_vector3 col = color;
            //MY_vector3 col = new MY_vector3(50, 205, 205);

            for (int i = 0; i < inp.Count(); i++)
            {
                if (inp[i].Substring(0, 2) == "v ")
                {
                    string[] point = inp[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    ver.Add(new MY_vertex(new MY_vector3(float.Parse(point[1]), float.Parse(point[2]), float.Parse(point[3])), Color.FromArgb(50, ((int)col.x /*+ Game.rnd.Next(-50, 50)*/), ((int)col.y /*+ Game.rnd.Next(-50, 50)*/), ((int)col.z /*+ Game.rnd.Next(-50, 50)*/))));
                }
                else if (inp[i].Substring(0, 2) == "f ")
                {
                    string[] point = inp[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    tria.Add((uint)(int.Parse(point[1].Split('/')[0]) - 1));
                    tria.Add((uint)(int.Parse(point[2].Split('/')[0]) - 1));
                    tria.Add((uint)(int.Parse(point[3].Split('/')[0]) - 1));
                }
            }

            //this.index = Game..Count();
            this.verts = ver.ToArray();
            this.triangle = tria.ToArray();


            this.centre = avgPos();
            this.count = this.verts.Count();
            //addverts(verts);
            this.angle = new MY_vector3(0, 0, 0);

            //shapes.Add(index);

            mooov2(centre, angle);

            this.centre = avgPos();
            shapes.Add(count);
        }



        MY_vector3 avgPos()
        {
            double x = 0; double y = 0; double z = 0;
            for (int i = 0; i < this.verts.Count(); i++)
            {
                x += this.verts[i].pos.x;
                y += this.verts[i].pos.y;
                z += this.verts[i].pos.z;
            }

            return new MY_vector3((float)(x / this.verts.Count()), (float)(y / this.verts.Count()), (float)(z / this.verts.Count()));
        }
        public void mooova(MY_vector3 movement, MY_vector3 rotation)
        {
            /*
            MY_Matrix3 moova1 = new MY_Matrix3(centre);
            MY_Matrix3 moova2 = new MY_Matrix3(new MY_vector3(centre.x, centre.y, centre.z), true);
            MY_Matrix3 moverr = new MY_Matrix3(true, rotation);

            moverr = MY_Matrix3.multi4(moova1, moverr);
            moverr = MY_Matrix3.multi4(moverr, moova2);*/

            this.angle.x += rotation.x;
            this.angle.y += rotation.y;
            this.angle.z += rotation.z;

            if (angle.x > 2 * Math.PI)
            {
                angle.x -= (float)(2 * Math.PI);
            }
            if (angle.x < 0)
            {
                angle.x += (float)(2 * Math.PI);
            }
            if (angle.y > 2 * Math.PI)
            {
                angle.y -= (float)(2 * Math.PI);
            }
            if (angle.y < 0)
            {
                angle.y += (float)(2 * Math.PI);
            }
            if (angle.z > 2 * Math.PI)
            {
                angle.z -= (float)(2 * Math.PI);
            }
            if (angle.z < 0)
            {
                angle.z += (float)(2 * Math.PI);
            }

            /*
            for (int i = 0; i < count; i++)
            {
                //mY_Vertices[i] = new MY_vertex (moverr.multiply(new MY_vector4(mY_Vertices[i].pos.x, mY_Vertices[i].pos.y, mY_Vertices[i].pos.z, 1)), mY_Vertices[i].color);
                verts[i] = new MY_vertex(moverr.multiply(new MY_vector4(verts[i].pos.x, verts[i].pos.y, verts[i].pos.z, 1)), verts[i].color);
            }
            */
            this.centre.x += movement.x;
            this.centre.y += movement.y; 
            this.centre.z += movement.z;
            //
            //this.centre = avgPos();
        }
        public void mooov2(MY_vector3 movement, MY_vector3 rotation)
        {
            MY_Matrix3 moova1 = new MY_Matrix3(centre);
            MY_Matrix3 moova2 = new MY_Matrix3(new MY_vector3(centre.x+movement.x, centre.y + movement.y, centre.z + movement.z), true);
            MY_Matrix3 moverr = new MY_Matrix3(true,true,true, rotation);


            moverr = MY_Matrix3.multi4(moova1, moverr);
            moverr = MY_Matrix3.multi4(moverr, moova2);

            this.angle.x += rotation.x;
            this.angle.y += rotation.y;
            this.angle.z += rotation.z;

            if (angle.x > 2 * Math.PI)
            {
                angle.x -= (float)(2 * Math.PI);
            }
            if (angle.x < 0)
            {
                angle.x += (float)(2 * Math.PI);
            }
            if (angle.y > 2 * Math.PI)
            {
                angle.y -= (float)(2 * Math.PI);
            }
            if (angle.y < 0)
            {
                angle.y += (float)(2 * Math.PI);
            }
            if (angle.z > 2 * Math.PI)
            {
                angle.z -= (float)(2 * Math.PI);
            }
            if (angle.z < 0)
            {
                angle.z += (float)(2 * Math.PI);
            }


            for (int i = 0; i < count; i++)
            {
                //mY_Vertices[i] = new MY_vertex (moverr.multiply(new MY_vector4(mY_Vertices[i].pos.x, mY_Vertices[i].pos.y, mY_Vertices[i].pos.z, 1)), mY_Vertices[i].color);
                verts[i] = new MY_vertex(moverr.multiply(new MY_vector4(verts[i].pos.x, verts[i].pos.y, verts[i].pos.z, 1)), verts[i].color);
            }

            this.centre.x += movement.x;
            this.centre.y += movement.y;
            this.centre.z += movement.z;
            //
            //= avgPos();
        }

        public void scala(MY_vector3 scale)
        {
            MY_Matrix3 moverr = new MY_Matrix3(scale.x, scale.y, scale.z,true);
            for (int i = 0; i < count; i++)
            {
                //mY_Vertices[i] = new MY_vertex (moverr.multiply(new MY_vector4(mY_Vertices[i].pos.x, mY_Vertices[i].pos.y, mY_Vertices[i].pos.z, 1)), mY_Vertices[i].color);
                verts[i] = new MY_vertex(moverr.multiply(new MY_vector4(verts[i].pos.x, verts[i].pos.y, verts[i].pos.z, 1)), verts[i].color);
            }

            this.centre = avgPos();
        }

        public float[] GetFloat()
        {
            float[] result = new float[count*6];
            for (int i = 0; i < count;i++)
            {
                result[i * 6] = verts[i].pos.x;
                result[i * 6 + 1] = verts[i].pos.y /*+ (float)Math.Sin(verts[i].pos.x + verts[i].pos.z + Game.stopstopstop.Elapsed.TotalMilliseconds/1000f);/*+ 150 * (float)Math.Sin(verts[i].pos.x + verts[i].pos.z + Game.stopstopstop.Elapsed.TotalMilliseconds/(1000/6f))*/;
                result[i * 6 + 2] = verts[i].pos.z;
                result[i * 6 + 3] = verts[i].color.R/255f;
                result[i * 6 + 4] = verts[i].color.G/255f;
                result[i * 6 + 5] = verts[i].color.B/255f;
            }

            return result;
        }
    }

    public class Terrain
    {
        public float[,] heights;
        public Shape terrain;
        public int shapeIndex;

        public Terrain(Shape terra, int index, int x, int y)
        {
            this.heights = new float[x, y];
            for (int i = 0; i < terra.verts.Count(); i++)
            {
                terra.verts[i].pos.y += 2f * ((float)Game.rnd.NextDouble() - 0.5f);
            }
            this.terrain = terra;
            this.shapeIndex = index;
            Shape.Models[shapeIndex] = this.terrain;
        }
        public Terrain(Shape terra, int index, float x, float y, bool UNUSED)
        {
            this.heights = new float[256, 256];

            this.heights = Perlin.DoPerlin(heights, x, y);

            Color colss = Color.LimeGreen;

            for (int i = 0; i < heights.GetLength(0); i++)
            {
                for (int j = 0; j < heights.GetLength(1); j++)
                {
                    terra.verts[i * 256 + j].pos.y = 150*heights[i, j];
                    /*if(heights[i, j] < 0.1f)
                    {
                        terra.verts[i * 256 + j].color = Color.Maroon;
                    }
                    else if (heights[i, j] < 0.2f)
                    {
                        terra.verts[i * 256 + j].color = Color.Red;
                    }
                    else if (heights[i, j] < 0.3f)
                    {
                        terra.verts[i * 256 + j].color = Color.DarkOrange;
                    }
                    else if (heights[i, j] < 0.4f)
                    {
                        terra.verts[i * 256 + j].color = Color.Orange;
                    }
                    else if (heights[i, j] < 0.5f)
                    {
                        terra.verts[i * 256 + j].color = Color.Yellow;
                    }
                    else if (heights[i, j] < 0.6f)
                    {
                        terra.verts[i * 256 + j].color = Color.YellowGreen;
                    }
                    else if (heights[i, j] < 0.7f)
                    {
                        terra.verts[i * 256 + j].color = Color.Green;
                    }
                    else if (heights[i, j] < 0.8f)
                    {
                        terra.verts[i * 256 + j].color = Color.DarkGreen;
                    }
                    else if (heights[i, j] < 0.9f)
                    {
                        terra.verts[i * 256 + j].color = Color.Blue;
                    }
                    else
                    {
                        terra.verts[i * 256 + j].color = Color.Violet;
                    }*/

                    Color top = Color.Blue;
                    Color bottom = Color.Tan;


                    var rAverage = top.R + (int)((bottom.R - top.R) *heights[i,j]);
                    var gAverage = top.G + (int)((bottom.G - top.G) *heights[i,j]);
                    var bAverage = top.B + (int)((bottom.B - top.B) *heights[i,j]);

                    terra.verts[i*256+j].color = Color.FromArgb(255, Math.Abs(rAverage-20+Game.rnd.Next(0,20)), Math.Abs(gAverage-20+Game.rnd.Next(0,20)), Math.Abs(bAverage-20+Game.rnd.Next(0,20)));
                    //terra.verts[i*256+j].color = Color.FromArgb(255, colss.R + Game.rnd.Next(0,20), colss.G - Game.rnd.Next(0,30), colss.B + Game.rnd.Next(0,20));
                    //var gAverage = gMin + (int)((gMax - gMin) * i / size);
                    //var bAverage = bMin + (int)((bMax - bMin) * i / size);
                    //terra.verts[i * 256 + j].color = Color.FromArgb(255, (int)(150 * heights[i, j])+55, (int)(200 * (1 - heights[i, j]))+55, 50);
                }
            }

            /*for (int i = 0; i < terra.verts.Count(); i++)
            {
                terra.verts[i].pos.y += heights[i % 257, i/257];
            }*/


            this.terrain = terra;
            this.shapeIndex = index;
            Shape.Models[shapeIndex] = this.terrain;
        }

        /*public float perlin(float x, float y)
        {
            int x0 = (int)Math.Floor(x);
            int x1 = x0 + 1;
            int y0 = (int)Math.Floor(y);
            int y1 = y0 + 1;

            float sx = x - (float)x0;
            float sy = y - (float)y0;

            float n0, n1, ix0, ix1, value;

            n0 = dotGridGradient(x0, y0, x, y);
            n1 = dotGridGradient(x1, y0, x, y);
            ix0 = interpolate(n0, n1, sx);

            n0 = dotGridGradient(x0, y1, x, y);
            n1 = dotGridGradient(x1, y1, x, y);
            ix1 = interpolate(n0, n1, sx);

            value = interpolate(ix0, ix1, sy);
            return value * 0.5f + 0.49f;
        }
        public float dotGridGradient(int ix, int iy, float x, float y)
        {
            float[] gradient = randomGradient(ix, iy);

            float dx = x - (float)ix;
            float dy = y - (float)iy;

            return dx * gradient[0] + dy * gradient[1];
        }

        
        public float interpolate(float a0, float a1, float w)
        {
            //if (0.0 > w) return a0;
            //if (1.0 < w) return a1;
            //return (a1 - a0) * w + a0;
            return (float)((a1 - a0) * ((w * (w * 6.0 - 15.0) + 10.0) * w * w * w) + a0);
        }
        public float[] randomGradient(int ix, int iy)
        {
            const uint w = 8 * sizeof(uint);
            const uint s = w / 2; // rotation width
            uint a = (uint)ix, b = (uint)iy;
            a *= 3284157443; b ^= (uint)((int)a << (int)s | (int)a >> (int)(w-s));


            b *= 1911520717; a ^= (uint)((int)b << (int)s | (int)b >> (int)(w - s));
            a *= 2048419325;

            float random = (float)(a * (3.14159265 / ~(~0u >> 1))); // in [0, 2*Pi]#


            random = Game.rnd.Next(0, 360);
            return new float[] { (float)Math.Cos(random), (float)Math.Sin(random) };

        }*/
    }

    public class Perlin
    {

        public static float[,] DoPerlin(float[,] c, float Ox, float Oy)
        {
            float[,] a = new float[c.GetLength(0), c.GetLength(1)];
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    //a[i, j] = (float)rnd.NextDouble();
                    //a[i, j] = (perlin(i * 256/237f + O, j * 256/63f + O)) + 0.5f*(perlin(i*256/(237*0.5f) + O, j*256/(63*0.5f) + O)) + 0.25f*(perlin(i*256/(237*0.25f) + O,j*256/(63*0.25f) + O)) ;
                    float x = Ox + i / (float)a.GetLength(0);
                    float y = Oy + j / (float)a.GetLength(1);

                    //a[i, j] = perlin(x,y) /*+ 0.5f*perlin(x2,y2) + 0.25f*perlin(x3,y3)*/; 
                    //a[i, j] = (float)Math.Pow(0.5,a[i, j]);
                    a[i, j] = EvaluateFBM(x, y, 1, 1, 3, 0.5f, 2f);
                }
            }

            float max = 0;
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    if (a[i, j] > max)
                    {
                        max = a[i, j];
                    }
                }
            }
            float min = 1;
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    if (a[i, j] < min)
                    {
                        min = a[i, j];
                    }
                }
            }

            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    a[i, j] = (a[i, j] - min) / (max - min);
                }
            }

            return a;
        }
        static float EvaluateFBM(float x, float y, float amplitude, float frequency, int octaveCount, float persistence, float lacunarity)
        {
            float value = 0;

            for (int i = 0; i < octaveCount; i++)
            {
                value += amplitude * perlin(x * frequency, y * frequency);
                amplitude *= persistence;
                frequency *= lacunarity;
            }
            return value;
        }

        static float interpolate(float a0, float a1, float w)
        {
            if (0.0 > w) return a0;
            if (1.0 < w) return a1;

            return (a1 - a0) * ((w * (w * 6.0f - 15.0f) + 10.0f) * w * w * w) + a0;
            //return (a1 - a0) * w + a0;
            /* // Use this cubic interpolation [[Smoothstep]] instead, for a smooth appearance:
             * return (a1 - a0) * (3.0 - w * 2.0) * w * w + a0;
             *
             * // Use [[Smootherstep]] for an even smoother result with a second derivative equal to zero on boundaries:
             * return (a1 - a0) * ((w * (w * 6.0 - 15.0) + 10.0) * w * w * w) + a0;
             */
        }

        static vector randomGradient(int ix, int iy)
        {
            // No precomputed gradients mean this works for any number of grid coordinates
            const long w = 8 * sizeof(long);
            const long s = w / 2; // rotation width
            long a = (long)ix, b = (long)iy;
            a *= 3284157443; b ^= (int)a << (int)s | (int)a >> (int)w - (int)s;
            b *= 1911520717; a ^= (int)b << (int)s | (int)b >> (int)w - (int)s;
            a *= 2048419325;
            float random = a * (float)(3.14159265 / ~(~0u >> 1)); // in [0, 2*Pi]
            vector v;
            v.x = (float)Math.Cos(random); v.y = (float)Math.Sin(random);
            return v;
        }

        static float dotGridGradient(int ix, int iy, float x, float y)
        {
            // Get gradient from integer coordinates
            vector gradient = randomGradient(ix, iy);

            // Compute the distance vector
            float dx = x - (float)ix;
            float dy = y - (float)iy;

            // Compute the dot-product
            return (dx * gradient.x + dy * gradient.y);
        }

        public static float perlin(float x, float y)
        {
            // Determine grid cell coordinates
            int x0 = (int)Math.Floor(x);
            int x1 = x0 + 1;
            int y0 = (int)Math.Floor(y);
            int y1 = y0 + 1;

            // Determine interpolation weights
            // Could also use higher order polynomial/s-curve here
            float sx = x - (float)x0;
            float sy = y - (float)y0;

            // Interpolate between grid point gradients
            float n0, n1, ix0, ix1, value;

            n0 = dotGridGradient(x0, y0, x, y);
            n1 = dotGridGradient(x1, y0, x, y);
            ix0 = interpolate(n0, n1, sx);

            n0 = dotGridGradient(x0, y1, x, y);
            n1 = dotGridGradient(x1, y1, x, y);
            ix1 = interpolate(n0, n1, sx);

            value = interpolate(ix0, ix1, sy);
            return value * 0.5f + 0.5f; // Will return in range -1 to 1. To make it in range 0 to 1, multiply by 0.5 and add 0.5
        }

        struct vector
        {
            public float x;
            public float y;
        }
    }

    public class Shader
    {
        int Handle;

        public Shader(string vertexPath, string fragmentPath)
        {
            int VertexShader, FragmentShader;
            string VertexShaderSource = File.ReadAllText(vertexPath);

            string FragmentShaderSource = File.ReadAllText(fragmentPath);

            VertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(VertexShader, VertexShaderSource);

            FragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(FragmentShader, FragmentShaderSource);


            GL.CompileShader(VertexShader);

            GL.GetShader(VertexShader, ShaderParameter.CompileStatus, out int success);
            if (success == 0)
            {
                string infoLog = GL.GetShaderInfoLog(VertexShader);
                Console.WriteLine(infoLog);
            }

            GL.CompileShader(FragmentShader);

            GL.GetShader(FragmentShader, ShaderParameter.CompileStatus, out int ssuccess);
            if (ssuccess == 0)
            {
                string infoLog = GL.GetShaderInfoLog(FragmentShader);
                Console.WriteLine(infoLog);
            }




            Handle = GL.CreateProgram();

            GL.AttachShader(Handle, VertexShader);
            GL.AttachShader(Handle, FragmentShader);

            GL.LinkProgram(Handle);

            GL.GetProgram(Handle, GetProgramParameterName.LinkStatus, out int sssuccess);
            if (sssuccess == 0)
            {
                string infoLog = GL.GetProgramInfoLog(Handle);
                Console.WriteLine(infoLog);
            }

            GL.DetachShader(Handle, VertexShader);
            GL.DetachShader(Handle, FragmentShader);
            GL.DeleteShader(FragmentShader);
            GL.DeleteShader(VertexShader);
        }
        public void Use()
        {
            GL.UseProgram(Handle);
        }
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                GL.DeleteProgram(Handle);

                disposedValue = true;
            }
        }

        ~Shader()
        {
            if (disposedValue == false)
            {
                Console.WriteLine("GPU Resource leak! Did you forget to call Dispose()?");
            }
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
