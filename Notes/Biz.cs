using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes
{
    public class NotesModel
    {
        public NotesModel() { }

        public int ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public DateTime UpdateTime { get; set; }
        
    }
    public class Biz
    {

        public void CreateTable()
        {
            
            string strSql = @"CREATE table if not exists notes3 
                              (
	                                id int not null auto_increment primary key,
	                                title text,
	                                content text,
	                                updatetime datetime
                               )";
            DBOperate.ExecuteSql(strSql);
        }

        public List<NotesModel> GetAllData()
        {
            List<NotesModel> lstResult = new List<NotesModel>();
            string strSql = @"SELECT id, title, content, updatetime
                                FROM notes3; ";
            DataTable dt=DBOperate.GetSqlDataTable(strSql);
            lstResult=DataTableToList(dt);
            return lstResult;

        }

        public NotesModel GetModelDataByID(int ID) {
            string strSql = @"SELECT id, title, content, updatetime
                                FROM notes3 where id={0}; ";
            strSql=string.Format(strSql, ID);
            DataTable dtResult = DBOperate.GetSqlDataTable(strSql);
            var listResult = DataTableToList(dtResult);
            return listResult.FirstOrDefault();
        }

        public void InsertData(NotesModel model)
        {
            string strSql = "insert into notes3(title, content, updatetime) values ('{0}','{1}','{2}')";
            strSql = string.Format(strSql,model.Title,model.Content,DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            DBOperate.ExecuteSql(strSql);
        }

        public void UpdateData(NotesModel model)
        {
            string strSql = "update notes3 set title='{0}', content='{1}', updatetime='{2}' WHERE id={3}";
            strSql = string.Format(strSql, model.Title, model.Content, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),model.ID);
            DBOperate.ExecuteSql(strSql);
        }

        public void DeleteData(int ID)
        {
            string strSql = "delete from notes3 where id={0}";
            strSql= string.Format(strSql, ID);
            DBOperate.ExecuteSql(strSql);
        }



        private List<NotesModel> DataTableToList(DataTable dtResult)
        {
            List<NotesModel> lstResult = new List<NotesModel>();
            if (dtResult != null && dtResult.Rows.Count > 0)
            {
                foreach (DataRow dr in dtResult.Rows) {
                    lstResult.Add(new NotesModel {
                        ID =int.Parse( dr["id"].ToString()),
                        Title =dr["title"].ToString(),
                        Content =dr["content"].ToString(),
                        UpdateTime = DateTime.Parse(dr["updatetime"].ToString())
                    
                    });


                }
            }
            return lstResult;

        }
    }
}
