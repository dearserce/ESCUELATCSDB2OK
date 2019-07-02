namespace EscuelaTCSDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RolesUsuarios : DbMigration
    {
        public override void Up()
        {
            //Roles
            Sql(@"insert into AspNetUsers (
  [Id]
      ,[Email]
      ,[EmailConfirmed]
      ,[PasswordHash]
      ,[SecurityStamp]
      ,[PhoneNumber]
      ,[PhoneNumberConfirmed]
      ,[TwoFactorEnabled]
      ,[LockoutEndDateUtc]
      ,[LockoutEnabled]
      ,[AccessFailedCount]
      ,[UserName]
  ) values ('13d69202-b361-4c29-9c1f-470b39516169','profesor@1.com',0,'ABYK96k6KknKzDNYfFfGjpV7Pdkh2efWHoqoAwi1Pumburn3Xs7G69oBuHZMCRwJRg==','251c915b-894f-4225-8f22-df21bfda82da','',0,0,null,1,0,'profesor@1.com')
insert into AspNetUsers (
  [Id]
      ,[Email]
      ,[EmailConfirmed]
      ,[PasswordHash]
      ,[SecurityStamp]
      ,[PhoneNumber]
      ,[PhoneNumberConfirmed]
      ,[TwoFactorEnabled]
      ,[LockoutEndDateUtc]
      ,[LockoutEnabled]
      ,[AccessFailedCount]
      ,[UserName]
  ) values ('1c239f7a-e1d1-433d-af8f-5fc1ae4f5fa3','alumno@1.com',0,'AHzFdXtP2UJg+YQsSyGM+IVQb24uLq+AI0NZYnnfzOXK9GwyV6Jy6R0jPILsUjumhg==','9de33a9c-deac-43ee-9890-c365874bd452','',0,0,null,1,0,'alumno@1.com')
insert into AspNetUsers (
  [Id]
      ,[Email]
      ,[EmailConfirmed]
      ,[PasswordHash]
      ,[SecurityStamp]
      ,[PhoneNumber]
      ,[PhoneNumberConfirmed]
      ,[TwoFactorEnabled]
      ,[LockoutEndDateUtc]
      ,[LockoutEnabled]
      ,[AccessFailedCount]
      ,[UserName]
  ) values ('8eec4616-2d21-4670-abac-602d76ea4045','directivo@1.com',0,'ABrx2NR1U2x+90VCMkFi59c+8+0eqA1YPJ0xr/9teH6ovD1XLcAbEwFg3cTxkMXggw==','68cf5936-ba04-43d5-a60d-7da5b17c64c8','',0,0,null,1,0,'directivo@1.com')
");
            //Tipos
            Sql(@"insert into AspNetRoles (
  [Id]
      ,[Name]
  ) values ('d7d0e8cc-fc7e-405f-bbf1-cf827ba466f2','Alumno')

  insert into AspNetRoles (
  [Id]
      ,[Name]
  ) values ('a986e43b-6841-4c95-9c26-eaf59b841bc6','Directivo')

insert into AspNetRoles (
  [Id]
      ,[Name]
  ) values ('e14d085d-7c37-4c4c-956d-e11486a7ecd1','Profesor')
");
            //RolesTipos
            Sql(@"  insert into AspNetUserRoles (
  [UserId]
      ,[RoleId]
  ) values ('8eec4616-2d21-4670-abac-602d76ea4045','a986e43b-6841-4c95-9c26-eaf59b841bc6')

  insert into AspNetUserRoles (
  [UserId]
      ,[RoleId]
  ) values ('1c239f7a-e1d1-433d-af8f-5fc1ae4f5fa3','d7d0e8cc-fc7e-405f-bbf1-cf827ba466f2')

  insert into AspNetUserRoles (
  [UserId]
      ,[RoleId]
  ) values('13d69202-b361-4c29-9c1f-470b39516169','e14d085d-7c37-4c4c-956d-e11486a7ecd1')
");
        }
        
        public override void Down()
        {
        }
    }
}
