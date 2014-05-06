namespace Pass.Container.Repository.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("pscn.Pass", "PassTypeIdentifier");
        }
        
        public override void Down()
        {
            AddColumn("pscn.Pass", "PassTypeIdentifier", c => c.String(nullable: false, maxLength: 512));
        }
    }
}
