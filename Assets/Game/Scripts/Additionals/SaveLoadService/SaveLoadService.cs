using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Game.Scripts.Additions.SaveLoadService
{
	public static class SaveLoadService
	{
		public static void SaveData<TData>(TData data) where TData : ISaveLoadData
		{
			var binaryFormatter = new BinaryFormatter();
			using (FileStream fileStream = File.Create(Application.persistentDataPath + data.SaveDataPath))
				binaryFormatter.Serialize(fileStream, data);
		}

		public static TData LoadData<TData>(TData data) where TData : ISaveLoadData
		{
			var binaryFormatter = new BinaryFormatter();
			using (FileStream fileStream = File.Open(Application.persistentDataPath + data.SaveDataPath, FileMode.Open))
			{
				var loadData = (TData) binaryFormatter.Deserialize(fileStream);

				return loadData;
			}
		}
	}
}