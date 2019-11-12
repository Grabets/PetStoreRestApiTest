using PetStoreRestApiTest.Modules;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetStoreRestApiTest.Service
{
    public static class WebServiceSupport
    {
        private const string JsonHeaderValue = "application/json";
        private const string ContentTypeHeaderName = "Content-Type";

        public static IRestRequest AddAcceptHeader(this IRestRequest request)
        {
            return request.AddHeader("accept", JsonHeaderValue);
        }
        public static IRestRequest AddContentTypeHeader(this IRestRequest request)
        {
            return request.AddHeader(ContentTypeHeaderName, JsonHeaderValue);
        }
        public static IRestRequest AddContentTypeHeaderForUpdate(this IRestRequest request)
        {
            return request.AddHeader(ContentTypeHeaderName, "application/x-www-form-urlencoded");
        }
        public static IRestRequest AddParametersForPetUpdate(this IRestRequest request, Pet pet)
        {
            request.AddParameter("name", pet.Name);
            request.AddParameter("status", pet.Status);
            return request;
        }

    }
}
