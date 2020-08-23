namespace Ddd.EfCore
{
    // Class create to test lazy Loading collections.
    public class Subject : Entity
    {
        protected Subject() { }

        public Subject(string name) : this()
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}
