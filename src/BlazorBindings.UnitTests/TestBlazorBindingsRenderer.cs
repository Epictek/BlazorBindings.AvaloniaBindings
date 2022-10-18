using BlazorBindings.Maui;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BlazorBindings.UnitTests
{
    public class TestBlazorBindingsRenderer : MauiBlazorBindingsRenderer
    {
        public TestBlazorBindingsRenderer(IServiceProvider serviceProvider, ILoggerFactory loggerFactory) : base(serviceProvider, loggerFactory)
        {
        }

        public override Dispatcher Dispatcher => NullDispatcher.Instance;

        public static TestBlazorBindingsRenderer Create()
        {
            var provider = new ServiceCollection().AddLogging().BuildServiceProvider();
            var loggingFactory = provider.GetRequiredService<ILoggerFactory>();
            return new TestBlazorBindingsRenderer(provider, loggingFactory);
        }

        sealed class NullDispatcher : Dispatcher
        {
            public static readonly Dispatcher Instance = new NullDispatcher();

            private NullDispatcher()
            {
            }

            public override bool CheckAccess() => true;

            public override Task InvokeAsync(Action workItem)
            {
                if (workItem is null)
                {
                    throw new ArgumentNullException(nameof(workItem));
                }

                workItem();
                return Task.CompletedTask;
            }

            public override Task InvokeAsync(Func<Task> workItem)
            {
                if (workItem is null)
                {
                    throw new ArgumentNullException(nameof(workItem));
                }

                return workItem();
            }

            public override Task<TResult> InvokeAsync<TResult>(Func<TResult> workItem)
            {
                if (workItem is null)
                {
                    throw new ArgumentNullException(nameof(workItem));
                }

                return Task.FromResult(workItem());
            }

            public override Task<TResult> InvokeAsync<TResult>(Func<Task<TResult>> workItem)
            {
                if (workItem is null)
                {
                    throw new ArgumentNullException(nameof(workItem));
                }

                return workItem();
            }
        }
    }
}