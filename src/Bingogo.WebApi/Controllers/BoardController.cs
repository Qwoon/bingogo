using AutoMapper;
using Bingogo.Core.Exceptions;
using Bingogo.Models;
using Bingogo.Services.Extensions;
using Bingogo.Services.Interfaces;
using Bingogo.Services.Specifications.Implementations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

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
    public async Task<BoardModel> Get([FromQuery] long id, [FromQuery] ResourceQuery query)
    {
        return await _service
            .Search()
            .OrderBy(x => x.Id)
            .ProjectTo<BoardModel>(_mapper, query.Include)
            .FirstAsync() ?? throw new EntityNotFoundException();
    }
}
