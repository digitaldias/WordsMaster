using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordsMaster
{
    public class RuntimeRegistry : Registry
    {
        public RuntimeRegistry()
        {
            Scan(x => 
            {
                x.Assembly("WordsMaster.Business");
                x.Assembly("WordsMaster.Domain");
                x.Assembly("WordsMaster.Data");

                x.WithDefaultConventions();
            });
        }
    }
}
