using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace slutprojekt;

class Raft : PhysicalObject
{
    public Raft(Texture2D texture, float X, float Y, float speedX, float speedY) : base(texture, X, Y, speedX, speedY)
    {
    }
}

class Sea : PhysicalObject
{
    public Sea(Texture2D texture, float X, float Y, float speedX, float speedY) : base(texture, X, Y, speedX, speedY)
    {
    }
}