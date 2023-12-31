﻿using System.Collections.Generic;
using Pass.Container.Core.Entities;

namespace Pass.Container.Core
{
    public interface IPassGenerator
    {
        string GeneratePass(string authToken, string serialNumber, IEnumerable<PassFieldInfo> fields, string dstPassFilesPath);
    }
}