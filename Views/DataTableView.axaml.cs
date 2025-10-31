using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using SukiUIDemo.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace SukiUIDemo.Views
{
    public partial class DataTableView : UserControl
    {
        private ObservableCollection<Product> _products;
        private Random _random = new Random();

        public DataTableView()
        {
            InitializeComponent();
            _products = new ObservableCollection<Product>();
            
            // 确保控件已加载后再设置数据源
            var dataGrid = this.FindControl<DataGrid>("ProductsDataGrid");
            if (dataGrid != null)
            {
                dataGrid.ItemsSource = _products;
                
                // 异步初始化数据，防止界面卡顿
                _ = InitializeDataAsync();
            }
        }

        private async Task InitializeDataAsync()
        {
            // 显示加载状态
            UpdateStatusText("正在初始化数据...");
            
            // 异步添加初始数据
            await AddSampleDataAsync();
            
            UpdateStatusText("数据初始化完成");
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private async Task AddSampleDataAsync(object? sender = null, RoutedEventArgs? e = null)
        {
            var categories = new[] { "电子产品", "服装", "食品", "家居", "图书" };
            var statuses = new[] { "已完成", "处理中", "已取消", "待发货" };

            // 检查是否要添加大量数据
            var addLargeData = sender is Button button && button.Content?.ToString()?.Contains("10万") == true;
            var count = addLargeData ? 10000 : 10; // 减少到1万条，避免性能问题

            // 显示加载进度
            UpdateStatusText($"正在生成{count}条数据...");

            // 使用批量添加优化性能
            var batchSize = 100;
            var batch = new List<Product>();

            for (int i = 1; i <= count; i++)
            {
                var product = new Product
                {
                    Id = _products.Count + i,
                    Name = $"产品 {_products.Count + i}",
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
                    // 一次性添加整个批次到UI线程，减少Dispatcher调用次数
                    var currentBatch = batch.ToList();
                    await Dispatcher.UIThread.InvokeAsync(() =>
                    {
                        foreach (var item in currentBatch)
                        {
                            _products.Add(item);
                        }
                    });
                    
                    batch.Clear();
                    
                    // 让出控制权，允许UI响应
                    await Task.Delay(1);
                    
                    // 更新进度显示
                    if (i % 1000 == 0 || i == count)
                    {
                        UpdateStatusText($"已生成 {i}/{count} 条数据...");
                    }
                }
            }

            UpdateStatusText($"已添加{count}条产品数据，共{_products.Count}条记录");
        }

        // 保持同步方法用于按钮事件
        private async void AddSampleData(object? sender, RoutedEventArgs e)
        {
            // 禁用按钮防止重复点击
            if (sender is Button button)
            {
                button.IsEnabled = false;
                try
                {
                    await AddSampleDataAsync(sender, e);
                }
                finally
                {
                    button.IsEnabled = true;
                }
            }
            else
            {
                await AddSampleDataAsync(sender, e);
            }
        }

        private async void ClearData(object? sender, RoutedEventArgs e)
        {
            // 异步清空数据，防止界面卡顿
            await Task.Run(() =>
            {
                Dispatcher.UIThread.InvokeAsync(() =>
                {
                    _products.Clear();
                    UpdateStatusText("数据已清空");
                });
            });
        }

        private void ExpandAllRows(object? sender, RoutedEventArgs e)
        {
            // 通过选择所有行来展开详情
            ProductsDataGrid.SelectedItems.Clear();
            foreach (var item in _products)
            {
                ProductsDataGrid.SelectedItems.Add(item);
            }
            UpdateStatusText("所有行已展开");
        }

        private void CollapseAllRows(object? sender, RoutedEventArgs e)
        {
            // 清除选择来折叠所有行
            ProductsDataGrid.SelectedItems.Clear();
            UpdateStatusText("所有行已折叠");
        }

        private void UpdateStatusText(string message)
        {
            var statusText = this.FindControl<TextBlock>("StatusText");
            if (statusText != null)
            {
                statusText.Text = $"{DateTime.Now:HH:mm:ss} - {message}";
            }
        }
    }
}