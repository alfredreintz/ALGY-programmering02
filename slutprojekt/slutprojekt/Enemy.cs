using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace slutprojekt;

// Använder abstract för att inget object ska kunna skapas av denna förälder-klass
abstract class Enemy : PhysicalObject
{
    protected static Random rand = new Random();
    protected int randX1;
    protected int randX2;
    protected int randY1;
    protected int randY2;
    protected int randPos;

    public Enemy(Texture2D texture, float X, float Y, float speedX, float speedY) : base(texture, X, Y, speedX, speedY)
    {
    }

    public void Update(GameWindow window)
    {
        // Flyttar objektet på skärmen
        vector.X += speed.X;
        vector.Y += speed.Y;
    }

    public abstract void setRandPosition(GameWindow window, float x1, float x2, float otherY);


    public bool IsAlive
    {
        get { return isAlive; }
        set { isAlive = value; }
    }
}

class HBird : Enemy
{
    private float speedConstX;

    // Lägger till en konstant för x-hastighet
    public HBird(Texture2D texture, float X, float Y, float speedX, float speedY, float speedConstX) : base(texture, X,
        Y, speedX, speedY)
    {
        this.speedConstX = speedConstX;
    }

    /// <summary>
    /// Slumpar fram en ny position för objektet
    /// </summary>
    /// <param name="window">spelfönstret</param>
    /// <param name="x1">ett annat objekts x-koordinat i någon form</param>
    /// <param name="x2">ett annat objekts x-koordinat i någon form</param>
    /// <param name="y1">ett annat objekts y-koordinat i någon form</param>
    /// <param name="y2">ett annat objekts y-koordinat i någon form</param>
    public override void setRandPosition(GameWindow window, float x1, float x2, float otherY)
    {
        // Om objektet befinner sig utanför skärmen
        if (vector.X > window.ClientBounds.Width && speed.X > 0 || vector.X < 0 - Width && speed.X < 0)
        {
            // Skapar random koordinater för att slumpa position
            randX1 = rand.Next(-2000, -500);
            randX2 = rand.Next(window.ClientBounds.Width + 500, window.ClientBounds.Width + 2000);
            randY1 = rand.Next((int)otherY - 50, (int)otherY);

            randPos = rand.Next(0, 2);

            speed.X = speedConstX * rand.Next(3, 20) / 10;

            vector.Y = randY1;
            if (randPos == 0)
            {
                // Flyttar objektet
                vector.X = randX1;
                // Använder absolutbelopp för att försäkra så att värdet är större än noll
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
        // Om objektets hastighet är mindre än noll
        if (speed.X > 0)
        {
            // Flippar objektet horizontellt så att den pekar åt andra hållet
            spriteBatch.Draw(texture, vector, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.FlipHorizontally,
                0f);
        }
        else
        {
            spriteBatch.Draw(texture, vector, Color.White);
        }
    }
}

class VBird : Enemy
{
    private float speedConstX;

    // Lägger till en konstant för x-hastighet
    public VBird(Texture2D texture, float X, float Y, float speedX, float speedY, float speedConstX) : base(texture, X,
        Y, speedX, speedY)
    {
        this.speedConstX = speedConstX;
    }

    public override void setRandPosition(GameWindow window, float x1, float x2, float otherY)
    {
        // Om objektet är under skärmen
        if (vector.Y > window.ClientBounds.Height)
        {
            randX1 = rand.Next((int)x1, (int)x2);
            randY1 = rand.Next(-2000, -500);

            speed.Y = speedConstX * rand.Next(10, 30) / 10;

            vector.X = randX1;
            vector.Y = randY1;
            speed.Y = Math.Abs(speed.Y);
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(texture, vector, Color.White);
    }
}

class DBird : Enemy
{
    private float speedConstX;

    // Lägger till en konstant för x-hastighet
    public DBird(Texture2D texture, float X, float Y, float speedX, float speedY, float speedConstX) : base(texture, X,
        Y, speedX, speedY)
    {
        this.speedConstX = speedConstX;
    }

    public override void setRandPosition(GameWindow window, float x1, float x2, float otherY)
    {
        // Om objektet är under skärmen
        if (vector.Y > window.ClientBounds.Height)
        {
            randX1 = rand.Next((int)x1 - 2000, (int)x1);
            randX2 = rand.Next((int)x2, (int)x2 + 2000);

            randY1 = rand.Next(-2000, -500);

            speed.X = speedConstX * rand.Next(3, 10) / 10;
            speed.Y = speedConstX * rand.Next(3, 10) / 10;
            ;

            randPos = rand.Next(0, 2);

            vector.Y = randY1;
            if (randPos == 0)
            {
                // Flyttar objektet
                vector.X = randX1;
                // Använder absolutbelopp för att försäkra så att värdet är större än noll
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
        // Räknar ut lutningen på bjektet i radianer
        float rotation = -(float)Math.Atan2(speed.X, speed.Y);
        Vector2 origin = new Vector2(texture.Width / 2f, texture.Height / 2f);
        
        // Om objektets hastighet är mindre än noll
        if (speed.X > 0)
        {
            spriteBatch.Draw(texture, vector, null, Color.White, rotation, origin, 1f, SpriteEffects.None, 0f);
        }
        else
        {
            spriteBatch.Draw(texture, vector, null, Color.White, rotation, origin, 1f, SpriteEffects.FlipHorizontally, 0f);
        }
    }
}