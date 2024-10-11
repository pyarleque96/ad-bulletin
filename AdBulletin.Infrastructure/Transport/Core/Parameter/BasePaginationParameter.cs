namespace AdBulletin.Infrastructure.Transport;

public class BasePaginationParameter
{
    public int PageIndex { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
