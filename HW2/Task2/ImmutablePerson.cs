namespace Immutable
{
    public class ImmutablePerson
    {
        public string FirstName { get; }
        public string LastName { get; }
        public int Age { get; }

        public ImmutablePerson(string firstName, string lastName, int age)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }

        public ImmutablePerson WithAge(int newAge)
        {
            return new ImmutablePerson(this.FirstName, this.LastName, newAge);
        }

        public ImmutablePerson WithFirstName(string newFirstName)
        {
            return new ImmutablePerson(newFirstName, this.LastName, this.Age);
        }

        public ImmutablePerson WithLastName(string newLastName)
        {
            return new ImmutablePerson(this.FirstName, newLastName, this.Age);
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName}, Age: {Age}";
        }
    }
}