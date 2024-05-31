using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebPlatform.SyncDataServices;

namespace WebPlatform;

[Route("api/platforms")]
[ApiController]
public class PlatformsController : ControllerBase
{
    private readonly IPlatformRepo _repository;
    private readonly IMapper _mapper;
    private readonly ICommandServices _commandDataService;
    
    public PlatformsController(IPlatformRepo platformRepo, IMapper mapper, ICommandServices commandDataService)
    {
        _mapper = mapper;
        _repository = platformRepo;
        _commandDataService = commandDataService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
    {
        Console.WriteLine("--> Getting Platforms from PlatformService");
        var platforms = _repository.GetAllPlatforms();
        return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platforms));
    }

    [HttpGet("{id}", Name = "GetPlatformById")]
    public ActionResult<PlatformReadDto> GetPlatformById(int id)
    {
        var platform = _repository.GetPlatformById(id);
        return Ok(_mapper.Map<PlatformReadDto>(platform));

        return NotFound();
    }
    
    [HttpPost]
    public async Task<ActionResult<PlatformReadDto>> CreatePlatform(PlatformCreateDto platformCreateDto)
    {
        var platformModel = _mapper.Map<Platform>(platformCreateDto);
        _repository.CreatePlatform(platformModel);
        _repository.SaveChanges();

        var platFormReadDto = _mapper.Map<PlatformReadDto>(platformModel);

        try
        {
            await _commandDataService.SendPlatformToComment(platFormReadDto);
            
        }
        catch (Exception e)
        {
            Console.WriteLine($"Could not sync data: {e.Message}");
            throw;
        }
        return CreatedAtRoute(nameof(GetPlatformById), new {Id = platFormReadDto.Id}, platFormReadDto);
    }

}
