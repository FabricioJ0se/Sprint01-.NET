using Xunit;
namespace PortariaLight.Tests.Integration.Fixtures;

[CollectionDefinition("PortariaLight Integration Tests")]
public class PortariaLightCollectionFixture
    : ICollectionFixture<PortariaLightWebApplicationFactory>
{

}