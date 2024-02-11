using System;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;

namespace MusicBeePlugin
{
    public class SettingsManager
    {
        private readonly string SETTINGS_FILE_LOC = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/MusicBee/VGMVSettingsFile.ini";

        public bool P1Start { get; set; }
        public int MinutesP1 { get; set; }
        public int SecondsP1 { get; set; }
        public int MinutesP2 { get; set; }
        public int SecondsP2 { get; set; }


        public string P1Name { get; set; }
        public Color P1Color { get; set; }
        public int P1PointsToPass { get; set; }
        public float P1TimeIncrement { get; set; }
        
        public string P2Name { get; set; }
        public Color P2Color { get; set; }
        public int P2PointsToPass { get; set; }
        public float P2TimeIncrement { get; set; }
        
        public bool DisplayHistory { get; set; }
        public bool LoopPlaylist { get; set; }
        public bool ShufflePlaylist { get; set; }
        public bool SinglePlayer { get; set; }

        public float AutoPause { get; set; }
        public bool QuickRounds { get; set; }
        public float QuickRoundLength { get; set; }

        public string UpdateURL { get; set; }

        public void SaveSettings()
        {
            using (var writer = new StreamWriter(SETTINGS_FILE_LOC))
            {
                writer.WriteLine($"P1Start={P1Start}");
                writer.WriteLine($"MinutesP1={MinutesP1}");
                writer.WriteLine($"SecondsP1={SecondsP1}");
                writer.WriteLine($"MinutesP2={MinutesP2}");
                writer.WriteLine($"SecondsP2={SecondsP2}");
                writer.WriteLine($"P1Name={P1Name}");
                writer.WriteLine($"P1Color={ColorTranslator.ToHtml(P1Color)}");
                writer.WriteLine($"P1PointsToPass={P1PointsToPass}");
                writer.WriteLine($"P1TimeIncrement={P1TimeIncrement}");
                writer.WriteLine($"P2Name={P2Name}");
                writer.WriteLine($"P2Color={ColorTranslator.ToHtml(P2Color)}");
                writer.WriteLine($"P2PointsToPass={P2PointsToPass}");
                writer.WriteLine($"P2TimeIncrement={P2TimeIncrement}");
                writer.WriteLine($"DisplayHistory={DisplayHistory}");
                writer.WriteLine($"LoopPlaylist={LoopPlaylist}");
                writer.WriteLine($"ShufflePlaylist={ShufflePlaylist}");
                writer.WriteLine($"SinglePlayer={SinglePlayer}");
                writer.WriteLine($"AutoPause={AutoPause}");
                writer.WriteLine($"QuickRounds={QuickRounds}");
                writer.WriteLine($"QuickRoundLength={QuickRoundLength}");
                writer.WriteLine($"UpdateURL={UpdateURL}");
            }
        }

        private static bool LoadBool(string value)
        {
            if (bool.TryParse(value, out var parsedBool))
            {
                return parsedBool;
            }

            throw new Exception($"Failed to load boolean value: {value}");
        }

        private static int LoadInt(string value)
        {
            if (int.TryParse(value, out var parsedInt))
            {
                return parsedInt;
            }

            throw new Exception($"Failed to load int value: {value}");
        }
        
        private static Color LoadColor(string value)
        {
            var outColor = ColorTranslator.FromHtml(value);
            if ( outColor != Color.Empty)
            {
                return outColor;
            }

            throw new Exception($"Failed to load Color {value}");
        }
        
        private static float LoadFloat(string value)
        {
            if (float.TryParse(value, out var parsedFloat))
            {
                return parsedFloat;
            }

            throw new Exception($"Failed to load float value: {value}");
        }

        public void SetDefaultSettings()
        {
            P1Start = true;
            MinutesP1 = 2;
            SecondsP1 = 30;
            MinutesP2 = 2;
            SecondsP2 = 30;
            P1Name = "Player 1";
            P1Color = Color.Green;
            P1PointsToPass = 2;
            P1TimeIncrement = 2.0f;
            P2Name = "Player 2";
            P2Color = Color.Blue;
            P2PointsToPass = 2;
            P2TimeIncrement = 2.0f;
            DisplayHistory = true;
            LoopPlaylist = true;
            ShufflePlaylist = true;
            SinglePlayer = false;
            AutoPause = 4;
            QuickRounds = false;
            QuickRoundLength = 1.0f;
            UpdateURL = "";
            SaveSettings();
        }

        public bool LoadSettings()
        {
            // Combine the base folder with your specific folder....
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MusicBee");

            // Check if folder exists and if not, create it
            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);


            Console.WriteLine("Loading Settings");
            if (!File.Exists(SETTINGS_FILE_LOC))
            {
                Console.WriteLine("SettingsFile did not exist, creating new one");
                SetDefaultSettings();
            }

            try
            {
                using (var reader = new StreamReader(SETTINGS_FILE_LOC))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var parts = line.Split('=');
                        if (parts.Length == 2)
                        {
                            var key = parts[0].Trim();
                            var value = parts[1].Trim();

                            switch (key)
                            {
                                case "P1Start":
                                    P1Start = LoadBool(value);
                                    break;
                                case "MinutesP1":
                                    MinutesP1 = LoadInt(value);
                                    break;
                                case "SecondsP1":
                                    SecondsP1 = LoadInt(value);
                                    break;
                                case "MinutesP2":
                                    MinutesP2 = LoadInt(value);
                                    break;
                                case "SecondsP2":
                                    SecondsP2 = LoadInt(value);
                                    break;
                                case "P1Name":
                                    P1Name = value;
                                    break;
                                case "P1Color":
                                    P1Color = LoadColor(value);
                                    break;
                                case "P1PointsToPass":
                                    P1PointsToPass = LoadInt(value);
                                    break;
                                case "P1TimeIncrement":
                                    P1TimeIncrement = LoadFloat(value);
                                    break;
                                case "P2Name":
                                    P2Name = value;
                                    break;
                                case "P2Color":
                                    P2Color = LoadColor(value);
                                    break;
                                case "P2PointsToPass":
                                    P2PointsToPass = LoadInt(value);
                                    break;
                                case "P2TimeIncrement":
                                    P2TimeIncrement = LoadFloat(value);
                                    break;
                                case "DisplayHistory":
                                    DisplayHistory = LoadBool(value);
                                    break;
                                case "LoopPlaylist":
                                    LoopPlaylist = LoadBool(value);
                                    break;
                                case "ShufflePlaylist":
                                    ShufflePlaylist = LoadBool(value);
                                    break;
                                case "SinglePlayer":
                                    SinglePlayer = LoadBool(value);
                                    break;
                                case "AutoPause":
                                    AutoPause = LoadFloat(value);
                                    break;
                                case "QuickRounds":
                                    QuickRounds = LoadBool(value);
                                    break;
                                case "QuickRoundLength":
                                    QuickRoundLength = LoadFloat(value);
                                    break;
                                case "UpdateURL":
                                    UpdateURL = value;
                                    break;
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($@"Failed to load settings: {ex.Message}");
                return false;
            }
        }
    }

    
}