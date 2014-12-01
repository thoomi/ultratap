using System.IO.IsolatedStorage;
using System.IO;
using System.Windows.Resources;
using System.Collections.Generic;
using System;

namespace Ultratap
{
    class savegame
    {

        private const string FileName= "savegame.txt";

        public int Score;   
        public int LevelID;
        public int Taps;
        public TimeSpan ts;

        public savegame()
        {
            Score = 0;
            LevelID = 0;
            Taps = 0;
            ts = new TimeSpan(0, 0, 0, 0, 0);
        }


        // Load the gamestate from File
        public void LoadGameState()
        {
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

                                Score = Int32.Parse(reader.ReadLine());
                                LevelID = Int32.Parse(reader.ReadLine());
                                Taps = Int32.Parse(reader.ReadLine());
                                ts = TimeSpan.Parse(reader.ReadLine());
                                
                                reader.Close();
                            }
                            fs.Close();
                        }
                    }
                }
            }    
        }

        // Save the game state
        public void SaveGameState()
        {
            using (IsolatedStorageFile savegameStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (IsolatedStorageFileStream fs = savegameStorage.CreateFile(FileName))
                {
                    // Create a new file or overwrite an existing to store the highscore
                    using (StreamWriter writer = new StreamWriter(fs))
                    {

                        writer.WriteLine(Score);
                        writer.WriteLine(LevelID);
                        writer.WriteLine(Taps);
                        writer.WriteLine(ts);

                        writer.Close();
                    }
                    fs.Close();
                }
            }
        }

        public void ResetGameState()
        {
            Score = 0;
            LevelID = 0;
            Taps = 0;
            ts = new TimeSpan(0, 0, 0, 0, 0);

            SaveGameState();
        }
    }
}
