namespace Accounting.APP;

class ComboBoxItem<T>
{
    public string Name { get; set; } = string.Empty;
    public T? Value { get; set; } = default;
}
