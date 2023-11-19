using SpaManagement.Data.Abstract;
using SpaManagement.Domain.Entities;

namespace SpaManagement.Data
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        SpaManagementContext _spaManagementContext;

        Repository<Appointment> _repositoryAppointment;
        Repository<AppointmentPlanDetail> _repositoryAppointmentPlanDetail;
        Repository<AppointmentProductDetail> _repositoryAppointmentProductDetail;
        Repository<Plan> _repositoryPlan;
        Repository<PlanDetail> _repositoryPlanDetail;
        Repository<Product> _repositoryProduct;
        Repository<Services> _repositoryServices;
        Repository<UserToken> _repositoryUserToken;
        private bool disposedValue;

        public UnitOfWork(SpaManagementContext spaManagementContext)
        {
            _spaManagementContext = spaManagementContext;
        }

        public Repository<Appointment> AppointmentRepository => _repositoryAppointment ??= new Repository<Appointment>(_spaManagementContext);

        public Repository<AppointmentPlanDetail> AppointmentPlanDetailRepository => _repositoryAppointmentPlanDetail ??= new Repository<AppointmentPlanDetail>(_spaManagementContext);

        public Repository<AppointmentProductDetail> AppointmentProductDetailRepository => _repositoryAppointmentProductDetail ??= new Repository<AppointmentProductDetail>(_spaManagementContext);

        public Repository<Plan> PlanRepository => _repositoryPlan ??= new Repository<Plan>(_spaManagementContext);

        public Repository<PlanDetail> PlanDetailRepository => _repositoryPlanDetail ??= new Repository<PlanDetail>(_spaManagementContext);

        public Repository<Product> ProductRepository => _repositoryProduct ??= new Repository<Product>(_spaManagementContext);

        public Repository<Services> ServicesRepository => _repositoryServices ??= new Repository<Services>(_spaManagementContext);

        public Repository<UserToken> UserTokenRepository => _repositoryUserToken ??= new Repository<UserToken>(_spaManagementContext);

        public async Task CommitAsync()
        {
            await _spaManagementContext.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _spaManagementContext.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
