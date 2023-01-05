using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using RestSharp;
using szop_vizsga_kliens.Models;

namespace szop_vizsga_kliens.Backend;

static class RestCalls
{

    static RestClient loginClient = new RestClient("http://localhost:8080/login");
    static RestClient registerClient = new RestClient("http://localhost:8080/register");
    static RestClient drawingsClient = new RestClient("http://localhost:8080/drawings");
    static RestClient singleDrawingClient = new RestClient("http://localhost:8080/drawing");
    static RestClient getPhpData = new RestClient("http://localhost:8080/php");

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

            LoginResponse response = loginClient.Deserialize<LoginResponse>(res).Data;

            if (response is null)
            {
                response = new LoginResponse() {
                    Error = 1,
                    Message = "Unknown error"
                };
            }

            return response;

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

    /// <summary>
    /// Make a registration call
    /// </summary>
    /// <param name="username">Username to register</param>
    /// <param name="password">Password to register</param>
    /// <returns>Response from API</returns>
    public static RegisterResponse Register(string username, string password)
    {
        RestRequest request = new RestRequest();
        request.AddParameter("username", username);
        request.AddParameter("password", password);

        try
        {
            RestResponse res = registerClient.Post(request);

            RegisterResponse response = loginClient.Deserialize<RegisterResponse>(res).Data;

            if (response is null)
            {
                response = new RegisterResponse()
                {
                    Error = 1,
                    Message = "Unknown error"
                };
            }

            return response;

        }
        catch (DeserializationException)
        {
            return new RegisterResponse()
            {
                Error = 1,
                Message = "Cannot deserialize response"
            };
        }
        catch (Exception e)
        {
            return new RegisterResponse()
            {
                Error = 1,
                Message = e.Message
            };
        }
    }


    /// <summary>
    /// Get a list of all drawings
    /// </summary>
    /// <returns>A list fo drawings</returns>
    public static ListOfDrawingsResponse GetAllDrawings()
    {
        RestRequest request = new RestRequest();

        try
        {
            RestResponse res = drawingsClient.Get(request);

            ListOfDrawingsResponse response = drawingsClient.Deserialize<ListOfDrawingsResponse>(res).Data;

            if (response is null)
            {
                response = new ListOfDrawingsResponse()
                {
                    Error = 1,
                    Message = "Unknown error"
                };
            }

            return response;
        }
        catch (DeserializationException)
        {
            return new ListOfDrawingsResponse()
            {
                Error = 1,
                Message = "Cannot deserialize response"
            };
        }
        catch (Exception e)
        {
            return new ListOfDrawingsResponse()
            {
                Error = 1,
                Message = e.Message
            };
        }
    }


    /// <summary>
    /// Get a single drawing
    /// </summary>
    /// <param name="id">Id of drawing</param>
    /// <returns>Properties of the drawing</returns>
    public static DrawingResponse GetSingleDrawing(int id)
    {
        RestRequest request = new RestRequest("/{id}");
        request.AddUrlSegment("id", id);

        try
        {
            RestResponse res = singleDrawingClient.Get(request);

            DrawingResponse response = singleDrawingClient.Deserialize<DrawingResponse>(res).Data;

            if (response is null)
            {
                response = new DrawingResponse() { 
                    Error = 1,
                    Message = "Unknown error"
                };
            }

            return response;
        }
        catch (DeserializationException)
        {
            return new DrawingResponse()
            {
                Error = 1,
                Message = "Cannot deserialize response"
            };
        }
        catch (Exception e)
        {
            return new DrawingResponse()
            {
                Error = 1,
                Message = e.Message
            };
        }
    }


    /// <summary>
    /// Uplaod new drawing
    /// </summary>
    /// <param name="token">Token of User</param>
    /// <param name="title">The title of the drawing</param>
    /// <param name="drawing_data">The data of drawing</param>
    /// <returns>Response from API</returns>
    public static SimpleResponse NewDrawing(string token, string title, string drawing_data)
    {
        RestRequest request = new RestRequest();
        request.AddParameter("token", token);
        request.AddParameter("title", title);
        request.AddParameter("drawing_data", drawing_data);

        try
        {
            RestResponse res = singleDrawingClient.Post(request);

            SimpleResponse response = singleDrawingClient.Deserialize<SimpleResponse>(res).Data;

            if (response is null )
            {
                response = new SimpleResponse() { 
                    Error = 1,
                    Message = "Unknown error"
                };
            }

            return response;
        }
        catch (DeserializationException)
        {
            return new SimpleResponse()
            {
                Error = 1,
                Message = "Cannot deserialize response"
            };
        }
        catch (Exception e)
        {
            return new SimpleResponse()
            {
                Error = 1,
                Message = e.Message
            };
        }
    }

    /// <summary>
    /// Update drawing
    /// </summary>
    /// <param name="token">Token of User</param>
    /// <param name="title">The title of the drawing</param>
    /// <param name="drawing_data">The data of drawing</param>
    /// <param name="id">The id of drawing to be updated</param>
    /// <returns>Response from API</returns>
    public static SimpleResponse UpdateDrawing(string token, string title, int id, string drawing_data)
    {
        RestRequest request = new RestRequest("/{id}");
        request.AddParameter("token", token);
        request.AddParameter("title", title);
        request.AddParameter("drawing_data", drawing_data);
        request.AddUrlSegment("id", id);

        try
        {
            RestResponse res = singleDrawingClient.Put(request);

            SimpleResponse response = singleDrawingClient.Deserialize<SimpleResponse>(res).Data;

            if (response is null )
            {
                response = new SimpleResponse()
                {
                    Error = 1,
                    Message = "Unknown error"
                };
            }

            return response;

        }
        catch (DeserializationException)
        {
            return new SimpleResponse()
            {
                Error = 1,
                Message = "Cannot deserialize response"
            };
        }
        catch (Exception e)
        {
            return new SimpleResponse()
            {
                Error = 1,
                Message = e.Message
            };
        }
    }


    /// <summary>
    /// Delete drawing
    /// </summary>
    /// <param name="token">Token of User</param>
    /// <param name="id">The id of drawing to be deleted</param>
    /// <returns>Response from API</returns>
    public static SimpleResponse DeleteDrawing(string token, int id)
    {
        RestRequest request = new RestRequest("http://localhost:8080/drawing/{token}/{id}", Method.Delete);
        request.AddUrlSegment("token", token);
        request.AddUrlSegment("id", id);


        try
        {
            RestResponse res = singleDrawingClient.Execute(request);

            SimpleResponse response = singleDrawingClient.Deserialize<SimpleResponse>(res).Data;

            if (response is null )
            {
                response = new SimpleResponse()
                {
                    Error = 1,
                    Message = "Unknown error"
                };
            }

            return response;

        }
        catch (DeserializationException)
        {
            return new SimpleResponse()
            {
                Error = 1,
                Message = "Cannot deserialize response"
            };
        }
        catch (Exception e)
        {
            return new SimpleResponse()
            {
                Error = 1,
                Message = e.Message
            };
        }
    }


    /// <summary>
    /// Access PHP API
    /// </summary>
    /// <param name="username">Username</param>
    /// <param name="password">Password</param>
    /// <returns>List of grades in PHP API</returns>
    public static PHPResponse AccessPHP(string username, string password)
    {
        RestRequest request = new RestRequest();
        request.AddParameter("username", username);
        request.AddParameter("password", password);

        try
        {
            RestResponse res = getPhpData.Get(request);

            PHPResponse response = singleDrawingClient.Deserialize<PHPResponse>(res).Data;

            if (response is null )
            {
                response = new PHPResponse()
                {
                    Error = 1,
                    Message = "Unknown error"
                };
            }

            return response;

        }
        catch (DeserializationException)
        {
            return new PHPResponse()
            {
                Error = 1,
                Message = "Cannot deserialize response"
            };
        }
        catch (Exception e)
        {
            return new PHPResponse()
            {
                Error = 1,
                Message = e.Message
            };
        }
    }

}
