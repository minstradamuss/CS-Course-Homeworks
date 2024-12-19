using System.Runtime.Serialization.Formatters.Binary;

namespace StudentSerialization
{
    class Program
    {
        static void Main()
        {
            var first = new Student { StudentId = 1, FirstName = "Ann", LastName = "Brown", Age = 3, Group = null };
            var second = new Student { StudentId = 2, FirstName = "Bob", LastName = "Black", Age = 20, Group = null };
            var third = new Student { StudentId = 3, FirstName = "Steve", LastName = "Black", Age = 25, Group = null };
            var all = new List<Student>() { first, second, third };

            var group = new Group { GroupId = 1, Name = "NEWGROUP", Students = all };

            foreach (var student in all)
            {
                student.Group = group;
            }

            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();

                formatter.Serialize(stream, group);

                stream.Position = 0;
                var newGroup = (Group)formatter.Deserialize(stream);

                Console.WriteLine("Groups:");
                Console.WriteLine($"{newGroup.GroupId} {newGroup.Name}");
                Console.WriteLine("Students:");
                foreach (var student in newGroup.Students)
                {
                    Console.WriteLine($"{student.StudentId} {student.FirstName} {student.LastName}  {student.Age}");
                }
                Console.WriteLine($"Number of students: {newGroup.StudentsCount}");
            }
        }
    }
}
