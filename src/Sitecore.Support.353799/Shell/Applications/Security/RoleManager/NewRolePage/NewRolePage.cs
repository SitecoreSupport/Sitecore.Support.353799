using Sitecore.Controls;
using Sitecore.Diagnostics;
using Sitecore.Security;
using Sitecore.Security.Domains;
using Sitecore.StringExtensions;
using Sitecore.Web.UI.Sheer;
using Sitecore.Web.UI.XamlSharp.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;

namespace Sitecore.Support.Shell.Applications.Security.RoleManager
{
    public class NewRolePage: DialogPage
    {

        protected DropDownList Domain;
        protected TextBox Name;

        protected override void OK_Click()
        {
            string text = this.Name.Text;
            if (!IsValidRoleName(ref text))
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

        private bool IsValidRoleName(ref string param)
        {
            if (param == null)
            {
                return false;
            }
            param = param.Trim();
            return ((param.Length >= 1) ? Regex.IsMatch(param, @"^[a-zA-Z0-9_\s\&]*$") : false);

        }

        protected override void OnLoad(EventArgs e)
        {
            Assert.ArgumentNotNull(e, "e");
            Assert.CanRunApplication("/sitecore/content/Applications/Security/Role Manager");
            base.OnLoad(e);
            if (!XamlControl.AjaxScriptManager.IsEvent)
            {
                foreach (Domain domain in Sitecore.Context.User.Delegation.GetManagedDomains())
                {
                    ListItem item = new ListItem(domain.Name, domain.Name);
                    this.Domain.Items.Add(item);
                    if (domain.Name.Equals("sitecore", StringComparison.InvariantCultureIgnoreCase))
                    {
                        item.Selected = true;
                    }
                }
            }
        }
    }

    
}