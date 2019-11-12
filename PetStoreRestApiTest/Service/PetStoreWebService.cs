using Newtonsoft.Json;
using PetStoreRestApiTest.Modules;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PetStoreRestApiTest.Service
{
    public class PetStoreWebService
    {
        public const string SwaggerUrl = @"https://petstore.swagger.io/v2";
        private const string Resource = "/pet";
        private static readonly string ResourceIdEndPointTemplate = Resource + "/{0}";

        public static RestClient Client => new RestClient(SwaggerUrl);

        public static IRestResponse CreatePet(Pet pet)
        {
            IRestRequest request = new RestRequest(Resource, Method.POST);
            request.AddAcceptHeader();
            request.AddContentTypeHeader();
            string jsonBody = JsonConvert.SerializeObject(pet);
            request.AddJsonBody(jsonBody);
            return Client.Execute(request);
        }

        public static IRestResponse ReadPetData(string petId)
        {
            RestRequest request = new RestRequest(GetPetEndPointById(petId), Method.GET);
            request.AddAcceptHeader();
            return Client.Execute(request);
        }

        public static IRestResponse UpdatePet(Pet pet)
        {
            RestRequest request = new RestRequest(GetPetEndPointById(pet.Id.ToString()), Method.POST);
            request.AddAcceptHeader();
            request.AddContentTypeHeaderForUpdate();
            request.AddParametersForPetUpdate(pet);
            return Client.Execute(request);
        }

        public static IRestResponse DeletePet(string petId)
        {
            RestRequest request = new RestRequest(GetPetEndPointById(petId), Method.DELETE);
            request.AddAcceptHeader();
            return Client.Execute(request);
        }

        public static bool IsPetExist(string petId)
        {
            return ReadPetData(petId).StatusCode.Equals(HttpStatusCode.OK) ? true : false;
        }

        private static string GetPetEndPointById(string petId)
        {
            return string.Format(ResourceIdEndPointTemplate, petId);
        }
    }
}
