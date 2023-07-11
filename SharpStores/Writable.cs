namespace SharpStores;

/// <summary>
///     A writable store for reactive data storage and manipulation
/// </summary>
/// <typeparam name="T">The stored content's type</typeparam>
public class Writable<T>
{
    private T _value;
    private readonly List<Action<T>> _subscribers = new();

    /// <summary>
    ///     Constructs a <see cref="Writable{T}" /> with a default value
    /// </summary>
    /// <param name="value">The <see cref="Writable{T}" />'s initial value</param>
    public Writable(T value)
    {
        _value = value;
    }

    private void Notify()
    {
        _subscribers.ForEach(x => x(_value));
    }

    /// <summary>
    ///     Subscribes to store value changes
    /// </summary>
    /// <param name="callback">The callback to be invoked upon a value change</param>
    /// <returns>An action to be invoked by the caller upon unsubscribing</returns>
    public Action Subscribe(Action<T> callback)
    {
        _subscribers.Add(callback);
        Notify();
        return () => { _subscribers.Remove(callback); };
    }

    /// <summary>
    ///     Updates the store's value and notifies subscribers
    /// </summary>
    /// <param name="callback">A callback which receives the store's value and returns a mutated value</param>
    public void Update(Func<T, T> callback)
    {
        _value = callback(_value);
        Notify();
    }


    /// <summary>
    ///     Sets the store's value to <paramref name="value" /> and notifies subscribers
    /// </summary>
    /// <param name="value"></param>
    public void Set(T value)
    {
        Update(_ => value);
    }
}