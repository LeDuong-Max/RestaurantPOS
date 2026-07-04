using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF
{
    public static class AppSession
    {
        public static Account? CurrentUser { get; set; }
    }
}
