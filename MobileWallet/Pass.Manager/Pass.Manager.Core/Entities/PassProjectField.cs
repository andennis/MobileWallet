using Common.Repository;

namespace Pass.Manager.Core.Entities
{
    public class PassProjectField : EntityVersionable//, IEntityWithId
    {
        /*
        #region IEntityWithId
        public int EntityId { get { return PassFieldId; } }
        #endregion
        */
        public int PassProjectFieldId { get; set; }
        public string Name { get; set; }
        public string DefaultValue { get; set; }
        public string DefaultLabel { get; set; }
        public int PassProjectId { get; set; }
        public PassProject PassProject { get; set; }
    }
}
