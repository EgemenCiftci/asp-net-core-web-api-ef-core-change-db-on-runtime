using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspNetCoreWebApiEfCoreChangeDbOnRuntime.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            _ = migrationBuilder.RenameColumn(
                name: "InvoiceItemId",
                table: "InvoiceItems",
                newName: "Id");

            _ = migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Customers",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            _ = migrationBuilder.RenameColumn(
                name: "Id",
                table: "InvoiceItems",
                newName: "InvoiceItemId");

            _ = migrationBuilder.RenameColumn(
                name: "Id",
                table: "Customers",
                newName: "CustomerId");
        }
    }
}
