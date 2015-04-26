namespace Pass.Manager.Repository.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M27 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "pm.PassContentField",
                c => new
                    {
                        PassContentFieldId = c.Int(nullable: false, identity: true),
                        PassProjectFieldId = c.Int(nullable: false),
                        FieldLabel = c.String(maxLength: 512),
                        FieldValue = c.String(maxLength: 512),
                        Version = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PassContentFieldId)
                .ForeignKey("pm.PassProjectField", t => t.PassProjectFieldId)
                .Index(t => t.PassProjectFieldId);
            
            AddColumn("pm.PassContent", "ExpDate", c => c.DateTime());
            AddColumn("pm.PassContent", "IsValid", c => c.Boolean(nullable: false, defaultValue:true));
        }
        
        public override void Down()
        {
            DropForeignKey("pm.PassContentField", "PassProjectFieldId", "pm.PassProjectField");
            DropIndex("pm.PassContentField", new[] { "PassProjectFieldId" });
            DropColumn("pm.PassContent", "IsValid");
            DropColumn("pm.PassContent", "ExpDate");
            DropTable("pm.PassContentField");
        }
    }
}
