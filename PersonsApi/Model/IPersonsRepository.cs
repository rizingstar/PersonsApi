namespace PersonsApi.Model
{
    public interface IPersonsRepository
    {
        bool DoesItemExist(int id);
        IEnumerable<Person> All { get; }
        Person Find(int id);
        void Insert(Person Person);
        void Update(Person Person);
        void Delete(int id);
    }
}
