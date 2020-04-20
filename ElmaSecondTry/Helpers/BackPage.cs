using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElmaSecondTry.Helpers
{
    public static class BackPageRememberer<TEntity> where TEntity : class
    {
        public static string BackView { get; set; }
        public static TEntity BackModel { get; set; } 

        public static void SetBack(string view, TEntity model)
        {
            BackView = view;
            BackModel = model;
        }

        public static (string GoalView, TEntity GoalModel) GetBack()
        {
            return (BackView, BackModel);
        }
    }
}