namespace backend.Models;

public class Order
{
    public int ID { get; set; }
    public decimal OrderPrice { get; set; }
    public DateTime OrderDate { get; set; }
    public int customerID { get; set; }
}