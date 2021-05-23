using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingApp.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CustomerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Telephone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "ObjectForRent",
                columns: table => new
                {
                    ObjectForRentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Photo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObjectForRent", x => x.ObjectForRentId);
                });

            migrationBuilder.CreateTable(
                name: "PricePerPeople",
                columns: table => new
                {
                    PricePerPeopleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ObjectForRentId = table.Column<int>(nullable: true),
                    People = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PricePerPeople", x => x.PricePerPeopleId);
                    table.ForeignKey(
                        name: "FK_PricePerPeople_ObjectForRent_ObjectForRentId",
                        column: x => x.ObjectForRentId,
                        principalTable: "ObjectForRent",
                        principalColumn: "ObjectForRentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservation",
                columns: table => new
                {
                    ReservationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CheckIn = table.Column<DateTime>(nullable: false),
                    CheckOut = table.Column<DateTime>(nullable: false),
                    People = table.Column<int>(nullable: false),
                    ReservationDepositPayment = table.Column<bool>(nullable: false),
                    ReservationPricePayment = table.Column<bool>(nullable: false),
                    ReservationDeposit = table.Column<int>(nullable: false),
                    ReservationPrice = table.Column<int>(nullable: false),
                    CustomerId = table.Column<int>(nullable: true),
                    ObjectForRentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservation", x => x.ReservationId);
                    table.ForeignKey(
                        name: "FK_Reservation_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservation_ObjectForRent_ObjectForRentId",
                        column: x => x.ObjectForRentId,
                        principalTable: "ObjectForRent",
                        principalColumn: "ObjectForRentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PricePerPeople_ObjectForRentId",
                table: "PricePerPeople",
                column: "ObjectForRentId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_CustomerId",
                table: "Reservation",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_ObjectForRentId",
                table: "Reservation",
                column: "ObjectForRentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PricePerPeople");

            migrationBuilder.DropTable(
                name: "Reservation");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "ObjectForRent");
        }
    }
}
