using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace StarterAssets.Managers
{
    public class SaveManager:MonoBehaviour
    {
        [SerializeField] private string fileName;
        [SerializeField] private InventorySlot[] startingItems;
        private static SaveManager Instance { get; set; }
        private SaveItems _save;
        private List<ISaveManager> _objects;
        private FileManager _fileManager;

        private void Awake()
        {
            if(Instance != null) Debug.LogError("more than one instance for save manager.");
            Instance = this;
        }

        private void Start()
        {
            _fileManager = new FileManager(Application.persistentDataPath, fileName);
            _objects = FindAllSaveManagers();
            LoadGame();
        }

        private static List<ISaveManager> FindAllSaveManagers()
        {
            var foundObjects = FindObjectsOfType<MonoBehaviour>().OfType<ISaveManager>();
            return new List<ISaveManager>(foundObjects);
        }

        private void NewGame()
        {
            var items = (from inventorySlot in startingItems select inventorySlot.GetComponentInChildren<InventoryItem>().item into item where item select item).ToList();
            _save.Items = items.ToArray();
            _save.Coins = 0;
            _save.Counts = (from inventorySlot in startingItems select inventorySlot.GetComponentInChildren<InventoryItem>().count into item select item).ToArray();
        }

        public void LoadGame()
        {
            var existingData = _fileManager.Load();

            if (existingData == null)
            {
                Debug.Log("No data was found, initializing to default.");
                NewGame();
            }
            else
            {
                _save = existingData;
            }

            foreach (var manager in _objects)
            {   
                manager.LoadData(_save);
            }
            
            Debug.Log("Loaded data" + _save);
        }

        public void SaveGame()
        {
            foreach (var manager in _objects)
            {
                manager.SaveData(_save);
            }
            
            Debug.Log("Saved data" + _save);
            
            _fileManager.Save(_save);
        }

        private void OnApplicationQuit()
        {
            SaveGame();
        }
    }
}