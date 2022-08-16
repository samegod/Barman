using System.Collections.Generic;
using Additions.UI;
using UnityEngine;

namespace Game.Scripts.LevelLogic
{
	public class UiManager : MonoBehaviour
	{
		[SerializeField] private RetractablePanel winPanel;
		[SerializeField] private RetractablePanel loosePanel;
		[SerializeField] private RetractablePanel pausePanel;

		[SerializeField] private List<RetractableUiElement> lives;

		private int _currentIndex;

		public void Init(int countHealth)
		{
			for (var i = 0; countHealth != lives.Count; i++)
			{
				lives[i].gameObject.SetActive(false);
				lives.RemoveAt(i);
			}
		}

		public void HideHealthPoint() =>
			lives[_currentIndex++].Hide();

		public void ShowWinPanel() =>
			winPanel.Show();

		public void ShowLoosePanel() =>
			loosePanel.Show();

		public void ShowPausePanel() =>
			pausePanel.Show();
	}
}