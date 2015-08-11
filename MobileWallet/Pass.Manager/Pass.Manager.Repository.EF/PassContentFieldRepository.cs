using System.Data.SqlClient;
using Pass.Manager.Core.Entities;
using Common.Repository.EF;
using Pass.Manager.Core.Exceptions;
using Pass.Manager.Core.Repositories;

namespace Pass.Manager.Repository.EF
{
    public class PassContentFieldRepository : Repository<PassContentField>, IPassContentFieldRepository
    {
        public PassContentFieldRepository(DbContextBase dbContext)
            : base(dbContext)
        {
        }

        public PassContentFieldView GetView(int passContentId, int passProjectFieldId)
        {
            return GetView(null, passContentId, passProjectFieldId);
        }

        public override TEntityView GetView<TEntityView>(int entityId)
        {
            if (typeof(TEntityView) != typeof(PassContentFieldView))
                throw new PassManagerGeneralException("TEntityType should be PassContentFieldView");

            return GetView(entityId, null, null) as TEntityView;
        }

        private PassContentFieldView GetView(int? passContentFieldId, int? passContentId, int? passProjectFieldId)
        {
            return SqlQueryScalarStoredProc<PassContentFieldView>(DbScheme + ".PassContentField_GetView",
                                        new SqlParameter("PassContentFieldId", passContentFieldId),
                                        new SqlParameter("PassContentId", passContentId),
                                        new SqlParameter("PassProjectFieldId", passProjectFieldId));
        }

    }

}
