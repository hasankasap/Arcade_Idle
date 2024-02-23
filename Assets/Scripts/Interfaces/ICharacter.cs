
public interface ICharacter
{
    public void TakeProducts(Product asset);
    public bool CanTakeProducts();
    public Product DropProductsWithType(ProductTypes type);
    public bool CanDropWantedProductTypes(ProductTypes type);
    public Product DropToTrash();
    public bool CanDropProductToTrash();
    public float Speed { get; set; }
    public float Sensitivity { get; set; }
    public float RotationSpeed { get; set; }
    public float Acceleration { get; set; }
}