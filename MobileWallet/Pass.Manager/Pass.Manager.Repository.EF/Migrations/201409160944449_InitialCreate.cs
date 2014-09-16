namespace Pass.Manager.Repository.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "pm.PassCertificate",
                c => new
                    {
                        PassCertificateId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 512),
                        Description = c.String(),
                        ExpDate = c.DateTime(nullable: false),
                        CertificateStorageId = c.Int(nullable: false),
                        Version = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PassCertificateId);
            
            CreateTable(
                "pm.PassSiteCertificate",
                c => new
                    {
                        PassSiteId = c.Int(nullable: false),
                        PassCertificateId = c.Int(nullable: false),
                        Version = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.PassSiteId, t.PassCertificateId })
                .ForeignKey("pm.PassCertificate", t => t.PassCertificateId, cascadeDelete: true)
                .ForeignKey("pm.PassSite", t => t.PassSiteId, cascadeDelete: true)
                .Index(t => t.PassSiteId)
                .Index(t => t.PassCertificateId);
            
            CreateTable(
                "pm.PassSite",
                c => new
                    {
                        PassSiteId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 512),
                        Description = c.String(),
                        Version = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PassSiteId);
            
            CreateTable(
                "pm.PassProject",
                c => new
                    {
                        PassProjectId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 512),
                        Description = c.String(),
                        PassTemplateId = c.Int(nullable: false),
                        PassSiteId = c.Int(nullable: false),
                        ProjectType = c.Int(nullable: false),
                        Version = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PassProjectId)
                .ForeignKey("pm.PassSite", t => t.PassSiteId, cascadeDelete: true)
                .Index(t => t.PassSiteId);
            
            CreateTable(
                "pm.PassSiteUser",
                c => new
                    {
                        PassSiteId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        Version = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.PassSiteId, t.UserId })
                .ForeignKey("pm.PassSite", t => t.PassSiteId, cascadeDelete: true)
                .ForeignKey("pm.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.PassSiteId)
                .Index(t => t.UserId);
            
            CreateTable(
                "pm.User",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 512),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Password = c.String(),
                        Version = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .Index(t => t.UserName, unique: true, name: "IX_User_Name");
            
            CreateTable(
                "pm.PassCertificateApple",
                c => new
                    {
                        PassCertificateId = c.Int(nullable: false),
                        TeamId = c.String(nullable: false, maxLength: 128),
                        PassTypeId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.PassCertificateId)
                .ForeignKey("pm.PassCertificate", t => t.PassCertificateId)
                .Index(t => t.PassCertificateId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("pm.PassCertificateApple", "PassCertificateId", "pm.PassCertificate");
            DropForeignKey("pm.PassSiteCertificate", "PassSiteId", "pm.PassSite");
            DropForeignKey("pm.PassSiteUser", "UserId", "pm.User");
            DropForeignKey("pm.PassSiteUser", "PassSiteId", "pm.PassSite");
            DropForeignKey("pm.PassProject", "PassSiteId", "pm.PassSite");
            DropForeignKey("pm.PassSiteCertificate", "PassCertificateId", "pm.PassCertificate");
            DropIndex("pm.PassCertificateApple", new[] { "PassCertificateId" });
            DropIndex("pm.User", "IX_User_Name");
            DropIndex("pm.PassSiteUser", new[] { "UserId" });
            DropIndex("pm.PassSiteUser", new[] { "PassSiteId" });
            DropIndex("pm.PassProject", new[] { "PassSiteId" });
            DropIndex("pm.PassSiteCertificate", new[] { "PassCertificateId" });
            DropIndex("pm.PassSiteCertificate", new[] { "PassSiteId" });
            DropTable("pm.PassCertificateApple");
            DropTable("pm.User");
            DropTable("pm.PassSiteUser");
            DropTable("pm.PassProject");
            DropTable("pm.PassSite");
            DropTable("pm.PassSiteCertificate");
            DropTable("pm.PassCertificate");
        }
    }
}
