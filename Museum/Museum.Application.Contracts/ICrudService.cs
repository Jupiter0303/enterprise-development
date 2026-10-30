namespace Museum.Application.Contracts;

public interface ICrudService<TDto, TCreateUpdateDto>
{
    public Task<TDto?> Get(int id);
    public Task<IEnumerable<TDto>> GetAll();
    public Task<TDto> Create(TCreateUpdateDto dto);
    public Task<TDto?> Update(int id, TCreateUpdateDto dto);
    public Task<bool> Delete(int id);
}
