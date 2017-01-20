namespace ExpressMapperTutorial
{
    public interface IUnitOfTestSetCreatable
    {
        T[] CreateSet<T>(int numerOfMapping);
    }
}