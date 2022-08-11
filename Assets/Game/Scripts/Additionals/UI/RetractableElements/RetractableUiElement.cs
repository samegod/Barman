using System;
using DG.Tweening;
using UnityEngine;

namespace Additions.UI
{
	[RequireComponent(typeof(RectTransform))]
	public class RetractableUiElement : MonoBehaviour
	{
		[SerializeField] protected RectTransform rectTransform;
		[SerializeField] private Ease ease = Ease.Linear;
		[SerializeField] private Vector2 showPosition;
		[SerializeField] private Vector2 hiddenPosition;

		private bool _isShown;

		public bool IsShown => _isShown;

		public event Action OnPanelOpened;

		public event Action OnPanelClosed;

		public virtual void Show(float time = 0.4f)
		{
			_isShown = true;
			rectTransform.DOAnchorPos(showPosition, time)
				.SetEase(ease)
				.OnComplete(() => OnPanelOpened?.Invoke());
		}

		public virtual void Hide(float time = 0.4f)
		{
			_isShown = false;
			rectTransform.DOAnchorPos(hiddenPosition, time)
				.SetEase(ease)
				.OnComplete(() => OnPanelClosed?.Invoke());
		}

		[ContextMenu("CopyShowPosition")]
		public void CopyShowPosition() => showPosition = rectTransform.anchoredPosition;

		[ContextMenu("CopyHiddenPosition")]
		public void CopyHiddenPosition() => hiddenPosition = rectTransform.anchoredPosition;

		protected virtual void OnValidate()
		{
			if (rectTransform == null)
				rectTransform = GetComponent<RectTransform>();
		}
	}
}