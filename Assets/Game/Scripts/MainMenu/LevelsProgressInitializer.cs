using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainMenu
{
    public class LevelsProgressInitializer : MonoBehaviour
    {
        private LevelsData _data;

        public void InitLevels (List<LevelButton> levels)
        {
            //_data = LevelsSaveAndLoader.LoadLevelProgress();
            LockLevels(levels);
            SetStars(levels);
        }

        private void LockLevels (List<LevelButton> levels)
        {
            // for (int i = _data.LastPassedLevel + 1; i < levels.Count; i++)
            // {
            //     levels[i].Lock();
            // }
        }

        private void SetStars (List<LevelButton> levels)
        {
            // for (int i = _data.LastPassedLevel - 1; i >= 0; i--)
            // {
            //     levels[i].SetStars(_data.Levels[i].Stars);
            // }
            // if (levels[_data.LastPassedLevel])
            // {
            //     levels[_data.LastPassedLevel].SetStars(0);
            // }
        }
    }
}
