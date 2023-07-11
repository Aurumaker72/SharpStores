namespace SharpStores;

/// <summary>
///     The default contract for a value change notification provider
/// </summary>
/// <typeparam name="T">The value's type</typeparam>
public interface ISubscribe<out T>
{
    /// <summary>
    ///     Subscribes to value changes
    /// </summary>
    /// <param name="callback">The callback to be invoked upon a value change</param>
    /// <returns>An action to be invoked by the caller upon unsubscribing</returns>
    public Unsubscribe Subscribe(ValueChanged<T> callback);
}

/// <summary>
///     The <see cref="Delegate" />, which is invoked when a store's value changes
/// </summary>
/// <typeparam name="T">The value's type</typeparam>
public delegate void ValueChanged<in T>(T value);

/// <summary>
///     Stops receiving value change notifications from a store
/// </summary>
public delegate void Unsubscribe();