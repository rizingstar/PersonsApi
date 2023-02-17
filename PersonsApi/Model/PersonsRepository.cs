namespace PersonsApi.Model
{
    public class PersonsRepository : IPersonsRepository
    {
        private PersonContext _personContext;

        public PersonsRepository(PersonContext PersonContext)
        {
           _personContext = PersonContext;
        }

        public IEnumerable<Person> All
        {
            get { return _personContext.Persons; }
        }

        public bool DoesItemExist(int id)
        {
            return _personContext.Persons.Any(x => x.Id == id);
        }

        public Person Find(int id)
        {
            return _personContext.Persons.Find(id);
        }

        public void Insert(Person Person)
        {
           _personContext.Add(Person);
           _personContext.SaveChanges();
        }

        public void Update(Person Person)
        {
           _personContext.Update(Person);
           _personContext.SaveChanges();
        }

        public void Delete(int id)
        {
           _personContext.Remove(Find(id));
           _personContext.SaveChanges();
        }
    }
}