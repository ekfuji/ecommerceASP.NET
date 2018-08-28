namespace ecommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pedidoAlterado : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pedido", "CarrinhoId", c => c.String(maxLength: 100));
            AddColumn("dbo.Pedido", "NomeCliente", c => c.String(maxLength: 100));
            AddColumn("dbo.Pedido", "EnderecoCliente", c => c.String(maxLength: 100));
            AddColumn("dbo.Pedido", "TelefoneCliente", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pedido", "TelefoneCliente");
            DropColumn("dbo.Pedido", "EnderecoCliente");
            DropColumn("dbo.Pedido", "NomeCliente");
            DropColumn("dbo.Pedido", "CarrinhoId");
        }
    }
}
