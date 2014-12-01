using System.IO;
using System.Windows.Resources;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework;
using System.IO.IsolatedStorage;

namespace Ultratap
{

    class highscoreEntry
    {
        public string name;
        public int level;
        public TimeSpan ts;


        public highscoreEntry(string Name, int Level, TimeSpan timespan)             
        {
            name = Name;
            level = Level;
            ts = new TimeSpan(timespan.Days, timespan.Hours, timespan.Minutes, timespan.Seconds, timespan.Milliseconds);
        }
    }



    class highscore
    {

        const int MAXENTRIES = 5;

        // The file where the highscore is stored
        private const string FileName = "highscore.txt";


        // List of Entrys 
        public List<highscoreEntry> entries = new List<highscoreEntry>();

        // Constructor
        public highscore() 
        {

            // open isolated storage, and load data from the highscore if it exists.
            using (IsolatedStorageFile savegameStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (savegameStorage.FileExists(FileName))
                {
                    using (IsolatedStorageFileStream fs = savegameStorage.OpenFile(FileName, FileMode.Open, FileAccess.Read))
                    {
                        if (fs != null)
                        {
                            fs.Position = 0;

                            using (StreamReader reader = new StreamReader(fs))
                            {

                                for (int i = 0; i < MAXENTRIES; i++)
                                {
                                    entries.Add(new highscoreEntry(reader.ReadLine(), Int32.Parse(reader.ReadLine()), TimeSpan.Parse(reader.ReadLine())));
                                }
                                reader.Close();
                            }
                            fs.Close();
                        }
                    }
                }
                else
                { 
                    // If the file does not exist create default data and save it
                    for (int i = 0; i < MAXENTRIES; i++)
                    {
                        entries.Add(new highscoreEntry("Ernie", 0, new TimeSpan(99, 0, 0, 0, 0)));
                    }
                    saveHighscore();
                }
            }    
        }
        
        // Saves the current highscore list
        public void saveHighscore()
        {

            using (IsolatedStorageFile savegameStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (IsolatedStorageFileStream fs = savegameStorage.CreateFile(FileName))
                {
                    // Create a new file or overwrite an existing to store the highscore
                    using (StreamWriter writer = new StreamWriter(fs))
                    {
                        foreach (highscoreEntry entry in entries)
                        {
                            writer.WriteLine(entry.name);
                            writer.WriteLine(entry.level);
                            writer.WriteLine(entry.ts);
                        }
                        writer.Close();
                    }
                    fs.Close();
                }
            }
        }


        int newLevel;
        TimeSpan newTS;

        // Adds a score to the highscore board
        public void addHighscoreEntry(int level, TimeSpan ts)
        {
            newLevel = level;
            newTS = ts;

            // Try to get user input
            try
            {
                Guide.BeginShowKeyboardInput(PlayerIndex.One, "You got a new highscore!", "Please enter a name for the scoreboard (Minimum 3 and no longer than 10 characters)", username, GetText, null);
            }
            catch (Exception ex)
            {
            }

        }

        // Checks if the entry is better than the worst board entry
        public bool isInHighscore(TimeSpan ts, int level)
        {
            if (level < 1)
            {
                return false;
            }


            if (level > entries[MAXENTRIES - 1].level)
            {
                return true;
            }

            if (level == entries[MAXENTRIES -1].level)
            {
                if (ts < entries[MAXENTRIES - 1].ts)
                {
                    return true;
                }

            }
            return false;
        }

        // Sorts the highscore list from top to bottom
        public void sortHighscore()
        {
            entries.Sort(CompareTimes);
            entries.Sort(CompareLevel);
        }

        /// <summary>
        /// Comparison method used to compare two highscore entries.
        /// </summary>
        /// <param name="score1">First highscore entry.</param>
        /// <param name="score2">Second highscore entry.</param>
        /// <returns>1 if the first highscore is smaller than the second, 0 if both
        /// are equal and -1 otherwise.</returns>
        private static int CompareTimes(highscoreEntry entry1, highscoreEntry entry2)
        {

            if (entry1.ts > entry2.ts)
            {
                return 1;
            }

            if (entry1.ts == entry2.ts)
            {
                return 0;
            }

            return -1;
        }

        /// <summary>
        /// Comparison method used to compare two highscore entries.
        /// </summary>
        /// <param name="score1">First highscore entry.</param>
        /// <param name="score2">Second highscore entry.</param>
        /// <returns>1 if the first highscore is smaller than the second, 0 if both
        /// are equal and -1 otherwise.</returns>
        private static int CompareLevel(highscoreEntry entry1, highscoreEntry entry2)
        {

            if (entry1.level < entry2.level)
            {
                return 1;
            }

            if (entry1.level == entry2.level)
            {
                return 0;
            }

            return -1;
        }


        #region UserKeyboardInput

        string username = "Player";
        private void GetText(IAsyncResult result)
        {
            try
            {
                string resultString = Guide.EndShowKeyboardInput(result); ;

                if (resultString != null)
                {

                    if (resultString.Length > 10)
                    {
                        resultString = resultString.Remove(10);
                    }

                    if (resultString.Length < 3)
                    {
                        return;
                    }
                    username = resultString;

                }
                else
                {
                    username = "Player";
                }
            }
            catch (Exception ex)
            {
                username = "Player";
                return;
            }

            entries.Add(new highscoreEntry(username, newLevel, newTS));
            sortHighscore();
            saveHighscore();
        }
        #endregion

    }
}
