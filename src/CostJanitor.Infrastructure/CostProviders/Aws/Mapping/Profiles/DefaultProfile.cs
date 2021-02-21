using CloudEngineering.CodeOps.Abstractions.Aggregates;
using CloudEngineering.CodeOps.Infrastructure.AmazonWebServices.DataTransferObjects.Cost;
using CostJanitor.Infrastructure.CostProviders.Aws.Mapping.Converters;

namespace CostJanitor.Infrastructure.CostProviders.Aws.Mapping.Profiles
{
    public sealed class DefaultProfile : AutoMapper.Profile
    {
        public DefaultProfile()
        {
            CreateMap<CostDto, IAggregateRoot>()
            .ConvertUsing<AwsCostDtoToAggregateConvert>();
        }
    }
}
