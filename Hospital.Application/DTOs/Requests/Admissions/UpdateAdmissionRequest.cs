namespace Hospital.Application.DTOs.Requests.Admissions;

public record UpdateAdmissionRequest(
    DateTime? DischargeDate,
    string Reason
);