using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace MainMenu
{
    public enum ContentType
    {
        Levels, 
        Shop,
    }

    public class BookContent : MonoBehaviour
    {
        [SerializeField] private ContentType contentType;
        [SerializeField] private List<Page> pages;

        private int _currentPage;
        
        public ContentType Type => contentType;

        public void Activate()
        {
            
        }

        public void FlipToNextPage()
        {
            if (_currentPage >= pages.Count - 2)
                return;

            _currentPage++;
            pages[_currentPage].transform.DORotate(new Vector3(0, 175, 0), 1f);
        }

        public void FlipToPrevious()
        {
            if (_currentPage >= pages.Count - 2)
                return;

            _currentPage++;
            pages[_currentPage].transform.DORotate(new Vector3(0, 175, 0), 1f);
        }
    }
}
