using CourseWork.Server.Interfaces;
using CourseWork.Server.Models;

namespace CourseWork.Server.Services
{
    public class AmazonBestSellerAnalyzerService : IAmazonBestSellerAnalyzerService
    {
        public MLModel.ModelOutput GetProductBestSellerAnalysis(AmazonProduct product)
        {
            MLModel.ModelInput modelInput = new MLModel.ModelInput() { 
                Stars = (float)product.Stars.GetValueOrDefault(), 
                Reviews = product.Reviews.GetValueOrDefault(), 
                Price = (float)product.Price.GetValueOrDefault(),
                ListPrice = (float)product.ListPrice.GetValueOrDefault(),
                Category_id = product.CategoryId.GetValueOrDefault(),
                IsBestSeller = product.IsBestSeller.GetValueOrDefault(),
                BoughtInLastMonth = product.BoughtInLastMonth.GetValueOrDefault(),
            };

            return MLModel.Predict(modelInput);
        }
    }
}
