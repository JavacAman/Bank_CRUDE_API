//using crude_class_library;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Data.SqlClient;
//using System.Data;
//using System.Dynamic;

//namespace IB_Crude_Web_API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class SearchController : ControllerBase
//    {


//        IBDatabaseDbContext _dbContext;
//        public SearchController(IBDatabaseDbContext dbContext)
//        {
//            _dbContext = dbContext;
//        }

//        [HttpPost]
//        [Route("SearchNotes")]
//        public async Task<List<Models.Notes>> SearchNotes(Models.Notes objDaks)
//        {
//            List<Models.Notes> serachresult = new List<Models.Notes>();

//            try
//            {
//                crude_class_library.Departments objDepartments = _dbContext.Departments.Where(t => t.DepartmentName == objDaks.DepartmentName).FirstOrDefault();

//                int? statusId = (int)(NoteStatus)objDaks.status;
//                int? FinancialTypeId = (int)(financialType)objDaks.financialType;
//                int? MovementTypeId = (int)(natureofNote)objDaks.natureofNote;

//                var fromDate = new DateTime();
//                var ToDate = new DateTime();

//                if (objDaks.fromDate != null)
//                {
//                    string[] dateParts = objDaks.fromDate.Split('-');
//                    int day = int.Parse(dateParts[0]);
//                    int month = int.Parse(dateParts[1]);
//                    int year = int.Parse(dateParts[2]);
//                    fromDate = new DateTime(year, month, day);
//                }
//                if (objDaks.ToDate != null)
//                {
//                    string[] dateParts = objDaks.ToDate.Split('-');
//                    int day = int.Parse(dateParts[0]);
//                    int month = int.Parse(dateParts[1]);
//                    int year = int.Parse(dateParts[2]);
//                    ToDate = new DateTime(year, month, day);
//                }

//                using (var cmd = _dbContext.Database.GetDbConnection().CreateCommand())
//                {
//                    cmd.CommandText = "SearchNotes";
//                    cmd.CommandType = CommandType.StoredProcedure;

//                    cmd.Parameters.Add(new SqlParameter("@Note", SqlDbType.VarChar)
//                    {
//                        Value = objDaks.noteNumber
//                    });
//                    cmd.Parameters.Add(new SqlParameter("@requester", SqlDbType.VarChar)
//                    {
//                        Value = objDaks.createdBy
//                    });
//                    if (objDepartments != null)
//                    {
//                        cmd.Parameters.Add(new SqlParameter("@departmentId", SqlDbType.Int)
//                        {
//                            Value = objDepartments.departmentId
//                        });
//                    }
//                    cmd.Parameters.Add(new SqlParameter("@SearchText", SqlDbType.VarChar)
//                    {
//                        Value = objDaks.searchKeyword
//                    });

//                    if (objDaks.fromDate != null)
//                    {
//                        cmd.Parameters.Add(new SqlParameter("@fromDate", SqlDbType.DateTime)
//                        {
//                            Value = fromDate
//                        });
//                    }
//                    if (objDaks.ToDate != null)
//                    {
//                        cmd.Parameters.Add(new SqlParameter("@ToDate", SqlDbType.DateTime)
//                        {
//                            Value = ToDate
//                        });
//                    }
//                    if (statusId != 0)
//                    {
//                        cmd.Parameters.Add(new SqlParameter("@status", SqlDbType.Int)
//                        {
//                            Value = statusId.Value
//                        });
//                    }
//                    cmd.Parameters.Add(new SqlParameter("@Subject", SqlDbType.VarChar)
//                    {
//                        Value = objDaks.Subject

//                    });
//                    if (FinancialTypeId != 0)
//                    {
//                        cmd.Parameters.Add(new SqlParameter("@financialType", SqlDbType.Int)
//                        {
//                            Value = FinancialTypeId.Value
//                        });
//                    }
//                    cmd.Parameters.Add(new SqlParameter("@fy", SqlDbType.VarChar)
//                    {
//                        Value = objDaks.fy
//                    });

//                    cmd.Parameters.Add(new SqlParameter("@approverEmail", SqlDbType.VarChar)
//                    {
//                        Value = objDaks.approverEmail
//                    });

//                    if (MovementTypeId != 0)
//                    {
//                        cmd.Parameters.Add(new SqlParameter("@noteType", SqlDbType.Int)
//                        {
//                            Value = MovementTypeId.Value
//                        });
//                    }
//                    if (cmd.Connection.State != ConnectionState.Open)
//                        cmd.Connection.Open();

//                    var retObject = new List<dynamic>();
//                    try
//                    {
//                        using (var dataReader = await cmd.ExecuteReaderAsync())
//                        {
//                            while (await dataReader.ReadAsync())
//                            {
//                                Models.Notes searchResponse = new Models.Notes();
//                                var dataRow = new ExpandoObject() as IDictionary<string, object>;
//                                for (var iFiled = 0; iFiled < dataReader.FieldCount; iFiled++)
//                                {
//                                    dataRow.Add(
//                                        dataReader.GetName(iFiled),
//                                        dataReader.IsDBNull(iFiled) ? 0 : dataReader[iFiled]
//                                 );
//                                    if (dataReader.GetName(iFiled).ToString() == "noteId") { searchResponse.noteId = (int)dataReader[iFiled]; }
//                                    if (dataReader.GetName(iFiled).ToString() == "noteNumber") { searchResponse.noteNumber = dataReader[iFiled].ToString(); }
//                                    if (dataReader.GetName(iFiled).ToString() == "DepartmentName") { searchResponse.DepartmentName = dataReader[iFiled].ToString(); }
//                                    if (dataReader.GetName(iFiled).ToString() == "Subject") { searchResponse.Subject = dataReader[iFiled].ToString(); }
//                                    if (dataReader.GetName(iFiled).ToString() == "status") { searchResponse.strNoteStatus = ((NoteStatus)(dataReader[iFiled])).ToString(); }
//                                    if (dataReader.GetName(iFiled).ToString() == "createdDate") { searchResponse.createdDate = Convert.ToDateTime(dataReader[iFiled].ToString()); }
//                                    if (dataReader.GetName(iFiled).ToString() == "modifiedDate") { searchResponse.modifiedDate = Convert.ToDateTime(dataReader[iFiled].ToString()); }
//                                    if (dataReader.GetName(iFiled).ToString() == "createdBy") { searchResponse.createdBy = dataReader[iFiled].ToString(); }
//                                    if (dataReader.GetName(iFiled).ToString() == "modifiedBy") { searchResponse.modifiedBy = dataReader[iFiled].ToString(); }
//                                    if (dataReader.GetName(iFiled).ToString() == "CurrentActioner") { searchResponse.CurrentActioner = dataReader[iFiled].ToString(); }
//                                }
//                                retObject.Add((ExpandoObject)dataRow);
//                                serachresult.Add(searchResponse);
//                            }
//                        }
//                    }
//                    catch (Exception ex)
//                    {
//                        Console.WriteLine(ex.ToString());
//                        LogsController.AddLogs(_dbContext, "ENote/Search", ex.Message, ex.GetBaseException().StackTrace);
//                    }
//                }
//                int i = serachresult.Count;
//                return serachresult;
//            }
//            catch (Exception ex)
//            {
//                LogsController.AddLogs(_dbContext, "Search/SearchNotes", ex.Message, ex.GetBaseException().StackTrace);
//                return serachresult;
//            }
//        }


//        private int? GetNoteTypeName(string strNoteType)
//        {
//            if (string.IsNullOrEmpty(strNoteType))
//                return null;

//            noteType? noteType = Enum.Parse<noteType>(strNoteType, ignoreCase: true);

//            if (noteType != null && Enum.IsDefined(typeof(noteType), noteType))
//            {
//                return (int)noteType;
//            }
//            else
//            {
//                return null;
//            }
//        }

//        private int? GetnatureOfApprovalOrSanctionIdIdName(string strnatureOfApprovalOrSanction)
//        {
//            if (string.IsNullOrEmpty(strnatureOfApprovalOrSanction))
//                return null;

//            natureOfApprovalOrSanction? natureOfApprovalOrSanction = Enum.Parse<natureOfApprovalOrSanction>(strnatureOfApprovalOrSanction, ignoreCase: true);

//            if (natureOfApprovalOrSanction != null && Enum.IsDefined(typeof(natureOfApprovalOrSanction), natureOfApprovalOrSanction))
//            {
//                return (int)natureOfApprovalOrSanction;
//            }
//            else
//            {
//                return null;
//            }
//        }

//        private int? GetFinancialTypeIdName(string strFinancialType)
//        {
//            if (string.IsNullOrEmpty(strFinancialType))
//                return null;

//            financialType? financialType = Enum.Parse<financialType>(strFinancialType, ignoreCase: true);

//            if (financialType != null && Enum.IsDefined(typeof(financialType), financialType))
//            {
//                return (int)financialType;
//            }
//            else
//            {
//                return null;
//            }
//        }

//        private int? GetStatusIdFromName(string statusName)
//        {
//            if (string.IsNullOrEmpty(statusName))
//                return null;

//            NoteStatus? noteStatus = Enum.Parse<NoteStatus>(statusName, ignoreCase: true);

//            if (noteStatus != null && Enum.IsDefined(typeof(NoteStatus), noteStatus))
//            {
//                return (int)noteStatus;
//            }
//            else
//            {
//                return null;
//            }
//        }
//    }
//}
