using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.DbModel;
using TaskManager.Models.Enums;

namespace TaskManager.Models
{
    public class TaskModel
    {
        public Task Task { get; set; }
        public bool IsModified { get; set; }
        public string ExDisplayNo { get; set; }
        public bool IsDleted { get; set; }

        public List<SelectListItem> PriorityList
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
                        
                        Selected = (int)Enum.Parse(typeof(Priorities), data.ToString()) == this.Task.Priority
                    });
                }

                return enumList;
            }
        }
    }
}