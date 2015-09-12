using System.Collections.Generic;
using System.Data;
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

        /*
        public override void Insert(PassContentTemplateField entity)
        {
            ExecuteNonQueryStoredProc(DbScheme + ".PassContentTemplateField_Insert",
                new SqlParameter("PassContentTemplateFieldId", SqlDbType.Int) { Direction = ParameterDirection.Output },
                new SqlParameter("FieldKind", (int)entity.FieldKind),
                new SqlParameter("AttributedValue", entity.AttributedValue),
                new SqlParameter("ChangeMessage", entity.ChangeMessage),
                new SqlParameter("TextAlignment", entity.TextAlignment),
                new SqlParameter("Name", entity.Name),
                new SqlParameter("Label", entity.Label),
                new SqlParameter("Value", entity.Value),
                new SqlParameter("PassProjectFieldId", entity.PassProjectFieldId),
                new SqlParameter("PassContentTemplateId", entity.PassContentTemplateId));
        }
        */

        public IEnumerable<PassProjectField> GetUnmappedFields(int passContentTemplateId, int? curPassProjectFieldId = null)
        {
            return SqlQueryStoredProc<PassProjectField>(DbScheme + ".PassContentTemplateField_GetUnmappedFields",
                                          new SqlParameter("PassContentTemplateId", passContentTemplateId),
                                          new SqlParameter("CurPassProjectFieldId", curPassProjectFieldId)).ToList();
        }
    }
}
