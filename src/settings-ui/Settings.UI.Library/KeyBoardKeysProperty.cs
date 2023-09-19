﻿// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Text.Json.Serialization;

namespace Microsoft.PowerToys.Settings.UI.Library
{
    public class KeyboardKeysProperty
    {
        public KeyboardKeysProperty()
        {
            Value = new HotkeySettings();
        }

        public KeyboardKeysProperty(HotkeySettings hkSettings)
        {
            Value = hkSettings;
        }

        [JsonPropertyName("value")]
        public HotkeySettings Value { get; set; }
    }
}
