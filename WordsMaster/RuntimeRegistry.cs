using StructureMap;

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
                x.Assembly("WordsMaster.Console");

                x.WithDefaultConventions();
            });
        }
    }
}
