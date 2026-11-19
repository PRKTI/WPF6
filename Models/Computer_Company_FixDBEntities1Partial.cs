using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF6.Models
{
    public partial class Computer_Company_FixDBEntities1
    {
        private static Computer_Company_FixDBEntities1 _context;

        public static Computer_Company_FixDBEntities1 GetContext()
        {
            if (_context == null)
            {
                _context = new Computer_Company_FixDBEntities1();
            }
            return _context;
        }
    }
}
