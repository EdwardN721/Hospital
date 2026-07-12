using Hospital.Application.DTOs.Requests.Patients;
using Hospital.Application.DTOs.Responses.Patients;

namespace Hospital.Application.Interfaces;

public interface IPatientService
{
    Task<IEnumerable<PatientResponse>> ObtenerTodosAsync();
    Task<PatientResponse> ObtenerPorIdAsync(Guid idPaciente);
    Task<PatientResponse> CreatePatientAsync(CreatePatientRequest request);
    Task ActualizarPacienteAsync(Guid idPaciente, UpdatePatientRequest request);
    Task EliminarPacienteAsync(Guid idPaciente);
} 