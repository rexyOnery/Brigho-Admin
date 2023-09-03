using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Vehicle.Models;

namespace Vehicle.Services
{
    public class Server
    {
        internal static async Task<bool> AddUser<T>(Login account) where T : class
        {
            using (var _context = new DB_A5DE44_HoldingContext())
            {
                var user = _context.Logins
                    .Where(c => c.LoginName == account.LoginName);

                if (user.Count() == 0)
                {
                    string passwordHash;
                    PasswordHashing.CreatePasswordHash(account.Password, out passwordHash);
                    account.Password = passwordHash;
                    _context.Logins.Add(account);
                    await _context.SaveChangesAsync();

                    return true;
                }

                return false;

            }
        }

        
        internal static bool ProcessPay(string trans)
        {

            RandomGenerator randomGenerator = new RandomGenerator();
            bool _confirmed = false;
            using (var _context = new DB_A5DE44_HoldingContext())
            {
                var _buyerPaid = _context.Ahhttransactions
                    .FirstOrDefault(c => c.TransactionCode == trans);

                RandomGenerator rnd = new RandomGenerator();
                if (_buyerPaid != null)
                {
                    string confirmation_code = rnd.RandomNumber(1000, 9999).ToString();
                    confirmation_code = _buyerPaid.Id + "" + confirmation_code;

                    _buyerPaid.ConfirmationCode = confirmation_code.Length > 6 ? confirmation_code.Substring(0, 6) : confirmation_code;
                    _buyerPaid.Paid = true;
                    _buyerPaid.PaymentDate = randomGenerator.Month(DateTime.Now.Month) + " " + DateTime.Now.Day + ", " + DateTime.Now.Year;
                    _buyerPaid.Reasons = "Pending: Buyer has made payment. Kindly send item(s) to buyer using Delivery Number: " + _buyerPaid.ConfirmationCode;
                    //_buyerPaid.TransactionDate = rnd.Month(DateTime.Now.Month) + " " + DateTime.Now.Day + ", " + DateTime.Now.Year + " " + DateTime.Now.ToLongTimeString();
                    _context.Ahhttransactions.Update(_buyerPaid);

                    _context.SaveChanges();
                    _confirmed = true;

                    //string message_ = "Your Brigho Transaction with the Number: " + _buyerPaid.SellertTransactionCode + " was accepted and payment made. Kindly send the item(s) to the buyer. Buyer Delivery Number is: "+_buyerPaid.ConfirmationCode;
                    string message_ = "Brigho transaction with number " + _buyerPaid.SellertTransactionCode + " has been paid for by the Buyer. Kindly send item(s) to Buyer using Delivery Number " + _buyerPaid.ConfirmationCode;
                    SendMessageBySMS(_buyerPaid.SellerMobileNumber, message_);

                    string buyerMsg = "Your payment of N" + _buyerPaid.TotalCost + " has been received. And the Seller notified";

                    // SendMessageBySMS(_buyerPaid.BuyerMobileNumber, message_);

                }

                return _confirmed;
            }
        }


        static string _message_out = "";
        private static void SendMessageBySMS(string phoneNumber, string message_)
        {
            string admin_message = message_;
            string phone = phoneNumber;

            //string url = "https://www.bulksmsnigeria.com/api/v1/sms/create?api_token=z8JT6uJ5rPksUGLBWzGs6KUTa5JFttdT4qIyYjnsIrvg4N2uPklYhK9DNpxy&from=EXCELLENTUT&to=" + phone +"& body="+message + "&dnd=1"; 

            string sender = "BRIGHO";
            string to = phone;
            string token = "f7DpAIbD3BDtlLkOEONdkT0R1bCdqYtVy4gakVH4cPF8edvA4NoVfz3ePs7lDITAB8NCFaDvugyFwTncpCj6mCDwIWN0K7Ckulk7";
            int routing = 3;
            int type = 0;
            string baseurl = "https://smartsmssolutions.com/api/json.php?";
            string url = baseurl + "message=" + admin_message + "&to=" + to + "&sender=" + sender + "&type=" + type + "&routing=" + routing + "&token=" + token;

            HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(string.Format(url));
            webReq.Method = "GET";
            HttpWebResponse webResponse = (HttpWebResponse)webReq.GetResponse();
            Stream answer = webResponse.GetResponseStream();
            StreamReader _recivedAnswer = new StreamReader(answer);
            var ans = _recivedAnswer.ReadToEnd();




            //var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://api.bulksmslive.com/v2/app/sms");
            //httpWebRequest.ContentType = "application/json";
            //httpWebRequest.Method = "POST";
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            //using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            //{
            //    string email = "ovedeotite@gmail.com";
            //    string password = "Stefny101";
            //    string message = message_;
            //    string sender_name = "BRIGHO";
            //    string recipients = phoneNumber;
            //    string forcednd = "1";
            //    string json = "{\"email\":\"" + email + "\",\"password\":\"" + password + "\",\"message\":\"" + message + "\",\"sender_name\":\"" + sender_name + "\",\"recipients\":\"" + recipients + "\",\"forcednd\":\"" + forcednd + "\"}";
            //    streamWriter.Write(json);
            //    streamWriter.Flush();
            //    streamWriter.Close();
            //}

            //var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            //using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            //{
            //    var result = streamReader.ReadToEnd();
            //    Console.WriteLine(result);
            //    _message_out = result;
            //}
        }


        internal static async Task<List<Account>> Login<T>(Account account) where T : class
        {
            using (var _context = new DB_A5DE44_HoldingContext())
            {
                var _user = _context.Logins.FirstOrDefault(c => c.LoginName.Equals(account.Username));

                List<Account> accounts = new List<Account>();
                if (!PasswordHashing.VerifyPasswordHash(account.Password, _user.Password))
                {
                    Account acc = new Account
                    {
                        Username = "No Login"
                    };
                    accounts.Add(acc);
                    return accounts;
                }
                else
                {
                    Account acc = new Account
                    {
                        RecoveryCode = _user.FirstName + " " + _user.LastName,
                        Username = _user.LoginName,
                        UserType = _user.UserType
                    };
                    accounts.Add(acc);
                    return accounts;

                }
            }
        }

        internal static async Task<T> FindById<T>(int id) where T : class
        {
            using (var _context = new DB_A5DE44_HoldingContext())
            {
                return await _context.Set<T>().FindAsync(id);
            }
        }

        internal static async Task<List<T>> GetUserData<T>() where T : class
        {
            using (var _context = new DB_A5DE44_HoldingContext())
            {
                return await _context.Set<T>().ToListAsync();
            }
        }


        internal static string GetAccountLoginData<T>(string email) where T : class
        {
            using (var _context = new DB_A5DE44_HoldingContext())
            {
                var _data = _context.Logins.FirstOrDefault(c => c.Phone.Equals(email));
                if (_data != null)
                {
                    Random random = new Random();
                    string code = random.Next(1000, 9999).ToString();
                    _data.RecoveryCode = code;
                    _context.Logins.Update(_data);
                    _context.SaveChanges();

                    return code;
                }
                    return "";
            }
        }

        internal static bool GetConfirmedCode(string code, string email)
        {
            var _data = GetUserData<Login>().Result.Where(c => c.Phone.Equals(email) && c.RecoveryCode.Equals(code)) ;
            if (_data != null)
            {
                 
                    return true;
                 
            }
            return false;
        }



        internal static List<Ahhttransaction> GetCurrentDasboad<T>() where T : class
        {
            using (var _context = new DB_A5DE44_HoldingContext())
            {
                var _trns = _context.Ahhttransactions
                    .OrderByDescending(c => c.Id)
                    .ToList();

                return _trns;
            }
        }

        internal static string GetTotalCash<T>() where T : class
        {
            using (var _context = new DB_A5DE44_HoldingContext())
            {
                var _trns = _context.Ahhttransactions
                    .Sum(c => Convert.ToInt32(c.TotalCost));

                return Convert.ToInt32(_trns * 2000).ToString();
            }
        }


        internal static List<Ahhttransaction> GetNotPaidSevenDays<T>() where T : class
        {
            using (var _context = new DB_A5DE44_HoldingContext())
            {
                var _trns = _context.Ahhttransactions
                    .Where(c => c.Paid == false && (Convert.ToDateTime(c.TransactionDate).AddDays(7) > DateTime.Now))
                    .OrderByDescending(c => c.Id)
                    .ToList();

                return _trns;
            }
        }

        internal static List<Ahhttransaction> GetPrint<T>() where T : class
        {
            using (var _context = new DB_A5DE44_HoldingContext())
            {
                var _trns = _context.Ahhttransactions
                    .OrderByDescending(c => c.Id)
                    .ToList();

                return _trns;
            }
        }

        internal static void Process<T>(int id, string processby) where T : class
        {
            using (var _context = new DB_A5DE44_HoldingContext())
            {
                var _pro = _context.Ahhttransactions.Find(id);
                _pro.Processed = true;
                _pro.RefundDate = Month(DateTime.Now.Month) + " " + DateTime.Now.Day + ", " + DateTime.Now.Year;
                _pro.ProcessedBy = "Processed By " + processby;
                _context.SaveChanges();
            }
        }

        private static string Month(int m)
        {
            switch (m)
            {
                case 1: return "Jan";
                case 2: return "Feb";
                case 3: return "Mar";
                case 4: return "Apr";
                case 5: return "May";
                case 6: return "Jun";
                case 7: return "Jul";
                case 8: return "Aug";
                case 9: return "Sep";
                case 10: return "Oct";
                case 11: return "Nov";
                case 12: return "Dec";
                default: return "";
            }
        }

        internal static void Completed<T>(int id, string processby) where T : class
        {
            using (var _context = new DB_A5DE44_HoldingContext())
            {
                var _pro = _context.Ahhttransactions.Find(id);
                _pro.ProcessedBy = "Completed By " + processby;
                _pro.Completed = true;
                _context.SaveChanges();
            }
        }

        internal static void Delete<T>(int id, string processby) where T : class
        {
            using (var _context = new DB_A5DE44_HoldingContext())
            {
                var _pro = _context.Ahhttransactions.Find(id);
                _pro.Deleted = true;
                _pro.Processed = true;
                _pro.ProcessedBy = "Deleted By " + processby;
                _context.SaveChanges();
            }
        }

        public static string sendForgotSMS(int id)
        {
            using (var _context = new DB_A5DE44_HoldingContext())
            {
                var user = _context.Logins.FirstOrDefault(c => c.Id.Equals(id));

                RandomGenerator generator = new RandomGenerator();
                string str = generator.RandomString(10, false);

                //string msg = "Your Brigho password recovery code: " + str;
                //SendMessageBySMS(user.Phone, msg);
                if (user != null)
                {
                    try
                    {

                        user.RecoveryCode = str;

                        _context.Logins.Update(user);
                        _context.SaveChanges();

                        return str;
                    }

                    catch (Exception e)
                    {

                    }
                }
                return "";
            }
        }

        internal static bool ChangePassword(string password, string email)
        {
            using (var _context = new DB_A5DE44_HoldingContext())
            {
                var user = _context.Logins
                    .Where(c => c.Phone == email);

                if (user.Count() > 0)
                {
                    string passwordHash;
                    PasswordHashing.CreatePasswordHash(password, out passwordHash);
                    user.FirstOrDefault().Password = passwordHash;
                    user.FirstOrDefault().RecoveryCode = "-";
                    _context.SaveChanges();

                    return true;
                }

                return false;

            }
        }

        internal static bool SendCode2Email(RecoverMailModel model)
        {
            MailMessage m = new MailMessage();
            SmtpClient sc = new SmtpClient();
            m.From = new MailAddress("contact@brigho.com");
            m.To.Add(model.EmailAddress.ToLower());
            m.Subject = model.Title;
            m.Body = model.MessageContent;
            m.IsBodyHtml = true;
            sc.Host = "mail.brigho.com";
            string str1 = "gmail.com";
            string str2 = model.EmailAddress.ToLower();
            if (str2.Contains(str1))
            {
                try
                {
                    sc.Port = 8889;
                    sc.Credentials = new System.Net.NetworkCredential("contact@brigho.com", "Stefny101.Brigho2021");
                    sc.EnableSsl = false;
                    sc.Send(m);
                    return true;
                }
                catch (Exception e)
                {
                    var err = e;
                    return false;
                }
            }
            else
            {
                try
                {
                    sc.Port = 25;
                    sc.Credentials = new System.Net.NetworkCredential("contact@brigho.com", "Stefny101.Brigho2021");
                    sc.EnableSsl = false;
                    sc.Send(m);
                    return true;
                }
                catch (Exception e)
                {
                    var s = e.ToString();
                    return false;
                }
            }
        }
        // private static Vitsitem TagDTO(Vitsitem user) =>
        //new Vitsitem
        //{
        //    ChasisNumber = user.ChasisNumber,
        //    ItemType = user.ItemType,
        //    Photo = user.Photo,
        //    PlateNumber = user.PlateNumber,
        //    TagNumber = user.TagNumber
        //};
    }
}
