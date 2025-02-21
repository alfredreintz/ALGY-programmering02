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

class GoldCoin : PhysicalObject
{
    private double timeToDie;

    public GoldCoin(Texture2D texture, float X, float Y, GameTime gameTime) : base(texture, X, Y, 0, 2f)
    {
        timeToDie = gameTime.TotalGameTime.TotalMilliseconds + 5000;
    }

    public void Update(GameTime gameTime)
    {
        if (timeToDie < gameTime.TotalGameTime.TotalMilliseconds) isAlive = false;
    }
}