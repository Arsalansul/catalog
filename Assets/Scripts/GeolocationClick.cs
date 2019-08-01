using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class GeolocationClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
    {
        public CountryInfo country;
        public float timeToSelect;

        private float time;

        private bool clicked;

        [HideInInspector]public MapManager mapManager;

        void Update()
        {
            if (clicked)
            {
                time += Time.deltaTime;

                if (time > timeToSelect)
                {
                    country.Selected = true;
                    country.needAddToSheet = true;
                    clicked = false;
                    mapManager.ClickedAndSelected(country);
                }
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            time = 0;
            clicked = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (clicked)
            {
                mapManager.Clicked(country);
            }
            clicked = false;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            clicked = false;
        }
    }
}
