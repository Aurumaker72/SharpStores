namespace SharpStores;

public interface ISet<in T>
{
    /// <summary>
    /// Sets a store's value to the <see cref="value"/> and notifies all subscribers
    /// </summary>
    /// <typeparam name="T">The value's type</typeparam>
    public void Set(T value);
}

/// <summary>
/// Sets a store's value to the <see cref="value"/> and notifies all subscribers
/// </summary>
/// <typeparam name="T">The value's type</typeparam>
public delegate void Set<in T>(T value);