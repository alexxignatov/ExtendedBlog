using ExtendedBlog.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExtendedBlog.WebUI.Binder
{
    public class TagModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var tag = (Tag)base.BindModel(controllerContext, bindingContext);

            string idStr = bindingContext.ValueProvider.GetValue("id").AttemptedValue;

            int tagId;
            if (Int32.TryParse(idStr, out tagId))
                tag.TagId = tagId;

            return tag;
        }
    }
}