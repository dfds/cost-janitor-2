using Amazon.CostExplorer.Model;
using CostJanitor.Infrastructure.CostProviders.Aws.DataTransferObjects;
using CostJanitor.Infrastructure.CostProviders.Aws.Mapping.Converters;
using ResourceProvisioning.Abstractions.Aggregates;

namespace CostJanitor.Infrastructure.CostProviders.Aws.Mapping.Profiles
{
    public sealed class DefaultProfile : AutoMapper.Profile
    {
        public DefaultProfile()
        {
            CreateMap<GetCostAndUsageResponse, AwsCostDto>()
            .ConvertUsing<GetCostAndUsageResponseToAwsCostDto>();

            CreateMap<AwsCostDto, IAggregateRoot>()
            .ConvertUsing<AwsCostDtoToAggregateConvert>();
        }
    }
}
