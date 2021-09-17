﻿using Demo.Application.Customers.Queries.GetCustomerById;
using Demo.Domain.Customer;
using Demo.WebApi.Tests.Controllers.Customers.Helpers;
using Demo.WebApi.Tests.Helpers;
using FluentAssertions;
using System;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace Demo.WebApi.Tests.Controllers.Customers
{
    [Collection(nameof(SharedFixture))]
    public class GetCustomerByIdTests : TestBase
    {
        public GetCustomerByIdTests(SharedFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task GetCustomerById_When_customer_exists_It_should_return_statuscode_OK()
        {
            // Arrange
            var existingCustomer = new Customer { Id = Guid.NewGuid(), Name = "Test" };

            await ResetDatabaseAsync();
            await AddAsExistingEntityAsync(existingCustomer);

            // Act
            var response = await _client.CustomersController().GetById(existingCustomer.Id);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadFromJsonAsync<GetCustomerByIdQueryResult>();
            content.Customer.Should().NotBeNull();
            content.Customer.Id.Should().Be(existingCustomer.Id);
            content.Customer.Name.Should().Be(existingCustomer.Name);
        }

        [Fact]
        public async Task GetCustomerById_When_customer_does_not_exist_It_should_return_statuscode_NotFound()
        {
            // Arrange
            var customerId = Guid.NewGuid();

            // Act
            var response = await _client.CustomersController().GetById(customerId);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
