using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Repository.Migrations
{
    /// <inheritdoc />
    public partial class initialregisters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ManagerId",
                table: "Employee",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Employee",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.Sql(@"
insert into public.""PositionEmployee""(
    ""Id"",
    ""Name""
)
values(
    'c3d4c221-c289-4100-8f8b-23e3ca578328',
    'Director');

insert into public.""PositionEmployee""(
    ""Id"",
    ""Name""
)
values(
    '8aa13f02-fedc-4bdc-8a61-6bd583086332',
    'Leader');    

insert into public.""PositionEmployee""(
    ""Id"",
    ""Name""
)
values(
    '1f563b65-4ee8-4595-aef8-9f00a50a2899',
    'Employee');        


INSERT INTO public.""Employee"" (
    ""Id"",
    ""FirstName"",
    ""LastName"",
    ""Email"",
    ""DocNumer"",
    ""Password"",
    ""DateOfBirth"",
    ""NewPasswordRequired"",
    ""PositionEmployeeId"",
    ""Phone"",
    ""ManagerId""
) VALUES (
    '0dc478ff-dc5d-4e6a-9b9e-fbb96a359fc8', -- Replace with a valid UUID
    'admin',
    'admin',
    'admin@example.com',
    '000000000', -- Example document number
    '123456789', -- VERY IMPORTANT: Hash the password!
    '1990-05-15 00:00:00+00', -- Example date of birth (YYYY-MM-DD HH:MM:SS+TZ)
    false,
    'c3d4c221-c289-4100-8f8b-23e3ca578328', -- Replace with a valid UUID from PositionEmployee table
    '00-000000000',
    '0dc478ff-dc5d-4e6a-9b9e-fbb96a359fc8'
);
");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Employee");

            migrationBuilder.Sql(@"
        DELETE FROM public.""Employee""
        DELETE FROM public.""PositionEmployee""
");

        }
    }
}
