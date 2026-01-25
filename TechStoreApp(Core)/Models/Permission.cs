using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStoreApp_Core_.Models
{
    public class Permission
    {
        public int? Id { get; set; }
        public string PermissionName { get; set; }
        public int PermissionCode { get; set; }

        public Permission(int ? id , string permissionName , int permissiomCode)
        {
            Id = id;
            PermissionName = permissionName;
            PermissionCode = permissiomCode;
        }

    }
}
