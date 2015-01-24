using System;
using System.Collections.Generic;

namespace KinectFittingRoom.ViewModel.Helpers
{
    public class OrderedDictionary<TKey, TValue>
    {
        #region Private Fields

        private Dictionary<TKey, TValue> _dictionary;

        /// <summary>
        /// The keys added to the dictionary
        /// </summary>
        private List<TKey> _dictionaryKeys;

        #endregion Private Fields
        #region Public Properties
        /// <summary>
        /// Gets the <see cref="TValue"/> with the specified key.
        /// </summary>
        /// <value>
        /// The <see cref="TValue"/>.
        /// </value>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public TValue this[TKey key]
        {
            get { return _dictionary[key]; }
            set
            {
                _dictionary[key] = value;
                _dictionaryKeys.Remove(key);
                _dictionaryKeys.Add(key);
            }
        }
        /// <summary>
        /// Gets the number of items.
        /// </summary>
        /// <value>
        /// The number of items.
        /// </value>
        public int Count { get { return _dictionaryKeys.Count; } }
        /// <summary>
        /// Gets the last added value.
        /// </summary>
        /// <value>
        /// The last added value.
        /// </value>
        public TValue Last
        {
            get
            {
                TValue value;
                try
                {
                    _dictionary.TryGetValue(LastKey, out value);
                }
                catch (Exception)
                {
                    value = default(TValue);
                }
                return value;
            }
        }
        /// <summary>
        /// Gets the last key.
        /// </summary>
        /// <value>
        /// The last key.
        /// </value>
        public TKey LastKey { get { return _dictionaryKeys[_dictionaryKeys.Count - 1]; } }
        /// <summary>
        /// Gets the values from the dictionary
        /// </summary>
        /// <value>
        /// The values from the dictionary.
        /// </value>
        public Dictionary<TKey, TValue>.ValueCollection Values { get { return _dictionary.Values; } }
        #endregion Public Properties
        #region .ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderedDictionary{TKey, TValue}"/> class.
        /// </summary>
        public OrderedDictionary()
        {
            _dictionary = new Dictionary<TKey, TValue>();
            _dictionaryKeys = new List<TKey>();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderedDictionary{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="dictionary">The dictionary.</param>
        public OrderedDictionary(OrderedDictionary<TKey, TValue> dictionary)
        {
            _dictionary = new Dictionary<TKey, TValue>(dictionary._dictionary);
            _dictionaryKeys = new List<TKey>(dictionary._dictionaryKeys);
        }
        #endregion .ctor
        #region Public Methods
        /// <summary>
        /// Removes the specified key from the dictionary.
        /// </summary>
        /// <param name="key">The key.</param>
        public void Remove(TKey key)
        {
            _dictionaryKeys.Remove(key);
            try
            {
                _dictionary.Remove(key);
            }
            catch (Exception)
            {
                Console.WriteLine("No such key in the dictionary.");
            }
        }
        /// <summary>
        /// Determines whether the specified key contains key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>True if the dictionary contains such key, false otherwise</returns>
        public bool ContainsKey(TKey key)
        {
            return _dictionaryKeys.Contains(key);
        }
        #endregion Public Methods
    }
}
