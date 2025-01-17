﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using VisitorRegistrationSystem.Domain.Entitiy;
using VisitorRegistrationSystem.Domain.Entity;

namespace VisitorRegistrationSystem.Repository.Mappings
{
    public class VisitorMap : IEntityTypeConfiguration<Visitor>
    {
        public void Configure(EntityTypeBuilder<Visitor> builder)
        {
            builder.HasKey(v => v.Id);
            builder.Property(v => v.Id).ValueGeneratedOnAdd();
            builder.Property(v => v.TcNo).IsRequired();
            builder.Property(v => v.TcNo).HasMaxLength(11);
            builder.Property(v => v.ContactNo).IsRequired();
            builder.Property(v => v.ContactNo).HasMaxLength(11);
            builder.Property(v => v.FirstName).IsRequired();
            builder.Property(v => v.FirstName).HasMaxLength(30);
            builder.Property(v => v.LastName).IsRequired();
            builder.Property(v => v.LastName).HasMaxLength(30);
            builder.Property(v => v.BirthDate).IsRequired();

            builder.Property(v => v.IsExit).IsRequired();
            builder.Property(v => v.EnterDate).IsRequired();
            builder.Property(v => v.OutDate).IsRequired();



            builder.Property(v => v.CreatedByName).IsRequired();
            builder.Property(v => v.CreatedByName).HasMaxLength(50);
            builder.Property(v => v.ModifiedByName).IsRequired();
            builder.Property(v => v.ModifiedByName).HasMaxLength(50);
            builder.Property(v => v.ModifiedDate).IsRequired();
            builder.Property(v => v.CreatedDate).IsRequired();
            builder.Property(v => v.IsActive).IsRequired();
            builder.Property(v => v.IsDeleted).IsRequired();

            builder.HasOne<Department>(d => d.Department).WithMany(v => v.Visitors).HasForeignKey(d => d.DepartmentId);
            builder.ToTable("Visitors");
            builder.HasData(new Visitor()
            {
                Id = 1,
                TcNo = "55555555555",
                FirstName = "Kamer",
                LastName = "Kaya",
                BirthDate = new DateTime(1992, 01, 25),
                DepartmentId = 1,

                IsExit = false,
                EnterDate = DateTime.Now,
                OutDate = new DateTime(1999, 10, 30),
                IsActive = true,
                IsDeleted = false,
                CreatedByName = "IntinialCreated",
                CreatedDate = DateTime.Now,
                ModifiedByName = "IntinialCreated",
                ModifiedDate = DateTime.Now,
                ContactNo = "11144445555"

            },
            new Visitor()
            {
                Id = 2,
                TcNo = "11155555551",
                FirstName = "Hakkı",
                LastName = "Kurumahmut",
                BirthDate = new DateTime(1999, 10, 30),
                DepartmentId = 2,

                IsExit = false,
                EnterDate = DateTime.Now,
                OutDate = new DateTime(1999, 10, 30),
                IsActive = true,
                IsDeleted = false,
                CreatedByName = "IntinialCreated",
                CreatedDate = DateTime.Now,
                ModifiedByName = "IntinialCreated",
                ModifiedDate = DateTime.Now,
                ContactNo = "22244445555"



            },
            new Visitor()
            {
                Id = 3,
                TcNo = "22225555555",
                FirstName = "Furkan",
                LastName = "Bakal",
                BirthDate = new DateTime(1999, 10, 30),
                DepartmentId = 3,

                IsExit = false,
                EnterDate = DateTime.Now,
                OutDate = new DateTime(1999, 10, 30),
                IsActive = true,
                IsDeleted = false,
                CreatedByName = "IntinialCreated",
                CreatedDate = DateTime.Now,
                ModifiedByName = "IntinialCreated",
                ModifiedDate = DateTime.Now,
                ContactNo = "33344445555"



            });
        }
    }
}
