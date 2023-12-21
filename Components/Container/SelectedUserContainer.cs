namespace Christmas.Components.Container;

public class SelectedUserContainer
{
    private string? _selectedUser;

    public string SelectedUser
    {
        get => _selectedUser ?? string.Empty;
        set
        {
            _selectedUser = value;
            NotifyStateChanged();
        }
    }
    
    public event Action? OnChange;

    private void NotifyStateChanged() => OnChange?.Invoke();
}