using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Stories.Childrens.Field.CarRacing
{
	public class SelectPointer : MonoBehaviour
	{
		[SerializeField] private float showDuration;
		
		private Image _image;

		private void Awake()
		{
			_image = GetComponent<Image>();
		}

		public void ShowSelected()
		{
			_image.DOFade(1, showDuration);
		}

		public void HideSelected()
		{
			_image.DOFade(0, showDuration);
		}
	}
}
