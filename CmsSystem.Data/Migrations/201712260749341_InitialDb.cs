namespace CmsSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActionRoles",
                c => new
                    {
                        RoleId = c.Int(nullable: false),
                        ActionId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RoleId, t.ActionId })
                .ForeignKey("dbo.Actions", t => t.ActionId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.ActionId);
            
            CreateTable(
                "dbo.Actions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Link = c.String(maxLength: 255, unicode: false),
                        Description = c.String(maxLength: 500),
                        ParentId = c.Int(),
                        Display = c.Boolean(nullable: false),
                        Position = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        Description = c.String(maxLength: 500),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FunctionRoles",
                c => new
                    {
                        RoleId = c.Int(nullable: false),
                        FunctionId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RoleId, t.FunctionId })
                .ForeignKey("dbo.Functions", t => t.FunctionId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.FunctionId);
            
            CreateTable(
                "dbo.Functions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 255, unicode: false),
                        Description = c.String(maxLength: 500),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        ProductCategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductCategories", t => t.ProductCategoryId, cascadeDelete: true)
                .Index(t => t.ProductCategoryId);
            
            CreateTable(
                "dbo.RoleUsers",
                c => new
                    {
                        RoleId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 100, unicode: false),
                        Password = c.String(nullable: false, maxLength: 100, unicode: false),
                        Salt = c.String(nullable: false, maxLength: 100, unicode: false),
                        FullName = c.String(maxLength: 100),
                        Address = c.String(maxLength: 150),
                        Email = c.String(maxLength: 100, unicode: false),
                        Mobile = c.String(maxLength: 20, unicode: false),
                        Description = c.String(maxLength: 200),
                        IsAdmin = c.Boolean(nullable: false),
                        Status = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastLogin = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RoleUsers", "UserId", "dbo.Users");
            DropForeignKey("dbo.RoleUsers", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Products", "ProductCategoryId", "dbo.ProductCategories");
            DropForeignKey("dbo.FunctionRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.FunctionRoles", "FunctionId", "dbo.Functions");
            DropForeignKey("dbo.ActionRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.ActionRoles", "ActionId", "dbo.Actions");
            DropIndex("dbo.RoleUsers", new[] { "UserId" });
            DropIndex("dbo.RoleUsers", new[] { "RoleId" });
            DropIndex("dbo.Products", new[] { "ProductCategoryId" });
            DropIndex("dbo.FunctionRoles", new[] { "FunctionId" });
            DropIndex("dbo.FunctionRoles", new[] { "RoleId" });
            DropIndex("dbo.ActionRoles", new[] { "ActionId" });
            DropIndex("dbo.ActionRoles", new[] { "RoleId" });
            DropTable("dbo.Users");
            DropTable("dbo.RoleUsers");
            DropTable("dbo.Products");
            DropTable("dbo.ProductCategories");
            DropTable("dbo.Functions");
            DropTable("dbo.FunctionRoles");
            DropTable("dbo.Roles");
            DropTable("dbo.Actions");
            DropTable("dbo.ActionRoles");
        }
    }
}
