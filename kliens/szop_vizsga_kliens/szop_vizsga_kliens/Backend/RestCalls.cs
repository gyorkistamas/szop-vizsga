﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using szop_vizsga_kliens.Models;

namespace szop_vizsga_kliens.Backend;

static class RestCalls
{

    static RestClient loginClient = new RestClient("http://localhost:8080/login");
    static RestClient registerClient = new RestClient("http://localhost:8080/register");
    static RestClient drawingsClient = new RestClient("http://localhost:8080/drawings");
    static RestClient singleDrawingClient = new RestClient("http://localhost:8080/drawing");

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

            if (res.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return new RegisterResponse()
                {
                    Error = 1,
                    Message = "Status code not OK, please try again!"
                };
            }

            return loginClient.Deserialize<RegisterResponse>(res).Data;

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
            RestResponse response = drawingsClient.Get(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return new ListOfDrawingsResponse()
                {
                    Error = 1,
                    Message = "Status code not OK, please try again!"
                };
            }

            return drawingsClient.Deserialize<ListOfDrawingsResponse>(response).Data;
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
        RestRequest request = new RestRequest();
        request.AddParameter("drawing_id", id);

        try
        {
            RestResponse response = singleDrawingClient.Get(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return new DrawingResponse()
                {
                    Error = 1,
                    Message = "Status code not OK, please try again!"
                };
            }

            return singleDrawingClient.Deserialize<DrawingResponse>(response).Data;

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
        

        try
        {
            RestResponse response = singleDrawingClient.Post(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return new SimpleResponse()
                {
                    Error = 1,
                    Message = "Status code not OK, please try again!"
                };
            }

            return singleDrawingClient.Deserialize<SimpleResponse>(response).Data;

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

}
