using Autofac;
using Autofac.Core;
using ConsoleApp1.BC;
using ConsoleApp1.DB;
using ConsoleApp1.Src;
using RecExporter;
using RecExporter.Code.Interfaces;
using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public sealed class Worker1Factory : ISimpleWorker
    {
        public static readonly Worker1Factory Instance = new Worker1Factory();
        static BlockChain OpenLedger = new BlockChain();

        public void Do()
        {
            double d = 0x0404cb * 2 * Math.Pow(8 * (0x1b - 3), 2);
            
            for (int i = 0; i < 10; i++)
            {
                Block b1 = new Block(new BlockData[] { new BlockData() { message = "wiadomosc1 nr: " + i.ToString() },
                new BlockData() { message = "wiadomosc2 nr: " + i.ToString() },
                new BlockData() { message = "wiadomosc3 nr: " + i.ToString() },
                new BlockData() { message = "wiadomosc4 nr: " + i.ToString() },
                new BlockData() { message = "wiadomosc5 nr: " + i.ToString() }});
                OpenLedger.Add(b1);
            }
            
            MemoryStream stream1 = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(BlockChain));
            ser.WriteObject(stream1, OpenLedger);
            stream1.Position = 0;
            StreamReader sr = new StreamReader(stream1);
            Console.Write("JSON form of Person object: ");
            Console.WriteLine(sr.ReadToEnd());
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
