using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UIObjectsLine : MonoBehaviour
{
	[SerializeField] private List<RectTransform> objects;

	private RectTransform _myRect;
	private int _currentObjectID;

	private void Awake()
	{
		_myRect = GetComponent<RectTransform>();
	}

	public void SetNewLine (List<RectTransform> newLine)
	{
		objects = newLine;
	}

	public void AddNewObject (Transform newObject)
	{
		RectTransform newObjectRect = newObject.GetComponent<RectTransform>();

		if (newObjectRect)
		{
			objects.Add(newObjectRect);
		}
	}
	
	public void MoveToNext()
	{
		if (_currentObjectID != objects.Count - 1)
		{
			_currentObjectID++;
			_myRect.DOAnchorPos(objects[_currentObjectID].localPosition * -1, 0.4f);
			}
	}

	public void MoveToPrevious()
	{
		if (_currentObjectID != 0)
		{
			_currentObjectID--;
			_myRect.DOAnchorPos(objects[_currentObjectID].localPosition * -1, 0.4f);
		}
	}
}
