using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMOS.BAL.Helpers
{
    public class FileHelper
    {
        private static string ApiKey;
        private static string Bucket;
        private static string AuthEmail;
        private static string AuthPassword;

        /*public static void SetCredentials(FireBaseImage fireBaseImage)
        {
            ApiKey = fireBaseImage.ApiKey;
            Bucket = fireBaseImage.Bucket;
            AuthEmail = fireBaseImage.AuthEmail;
            AuthPassword = fireBaseImage.AuthPassword;
        }

        #region Convert IFormFile to FileStream
        public static FileStream ConvertFormFileToStream(IFormFile file)
        {
            // Create a unique temporary file path
            string tempFilePath = Path.GetTempFileName();

            // Open a FileStream to write the file
            using (FileStream stream = new FileStream(tempFilePath, FileMode.Create))
            {
                // Copy the contents of the IFormFile to the FileStream
                file.CopyToAsync(stream).GetAwaiter().GetResult();
            }

            // Open the temporary file in read mode and return the FileStream
            FileStream fileStream = new FileStream(tempFilePath, FileMode.Open, FileAccess.Read);

            return fileStream;
        }
        #endregion

        #region Upload Image
        public static async Task<Tuple<string, string>> UploadImage(FileStream stream, string folder)
        {
            try
            {
                Guid guid = Guid.NewGuid();
                var fileId = guid.ToString();

                var auth = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
                var a = await auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);

                // you can use CancellationTokenSource to cancel the upload midway
                var cancellation = new CancellationTokenSource();

                var task = new FirebaseStorage(
                    Bucket,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                        ThrowOnCancel = true //when you cancel the upload, exception is thrown. By default no exception is thrown
                    })
                    .Child(folder)
                    .Child(fileId)
                    .PutAsync(stream, cancellation.Token);


                // error during upload will be thrown when you await the task
                string link = await task;
                return Tuple.Create(link, fileId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        #region Delete image
        public static async Task DeleteImageAsync(string fileId, string folder)
        {
            try
            {
                var auth = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
                var a = await auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);

                // you can use CancellationTokenSource to cancel the upload midway
                var cancellation = new CancellationTokenSource();

                var task = new FirebaseStorage(
                    Bucket,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                        ThrowOnCancel = true //when you cancel the upload, exception is thrown. By default no exception is thrown
                    })
                    .Child(folder)
                    .Child(fileId)
                    .DeleteAsync();
                await task;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion*/

        #region HaveSupportedFileType
        public static bool HaveSupportedFileType(string fileName)
        {
            string[] validFileTypes = { ".png", ".jpg", ".jpeg" };
            string extensionFile = Path.GetExtension(fileName);
            if (validFileTypes.Contains(extensionFile))
            {
                return true;
            }
            return false;
        }
        #endregion
    }
}
