// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Maple.Branch
{
    public class KeyValue : KeyValue<string, string>
    {
        public KeyValue(string key, string value) : base(key, value)
        {
        }
    }

    public class KeyValue<TKey, TValue>
    {
        public KeyValue(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }

        public TKey Key { get; set; }

        public TValue Value { get; set; }
    }
}
