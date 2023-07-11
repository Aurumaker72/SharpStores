namespace SharpStores;

/// <summary>
/// The <see cref="Delegate"/> to be invoked when a store has no subscribers left
/// </summary>
public delegate void Stop();

public class Readable<T> : ISubscribe<T>
{
    private readonly Func<Set<T>, Stop> _start;
    private readonly Stop? _stop = null;
    private readonly List<ValueChanged<T>> _subscribers = new();
    private T _value;
    
    /// <summary>
    ///     Constructs a <see cref="Readable{T}" /> with an initial value
    /// </summary>
    /// <param name="value">The initial value</param>
    /// <param name="start">
    ///     The <see cref="Func{T,TResult}"/> to be invoked when the store gets its first subscriber.
    ///     The function accepts a <see cref="Set{T}"/> delegate to be invoked for setting the store's value
    ///     The function is expected to return a <see cref="Stop"/> delegate, which is invoked when the store loses its last
    ///     subscriber.
    /// </param>
    public Readable(T value, Func<Set<T>, Stop> start)
    {
        _value = value;
        _start = start;
    }

    private void OnValueChanged()
    {
        _subscribers.ForEach(x => x(_value));
    }
    
    private void Update(Func<T, T> callback)
    {
        _value = callback(_value);
        OnValueChanged();
    }

    /// <inheritdoc/>
    public Unsubscribe Subscribe(ValueChanged<T> callback)
    {
        var previousCount = _subscribers.Count;
        _subscribers.Add(callback);

        switch (previousCount)
        {
            case 0 when _subscribers.Count > 0:
                // First subscription, call the start function
                _start(x => Update(y => x));
                break;
            case > 0 when _subscribers.Count == 0:
                // No more subscribers, call the end function
                _stop?.Invoke();
                break;
        }

        OnValueChanged();

        return () => { _subscribers.Remove(callback); };
    }
}