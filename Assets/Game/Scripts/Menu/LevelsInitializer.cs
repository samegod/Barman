using UnityEngine;

namespace Stories.Childrens.Field.CarRacing
{
	public class LevelsInitializer : MonoBehaviour
	{
		[SerializeField] private int levelsNumber;
		[SerializeField] private UIObjectsLine objectsLine;
		[SerializeField] private LevelsPanel panelPrefab;

		private int _lastCreatedLevel;
		
		private void Awake()
		{
			InitializeLevels();
		}

		private void InitializeLevels()
		{
			int neededPanelsNumber = levelsNumber / panelPrefab.MaxLevelsOnPanel;
			
			if (levelsNumber % panelPrefab.MaxLevelsOnPanel > 0)
				neededPanelsNumber++;

			for (int i = 0; i < neededPanelsNumber; i++)
			{
				LevelsPanel newPanel = Instantiate(panelPrefab, objectsLine.transform);
				InitPanel(newPanel);
				AddPanelToLine(newPanel);
			}
		}

		private void InitPanel(LevelsPanel panel)
		{
			int levelsForPanel;

			if (levelsNumber - _lastCreatedLevel > panelPrefab.MaxLevelsOnPanel)
			{
				levelsForPanel = 9;
			}
			else
			{
				levelsForPanel = levelsNumber - _lastCreatedLevel;
			}
				
			panel.SetLevels(levelsForPanel, _lastCreatedLevel);
			_lastCreatedLevel += levelsForPanel;
		}

		private void AddPanelToLine(Component panel)
		{
			objectsLine.AddNewObject(panel.transform);
		}
	}
}
