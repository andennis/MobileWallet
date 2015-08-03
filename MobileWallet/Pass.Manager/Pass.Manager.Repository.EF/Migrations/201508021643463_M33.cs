namespace Pass.Manager.Repository.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M33 : DbMigration
    {
        public override void Up()
        {
            AddColumn("pm.PassContent", "PassContentTemplateVersion", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("pm.PassContent", "PassContentTemplateVersion");
        }
    }
}
