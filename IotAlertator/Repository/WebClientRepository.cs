using IotAlertator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace IotAlertator.Repository
{
    internal class WebClientRepository : IRepository
    {
        private readonly WebClientRepositoryConfig _config;


        public WebClientRepository(WebClientRepositoryConfig config)
        {
            _config = config;
        }


        public int CreateIotAlert(IotAlert iotAlert)
        {
            //https://www.tutorialsteacher.com/webapi/consume-web-api-post-method-in-aspnet-mvc


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_config.BaseAddress);        //http://localhost:5028"
                var postTask = client.PostAsJsonAsync<IotAlert>("iotAlert", iotAlert);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                    return 1;

                //TODO: Log
                return 0;

            }
        }
    }
}
