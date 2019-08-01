using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class MapManager
    {
        private Canvas canvas;
        private Camera camera;

        private Transform countryInfoPanel;
        private Text areaText;
        private Text vvpText;
        private Text populationText;

        private Transform selectedCountriesCountPanel;
        private Text selectedCountriesCountText;
        public int selectedCountriesCount = 0;

        private Transform clearSheetButtonTransform;

        private Transform geolocationParent;
        private readonly GameObject geolocation = Resources.Load<GameObject>("Prefabs/UI/Geolocation");

        private Transform selectedParent;
        private readonly GameObject selected = Resources.Load<GameObject>("Prefabs/UI/Selected");

        private CountryInfo[] countries;

        public MapManager(Canvas canvas, CountryInfo[] countries)
        {
            this.canvas = canvas;
            camera = Camera.main;

            this.countries = countries;

            countryInfoPanel = canvas.transform.Find("CountryInfo");
            areaText = countryInfoPanel.GetChild(1).GetComponent<Text>();
            vvpText = countryInfoPanel.GetChild(2).GetComponent<Text>();
            populationText = countryInfoPanel.GetChild(3).GetComponent<Text>();

            selectedCountriesCountPanel = canvas.transform.Find("SelectedCountriesCount");
            selectedCountriesCountText = selectedCountriesCountPanel.GetChild(0).GetComponent<Text>();

            clearSheetButtonTransform = canvas.transform.Find("Head").Find("ClearSheet");

            geolocationParent = new GameObject("GeolocationIcons").transform;
            geolocationParent.SetParent(canvas.transform);
            geolocationParent.SetSiblingIndex(0);

            selectedParent = new GameObject("SelectedIcons").transform;
            selectedParent.SetParent(canvas.transform);
            selectedParent.SetSiblingIndex(0);
        }

        public void SetValueInCountryInfoPanel(CountryInfo countryInfo)
        {
            areaText.text = $"Площадь {countryInfo.Area} \n___________________";
            vvpText.text = $"ВВП {countryInfo.VVP} \n___________________";
            populationText.text = $"Население {countryInfo.Population}";
            countryInfoPanel.gameObject.SetActive(true);
        }

        public void HideCanvas()
        {
            countryInfoPanel.gameObject.SetActive(false);
            canvas.gameObject.SetActive(false);
        }

        public void SetIconPosition(Vector3 worldPosition, RectTransform iconRect)
        {
            iconRect.position = camera.WorldToScreenPoint(worldPosition);
        }

        public void CreateIcons()
        {
            foreach (var country in countries)
            {
                //create geolocation icons
                var instance = Object.Instantiate(geolocation, geolocationParent);
                instance.GetComponent<GeolocationClick>().country = country;
                instance.GetComponent<GeolocationClick>().mapManager = this;

                country.GeolocationGO = instance;
                country.GeolocationRect = instance.GetComponent<RectTransform>();

                SetIconPosition(country.Geolocation, country.GeolocationRect);

                //create selected icons
                instance = Object.Instantiate(selected, selectedParent);

                country.SelectedGO = instance;
                country.SelectedRect = instance.GetComponent<RectTransform>();

                SetIconPosition(country.Geolocation, country.SelectedRect);
            }
        }

        public void ClickedAndSelected(CountryInfo country)
        {
            Clicked(country);

            SetIconPosition(country.Geolocation, country.SelectedRect);
            country.SelectedGO.SetActive(true);

            selectedCountriesCount++;
            SetValueInSelectedCountriesCountPanel();
            clearSheetButtonTransform.gameObject.SetActive(true);
        }

        public void Clicked(CountryInfo country)
        {
            DefualtGameObjectsActiveState();

            country.modelGO.SetActive(true);
            country.GeolocationGO.SetActive(false);

            if (selectedCountriesCount == 0)
                SetValueInCountryInfoPanel(country);
        }

        private void SetValueInSelectedCountriesCountPanel()
        {
            countryInfoPanel.gameObject.SetActive(false);
            selectedCountriesCountText.text = $"Выбрано {selectedCountriesCount} cтран";
            selectedCountriesCountPanel.gameObject.SetActive(true);
        }

        public void DefualtGameObjectsActiveState()
        {
            foreach (var country in countries)
            {
                if (country.Selected)
                    continue;

                country.modelGO.SetActive(false);
                country.SelectedGO.SetActive(false);

                SetIconPosition(country.Geolocation, country.GeolocationRect);
                country.GeolocationGO.SetActive(true);
            }
        }

        public void DisablePanels()
        {
            selectedCountriesCountPanel.gameObject.SetActive(false);
            countryInfoPanel.gameObject.SetActive(false);
            clearSheetButtonTransform.gameObject.SetActive(false);

            countryInfoPanel.gameObject.GetComponent<Animator>().SetBool("open", false);
            selectedCountriesCountPanel.gameObject.GetComponent<Animator>().SetBool("open", false);
        }

        private void SetModelPosition(Vector3 position, GameObject model)
        {
            model.transform.position = position;
        }

        public void CreateModels()
        {
            foreach (var country in countries)
            {
                CreateModel(country);
            }
        }

        public void CreateModel(CountryInfo country)
        {
            var instance = Object.Instantiate(Resources.Load<GameObject>("Prefabs/Models/" + country.modelPrefabName));
            SetModelPosition(country.Geolocation, instance);
            country.modelGO = instance;
        }
    }
}
