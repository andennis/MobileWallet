namespace Pass.Manager.Repository.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M30 : DbMigration
    {
        public override void Up()
        {
            AddColumn("pm.PassContent", "IsVoided", c => c.Boolean(nullable: false));
            DropColumn("pm.PassContent", "IsValid");
        }
        
        public override void Down()
        {
            AddColumn("pm.PassContent", "IsValid", c => c.Boolean(nullable: false));
            DropColumn("pm.PassContent", "IsVoided");
        }
    }
}
