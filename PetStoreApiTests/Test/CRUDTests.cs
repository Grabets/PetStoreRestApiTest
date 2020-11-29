using System.Net;
using Newtonsoft.Json;
using NUnit.Framework;
using PetStoreRestApiTest.Modules;
using PetStoreRestApiTest.Service;
using RestSharp;

namespace PetStoreApiTests.Test
{
    [TestFixture]
    internal class CrudTests
    {
        private static readonly Pet TestPet = new Pet
        {
            Id = 56,
            Name = "Doggy",
            Status = "available"
        };

        private static readonly Pet UpdatedPet = new Pet
        {
            Id = TestPet.Id,
            Name = "Cread",
            Status = "sold"
        };

        [Test, Order(1)]
        public void CreatePet()
        {
            var response = PetStoreWebService.CreatePet(TestPet);
            var responsePet = DeserializeResponseToPet(response);
            Assert.That(responsePet, Is.EqualTo(TestPet), "The created pet contains unexpected data");
        }

        [Test, Order(2)]
        public void ReadPetData()
        {
            VerifyThatPetExist(TestPet.Id.ToString());
            var response = PetStoreWebService.ReadPetData(TestPet.Id.ToString());
            var responsePet = DeserializeResponseToPet(response);
            Assert.That(responsePet, Is.EqualTo(TestPet), "The pets aren't equals");
        }

        private static Pet DeserializeResponseToPet(IRestResponse response)
        {
            return JsonConvert.DeserializeObject<Pet>(response.Content);
        }

        [Test, Order(3)]
        public void UpdatePetData()
        {
            VerifyThatPetExist(UpdatedPet.Id.ToString());
            var response = PetStoreWebService.UpdatePet(UpdatedPet);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "The pet isn't updated.");
        }

        [Test, Order(4)]
        public void DeletePet()
        {
            VerifyThatPetExist(TestPet.Id.ToString());
            var response = PetStoreWebService.DeletePet(TestPet.Id.ToString());
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Deleting failed.");
        }

        private static void VerifyThatPetExist(string petId)
        {
            Assert.IsTrue(PetStoreWebService.IsPetExist(petId), $"The pet with id {petId} doesn't exist");
        }
    }
}
