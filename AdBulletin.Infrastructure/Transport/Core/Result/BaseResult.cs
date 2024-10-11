namespace AdBulletin.Infrastructure.Transport;

public class BaseResult<T>
{
    public PaginationRawResult Pagination { get; set; } = new();
    public T Result { get; set; }
}