using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GlacialBytes.ShortPathService.Persistence.Database.SqlServer.Migrations
{
    public partial class GetDataSchemeVersionAdded : Migration
    {
    /// <inheritdoc/>
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      const string dropProcedureCommandTemplate = @"EXEC sp_executesql N'
        IF EXISTS (SELECT * FROM sys.objects WHERE [name] = N''GetDataSchemeVersion'' AND [type] = N''P'')
        BEGIN
              DROP PROCEDURE dbo.GetDataSchemeVersion;
        END'";
      const string createProcedureCommandTemplate = @"EXEC sp_executesql N'
        CREATE PROCEDURE [dbo].[GetDataSchemeVersion]
        (@dbversion varchar(50) OUTPUT)
        AS
        BEGIN
            SET NOCOUNT ON;

	        IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES  WHERE TABLE_SCHEMA = ''dbo'' AND  TABLE_NAME = ''__EFMigrationsHistory''))
		        select @dbversion = CONCAT(''1.0.'', CONVERT(varchar(10), (select count(*) from dbo.__EFMigrationsHistory)))
	        else
		        select @dbversion = ''1.0.0''
        END'";

      migrationBuilder.Sql(dropProcedureCommandTemplate);
      migrationBuilder.Sql(createProcedureCommandTemplate);
    }

    /// <inheritdoc/>
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      const string dropProcedureCommandTemplate = "DROP PROCEDURE dbo.GetDataSchemeVersion";
      migrationBuilder.Sql(dropProcedureCommandTemplate);
    }
  }
}
