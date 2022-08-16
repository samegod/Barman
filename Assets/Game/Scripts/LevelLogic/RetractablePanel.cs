using Additions.UI;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.LevelLogic
{
	public class RetractablePanel : RetractableUiElement
	{
		[SerializeField] private Image fade;

		public override void Show(float time = 0.4f)
		{
			base.Show(time);

			rectTransform.DOScale(1f, time);
			fade.DOFade(0.65f, time * 2);
		}

		public override void Hide(float time = 0.4f)
		{
			base.Hide(time);

			rectTransform.DOScale(0f, time);
			fade.DOFade(0f, time * 2);

		}
	}
}