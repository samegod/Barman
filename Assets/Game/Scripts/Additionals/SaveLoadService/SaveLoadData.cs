using System;

namespace Game.Scripts.Additions.SaveLoadService
{
	[Serializable]
	public class SaveLoadData : ISaveLoadData
	{
		protected const string DataPath = "SaveLoadData";
		protected const string TypeOfData = ".dat";

		public virtual string SaveDataPath => "/" + DataPath + TypeOfData;

		protected string GeneratePathWithKey(string key) => "/" + key + DataPath + TypeOfData;
	}
}