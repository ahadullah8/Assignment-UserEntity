using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment_UserEntity.Migrations
{
    /// <inheritdoc />
    public partial class addedDefaultUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "7fc30f3f-8d40-4a82-a946-e171a373aa76", 0, "cbddf737-ee1b-47e1-99b0-a38d137213cb", "admin@gmail.com", false, "adminUser", false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEH9RlPPmwRwIk3n7qh9QlKuuLFho8eMueN1GHAR61ioSLH9KuUfJVlCpyDf1LONnrw==", null, false, "5bb39d12-1243-408a-b7e0-c1b705465c17", false, "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7fc30f3f-8d40-4a82-a946-e171a373aa76");
        }
    }
}
