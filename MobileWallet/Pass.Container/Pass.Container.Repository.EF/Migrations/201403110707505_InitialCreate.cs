namespace Pass.Container.Repository.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "pscn.ClientDevice",
                c => new
                    {
                        ClientDeviceId = c.Int(nullable: false, identity: true),
                        DeviceId = c.String(nullable: false, maxLength: 400),
                    })
                .PrimaryKey(t => t.ClientDeviceId);
            
            CreateTable(
                "pscn.Registration",
                c => new
                    {
                        ClientDeviceId = c.Int(nullable: false),
                        PassId = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ClientDeviceId, t.PassId })
                .ForeignKey("pscn.ClientDevice", t => t.ClientDeviceId, cascadeDelete: true)
                .ForeignKey("pscn.Pass", t => t.PassId, cascadeDelete: true)
                .Index(t => t.ClientDeviceId)
                .Index(t => t.PassId);
            
            CreateTable(
                "pscn.Pass",
                c => new
                    {
                        PassId = c.Int(nullable: false, identity: true),
                        AuthToken = c.String(nullable: false, maxLength: 400),
                        SerialNumber = c.String(nullable: false, maxLength: 400),
                        PassTypeIdentifier = c.String(nullable: false, maxLength: 400),
                        ExpirationDate = c.DateTime(),
                        Status = c.Int(nullable: false),
                        PassTemplateId = c.Int(nullable: false),
                        Version = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PassId)
                .ForeignKey("pscn.PassTemplate", t => t.PassTemplateId)
                .Index(t => t.PassTemplateId);
            
            CreateTable(
                "pscn.PassFieldValue",
                c => new
                    {
                        PassFieldValueId = c.Int(nullable: false, identity: true),
                        PassFieldId = c.Int(nullable: false),
                        PassId = c.Int(nullable: false),
                        Label = c.String(maxLength: 400),
                        Value = c.String(maxLength: 400),
                        Version = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PassFieldValueId)
                .ForeignKey("pscn.Pass", t => t.PassId, cascadeDelete: true)
                .ForeignKey("pscn.PassField", t => t.PassFieldId)
                .Index(t => t.PassId)
                .Index(t => t.PassFieldId);
            
            CreateTable(
                "pscn.PassField",
                c => new
                    {
                        PassFieldId = c.Int(nullable: false, identity: true),
                        PassTemplateId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 400),
                        Version = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PassFieldId)
                .ForeignKey("pscn.PassTemplate", t => t.PassTemplateId, cascadeDelete: true)
                .Index(t => t.PassTemplateId);
            
            CreateTable(
                "pscn.PassTemplate",
                c => new
                    {
                        PassTemplateId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 400),
                        PackageId = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        Version = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PassTemplateId);
            
            CreateTable(
                "pscn.PassTemplateNative",
                c => new
                    {
                        PassTemplateNativeId = c.Int(nullable: false, identity: true),
                        PassTemplateId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PassTemplateNativeId)
                .ForeignKey("pscn.PassTemplate", t => t.PassTemplateId, cascadeDelete: true)
                .Index(t => t.PassTemplateId);
            
            CreateTable(
                "pscn.PassTemplateApple",
                c => new
                    {
                        PassTemplateNativeId = c.Int(nullable: false),
                        PassTypeId = c.String(nullable: false, maxLength: 400),
                    })
                .PrimaryKey(t => t.PassTemplateNativeId)
                .ForeignKey("pscn.PassTemplateNative", t => t.PassTemplateNativeId)
                .Index(t => t.PassTemplateNativeId);
            
            CreateTable(
                "pscn.ClientDeviceApple",
                c => new
                    {
                        ClientDeviceId = c.Int(nullable: false),
                        PushToken = c.String(nullable: false, maxLength: 400),
                    })
                .PrimaryKey(t => t.ClientDeviceId)
                .ForeignKey("pscn.ClientDevice", t => t.ClientDeviceId)
                .Index(t => t.ClientDeviceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("pscn.ClientDeviceApple", "ClientDeviceId", "pscn.ClientDevice");
            DropForeignKey("pscn.PassTemplateApple", "PassTemplateNativeId", "pscn.PassTemplateNative");
            DropForeignKey("pscn.Registration", "PassId", "pscn.Pass");
            DropForeignKey("pscn.Pass", "PassTemplateId", "pscn.PassTemplate");
            DropForeignKey("pscn.PassFieldValue", "PassFieldId", "pscn.PassField");
            DropForeignKey("pscn.PassField", "PassTemplateId", "pscn.PassTemplate");
            DropForeignKey("pscn.PassTemplateNative", "PassTemplateId", "pscn.PassTemplate");
            DropForeignKey("pscn.PassFieldValue", "PassId", "pscn.Pass");
            DropForeignKey("pscn.Registration", "ClientDeviceId", "pscn.ClientDevice");
            DropIndex("pscn.ClientDeviceApple", new[] { "ClientDeviceId" });
            DropIndex("pscn.PassTemplateApple", new[] { "PassTemplateNativeId" });
            DropIndex("pscn.Registration", new[] { "PassId" });
            DropIndex("pscn.Pass", new[] { "PassTemplateId" });
            DropIndex("pscn.PassFieldValue", new[] { "PassFieldId" });
            DropIndex("pscn.PassField", new[] { "PassTemplateId" });
            DropIndex("pscn.PassTemplateNative", new[] { "PassTemplateId" });
            DropIndex("pscn.PassFieldValue", new[] { "PassId" });
            DropIndex("pscn.Registration", new[] { "ClientDeviceId" });
            DropTable("pscn.ClientDeviceApple");
            DropTable("pscn.PassTemplateApple");
            DropTable("pscn.PassTemplateNative");
            DropTable("pscn.PassTemplate");
            DropTable("pscn.PassField");
            DropTable("pscn.PassFieldValue");
            DropTable("pscn.Pass");
            DropTable("pscn.Registration");
            DropTable("pscn.ClientDevice");
        }
    }
}
