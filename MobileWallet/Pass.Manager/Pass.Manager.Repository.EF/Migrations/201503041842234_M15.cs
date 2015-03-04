namespace Pass.Manager.Repository.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M15 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("pm.PassBarcode", "MessageEncoding", c => c.String(maxLength: 32));
        }
        
        public override void Down()
        {
            AlterColumn("pm.PassBarcode", "MessageEncoding", c => c.String());
        }
    }
}
