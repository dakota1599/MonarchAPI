using System;
using System.Linq;
using MonarchAPI.Models;

namespace MonarchAPI.Data
{
    public class DbInitializer
    {

        public static void Initialize(MonarchContext context) {

            //To Ensure the database is created.
            context.Database.EnsureCreated();
        }

    }
}
