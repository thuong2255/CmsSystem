namespace CmsSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIconToAction : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Actions", "Icon", c => c.String(maxLength: 100, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Actions", "Icon");
        }
    }
}
