namespace ecommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ItemVenda : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ItemVenda",
                c => new
                    {
                        ItemVendaId = c.Int(nullable: false, identity: true),
                        Quantidade = c.Int(nullable: false),
                        Valor = c.Double(nullable: false),
                        Data = c.DateTime(nullable: false),
                        Produto_ProdutoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ItemVendaId)
                .ForeignKey("dbo.Produto", t => t.Produto_ProdutoId, cascadeDelete: true)
                .Index(t => t.Produto_ProdutoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ItemVenda", "Produto_ProdutoId", "dbo.Produto");
            DropIndex("dbo.ItemVenda", new[] { "Produto_ProdutoId" });
            DropTable("dbo.ItemVenda");
        }
    }
}
