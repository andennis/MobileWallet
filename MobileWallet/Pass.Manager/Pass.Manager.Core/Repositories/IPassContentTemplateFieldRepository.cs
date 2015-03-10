using System.Collections.Generic;
using Common.Repository;
using Pass.Manager.Core.Entities;

namespace Pass.Manager.Core.Repositories
{
    public interface IPassContentTemplateFieldRepository : IRepository<PassContentTemplateField>
    {
        IEnumerable<PassProjectField> GetUnmappedFields(int passContentTemplateId); 
    }
}