using System.Threading.Tasks;
using FastEndpoints;
using Microsoft.Extensions.Logging;
using FluentAssertions;
using MamisSolidarias.Utils.Test;
using MamisSolidarias.WebAPI.TEMPLATE.Utils;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace MamisSolidarias.WebAPI.TEMPLATE.Endpoints.Test;

internal class EndpointTest
{
    private readonly Mock<DbService> _mockDbService = new ();
    private Endpoint _endpoint = null!;
    
    [SetUp]
    public void Setup()
    {
        _endpoint = EndpointFactory
            .CreateEndpoint<Endpoint>(null,_mockDbService.Object)
            .WithEndpointLogger<Request,Response>()
            .Build();
    }
    
    [Test]
    public async Task WithValidParameters_Succeeds()
    {
        //arrange
        var req = new Request
        {
            Name = "lucas"
        };

        //act
        await _endpoint.HandleAsync(req, default);
        var response = _endpoint.Response;

        //assert
        response.Should().NotBeNull();
        _endpoint.ValidationFailed.Should().BeFalse();
        response.Name.Should().Be("Lucassss");
        response.Id.Should().BePositive().And.BeLessThanOrEqualTo(10);
        response.Email.Should().Be("mymail@mail.com");
    }

    [Test]
    public async Task WithNonExistentUser_ThrowsNotFound()
    {
        //arrange
        var req = new Request
        {
            Name = "Not lucas"
        };

        //act
        await _endpoint.HandleAsync(req, default);
        var statusCode = _endpoint.HttpContext.Response.StatusCode;

        //assert
        _endpoint.ValidationFailed.Should().BeFalse();
        statusCode.Should().Be(404);
    }
}