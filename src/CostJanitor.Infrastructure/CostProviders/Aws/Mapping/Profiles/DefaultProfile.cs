using CloudEngineering.CodeOps.Infrastructure.AmazonWebServices.DataTransferObjects.Cost;
using CostJanitor.Domain.Aggregates;
using CostJanitor.Infrastructure.CostProviders.Aws.Mapping.Converters;

namespace CostJanitor.Infrastructure.CostProviders.Aws.Mapping.Profiles
{
    public sealed class DefaultProfile : AutoMapper.Profile
    {
        public DefaultProfile()
        {
            CreateMap<CostDto, ReportRoot>()
            .ConvertUsing<CostDtoToReportRootConverter>();
        }
    }
}
