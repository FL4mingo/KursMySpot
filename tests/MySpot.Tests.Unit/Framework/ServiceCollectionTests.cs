using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace MySpot.Tests.Unit.Framework;

public class ServiceCollectionTests
{
    [Fact]
    public void test()
    {
        var serviceColletion = new ServiceCollection();

        serviceColletion.AddSingleton<IMessenger,MessengerSingleton>();
        serviceColletion.AddTransient<IMessenger,MessengerTransient>();
        serviceColletion.AddScoped<IMessenger,MessengerScoped>();

        var serviceProvider = serviceColletion.BuildServiceProvider();

        var messengers = serviceProvider.GetServices<IMessenger>();


        using(var scope = serviceProvider.CreateScope())
        {
            var messangers = scope.ServiceProvider.GetServices<IMessenger>();

        }
    }

    private interface IMessenger
    {
        void Send();
    }

    private class MessengerSingleton : IMessenger
    {
        private readonly Guid _id = Guid.NewGuid();

        public void Send() => Console.WriteLine($"Sending a message... [{_id}]");
    }
    private class MessengerTransient : IMessenger
    {
        private readonly Guid _id = Guid.NewGuid();

        public void Send() => Console.WriteLine($"Sending a message... [{_id}]");
    }
    private class MessengerScoped : IMessenger
    {
        private readonly Guid _id = Guid.NewGuid();

        public void Send() => Console.WriteLine($"Sending a message... [{_id}]");
    }
}
