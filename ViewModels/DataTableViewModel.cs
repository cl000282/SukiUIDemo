using Avalonia;
using Avalonia.Controls;
using Avalonia.Threading;
using Prism.Commands;
using Prism.Mvvm;
using SukiUIDemo.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SukiUIDemo.ViewModels
{
    public class DataTableViewModel : BindableBase
    {
        private ObservableCollection<Product>? _products;
        private string? _statusText;
        private bool _areAllRowsExpanded;
        private readonly Random _random = new();

        public ObservableCollection<Product> Products
        {
            get => _products ?? [];
            set => SetProperty(ref _products, value);
        }

        public string StatusText
        {
            get => _statusText ?? "";
            set => SetProperty(ref _statusText, value);
        }

        public ICommand AddSampleDataCommand { get; }
        public ICommand ClearDataCommand { get; }
        public ICommand ExpandAllRowsCommand { get; }
        public ICommand CollapseAllRowsCommand { get; }

        public DataTableViewModel()
        {
            Products = [];
            StatusText = "点击按钮操作数据表格";

            AddSampleDataCommand = new DelegateCommand<string>(async (count) => await AddSampleDataAsync(count));
            ClearDataCommand = new DelegateCommand(async () => await ClearDataAsync());
            ExpandAllRowsCommand = new DelegateCommand(ExpandAllRows);
            CollapseAllRowsCommand = new DelegateCommand(CollapseAllRows);

            // 异步初始化数据
            _ = InitializeDataAsync();
        }

        private async Task InitializeDataAsync()
        {
            StatusText = "正在初始化数据...";
            await AddSampleDataAsync("10");
            StatusText = "数据初始化完成";
        }

        private async Task AddSampleDataAsync(string countType)
        {
            var categories = new[] { "电子产品", "服装", "食品", "家居", "图书" };
            var statuses = new[] { "已完成", "处理中", "已取消", "待发货" };

            var addLargeData = countType == "100000";
            var count = addLargeData ? 10000 : 10; // 减少到1万条，避免性能问题

            StatusText = $"正在生成{count}条数据...";

            // 使用批量添加优化性能
            var batchSize = 100;
            var batch = new List<Product>();

            for (int i = 1; i <= count; i++)
            {
                var product = new Product
                {
                    Id = Products.Count + i,
                    Name = $"产品 {Products.Count + i}",
                    Category = categories[_random.Next(categories.Length)],
                    Price = Math.Round((decimal)(_random.NextDouble() * 1000 + 10), 2),
                    Stock = _random.Next(0, 100),
                    IsActive = _random.Next(2) == 1
                };

                // 为每个产品添加2-5个订单
                var orderCount = _random.Next(2, 6);
                for (int j = 1; j <= orderCount; j++)
                {
                    product.Orders.Add(new Order
                    {
                        OrderId = j,
                        OrderDate = DateTime.Now.AddDays(-_random.Next(1, 30)),
                        Quantity = _random.Next(1, 10),
                        TotalAmount = Math.Round((decimal)(_random.NextDouble() * 500 + 10), 2),
                        Status = statuses[_random.Next(statuses.Length)]
                    });
                }

                batch.Add(product);

                // 批量添加，提高性能
                if (batch.Count >= batchSize || i == count)
                {
                    var currentBatch = batch.ToList();
                    await Dispatcher.UIThread.InvokeAsync(() =>
                    {
                        foreach (var item in currentBatch)
                        {
                            Products.Add(item);
                        }
                    });
                    
                    batch.Clear();
                    
                    // 让出控制权，允许UI响应
                    await Task.Delay(1);
                    
                    // 更新进度显示
                    if (i % 1000 == 0 || i == count)
                    {
                        StatusText = $"已生成 {i}/{count} 条数据...";
                    }
                }
            }

            StatusText = $"已添加{count}条产品数据，共{Products.Count}条记录";
        }

        private async Task ClearDataAsync()
        {
            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                Products.Clear();
                StatusText = "数据已清空";
            });
        }

        private void ExpandAllRows()
        {
            // 在实际应用中，这里应该通过事件或消息机制通知View
            // 由于Avalonia DataGrid在MVVM模式下控制行展开比较复杂
            // 这里我们提供一个简单的状态指示
            StatusText = "所有行已展开（功能需要在前端代码中实现）";
        }

        private void CollapseAllRows()
        {
            // 在实际应用中，这里应该通过事件或消息机制通知View
            StatusText = "所有行已折叠（功能需要在前端代码中实现）";
        }
    }
}