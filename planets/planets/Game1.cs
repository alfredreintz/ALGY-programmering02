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

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private List<Planet> planets;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        // Ändrar skärmstorleken till helskärm
        _graphics.IsFullScreen = false;
        _graphics.PreferredBackBufferWidth = 1200;
        _graphics.PreferredBackBufferHeight = 700;
        _graphics.ApplyChanges();

        Random rand = new Random();

        planets = new List<Planet>();
        for (int i = 0; i < 20; i++)
        {
            float rndX = rand.Next(-100, 100);
            rndX /= 10;
            float rndY = rand.Next(-100, 100);
            rndY /= 10;

            Planet tmpPlanet = new Planet(Content.Load<Texture2D>("basePlanet"), 200 * i, 0, rndX, rndY, 0, -1.0f,
                -1.0f);
            planets.Add(tmpPlanet);
        }

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        
        for (int i = 0; i < planets.Count; i++)
        {
            for (int j = i + 1; j < planets.Count; j++) // Start from i + 1 to avoid duplicate checks
            {
                float radius1 = planets[i].texture.Width / 2f;
                float radius2 = planets[j].texture.Width / 2f;
        
                Vector2 center1 = planets[i].position + new Vector2(radius1, radius1);
                Vector2 center2 = planets[j].position + new Vector2(radius2, radius2);
        
                float distance = Vector2.Distance(center1, center2);
        
                if (distance <= (radius1 + radius2)) // Correct circle collision condition
                {
                    float tmpSpeedX = planets[i].speed.X;
                    float tmpSpeedY = planets[j].speed.Y;

                    planets[i].speed.X = planets[j].speed.X;
                    planets[i].speed.Y = planets[j].speed.Y;
                    
                    planets[j].speed.X = tmpSpeedX;
                    planets[j].speed.Y = tmpSpeedY;
                    
                    planets[i].Update(Window);
                    planets[j].Update(Window);
                }
            }
        }
        
        foreach (Planet planet in planets)
        {
            planet.Update(Window);
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        // Sätter bakgrundsfärg med rgb
        GraphicsDevice.Clear(new Color(31, 39, 56));

        // TODO: Add your drawing code here
        _spriteBatch.Begin();

        foreach (Planet planet in planets)
        {
            planet.Draw(_spriteBatch);
        }


        _spriteBatch.End();

        base.Draw(gameTime);
    }
}