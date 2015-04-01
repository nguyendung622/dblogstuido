using BroastCastMessageProject.DAL;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BroastCastMessageProject
{
    public partial class BroastCastMessageWebform : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        bool ValidationData()
        {
            if (string.IsNullOrEmpty(txtMessage.Text))
                return false;
            if (txtMessage.Text.Length > 100)
                return false;
            return true;
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            if (!ValidationData())
            {
                lblNotification.Text = "Vui lòng kiểm tra lại dữ liệu. Dữ liệu không được để trống hoặc lớn 100 ký tự";
                return;
            }
            else
                lblNotification.Text = "";
            using (var db = new BroastCastMessageDBEntities())
            {
                string messageContent = txtMessage.Text;
                //Lưu tin nhắn vào cơ sở dữ liệu
                BCMMessage message = new BCMMessage { DateCreate = DateTime.Now, Enabled = true, Message = messageContent };
                db.BCMMessages.Add(message);
                db.SaveChanges();
                lblNotification.Text = "Thông báo! Lưu tin nhắn vào CSDL thành công";

                //Gởi tin nhắn tới GCM Server
                //lấy danh sách Registration Id
                string[] arrRegid = db.BCMRegistrations.Where(c => c.Enabled == true).Select(c => c.RegistrationID).ToArray();
                String collaspeKey = Guid.NewGuid().ToString("n");
                string gcmSenderID = "284219605451";
                string gcmAppID = "AIzaSyDvMYI_-Jop0kDZZJ18xMYWc4SeCdpTINU";
                WebRequest tRequest;
                //thiết lập GCM send
                tRequest = WebRequest.Create("https://android.googleapis.com/gcm/send");
                tRequest.Method = "POST";
                tRequest.UseDefaultCredentials = true;
                tRequest.PreAuthenticate = true;
                tRequest.Credentials = CredentialCache.DefaultNetworkCredentials;
                //định dạng JSON
                tRequest.ContentType = "application/json";
                //tRequest.ContentType = " application/x-www-form-urlencoded;charset=UTF-8";
                tRequest.Headers.Add(string.Format("Authorization: key={0}", gcmAppID));

                tRequest.Headers.Add(string.Format("Sender: id={0}", gcmSenderID));

                string RegArr = string.Empty;

                RegArr = string.Join("\",\"", arrRegid);
                //Post Data có định dạng JSON như sau:
                /*
                *  { "collapse_key": "score_update",     "time_to_live": 108,       "delay_while_idle": true,
                "data": {
                "score": "223/3",
                "time": "14:13.2252"
                },
                "registration_ids":["dh4dhdfh", "dfhjj8", "gjgj", "fdhfdjgfj", "đfjdfj25", "dhdfdj38"]
                }
                */
                string postData = "{ \"registration_ids\": [ \"" + RegArr + "\" ],\"data\": {\"message\": \"" + messageContent + "\",\"collapse_key\":\"" + collaspeKey + "\"}}";

                Console.WriteLine(postData);
                Byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                tRequest.ContentLength = byteArray.Length;
                Stream dataStream = tRequest.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                WebResponse tResponse = tRequest.GetResponse();
                dataStream = tResponse.GetResponseStream();
                StreamReader tReader = new StreamReader(dataStream);
                String sResponseFromServer = tReader.ReadToEnd();
                lblNotification.Text = sResponseFromServer; //Lấy thông báo kết quả từ GCM server.
                tReader.Close();
                dataStream.Close();
                tResponse.Close();
            }
        }
    }
}