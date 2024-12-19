using System.Runtime.Serialization;

namespace StudentSerialization
{
    [Serializable]
    class Group
    {
        public decimal GroupId { get; set; }

        public string Name { get; set; }
        
        public List<Student> Students { get; set; }
        
        // no need to serialize this
        [field: NonSerialized]
        public int StudentsCount { get; set; }

        
        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            StudentsCount = Students.Count;
        }
    }
}
