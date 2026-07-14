using BusinessObject;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository roleService;
        public RoleService()
        {
            roleService = new RoleRepository();
        }
        public List<Role> GetAllRole()
        {
            return roleService.GetAllRoles();
        }
    }
}
