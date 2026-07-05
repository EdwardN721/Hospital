using Hospital.Domain.Models.Core;
using Microsoft.EntityFrameworkCore;
using Hospital.Domain.Models.Billing;
using Hospital.Domain.Models.Clinical;

namespace Hospital.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // Schema - Core
    public DbSet<Person> Personas => Set<Person>();
    public DbSet<Doctor> Doctores => Set<Doctor>();
    public DbSet<Patient> Pacientes => Set<Patient>();
    public DbSet<Specialty> Especialidades => Set<Specialty>();

    // Schema - Clinical
    public DbSet<Admission> Admisiones => Set<Admission>();
    public DbSet<Appointment> Citas => Set<Appointment>();
    public DbSet<Bed> Camas => Set<Bed>();
    public DbSet<Consultation> Consultas => Set<Consultation>();
    public DbSet<Floor> Pisos => Set<Floor>();
    public DbSet<Prescription> Prescripciones => Set<Prescription>();
    public DbSet<Room> Habitaciones => Set<Room>();

    // Schema - Billing
    public DbSet<Invoice> Facturas => Set<Invoice>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Esto escaneará el ensamblado de infraestructura y aplicará todas las clases IEntityTypeConfiguration
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}