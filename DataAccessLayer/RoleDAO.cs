using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class RoleDAO
    {
        public static List<Role> GetAllRole()
        {
            using var db = new RestaurantPosContext();
            return db.Roles.ToList();
        }
    }
}
