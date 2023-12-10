
namespace SpaManagement.Service.DTOs
{
    public class ProductByPlanDTO
    {
        public int Id { get; set; }
        public string PlanName { get; set; }
        public double Price { get; set; }
        public List<PlanProduct> Product { get; set; }
    }

    public class PlanProduct
    {
        public string ProductName { get; set; }
    }
}

