namespace SharpStores;

public class Readable<T>
{
    private readonly Func<Action<T>, Action> _start;
    private readonly Action? _stop = null;
    private readonly List<Action<T>> _subscribers = new();
    private T _value;
    
    /// <summary>
    ///     Constructs a <see cref="Readable{T}" /> with an initial value
    /// </summary>
    /// <param name="value">The initial value</param>
    /// <param name="start">
    ///     The <see cref="Func{T,TResult}" /> to be invoked when the store gets its first subscriber.
    ///     <para />
    ///     The function accepts a
    ///     <c>
    ///         Set(
    ///         <typeparam name="T"></typeparam>
    ///         )
    ///     </c>
    ///     function to be invoked for setting the store's value
    ///     <para />
    ///     The function is expected to return a <c>Stop</c> function, which is invoked when the store loses its last
    ///     subscriber.
    /// </param>
    public Readable(T value, Func<Action<T>, Action> start)
    {
        _value = value;
        _start = start;
    }

    private void Notify()
    {
        _subscribers.ForEach(x => x(_value));
    }
    
    private void Update(Func<T, T> callback)
    {
        _value = callback(_value);
        Notify();
    }

    /// <summary>
    ///     Subscribes to store value changes
    /// </summary>
    /// <param name="callback">The callback to be invoked upon a value change</param>
    /// <returns>An action to be invoked by the caller upon unsubscribing</returns>
    public Action Subscribe(Action<T> callback)
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

        Notify();

        return () => { _subscribers.Remove(callback); };
    }
}