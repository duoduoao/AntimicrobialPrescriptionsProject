using AntimicrobialPrescriptions.Domain.Entities;
using AntimicrobialPrescriptions.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AntimicrobialPrescriptions.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Prescription> Prescriptions { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Prescription>(builder =>
            {
                builder.HasKey(p => p.Id);

                builder.Property(p => p.PatientId)
                    .IsRequired()
                    .HasMaxLength(50);

                builder.Property(p => p.AntimicrobialName)
                    .IsRequired()
                    .HasMaxLength(100);

                builder.Property(p => p.Dose)
                    .IsRequired()
                    .HasMaxLength(50);

                builder.Property(p => p.Frequency)
                    .IsRequired()
                    .HasMaxLength(50);

                builder.Property(p => p.Route)
                    .IsRequired()
                    .HasMaxLength(50);

                builder.Property(p => p.Indication)
                    .IsRequired()
                    .HasMaxLength(200);

                builder.Property(p => p.PrescriberName)
                    .IsRequired()
                    .HasMaxLength(100);

                builder.Property(p => p.PrescriberRole)
                    .IsRequired()
                    .HasMaxLength(50);

                builder.Property(p => p.Status)
                    .IsRequired();

                builder.Property(p => p.StartDate)
                    .IsRequired();

                builder.Property(p => p.ExpectedEndDate)
                    .IsRequired();
          


           

                // Seed three initial prescriptions
                builder.HasData(
                    new
                    {
                        Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                        PatientId = "123",
                        AntimicrobialName = "Amoxicillin",
                        Dose = "500mg",
                        Frequency = "3x/day",
                        Route = "oral",
                        Indication = "Pneumonia",
                        StartDate = new DateTime(2025, 9, 1),
                        ExpectedEndDate = new DateTime(2025, 9, 10),
                        PrescriberName = "Dr. Smith",
                        PrescriberRole = "Clinician",
                        Status = PrescriptionStatus.Active
                    },
                    new
                    {
                        Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                        PatientId = "456",
                        AntimicrobialName = "Ceftriaxone",
                        Dose = "1g",
                        Frequency = "2x/day",
                        Route = "IV",
                        Indication = "UTI",
                        StartDate = new DateTime(2025, 8, 28),
                        ExpectedEndDate = new DateTime(2025, 9, 3),
                        PrescriberName = "Dr. Jones",
                        PrescriberRole = "Clinician",
                        Status = PrescriptionStatus.Active
                    },
                    new
                    {
                        Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                        PatientId = "789",
                        AntimicrobialName = "Vancomycin",
                        Dose = "500mg",
                        Frequency = "2x/day",
                        Route = "IV",
                        Indication = "Sepsis",
                        StartDate = new DateTime(2025, 9, 5),
                        ExpectedEndDate = new DateTime(2025, 9, 15),
                        PrescriberName = "Dr. Lee",
                        PrescriberRole = "InfectionControl",
                        Status = PrescriptionStatus.Active
                    }
                );
            });
        }
    }
}
