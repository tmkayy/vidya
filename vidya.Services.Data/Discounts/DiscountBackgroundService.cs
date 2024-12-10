using CloudinaryDotNet.Actions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace vidya.Services.Data.Discounts
{
    public class DiscountBackgroundService : BackgroundService
    {
        // inject service provider because i can't inject scoped in singleton
        private readonly IServiceProvider _serviceProvider;

        public DiscountBackgroundService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var discountsService = scope.ServiceProvider.GetRequiredService<IDiscountService>();
                    await discountsService.DeleteExpiredDiscountsAsync();
                }

                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }
        }
    }
}
