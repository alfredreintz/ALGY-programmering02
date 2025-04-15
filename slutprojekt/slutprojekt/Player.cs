using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace slutprojekt;

class Player : PhysicalObject
{
    private int points = 0;
    private List<Bullet> bullets;
    private Texture2D bulletTexture;
    private double timeSinceLastBullet = 0;
    private bool isMoving = false;
    private char movingDirection;
    private int textureIndex = 0;
    private float textureDeltaTime = 125f;
    private float gravityConstant;
    private bool isJumping = false;
    private float gravityDeltaTime;

    public Player(Texture2D texture, float X, float Y, float speedX, float speedY, float gravityConstant,
        Texture2D bulletTexture) : base(
        texture, X, Y, speedX, speedY)
    {
        bullets = new List<Bullet>();
        this.bulletTexture = bulletTexture;
        this.gravityConstant = gravityConstant;
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


        if (keyboardState.IsKeyDown(Keys.E)) this.isAlive = false;

        if (vector.X <= window.ClientBounds.Width - texture.Width && vector.X >= 0)
        {
            if (keyboardState.IsKeyDown(Keys.D))
            {
                vector.X += speed.X;
                isMoving = true;
                movingDirection = 'R';
            }

            if (keyboardState.IsKeyDown(Keys.A))
            {
                vector.X -= speed.X;
                isMoving = true;
                movingDirection = 'L';
            }

            // Är ingen av knapparna nedtryckta eller båda knapparna nedtryckta
            if (keyboardState.IsKeyUp(Keys.D) && keyboardState.IsKeyUp(Keys.A) ||
                !keyboardState.IsKeyUp(Keys.D) && !keyboardState.IsKeyUp(Keys.A))
            {
                // Återställer variabler
                textureDeltaTime = 125f;
                textureIndex = 0;
                movingDirection = '0';
            }
        }

        if (vector.Y <= window.ClientBounds.Height - texture.Height && vector.Y >= 0)
        {
            if (keyboardState.IsKeyDown(Keys.Space) && !isJumping)
            {
                isJumping = true;
            }

            if (isJumping)
            {
                gravityDeltaTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                Console.WriteLine(gravityConstant * gravityDeltaTime);
                vector.Y += -speed.Y + gravityConstant * gravityDeltaTime;
            }
        }

        if (vector.X < 0) vector.X = 0;
        if (vector.X > window.ClientBounds.Width - texture.Width) vector.X = window.ClientBounds.Width - texture.Width;
        if (vector.Y < 0) vector.Y = 0;
        if (vector.Y > window.ClientBounds.Height - texture.Height)
        {
            vector.Y = window.ClientBounds.Height - texture.Height;
            if (isJumping)
            {
                isJumping = false;
                gravityDeltaTime = 0;
            }
        }

        /* if (keyboardState.IsKeyDown(Keys.Å))
        {
            if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastBullet + 200)
            {
                Bullet temp = new Bullet(bulletTexture, vector.X + texture.Width / 2, vector.Y);

                bullets.Add(temp);

                timeSinceLastBullet = gameTime.TotalGameTime.TotalMilliseconds;
            }
        } */

        foreach (Bullet b in bullets.ToList())
        {
            b.Update();

            if (!b.IsAlive)
            {
                bullets.Remove(b);
            }
        }
    }

    public void Walkcycle(GameTime gameTime, List<Texture2D> texturesLeft, List<Texture2D> texturesRight)
    {
        if (movingDirection == 'L')
        {
            textureDeltaTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (textureIndex == texturesLeft.Count - 1)
            {
                textureIndex = 0;
            }

            if (textureDeltaTime > 125f)
            {
                textureIndex++;
                texture = texturesLeft[textureIndex];
                textureDeltaTime = 0f;
            }
        }
        else if (movingDirection == 'R')
        {
            textureDeltaTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (textureIndex == texturesRight.Count - 1)
            {
                textureIndex = 0;
            }

            if (textureDeltaTime > 125f)
            {
                textureIndex++;
                texture = texturesRight[textureIndex];
                textureDeltaTime = 0f;
            }
        }
        else
        {
            texture = texturesLeft[0];
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(texture, vector, Color.White);
        foreach (var b in bullets) b.Draw(spriteBatch);
    }

    public void Reset(float X, float Y, float speedX, float speedY)
    {
        vector.X = X;
        vector.Y = Y;
        speed.X = speedX;
        speed.Y = speedY;

        bullets.Clear();
        timeSinceLastBullet = 0;
        points = 0;
        isAlive = true;
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

class Raft : PhysicalObject
{
    public Raft(Texture2D texture, float X, float Y, float speedX, float speedY) : base(texture, X, Y, speedX, speedY)
    {
    }
}