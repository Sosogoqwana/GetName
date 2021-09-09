using SoyisileConsole.Model;
using StarWarsAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace StarWarsAPI
{
    //
    public class StarWarsAPIClient
    {
        readonly protected string BaseAddress = @"http://swapi.dev/api/";
        readonly protected string AcceptHeader = "application/json";

        HttpClient GetClient()
        {
            var client = new HttpClient();

            client.BaseAddress = new Uri(BaseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AcceptHeader));

            return client;
        }



        public async Task<T> GetAsync<T>(string url)
        {

            T result = default(T);

            using (HttpClient client = GetClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);

                response.EnsureSuccessStatusCode();
                result = await response.Content.ReadAsAsync<T>();
            }

            return result;
        }



        public async Task<IEnumerable<T>> GetListAsync<T>(IEnumerable<string> urls)
        {

            Task<IEnumerable<T>> t = Task.Run(() =>
            {
                List<T> items = new List<T>();
                foreach (var url in urls)
                {
                    T item = GetAsync<T>(url).Result;
                    items.Add(item);

                }
                return items.AsEnumerable();
            });

            return await t;

        }


        #region Get methods

        public async Task<People> GetPeopleAsync(string id)
        {
            string url = string.Format("{0}/{1}", "people", id);
            return await GetAsync<People>(url);
        }

        public async Task<Planet> GetPlanetAsync(string id)
        {
            string url = string.Format("{0}/{1}", "planets", id);
            return await GetAsync<Planet>(url);
        }

        public async Task<Specie> GetSpecieAsync(string id)
        {
            string url = string.Format("{0}/{1}", "species", id);
            return await GetAsync<Specie>(url);
        }

        public async Task<Starship> GetStarshipAsync(string id)
        {
            string url = string.Format("{0}/{1}", "starships", id);
            return await GetAsync<Starship>(url);
        }

        public async Task<Film> GetFilmAsync(string id)
        {
            string url = string.Format("{0}/{1}", "films", id);
            return await GetAsync<Film>(url);
        }

        public async Task<Vehicle> GetVehicleAsync(string id)
        {
            string url = string.Format("{0}/{1}", "vehicles", id);
            return await GetAsync<Vehicle>(url);
        }

        #endregion


        #region GetAll methods

        public async Task<StarWarsEntityList<People>> GetAllPeopleAsync(string page = "1")
        {
            var url = string.Format("people?page={0}", page);
            return await GetAsync<StarWarsEntityList<People>>(url);
        }

        public async Task<StarWarsEntityList<Planet>> GetAllPlanetAsync(string page = "1")
        {
            var url = string.Format("planets?page={0}", page);
            return await GetAsync<StarWarsEntityList<Planet>>(url);
        }

        public async Task<StarWarsEntityList<Specie>> GetAllSpecieAsync(string page = "1")
        {
            var url = string.Format("species?page={0}", page);
            return await GetAsync<StarWarsEntityList<Specie>>(url);
        }

        public async Task<StarWarsEntityList<Starship>> GetAllStarshipAsync(string page = "1")
        {
            var url = string.Format("starships?page={0}", page);
            return await GetAsync<StarWarsEntityList<Starship>>(url);
        }

        public async Task<StarWarsEntityList<Film>> GetAllFilmAsync(string page = "1")
        {
            var url = string.Format("films?page={0}", page);
            return await GetAsync<StarWarsEntityList<Film>>(url);
        }

        public async Task<StarWarsEntityList<Vehicle>> GetAllVehicleAsync(string page = "1")
        {
            var url = string.Format("vehicles?page={0}", page);
            return await GetAsync<StarWarsEntityList<Vehicle>>(url);
        }

        #endregion

    }
}
