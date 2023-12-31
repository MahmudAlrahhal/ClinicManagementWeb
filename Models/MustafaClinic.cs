using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
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

    public int ExecuteAppointmentUpdateStoredProcedure(int appointment_id, DateOnly? appointment_date,
        TimeOnly? appointment_time, string? totalPrice, string? paidAmount, string? remainingAmount,
        string? patient_id, int? doctor_id, string? Note, bool IsPreserved)
    {
        var parameter1Param = new SqlParameter("@Parameter1", appointment_id);
        var parameter2Param = new SqlParameter("@Parameter2", appointment_date?.ToString("yyyy-MM-dd"));
        var parameter3Param = new SqlParameter("@Parameter3", appointment_time?.ToString("HH:mm:ss"));
        var parameter4Param = new SqlParameter("@Parameter4", (object)totalPrice ?? DBNull.Value);
        var parameter5Param = new SqlParameter("@Parameter5", (object)paidAmount ?? DBNull.Value);
        var parameter6Param = new SqlParameter("@Parameter6", remainingAmount);
        var parameter7Param = new SqlParameter("@Parameter7", patient_id);
        var parameter8Param = new SqlParameter("@Parameter8", (object)doctor_id ?? DBNull.Value);
        var parameter9Param = new SqlParameter("@Parameter9", (object)Note ?? DBNull.Value);
        var parameter10Param = new SqlParameter("@Parameter10", IsPreserved);

        // Execute stored procedure using ExecuteSqlRaw
        return Database.ExecuteSqlRaw("EXEC [dbo].[UpdateAppointment]" +
            " @Parameter1, @Parameter2, @Parameter3, @Parameter4, @Parameter5, @Parameter6," +
            " @Parameter7, @Parameter8, @Parameter9, @Parameter10",
            parameter1Param, parameter2Param, parameter3Param, parameter4Param,
            parameter5Param, parameter6Param, parameter7Param, parameter8Param,
            parameter9Param, parameter10Param);
    }

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
