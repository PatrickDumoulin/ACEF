using CoreLib.Injection;
using Microsoft.Extensions.Configuration;
using Ninject.Modules;
using UnitTests;
using UnitTests.Core.Configuration;
using UnitTests.Core.Mappings;

var builder = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false);

IConfiguration config = builder.Build();
ConfigurationHelper.Initialize(config);

List<INinjectModule> list = new List<INinjectModule>();
list.Add(new ProdBinder());
Injector.InitializeKernel(list);

InitialTests tests = new InitialTests();
tests.RunTests();