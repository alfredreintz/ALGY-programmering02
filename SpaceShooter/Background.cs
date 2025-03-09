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
namespace SpaceShooter;

class BackgroundSprite : GameObject
{
    public BackgroundSprite(Texture2D texture, float X, float Y) : base(texture, X, Y)
    {
    }

    public void Update(GameWindow window, int nrBackgroundsY)
    {
        vector.Y += 2f;

        if (vector.Y > window.ClientBounds.Height)
        {
            vector.Y = vector.Y - nrBackgroundsY * texture.Height;
        }
    }
}

class Background
{
    BackgroundSprite[,] background;
    int nrBackgroundsX, nrBackgroundsY;

    public Background(Texture2D texture, GameWindow window)
    {
        double tmpX = (double)window.ClientBounds.Width / texture.Width;

        nrBackgroundsX = (int)Math.Ceiling(tmpX);

        double tmpY = (double)window.ClientBounds.Height / texture.Height;

        nrBackgroundsY = (int)Math.Ceiling(tmpY) + 1;

        background = new BackgroundSprite[nrBackgroundsX, nrBackgroundsY];

        for (int i = 0; i < nrBackgroundsX; i++)
        {
            for (int j = 0; j < nrBackgroundsY; j++)
            {
                int posX = i * texture.Width;
                int posY = j * texture.Height - texture.Height;
                background[i, j] = new BackgroundSprite(texture, posX, posY);
            }
        }
    }

    public void Update(GameWindow window)
    {
        for (int i = 0; i < nrBackgroundsX; i++)
        {
            for (int j = 0; j < nrBackgroundsY; j++)
            {
                background[i, j].Update(window, nrBackgroundsY);
            }
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        for (int i = 0; i < nrBackgroundsX; i++)
        {
            for (int j = 0; j < nrBackgroundsY; j++)
            {
                background[i, j].Draw(spriteBatch);
            }
        }
    }
    
    
}