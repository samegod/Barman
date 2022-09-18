using DG.Tweening;
using UnityEngine;

public class Page : MonoBehaviour
{
	[SerializeField] private RectTransform frontSide;
	[SerializeField] private RectTransform backSide;

	private const float _flipTime = 1f;
	private const float _fullOpenAngle = 179f;
	private const float _openAngle = 175f;
	private const float _preopenAngle = 5f;
	private const float _closedAngle = 1f;
	
	public void Open()
	{
		FlipPage(_openAngle);
	}

	public void PreOpen()
	{
		FlipPage(_preopenAngle);
	}
	
	public void Close()
	{
		FlipPage(_closedAngle);
	}

	public void FullOpen()
	{
		FlipPage(_fullOpenAngle);
	}

	private void FlipPage(float angle)
	{
		transform.DOLocalRotate(new Vector3(0, 0, angle), _flipTime);
	}
}
