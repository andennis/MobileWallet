namespace Pass.Container.Repository.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("pscn.Pass", "PassTypeId", c => c.String(nullable: false, maxLength: 128, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("pscn.Pass", "PassTypeId");
        }
    }
}
