using Newtonsoft.Json;
using NUnit.Framework;
using PetStoreRestApiTest.Modules;
using PetStoreRestApiTest.Service;
using RestSharp;
using System.Net;

namespace PetStoreRestApiTest.Test
{
    [TestFixture]
    internal class CRUDTest : ExtentReport
    {
        private Pet testPet = new Pet
        {
            Id = 56,
            Name = "Doggy",
            Status = "available"
        };

        [Test, Order(1)]
        public void CreatePet()
        {
            IRestResponse responce = PetStoreWebService.CreatePet(testPet);
            Pet responcePet = DeserializeResponceToPet(responce);
            Assert.That(responcePet, Is.EqualTo(testPet), "The created pet contains unexpected data");
        }

        [Test, Order(2)]
        public void ReadPetData()
        {
            VerifyThatPetExist(testPet.Id.ToString());
            IRestResponse responce = PetStoreWebService.ReadPetData(testPet.Id.ToString());
            var responcePet = DeserializeResponceToPet(responce);
            Assert.That(responcePet, Is.EqualTo(testPet), "The pets aren't equals");
        }

        private static Pet DeserializeResponceToPet(IRestResponse responce)
        {
            return JsonConvert.DeserializeObject<Pet>(responce.Content);
        }

        [Test, Order(3)]
        public void UpdatePetData()
        {
            var updatedPet = new Pet
            {
                Id = testPet.Id,
                Name = "Cread",
                Status = "sold"
            };
            VerifyThatPetExist(updatedPet.Id.ToString());
            IRestResponse responce = PetStoreWebService.UpdatePet(updatedPet);
            Assert.That(responce.StatusCode, Is.EqualTo(HttpStatusCode.OK), $"The pet isn't updated.");
        }

        [Test, Order(4)]
        public void DeletePet()
        {
            VerifyThatPetExist(testPet.Id.ToString());
            IRestResponse responce = PetStoreWebService.DeletePet(testPet.Id.ToString());
            Assert.That(responce.StatusCode, Is.EqualTo(HttpStatusCode.OK), $"Deleting failed.");
        }

        private void VerifyThatPetExist(string petId)
        {
            Assert.IsTrue(PetStoreWebService.IsPetExist(petId), $"The pet with id {petId} doesn't exist");
        }
    }



}
