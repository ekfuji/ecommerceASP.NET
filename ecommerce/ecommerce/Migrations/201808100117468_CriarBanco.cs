namespace ecommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CriarBanco : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categoria",
                c => new
                    {
                        CategoriaId = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.CategoriaId);
            
            AddColumn("dbo.Produtos", "CategoriaId", c => c.Int(nullable: false));
            CreateIndex("dbo.Produtos", "CategoriaId");
            AddForeignKey("dbo.Produtos", "CategoriaId", "dbo.Categoria", "CategoriaId", cascadeDelete: true);
            DropColumn("dbo.Produtos", "Categoria");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Produtos", "Categoria", c => c.String(nullable: false, maxLength: 50));
            DropForeignKey("dbo.Produtos", "CategoriaId", "dbo.Categoria");
            DropIndex("dbo.Produtos", new[] { "CategoriaId" });
            DropColumn("dbo.Produtos", "CategoriaId");
            DropTable("dbo.Categoria");
        }
    }
}
