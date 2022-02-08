using NextStar.Library.MicroService.Outputs;
using NextStar.SystemService.Core.Entities.ApplicationConfig;

namespace NextStar.SystemService.Core.Businesses.ApplicationConfig;

public interface IApplicationConfigBusiness
{
    Task<PageCommonDto<ManagementDbModels.ApplicationConfig>> GetApplicationConfigListAsync(SelectInput selectInput);
    
    Task<bool> UpdateApplicationConfigAsync(ManagementDbModels.ApplicationConfig config);
}