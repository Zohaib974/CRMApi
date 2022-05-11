using AutoMapper;
using CRMContracts;
using CRMEntities.Models;
using CRMModels.DataTransfersObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRMServices.Implementation
{
    public class ServiceManager : IServiceManager
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repositoryManager;
        public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper, ILoggerManager logger)
        {
            _logger = logger;
            _mapper = mapper;
            _repositoryManager = repositoryManager;
        }
        // public CompanyDto CreateCompany(CompanyForCreationDto company)
        //{
        //    var companyEntity = _mapper.Map<Company>(company);
        //    _repositoryManager.Company.CreateCompany(companyEntity);
        //    _repositoryManager.SaveAsync();
        //    return _mapper.Map<CompanyDto>(companyEntity);
        //}
    }
}
