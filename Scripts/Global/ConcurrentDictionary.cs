/// <summary>
/// 
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TValue"></typeparam>
/// 
using System;
using System.Collections.Generic;

public sealed class ConcurrentDictionary<TKey, TValue>
{
    private readonly object locker = new object();
    private Dictionary<TKey, TValue> dict;


    public ConcurrentDictionary(int capacity)
    {
        dict = new Dictionary<TKey, TValue>(capacity);
    }

    public ConcurrentDictionary()
    {
        dict = new Dictionary<TKey, TValue>();
    }

    /// <summary>
    /// Gets the number of key/value pairs contained within.
    /// </summary>
    public int Count
    {
        get
        {
            lock (locker)
            {
                return dict.Count;
            }
        }
    }

    /// <summary>
    /// Gets a value that indicates whether the dictionary is empty.
    /// </summary>
    public bool IsEmpty
    {
        get
        {
            lock (locker)
            {
                return dict.Count > 0;
            }
        }
    }

    /// <summary>
    /// Gets or sets the value associated with the specified key.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public TValue this[TKey key]
    {
        get
        {
            lock (locker)
            {
                return dict[key];
            }

        }

        set
        {
            lock (locker)
            {
                dict[key] = value;
            }

        }
    }

    /// <summary>
    /// Removes all keys and values from the dictionary.
    /// </summary>
    public void Clear()
    {
        lock (locker)
        {
            dict.Clear();
        }
    }

    /// <summary>
    /// Determines whether the dictionary contains the specified key.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool ContainsKey(TKey key)
    {
        lock (locker)
        {
            return dict.ContainsKey(key);
        }
    }

    /// <summary>
    /// Copies the key and value pairs stored in the dictionary to a new array.
    /// </summary>
    /// <returns></returns>
    public KeyValuePair<TKey, TValue>[] ToArray()
    {
        lock (locker)
        {
            var keys = dict.Keys;
            var values = dict.Values;
            int count = dict.Count;
        }


        lock (locker)
        {
            var output = new KeyValuePair<TKey, TValue>[dict.Count];
            int i = 0;
            foreach (KeyValuePair<TKey, TValue> kvp in dict)
            {
                output[i] = kvp;
                i++;
            }
            return output;
        }
    }

    /// <summary>
    /// Attempts to add the specified key and value to the dictionary.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns>True if the key/value pair was added to the dictionary successfully. False if the key already exists.</returns>
    public bool TryAdd(TKey key, TValue value)
    {
        lock (locker)
        {
            if (dict.ContainsKey(key)) return false;
            dict.Add(key, value);
            return true;
        }
    }

    /// <summary>
    /// Adds a key/value pair to the ConcurrentDictionary&lt;TKey, TValue&gt;
    /// if the key does not already exist, or updates a key/value pair
    /// in the ConcurrentDictionary&lt;TKey, TValue&gt; by using the
    /// specified function if the key already exists.
    /// </summary>
    /// <param name="key">The key to be added or whose value should be updated.</param>
    /// <param name="addValue">The value to be added for an absent key.</param>
    /// <param name="updateValueFactory">The function used to generate a new value
    /// for an existing key based on the key's existing value.</param>
    /// <returns>The new value for the key. This will be either be addValue
    /// (if the key was absent) or the result of updateValueFactory (if the key was present).</returns>
    public TValue AddOrUpdate(
        TKey key,
        TValue addValue,
        Func<TKey, TValue, TValue> updateValueFactory)
    {
        lock (locker)
        {
            dict[key] = dict.ContainsKey(key) ?
                updateValueFactory(key, dict[key]) :
                addValue;
            return dict[key];
        }
    }

    /// <summary>
    /// Uses the specified functions to add a key/value pair to the
    /// ConcurrentDictionary&lt;TKey, TValue&gt; if the key does not
    /// already exist, or to update a key/value pair in the
    /// ConcurrentDictionary&lt;TKey, TValue&gt; if the key already exists.
    /// </summary>
    /// <param name="key">The key to be added or whose value should be updated.</param>
    /// <param name="addValueFactory">The function used to generate a value for an absent key.</param>
    /// <param name="updateValueFactory">The function used to generate a new value for an existing key based on the key's existing value.</param>
    /// <returns></returns>
    public TValue AddOrUpdate(
        TKey key,
        Func<TKey, TValue> addValueFactory,
        Func<TKey, TValue, TValue> updateValueFactory)
    {
        lock (locker)
        {
            dict[key] = dict.ContainsKey(key) ?
                updateValueFactory(key, dict[key]) :
                addValueFactory(key);
            return dict[key];
        }
    }
}