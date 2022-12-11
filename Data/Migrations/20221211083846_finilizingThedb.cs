using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRentingSystem.Data.Migrations
{
    public partial class finilizingThedb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2862455e-92c3-4d49-aa2c-f7e4cd44b9b1", "AQAAAAEAACcQAAAAEKX7NbK5BRuvja8xNSUqenXfDuUyxKzAqmXQKMMRg2MO90xAXsx7Ky2CZM0xjRc4JA==", "baf69e96-0afd-45e1-9523-d23762395241" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "85515f66-e59d-4b71-94ed-0a2993a25c6b", "AQAAAAEAACcQAAAAELDxd32+nFC/dn4Wg9we1Fl8exsuasYdl+5RODGa/7Y4u9Pyx1rusm+ucJlT+QHtWw==", "a1775082-8f75-4afa-bed1-13f7ed7de1d9" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "90dbc5a3-229f-4efe-b4f4-f4353c9b1c96", "AQAAAAEAACcQAAAAEBllzQvnvPwK/+1nGx55TTcEJ68n9SQSjOODN/FjEU2ej09r8Ni2c1frS3y6z51EYA==", "89006c0a-a7f6-4752-91d0-67b478bed9f5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "73546220-ae9b-473a-ba32-a94494073741", "AQAAAAEAACcQAAAAEF+hWcQxNDrchtkjP4fYGvGFTfi4NifXfZilNYB6qmP3ekAjLZl4pxUvUBKWEqtQ4A==", "4d9fabd0-bb2f-4f5d-a118-ba79479a378a" });
        }
    }
}
