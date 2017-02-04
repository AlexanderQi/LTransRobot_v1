using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Web.Security;
using System.IO;
using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Windows.Forms;


namespace LTransDeal_v1
{
    public class LTransDeal
    {

        private static ILog log = LogManager.GetLogger("log");


        private static string ini_url = @"http://api.fanyi.baidu.com/api/trans/vip/translate?";

        public static string langfrom = "en";
        public static string langto = "zh";
        public static string appid = "20170203000038505";
        public static string key = "ixhdMk7XCe6dcXZUstrX";
        public static string salt = "1702032153";

        public static string sign = "";
        public static string getSign(string info)
        {
            //拼接appid=2015063000000001+q=apple+salt=1435660288+密钥=12345678
            //得到字符串1 = 2015063000000001apple143566028812345678
            //api.fanyi.baidu.com/api/trans/vip/translate?q=apple&from=en&to=zh&appid=2015063000000001&salt=1435660288&sign=f89f9594663708c1605f3d736d01d2d4
            string orgin = appid + info + salt + key;
            sign = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(orgin, "MD5").ToLower();
            return sign;
        }
        public static string getTransUrl(string info)
        {    
            getSign(info);
            StringBuilder sb = new StringBuilder();
            sb.Append(ini_url).Append("q=").Append(info).Append("&from=").Append(langfrom).Append("&to=").Append(langto).Append("&appid=").Append(appid)
                .Append("&salt=").Append(salt).Append("&sign=").Append(sign);
            return sb.ToString();
        }

        public static string url = "";
        public static string getTransResult(string info)
        {
            url = getTransUrl(info);
            //log.Debug("RequestUrl = "+url);
            try
            {
                WebRequest request = WebRequest.Create(url);
                request.Method = "GET";
                WebResponse response = request.GetResponse();
                System.IO.Stream stream = response.GetResponseStream();
                string r;
                using (StreamReader sr = new StreamReader(stream, Encoding.UTF8))
                {
                    r = sr.ReadToEnd();
                }
                //{ "from":"en","to":"zh","trans_result":[{"src":"you are welcome.","dst":"\u4e0d\u5ba2\u6c14."}]}

                JObject jo = (JObject)JsonConvert.DeserializeObject(r);
                JArray ja = (JArray)jo["trans_result"];
                JToken jt = ja[0]["dst"];
                if (jt != null)
                {
                    string result = jt.ToString();
                    result = System.Web.HttpUtility.UrlDecode(result, Encoding.UTF8);
                    return result;
                }
                else
                    return null;
            
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return ex.Message + " info: "+info;
            }

        }

    }
}
