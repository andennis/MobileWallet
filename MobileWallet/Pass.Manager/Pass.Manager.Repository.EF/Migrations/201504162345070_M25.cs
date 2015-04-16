namespace Pass.Manager.Repository.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M25 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("pm.PassContent", "SerialNumber", c => c.String(nullable: false, maxLength: 64, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("pm.PassContent", "SerialNumber", c => c.String(nullable: false));
        }
    }
}
