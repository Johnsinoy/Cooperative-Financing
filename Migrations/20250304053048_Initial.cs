using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CooperativeFinancing.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CooperativeLoans",
                columns: table => new
                {
                    Loan_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Member_Id = table.Column<int>(type: "int", nullable: false),
                    Loan_Amount = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Purpose_Loan = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Annual_Interest = table.Column<float>(type: "float", nullable: false),
                    Term = table.Column<int>(type: "int", nullable: false),
                    Release_Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    First_Month = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    End_Month = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Monthly_Payment = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Total_Payment = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CooperativeLoans", x => x.Loan_Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CooperativeMembers",
                columns: table => new
                {
                    Member_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Street = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    City = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Province = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Phone = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    JoinDate = table.Column<DateTime>(type: "date", nullable: false),
                    Contribution = table.Column<int>(type: "int", maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CooperativeMembers", x => x.Member_Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CooperativePayment",
                columns: table => new
                {
                    Payment_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Loan_Id = table.Column<int>(type: "int", nullable: false),
                    Member_Id = table.Column<int>(type: "int", nullable: false),
                    Payment_Date = table.Column<DateTime>(type: "date", nullable: false),
                    Payment_Amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CooperativePayment", x => x.Payment_Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CooperativeUsers",
                columns: table => new
                {
                    User_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Member_Id = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Is_Admin = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CooperativeUsers", x => x.User_Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "CooperativeLoans",
                columns: new[] { "Loan_Id", "Annual_Interest", "End_Month", "First_Month", "Loan_Amount", "Member_Id", "Monthly_Payment", "Purpose_Loan", "Release_Date", "Status", "Term", "Total_Payment" },
                values: new object[,]
                {
                    { 1, 5.5f, new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5000.00m, 1, 230.00m, "Business Expansion", new DateTime(2023, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Active", 24, 5520.00m },
                    { 2, 6.2f, new DateTime(2026, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 7000.00m, 2, 215.00m, "Home Renovation", new DateTime(2023, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Active", 36, 7740.00m }
                });

            migrationBuilder.InsertData(
                table: "CooperativeMembers",
                columns: new[] { "Member_Id", "City", "Contribution", "Email", "FirstName", "JoinDate", "LastName", "Phone", "Province", "Street" },
                values: new object[,]
                {
                    { 1, "Springfield", 600, "john.doe@example.com", "John", new DateTime(2023, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Doe", "123-456-7890", "Illinois", "123 Elm St" },
                    { 2, "Los Angeles", 500, "jane.smith@example.com", "Jane", new DateTime(2023, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Smith", "987-654-3210", "California", "456 Oak Ave" }
                });

            migrationBuilder.InsertData(
                table: "CooperativePayment",
                columns: new[] { "Payment_Id", "Loan_Id", "Member_Id", "Payment_Amount", "Payment_Date" },
                values: new object[,]
                {
                    { 1, 1, 1, 500.00m, new DateTime(2023, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 1, 1, 500.00m, new DateTime(2023, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 2, 2, 700.00m, new DateTime(2023, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CooperativeLoans");

            migrationBuilder.DropTable(
                name: "CooperativeMembers");

            migrationBuilder.DropTable(
                name: "CooperativePayment");

            migrationBuilder.DropTable(
                name: "CooperativeUsers");
        }
    }
}
