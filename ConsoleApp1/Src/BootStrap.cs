using Autofac;
using RecExporter.Code;

namespace RecExporter
{
    public class BootStrap
    {
        public static IContainer Components()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new MainModule());
            return builder.Build();
        }
    }
}
