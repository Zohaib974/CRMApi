using AutoMapper;
using CRMContracts;
using CRMEntities.Models;
using CRMHelper;
using CRMModels;
using CRMModels.Common;
using CRMModels.DataTransfersObjects;
using CRMWebHost.ActionFilters;
using CRMWebHost.Base;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
namespace CRMWebHost.Base
{
    public class BaseController : Controller
    {
        private readonly UserManager<User> _userManager;
        public BaseController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public long LoggedInUserId
        {
            get
            {
                var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
                return string.IsNullOrEmpty(id) ? 0 : long.Parse(id);
            }
        }
        public long LoggedInUserCompanyId
        {
            get
            {
                var id = User.FindFirstValue("CompanyId");
                return string.IsNullOrEmpty(id) ? 0 : long.Parse(id);
            }
        }


    }
}
