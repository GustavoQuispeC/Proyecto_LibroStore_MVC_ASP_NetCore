using AppStore.Repositories.Abstract;

namespace AppStore.Repositories.Implementation
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment environment;
        public FileService(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }
       
        
        public Tuple<int, string> SaveImage(IFormFile imageFile)
        {
            try
            {
                var wwwPath = this.environment.WebRootPath;
                var path = Path.Combine(wwwPath, "Uploads");
                if(!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                var ext = Path.GetExtension(imageFile.FileName);
                var allowedExtension = new string[]{".jpg", ".jpeg", ".png"};
                if(!allowedExtension.Contains(ext))
                {
                    return new Tuple<int, string>(0, "Solo se permiten archivos jpg, jpeg y png");
                }
                var uniqueString = Guid.NewGuid().ToString();
                var newFileName = uniqueString + ext;
                var fileWithPath = Path.Combine(path, newFileName);

                var stream = new FileStream(fileWithPath, FileMode.Create);
                imageFile.CopyTo(stream);
                stream.Close();
                
                return new Tuple<int, string>(1, newFileName);
            }
            catch(Exception)
            {
                return new Tuple<int, string>(0, "Errores guardando la imagen");
            }
            
        }

        public bool DeleteImage(string imageFileName)
        {
            try
            {
                var wwwPath = environment.WebRootPath;
                var path = Path.Combine(wwwPath, "Uploads\\", imageFileName);

                if(File.Exists(path))
                {
                    File.Delete(path);
                    return true;
                }
               
                return false;
            }
            catch(Exception)
            {
                return false;
            }
            
        }

    }
}