using Museum.Application.Common;
using Microsoft.Extensions.Logging;
using Museum.Application.Contracts.Visitors;
using Museum.Application.Mapping;
using Museum.Domain;

namespace Museum.Application.Services;

public class VisitorService(IRepository<Visitor> repository, ILogger<VisitorService> logger)
    : CrudServiceBase<Visitor, VisitorDto, VisitorCreateUpdateDto>(repository, logger), IVisitorService
{
    protected override VisitorDto ToDto(Visitor entity) => entity.ToDto();

    protected override Task<Visitor> CreateEntity(VisitorCreateUpdateDto dto) =>
        Task.FromResult(dto.ToEntity());

    protected override Task ApplyChanges(Visitor entity, VisitorCreateUpdateDto dto)
    {
        entity.UpdateFrom(dto);
        return Task.CompletedTask;
    }
}