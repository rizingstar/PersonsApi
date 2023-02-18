using Microsoft.EntityFrameworkCore;
using PersonsApi.Model;

namespace PersonsApi.Test
{
    [TestClass]
    public class PersonsRepoTest
    {
        private IPersonsRepository? _personsRepository;
        private Person? _bobPerson;
        private Person? _jackPerson;

        [TestInitialize] 
        public void Init() 
        {
            var contextOptions = new DbContextOptionsBuilder<PersonContext>().UseInMemoryDatabase("Persons").Options;
            var context = new PersonContext(contextOptions);
            _personsRepository = new PersonsRepository(context);
            _bobPerson = new Person { Id = 1, PersonName = "Bob", Address = "Fulshear,TX" };
            _jackPerson = new Person { Id = 2, PersonName = "Jack", Address = "Katy,TX" };
        } 

        [TestMethod]
        public void Insert_AddsPerson_Correctly()
        {
            //Act
            _personsRepository.Insert(_bobPerson);

            // Assert
            Assert.AreEqual(1, _personsRepository.All.Count());
            Assert.IsTrue(_personsRepository.All.Contains(_bobPerson));
            Assert.IsNotNull(_personsRepository.All.Count());
            Assert.AreEqual(_bobPerson.Id, _personsRepository.Find(1).Id);
            Assert.AreEqual(_bobPerson.PersonName, _personsRepository.Find(1).PersonName);
            Assert.AreEqual(_bobPerson.Address, _personsRepository.Find(1).Address);
        }

        [TestMethod]
        public void Insert_And_Remove_LeavesCollectionUnchanged()
        {
            Assert.AreEqual(1, _personsRepository.All.Count());
            _personsRepository.Insert(_jackPerson);
            _personsRepository.Delete(_jackPerson.Id);
            Assert.AreEqual(1, _personsRepository.All.Count());
            Assert.IsNull(_personsRepository.Find(_jackPerson.Id));
        }
    }
}