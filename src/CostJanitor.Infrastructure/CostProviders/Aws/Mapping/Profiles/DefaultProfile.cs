using Amazon.CostExplorer.Model;
using CostJanitor.Infrastructure.CostProviders.Aws.Mapping.Converters;
using CostJanitor.Infrastructure.CostProviders.Aws.Model;
using ResourceProvisioning.Abstractions.Aggregates;

namespace CostJanitor.Infrastructure.CostProviders.Aws.Mapping.Profiles
{
    public sealed class DefaultProfile : AutoMapper.Profile
    {
        public DefaultProfile()
        {
            CreateMap<GetCostAndUsageResponse, AwsCostDto>()
            .ReverseMap();

            CreateMap<AwsCostDto, IAggregateRoot>()
            .ConvertUsing<AwsCostDtoToAggregateConvert>();
        }
    }
}
