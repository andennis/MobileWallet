using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Common.Repository.EF;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.Repositories;

namespace Pass.Manager.Repository.EF
{
    public class PassContentTemplateFieldRepository : PassManagerDefaultRepository<PassContentTemplateField>, IPassContentTemplateFieldRepository
    {
        public PassContentTemplateFieldRepository(DbContextBase dbContext)
            : base(dbContext)
        {
        }

        public IEnumerable<PassProjectField> GetUnmappedFields(int passContentTemplateId, int? curPassProjectFieldId = null)
        {
            return SqlQuery<PassProjectField>(DbScheme + ".PassContentTemplateField_GetUnmappedFields @PassContentTemplateId, @CurPassProjectFieldId",
                                          new SqlParameter("PassContentTemplateId", passContentTemplateId),
                                          new SqlParameter("CurPassProjectFieldId", curPassProjectFieldId ?? (object)DBNull.Value)).ToList();
        }
    }
}
