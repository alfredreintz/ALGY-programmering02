using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;
using System.Text.Unicode;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Plane = Microsoft.Xna.Framework.Plane;
using Quaternion = Microsoft.Xna.Framework.Quaternion;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace slutprojekt;

// Ärver från förälder
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
    private bool isJumping;
    private bool isFalling;
    private float gravityDeltaTime;
    private bool fellOf;

    // Lägger till gravitationskonstant och bullettexture
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

        if (keyboardState.IsKeyDown(Keys.K)) this.isAlive = false;


        // Logik för att flytt spelaren i sidleds
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

        // Om man håller nere space och spelaren inte hoppar redaan
        if (keyboardState.IsKeyDown(Keys.Space) && !isJumping)
        {
            isJumping = true;
        }

        // Om spelaren hoppar men faller inte (OBS! Spelaren faller då den ramlar av plattformen)
        if (isJumping && !isFalling)
        {
            gravityDeltaTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            vector.Y += -speed.Y + gravityConstant * gravityDeltaTime;
        }

        // Om spelaren faller
        if (isFalling)
        {
            gravityDeltaTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            vector.Y += gravityConstant * gravityDeltaTime;
        }

        // Gör så att spelaren kan skicka iväg bullets
        if (keyboardState.IsKeyDown(Keys.E))
        {
            if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastBullet + 100u)
            {
                Bullet temp = new Bullet(bulletTexture, vector.X + texture.Width / 2, vector.Y, 7.5f);

                bullets.Add(temp);

                timeSinceLastBullet = gameTime.TotalGameTime.TotalMilliseconds;
            }
        }

        // Loopar igenom och uppdaterar bullets
        foreach (Bullet b in bullets.ToList())
        {
            b.Update(window, gameTime);

            if (!b.IsAlive)
            {
                bullets.Remove(b);
            }
        }
    }

    /// <summary>
    /// Metod för att kolla om spelaren befinner sig på ett objekt eller inte
    /// </summary>
    /// <param name="gameTime">Ibyggd i monogame</param>
    /// <param name="x">Ett annat objekts x-position</param>
    /// <param name="y">Ett annat objekts y-position</param>
    /// <param name="width">Ett annat objekts bredd</param>
    /// <param name="height">Ett annat objekts höjd</param>
    /// Denna metoden var väldigt klurig för mig att lista ut och det finns definitivt finare och mer läsbara sätt att lösa det på
    /// Huvudsaken är att den fungerar
    public void checkTouchable(GameTime gameTime, PhysicalObject otherObject)
    {
        // Om spelaren befinner sig inom gränserna för plattformen
        if (!fellOf && vector.Y >= otherObject.Y - Height && vector.X > otherObject.X - texture.Width &&
            vector.X < otherObject.X + otherObject.Width)
        {
            // Ställer om variabelvärden
            isFalling = false;
            vector.Y = otherObject.Y - Height;

            // Kollar om spelaren hoppar eller inte
            if (isJumping)
            {
                isJumping = false;
                gravityDeltaTime = 0;
            }

            // Om spelaren inte är under plattformen
            if (!isFalling)
            {
                gravityDeltaTime = 0;
            }
        }
        else
        {
            // Hoppar inte spelaren och är under platformen så måste gravitationen ställas om
            if (!isJumping)
            {
                isFalling = true;
            }

            // Om spelaren är under plattformen har den inte någon chans att komma upp igen
            if (vector.Y > otherObject.Y - otherObject.Height)
            {
                fellOf = true;
            }
        }
    }

    /// <summary>
    /// Ändrar texturer för karaktär när karaktären går
    /// </summary>
    /// <param name="gameTime">speltiden</param>
    /// <param name="texturesLeft">Lista med texturer för gång åt vänster</param>
    /// <param name="texturesRight">Lista med texturer för gång åt höger</param>
    public void Walkcycle(GameTime gameTime, List<Texture2D> texturesLeft, List<Texture2D> texturesRight)
    {
        // Om spelaren rör sig åt vänster
        if (movingDirection == 'L')
        {
            textureDeltaTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            // Om index är för stort
            if (textureIndex == texturesLeft.Count - 1)
            {
                textureIndex = 0;
            }

            // Om 125 millisekunder eller mer har passerat
            if (textureDeltaTime > 125f)
            {
                // Ändra texture
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
        // Resettar variabler
        vector.X = X;
        vector.Y = Y;
        speed.X = speedX;
        speed.Y = speedY;

        isFalling= false;
        isJumping = false;
        fellOf = false;
        gravityDeltaTime = 0f;

        bullets.Clear();
        timeSinceLastBullet = 0;
        // points = 0;
        isAlive = true;
    }
}

class Bullet : PhysicalObject
{
    public Bullet(Texture2D texture, float X, float Y, float bulletSpeedConst) : base(texture, X, Y, 0, 0)
    {
        // Initialiserar logik för bullets
        MouseState mouseState = Mouse.GetState();
        
        int mouseX = mouseState.X;
        int mouseY = mouseState.Y;

        float deltaX = mouseX - vector.X;
        float deltaY = mouseY - vector.Y;

        // Använnder metoden för arctaneee som automatiskt tar hand om division med 0
        double angle = Math.Atan2(deltaY, deltaX);
        
        speed.X = (float)Math.Cos(angle);
        speed.Y = (float)Math.Sin(angle);

        speed.X *= bulletSpeedConst;
        speed.Y *= bulletSpeedConst;

    }

    public void Update(GameWindow window, GameTime gameTime)
    {
        vector.X += speed.X;
        vector.Y += speed.Y;

        if (vector.Y < 0 || vector.Y > window.ClientBounds.Height || vector.X < 0 || vector.X > window.ClientBounds.Width)
        {
            isAlive = false;
        }
    }
}