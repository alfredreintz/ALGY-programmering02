using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace slutprojekt;

// Ärver inte från någon
class MenuItem
{
    private Texture2D texture;
    private Vector2 position;
    private int currentState;

    public MenuItem(Texture2D texture, Vector2 position, int currentState)
    {
        this.texture = texture;
        this.position = position;
        this.currentState = currentState;
    }

    public Texture2D Texture
    {
        get { return texture; }
    }

    public Vector2 Position
    {
        get { return position; }
    }

    public int State
    {
        get { return currentState; }
    }
}

// Ärver inte från någon förälder
class Menu
{
    private List<MenuItem> menu;
    private int selected = 0;

    private float currentHeight = 0;
    private double lastChange = 0;
    private int defaultMenuState;

    public Menu(int defaultMenuState)
    {
        menu = new List<MenuItem>();
        this.defaultMenuState = defaultMenuState;
    }

    /// <summary>
    /// Lägger till item i menyn
    /// </summary>
    /// <param name="itemtexture">Texture för item</param>
    /// <param name="state">State</param>
    public void AddItem(Texture2D itemtexture, int state)
    {
        float X = 0;
        float Y = +currentHeight;

        currentHeight += itemtexture.Height + 20;

        MenuItem temp = new MenuItem(itemtexture, new Vector2(X, Y), state);

        menu.Add(temp);
    }

    public int Update(GameTime gameTime)
    {
        KeyboardState keyboardState = Keyboard.GetState();

        // Kollar om spelaren ändrat menyval nyss, då får den vänta lite
        if (lastChange + 130 < gameTime.TotalGameTime.TotalMilliseconds)
        {
            // Om spelaren vill ändra alternativ
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                // Gå ned ett steg
                selected++;

                if (selected > menu.Count - 1) selected = 0;
            }
            // Vise versa
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                selected--;

                if (selected < 0)
                {
                    selected = menu.Count - 1;
                }
            }

            lastChange = gameTime.TotalGameTime.TotalMilliseconds;
        }

        // Om spelaren vill in i alternativet
        if (keyboardState.IsKeyDown(Keys.Enter))
        {
            // Returnera state
            return menu[selected].State;
        }
        
        return defaultMenuState;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        // Loopar igenom alla menyalternativ
        for (int i = 0; i < menu.Count; i++)
        {
            // Om spelaren har valt alternativet har den en annan färg
            if (i == selected) spriteBatch.Draw(menu[i].Texture, menu[i].Position, Color.Yellow);
            else spriteBatch.Draw(menu[i].Texture, menu[i].Position, Color.LightGray);
        }
    }
}