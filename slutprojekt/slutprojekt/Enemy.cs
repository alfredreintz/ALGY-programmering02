using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace slutprojekt;

abstract class Enemy : PhysicalObject
{
    public Enemy(Texture2D texture, float X, float Y, float speedX, float speedY) : base(texture, X, Y, speedX, speedY)
    {
    }

    public abstract void Update(GameWindow window);

    public bool IsAlive
    {
        get { return isAlive; }
        set { isAlive = value; }
    }
}

class horizontalBird : Enemy
{
    public horizontalBird(Texture2D texture, float X, float Y, float speedX, float speedY) : base(texture, X, Y, speedX, speedY)
    {
    }

    public override void Update(GameWindow window)
    {
        vector.X += speed.X;

        Random rand = new Random();
        
        if (vector.X > window.ClientBounds.Width && speed.X > 0 || vector.X < 0 - Width && speed.X < 0)
        {
            int randX1 = rand.Next(-2000, -500);
            int randX2 = rand.Next(window.ClientBounds.Width + 500, window.ClientBounds.Width + 2000);

            int randPos = rand.Next(0, 2);
            
            speed.X = 7f * rand.Next(1, 20) / 10;

            if (randPos == 0)
            {
                vector.X = randX1;
                speed.X = Math.Abs(speed.X);
            }
            else
            {
                vector.X = randX2;
                speed.X = -Math.Abs(speed.X);
            }
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        if (speed.X > 0)
        {
            spriteBatch.Draw(texture, vector, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.FlipHorizontally, 0f);
        }
        else
        {
            spriteBatch.Draw(texture, vector, Color.White);
        }
    }
}

class Tripod : Enemy
{
    public Tripod(Texture2D texture, float X, float Y) : base(texture, X, Y, 6f, 0.3f)
    {
    }

    public override void Update(GameWindow window)
    {
        vector.Y += speed.Y;
        
        if (vector.Y > window.ClientBounds.Height) isAlive = false;
    }
}