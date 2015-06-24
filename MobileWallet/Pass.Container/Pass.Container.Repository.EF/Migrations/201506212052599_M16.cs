namespace Pass.Container.Repository.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M16 : DbMigration
    {
        public override void Up()
        {
            AddColumn("pscn.Pass", "PassFileStorageId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("pscn.Pass", "PassFileStorageId");
        }
    }
}
