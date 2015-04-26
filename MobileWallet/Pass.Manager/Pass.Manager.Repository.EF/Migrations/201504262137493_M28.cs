namespace Pass.Manager.Repository.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M28 : DbMigration
    {
        public override void Up()
        {
            AddColumn("pm.PassContent", "AuthToken", c => c.String(nullable: false, maxLength: 64, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("pm.PassContent", "AuthToken");
        }
    }
}
