using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NextStar.ManageService.Core.Repositories;

namespace NextStar.ManageService.Core.Businesses
{
    public class ApplicationConfigBusiness:IApplicationConfigBusiness
    {
        private readonly IApplicationConfigRepository _repository;
        public ApplicationConfigBusiness(IApplicationConfigRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<NextStarAccountDbModels.ApplicationConfig>> GetListAsync()
        {
            return await _repository.GetAllListAsync();
        }
    }
}