namespace Pass.Container.Repository.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M18 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "pscn.PassApple",
                c => new
                    {
                        PassNativeId = c.Int(nullable: false),
                        PassTypeId = c.String(nullable: false, maxLength: 128, unicode: false),
                    })
                .PrimaryKey(t => t.PassNativeId)
                .ForeignKey("pscn.PassNative", t => t.PassNativeId)
                .Index(t => t.PassNativeId);
            
            AddColumn("pscn.PassNative", "PassTemplateNativeId", c => c.Int(nullable: false));
            CreateIndex("pscn.PassNative", "PassTemplateNativeId");
            AddForeignKey("pscn.PassNative", "PassTemplateNativeId", "pscn.PassTemplateNative", "PassTemplateNativeId", cascadeDelete: true);
            DropColumn("pscn.Pass", "PassTypeId");
        }
        
        public override void Down()
        {
            AddColumn("pscn.Pass", "PassTypeId", c => c.String(nullable: false, maxLength: 128, unicode: false));
            DropForeignKey("pscn.PassApple", "PassNativeId", "pscn.PassNative");
            DropForeignKey("pscn.PassNative", "PassTemplateNativeId", "pscn.PassTemplateNative");
            DropIndex("pscn.PassApple", new[] { "PassNativeId" });
            DropIndex("pscn.PassNative", new[] { "PassTemplateNativeId" });
            DropColumn("pscn.PassNative", "PassTemplateNativeId");
            DropTable("pscn.PassApple");
        }
    }
}
