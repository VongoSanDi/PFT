namespace server.Dto;

public class Entry
{
    public int Id { get; set; }

    public double Amount { get; set; }

    public DateTime Date { get; set; }

    public string Description { get; set; } = string.Empty;

    public int TypeId { get; set; }

    public int CategoryId { get; set; }
}
