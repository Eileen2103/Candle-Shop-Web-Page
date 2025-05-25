namespace backend.Models;

public class Product
{

    public int ID { get; set; }
    public string? Name { get; set; }
    public decimal OrderPrice { get; set; }
    public string? Description { get; set; }
    public string? ImagePath { get; set; }
    public string? SubTitle { get; set; }
    public string? Category { get; set; }
    public decimal? Rating { get; set; }

}