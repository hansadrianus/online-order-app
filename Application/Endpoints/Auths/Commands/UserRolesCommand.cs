﻿using Application.Models;
using Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Endpoints.Auths.Commands
{
    public class UserRolesCommand : IRequest<EndpointResult<UserRolesViewModel>>
    {
        public int? UserId { get; set; }
        public int RoleId { get; set; }
        public bool IsSelected { get; set; }
    }
}
