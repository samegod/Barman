using UnityEngine;

namespace Game
{
    public class Trajectory : MonoBehaviour
    {
        [SerializeField] int dotsNumber;
        [SerializeField] private GameObject dotsParent;
        [SerializeField] private GameObject dotsPrefab;
        [SerializeField] private float dotSpacing;
        [SerializeField] private float detentionFacility = 2500;

        private Vector3 _pos;
        private float _timeStamp;
        private Transform[] _dotsList;

        private void Start()
        {
            Hide();

            PrepareDots();
        }

        void PrepareDots()
        {
            _dotsList = new Transform[dotsNumber];

            for (int i = 0; i < dotsNumber; i++)
            {
                _dotsList[i] = Instantiate(dotsPrefab, null).transform;
                _dotsList[i].parent = dotsParent.transform;

                if (i > 0)
                {
                    Vector3 scale = new Vector3(_dotsList[i - 1].transform.localScale.x - 0.01f, _dotsList[i - 1].transform.localScale.y - 0.01f, 1);
                    _dotsList[i].transform.localScale = scale;
                }
            }
        }

        public void UpdateDots(Vector3 ballPos, Vector2 forceApplied)
        {
            _timeStamp = dotSpacing;

            for (int i = 0; i < dotsNumber; i++)
            {
                _pos.x = (ballPos.x + forceApplied.x / detentionFacility * _timeStamp);
                _pos.y = ballPos.y;
                _pos.z = ballPos.z;

                _dotsList[i].position = _pos;
                _timeStamp += dotSpacing;
            }
        }

        public void Show() =>
            dotsParent.SetActive(true);

        public void Hide() =>
            dotsParent.SetActive(false);
    }
}
