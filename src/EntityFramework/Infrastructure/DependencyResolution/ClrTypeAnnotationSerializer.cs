﻿// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

namespace System.Data.Entity.Infrastructure.DependencyResolution
{
    using System.Data.Entity.Utilities;
    using System.Diagnostics;
    using System.Reflection;

    internal class ClrTypeAnnotationSerializer : IMetadataAnnotationSerializer
    {
        public string SerializeValue(string name, object value)
        {
            DebugCheck.NotEmpty(name);
            DebugCheck.NotNull(value);
            Debug.Assert(value is Type);

            return ((Type)value).AssemblyQualifiedName;
        }

        public object DeserializeValue(string name, string value)
        {
            DebugCheck.NotEmpty(name);
            DebugCheck.NotNull(value);

            // We avoid throwing here if the type could not be loaded because we might be loading an
            // old EDMX from, for example, the MigrationHistory table, and the CLR type might no longer exist.
            return Type.GetType(value, throwOnError: false);
        }
    }
}
