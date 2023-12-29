using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementWeb.Models;

public partial class MustafaClinic : DbContext
{
    public MustafaClinic()
    {
    }

    public MustafaClinic(DbContextOptions<MustafaClinic> options)
        : base(options)
    {
    }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-8S8D5IM\\MSSQLSERVER01;database=MustafaClinic;Integrated Security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.AppointmentId).HasName("PK__Appointm__A50828FC26DB709D");

            entity.ToTable("Appointment");

            entity.Property(e => e.AppointmentId).HasColumnName("appointment_id");
            entity.Property(e => e.AppointmentDate).HasColumnName("appointment_date");
            entity.Property(e => e.AppointmentTime).HasColumnName("appointment_time");
            entity.Property(e => e.DoctorId).HasColumnName("doctor_id");
            entity.Property(e => e.PaidAmount)
                .HasMaxLength(255)
                .HasColumnName("paidAmount");
            entity.Property(e => e.PatientId)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("patient_id");
            entity.Property(e => e.RemainingAmount)
                .HasMaxLength(255)
                .HasColumnName("remainingAmount");
            entity.Property(e => e.TotalPrice)
                .HasMaxLength(255)
                .HasColumnName("totalPrice");

            entity.HasOne(d => d.Doctor).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.DoctorId)
                .HasConstraintName("FK__Appointme__docto__6E01572D");

            entity.HasOne(d => d.Patient).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK__Appointme__patie__6D0D32F4");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.DoctorId).HasName("PK__Doctor__F3993564D9B1FE8C");

            entity.ToTable("Doctor");

            entity.Property(e => e.DoctorId).HasColumnName("doctor_id");
            entity.Property(e => e.ContactNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("contact_number");
            entity.Property(e => e.DoctorName)
                .HasMaxLength(255)
                .HasColumnName("doctor_name");
            entity.Property(e => e.DoctorSurname)
                .HasMaxLength(255)
                .HasColumnName("doctor_surname");
            entity.Property(e => e.Specialization)
                .HasMaxLength(255)
                .HasColumnName("specialization");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.PhoneNumber).HasName("PK__Patient__A1936A6AB0034A2D");

            entity.ToTable("Patient");

            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("phone_number");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.Note).HasColumnName("note");
            entity.Property(e => e.PatientName)
                .HasMaxLength(255)
                .HasColumnName("patient_name");
            entity.Property(e => e.PatientSurname)
                .HasMaxLength(255)
                .HasColumnName("patient_surname");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
