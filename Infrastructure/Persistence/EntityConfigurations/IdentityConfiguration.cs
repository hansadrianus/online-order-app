﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Infrastructure.Persistence.EntityConfigurations
{
    public class IdentityConfiguration :
        IEntityTypeConfiguration<ApplicationUser>,
        IEntityTypeConfiguration<ApplicationRole>,
        IEntityTypeConfiguration<ApplicationUserRole>,
        IEntityTypeConfiguration<ApplicationUserClaim>,
        IEntityTypeConfiguration<ApplicationUserLogin>,
        IEntityTypeConfiguration<ApplicationUserToken>,
        IEntityTypeConfiguration<ApplicationRoleClaim>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("UserLoginInfo");
            builder.HasIndex(q => q.UserName).IsUnique();
            builder.HasIndex(q => q.Email).IsUnique();
            builder.HasMany(q => q.UserRoles).WithOne().HasForeignKey(q => q.UserId).OnDelete(DeleteBehavior.NoAction);
        }

        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            builder.ToTable("Role");
            builder.HasIndex(q => q.Name).IsUnique();
            builder.HasMany(q => q.UserRoles).WithOne().HasForeignKey(q => q.RoleId).OnDelete(DeleteBehavior.NoAction);
        }

        public void Configure(EntityTypeBuilder<ApplicationUserRole> builder)
        {
            builder.ToTable("UserRoles");
        }

        public void Configure(EntityTypeBuilder<ApplicationUserClaim> builder)
        {
            builder.ToTable("UserClaims");
        }

        public void Configure(EntityTypeBuilder<ApplicationUserLogin> builder)
        {
            builder.ToTable("UserLogin");
        }

        public void Configure(EntityTypeBuilder<ApplicationUserToken> builder)
        {
            builder.ToTable("UserToken");
        }

        public void Configure(EntityTypeBuilder<ApplicationRoleClaim> builder)
        {
            builder.ToTable("RoleClaim");
        }
    }
}
