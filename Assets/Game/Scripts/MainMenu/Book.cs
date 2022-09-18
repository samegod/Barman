using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace MainMenu
{
	public class Book : MonoBehaviour
	{
		[SerializeField] private Transform closedPoint;
		[SerializeField] private Transform openedPoint;
		[SerializeField] private List<Page> pages;

		private int _currentPage = 0;

		private const float _pageFlipDelay = 0.3f;
		private const float _flipTime = 1f;

		public void NextPage()
		{
			if (_currentPage >= pages.Count - 1)
				return;

			Open();
			
			_currentPage++;
			pages[_currentPage].PreOpen();
			pages[_currentPage - 1].Open();

			if (_currentPage >= 2)
				pages[_currentPage - 2].FullOpen();
		}

		public void PreviousPage()
		{
			Close();
			
			pages[_currentPage].Close();

			if (_currentPage >= 1)
				pages[_currentPage - 1].PreOpen();

			if (_currentPage >= 2)
				pages[_currentPage - 2].Open();

			if (_currentPage > 0)
				_currentPage--;
		}

		public void OpenPage (Page page)
		{ 
			int pageId = pages.LastIndexOf(page);
			OpenPage(pageId);
		}
		
		public async void OpenPage(int number)
		{
			for (int i = 0; i <= number; i++)
			{
				if (_currentPage > number)
				{
					PreviousPage();
				}
				else if (_currentPage< number)
				{
					NextPage();
				}

				await Task.Delay((int)(_pageFlipDelay * 1000));
			}
		}
		
		public void Open()
		{
			if (_currentPage == 0)
			{
				SetPoint(openedPoint, _flipTime);
			}
		}

		public void Close()
		{
			if (_currentPage == 1)
			{
				SetPoint(closedPoint, _flipTime);
			}
		}

		private void SetPoint (Transform point, float time)
		{
			transform.DOMove(point.position, time);
			transform.DORotate(point.eulerAngles, time);
		}
	}
}