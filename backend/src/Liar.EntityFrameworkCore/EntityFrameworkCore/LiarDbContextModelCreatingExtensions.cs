using Liar.Domain.Sys;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace Liar.EntityFrameworkCore
{
    public static class LiarDbContextModelCreatingExtensions
    {
        public static void ConfigureLiar(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));
             
            builder.Entity<SysUser>(x =>
            {
                x.HasKey(x => x.Id);
                x.Property(x => x.Account).IsRequired().HasMaxLength(16);
                x.Property(x => x.Avatar).HasMaxLength(64);
                x.Property(x => x.Email).HasMaxLength(32);
                x.Property(x => x.Name).IsRequired().HasMaxLength(16);
                x.Property(x => x.Password).IsRequired().HasMaxLength(32);
                x.Property(x => x.Phone).HasMaxLength(11);
                x.Property(x => x.RoleIds).HasMaxLength(72);
                x.Property(x => x.Salt).IsRequired().HasMaxLength(6);

                //一对多,SysDept没有UserId字段
                x.HasOne(d => d.Dept)
                   .WithMany(p => p.Users)
                   .HasForeignKey(d => d.DeptId)
                   .OnDelete(DeleteBehavior.SetNull);
            });

            builder.Entity<SysRole>(x =>
            {
                x.HasKey(x => x.Id);
                x.Property(x => x.Name).IsRequired().HasMaxLength(32);
                x.Property(x => x.Tips).HasMaxLength(64);

                //一对多,SysDept没有UserId字段
                x.HasMany(d => d.Relations)
                       .WithOne(p => p.Role)
                       .HasForeignKey(d => d.RoleId)
                       .IsRequired()
                       .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<SysRelation>(x =>
            {
                x.HasKey(x => x.Id);
                x.Property(x => x.RoleId).IsRequired();
                x.Property(x => x.MenuId).IsRequired();
            });

            builder.Entity<SysMenu>(x =>
            {
                x.HasKey(x => x.Id);
                x.Property(x => x.Code).IsRequired().HasMaxLength(16);
                x.Property(x => x.PCode).HasMaxLength(16);
                x.Property(x => x.PCodes).HasMaxLength(128);
                x.Property(x => x.Component).HasMaxLength(64);
                x.Property(x => x.Icon).HasMaxLength(16);
                x.Property(x => x.Name).IsRequired().HasMaxLength(16);
                x.Property(x => x.Tips).HasMaxLength(32);
                x.Property(x => x.Url).HasMaxLength(64);

                x.HasMany(d => d.Relations)
                       .WithOne(m => m.Menu)
                       .HasForeignKey(d => d.MenuId)
                       .IsRequired()
                       .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<SysDept>(x =>
            {
                x.HasKey(x => x.Id);
                x.Property(x => x.FullName).IsRequired().HasMaxLength(32);
                x.Property(x => x.SimpleName).IsRequired().HasMaxLength(16);
                x.Property(x => x.Tips).HasMaxLength(64);
                x.Property(x => x.Pids).HasMaxLength(80);
            });
        }
    }
}