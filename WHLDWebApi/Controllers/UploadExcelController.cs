using LinqToExcel;
using LinqToExcel.Attributes;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using WHLDWebApi.Models;

namespace WHLDWebApi.Controllers
{
    public class UploadExcelController : ApiController
    {
        private DbEntities db = new DbEntities();

        public class ExcelTask
        {
            [ExcelColumn("任务名称")]
            public string TaskName { get; set; }

            [ExcelColumn("任务时间")]
            public string Time { get; set; }

            [ExcelColumn("任务负责人")]
            public string Person { get; set; }
        }

        public class ExcelPlan
        {
            [ExcelColumn("计划名称")]
            public string PlanName { get; set; }

            [ExcelColumn("任务名称")]
            public string TaskName { get; set; }

            [ExcelColumn("小班编号")]
            public Int32 XBH { get; set; }

            [ExcelColumn("管护措施")]
            public string GHCS { get; set; }
        }

        public HttpResponseMessage ImportExcelToDatabase()
        {
            //接收form表单中提交过来的excel数据表
            var file = HttpContext.Current.Request.Files["File"];
            //限定上传excel扩展名
            string[] extensionName = new string[] { ".XLSX", ".XLS" };
            //上传至服务器的路径
            string serverPath = string.Empty;
            //返回至客户端的信息
            string msg = string.Empty;
            //错误时返回的信息对象；
            var resp = new HttpResponseMessage(HttpStatusCode.NotFound);

            //首先将excel文件上传至服务器
            //然后转换成从服务器端读取数据插入数据中(浏览器相当于客户端，是无法直接读取远程客户端excel中的数据的)
            //判断excel文件已经跟随表单被传递
            if (!string.IsNullOrWhiteSpace(file.FileName))
            {
                //说明文件已经上传
                string newName = string.Empty;
                //获取excel文件扩展名
                string extName = Path.GetExtension(file.FileName);
                //获取服务器根路径
                string rootPath = AppDomain.CurrentDomain.BaseDirectory;
                //上传值服务器全路径
                string fullPath = string.Empty;
                //判断excel文件是否符合上传标准
                if (extensionName.Contains(extName.ToUpper()))
                {
                    //符合上传的文件标准
                    newName = Guid.NewGuid().ToString();
                    //此时是文件上传至服务器的文件全名
                    newName = newName + extName;
                    //上传至服务器的路径
                    serverPath = "Excels\\"; //+ newName;
                    fullPath = rootPath + serverPath;
                    //判断文件上传文件路径
                    if (!Directory.Exists(fullPath))
                    {
                        //如果不存在，则创建目录
                        Directory.CreateDirectory(fullPath);
                    }
                    //文件保存到服务器
                    file.SaveAs(HttpContext.Current.Server.MapPath("~/" + serverPath + newName));
                    fullPath = fullPath + newName;
                    //读取文件为DataTable
                    DataTable PlanTable = ExceltoDataTable(fullPath, "Plan");
                    DataTable TaskTable = ExceltoDataTable(fullPath, "Task");
                    List<DataTable> dataTables = new List<DataTable> { PlanTable, TaskTable };
                    //检查文件内部错误    
                    var nullTask = CheckNullValue(TaskTable);
                    var nullPlan = CheckNullValue(PlanTable);
                    if (nullPlan.Count != 0 || nullTask.Count != 0)
                    {
                        string TaskSheet = string.Join(",", nullTask.ToArray());
                        string PlanSheet = string.Join(",", nullPlan.ToArray());
                        resp.Content = new StringContent(string.Format("文件存在空值！ 表一空值:" + PlanSheet + "表二空值:" + TaskSheet));
                        resp.ReasonPhrase = "Null value";
                        throw new HttpResponseException(resp);
                    }

                    List<string> errorNames = CheckTaskName(TaskTable);
                    if (errorNames.Count != 0)
                    {
                        string str = string.Join(",", errorNames.ToArray());
                        resp.Content = new StringContent(string.Format("任务名已存在:" + str));
                        resp.ReasonPhrase = "The task name already exists";
                        throw new HttpResponseException(resp);
                    }

                    List<Int32> XBH = CheckXBH(PlanTable);
                    if (XBH.Count != 0)
                    {
                        string str = string.Join(",", XBH.ToArray());
                        resp.Content = new StringContent(string.Format("小班号不存在:" + str));
                        resp.ReasonPhrase = "XBID does not exist";
                        throw new HttpResponseException(resp);
                    }

                    //对文件进行插入数据库操作
                    try
                    {
                        //导入到数据库
                        ImportDataToDataBase(TaskTable,PlanTable);
                    }
                    catch
                    {
                        //异常时删除上传至服务器的文件
                        File.Delete(fullPath);
                        resp.Content = new StringContent(string.Format("不知名错误！"));
                        resp.ReasonPhrase = "Unknown mistake";
                        throw new HttpResponseException(resp);
                    }
                    finally
                    {
                        //异常时，手动关闭文件流，并释放内存！
                        file.InputStream.Close();
                        file.InputStream.Dispose();
                    }
                }
                else
                {
                    resp.Content = new StringContent(string.Format("数据导入失败，文件格式不符合标准，请选择后缀名为：.xlsx，.xls类型文件！"));
                    resp.ReasonPhrase = "Wrong format";
                    throw new HttpResponseException(resp);
                }
            }
            else
            {
                resp.Content = new StringContent(string.Format("导入数据失败，请在表单中选中要导入excel数据表！"));
                resp.ReasonPhrase = "Failed";
                throw new HttpResponseException(resp);
            }

            return new HttpResponseMessage { Content = new StringContent("上传成功!", System.Text.Encoding.UTF8, "application/json") };        
        }

        private List<string> CheckTaskName(DataTable table)
        {
            var taskNames = db.Tasks.Select(s => s.Title);
            List<string> errorNames = new List<string>();

            foreach (DataRow dr in table.Rows)
            {
                if (taskNames.Contains(Convert.ToString(dr["任务名称"])))
                {
                    errorNames.Add(Convert.ToString(dr["任务名称"]));
                }
                else
                {
                    continue;
                }
            }
            return errorNames;
        }

        private List<Int32> CheckXBH(DataTable table)
        {
            var XBHArr = db.XBInfos.Select(s => s.XBH);
            List<Int32> errorXBH = new List<Int32>();

            foreach (DataRow dr in table.Rows)
            {
                if (XBHArr.Contains(Convert.ToInt32(dr["小班编号"])))
                {
                    continue;
                }
                else
                {
                    errorXBH.Add(Convert.ToInt32(dr["小班编号"]));
                }
            }
            return errorXBH;
        }

        private Dictionary<string, string> CheckNullValue(DataTable table)
        {
            Dictionary<string, string> nullDic = new Dictionary<string, string>();

            for (var i = 0; i < table.Rows.Count; i++)
            {
                for (var j = 0; j < table.Rows[i].ItemArray.Length; j++)
                {
                    if (Convert.IsDBNull(table.Rows[i][j]))
                    {
                        int rowNum = i + 1;
                        nullDic.Add(table.Columns[j].ColumnName, string.Format("第"+ rowNum + "行"));
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            return nullDic;
        }

        private void ImportDataToDataBase(DataTable TaskTable, DataTable PlanTable)
        {     
            List<ExcelPlan> excelPlanList = new List<ExcelPlan>();
            List<ExcelTask> excelTaskList = new List<ExcelTask>();

            foreach (DataRow dr in PlanTable.Rows)
            {
                ExcelPlan s = new ExcelPlan
                {
                    XBH = Convert.ToInt32(dr["小班编号"]),
                    GHCS = Convert.ToString(dr["管护措施"]),
                    TaskName = Convert.ToString(dr["任务名称"]),
                    PlanName = Convert.ToString(dr["计划名称"])
                };
                excelPlanList.Add(s);
            }

            foreach (DataRow dr in TaskTable.Rows)
            {
                ExcelTask s = new ExcelTask
                {
                    Person = Convert.ToString(dr["任务负责人"]),
                    Time = Convert.ToString(dr["任务时间"]),
                    TaskName = Convert.ToString(dr["任务名称"])
                };
                excelTaskList.Add(s);
            }

            var TaskMaxId = db.Tasks.Select(s => s.TId).Max();
            var PlanMaxId = db.Plans.Select(s => s.PId).Max();

            Dictionary<string, int> TaskDic = new Dictionary<string, int>();
            Dictionary<int, Array> PlanDic = new Dictionary<int, Array>();

            for (int i = 0; i < excelTaskList.Count(); i++)
            {
                TaskDic.Add(excelTaskList[i].TaskName, TaskMaxId + i + 1);
            }

            for (int i = 0; i < excelPlanList.Count(); i++)
            {
                string[] sArray = Regex.Split(excelPlanList[i].GHCS, ",", RegexOptions.IgnoreCase);
                PlanDic.Add(i + 1 + PlanMaxId, sArray);
            }
            foreach (var model in excelTaskList)
            {
                Task entity = new Task()
                {
                    Title = model.TaskName,
                    DateTime = model.Time,
                    Person = model.Person,
                };

                db.Tasks.Add(entity);
            }
            db.SaveChanges();

            foreach (var model in excelPlanList)
            {
                Plan entity = new Plan()
                {
                    XBH = model.XBH,
                    TId = TaskDic[model.TaskName],
                };

                db.Plans.Add(entity);
            }
            db.SaveChanges();

            foreach (var s in PlanDic)
            {
                foreach (string a in s.Value)
                {
                    PlanGhcsRel entity = new PlanGhcsRel()
                    {
                        PId = s.Key,
                        GId = int.Parse(a)
                    };
                    db.PlanGhcsRels.Add(entity);
                }
            }
            db.SaveChanges();


            //var excel = new ExcelQueryFactory(fullPath);

            //var plan = from c in excel.Worksheet<ExcelPlan>("Plan") select c;
            //var task = from c in excel.Worksheet<ExcelTask>("Task") select c;

            //var TaskMaxId = db.Tasks.Select(s => s.TId).Max();
            //var PlanMaxId = db.Plans.Select(s => s.PId).Max();

            //Dictionary<string, int> TaskDic = new Dictionary<string, int>();
            //Dictionary<int, Array> PlanDic = new Dictionary<int, Array>();

            //var taskList = task.ToList();
            //var planList = plan.ToList();

            //for (int i = 0; i < taskList.Count(); i++)
            //{
            //    TaskDic.Add(taskList[i].TaskName, TaskMaxId + i + 1);
            //}

            //for (int i = 0; i < planList.Count(); i++)
            //{
            //    string[] sArray = Regex.Split(planList[i].GHCS, ",", RegexOptions.IgnoreCase);
            //    PlanDic.Add(i + 1 + PlanMaxId, sArray);
            //}
            //foreach (var model in task)
            //{
            //    Task entity = new Task()
            //    {
            //        Title = model.TaskName,
            //        DateTime = model.Time,
            //        Person = model.Person,
            //    };

            //    db.Tasks.Add(entity);
            //}
            //db.SaveChanges();

            //foreach (var model in plan)
            //{
            //    Plan entity = new Plan()
            //    {
            //        XBH = model.XBH,
            //        TId = TaskDic[model.TaskName],
            //    };

            //    db.Plans.Add(entity);
            //}
            //db.SaveChanges();

            //foreach (var s in PlanDic)
            //{
            //    foreach (string a in s.Value)
            //    {
            //        PlanGhcsRel entity = new PlanGhcsRel()
            //        {
            //            PId = s.Key,
            //            GId = int.Parse(a)
            //        };
            //        db.PlanGhcsRels.Add(entity);
            //    }
            //}
            //db.SaveChanges();

            //return true;
        }

        private DataTable ExceltoDataTable(string filePath,string sheetName) {
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            IWorkbook workbook = new XSSFWorkbook(fs);
            ISheet sheet = workbook.GetSheet(sheetName);

            DataTable data = new DataTable();
            int startRow = 0;

            try
            {
                fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                workbook = new XSSFWorkbook(fs);

                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(0);
                    int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数

                    if (true)
                    {
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            ICell cell = firstRow.GetCell(i);
                            if (cell != null)
                            {
                                string cellValue = cell.StringCellValue;
                                if (cellValue != null)
                                {
                                    DataColumn column = new DataColumn(cellValue);
                                    data.Columns.Add(column);
                                }
                            }
                        }
                        startRow = sheet.FirstRowNum + 1;
                    }
                    else
                    {
                        startRow = sheet.FirstRowNum;
                    }

                    //最后一列的标号
                    int rowCount = sheet.LastRowNum;
                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue; //没有数据的行默认是null　　　　　　　

                        DataRow dataRow = data.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                                dataRow[j] = row.GetCell(j).ToString();
                        }
                        data.Rows.Add(dataRow);
                    }
                }

                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return null;
            }
        }
    }
}

