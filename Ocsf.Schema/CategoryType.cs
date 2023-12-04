namespace Ocsf.Schema
{
    public readonly struct CategoryType
    {
        public readonly int Id;
        public readonly string Name;

        private CategoryType(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override readonly string ToString()
        {
            return Name;
        }

        public static implicit operator int(CategoryType value)
        {
            return value.Id;
        }

        public static implicit operator CategoryType(int id)
        {
            return id switch
            {
                3 => IDAM,
                _ => throw new IndexOutOfRangeException($"Value {id} is outside of range"),
            };
        }

        // Predefined instances

        /// <summary>
        /// The event activity is unknown.
        /// </summary>
        public static CategoryType IDAM => new(3, "Identity & Access Management");
    }
}
