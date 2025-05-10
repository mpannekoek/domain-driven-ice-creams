namespace IceCream.SharedKernel.Domain;

public abstract class Entity<TId> : Entity, IEquatable<Entity<TId>> where TId : struct
{
    protected Entity(TId id) => Id = id;

    public TId Id { get; }

    public bool Equals(Entity<TId>? other)
    {
        if (other is null)
        {
            return false;
        }

        if (other.GetType() != GetType())
        {
            return false;
        }

        return other.Id.Equals(Id);
    }

    public override bool Equals(object? obj)
    {
        if (obj is Entity<TId> entity)
        {
            return Equals(entity);
        }

        return false;
    }

    public static bool operator ==(Entity<TId>? first, Entity<TId>? second)
    {
        if (first is null && second is null)
        {
            return true;
        }

        if (first is null || second is null)
        {
            return false;
        }

        return first.Equals(second);
    }

    public static bool operator !=(Entity<TId>? first, Entity<TId>? second) => !(first == second);

    public override int GetHashCode() => Id.GetHashCode();
}

public abstract class Entity
{
}
