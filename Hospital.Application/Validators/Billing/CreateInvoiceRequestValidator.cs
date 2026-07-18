using FluentValidation;
using Hospital.Application.DTOs.Requests.Billing;

namespace Hospital.Application.Validators.Billing;

public class CreateInvoiceRequestValidator : AbstractValidator<CreateInvoiceRequest>
{
    public CreateInvoiceRequestValidator()
    {
        RuleFor(x => x.PatientId).NotEmpty();
        
        RuleFor(x => x.TotalCost)
            .GreaterThanOrEqualTo(0).WithMessage("El costo total no puede ser negativo.");
            
        RuleFor(x => x.Discount)
            .GreaterThanOrEqualTo(0).WithMessage("El descuento no puede ser negativo.")
            .LessThanOrEqualTo(x => x.TotalCost).WithMessage("El descuento no puede ser mayor al costo total.");
    }
}