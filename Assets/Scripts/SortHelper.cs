namespace Assets.Scripts
{
    public class SortHelper
    {
        public void SortArea(CountryInfo[] counties)
        {
            for (var i = 1; i < counties.Length; i++)
            {
                var country = counties[i];
                var j = i - 1;
                while (j >= 0 && counties[j].Area > country.Area)
                {
                    counties[j + 1] = counties[j];
                    j--;
                }

                counties[j + 1] = country;
            }
        }

        public void SortPopulation(CountryInfo[] counties)
        {
            for (var i = 1; i < counties.Length; i++)
            {
                var country = counties[i];
                var j = i - 1;
                while (j >= 0 && counties[j].Population > country.Population)
                {
                    counties[j + 1] = counties[j];
                    j--;
                }

                counties[j + 1] = country;
            }
        }

        public void SortVVP(CountryInfo[] counties)
        {
            for (var i = 1; i < counties.Length; i++)
            {
                var country = counties[i];
                var j = i - 1;
                while (j >= 0 && counties[j].VVP > country.VVP)
                {
                    counties[j + 1] = counties[j];
                    j--;
                }

                counties[j + 1] = country;
            }
        }
    }
}
