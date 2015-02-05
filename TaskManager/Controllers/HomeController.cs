using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using TaskManager.DbModel;
using TaskManager.Helpers;
using TaskManager.Models;
using TaskManager.Models.Enums;

namespace TaskManager.Controllers
{
    public class HomeController : Controller
    {
        public static List<SelectListItem> PriorityList
        {
            get
            {
                List<SelectListItem> enumList = new List<SelectListItem>();
                foreach (Priorities data in Enum.GetValues(typeof(Priorities)))
                {
                    enumList.Add(new SelectListItem
                    {
                        Text = data.ToString().Replace("_", " "),
                        Value = ((int)Enum.Parse(typeof(Priorities), data.ToString())).ToString(),

                    });
                }

                return enumList;
            }
        }
        //
        // GET: /Home/
        [HttpGet]
        public ActionResult ManageTasks()
        {
            var tasks = DbHelper.GetAllTasks();
            var model = GetTaskModelFromTasks(tasks);
            var num = 1;
            model.ForEach(x => x.ExDisplayNo = (num++).ToString());
            return View(model);
        }

        [HttpPost]
        public ActionResult ManageTasks(List<TaskModel> tasks)
        {
            foreach (var task in tasks)
            {
                if (task.IsModified)
                {
                    DbHelper.UpdateTask(task.Task);
                }
                if (task.IsDleted)
                {
                    DbHelper.DeleteTask(task.Task);
                }
            }
            return RedirectToAction("ManageTasks");
        }

        [HttpGet]
        public ActionResult CreateTask()
        {
            return View( new Task());
        }

        [HttpPost]
        public ActionResult CreateTask(Task task)
        {
            DbHelper.AddTask(task);
            return RedirectToAction("ManageTasks");
        }

        private List<TaskModel> GetTaskModelFromTasks(List<Task> tasks)
        {
            var retVal = new List<TaskModel>();
            foreach (var task in tasks)
            {
                var tempModel = new TaskModel();
                tempModel.Task = task;
                tempModel.IsModified = false;
                tempModel.IsDleted = false;
                retVal.Add(tempModel);
            }
            return retVal;
        }

        public ActionResult SetUpChartView()
        {
            var tasks = DbHelper.GetAllTasks();
            var myChart = new Chart(width: 1000, height: 600, theme: ChartTheme.Green)
            .AddTitle("Tasks as per required time")
            .AddSeries(
                name: "Tasks",
                 xValue: tasks, xField: "Title",
                 yValues: tasks, yFields: "RequiredHours")
            .Write();

            myChart.Save("~/Content/chart/chartimg" , "jpeg");
            // Return the contents of the Stream to the client
            return base.File("~/Content/chart/chartimg", "jpeg");
        }

    }
}
