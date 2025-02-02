﻿using Microsoft.TeamsAI.Utilities;
using System.Text.Json;

namespace Microsoft.TeamsAI.State
{
    public class TurnStateEntry<TValue> : IReadOnlyEntry<TValue> where TValue : class
    {
        private TValue _value;
        private string _hash;

        /// <summary>
        /// Constructs the turn state entry.
        /// </summary>
        /// <param name="value">The entry value</param>
        /// <param name="storageKey">The storage key used to store object entry</param>
        public TurnStateEntry(TValue value, string? storageKey = null)
        {
            Verify.ParamNotNull(value);

            _value = value;
            StorageKey = storageKey;
            _hash = ComputeHash(value);
        }

        /// <inheritdoc />
        public bool HasChanged
        {
            get { return ComputeHash(_value) != _hash; }
        }

        /// <inheritdoc />
        public bool IsDeleted { get; private set; } = false;

        /// <inheritdoc />
        public TValue Value
        {
            get
            {
                if (IsDeleted)
                {
                    IsDeleted = false;
                }

                return _value;
            }
        }

        /// <inheritdoc />
        public string? StorageKey { get; }

        /// <summary>
        ///  Deletes the entry.
        /// </summary>
        public void Delete()
        {
            IsDeleted = true;
        }

        /// <summary>
        /// Replaces the value in the entry.
        /// </summary>
        /// <param name="value">The entry value.</param>
        public void Replace(TValue value)
        {
            Verify.ParamNotNull(value);

            _value = value;
        }

        // TODO: Optimize if possible
        /// <summary>
        /// Computes the hash from the object
        /// </summary>
        /// <param name="obj">The object to compute has from</param>
        /// <returns>Returns a Json object representation </returns>
        internal static string ComputeHash(object obj)
        {
            Verify.ParamNotNull(obj);

            return JsonSerializer.Serialize(obj, new JsonSerializerOptions() { MaxDepth = 64 });
        }
    }
}
