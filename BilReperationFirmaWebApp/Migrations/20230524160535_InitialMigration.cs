using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BilReperationFirmaWebApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phonenumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SignUpDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mechanics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HiringDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Salary = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mechanics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Types",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsFinished = table.Column<bool>(type: "bit", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    MechanicId = table.Column<int>(type: "int", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_Mechanics_MechanicId",
                        column: x => x.MechanicId,
                        principalTable: "Mechanics",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderTypes",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderTypes", x => new { x.OrderId, x.TypeId });
                    table.ForeignKey(
                        name: "FK_OrderTypes_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderTypes_Types_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Name", "Phonenumber", "SignUpDate" },
                values: new object[,]
                {
                    { -3, "Johan Jakobsen", "+45 10 09 08 07", new DateTime(2023, 5, 24, 18, 5, 34, 955, DateTimeKind.Local).AddTicks(8812) },
                    { -2, "Linse Testperson", "+45 87 65 43 21", new DateTime(2023, 5, 24, 18, 5, 34, 955, DateTimeKind.Local).AddTicks(8808) },
                    { -1, "John Doe", "+45 12 34 56 78", new DateTime(2023, 5, 24, 18, 5, 34, 955, DateTimeKind.Local).AddTicks(8757) }
                });

            migrationBuilder.InsertData(
                table: "Mechanics",
                columns: new[] { "Id", "HiringDate", "Name", "Salary" },
                values: new object[,]
                {
                    { -3, new DateTime(2023, 5, 24, 18, 5, 34, 955, DateTimeKind.Local).AddTicks(9124), "Mechanic #3", 1249.99 },
                    { -2, new DateTime(2023, 5, 24, 18, 5, 34, 955, DateTimeKind.Local).AddTicks(9121), "Mechanic #2", 2500.5 },
                    { -1, new DateTime(2023, 5, 24, 18, 5, 34, 955, DateTimeKind.Local).AddTicks(9107), "Mechanic #1", 2000.5 }
                });

            migrationBuilder.InsertData(
                table: "Types",
                columns: new[] { "Id", "Description" },
                values: new object[,]
                {
                    { -4, "Battery replacement" },
                    { -3, "Tire Change" },
                    { -2, "Oil-check" },
                    { -1, "Inspection" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CustomerId", "IsFinished", "MechanicId", "Price" },
                values: new object[,]
                {
                    { -5, -2, false, -3, 300.0 },
                    { -4, -3, true, -2, 250.0 },
                    { -3, -2, true, -2, 200.0 },
                    { -2, -1, false, -2, 150.0 },
                    { -1, -1, false, -1, 100.0 }
                });

            migrationBuilder.InsertData(
                table: "OrderTypes",
                columns: new[] { "OrderId", "TypeId" },
                values: new object[,]
                {
                    { -5, -2 },
                    { -4, -4 },
                    { -3, -4 },
                    { -3, -3 },
                    { -2, -2 },
                    { -2, -1 },
                    { -1, -1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_MechanicId",
                table: "Orders",
                column: "MechanicId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderTypes_TypeId",
                table: "OrderTypes",
                column: "TypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderTypes");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Types");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Mechanics");
        }
    }
}
