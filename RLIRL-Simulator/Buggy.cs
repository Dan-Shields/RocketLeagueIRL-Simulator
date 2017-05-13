using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RLIRL_Simulator
{
    class Buggy
    {
        public static Texture2D texture;
        public static Vector2 size = new Vector2(35,42), origin = new Vector2(size.X / 2, size.Y / 3);
        public static Rectangle hitbox = new Rectangle(0, 0, (int) size.X, (int) size.Y);

        public Vector2 position;
        public float rotation;

        public Buggy(Vector2 startPosition, float startRotation = 0)
        {
            position = startPosition;
            rotation = startRotation;
        }

        private void Update(Vector2 moveVector, float rotAngle)
        {
            position = Vector2.Add(position, moveVector);
            rotation += rotAngle;

            if (position.X > Sim.windowHeight || position.X < 0 || position.Y > Sim.windowHeight ||
                position.Y < 0)
            {
                position = new Vector2(Sim.windowWidth / 2.0f,Sim.windowHeight / 2.0f);
            }

        }

        public void Move(int leftWheel, int rightWheel)
        {
            double rightWheelDecimal = rightWheel / 255.0;
            double leftWheelDecimal = leftWheel / 255.0;

            double magnitude = Math.Sqrt(Math.Pow(rightWheelDecimal, 2) + Math.Pow(leftWheelDecimal, 2));

            double angle;

            if (Math.Abs(leftWheelDecimal - rightWheelDecimal) > 0.001)
            {
                angle = ((leftWheelDecimal - rightWheelDecimal) / 255) * 20;
            }
            else
            {
                angle = 0;
            }

            float floatAngle = (float) angle;

            float newAngle = floatAngle + rotation;

            double yDistance = Math.Cos(newAngle) * -1 * magnitude;
            double xDistance = Math.Sin(newAngle) * magnitude;

            Vector2 moveVector = new Vector2((float)xDistance, (float)yDistance);

            Update(moveVector, floatAngle);
        }
    }
}
