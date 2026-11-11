using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ByNoa
{
    public class FilePDF
    {
        public string path { get; set; }
        public string name { get; set; }
        public FileType type { get; set; }
        public Appointment appointment { get; set; }
        public FinancialTransaction transaction { get; set; }
        public bool is_new { get; set; }

        public FilePDF(string path, string name, FileType type, Appointment appointment, FinancialTransaction transaction, bool is_new)
        {
            this.path = path;
            this.name = name;
            this.type = type;
            this.appointment = appointment;
            this.transaction = transaction;
            if (transaction != null) { transaction.AddPdf(this); }
            this.is_new = is_new;

            if (is_new)
            {
                this.create_pdf();
                Program.PDFs.Add(this);
            }
        }

        public void viewPDF()
        {
            try
            {
                // 1. נסיון לפתוח לפי הנתיב המקורי (מה שרשום ב-SQL)
                if (File.Exists(this.path))
                {
                    Process.Start(new ProcessStartInfo(this.path) { UseShellExecute = true });
                }
                else
                {
                    // 2. פתרון למעבדה: אם הנתיב המלא לא נמצא, נחפש בתיקייה המקומית של הפרויקט
                    string localFolder = Path.Combine(Application.StartupPath, "ByNoa_Documents");
                    string fileNameOnly = Path.GetFileName(this.path); // חילוץ שם הקובץ בלבד
                    string localPath = Path.Combine(localFolder, fileNameOnly);

                    if (File.Exists(localPath))
                    {
                        Process.Start(new ProcessStartInfo(localPath) { UseShellExecute = true });
                    }
                    else
                    {
                        MessageBox.Show("File not found locally or in SQL path.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not open PDF: " + ex.Message);
            }
        }

        private void create_pdf()
        {
            SqlCommand c = new SqlCommand();
            c.CommandText = "EXECUTE dbo.SP_add_FilePDF @path, @fileName, @type, @appId, @transId";
            c.Parameters.AddWithValue("@path", this.path);
            c.Parameters.AddWithValue("@fileName", this.name);
            c.Parameters.AddWithValue("@type", this.type.ToString());
            c.Parameters.AddWithValue("@appId", (object)this.appointment?.id ?? DBNull.Value);
            c.Parameters.AddWithValue("@transId", (object)this.transaction?.id ?? DBNull.Value);

            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(c);
        }

        public void uploadPDF(string sourceFilePath)
        {
            try
            {
                string targetFolder = Path.Combine(Application.StartupPath, "ByNoa_Documents");
                if (!Directory.Exists(targetFolder)) Directory.CreateDirectory(targetFolder);

                string uniqueFileName = Guid.NewGuid().ToString().Substring(0, 8) + "_" + Path.GetFileName(sourceFilePath);
                string destinationPath = Path.Combine(targetFolder, uniqueFileName);

                File.Copy(sourceFilePath, destinationPath, true);

                string oldPath = this.path;
                this.path = destinationPath;
                this.name = uniqueFileName;

                update_pdf_in_db(oldPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to upload: " + ex.Message);
            }
        }

        private void update_pdf_in_db(string oldPath)
        {
            SqlCommand c = new SqlCommand();
            c.CommandText = "EXECUTE dbo.SP_update_FilePDF @oldPath, @fileName, @type, @appId, @transId";
            c.Parameters.AddWithValue("@oldPath", oldPath);
            c.Parameters.AddWithValue("@fileName", this.name);
            c.Parameters.AddWithValue("@type", this.type.ToString());
            c.Parameters.AddWithValue("@appId", (object)this.appointment?.id ?? DBNull.Value);
            c.Parameters.AddWithValue("@transId", (object)this.transaction?.id ?? DBNull.Value);

            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(c);
        }
    }
}