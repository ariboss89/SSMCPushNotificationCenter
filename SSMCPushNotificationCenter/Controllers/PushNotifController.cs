using Newtonsoft.Json;
using SSMCPushNotificationCenter.Models;
using SSMCPushNotificationCenter.Repositories;
using SSMCPushNotificationCenter.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SSMCPushNotificationCenter.Controllers
{
    public class PushNotifController : Controller
    {
        ApiService ap = new ApiService();
        HttpClient httpClient = new HttpClient();
        HttpResponseMessage response = new HttpResponseMessage();
        tb_notification myNotif = new tb_notification();

        public async Task<ActionResult> Index()
        {
            using (var client = new HttpClient())
            {
                response = httpClient.GetAsync(ap.GetData()).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    myNotif = JsonConvert.DeserializeObject<tb_notification>(result);

                }

                return View(myNotif);
            }
        }  

        public async Task<ActionResult> Edit(int id)
        {
            tb_notification notif = null;

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(ap.Url());

                var responseTask = client.GetAsync(ap.GetDataById(id));
                responseTask.Wait();


                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<tb_notification>();
                    readTask.Wait();

                    notif = readTask.Result;
                }
            }
            
            return View(notif);
        }
        
        [HttpPost]
        public ActionResult Edit(tb_notification notif)
        {
            using (var client = new HttpClient())
            {
                if (ModelState.IsValid)
                {
                    client.BaseAddress = new Uri(ap.Url());

                    var putTask = client.PutAsJsonAsync<tb_notification>(ap.PutNotification(), notif);
                    putTask.Wait();

                    var result = putTask.Result;
                    if (result.IsSuccessStatusCode)
                    {

                        return RedirectToAction("Index");
                    }
                }
            }
            return View(notif);
        }

        [HttpGet]
        public ActionResult Post()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Post(tb_notification notif)
        {
            using (var client = new HttpClient())
            {
                HttpPostedFileBase file = Request.Files["ImageData"];
                ContentRepository service = new ContentRepository();
                int i = service.UploadImageInDataBase(file, notif);

                if (i == 1)
                {
                    client.BaseAddress = new Uri(ap.Url());

                    var postTask = client.PostAsJsonAsync<tb_notification>(ap.PostNotification(), notif);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {

                        return RedirectToAction("Index");
                    }
                }

                else
                {
                    return View(notif);
                }
            }
            return View(notif);
        }
    }
}