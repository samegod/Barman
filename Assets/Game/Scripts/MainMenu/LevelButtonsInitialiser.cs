using System.Collections.Generic;
using UnityEngine;

namespace MainMenu
{
	public class LevelButtonsInitialiser : MonoBehaviour
	{
		[SerializeField] private List<SceneManagement.Scenes> scenes;
		[SerializeField] private Transform levelPanelsContainer;
		[SerializeField] private LevelsPanel panelPrefab;

		private LevelsProgressInitializer _progressInitializer = new LevelsProgressInitializer();
		private int _lastCreatedLevel;
		private List<LevelButton> _levels = new List<LevelButton>();

		private void Awake()
		{
			InitializeLevels();
		}

		private void InitializeLevels()
		{
			int neededPanelsNumber = scenes.Count / panelPrefab.MaxLevelsOnPanel;

			if (scenes.Count % panelPrefab.MaxLevelsOnPanel > 0)
				neededPanelsNumber++;

			for (int i = 0; i < neededPanelsNumber; i++)
			{
				LevelsPanel newPanel = Instantiate(panelPrefab, levelPanelsContainer);
				InitPanel(newPanel);
				AddPanelToLine(newPanel);
			}

			_progressInitializer.InitLevels(_levels);
		}

		private void InitPanel (LevelsPanel panel)
		{
			int levelsForPanel;

			if (scenes.Count - _lastCreatedLevel > panelPrefab.MaxLevelsOnPanel)
			{
				levelsForPanel = 9;
			}
			else
			{
				levelsForPanel = scenes.Count - _lastCreatedLevel;
			}

			panel.SetLevels(levelsForPanel, _lastCreatedLevel);
			_lastCreatedLevel += levelsForPanel;

			_levels.AddRange(panel.Levels);
		}

		private void AddPanelToLine (Component panel)
		{
			//objectsLine.AddNewObject(panel.transform);
		}
	}
}