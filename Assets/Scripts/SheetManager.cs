using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Assets.Scripts
{
    public class SheetManager
    {
        private readonly GameObject countryGameObject = Resources.Load<GameObject>("Prefabs/UI/Country");
        private Transform parenTransform;

        private Vector2 rowPosition;
        private Vector2 rowStartPosition = new Vector2(0, -50);

        private SortHelper sortHelper;

        private bool sortDescendingArea;
        private bool sortDescendingPopulation;
        private bool sortDescendingVVP;

        private CountryInfo[] countries;

        public SheetManager(Canvas canvas, CountryInfo[] countries)
        {
            parenTransform = canvas.transform.Find("Body");
            this.countries = countries;
            rowPosition = rowStartPosition;
            sortHelper = new SortHelper();
        }

        public void AddMissingRows()
        {
            var deltaPosition = new Vector2(0, -50);

            foreach (var county in countries)
            {
                if (!county.needAddToSheet)
                    continue;

                FillRow(county, rowPosition);
                rowPosition += deltaPosition;
                county.needAddToSheet = false;
            }
        }

        public void FillSheet(bool sortDescending)
        {
            var deltaPosition = new Vector2(0, -50);

            for (var i = 0;i < countries.Length; i++)
            {
                int j;
                if (sortDescending)
                    j = countries.Length - 1 - i;
                else
                {
                    j = i;
                }
                if (!countries[j].Selected)
                    continue;

                FillRow(countries[j], rowPosition);
                rowPosition += deltaPosition;
                countries[j].needAddToSheet = false;
            }
        }

        private void FillRow(CountryInfo country, Vector2 position)
        {
            var instance = Object.Instantiate(countryGameObject, parenTransform);
            var m_RectTransform = instance.GetComponent<RectTransform>();

            m_RectTransform.offsetMax = position;

            instance.transform.GetChild(0).GetComponent<Text>().text = country.Name;
            instance.transform.GetChild(1).GetComponent<Text>().text = country.Area.ToString();
            instance.transform.GetChild(2).GetComponent<Text>().text = country.Population.ToString();
            instance.transform.GetChild(3).GetComponent<Text>().text = country.VVP.ToString();
        }

        public void ClearSheet()
        {
            foreach (var country in countries)
            {
                country.Selected = false;
                country.needAddToSheet = false;
            }
            DestroyObjectsInSheet();
        }

        public void DestroyObjectsInSheet()
        {
            for (var i = 1; i < parenTransform.childCount; i++)
            {
                Object.Destroy(parenTransform.GetChild(i).gameObject);
            }

            rowPosition = rowStartPosition;
        }

        public void SortArea(Transform sortAreaArrow)
        {
            sortHelper.SortArea(countries);
            DestroyObjectsInSheet();
            FillSheet(sortDescendingArea);
            sortDescendingArea = !sortDescendingArea;
            sortAreaArrow.Rotate(0, 0, 180);
        }

        public void SortPopulation(Transform sortPopulationArrow)
        {
            sortHelper.SortPopulation(countries);
            DestroyObjectsInSheet();
            FillSheet(sortDescendingPopulation);
            sortDescendingPopulation = !sortDescendingPopulation;
            sortPopulationArrow.Rotate(0, 0, 180);
        }

        public void SortVVP(Transform sortVVPArrow)
        {
            sortHelper.SortVVP(countries);
            DestroyObjectsInSheet();
            FillSheet(sortDescendingVVP);
            sortDescendingVVP = !sortDescendingVVP;
            sortVVPArrow.Rotate(0, 0, 180);
        }
    }
}
