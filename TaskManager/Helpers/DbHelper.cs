using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskManager.DbModel;

namespace TaskManager.Helpers
{
    public class DbHelper
    {
        public static List<Task> GetAllTasks()
        {
            using (var db = new TaskManagerEntities())
            {
                return db.Tasks.ToList();
            }
        }
        public static void UpdateTask(Task task)
        {
            using (var db = new TaskManagerEntities())
            {
                var dbTask = db.Tasks.FirstOrDefault(x => x.TaskId == task.TaskId);
                dbTask.Title = task.Title;
                dbTask.DueDate = task.DueDate;
                dbTask.Priority = task.Priority;
                dbTask.RequiredHours = task.RequiredHours;
                db.SaveChanges();
            }
        }
        public static void DeleteTask(Task task)
        {
            using (var db = new TaskManagerEntities())
            {
                var dbTask = db.Tasks.FirstOrDefault(x => x.TaskId == task.TaskId);
                db.Tasks.Remove(dbTask);
                db.SaveChanges();
            }
        }
    }
}