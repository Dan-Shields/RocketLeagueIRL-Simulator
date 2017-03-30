using System;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RLIRL_Simulator
{
    class Buggy
    {
        public static Texture2D texture;
        public static Vector2 size = new Vector2(36,42), origin = new Vector2(size.X / 2, size.Y / 3);
        public static Rectangle hitbox = new Rectangle(0, 0, (int) size.X, (int) size.Y);

        public Vector2 leftWheelPosition;
        public Vector2 rightWheelPosition;
        public float rotation;

        public Buggy(Vector2 startPosition, float startRotation = 0)
        {
            leftWheelPosition = Vector2.Subtract(startPosition, new Vector2(18, 0));
            rightWheelPosition = Vector2.Add(startPosition, new Vector2(18, 0));
            rotation = startRotation;
        }

        public void MoveForward(float leftWheel, float rightWheel)
        {
            bool left = leftWheel < rightWheel;
            bool straight = Math.Abs(leftWheel - rightWheel) < 0.001;

            leftWheel *= 100;
            rightWheel *= 100;

            Vector2 a, b2, c2, u, du, v, r1, r2;
            Vector2 b1 = leftWheelPosition;
            Vector2 c1 = rightWheelPosition;
            float d, theta;

            if (left && !straight)
            {
                d = 1.0f /(1.0f-leftWheel/rightWheel) - 1.0f;
                v = Vector2.Subtract(rightWheelPosition, leftWheelPosition);
                
                u = new Vector2(v.X/36,v.Y/36); //36 is the current assumed separation of the wheels

                du = new Vector2(u.X*d,u.Y*d);

                a = Vector2.Add(b1, du);

                theta = rightWheel / (d + 36.0f);

                r1 = Vector2.Subtract(b1, a);
                if (r1 != Vector2.Zero)
                {
                    r2 = new Vector2((float) (Math.Pow(d, 2) * Math.Cos(theta)) / r1.X,
                        (float) (Math.Pow(d, 2) * Math.Cos(theta)) / r1.Y);
                }
                else
                {
                    r2 = Vector2.Zero;
                }

                b2 = Vector2.Subtract(r1, r2);

                leftWheelPosition = r1;
                rightWheelPosition = r2;

                Console.WriteLine("cos(" + theta + ") = " + Math.Cos(theta));

                r1 = Vector2.Subtract(c1, a);
                if (r1 != Vector2.Zero)
                {
                    r2 = new Vector2((float)(Math.Pow(d + 36.0, 2) * Math.Cos(theta)) / r1.X,
                        (float)(Math.Pow(d + 36.0, 2) * Math.Cos(theta)) / r1.Y);
                }
                else
                {
                    r2 = Vector2.Zero;
                }

                c2 = Vector2.Subtract(r1, r2);

                

                




            }
        }
    }
}
