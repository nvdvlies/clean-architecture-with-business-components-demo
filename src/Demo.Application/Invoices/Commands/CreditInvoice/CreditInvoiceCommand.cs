using MediatR;
using System;

namespace Demo.Application.Invoices.Commands.CreditInvoice
{
    public class CreditInvoiceCommand : IRequest<CreditInvoiceResponse>
    {
        internal Guid Id { get; set; }

        public void SetInvoiceId(Guid id)
        {
            Id = id;
        }
    }
}