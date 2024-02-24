using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
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
    [Route("api/[controller]")]
    [ApiController]
    public class VitsController : ControllerBase
    {
        [Obsolete]
        private IHostingEnvironment _hostingEnvironment;
        Dictionary<string, object> dict = new Dictionary<string, object>();
        private readonly IFileProvider _fileProvider;

        [Obsolete]
        public VitsController(IHostingEnvironment hostingEnvironment)
        {
            _fileProvider = hostingEnvironment.WebRootFileProvider;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        [Route("addusers")]
        [Obsolete]
        public async Task<bool> AddUser(UserModel user)
        {
            try
            {
                var file = Request.Form.Files[0];

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

                        return false;
                    }
                    string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    RandomGenerator generator = new RandomGenerator();
                    string _tag = generator.RandomString(8, true);
                    fileName = _tag + extension;// + fileName;
                    string fullPath = Path.Combine(newPath, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    Stream strm = file.OpenReadStream();
                    Compressimage(strm, fullPath);

                    try
                    {
                        Vitsuser vitsuser = new Vitsuser
                        {
                            Address = user.Address,
                            AgentId = user.AgentId,
                            City = user.City,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Phone = user.Phone,
                            Pix = fileName,
                            State = user.State
                        };

                        var _photo = Server.AddUsers<Vitsuser>(vitsuser);
                        if (_photo > 0)
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
            catch
            {
                return false;
            }

        }

        public static void Compressimage(Stream sourcePath, string targetPath)
        {


            try
            {
                using (var image = Image.FromStream(sourcePath))
                {
                    float maxWidth = 300.0f;
                    float maxHeight = 466.0f;
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

        [HttpGet]
        [Route("additems"), DisableRequestSizeLimit]
        [Obsolete]
        public async Task<bool> AddItem(UserModel user)
        {
            return true;
        }

         
    }
}
