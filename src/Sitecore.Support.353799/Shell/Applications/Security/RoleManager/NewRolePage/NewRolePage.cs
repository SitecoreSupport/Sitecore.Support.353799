using Sitecore.Security;
using Sitecore.StringExtensions;
using Sitecore.Web.UI.Sheer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Support.Shell.Applications.Security.RoleManager.NewRolePage
{
    public class NewRolePage: Sitecore.Shell.Applications.Security.RoleManager.NewRolePage
    {
        protected override void OK_Click()
        {
            string text = this.Name.Text;
            if (!SecurityUtil.IsValidRoleName(ref text))
            {
                string[] arguments = new string[] { text };
                SheerResponse.Alert("The role name \"{0}\" contains illegal characters.\n\nThe role name can only contain the following characters: A-Z, a-z, 0-9, ampersand and space.", arguments);
            }
            else
            {
                object[] parameters = new object[] { this.Domain.SelectedValue, text };
                SheerResponse.SetDialogValue(@"{0}\{1}".FormatWith(parameters));
                base.OK_Click();
            }
        }


    }
}