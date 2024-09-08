using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HienTangToc.Migrations
{
    /// <inheritdoc />
    public partial class Hientangtoc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NguoihienModels",
                columns: table => new
                {
                    Idnh = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hoten = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sdt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Diachi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gioitinh = table.Column<int>(type: "int", nullable: false),
                    Ngayhien = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguoihienModels", x => x.Idnh);
                });

            migrationBuilder.CreateTable(
                name: "NguoimuonModels",
                columns: table => new
                {
                    Idnm = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hoten = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sdt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Diachi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gioitinh = table.Column<int>(type: "int", nullable: false),
                    Ngaysinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Nguyennhan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ngaydangky = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ngaytra = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguoimuonModels", x => x.Idnm);
                });

            migrationBuilder.CreateTable(
                name: "SalonModels",
                columns: table => new
                {
                    Idsl = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Diachi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Facebook = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Thoigianhd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Uudai = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalonModels", x => x.Idsl);
                });

            migrationBuilder.CreateTable(
                name: "TocModels",
                columns: table => new
                {
                    Idtoc = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Loaitoc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Soluong = table.Column<int>(type: "int", nullable: false),
                    Dodaitoc = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TocModels", x => x.Idtoc);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HSalonModels",
                columns: table => new
                {
                    HSalonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Idnh = table.Column<int>(type: "int", nullable: false),
                    Idsl = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HSalonModels", x => x.HSalonId);
                    table.ForeignKey(
                        name: "FK_HSalonModels_NguoihienModels_Idnh",
                        column: x => x.Idnh,
                        principalTable: "NguoihienModels",
                        principalColumn: "Idnh",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HSalonModels_SalonModels_Idsl",
                        column: x => x.Idsl,
                        principalTable: "SalonModels",
                        principalColumn: "Idsl",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MSalonModels",
                columns: table => new
                {
                    MSalonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Idnm = table.Column<int>(type: "int", nullable: false),
                    Idsl = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MSalonModels", x => x.MSalonId);
                    table.ForeignKey(
                        name: "FK_MSalonModels_NguoimuonModels_Idnm",
                        column: x => x.Idnm,
                        principalTable: "NguoimuonModels",
                        principalColumn: "Idnm",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MSalonModels_SalonModels_Idsl",
                        column: x => x.Idsl,
                        principalTable: "SalonModels",
                        principalColumn: "Idsl",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HSalonModels_Idnh",
                table: "HSalonModels",
                column: "Idnh");

            migrationBuilder.CreateIndex(
                name: "IX_HSalonModels_Idsl",
                table: "HSalonModels",
                column: "Idsl");

            migrationBuilder.CreateIndex(
                name: "IX_MSalonModels_Idnm",
                table: "MSalonModels",
                column: "Idnm");

            migrationBuilder.CreateIndex(
                name: "IX_MSalonModels_Idsl",
                table: "MSalonModels",
                column: "Idsl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HSalonModels");

            migrationBuilder.DropTable(
                name: "MSalonModels");

            migrationBuilder.DropTable(
                name: "TocModels");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "NguoihienModels");

            migrationBuilder.DropTable(
                name: "NguoimuonModels");

            migrationBuilder.DropTable(
                name: "SalonModels");
        }
    }
}
