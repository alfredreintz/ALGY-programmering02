using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

// =======================================================================
// HsItem, en beh�llare-klass som inneh�ller info om en person i
// highscorelistan.
// =======================================================================
class HSItem
{
    // Variabler och egenskaper f�r dem:
    string name;
    int points;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public int Points
    {
        get { return points; }
        set { points = value; }
    }

    // =======================================================================
    // HSItem(), klassens konstruktor
    // =======================================================================
    public HSItem(string name, int points)
    {
        this.name = name;
        this.points = points;
    }
}

// =======================================================================
// HighScore, inneh�ller en lista med hsItems samt metoder f�r att
// manipulera listan.
// =======================================================================
class HighScore
{
    int maxInList = 5; // Hur m�nga som f�r vara i listan
    List<HSItem> highscore = new List<HSItem>();
    string name; // Spelarens namn

    // Anv�nds f�r att skriva ut vilket tecken spelaren har valt just nu:
    string currentChar;

    int key_index = 0; // Denna anv�nds f�r att mata in spelarens namn

    // Dessa anv�nds f�r att kontrollera n�r tangenter trycktes in:
    double lastChange = 0;
    Keys previousKey;

    // =======================================================================
    // HighScore(), klassens konstruktor
    // =======================================================================
    public HighScore(int maxInList)
    {
        this.maxInList = maxInList;
    }

    // =======================================================================
    // Sort(),  metod som sorterar listan. Metoden
    // anropas av Add() n�r en ny person l�ggs till i
    // listan. Anv�nder algoritmen bubblesort
    // =======================================================================
    void Sort()
    {
        int max = highscore.Count - 1;

        // Den yttre loopen, g�r igenom hela listan            
        for (int i = 0; i < max; i++)
        {
            // Den inre, g�r igenom element f�r element
            int nrLeft = max - i; // F�r att se hur m�nga som redan g�tts igenom
            for (int j = 0; j < nrLeft; j++)
            {
                if (highscore[j].Points < highscore[j + 1].Points) // J�mf�r elementen
                {
                    // Byt plats!
                    HSItem temp = highscore[j];
                    highscore[j] = highscore[j + 1];
                    highscore[j + 1] = temp;
                }
            }
        }
    }

    // =======================================================================
    // Add(), l�gger till en person i highscore-listan.
    // =======================================================================
    void Add(int points)
    {
        // Skapa en tempor�r variabel av typen HSItem:
        HSItem temp = new HSItem(name, points);
        // L�gg till tmp i listan. Observera att f�ljande Add()
        // tillh�r klassen List (�r allts� skapad av Microsoft).
        // Metoden har endast samma namn, som just denna Add():
        highscore.Add(temp);
        Sort(); // Sortera listan efter att vi har lagt till en person!

        // �r det f�r m�nga i listan?
        if (highscore.Count > maxInList)
        {
            // Eftersom vi har lagt till endast en person nu, s� betyder
            // det att det �r en person f�r mycket. Index p� personen
            // som �r sist i listan, �r samma som maxInList. Vi vill ju
            // att det h�gsta indexet ska vara maxInList-1. Allst� kan
            // vi bara ta bort elementet med index maxInList.
            // Exempel:
            // maxInList �r 5, vi har 6 element i listan. Det sj�tte
            // elementet har index 5. Vi g�r highscore.RemoveAt(5):
            highscore.RemoveAt(maxInList);
        }
    }

    // =======================================================================
    // CheckKey(), kontrollerar om en viss tangent har tryckts och huruvida
    // det har g�tt lagomt l�ng tid (130ms) sedan tidigare tryck av samma
    // tangent.
    // =======================================================================
    bool CheckKey(Keys key, GameTime gameTime)
    {
        KeyboardState keyboardState = Keyboard.GetState();
        if (keyboardState.IsKeyDown(key))
        {
            // Har det g�tt lagomt l�ng tid, eller �r det en helt annan
            // tangent som trycks ned denna g�ng?
            if (lastChange + 130 < gameTime.TotalGameTime.TotalMilliseconds
                || previousKey != key)
            {
                // s�tt om variablerna inf�r n�sta varv i spelloopen:
                previousKey = key;
                lastChange = gameTime.TotalGameTime.TotalMilliseconds;
                return true;
            }
        }

        // Just den tangenten (key) trycktes INTE ned, eller s� trycktes den
        // ned alldeles nyligen (mindre �n 130ms):
        return false;
    }

    // =======================================================================
    // PrintDraw(), metod f�r att skriva ut listan. Det finns ingen
    // PrintUpdate() d� det �r en helt statisk text som skrivs ut.
    // =======================================================================
    public void PrintDraw(SpriteBatch spriteBatch, SpriteFont font)
    {
        string text = "HIGHSCORE\n";
        foreach (HSItem h in highscore)
            text += h.Name + " " + h.Points + "\n";

        spriteBatch.DrawString(font, text, Vector2.Zero, Color.White);
    }

    // =======================================================================
    // EnterUpdate(), h�r matar anv�ndaren in sitt anv�ndarnamn. Precis som
    // klassiska gamla arkadspel kan man ha tre tecken A-Z i sitt namn. Detta
    // �r Update-delen i spel-loopen f�r inmatning av highscore-namn. Metoden
    // ska forts�tta anropas av Update() s� l�nge true returneras.
    // =======================================================================
    public bool EnterUpdate(GameTime gameTime, int points)
    {
        // Vilka tecken som �r m�jliga:
        char[] key =
        {
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K',
            'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U',
            'V', 'X', 'Y', 'Z'
        };


        // Anv�ndaren trycker knappen ned�t, stega framl�nges i key-vektorn:
        if (CheckKey(Keys.Down, gameTime))
        {
            key_index++;
            if (key_index >= key.Length)
                key_index = 0;
        }

        // Anv�ndaren trycker knappen upp�t, stega bakl�nges i key-vektorn:
        if (CheckKey(Keys.Up, gameTime))
        {
            key_index--;
            if (key_index <= 0)
                key_index = key.Length - 1;
        }

        // Anv�ndaren trycker ENTER, l�gg till det valda tecknet i 
        if (CheckKey(Keys.Enter, gameTime))
        {
            name += key[key_index].ToString();
            if (name.Length == 3)
            {
                // �terst�ll namnet och allt s� att man kan l�gga till namnet 
                // p� en ny spelare:
                Add(points);
                name = "";
                currentChar = "";
                key_index = 0;
                return true; // Ange att vi �r klara
            }
        }

        // Lagra det tecken som nu �r valt, s� att vi kan skriva ut det i
        // EnterDraw():
        currentChar = key[key_index].ToString();
        // Ange att vi inte �r klara, forts�tt anropa denna metod via Update():
        return false;
    }

    // =======================================================================
    // EnterDraw(), skriver ut de tecken spelaren har matat in av sitt namn
    // (om n�got) samt det tecken (av tre) som just nu �r valt.
    // =======================================================================
    public void EnterDraw(SpriteBatch spriteBatch, SpriteFont font)
    {
        string text = "ENTER NAME:" + name + currentChar;
        spriteBatch.DrawString(font, text, Vector2.Zero, Color.White);
    }

    // =======================================================================
    // SaveToFile(), spara till fil.
    // =======================================================================
    public void SaveToFile(string filename)
    {
        StreamWriter sw = new StreamWriter(filename);

        foreach (HSItem item in highscore)
        {
            string text = item.Name + ":" + item.Points;
            sw.WriteLine(text);
        }

        sw.Close();
    }

    // =======================================================================
    // LoadFromFile(), ladda fr�n fil.
    // =======================================================================
    public void LoadFromFile(string filename)
    {
        if (!File.Exists(filename))
        {
            File.Create(filename).Close();
        }
        else
        {
            StreamReader sr = new StreamReader(filename);

            string row;
            while ((row = sr.ReadLine()) != null)
            {
                string[] words = row.Split(':');
                int points = Convert.ToInt32(words[1]);

                HSItem temp = new HSItem(words[0], points);
                highscore.Add(temp);
            }

            sr.Close();
        }
    }
}