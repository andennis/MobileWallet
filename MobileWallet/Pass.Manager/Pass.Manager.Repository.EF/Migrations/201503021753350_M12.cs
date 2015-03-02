namespace Pass.Manager.Repository.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M12 : DbMigration
    {
        public override void Up()
        {
            AddColumn("pm.PassContentTemplate", "Name", c => c.String(nullable: false, maxLength: 512));
        }
        
        public override void Down()
        {
            DropColumn("pm.PassContentTemplate", "Name");
        }
    }
}
