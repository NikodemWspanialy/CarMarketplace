using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarMarketplace.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMissingFieledAndEntitiesToTheDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_cars_Price",
                table: "cars");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "cars",
                newName: "price_amount");

            migrationBuilder.AddColumn<Guid>(
                name: "SellerId",
                table: "cars",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "price_currency",
                table: "cars",
                type: "character varying(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "car_photos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CarId = table.Column<Guid>(type: "uuid", nullable: false),
                    Url = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    IsPrimary = table.Column<bool>(type: "boolean", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_car_photos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_car_photos_cars_CarId",
                        column: x => x.CarId,
                        principalTable: "cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "car_price_history",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CarId = table.Column<Guid>(type: "uuid", nullable: false),
                    price_amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    price_currency = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CarId1 = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_car_price_history", x => x.Id);
                    table.ForeignKey(
                        name: "FK_car_price_history_cars_CarId",
                        column: x => x.CarId,
                        principalTable: "cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_car_price_history_cars_CarId1",
                        column: x => x.CarId1,
                        principalTable: "cars",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_cars_SellerId",
                table: "cars",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_car_photos_CarId",
                table: "car_photos",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_car_price_history_CarId",
                table: "car_price_history",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_car_price_history_CarId1",
                table: "car_price_history",
                column: "CarId1");

            migrationBuilder.CreateIndex(
                name: "IX_car_price_history_ChangedAt",
                table: "car_price_history",
                column: "ChangedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "car_photos");

            migrationBuilder.DropTable(
                name: "car_price_history");

            migrationBuilder.DropIndex(
                name: "IX_cars_SellerId",
                table: "cars");

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "cars");

            migrationBuilder.DropColumn(
                name: "price_currency",
                table: "cars");

            migrationBuilder.RenameColumn(
                name: "price_amount",
                table: "cars",
                newName: "Price");

            migrationBuilder.CreateIndex(
                name: "IX_cars_Price",
                table: "cars",
                column: "Price");
        }
    }
}
