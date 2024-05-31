using AutoMapper;

namespace WebPlatform;

public class PlatformProfiles : Profile
{
    public PlatformProfiles()
    {
        // Source -> Target
        CreateMap<Platform, PlatformReadDto>();
        CreateMap<PlatformCreateDto, Platform>();
    }
}