using MediatR;
using Store.Core.Domain.ErrorHandling;

namespace Store.Payments.Application.Payments.Commands;

public record PaymentVerifyCommand(Guid PaymentId) : IRequest<Result>;