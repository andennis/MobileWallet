using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Common.Repository.EF;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.Repositories;

namespace Pass.Manager.Repository.EF
{
    public class PassContentTemplateFieldRepository : Repository<PassContentTemplateField>, IPassContentTemplateFieldRepository
    {
        public PassContentTemplateFieldRepository(DbContextBase dbContext)
            : base(dbContext)
        {
        }

        public IEnumerable<PassProjectField> GetUnmappedFields(int passContentTemplateId)
        {
            return SqlQuery<PassProjectField>(DbScheme + ".PassContentTemplateField_GetUnmappedFields @PassContentTemplateId",
                                          new SqlParameter("PassContentTemplateId", passContentTemplateId)).ToList();
        }
    }
}
