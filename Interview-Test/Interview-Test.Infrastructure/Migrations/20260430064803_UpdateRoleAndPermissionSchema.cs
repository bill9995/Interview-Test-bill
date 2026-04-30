using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Interview_Test.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRoleAndPermissionSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PermissionTb_RoleTb_RoleId",
                table: "PermissionTb");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoleMappingTb_RoleTb_RoleId",
                table: "UserRoleMappingTb");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleTb",
                table: "RoleTb");

            migrationBuilder.DropIndex(
                name: "IX_PermissionTb_RoleId",
                table: "PermissionTb");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "PermissionTb");

            migrationBuilder.DropIndex(
                name: "IX_UserRoleMappingTb_RoleId",
                table: "UserRoleMappingTb");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "UserRoleMappingTb");

            migrationBuilder.AddColumn<Guid>(
                name: "RoleId",
                table: "UserRoleMappingTb",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "RoleTb");

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "RoleTb",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "RoleTb",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleTb",
                table: "RoleTb",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "RolePermissionMappingTb",
                columns: table => new
                {
                    RolePermissionMappingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermissionId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissionMappingTb", x => x.RolePermissionMappingId);
                    table.ForeignKey(
                        name: "FK_RolePermissionMappingTb_PermissionTb_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "PermissionTb",
                        principalColumn: "PermissionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermissionMappingTb_RoleTb_RoleId",
                        column: x => x.RoleId,
                        principalTable: "RoleTb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissionMappingTb_PermissionId",
                table: "RolePermissionMappingTb",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissionMappingTb_RoleId",
                table: "RolePermissionMappingTb",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleMappingTb_RoleId",
                table: "UserRoleMappingTb",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoleMappingTb_RoleTb_RoleId",
                table: "UserRoleMappingTb",
                column: "RoleId",
                principalTable: "RoleTb",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRoleMappingTb_RoleTb_RoleId",
                table: "UserRoleMappingTb");

            migrationBuilder.DropTable(
                name: "RolePermissionMappingTb");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleTb",
                table: "RoleTb");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "RoleTb");

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "UserRoleMappingTb",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "RoleTb",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "PermissionTb",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleTb",
                table: "RoleTb",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionTb_RoleId",
                table: "PermissionTb",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionTb_RoleTb_RoleId",
                table: "PermissionTb",
                column: "RoleId",
                principalTable: "RoleTb",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoleMappingTb_RoleTb_RoleId",
                table: "UserRoleMappingTb",
                column: "RoleId",
                principalTable: "RoleTb",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
