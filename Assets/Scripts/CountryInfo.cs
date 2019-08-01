using System;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    public class CountryInfo
    {
        public string Name;
        public float Area;
        public float Population;
        public float VVP;
        public Vector3 Geolocation;
        public string modelPrefabName;

        [NonSerializedAttribute] public GameObject GeolocationGO;
        [NonSerializedAttribute] public RectTransform GeolocationRect;
        [NonSerializedAttribute] public GameObject SelectedGO;
        [NonSerializedAttribute] public RectTransform SelectedRect;
        [NonSerializedAttribute] public GameObject modelGO;

        [NonSerializedAttribute] public bool Selected;
        [NonSerializedAttribute] public bool needAddToSheet;
    }
}