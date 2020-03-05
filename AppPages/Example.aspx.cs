using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AppPages_Example : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        wucText_Edit1.SetDefinedValidator(Text_EditField.ExpressionTypeEnum.Number);
    }
}