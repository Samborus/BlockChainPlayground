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

        public void Do()
        {
            Block[] bs = { new Block("genesis", new BlockData[] { new BlockData() { message = "asd" } }) };
            Console.WriteLine(bs[0].Hash);
            Console.ReadKey();
            return;
            using (var container = BootStrap.Components())
            {
                //bindowanie
                ISimpleWorker worker = container.Resolve<ISimpleWorker>();
                //container.Resolve<IServerLogger>();
                // robienie tasków
                Task task1 = new Task(() => worker.Do());
                task1.Start();
                task1.Wait();
            }
        }        
    }
}
