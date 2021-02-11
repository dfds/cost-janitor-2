using CostJanitor.Application.Mapping.Converters;
using ResourceProvisioning.Abstractions.Aggregates;
using ResourceProvisioning.Abstractions.Commands;

namespace CostJanitor.Application.Mapping.Profiles
{
    public sealed class DefaultProfile : AutoMapper.Profile
    {
        public DefaultProfile()
        {
            CreateMap<IAggregateRoot, ICommand<IAggregateRoot>>()
            .ConvertUsing<AggregateRootToCommandConverter>();
        }
    }
}
