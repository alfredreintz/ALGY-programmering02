using System;
using System.Numerics;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace slutprojekt;

// Basklass
class GameObject
{
    protected Texture2D texture;
    protected Vector2 vector;

    // Constructor för textur och koordinater
    public GameObject(Texture2D texture, float X, float Y)
    {
        this.texture = texture;
        this.vector.X = X;
        this.vector.Y = Y;
    }

    // Erbjuder barn att använda metod med samma logik
    public virtual void Draw(SpriteBatch spritebatch)
    {
        spritebatch.Draw(texture, vector, Color.White);
    }

    // Set : Get
    public float X
    {
        get { return vector.X; }
    }

    public float Y
    {
        get { return vector.Y; }
    }

    public float Width
    {
        get { return texture.Width; }
    }

    public float Height
    {
        get { return texture.Height; }
    }
}

abstract class MovingObject : GameObject
{
    protected Vector2 speed;

    // Samma som gameobjekt men lägger till hastighet i båda led
    protected MovingObject(Texture2D texture, float X, float Y, float speedX, float speedY) : base(texture, X, Y)
    {
        this.speed.X = speedX;
        this.speed.Y = speedY;
    }
}

abstract class PhysicalObject : MovingObject
{
    protected bool isAlive = true;

    public PhysicalObject(Texture2D texture, float X, float Y, float speedX, float speedY) : base(texture, X, Y, speedX,
        speedY)
    {
    }

    /// <summary>
    /// Kollar om en kollision mellan två objekt sker
    /// </summary>
    /// <param name="other"></param>
    /// <returns>Om objekten kolliderar eller inte</returns>
    public virtual bool CheckCollision(PhysicalObject other)
    {
        int narrowIndex = 5;
        
        // Skapar två rektanglar med bredd och höjd som objekten
        Rectangle myRect = new Rectangle(Convert.ToInt32(X + narrowIndex), Convert.ToInt32(Y + narrowIndex), Convert.ToInt32(Width - narrowIndex),
            Convert.ToInt32(Height - narrowIndex));
        Rectangle otherRect = new Rectangle(Convert.ToInt32(other.X), Convert.ToInt32(other.Y),
            Convert.ToInt32(other.Width), Convert.ToInt32(other.Height));
        // Returnera kollision som true eller icke-kollision som false
        return myRect.Intersects(otherRect);
    }

    public bool IsAlive
    {
        get { return isAlive; }
        set { isAlive = value; }
    }
}