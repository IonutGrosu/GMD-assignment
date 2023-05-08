namespace StarterAssets.Managers
{
    public interface ISaveManager
    {
        void LoadData(SaveItems data);
        void SaveData(SaveItems data);
    }
}