namespace Ocsf.Schema
{
    public readonly struct CustomValue
    {
        public readonly int Id;
        public readonly string Name;

        private CustomValue(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override readonly string ToString()
        {
            return Name;
        }

        public static implicit operator int(CustomValue value)
        {
            return value.Id;
        }

        public static implicit operator CustomValue(int id)
        {
            return id switch
            {
                1 => Item1,
                2 => Item2,
                _ => throw new IndexOutOfRangeException($"Value {id} is outside of range"),
            };
        }

        public static CustomValue Item1 => new(1, "Item 1");

        public static CustomValue Item2 => new(2, "Item 2");
    }

}
