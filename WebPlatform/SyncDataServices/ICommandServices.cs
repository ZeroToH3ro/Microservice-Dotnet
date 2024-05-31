namespace WebPlatform.SyncDataServices;

public interface ICommandServices
{
    Task SendPlatformToComment(PlatformReadDto platformReadDto);
}

