using SpaManagement.Domain.Entities;

namespace SpaManagement.Data.Abstract
{
    public interface IUnitOfWork
    {
        Repository<AppointmentPlanDetail> AppointmentPlanDetailRepository { get; }
        Repository<AppointmentProductDetail> AppointmentProductDetailRepository { get; }
        Repository<Appointment> AppointmentRepository { get; }
        Repository<Customer> CustomerRepository { get; }
        Repository<Employee> EmployeeRepository { get; }
        Repository<PlanDetail> PlanDetailRepository { get; }
        Repository<Plan> PlanRepository { get; }
        Repository<Product> ProductRepository { get; }
        Repository<Services> ServicesRepository { get; }
        Repository<UserToken> UserTokenRepository { get; }

        Task CommitAsync();
        void Dispose();
    }
}