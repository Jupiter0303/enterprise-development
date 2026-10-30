using Museum.Application.Contracts.Common;

namespace Museum.Application.Contracts.Visitors;

public interface IVisitorService: ICrudService<VisitorDto, VisitorCreateUpdateDto>
{
}
