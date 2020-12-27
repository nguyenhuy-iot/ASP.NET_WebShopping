namespace WebShopping.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addproductimage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "image", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "image");
        }
    }
}
