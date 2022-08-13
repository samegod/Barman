using UnityEngine;

namespace Stories.Childrens.Field.CarRacing
{
	public class LevelsPanel : MonoBehaviour
	{
		[SerializeField] private int maxLevelsOnPanel;
		[SerializeField] private LevelButton buttonPrefab;
		
		public int MaxLevelsOnPanel => maxLevelsOnPanel;

		public void SetLevels (int levelsNumber, int startingLevelNumber)
		{
			for (int i = 0; i < levelsNumber; i++)
			{
				LevelButton newButton = Instantiate(buttonPrefab, transform);
				newButton.SetLevelNumber(startingLevelNumber + i + 1);
			}
		}
	}
}
