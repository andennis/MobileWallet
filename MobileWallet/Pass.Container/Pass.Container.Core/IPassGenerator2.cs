using System.Collections.Generic;
using Pass.Container.Core.Entities;

namespace Pass.Container.Core
{
    public interface IPassGenerator2
    {
        string GeneratePass(string serialNumber, IEnumerable<PassFieldInfo> fields, string dstPassFilesPath);
    }
}