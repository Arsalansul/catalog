using UnityEngine;

namespace Assets.Scripts
{
    public class OrientationSettings : MonoBehaviour
    {
        [System.Serializable]
        public class Portrait
        {
            public Vector2 anchorMax;
            public Vector2 anchorMin;
            public Vector2 pivot;
            public Vector3 anchoredPosition;
        }

        [System.Serializable]
        public class Landscape
        {
            public Vector2 anchorMax;
            public Vector2 anchorMin;
            public Vector2 pivot;
            public Vector3 anchoredPosition;
        }

        public Portrait portrait;
        public Landscape landscape;

        public void SetPortraitValues()
        {
            var rectTransform = GetComponent<RectTransform>();

            rectTransform.anchorMax = portrait.anchorMax;
            rectTransform.anchorMin = portrait.anchorMin;
            rectTransform.pivot = portrait.pivot;
            rectTransform.anchoredPosition = portrait.anchoredPosition;
        }

        public void SetLandscapeValues()
        {
            var rectTransform = GetComponent<RectTransform>();

            rectTransform.anchorMax = landscape.anchorMax;
            rectTransform.anchorMin = landscape.anchorMin;
            rectTransform.pivot = landscape.pivot;
            rectTransform.anchoredPosition = landscape.anchoredPosition;
        }
    }
}
