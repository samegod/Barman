using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainMenu
{
	[Serializable]
	public class LevelStars
	{
		public int ID;
		public int Stars;

		public LevelStars(int id, int stars)
		{
			ID = id;
			Stars = stars;
		}
        
		public LevelStars(int id)
		{
			ID = id;
			Stars = 0;
		}
	}

	[Serializable]
	public class LevelsData// : SaveLoadData
	{
		//private const string Key = "LevelsData";

		//private int _lastPassedLevel;
		//private readonly List<LevelStars> _levels = new List<LevelStars>();

		//public override string SaveDataPath => GeneratePathWithKey(Key);
		//public List<LevelStars> Levels => _levels;
		//public int LastPassedLevel
		//{
		//	get => _lastPassedLevel;
		//	set => _lastPassedLevel = value;
		//}
	}
}
