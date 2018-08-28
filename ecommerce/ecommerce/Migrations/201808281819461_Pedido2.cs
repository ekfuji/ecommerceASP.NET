namespace ecommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Pedido2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PedidoItemVenda", "Pedido_PedidoId", "dbo.Pedido");
            DropForeignKey("dbo.PedidoItemVenda", "ItemVenda_ItemVendaId", "dbo.ItemVenda");
            DropIndex("dbo.PedidoItemVenda", new[] { "Pedido_PedidoId" });
            DropIndex("dbo.PedidoItemVenda", new[] { "ItemVenda_ItemVendaId" });
            AddColumn("dbo.ItemVenda", "Pedido_PedidoId", c => c.Int());
            CreateIndex("dbo.ItemVenda", "Pedido_PedidoId");
            AddForeignKey("dbo.ItemVenda", "Pedido_PedidoId", "dbo.Pedido", "PedidoId");
            DropTable("dbo.PedidoItemVenda");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PedidoItemVenda",
                c => new
                    {
                        Pedido_PedidoId = c.Int(nullable: false),
                        ItemVenda_ItemVendaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Pedido_PedidoId, t.ItemVenda_ItemVendaId });
            
            DropForeignKey("dbo.ItemVenda", "Pedido_PedidoId", "dbo.Pedido");
            DropIndex("dbo.ItemVenda", new[] { "Pedido_PedidoId" });
            DropColumn("dbo.ItemVenda", "Pedido_PedidoId");
            CreateIndex("dbo.PedidoItemVenda", "ItemVenda_ItemVendaId");
            CreateIndex("dbo.PedidoItemVenda", "Pedido_PedidoId");
            AddForeignKey("dbo.PedidoItemVenda", "ItemVenda_ItemVendaId", "dbo.ItemVenda", "ItemVendaId", cascadeDelete: true);
            AddForeignKey("dbo.PedidoItemVenda", "Pedido_PedidoId", "dbo.Pedido", "PedidoId", cascadeDelete: true);
        }
    }
}
