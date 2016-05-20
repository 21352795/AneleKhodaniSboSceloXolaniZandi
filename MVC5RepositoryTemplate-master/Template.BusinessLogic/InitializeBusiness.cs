using System.Data.Entity;
using Template.Data;

namespace Template.BusinessLogic
{
    public  static class InitializeBusiness
    {
        public static void Initilize()
        {
            Database.SetInitializer<DataContext>(new DbInitialize<DataContext>());
            var ctx = new DataContext();
            //ctx.Database.Initialize(true);
        }

    }
}
