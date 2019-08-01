using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Main : MonoBehaviour
    {
        public Button closeSheetButton;
        public Button closeMapButton;
        public Button clearSheetButton;

        public Button sortAreaButton;
        public Button sortPopulationButton;
        public Button sortVVPButton;

        private MainCamera cameraMain;

        public Canvas canvasSheet;
        public Canvas canvasMap;

        private SheetManager sheetManager;

        private string pathToJson;
        private string jsonName = "Countries";
        private JsonHelper jsonHelper;

        private MapManager mapManager;

        private CountryInfo[] countries;
        
        private Transform sortAreaArrow;
        private Transform sortPopulationArrow;
        private Transform sortVVPArrow;

        private ScreenOrientation screenOrientation;

        private OrientationSettings SortLabelOrientation;
        private OrientationSettings SortAreaButtonOrientation;
        private OrientationSettings SortPopulationButtonOrientation;
        private OrientationSettings SortVVPButtonOrientation;

        void Start()
        {
            closeSheetButton.onClick.AddListener(HideCanvasSheet);
            closeMapButton.onClick.AddListener(HideCanvasMap);
            clearSheetButton.onClick.AddListener(ClearSheet);

            sortAreaButton.onClick.AddListener(() => sheetManager.SortArea(sortAreaArrow));
            sortPopulationButton.onClick.AddListener(() => sheetManager.SortPopulation(sortPopulationArrow));
            sortVVPButton.onClick.AddListener(() => sheetManager.SortVVP(sortVVPArrow));

            sortAreaArrow = sortAreaButton.transform.GetChild(1);
            sortPopulationArrow = sortPopulationButton.transform.GetChild(1);
            sortVVPArrow = sortVVPButton.transform.GetChild(1);

            cameraMain = Camera.main.GetComponent<MainCamera>();

            jsonHelper = new JsonHelper();
            jsonHelper.CheckJsonExist(jsonName);
            pathToJson = Path.Combine(Application.persistentDataPath, jsonName);

            countries = jsonHelper.GetCountiesFromJson(pathToJson);

            sheetManager = new SheetManager(canvasSheet, countries);
            mapManager = new MapManager(canvasMap, countries);
            
            mapManager.CreateIcons();
            mapManager.CreateModels();
            
            screenOrientation = Screen.orientation;

            SortLabelOrientation = sortAreaButton.transform.parent.GetChild(0).GetComponent<OrientationSettings>();
            SortAreaButtonOrientation = sortAreaButton.GetComponent<OrientationSettings>();
            SortPopulationButtonOrientation = sortPopulationButton.GetComponent<OrientationSettings>();
            SortVVPButtonOrientation = sortVVPButton.GetComponent<OrientationSettings>();
        }

        void Update()
        {
            if (canvasMap.gameObject.activeInHierarchy)
            {
                if (Input.touchCount == 2)
                {
                    cameraMain.Zoom();
                }
                else
                {
                    cameraMain.Move();
                }

                foreach (var country in countries)
                {
                    if (country.SelectedGO.activeInHierarchy)
                    {
                        mapManager.SetIconPosition(country.Geolocation, country.SelectedRect);
                    }
                    if (country.GeolocationGO.activeInHierarchy)
                    {
                        mapManager.SetIconPosition(country.Geolocation, country.GeolocationRect);
                    }
                }
            }

            if (screenOrientation != Screen.orientation)
            {
                screenOrientation = Screen.orientation;

                if (screenOrientation == ScreenOrientation.Portrait)
                {
                    SortLabelOrientation.SetPortraitValues();
                    SortAreaButtonOrientation.SetPortraitValues();
                    SortPopulationButtonOrientation.SetPortraitValues();
                    SortVVPButtonOrientation.SetPortraitValues();
                }
                else
                {
                    SortLabelOrientation.SetLandscapeValues();
                    SortAreaButtonOrientation.SetLandscapeValues();
                    SortPopulationButtonOrientation.SetLandscapeValues();
                    SortVVPButtonOrientation.SetLandscapeValues();
                }
            }
        }

        private void HideCanvasSheet()
        {
            canvasSheet.gameObject.SetActive(false);
            mapManager.DefualtGameObjectsActiveState();
            canvasMap.gameObject.SetActive(true);
        }

        private void HideCanvasMap()
        {
            mapManager.HideCanvas();
            sheetManager.AddMissingRows();
            canvasSheet.gameObject.SetActive(true);
        }

        private void ClearSheet()
        {
            sheetManager.ClearSheet();
            mapManager.selectedCountriesCount = 0;
            mapManager.DisablePanels();
            mapManager.DefualtGameObjectsActiveState();
        }
    }
}
