﻿using ApplicationCore.Interfaces;
using ApplicationCore.Models.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;

        public PermissionService(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public async Task<Permission> GetPermissionById(int permissionId)
        {

            return await _permissionRepository.GetPermissionById(permissionId);
        }
        public void CreatePermission(Permission Permission)
        {

        }
        public void UpdatePermission(Permission Permission)
        {

        }
        public void DeletePermission(int permissionId)
        {

        }
    }
}
