using System;
using System.Threading;
using System.Threading.Tasks;
using FirstProject.BackGroundConfiguration;
using FirstProject.Interfaces.Repositories;
using FirstProject.MailBox;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NCrontab;

namespace FirstProject.BackGroundTask
{
    public class BackGroundTaskProject : BackgroundService
    {
        private readonly ReminderMailConfiguration _config;
        private CrontabSchedule _schedule;
        private DateTime _nextRun;
        private readonly ILogger<BackGroundTaskProject> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public BackGroundTaskProject(IOptions<ReminderMailConfiguration> config, ILogger<BackGroundTaskProject> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _config = config.Value;
            _schedule = CrontabSchedule.Parse(_config.CronExpression);
            _nextRun = _schedule.GetNextOccurrence(DateTime.UtcNow);
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var now = DateTime.UtcNow;
                try
                {
                    using var scope = _serviceScopeFactory.CreateScope();
                    // var products = scope.ServiceProvider.GetRequiredService<IFarmProductRepository>();
                    // var orders = scope.ServiceProvider.GetRequiredService<IOrderRepository>();
                    // var farms = scope.ServiceProvider.GetRequiredService<IFarmRepository>();
                    var mail = scope.ServiceProvider.GetRequiredService<IMailMessage>();
                    var requests = scope.ServiceProvider.GetRequiredService<IRequestRepository>();
                    var products = scope.ServiceProvider.GetRequiredService<IFarmProductRepository>();

                    var pendingRequests = await requests.GetAllPendingRequestReturningObjectAsync();
                    if (pendingRequests.Count != 0)
                    {
                        foreach (var item in pendingRequests)
                        {

                            var obj = new GetFarmProductByRequestModel(item.YearNeeded, item.MonthNeeded, item.Grade, item.FarmProduce.Name, item.Quantity);
                            var farmProducts = await products.GetFarmProductByRequestAsync(obj);
                            if (farmProducts.Count != 0)
                            {
                                item.Status = RequestStatus.Merged;
                                await requests.UpdateAsync(item);
                               // mail.NotifyCompanyAboutOrder(item.Company.Email);
                            }
                            if(farmProducts.Count == 0)
                            {
                                item.Status = RequestStatus.Pending;
                                await requests.UpdateAsync(item);
                            }
                           

                        }
                    }

                    // var neededProducts = await products.GetAllFarmProductObjectAsync();
                    // if (neededProducts != null)
                    // {
                    //     foreach (var item in neededProducts)
                    //     {
                    //         var newHarvestedDay = item.HarvestedPeriod.AddDays(-1);
                    //         item.HarvestedPeriod = newHarvestedDay;
                    //         await products.UpdateAsync(item);
                    //     }
                    // }

                    // var sendReminderEmailForFarm = await farms.GetFarmByInspectionDate(now);
                    // if (sendReminderEmailForFarm != null)
                    // {
                    //     foreach (var item in sendReminderEmailForFarm)
                    //     {
                    //         mail.NotifyFarmerAboutFarmInspectorEmail(item.Farmer.Email, $"{item.InspectionDate}");
                    //         mail.NotifyFarmInspectorAboutToBeInspectedFarm(item.FarmInspector.Email, $"{item.InspectionDate}");
                    //     }
                    // }

                    // var sendReminderEmailForOrder = await orders.GetOrderByPickUpDateAsync(now);
                    // if(sendReminderEmailForOrder != null)
                    // {
                    //     foreach (var item in sendReminderEmailForOrder)
                    //     {

                    //     }
                    // }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error occured reading Reminder Table in database.{ex.Message}");
                    _logger.LogError(ex, ex.Message);
                }
                _logger.LogInformation($"Background Hosted Service for {nameof(BackGroundTaskProject)} is stopping");
                var timeSpan = _nextRun - now;
                await Task.Delay(timeSpan, stoppingToken);
                _nextRun = _schedule.GetNextOccurrence(DateTime.UtcNow);

            }
        }
    }
}