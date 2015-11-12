using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace ImageLoader.Migrations
{
    public partial class AddSiteField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Site",
                table: "Image",
                nullable: false,
                defaultValue: "static-s.aa-cdn.net");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "Site", table: "Image");
        }
    }
}
