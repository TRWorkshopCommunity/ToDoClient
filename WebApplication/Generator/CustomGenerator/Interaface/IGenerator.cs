// <copyright file="IGenerator.cs" company="Sprocket Enterprises">
//     Copyright (c) Ilya Myalik. All rights reserved.
// </copyright>

namespace Generator.CustomGenerator.Interface
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Generator interface.
    /// </summary>
    public interface IGenerator : IEnumerator<int>, ICloneable
    {
    }
}