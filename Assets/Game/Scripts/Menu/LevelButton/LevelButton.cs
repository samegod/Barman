using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Stories.Childrens.Field.CarRacing
{
    public class LevelButton : MonoBehaviour, IPointerDownHandler
    {
        public static Action<int> OnLevelSelected;
        public static Action OnButtonPushed;

        [SerializeField] private int levelNumber;
        //[SerializeField] private LevelProgress progress;
        //[SerializeField] private SelectPointer selection;
        [SerializeField] private Text levelText;
        [SerializeField] private Image lockImage;

        private bool _isSelected;
        private bool _isLocked;

        private void Awake()
        {
            Unlock();
        }

        private void OnEnable()
        {
            OnButtonPushed += Unselect;
        }

        private void OnDisable()
        {
            OnButtonPushed -= Unselect;
        }

        public void SetLevelNumber(int number)
        {
            levelNumber = number;
            levelText.text = levelNumber.ToString();
        }

        public void Unlock()
        {
            _isLocked = false;
            lockImage.gameObject.SetActive(false);
            //progress.gameObject.SetActive(true);
        }

        public void Unlock (int stars)
        {
            Unlock();
            
            //progress.ShowStars(stars);
        }
        
        
        public void Lock()
        {
            _isLocked = true;
            lockImage.gameObject.SetActive(true);
            //progress.gameObject.SetActive(false);
        }

        public void Select()
        {
            if (!_isSelected)
            {
                _isSelected = true;
                //selection.ShowSelected();
                
                OnLevelSelected?.Invoke(levelNumber);
            }
        }

        public void Unselect()
        {
            if (_isSelected)
            {
                //selection.HideSelected();
                _isSelected = false;
            }
        }

        public void OnPointerDown (PointerEventData eventData)
        {
            if (!_isLocked)
            {
                OnButtonPushed?.Invoke();
                Select();
            }
        }
    }
}
