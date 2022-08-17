using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.LevelLogic
{
	public class RetractablePanel : MonoBehaviour
	{
		[SerializeField] protected Image fade;
		[SerializeField] private RectTransform rectTransform;

		public void Show(float time = 0.4f)
		{
			rectTransform.DOScale(1f, time);
			fade.DOFade(0.65f, time * 2);
		}

		public void Hide(float time = 0.4f)
		{
			rectTransform.DOScale(0f, time);
			fade.DOFade(0f, time * 2);
		}
	}
}