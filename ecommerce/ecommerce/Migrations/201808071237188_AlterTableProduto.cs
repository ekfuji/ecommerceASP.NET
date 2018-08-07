namespace ecommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterTableProduto : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Produtos", "Nome", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Produtos", "Categoria", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Produtos", "Categoria", c => c.String());
            AlterColumn("dbo.Produtos", "Nome", c => c.String());
        }
    }
}
