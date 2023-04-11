namespace Common.Exceptions;

public class EntityNotFoundException : NullReferenceException
{
    public EntityNotFoundException(Type type, Guid id) :
        base(String.Format($"{type.FullName} with id {id} not found"))
    {
        
    }
    public EntityNotFoundException(Type type, Guid id, Type collectionMemberType) :
        base(String.Format($"Not found items {collectionMemberType.FullName} in {type.FullName}"))
    {

    }
}