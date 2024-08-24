using Bingogo.Core.Exceptions;
using Bingogo.Data.Entities;
using Bingogo.Models;
using Bingogo.Services.Extensions;
using Bingogo.Services.Interfaces;
using Bingogo.Services.Specifications;
using Bingogo.Services.Specifications.Implementations;
using Microsoft.EntityFrameworkCore;

namespace Bingogo.WebApi.Controllers;

[ApiController]
[Route("api/boards")]
public class BoardController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IBoardService _service;

    public BoardController(IMapper mapper, IBoardService service)
    {
        _mapper = mapper;
        _service = service;
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<BoardModel> Get([FromRoute] long id, [FromQuery] ResourceQuery query)
    {
        return await _service
            .Search()
            .Where(x => x.Id == id)
            .OrderBy(x => x.Id)
            .ProjectTo<BoardModel>(_mapper, query.Include)
            .FirstAsync() ?? throw new EntityNotFoundException();
    }

    [HttpGet]
    public async Task<IEnumerable<BoardModel>> Get([FromQuery] BoardQuery query, [FromQuery] ListQuery listQuery)
    {
        var entities = await _service
            .Search()
            .Where(query)
            .Where(listQuery)
            .ToListAsync();

        return _mapper.Map<IEnumerable<BoardModel>>(entities);
    }

    [HttpPost]
    public async Task<IEnumerable<BoardModel>> Create([FromBody] IEnumerable<BoardForm> forms)
    {
        var entities = await _service
            .InsertRangeAsync(_mapper.Map<IEnumerable<Board>>(forms));

        return _mapper.Map<IEnumerable<BoardModel>>(entities);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<BoardModel> Update([FromRoute] long id, [FromBody] BoardProps props)
    {
        var entity = await _service.UpdateAsync(id, props, _mapper.Patch);

        return _mapper.Map<BoardModel>(entity);
    }

    [HttpPut]
    public async Task<IEnumerable<BoardModel>> Update([FromBody] IDictionary<long, BoardProps> data)
    {
        var entities = await _service.UpdateRangeAsync(data, _mapper.Patch);

        return _mapper.Map<IList<BoardModel>>(entities);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<BoardModel> Delete([FromRoute] long id)
    {
        var entity = await _service.DeleteAsync(id);

        return _mapper.Map<BoardModel>(entity);
    }

    [HttpDelete]
    public async Task<IEnumerable<BoardModel>> Delete([FromBody] ICollection<long> ids)
    {
        var entities = await _service.DeleteRangeAsync(ids);

        return _mapper.Map<IEnumerable<BoardModel>>(entities);
    }
}
