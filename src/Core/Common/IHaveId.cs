namespace AttachManagement.Core.Common;

public interface IHaveId<out T>
{
    T Id { get; }
}
