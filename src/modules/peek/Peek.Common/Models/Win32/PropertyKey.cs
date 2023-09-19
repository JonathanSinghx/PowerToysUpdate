﻿// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;

namespace Peek.Common.Models
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct PropertyKey
    {
        public Guid FormatId;
        public int PropertyId;

        public PropertyKey(Guid keyGuid, int propertyId)
        {
            this.FormatId = keyGuid;
            this.PropertyId = propertyId;
        }

        public PropertyKey(uint a, uint b, uint c, uint d, uint e, uint f, uint g, uint h, uint i, uint j, uint k, int propertyId)
            : this(new Guid((uint)a, (ushort)b, (ushort)c, (byte)d, (byte)e, (byte)f, (byte)g, (byte)h, (byte)i, (byte)j, (byte)k), propertyId)
        {
        }

        public override bool Equals(object? obj)
        {
            if ((obj == null) || !(obj is PropertyKey))
            {
                return false;
            }

            PropertyKey pk = (PropertyKey)obj;

            return FormatId.Equals(pk.FormatId) && (PropertyId == pk.PropertyId);
        }

        public static bool operator ==(PropertyKey a, PropertyKey b)
        {
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            return a.FormatId == b.FormatId && a.PropertyId == b.PropertyId;
        }

        public static bool operator !=(PropertyKey a, PropertyKey b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return FormatId.GetHashCode() ^ PropertyId;
        }

        // File properties: https://learn.microsoft.com/en-us/openspecs/windows_protocols/ms-wsp/2dbe759c-c955-4770-a545-e46d7f6332ed
        public static readonly PropertyKey ImageHorizontalSize = new PropertyKey(new Guid(0x6444048F, 0x4C8B, 0x11D1, 0x8B, 0x70, 0x08, 0x00, 0x36, 0xB1, 0x1A, 0x03), 3);
        public static readonly PropertyKey ImageVerticalSize = new PropertyKey(new Guid(0x6444048F, 0x4C8B, 0x11D1, 0x8B, 0x70, 0x08, 0x00, 0x36, 0xB1, 0x1A, 0x03), 4);
        public static readonly PropertyKey FileSizeBytes = new PropertyKey(new Guid(0xb725f130, 0x47ef, 0x101a, 0xa5, 0xf1, 0x02, 0x60, 0x8c, 0x9e, 0xeb, 0xac), 12);
        public static readonly PropertyKey FileType = new PropertyKey(new Guid(0xb725f130, 0x47ef, 0x101a, 0xa5, 0xf1, 0x02, 0x60, 0x8c, 0x9e, 0xeb, 0xac), 4);
        public static readonly PropertyKey FrameWidth = new PropertyKey(new Guid(0x64440491, 0x4C8B, 0x11D1, 0x8B, 0x70, 0x08, 0x00, 0x36, 0xB1, 0x1A, 0x03), 3);
        public static readonly PropertyKey FrameHeight = new PropertyKey(new Guid(0x64440491, 0x4C8B, 0x11D1, 0x8B, 0x70, 0x08, 0x00, 0x36, 0xB1, 0x1A, 0x03), 4);
    }
}
