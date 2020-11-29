using Newtonsoft.Json;
using NUnit.Framework;
using PetStoreRestApiTest.Modules;
using PetStoreRestApiTest.Service;
using RestSharp;
using System.Net;

namespace PetStoreRestApiTest.Test
{
    [TestFixture]
    internal class CRUDTest
    {
        private static Pet TestPet = new Pet
        {
            Id = 56,
            Name = "Doggy",
            Status = "available"
        };
        private static Pet UpdatedPet = new Pet
        {
            Id = TestPet.Id,
            Name = "Cread",
            Status = "sold"
        };

        [Test, Order(1)]
        public void CreatePet()
        {
            IRestResponse responce = PetStoreWebService.CreatePet(TestPet);
            Pet responcePet = DeserializeResponceToPet(responce);
            Assert.That(responcePet, Is.EqualTo(TestPet), "The created pet contains unexpected data");
        }

        [Test, Order(2)]
        public void ReadPetData()
        {
            VerifyThatPetExist(TestPet.Id.ToString());
            IRestResponse responce = PetStoreWebService.ReadPetData(TestPet.Id.ToString());
            var responcePet = DeserializeResponceToPet(responce);
            Assert.That(responcePet, Is.EqualTo(TestPet), "The pets aren't equals");
        }

        private static Pet DeserializeResponceToPet(IRestResponse responce)
        {
            return JsonConvert.DeserializeObject<Pet>(responce.Content);
        }

        [Test, Order(3)]
        public void UpdatePetData()
        {
            
            VerifyThatPetExist(UpdatedPet.Id.ToString());
            IRestResponse responce = PetStoreWebService.UpdatePet(UpdatedPet);
            Assert.That(responce.StatusCode, Is.EqualTo(HttpStatusCode.OK), $"The pet isn't updated.");
        }

        [Test, Order(4)]
        public void DeletePet()
        {
            VerifyThatPetExist(TestPet.Id.ToString());
            IRestResponse responce = PetStoreWebService.DeletePet(TestPet.Id.ToString());
            Assert.That(responce.StatusCode, Is.EqualTo(HttpStatusCode.OK), $"Deleting failed.");
        }

        private void VerifyThatPetExist(string petId)
        {
            Assert.IsTrue(PetStoreWebService.IsPetExist(petId), $"The pet with id {petId} doesn't exist");
        }
    }



}
