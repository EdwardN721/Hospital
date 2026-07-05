using Hospital.Domain.Interfaces;
using Hospital.Domain.Models.Billing;
using Hospital.Domain.Models.Clinical;
using Hospital.Domain.Models.Core;
using Hospital.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Storage;

namespace Hospital.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IDbContextTransaction? _transaction;

    private IGenericRepository<Doctor>? _doctores;    
    private IGenericRepository<Patient>? _pacientes;
    private IGenericRepository<Person>? _personas;
    private IGenericRepository<Specialty>? _especialedades;
    private IGenericRepository<Admission>? _admisiones;
    private IGenericRepository<Appointment>? _citas;
    private IGenericRepository<Bed>? _camas;
    private IGenericRepository<Consultation>? _consultas;
    private IGenericRepository<Floor>? _pisos;
    private IGenericRepository<Prescription>? _prescripciones;
    private IGenericRepository<Room>? _habitaciones;
    private IGenericRepository<Invoice>? _facturas;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IGenericRepository<Doctor> Doctores
    {
        get { return _doctores ??= new GenericRepository<Doctor>(_context); }
    }

    public IGenericRepository<Patient> Pacientes
    {
        get { return _pacientes ??= new GenericRepository<Patient>(_context); }
    }

    public IGenericRepository<Person> Personas
    {
        get { return _personas ??= new GenericRepository<Person>(_context); }
    }

    public IGenericRepository<Specialty> Especialidades
    {
        get { return _especialedades ??= new GenericRepository<Specialty>(_context); }
    }

    public IGenericRepository<Admission> Admisiones
    {
        get { return _admisiones ??= new GenericRepository<Admission>(_context); }
    }

    public IGenericRepository<Appointment> Citas
    {
        get { return _citas ??= new GenericRepository<Appointment>(_context); }
    }

    public IGenericRepository<Bed> Camas
    {
        get { return _camas ??= new GenericRepository<Bed>(_context); }
    }

    public IGenericRepository<Consultation> Consultas
    {
        get { return _consultas ??= new GenericRepository<Consultation>(_context); }
    }

    public IGenericRepository<Floor> Pisos
    {
        get { return _pisos ??= new GenericRepository<Floor>(_context); }
    }

    public IGenericRepository<Prescription> Prescripciones
    {
        get { return _prescripciones ??= new GenericRepository<Prescription>(_context); }
    }

    public IGenericRepository<Room> Habitaciones
    {
        get { return _habitaciones ??= new GenericRepository<Room>(_context); }
    }

    public IGenericRepository<Invoice> Facturas
    {
        get { return _facturas ??= new GenericRepository<Invoice>(_context); }
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task BeginTransactionAsync()
    {
        if (_transaction != null)
        {
            return;
        }
        _transaction = await _context.Database.BeginTransactionAsync(); 
    }

    public async Task CommitTransactionAsync()
    {
        try
        {
            await _context.SaveChangesAsync();

            if (_transaction != null)
            {
                await _transaction.CommitAsync();
            }
        }
        catch
        {
            await RollbackTransactionAsync();
            throw;
        }
        finally
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }
    }

    public async Task RollbackTransactionAsync()
    {
        try
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
            }
        }
        finally
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;    
            }
        }
    }
    
    public void Dispose()
    {
        if (_transaction != null)
        {
            _transaction.Dispose();
        }
        _context.Dispose();
    }
}