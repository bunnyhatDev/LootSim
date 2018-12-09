using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {
	public static void SaveData(GameManager gm) {
		BinaryFormatter formatter = new BinaryFormatter();
		string path = Application.persistentDataPath + "/stats.dat";
		
		using (var file = File.Open(path, FileMode.OpenOrCreate)) {
			SaveLoadData data = new SaveLoadData(gm);
			formatter.Serialize(file, data);
			file.Close();
		}
	}

	public static SaveLoadData LoadData() {
		string path = Application.persistentDataPath + "/stats.dat";
		if(File.Exists(path)) {
			BinaryFormatter formatter = new BinaryFormatter();
			using (var file = File.Open(path, FileMode.Open)) {
				SaveLoadData data = formatter.Deserialize(file) as SaveLoadData;
				file.Close();

				return data;
			}
		} else {
			Debug.LogError("Save file not found in " + path);
			return null;
		}
	}
}
