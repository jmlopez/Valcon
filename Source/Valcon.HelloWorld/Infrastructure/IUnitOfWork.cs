namespace Valcon.HelloWorld.Infrastructure
{
	public interface IUnitOfWork
	{
	    void Initialize();
		void Insert(object entity);
		void Update(object entity);
		void Delete(object entity);
		void Commit();
	    void Rollback();
	}

    public class UnitOfWork : IUnitOfWork
    {
        public void Initialize()
        {
        }

        public void Insert(object entity)
        {
        }

        public void Update(object entity)
        {
        }

        public void Delete(object entity)
        {
        }

        public void Commit()
        {
        }

        public void Rollback()
        {
        }
    }
}
