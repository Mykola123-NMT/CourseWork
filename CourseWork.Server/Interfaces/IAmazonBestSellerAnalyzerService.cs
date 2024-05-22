using CourseWork.Server.Models;

namespace CourseWork.Server.Interfaces
{
    public interface IAmazonBestSellerAnalyzerService
    {
        MLModel.ModelOutput GetProductBestSellerAnalysis(AmazonProduct product);
    }
}
