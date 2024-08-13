using System;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Nodes;
using BomberWars_MP.DataAccess;
using static System.Net.Mime.MediaTypeNames;
using BomberWars_MP.Model;

namespace BomberWars_MP.DataAccess
{
    /// <summary>
    /// Handles the BomberWars Data structure and all the IO operations.
    /// Reads all the files that contains data for maps.
    /// Saves and loads a Model object's all important data.
    /// Every generated file will located in AppData//BomberWars directory.
    /// </summary>
    public class DataAccess
    {
        private string? _firstFilePath;
        private string? _secondFilePath;
        private string? _thirdFilePath;
        private string? _bomberWarsDirectory;
        private bool _unitTest;

        /// <summary>
        /// Constructor for DataAccess class.
        /// Sets all paths.
        /// If testing the paths will be relative paths.
        /// </summary>
        /// <param name="isTesting">Testing the data access or not</param>
        /// <exception cref="DataAccessExeption">If IO exception rased.</exception>
        public DataAccess(bool isTesting=false)
        {
            _unitTest = isTesting;
            if (_unitTest)
            {
                string testResourcesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "maps");
                _firstFilePath = Path.Combine(testResourcesPath, "map_one.json");
                _secondFilePath = Path.Combine(testResourcesPath, "map_two.json");
                _thirdFilePath = Path.Combine(testResourcesPath, "map_three.json");
            }
            else
            {
                #region Solution with own folder

                string? firstFileName = "map_one.json";
                string? secondFileName = "map_two.json";
                string? thirdFileName = "map_three.json";
                string folderpath = "";
                string path;
                string dir = Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.Parent!.Parent!.FullName; ;
                folderpath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                _bomberWarsDirectory = folderpath + "\\BomberWars";
                path = _bomberWarsDirectory + "\\maps";
                try
                {
                    // if the directory doesnt exists
                    if (!Directory.Exists(path))
                    {
                        // try to create the directory
                        Directory.CreateDirectory(path);
                    }
                    if (!File.Exists(path + "\\map_one.json"))
                    {
                        try //to copy the json files
                        {
                            // create the mapfiles
                            var map_one = new FileInfo(dir + "\\BomberWars_MP\\DataAccess\\maps\\map_one.json");
                            map_one.CopyTo(path + "\\map_one.json");
                        }
                        catch (Exception e)
                        {
                            throw new DataAccessExeption(e.Message);
                        }
                    }
                    if (!File.Exists(path + "\\map_two.json"))
                    {
                        try //to copy the json files
                        {
                            // create the mapfiles
                            var map_two = new FileInfo(dir + "\\BomberWars_MP\\DataAccess\\maps\\map_two.json");
                            map_two.CopyTo(path + "\\map_two.json");
                        }
                        catch (Exception e)
                        {
                            throw new DataAccessExeption(e.Message);
                        }
                    }

                    if (!File.Exists(path + "\\map_three.json"))
                    {
                        try //to copy the json files
                        {
                            // create the mapfiles
                            var map_three = new FileInfo(dir + "\\BomberWars_MP\\DataAccess\\maps\\map_three.json");
                            map_three.CopyTo(path + "\\map_three.json");
                        }
                        catch (Exception e)
                        {
                            throw new DataAccessExeption(e.Message);
                        }
                    }

                    _firstFilePath = Path.Combine(path, firstFileName);
                    _secondFilePath = Path.Combine(path, secondFileName);
                    _thirdFilePath = Path.Combine(path, thirdFileName);

                }
                catch (Exception e)
                {
                    throw new DataAccessExeption(e.Message);
                }
                #endregion
            }
        }

        /// <summary>
        /// Gets the first map
        /// </summary>
        /// <returns>All data of the first map</returns>
        /// <exception cref="DataAccessExeption">If IO os JSON exception raised.</exception>
        public Data GetFirstMap()
        {
            if (File.Exists(_firstFilePath))
            {
                string dataString = File.ReadAllText(_firstFilePath);

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };


                try
                {

                    Data? data = JsonSerializer.Deserialize<Data>(dataString);
                    return data!;
                }
                catch (JsonException)
                {
                    throw new DataAccessExeption();
                }
            }
            else
            {
                throw new DataAccessExeption();
            }
        }

        /// <summary>
        /// Gets the second map
        /// </summary>
        /// <returns>All data of the second map</returns>
        /// <exception cref="DataAccessExeption">If IO os JSON exception raised.</exception>
        public Data GetSecondMap()
        {
            if (File.Exists(_secondFilePath))
            {
                string dataString = File.ReadAllText(_secondFilePath);

                try
                {
                    Data? data = JsonSerializer.Deserialize<Data>(dataString);
                    return data!;
                }
                catch (JsonException)
                {
                    throw new DataAccessExeption();
                }
            }
            else
            {
                throw new DataAccessExeption();
            }
        }

        /// <summary>
        /// Gets the third map
        /// </summary>
        /// <returns>All data of the third map</returns>
        /// <exception cref="DataAccessExeption">If IO os JSON exception raised.</exception>
        public Data GetThirdMap()
        {
            if (File.Exists(_thirdFilePath))
            {
                string dataString = File.ReadAllText(_thirdFilePath);

                try
                {
                    Data? data = JsonSerializer.Deserialize<Data>(dataString);
                    return data!;
                }
                catch (JsonException)
                {
                    throw new DataAccessExeption();
                }
            }
            else
            {
                throw new DataAccessExeption();
            }
        }

        /// <summary>
        /// Saves the given Data object into a JSON file. The file name will be the current datetime
        /// and the standings.
        /// </summary>
        /// <param name="data"></param>
        public void SaveModel(Data data)
        {
            //unit test path
            if (_unitTest)
            {
                string testResourcesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "maps");
                string testpath = Path.Combine(testResourcesPath, "test_map.json");
                try
                {
                    string jsonString = JsonSerializer.Serialize(data);
                    File.WriteAllText(testpath, jsonString);
                }
                catch (JsonException)
                {
                    throw new DataAccessExeption();
                }
            }
            //regular save path
            else
            {
                string path = _bomberWarsDirectory + "\\saves";
                // if the directory doesnt exists
                if (!Directory.Exists(path))
                {
                    // try to create the directory
                    Directory.CreateDirectory(path);
                }
                string filename = DateTime.Now.ToString("yyyy_MM_dd_HH_mm") + "_";
                if (data._numberOfPlayers == 2)
                {
                    filename = filename + data._playersData![0][3].ToString() + "_" + data._playersData[1][3].ToString() + ".json";
                }
                else
                {
                    filename = filename + data._playersData![0][3].ToString() + "_" + data._playersData[1][3].ToString() + "_" + data._playersData[2][3].ToString() + ".json";
                }
                string fullpath = Path.Combine(path, filename);

                try
                {
                    if (!File.Exists(fullpath))
                    {
                        string jsonString = JsonSerializer.Serialize(data);
                        File.WriteAllText(fullpath, jsonString);
                    }
                }
                catch (JsonException)
                {
                    throw new DataAccessExeption();
                }
            }
        }

        /// <summary>
        /// Tries to read the given file path into a Data object, and returns that object. The file path must ends with a .json.
        /// If any issue appears whiles reading, raising new DataAccessException.
        /// </summary>
        /// <param name="file_path"></param>
        /// <returns>A Data object, that contains all data for loading a new model object.</returns>
        public Data LoadModel(string file_path)
        {

            if (File.Exists(file_path))
            {
                string dataString = File.ReadAllText(file_path);

                try
                {
                    Data? data = JsonSerializer.Deserialize<Data>(dataString);
                    return data!;
                }
                catch (JsonException)
                {
                    throw new DataAccessExeption();
                }
            }
            else
            {
                throw new DataAccessExeption();
            }
        }

        /// <summary>
        /// Lists all saves from the appdata\\bomberwars\\saves directory. Returns a tuple, that contains 2 lists.
        /// The first list contains all saves' full path. The second one contains the title of the saves, created by
        /// the createSubtitle function.
        /// </summary>
        /// <returns></returns>
        public static (List<string>, List<string>) ListSaves()
        {
            string folderpath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string path = folderpath + "\\BomberWars\\saves";
            // if the directory doesnt exists
            if (!Directory.Exists(path))
            {
                // try to create the directory
                DirectoryInfo di = Directory.CreateDirectory(path);
            }

            int fileCount = Directory.GetFiles(path, "*", SearchOption.AllDirectories).Length;

            List<string> savesPath = new List<string>();
            List<string> savesSubtitle = new List<string>();

            if (fileCount <= 0)
            {
                return (savesPath, savesSubtitle);
            }

            var files = from file in Directory.EnumerateFiles(path) select file;
            foreach (var file in files)
            {
                if (File.Exists(file) && file.EndsWith(".json"))
                {
                    savesPath.Add(file);
                    savesSubtitle.Add(CreateSubtitle(Path.GetFileName(file)));
                }
            }


            return (savesPath, savesSubtitle);
        }

        /// <summary>
        /// Reformats the file name. The new string contains the datetime, and the standigs of the match.
        /// </summary>
        /// <param name="input">The file name</param>
        /// <returns>The reformatted file name</returns>
        static string CreateSubtitle(string input)
        {
            string cleaned = input.Replace(".json", "").Replace("_", "");

            string year = cleaned.Substring(0, 4);
            string month = cleaned.Substring(4, 2);
            string day = cleaned.Substring(6, 2);
            string hour = cleaned.Substring(8, 2);
            string minute = cleaned.Substring(10, 2);

            string playerA = cleaned.Substring(12, 1);
            string playerB = cleaned.Substring(13, 1);
            if (cleaned.Length == 15)
            {
                string playerC = cleaned.Substring(14, 1);
                return $"{year}-{month}-{day} {hour}:{minute} Player A: {playerA} Player B: {playerB} Player C: {playerC}";
            }
            else
            {
                return $"{year}-{month}-{day} {hour}:{minute} Player A: {playerA} Player B: {playerB}";
            }
        }

        /// <summary>
        /// Deletes the given path's file.
        /// </summary>
        /// <param name="path">The fullpath to the file</param>
        /// <returns>True it the deleteion was successful, false otherwise</returns>
        public static bool DeleteSave(string path)
        {
            FileInfo file = new FileInfo(path);
            if (File.Exists(path) && path.EndsWith(".json"))
            {
                file.Delete();
                return true;
            }
            return false;
        }
    }
}
