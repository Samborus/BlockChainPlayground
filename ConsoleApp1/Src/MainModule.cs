using Autofac;
using ConsoleApp1;
using ConsoleApp1.Src;
using RecExporter.Code.Classes;
using RecExporter.Code.Interfaces;

namespace RecExporter.Code
{
    public class MainModule : Module
    {
        public bool UseMock;

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<DBConnector>().As<ISimpleWorker>();
            builder.RegisterType<ServerLogger>().As<IServerLogger>();
        }
    }
}
