using Autofac;
using Autofac.Core;
using ConsoleApp1.BC;
using ConsoleApp1.DB;
using ConsoleApp1.Src;
using RecExporter;
using RecExporter.Code.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal sealed class Worker1Factory : ISimpleWorker
    {
        internal static readonly Worker1Factory Instance = new Worker1Factory();
        static BlockChain OpenLedger = new BlockChain();

        public void Do()
        {            
            //OpenLedger.Add(new Block("genesis", new BlockData[] { new BlockData() { message = "asd" } }));
            //Console.ReadKey();
            //return;
            var container = BootStrap.Components();
            using (var scope = container.BeginLifetimeScope())
            {
                //bindowanie
                ISimpleWorker worker = scope.Resolve<ISimpleWorker>();
                //container.Resolve<IServerLogger>();
                // robienie tasków
                Task task1 = new Task(() => worker.Do());
                task1.Start();
                task1.Wait();
            }
        }        
    }
}
