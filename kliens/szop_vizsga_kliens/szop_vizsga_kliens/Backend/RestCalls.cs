using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using szop_vizsga_kliens.Models;

namespace szop_vizsga_kliens.Backend
{
    static class RestCalls
    {

        static RestClient loginClient = new RestClient("http://localhost:8080/login");

        /// <summary>
        /// Log in backend for api
        /// </summary>
        /// <param name="username">Username for user</param>
        /// <param name="password">Password for user</param>
        /// <returns>Response from API</returns>
        public static LoginResponse Login(string username, string password)
        {
            RestRequest request = new RestRequest();
            request.AddParameter("username", username);
            request.AddParameter("password", password);

            try
            {
                RestResponse res = loginClient.Post(request);

                if(res.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    return new LoginResponse()
                    {
                        Error =  1,
                        Message = "Status code not OK, please try again!"
                    };
                }

                return loginClient.Deserialize<LoginResponse>(res).Data;

            }
            catch (DeserializationException)
            {
                return new LoginResponse()
                {
                    Error = 1,
                    Message = "Cannot deserialize response"
                };
            }
            catch (Exception e)
            {
                return new LoginResponse()
                {
                    Error = 1,
                    Message = e.Message
                };
            }
        }

    }
}
