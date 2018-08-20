namespace ecommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DoubleToDecimal_and_ItemVenda : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categoria",
                c => new
                    {
                        CategoriaId = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.CategoriaId);
            
            CreateTable(
                "dbo.ItemVenda",
                c => new
                    {
                        ItemVendaId = c.Int(nullable: false, identity: true),
                        Quantidade = c.Int(nullable: false),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Produto_ProdutoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ItemVendaId)
                .ForeignKey("dbo.Produto", t => t.Produto_ProdutoId, cascadeDelete: true)
                .Index(t => t.Produto_ProdutoId);
            
            CreateTable(
                "dbo.Produto",
                c => new
                    {
                        ProdutoId = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 50, unicode: false),
                        Descricao = c.String(maxLength: 100, unicode: false),
                        Preco = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CategoriaId = c.Int(nullable: false),
                        Imagem = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.ProdutoId)
                .ForeignKey("dbo.Categoria", t => t.CategoriaId, cascadeDelete: true)
                .Index(t => t.CategoriaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ItemVenda", "Produto_ProdutoId", "dbo.Produto");
            DropForeignKey("dbo.Produto", "CategoriaId", "dbo.Categoria");
            DropIndex("dbo.Produto", new[] { "CategoriaId" });
            DropIndex("dbo.ItemVenda", new[] { "Produto_ProdutoId" });
            DropTable("dbo.Produto");
            DropTable("dbo.ItemVenda");
            DropTable("dbo.Categoria");
        }
    }
}
