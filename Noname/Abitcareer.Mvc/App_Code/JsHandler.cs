using CultureEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

public class JsHandler : IHttpHandler
{
    private delegate string OperationDelegate(string param, System.Web.Routing.RequestContext context);
    private static Dictionary<string, OperationDelegate> operations;

    static JsHandler( )
    {
        operations =  new Dictionary<string, OperationDelegate>
        {
            { "Route", (name, context)=>{
                var Url = new UrlHelper(context);
                return Url.RouteUrl(name, new { locale = System.Threading.Thread.CurrentThread.CurrentCulture.Name });
            }},
            { "Resx", (name, context)=>{
                return LocalizationResx.ResourceManager.GetString(name, LocalizationResx.Culture);
            }}
        };
    }

    public void ProcessRequest(HttpContext context)
    {
        var request = context.Request;
        var response = context.Response;
        var server = context.Server;
        var lang = request.Cookies["lang"];
        if (lang != null && !string.IsNullOrEmpty(lang.Value))
        {
            CEngine.Instance.SetCultureForThread(lang.Value);
        }
        string pattern = @"\[([A-Z]\w+):([A-z]\w+)\]";
        using (var reader = new StreamReader(server.MapPath(Path.Combine("./", request.FilePath.Replace("handl", "Scripts")))))
        {
            var str = reader.ReadToEnd();
            foreach (Match match in Regex.Matches(str, pattern))
            {
                try
                {
                    string op = match.Groups[1].Value;
                    if (operations.ContainsKey(op))
                    {
                        var newValue = operations[op](match.Groups[2].Value, request.RequestContext);
                        str = str.Replace(match.Value, newValue);
                    }
                } catch (ArgumentException ex)
                {
                }
            }
            response.Write(str);
        }
    }

    public bool IsReusable
    {
        get { return true; }
    }
}