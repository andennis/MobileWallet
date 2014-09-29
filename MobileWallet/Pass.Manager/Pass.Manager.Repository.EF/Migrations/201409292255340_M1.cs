namespace Pass.Manager.Repository.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("pm.PassProject", "PassContentId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("pm.PassProject", "PassContentId");
        }
    }
}
