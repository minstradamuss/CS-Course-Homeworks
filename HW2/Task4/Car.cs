namespace NCar
{
    public class Car
    {
        public string Type { get; set; }

        public override string ToString()
        {
            return $"{Type}";
        }
    }
}