using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

[System.Serializable]
static class GameSaveUtils
{
    public static void SaveGame(PlayerData playerData)
    {

        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream($"{Application.persistentDataPath}/{playerData.Name}_saveFile.sav", FileMode.Create, FileAccess.Write);

        formatter.Serialize(stream, playerData);
        stream.Close();
    }

    public static PlayerData LoadGame(string saveFile)
    {
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream($"{Application.persistentDataPath}/{saveFile}_saveFile.sav", FileMode.Open, FileAccess.Read);

        var playerData = (PlayerData)formatter.Deserialize(stream);
        stream.Close();

        return playerData;
    }
}
