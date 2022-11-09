using BlazorBindings.Maui;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MauiDispatching = Microsoft.Maui.Dispatching;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.UnitTests
{
    class TestApplication : MC.Application
    {
        public TestApplication(IServiceProvider serviceProvider = null)
        {
            serviceProvider ??= TestServiceProvider.Create();
            Handler = new TestHandler
            {
                MauiContext = new MauiContext(serviceProvider),
                VirtualView = this
            };
        }

        class TestHandler : IElementHandler
        {
            public object PlatformView => null;
            public IElement VirtualView { get; set; }
            public IMauiContext MauiContext { get; set; }
            public void DisconnectHandler() { }
            public void Invoke(string command, object args = null) { }
            public void SetMauiContext(IMauiContext mauiContext) => MauiContext = mauiContext;
            public void SetVirtualView(IElement view) => VirtualView = view;
            public void UpdateValue(string property) { }
        }
    }

    public static class TestServiceProvider
    {
        public static IServiceProvider Create()
        {
            var builder = MauiApp.CreateBuilder();
            builder.Services.AddScoped<MauiBlazorBindingsRenderer, TestBlazorBindingsRenderer>();
            builder.Services.AddSingleton<MauiDispatching.IDispatcher, TestDispatcher>();
            return builder.Build().Services;
        }

        class TestDispatcher : MauiDispatching.IDispatcher
        {
            public bool IsDispatchRequired => false;
            public MauiDispatching.IDispatcherTimer CreateTimer() => null;
            public bool Dispatch(Action action)
            {
                action();
                return true;
            }

            public bool DispatchDelayed(TimeSpan delay, Action action)
            {
                Thread.Sleep(delay);
                action();
                return true;
            }
        }
    }

    public class TestBlazorBindingsRenderer : MauiBlazorBindingsRenderer
    {
        public TestBlazorBindingsRenderer(IServiceProvider serviceProvider, ILoggerFactory loggerFactory) : base(serviceProvider, loggerFactory)
        {
        }

        public List<Exception> Exceptions { get; } = new();

        protected override void HandleException(Exception exception)
        {
            Exceptions.Add(exception);
            base.HandleException(exception);
        }

        public override Dispatcher Dispatcher => NullDispatcher.Instance;

        public static MauiBlazorBindingsRenderer Create()
        {
            return TestServiceProvider.Create().GetRequiredService<MauiBlazorBindingsRenderer>();
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
                workItem();
                return Task.CompletedTask;
            }

            public override Task InvokeAsync(Func<Task> workItem)
            {
                return workItem();
            }

            public override Task<TResult> InvokeAsync<TResult>(Func<TResult> workItem)
            {
                return Task.FromResult(workItem());
            }

            public override Task<TResult> InvokeAsync<TResult>(Func<Task<TResult>> workItem)
            {
                return workItem();
            }
        }
    }
}
