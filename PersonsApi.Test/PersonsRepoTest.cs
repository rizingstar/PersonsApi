using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using PersonsApi.Controllers;
using PersonsApi.Model;

namespace PersonsApi.Test
{
    [TestClass]
    public class PersonsRepoTest
    {
        PersonContext _mockPersonContext;
        Mock<DbContextOptions<PersonContext>> _mockDbContextOptions;


        [TestInitialize]
        public void Initialize()
        {
            _mockPersonContext = new PersonContext(_mockDbContextOptions.Object);
        }

        [TestMethod]
        public void GetPersons()
        {
            // Arrange
            var items = new List<Person>()
            {
                new Person
                {
                    Id = 1,
                    PersonName = "Bob",
                },
                new Person
                {
                    Id = 2,
                    PersonName = "Bob2",
                }
            };

            PersonsRepository personsRepository = new PersonsRepository(_mockPersonContext);


            // Act
            IEnumerable<Person> result = personsRepository.All;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(1, result.First().Id);
            Assert.AreEqual(2, result.Where(x =>x.PersonName.Equals("Bob2")).First().Id);
        }
    }
}