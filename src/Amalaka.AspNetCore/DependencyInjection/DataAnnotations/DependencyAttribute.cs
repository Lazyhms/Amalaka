namespace Microsoft.Extensions.DependencyInjection;

[AttributeUsage(AttributeTargets.Interface, AllowMultiple = false)]
public sealed class DependencyAttribute(ServiceLifetime serviceLifetime) : Attribute
{
    public ServiceLifetime ServiceLifetime { get; set; } = serviceLifetime;
}