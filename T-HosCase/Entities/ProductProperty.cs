namespace T_HosCase.Entities
{
    public class ProductProperty
    {
        public int ProductPropertyId { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int PropertyId { get; set; }
        public Property Property { get; set; }
    }
}
