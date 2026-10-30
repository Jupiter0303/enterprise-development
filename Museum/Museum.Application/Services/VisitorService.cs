using Museum.Application.Common;
using Museum.Domain;
using Museum.Application.Contracts;

namespace Museum.Application.Services;

public interface VisitorService(IRepository<Visitor> repository) 
    : CrudServiceBase<Visitor, VisitorDto, VisitorCreateUpdateDto>
{
}
