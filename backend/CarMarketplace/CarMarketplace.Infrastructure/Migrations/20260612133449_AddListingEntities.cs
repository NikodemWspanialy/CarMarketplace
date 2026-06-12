using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarMarketplace.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddListingEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "contact_reveals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ListingId = table.Column<Guid>(type: "uuid", nullable: false),
                    ViewerId = table.Column<Guid>(type: "uuid", nullable: false),
                    ContactId = table.Column<Guid>(type: "uuid", nullable: false),
                    RevealedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contact_reveals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "contacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SellerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    phone_number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    country_code = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: true),
                    email_address = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    username = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Label = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "listing_views",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ListingId = table.Column<Guid>(type: "uuid", nullable: false),
                    ViewerId = table.Column<Guid>(type: "uuid", nullable: true),
                    ViewedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IpAddress = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_listing_views", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "listings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CarId = table.Column<Guid>(type: "uuid", nullable: false),
                    SellerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    IsFeatured = table.Column<bool>(type: "boolean", nullable: false),
                    FeaturedUntil = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ExpiresAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    ContactIds = table.Column<List<Guid>>(type: "uuid[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_listings", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_contact_reveals_ListingId_ViewerId",
                table: "contact_reveals",
                columns: new[] { "ListingId", "ViewerId" });

            migrationBuilder.CreateIndex(
                name: "IX_contacts_SellerId",
                table: "contacts",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_listing_views_ListingId_ViewerId_ViewedAt",
                table: "listing_views",
                columns: new[] { "ListingId", "ViewerId", "ViewedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_listings_CarId",
                table: "listings",
                column: "CarId",
                unique: true,
                filter: "\"Status\" = 1");

            migrationBuilder.CreateIndex(
                name: "IX_listings_CreatedAt",
                table: "listings",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_listings_SellerId",
                table: "listings",
                column: "SellerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "contact_reveals");

            migrationBuilder.DropTable(
                name: "contacts");

            migrationBuilder.DropTable(
                name: "listing_views");

            migrationBuilder.DropTable(
                name: "listings");
        }
    }
}
