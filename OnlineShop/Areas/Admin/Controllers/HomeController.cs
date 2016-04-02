using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using Model.DAO;
using OnlineShop.Areas.Admin.Models;
using OnlineShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Admin/Home
        public ActionResult Index()
        {

            //create a collection of data
            var transactionCounts = new List<TransactionCount> { 
                           new TransactionCount(){  MonthName="January", Count=30},
                           new TransactionCount(){  MonthName="February", Count=40},
                           new TransactionCount(){  MonthName="March", Count=4},
                           new TransactionCount(){  MonthName="April", Count=35}
                            };

            //modify data type to make it of array type
            var xDataMonths = transactionCounts.Select(i => i.MonthName).ToArray();
            var yDataCounts = transactionCounts.Select(i => new object[] { i.Count }).ToArray();

            //instanciate an object of the Highcharts type
            var chart = new Highcharts("chart")
                //define the type of chart 
                        .InitChart(new Chart { DefaultSeriesType = ChartTypes.Line })
                //overall Title of the chart 
                        .SetTitle(new Title { Text = "THỐNG KÊ SẢN PHẨM TRONG NĂM " + DateTime.Now.Year.ToString() })
                //small label below the main Title
                        //.SetSubtitle(new Subtitle { Text = "Accounting" })
                //load the X values
                        .SetXAxis(new XAxis { Categories = xDataMonths })
                //set the Y title
                        .SetYAxis(new YAxis { Title = new YAxisTitle { Text = "Số lượng sản phẩm" } })
                        .SetTooltip(new Tooltip
                        {
                            Enabled = true,
                            Formatter = @"function() { return '<b>'+ this.series.name +'</b><br/>'+ this.x +': '+ this.y; }"
                        })
                        .SetPlotOptions(new PlotOptions
                        {
                            Line = new PlotOptionsLine
                            {
                                DataLabels = new PlotOptionsLineDataLabels
                                {
                                    Enabled = true
                                },
                                EnableMouseTracking = false
                            }
                        })
                //load the Y values 
                        .SetSeries(new[]
                    {
                        new Series {Name = "Hour", Data = new Data(yDataCounts)},
                            //you can add more y data to create a second line
                            // new Series { Name = "Other Name", Data = new Data(OtherData) }
                    });


            return View(chart);
        }

        [HttpGet]
        public JsonResult GetData()
        {
            var lst = new List<int>();
            var productDAO = new ProductDAO();
            var lstChart = new List<ModelChart>();

            var lstProduct = productDAO.GetAllProduct();
            for (int i = 1; i <= 12; i++)
            {
                var count = lstProduct.Where(p => p.CreatedDate.Value.Month == i).Count();
                var objChart = new ModelChart
                {
                    name = "Tháng " + i.ToString(),
                    y = count,
                    id="thang" + i.ToString()
                };
                //lst.Add(count);
                lstChart.Add(objChart);

            }
            return new JsonResult { Data = lstChart, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}