using Bingogo.Models;
using Bingogo.Services.Extensions;
using Bingogo.Services.Interfaces;

namespace Bingogo.WebApi.Controllers;

[ApiController]
[Route("api/board-tiles")]
public class BoardTileController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IBoardTileService _service;

    public BoardTileController(IMapper mapper, IBoardTileService service)
    {
        _mapper = mapper;
        _service = service;
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<BoardTileModel> Update([FromRoute] long id, [FromBody] BoardProps props)
    {
        var entity = await _service
            .UpdateAsync(id, props, _mapper.Patch);

        return _mapper.Map<BoardTileModel>(entity);
    }

    [HttpPut]
    public async Task<IEnumerable<BoardTileModel>> Update([FromBody] IDictionary<long, BoardTileProps> data)
    {
        var entity = await _service
            .UpdateRangeAsync(data, _mapper.Patch);

        return _mapper.Map<IEnumerable<BoardTileModel>>(entity);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<BoardTileModel> Delete([FromRoute] long id)
    {
        var entity = await _service
                .DeleteAsync(id);

        return _mapper.Map<BoardTileModel>(entity);
    }
}
