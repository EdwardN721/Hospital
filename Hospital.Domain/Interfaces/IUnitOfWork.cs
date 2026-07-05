using Hospital.Domain.Models.Billing;
using Hospital.Domain.Models.Clinical;
using Hospital.Domain.Models.Core;

namespace Hospital.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    // Schema - core
    IGenericRepository<Doctor> Doctores { get; }
    IGenericRepository<Patient> Pacientes { get; }
    IGenericRepository<Person> Personas { get; }
    IGenericRepository<Specialty> Especialidades { get; }
    
    // Schema - clincal
    IGenericRepository<Admission> Admisiones { get; }
    IGenericRepository<Appointment> Citas { get; }
    IGenericRepository<Bed> Camas { get; }
    IGenericRepository<Consultation> Consultas { get; }
    IGenericRepository<Floor> Pisos { get; }
    IGenericRepository<Prescription> Prescripciones { get; }
    IGenericRepository<Room> Habitaciones { get; }
    
    // Schema - billing
    IGenericRepository<Invoice> Facturas { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    
    // Gestión de Transacciones especificas
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();

}