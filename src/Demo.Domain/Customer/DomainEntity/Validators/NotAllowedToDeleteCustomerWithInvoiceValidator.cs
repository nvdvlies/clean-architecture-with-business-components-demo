﻿using Demo.Domain.Invoice;
using Demo.Domain.Shared.DomainEntity;
using Demo.Domain.Shared.Extensions;
using Demo.Domain.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.Domain.Customer.DomainEntity.Validators
{
    internal class NotAllowedToDeleteCustomerWithInvoiceValidator : IValidator<Customer>
    {
        private readonly IDbQuery<Invoice.Invoice> _invoiceQuery;

        public NotAllowedToDeleteCustomerWithInvoiceValidator(IDbQuery<Invoice.Invoice> invoiceQuery)
        {
            _invoiceQuery = invoiceQuery;
        }

        public async Task<IEnumerable<ValidationMessage>> ValidateAsync(IDomainEntityContext<Customer> context, CancellationToken cancellationToken = default)
        {
            if (context.EditMode != EditMode.Delete)
            {
                return await Validations.Ok();
            }

            var invoiceStatussesWhichDisallowDeletionOfCustomer = new[] { InvoiceStatus.Draft, InvoiceStatus.Sent, InvoiceStatus.Paid };

            var hasInvoices = await _invoiceQuery.AsQueryable()
                .Where(x => x.CustomerId == context.Entity.Id)
                .Where(x => invoiceStatussesWhichDisallowDeletionOfCustomer.Contains(x.Status))
                .AnyAsync();

            if (hasInvoices)
            {
                return await Validations.Invalid("Cannot delete customer, because one or more invoices are linked to this customer.");
            }

            return await Validations.Ok();
        }
    }
}
