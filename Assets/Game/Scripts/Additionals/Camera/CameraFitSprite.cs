using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityIC
{
    [RequireComponent(typeof(Camera))]
    public class CameraFitSprite : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer m_Sprite = null;

        [SerializeField]
        private FitType m_FitType = default;

        private Camera m_Camera = null;

        private void Awake()
        {
            m_Camera = GetComponent<Camera>();

            FitSprite();
        }

        private void FitSprite()
        {
            m_Camera.orthographicSize = GetTargetOrthoSize(m_Sprite, m_FitType);
        }

        public static float GetTargetOrthoSize(SpriteRenderer sprite, FitType fitType = FitType.Horizontal)
        {
            float targetOrtho = 0;

            float screenRatio = (float)Screen.width / (float)Screen.height;
            float targetRatio = sprite.bounds.size.x / sprite.bounds.size.y;

            float differenceInSize = targetRatio / screenRatio;

            switch (fitType)
            {
                case FitType.Full:
                    if (differenceInSize > 1)
                    {
                        targetOrtho = sprite.bounds.size.y / 2;
                    }
                    else
                    {
                        targetOrtho = (sprite.bounds.size.y / 2) * differenceInSize;
                    }
                    break;
                case FitType.Horizontal:
                    targetOrtho = (sprite.bounds.size.y / 2) * differenceInSize;
                    break;
                case FitType.Vertical:
                    targetOrtho = sprite.bounds.size.y / 2;
                    break;
                default:
                    break;
            }
            
            return targetOrtho;
        }

        public enum FitType
        {
            Full = 0,
            Horizontal = 1,
            Vertical = 2
        }
    }
}