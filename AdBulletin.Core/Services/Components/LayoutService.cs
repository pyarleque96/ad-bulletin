namespace AdBulletin.Core.Services;

public class LayoutService
{
    public string HeaderClass { get; private set; } = "tp-header-inner-style"; // Default class empty
    public bool IsHeaderTransparent { get; private set; } = false; // Default transparent header value
    public bool IsPremiumHeaderEnable { get; private set; } = false;

    public event Action OnChange;

    public void SetHeaderProperties(string headerClass, bool isHeaderTransparent = false, bool isPremiumHeaderEnable = false)
    {
        HeaderClass = headerClass;
        IsHeaderTransparent = isHeaderTransparent;
        IsPremiumHeaderEnable = isPremiumHeaderEnable;
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}
