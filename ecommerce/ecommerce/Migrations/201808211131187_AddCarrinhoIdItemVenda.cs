namespace ecommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCarrinhoIdItemVenda : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ItemVenda", "Data", c => c.DateTime(nullable: false));
            AddColumn("dbo.ItemVenda", "CarrinhoId", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ItemVenda", "CarrinhoId");
            DropColumn("dbo.ItemVenda", "Data");
        }
    }
}
