using System;
using System.IO;
using UnityEngine;

namespace StarterAssets.Managers
{
    public class FileManager
    {
        private readonly string _dirPath;
        private readonly string _fileName;

        public FileManager(string dirPath, string fileName)
        {
            _dirPath = dirPath;
            _fileName = fileName;
        }

        public SaveItems Load()
        {
            var fullPath = Path.Combine(_dirPath, _fileName);
            var existingData = new SaveItems(Array.Empty<Item>(), 0, Array.Empty<int>());
            if (File.Exists(fullPath))
            {
                try
                {
                    var dataToLoad = "";
                    using (var stream = new FileStream(fullPath, FileMode.Open))
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            dataToLoad = reader.ReadToEnd();
                        }
                    }

                    JsonUtility.FromJsonOverwrite(dataToLoad, existingData);
                }
                catch (Exception e)
                {
                    Debug.LogError("Error occurred when trying to load data" + e);
                }
            }

            return existingData;
        }

        public void Save(SaveItems data)
        {
            var fullPath = Path.Combine(_dirPath, _fileName);
            try
            {
                var path = Path.GetDirectoryName(fullPath);
                if (path == null)
                {
                    throw new Exception("Error getting path " + fullPath);
                }
                Directory.CreateDirectory(path);
                Debug.Log("before json trying to write" + data.Items[0]);
                var toJson = JsonUtility.ToJson(data);
                Debug.Log("Trying to write \n" + toJson);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    using (var streamWriter = new StreamWriter(stream))
                    {
                        streamWriter.Write(toJson);
                    }
                }
                
                Debug.Log("Finished saving to file");
            }
            catch (Exception e)
            {
                Debug.LogError("Error while trying to save" + e);
            }
        }
    }
}