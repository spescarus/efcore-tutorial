namespace Domain.Base;

public  class Entity
{
    public long Id { get; protected init; }

    protected Entity()
    {

    }

    protected Entity(long id)
    {
        Id = id;
    }
}