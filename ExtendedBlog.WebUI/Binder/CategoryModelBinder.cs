using ExtendedBlog.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExtendedBlog.WebUI.Binder
{
    public class CategoryModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            Category category = (Category)base.BindModel(controllerContext, bindingContext);

            string idStr = bindingContext.ValueProvider.GetValue("id").AttemptedValue;

            int id;
            if (Int32.TryParse(idStr, out id))
                category.CategoryId = id;

            return category;
        }
    }
}