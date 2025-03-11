using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Vector2 = Microsoft.Xna.Framework.Vector2;
namespace planets;

public class Planet
{
    public Texture2D texture;
    public Vector2 position;
    public Vector2 speed;
    public float gravityConstant;
    public float energylossX;
    public float energylossY;
    
    public Planet(Texture2D texture, float X, float Y, float speedX, float speedY, float gravityConstant, float energylossX, float energylossY)
    {
        this.texture = texture;
        this.position.X = X;
        this.position.Y = Y;
        this.speed.X = speedX;
        this.speed.Y = speedY;
        this.gravityConstant = gravityConstant;
        this.energylossX = energylossX;
        this.energylossY = energylossY;
    }
    
    public void Update(GameWindow window)
    {
        position.X += speed.X;

        if (position.X >= window.ClientBounds.Width - texture.Width)
        {
            speed.X *= energylossX;
            position.X = window.ClientBounds.Width - texture.Width;
        }
        else if (position.X <= 0)
        {
            speed.X *= energylossX;
            position.X = 0;
        }        
        
        position.Y += speed.Y;

        if (position.Y >= window.ClientBounds.Height - texture.Height)
        {
            speed.Y *= energylossY;
            position.Y = window.ClientBounds.Height - texture.Height;
        }
        else if (position.Y <= 0)
        {
            speed.Y *= energylossY;
            position.Y = 0;
        }
        
        /*speed.Y += gravityConstant;
        position.Y += speed.Y;

        if (position.Y >= window.ClientBounds.Height - texture.Height)
        {
            speed.Y *= energylossY;
            position.Y = window.ClientBounds.Height - texture.Height;
        }*/

    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(texture, position, Color.White);
    }
    
}