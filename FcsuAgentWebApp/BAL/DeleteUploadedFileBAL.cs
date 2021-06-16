using FcsuAgentWebApp.DAL;
using FcsuAgentWebApp.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FcsuAgentWebApp.BAL
{
    public class DeleteUploadedFileBAL
    {
        public List<FileDirModel> getFilesDirectory()
        {
            DeleteUploadedFileDAL delUploadedFileDAL = new DeleteUploadedFileDAL();
            return delUploadedFileDAL.getFilesDirectory();
        }

        public int editFilesDirectory(int year, int month, int filesdir_i)
        {
            DeleteUploadedFileDAL delUploadedFileDAL = new DeleteUploadedFileDAL();
            return delUploadedFileDAL.editFilesDirectory(year, month, filesdir_i);
        }
    }
}