using UnityEngine.EventSystems;
using UnityEngine;
using System;

namespace Additions.UI.Buttons
{
	public class OpenRetractableElement : MonoBehaviour, IPointerClickHandler
	{
		[SerializeField] private RetractableUiElement elementToOpen;

		public event Action OnClick;

		public void SetElementToOpen(RetractableUiElement element)
		{
			elementToOpen = element;
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			OnClick?.Invoke();
			elementToOpen.Show();
		}
	}
}