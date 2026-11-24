using System.Reflection;
using LogiFast.Domain.Entities;
using Xunit;

namespace LogiFast.Domain.Tests;

public class ArchitectureTests
{
    [Fact]
    public void Domain_ShouldNotReference_OuterLayers()
    {
        Assembly domainAssembly = typeof(Delivery).Assembly;

        var forbiddenPrefixes = new[]
        {
            "LogiFast.Application",
            "LogiFast.Infrastructure",
            "LogiFast.Api"
        };

        var referencedAssemblies = domainAssembly.GetReferencedAssemblies()
            .Select(a => a.Name ?? string.Empty)
            .ToList();

        var violations = referencedAssemblies
            .Where(name => forbiddenPrefixes.Any(prefix =>
                name.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)))
            .ToList();

        Assert.Empty(violations);
    }
}