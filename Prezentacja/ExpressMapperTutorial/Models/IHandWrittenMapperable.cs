namespace ExpressMapperTutorial.Models
{
    public interface IHandWrittenMapperable<out TDest>
    {
        TDest Map();
    }
}