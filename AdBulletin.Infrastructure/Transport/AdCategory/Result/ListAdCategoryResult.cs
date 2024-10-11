namespace AdBulletin.Infrastructure.Transport;

public class ListAdCategoryResult<TResult>
{
    public IEnumerable<TResult> Items { get; set; }
}
