using DG.Tweening;
using UnityEngine;

namespace MainMenu
{
	public class Book : MonoBehaviour
	{
		[SerializeField] private Transform closedPoint;
		[SerializeField] private Transform openedPoint;
		[SerializeField] private Transform leftHalf;
		[SerializeField] private Transform rightHalf;
		[SerializeField] private Transform pagePrefab;

		private const float _flipTime = 1f;

		public void Open()
		{
			leftHalf.DOLocalRotate(new Vector3(0, 0, 175), _flipTime);
			rightHalf.DOLocalRotate(new Vector3(0, 0, 5), _flipTime);
			
			SetPoint(openedPoint, _flipTime);
		}

		public void Close()
		{
			leftHalf.DOLocalRotate(Vector3.zero, _flipTime);
			rightHalf.DOLocalRotate(Vector3.zero, _flipTime);
			
			SetPoint(closedPoint, _flipTime);
		}

		public void FlipToNextPage()
		{

		}

		public void FlipToPreviousPage()
		{

		}

		private void SetPoint (Transform point, float time)
		{
			transform.DOMove(point.position, time);
			transform.DORotate(point.eulerAngles, time);
		}
	}
}