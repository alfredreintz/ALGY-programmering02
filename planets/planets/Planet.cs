using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
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
    private Texture2D texture;
    private Vector2 position;
    private Vector2 speed;
    private float gravityConstant;
    private float energylossX;
    private float energylossY;
    
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

    public Texture2D Texture
    {
        get { return texture; }
        set { texture = value; }
    }

    public Vector2 Position
    {
        get { return position; }
        set { position = value; }
    }
    
    
    
    public void Update(GameWindow window)
    {
        Position.X += speed.X;

        if (Position.X >= window.ClientBounds.Width - Texture.Width)
        {
            speed.X *= energylossX;
            Position.X = window.ClientBounds.Width - Texture.Width;
        }
        else if (Position.X <= 0)
        {
            speed.X *= energylossX;
            Position.X = 0;
        }        
        
        Position.Y += speed.Y;

        if (position.Y >= window.ClientBounds.Height - Texture.Height)
        {
            speed.Y *= energylossY;
            Position.Y = window.ClientBounds.Height - Texture.Height;
        }
        else if (Position.Y <= 0)
        {
            speed.Y *= energylossY;
            Position.Y = 0;
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


/*
public class Planet
{
    private readonly Texture2D texture;
    private Vector2 position;
    private Vector2 speed;
    private float gravityConstant;
    private float energyLossX;
    private float energyLossY;

    public Texture2D Texture => texture; // Read-only access
    public Vector2 Position => position; // Read-only access
    public Vector2 Speed => speed; // Read-only access

    public float GravityConstant
    {
        get => gravityConstant;
        set
        {
            if (value < 0) throw new ArgumentException("Gravity constant cannot be negative.");
            gravityConstant = value;
        }
    }

    public float EnergyLossX
    {
        get => energyLossX;
        set
        {
            if (value < 0 || value > 1) throw new ArgumentException("Energy loss must be between 0 and 1.");
            energyLossX = value;
        }
    }

    public float EnergyLossY
    {
        get => energyLossY;
        set
        {
            if (value < 0 || value > 1) throw new ArgumentException("Energy loss must be between 0 and 1.");
            energyLossY = value;
        }
    }

    public Planet(Texture2D texture, float x, float y, float speedX, float speedY, float gravityConstant, float energyLossX, float energyLossY)
    {
        this.texture = texture ?? throw new ArgumentNullException(nameof(texture));
        position = new Vector2(x, y);
        speed = new Vector2(speedX, speedY);
        GravityConstant = gravityConstant;
        EnergyLossX = energyLossX;
        EnergyLossY = energyLossY;
    }

    public void Update(GameWindow window)
    {
        position.X += speed.X;

        if (position.X >= window.ClientBounds.Width - texture.Width)
        {
            speed.X *= EnergyLossX;
            position.X = window.ClientBounds.Width - texture.Width;
        }
        else if (position.X <= 0)
        {
            speed.X *= EnergyLossX;
            position.X = 0;
        }

        position.Y += speed.Y;

        if (position.Y >= window.ClientBounds.Height - texture.Height)
        {
            speed.Y *= EnergyLossY;
            position.Y = window.ClientBounds.Height - texture.Height;
        }
        else if (position.Y <= 0)
        {
            speed.Y *= EnergyLossY;
            position.Y = 0;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(texture, position, Color.White);
    }
}

