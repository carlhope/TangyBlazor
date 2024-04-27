using Microsoft.AspNetCore.Components.Forms;

namespace TangyWeb_Server.Service.IService
{
    public interface IFileUpload
    {
        Task<String> UploadFile(IBrowserFile file);

        bool DeleteFile(string filePath);
    }
}
