using System.Collections.Generic;
using UnityEngine;

namespace MainMenu
{
    public class LevelsPanel : MonoBehaviour
    {
        [SerializeField] private int maxLevelsOnPanel;
        [SerializeField] private LevelButton buttonPrefab;

        private readonly List<LevelButton> _levels = new List<LevelButton>();

        public int MaxLevelsOnPanel => maxLevelsOnPanel;
        public List<LevelButton> Levels => _levels;

        public void SetLevels (int levelsNumber, int startingLevelNumber)
        {
            for (int i = 0; i < levelsNumber; i++)
            {
                LevelButton newButton = Instantiate(buttonPrefab, transform);
                newButton.SetLevelNumber(startingLevelNumber + i + 1);

                Levels.Add(newButton);
            }
        }
    }
}
