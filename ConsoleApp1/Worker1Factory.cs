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
    public sealed class Worker1Factory : ISimpleWorker
    {
        public static readonly Worker1Factory Instance = new Worker1Factory();
        static BlockChain OpenLedger = new BlockChain();

        public void Do()
        {            
            for(int i = 0; i < 50; i++)
            {
                Block b1 = new Block(new BlockData[] { new BlockData() { message = "wiadomosc nr: " + i.ToString() } });
                OpenLedger.Add(b1);
            }
            bool valed = OpenLedger.Validate();
            Console.WriteLine($"ledger is valid : { valed }");
            Console.ReadKey();
            //return;
            var container = BootStrap.Components();
            using (var scope = container.BeginLifetimeScope())
            {
                //bindowanie
                ISimpleWorker worker = scope.Resolve<ISimpleWorker>();
                //container.Resolve<IServerLogger>();
                // robienie tasków
                Task task1 = new Task(() => worker.Do());
                //task1.Start();
                //task1.Wait();
            }
        }        
    }
}
