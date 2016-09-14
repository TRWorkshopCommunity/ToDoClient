using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebApplication.Infrastructure
{
    public sealed class IdGenerator
    {
        private static volatile IdGenerator instance;
        private static readonly object syncRoot = new object();
        private static Generator.CustomGenerator.Generator generator;

        private IdGenerator()
        {
            generator = new Generator.CustomGenerator.Generator(
                Convert.ToInt32(File.ReadAllText(HttpContext.Current.Server.MapPath("~/App_Data/id.txt"))));
        }

        public static IdGenerator Instance
        {
            get
            {
                if (instance != null) return instance;
                lock (syncRoot)
                {
                    if (instance == null)
                        instance = new IdGenerator();
                }

                return instance;
            }
        }

        public int GenerateId()
        {
            generator.MoveNext();
            File.WriteAllText(HttpContext.Current.Server.MapPath("~/App_Data/id.txt"), generator.Current.ToString());
            return generator.Current;
            
        }
    }
}