using Microsoft.JSInterop;

namespace AdBulletin.Core.Services;

public class LoadingService
{
    private int _pendingRequests = 0;
    public bool IsLoading => _pendingRequests > 0;

    public event Action OnChange;

    public void Show()
    {
        _pendingRequests++;
        NotifyStateChanged();
    }

    public void Hide()
    {
        if (_pendingRequests > 0)
        {
            _pendingRequests--;
            NotifyStateChanged();
        }
    }

    public async Task HideLoadingWithJsAsync(IJSRuntime jsRuntime)
    {
        Hide();
        if (!IsLoading)
        {
            // Invoke function JS to hide loading html
            await jsRuntime.InvokeVoidAsync("hideLoading");
        }
    }

    public async Task Check(IJSRuntime jsRuntime)
    {
        if (!IsLoading)
        {
            // Invoke function JS to hide loading html
            await jsRuntime.InvokeVoidAsync("hideLoading");
        }
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}