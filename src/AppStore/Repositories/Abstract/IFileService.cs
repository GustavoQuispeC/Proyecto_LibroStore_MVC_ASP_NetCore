using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppStore.Repositories.Abstract
{
    public interface IFileService
    {
        public Tuple<int, string>SaveImage(IFormFile imageFile);
        public bool DeleteImage(string imageFileName);
    }
}