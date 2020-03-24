using Store.Interfaces.Entity;

namespace Store.Interfaces.ViewModel
{
    public interface IEntityWindowViewModelBase<TEntity> : IViewModel
        where TEntity : class, IEntity
    {
        void Initilize();
        void Initilize(TEntity entity);

        T MapFrom<T>() where T : class;
    }
}
