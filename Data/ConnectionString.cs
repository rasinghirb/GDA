using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GDA.Data
{
    public class ConnectionString
    {
        private static string DCS = "Data Source=.\\SQLEXPRESS;Database=DbIRBn;Integrated Security=true";
        public static string _DCS { get => DCS; }
    }
}
