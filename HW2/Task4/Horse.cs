using NCar;

namespace NHorse
{
    public class Horse
    {
        public string Breed { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public int Age { get; set; }

        public Horse(string breed, double weight, double height, int age)
        {
            Breed = breed;
            Weight = weight;
            Height = height;
            Age = age;
        }

        public static implicit operator Car(Horse horse)
        {
            return new Car
            {
                Type = horse.Breed switch
                {
                    "Draft" => "Pickup",
                    "Race" => "Race car",
                    _ => "Sedan"
                }
            };
        }

        public static bool operator >(Horse h1, Horse h2)
        {
            return h1.Weight > h2.Weight;
        }

        public static bool operator <(Horse h1, Horse h2)
        {
            return h1.Weight < h2.Weight;
        }

        public static bool operator ==(Horse h1, Horse h2)
        {
            return h1.Weight == h2.Weight && h1.Height == h2.Height && h1.Age == h2.Age;
        }

        public static bool operator !=(Horse h1, Horse h2)
        {
            return !(h1 == h2);
        }

        public override bool Equals(object obj)
        {
            return obj is Horse horse && this == horse;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Weight, Height, Age);
        }
    }
}