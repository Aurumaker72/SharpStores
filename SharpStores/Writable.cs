namespace SharpStores;

/// <summary>
///     A writable store for reactive data storage and manipulation
/// </summary>
/// <typeparam name="T">The stored content's type</typeparam>
public class Writable<T> : ISubscribe<T>, ISet<T>
{
    private T _value;
    private readonly List<ValueChanged<T>> _subscribers = new();

    /// <summary>
    ///     Constructs a <see cref="Writable{T}" /> with a default value
    /// </summary>
    /// <param name="value">The <see cref="Writable{T}" />'s initial value</param>
    public Writable(T value)
    {
        _value = value;
    }

    private void OnValueChanged()
    {
        _subscribers.ForEach(x => x(_value));
    }
    

    /// <summary>
    ///     Updates the store's value and notifies subscribers
    /// </summary>
    /// <param name="callback">A callback which receives the store's value and returns a mutated value</param>
    public void Update(Func<T, T> callback)
    {
        _value = callback(_value);
        OnValueChanged();
    }
    
    /// <inheritdoc/>
    public void Set(T value)
    {
        Update(_ => value);
    }

    /// <inheritdoc/>
    public Unsubscribe Subscribe(ValueChanged<T> callback)
    {
        _subscribers.Add(callback);
        OnValueChanged();
        return () => { _subscribers.Remove(callback); };
    }
}