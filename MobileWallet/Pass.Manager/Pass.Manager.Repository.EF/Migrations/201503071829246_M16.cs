namespace Pass.Manager.Repository.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M16 : DbMigration
    {
        public override void Up()
        {
            AddColumn("pm.PassContentTemplate", "TransitType", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("pm.PassContentTemplate", "TransitType");
        }
    }
}
