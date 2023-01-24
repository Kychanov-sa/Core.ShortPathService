﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GlacialBytes.ShortPathService.Persistence.Database.PostgreSql.Migrations
{
    public partial class InitialDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Routes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    FullUrl = table.Column<string>(type: "text", nullable: false),
                    BestBefore = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Routes");
        }
    }
}
