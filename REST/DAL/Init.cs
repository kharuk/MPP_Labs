using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Observer;


namespace DAL
{
    public static class Init
    {
        /// <summary>
        /// Implements method subscription in observer
        /// </summary>
        public static void Initialize()
        {
            Get get = new Get();
            GetAll getAll = new GetAll();
            Create create = new Create();
            Delete delete = new Delete();
            UpdateBook update = new UpdateBook();
        }
    }
}
