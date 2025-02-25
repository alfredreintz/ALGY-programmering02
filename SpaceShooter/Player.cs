using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace SpaceShooter;

class Player : PhysicalObject
{
    private int points = 0;
    private List<Bullet> bullets;
    private Texture2D bulletTexture;
    private double timeSinceLastShot = 0;

    public Player(Texture2D texture, float X, float Y, float speedX, float speedY, Texture2D bulletTexture) : base(
        texture, X, Y, speedX, speedY)
    {
        bullets = new List<Bullet>();
        this.bulletTexture = bulletTexture;
    }

    public int Points
    {
        get { return points; }
        set { points = value; }
    }

    public List<Bullet> Bullets
    {
        get { return bullets; }
    }

    public void Update(GameWindow window, GameTime gameTime)
    {
        KeyboardState keyboardState = Keyboard.GetState();


        if (vector.X <= window.ClientBounds.Width - texture.Width && vector.X >= 0)
        {
            if (keyboardState.IsKeyDown(Keys.Right)) vector.X += speed.X;
            if (keyboardState.IsKeyDown(Keys.Left)) vector.X -= speed.X;
        }

        if (vector.Y <= window.ClientBounds.Height - texture.Height && vector.Y >= 0)
        {
            if (keyboardState.IsKeyDown(Keys.Down)) vector.Y += speed.Y;
            if (keyboardState.IsKeyDown(Keys.Up)) vector.Y -= speed.Y;
        }

        if (vector.X < 0) vector.X = 0;
        if (vector.X > window.ClientBounds.Width - texture.Width) vector.X = window.ClientBounds.Width - texture.Width;
        if (vector.Y < 0) vector.Y = 0;
        if (vector.Y > window.ClientBounds.Height - texture.Height)
            vector.Y = window.ClientBounds.Height - texture.Height;

        if (keyboardState.IsKeyDown(Keys.Space))
        {
            if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastShot + 200)
            {
                Bullet temp = new Bullet(bulletTexture, vector.X + texture.Width / 2, vector.Y);
            
                bullets.Add(temp);
            
                timeSinceLastShot = gameTime.TotalGameTime.TotalMilliseconds;
            }   
        }

        foreach (Bullet b in bullets.ToList())
        {
            b.Update();

            if (!b.IsAlive)
            {
                bullets.Remove(b);  
            }
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(texture, vector, Color.White);
        foreach (var b in bullets) b.Draw(spriteBatch);
    }
}

class Bullet : PhysicalObject
{
    public Bullet(Texture2D texture, float X, float Y) : base(texture, X, Y, 0, 3f)
    {
    }

    public void Update()
    {
        vector.Y -= speed.Y;

        if (vector.Y < 0)
        {
            isAlive = false;
        }
    }
}