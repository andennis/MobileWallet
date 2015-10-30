using System.Web.Mvc;

namespace Common.Web.Controls.DropDownList
{
    public class CrudOperationBuilder : CrudOperationBuilderBase<CrudOperationBuilder>
    {
        //public CrudOperationBuilder(CrudOperation operation, ViewContext viewContext, IUrlGenerator urlGenerator): base(operation, viewContext, urlGenerator)
        public CrudOperationBuilder(CrudOperation operation, ViewContext viewContext)
            : base(operation, viewContext)
        {
          
        }
    }
}
