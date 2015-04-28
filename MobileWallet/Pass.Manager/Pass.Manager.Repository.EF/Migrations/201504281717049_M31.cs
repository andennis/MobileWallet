namespace Pass.Manager.Repository.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M31 : DbMigration
    {
        public override void Up()
        {
            AddColumn("pm.PassContent", "ContainerPassId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("pm.PassContent", "ContainerPassId");
        }
    }
}
