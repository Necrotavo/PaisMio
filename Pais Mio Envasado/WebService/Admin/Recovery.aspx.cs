using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebService.Admin
{
    public partial class Recovery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
               
                string token = Request.QueryString["token"];
                if (!token.Equals("")) {
                    BL_Operario BLoperario = new BL_Operario();
                    BLoperario.enviarNuevaContrasena(token);
                }
            }
        }
    }
}