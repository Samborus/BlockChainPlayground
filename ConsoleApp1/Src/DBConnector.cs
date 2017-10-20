using System;
using System.Collections.Generic;
using ConsoleApp1.DB;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecExporter.Code.Interfaces;

namespace ConsoleApp1.Src   
{
    public sealed class DBConnector : Disposing, ISimpleWorker
    {
        private IServerLogger Logger;

        public DBConnector(IServerLogger logger)
        {
            this.Logger = logger;
        }

        public void Do()
        {
            Logger.LogInfo("Start");
            using (var db = new BloggingContext())
            {
                // Create and save a new Blog 
                Console.Write("Enter a name for a new Blog: ");
                var name = Console.ReadLine();

                var blog = new Blog { Name = name };
                db.Blogs.Add(blog);
                db.SaveChanges();

                // Display all Blogs from the database 
                var query = from b in db.Blogs
                            orderby b.Name
                            select b;

                Console.WriteLine("All blogs in the database:");
                foreach (var item in query)
                {
                    Console.WriteLine(item.Name);
                }

                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }

        public override void DisposingMethod()
        {
            base.DisposingMethod();

        }
    }
}
