using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Text;


namespace yandexapi
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var wb = new WebClient())
            {
                var reqData = new NameValueCollection();
                reqData["text"] = "Hello World"; // text to translate
                reqData["lang"] = "tr"; // target language
                reqData["key"] = "YOUR YANDEX API KEY";

                try
                {
                    var response = wb.UploadValues("https://translate.yandex.net/api/v1.5/tr.json/translate", "POST", reqData);
                    string responseInString = Encoding.UTF8.GetString(response);

                    var rootObject = JsonConvert.DeserializeObject<Translation>(responseInString);
                    Console.WriteLine($"Original text: {reqData["text"]}\n" +
                        $"Translated text: {rootObject.text[0]}\n" +
                        $"Lang: {rootObject.lang}");

                    Console.ReadLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR!!! " + ex.Message);
                    throw;
                }
 
            }
        }
    }

    public class Translation
    {
        public int code { get; set; }
        public string lang { get; set; }
        public List<string> text { get; set; }
    }
}
