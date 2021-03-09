using AutoMapper;
using CloudEngineering.CodeOps.Abstractions.Aggregates;
using CloudEngineering.CodeOps.Abstractions.Commands;
using CostJanitor.Application.Commands;
using CostJanitor.Application.Commands.Report;
using CostJanitor.Domain.Aggregates;
using System;

namespace CostJanitor.Application.Mapping.Converters
{
    public class AggregateRootToCommandConverter : ITypeConverter<IAggregateRoot, ICommand<IAggregateRoot>>
    {
        public ICommand<IAggregateRoot> Convert(IAggregateRoot source, ICommand<IAggregateRoot> destination = default, ResolutionContext context = default)
        {
            switch (source)
            {
                case ReportRoot report:
                    if (report.Id == Guid.Empty)
                    {
                        return new CreateReportCommand(report.CostItems);
                    }
                    else
                    {
                        return new UpdateReportCommand(report);
                    }

                case null:
                default:
                    break;
            }

            return null;
        }
    }
}
