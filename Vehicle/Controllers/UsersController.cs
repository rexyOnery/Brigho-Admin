using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Vehicle.Models;
using Vehicle.Services;

namespace Vehicle.Controllers
{
    public class UsersController : Controller
    {
        private IWebHostEnvironment _hostingEnvironment;
        Dictionary<string, object> dict = new Dictionary<string, object>();
        public UsersController(IWebHostEnvironment environment)
        {
            _hostingEnvironment = environment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Active()
        {
            return View();
        }

        public IActionResult Details(int? id)
        {
            return View();
        }

        public IActionResult Existing()
        {
            return View();
        }

        public IActionResult Expired()
        {
            return View();
        }

        public IActionResult Search()
        {
            return View();
        }

        public IActionResult Vehicle()
        {
            return View();
        }

         
        public async Task<bool> AddAgent(AgentModel model)
        {
            var _pdf = model.Pix;
            if (_pdf != null)
            { 
                int _counter;
                try { _counter = Server.GetUserData<Agent>().Result.Count() + 1; } catch { _counter = 1; }
                string _pdf_ = AddPDF(_pdf, "agent_" + _counter);
                if (_pdf_.StartsWith("Please") || _pdf_.Equals("error"))
                {

                    return false;
                }
                try
                {
                    Agent _agent = new Agent
                    {
                         FirstName = model.FirstName,
                         LastName = model.LastName,
                         LoginName = model.LoginName,
                         Password = model.Password,
                         Phone = model.Phone,
                         Admin = model.Admin,
                         Photo = _pdf_,
                         Disabled = false

                    }; 
                    var _photo = await Server.AddAgent<Agent>(_agent);
                    if (_photo)
                    {
                        return true;
                    }
                    else
                        return false;
                }
                catch
                {
                    // return error message if there was an exception
                    return false;
                }
            }
            return false;

            
        }

       
        public List<Agent> Login(Agent account)
        {

            return Server.Login<Agent>(account);
        }

        public async Task<List<UserItems>> Searcher(ItemModel model)
        {
            return await Server.SearchAsync<UserItems>(model.TagNumber);
        }


        public async Task<List<UserItems>> AdminSearcher(ItemModel model)
        {
            return await Server.AdminSearchAsync<UserItems>(model.TagNumber);
        }


        public int AddUsers(UserModel model)
        {
            var _pdf = model.Pix;
            if (_pdf != null)
            {
                int _counter;
                try { _counter = Server.GetUserData<Vitsuser>().Result.Count() + 1; } catch { _counter = 1; }
                string _pdf_ = AddPDF(_pdf, "user_" + _counter);
                if (_pdf_.StartsWith("Please") || _pdf_.Equals("error"))
                {

                    return 0;
                }
                try
                {
                    Vitsuser vitsuser = new Vitsuser
                    {
                        Address = model.Address,
                        AgentId = model.AgentId,
                        City = model.City,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Phone = model.Phone,
                        Pix = _pdf_,
                        State = model.State
                    };

                    var _photo = Server.AddUsers<Vitsuser>(vitsuser);
                    if (_photo > 0)
                    {
                        return vitsuser.Id;
                    }
                    else
                        return 0;
                }
                catch
                {
                    // return error message if there was an exception
                    return 0;
                }
            }
            return 0;
        }


        public async Task<int> AddItems(ItemModel model)
        {
            var _pdf = model.Photo;
            if (_pdf != null)
            {
                int _counter;
                try { _counter = Server.GetUserData<Vitsitem>().Result.Count() + 1; } catch { _counter = 1; } 
                string _pdf_ = AddPDF(_pdf, "item_" + _counter);
                if (_pdf_.StartsWith("Please") || _pdf_.Equals("error"))
                {

                    return 0;
                }
                try
                {
                    Vitsitem vitsuser = new Vitsitem
                    {
                        ChasisNumber = model.ChasisNumber,
                        ItemType = model.ItemType,
                        Paid = false,
                        Photo = _pdf_,
                        PlateNumber = model.PlateNumber,
                        RegisteredDate = DateTime.Now.ToLongDateString(),
                        VitsuserId = model.UserId
                    };

                    var _photo = await Server.PostItems<Vitsitem>(vitsuser);
                    if (_photo.Count > 0)
                    {
                        return _counter;
                    }
                    else
                        return 0;
                }
                catch
                {
                    // return error message if there was an exception
                    return 0;
                }
            }
            return 0;
        }

        public async Task<List<UserItems>> GetUserItems(int id)
        {
            return await Server.GetUserItemsAsync<UserItems>(id);
        }

        

        public async Task<List<DashItems>> GetDashBoardInfo(int id)
        {
            return Server.GetDashBoardInfoAsync(id);
        }
        
        public async Task<List<UserItems>> GetAllExistingAgentUsers(int id)
        {
            return Server.GetAllAgentUsersAsync(id);
        }

        public async Task<List<UserItems>> GetAllAgentClients(int id)
        {
            return Server.GetAllAgentClentsAsync(id);
        }
        

        public async Task<List<UserItems>> GetAllUsers()
        {
            return Server.GetAllAgentUsersAsync();
        }

        public async Task<List<UserItems>> GetAllExistingUsers()
        {
            return Server.GetAllUsersAsync();
        }

        public async void RemoveUsers(int id)
        {
            Server.RemoveUsersAsync(id);
        }
        
        public async Task<List<UserItems>> GetAllPendingAgentUsers(int id)
        {
            return Server.GetAllPendingAsync(id);
        }

        public async Task<List<UserItems>> GetAllPendingUsers(int id)
        {
            return Server.GetPendingAsync(id);
        }

        public async Task<List<UserItems>> GetAllInProgressAgentUsers(int id)
        {
            return Server.GetAllAgentUsersAsync(id);
        }
        public async Task<int> SetPayment(int id)
        {
            return await Server.SetPayment(id);
        }

        public bool CashingOut(int AgentId , string Amount)
        {
            return Server.CashingOut(AgentId, Amount);
        }


        

        public string AddPDF(IFormFile idCard, string id)
        {
            try
            {
                var file = idCard;

                if (file.Length > 0)
                {
                    string folderName = "Uploads";
                    string projectRootPath = _hostingEnvironment.ContentRootPath;

                    string webRootPath = _hostingEnvironment.WebRootPath;
                    string newPath = Path.Combine(webRootPath, folderName);
                    if (!Directory.Exists(newPath))
                    {
                        Directory.CreateDirectory(newPath);
                    }

                    IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".png" };
                    var ext = file.FileName.Substring(file.FileName.LastIndexOf('.'));
                    var extension = ext.ToLower();
                    if (!AllowedFileExtensions.Contains(extension))
                    {

                        var message = string.Format("Please Upload image of type .jpg, .png.");

                        return "error";
                    }
                    string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    fileName = id.ToString() + extension;
                    string fullPath = Path.Combine(newPath, fileName);



                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {

                        file.CopyTo(stream);

                    }

                    Stream strm = file.OpenReadStream();

                    return fileName;

                }
                return "error";
            }
            catch (System.Exception ex)
            {
                return "error";
            }
        }

        public static void Compressimage(Stream sourcePath, string targetPath)
        {


            try
            {
                using (var image = Image.FromStream(sourcePath))
                {
                    float maxHeight = 400.0f;
                    float maxWidth = 400.0f;
                    int newWidth;
                    int newHeight;
                    string extension;
                    Bitmap originalBMP = new Bitmap(sourcePath);
                    int originalWidth = originalBMP.Width;
                    int originalHeight = originalBMP.Height;

                    if (originalWidth > maxWidth || originalHeight > maxHeight)
                    {

                        // To preserve the aspect ratio  
                        float ratioX = (float)maxWidth / (float)originalWidth;
                        float ratioY = (float)maxHeight / (float)originalHeight;
                        float ratio = Math.Min(ratioX, ratioY);
                        newWidth = (int)(originalWidth * ratio);
                        newHeight = (int)(originalHeight * ratio);
                    }
                    else
                    {
                        newWidth = (int)originalWidth;
                        newHeight = (int)originalHeight;

                    }
                    Bitmap bitMAP1 = new Bitmap(originalBMP, newWidth, newHeight);
                    Graphics imgGraph = Graphics.FromImage(bitMAP1);
                    extension = Path.GetExtension(targetPath);
                    if (extension == ".png" || extension == ".gif")
                    {
                        imgGraph.SmoothingMode = SmoothingMode.AntiAlias;
                        imgGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        imgGraph.DrawImage(originalBMP, 0, 0, newWidth, newHeight);


                        bitMAP1.Save(targetPath, image.RawFormat);

                        bitMAP1.Dispose();
                        imgGraph.Dispose();
                        originalBMP.Dispose();
                    }
                    else if (extension == ".jpg")
                    {

                        imgGraph.SmoothingMode = SmoothingMode.AntiAlias;
                        imgGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        imgGraph.DrawImage(originalBMP, 0, 0, newWidth, newHeight);
                        ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);
                        Encoder myEncoder = Encoder.Quality;
                        EncoderParameters myEncoderParameters = new EncoderParameters(1);
                        EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 50L);
                        myEncoderParameters.Param[0] = myEncoderParameter;
                        bitMAP1.Save(targetPath, jpgEncoder, myEncoderParameters);

                        bitMAP1.Dispose();
                        imgGraph.Dispose();
                        originalBMP.Dispose();

                    }


                }

            }
            catch (Exception)
            {
                throw;

            }
        }

        public static ImageCodecInfo GetEncoder(ImageFormat format)
        {

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
    }
}
