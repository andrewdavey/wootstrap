﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Wootstrap.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    using System.Web.WebPages.Html;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorSingleFileGenerator", "1.0")]
    public class Google : System.Web.WebPages.HelperPage
    {

public static System.Web.WebPages.HelperResult Analytics(string accountId) {
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {


WriteLiteralTo(@__razor_helper_writer, "\r\n<script type=\"text/javascript\">\r\n  var _gaq = _gaq || [];\r\n  _gaq.push([\'_setAc" +
"count\', \'");


WriteTo(@__razor_helper_writer, accountId);

WriteLiteralTo(@__razor_helper_writer, @"']);
  _gaq.push(['_trackPageview']);

  (function() {
    var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
    ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
    var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
  })();
</script>
");


});

}


        protected static System.Web.HttpApplication ApplicationInstance
        {
            get
            {
                return ((System.Web.HttpApplication)(Context.ApplicationInstance));
            }
        }
    }
}