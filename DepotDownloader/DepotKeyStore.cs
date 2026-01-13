// This file is subject to the terms and conditions defined
// in file 'LICENSE', which is part of this source code package.

using System;
using System.Collections.Generic;

namespace DepotDownloader
{
    internal static class DepotKeyStore
    {
        private static readonly Dictionary<uint, byte[]> depotKeysCache = new Dictionary<uint, byte[]>();

        public static void AddAll(string[] values)
        {
            foreach (var value in values)
            {
                var split = value.Split(';');

                if (split.Length != 2)
                {
                    throw new FormatException($"Invalid depot key line: {value}");
                }

                depotKeysCache.Add(uint.Parse(split[0]), Convert.FromHexString(split[1]));
            }
        }

        public static void Add(uint depotId, string decryptionkey)
        {
            var decryptionkeyBytes = Convert.FromHexString(decryptionkey);
            depotKeysCache.Add(depotId, decryptionkeyBytes);
        }

        public static bool ContainsKey(uint depotId)
        {
            return depotKeysCache.ContainsKey(depotId);
        }

        public static bool TryGetValue(uint depotId, out byte[] value)
        {
            return depotKeysCache.TryGetValue(depotId, out value);
        }

        public static byte[] Get(uint depotId)
        {
            return depotKeysCache[depotId];
        }
    }
}
