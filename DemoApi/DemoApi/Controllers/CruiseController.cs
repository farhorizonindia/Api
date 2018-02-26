using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FarHorizon.Reservations.BusinessServices;
using FarHorizon.Reservations.BusinessServices.Online.BAL;
using FarHorizon.Reservations.BusinessServices.Online.DAL;
using FarHorizon.Reservations.MasterServices;
using System.Configuration;
using System.Data;
using FarHorizon.Reservations.Common.DataEntities.Masters;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.Web;
namespace DemoApi.Controllers
{
    public class CruiseController : ApiController
    {
        BALBooking blsr = new BALBooking();
        DALBooking dlsr = new DALBooking();
        BALSearch blsrch = new BALSearch();
        DALSearch dlsrch = new DALSearch();
        public Cabin cab = new Cabin();
        string pakackageid;
        int departureId;
        DataView dv;
        public DataTable dtres;
        //Student[] stud = new Student[]
        //    {
        //    new Student{StudRollNO=1, StudName="Amit",
        //    StudAddress="Noida", StudMONO=123, StudCourse="MCA"},
        //    new Student{StudRollNO=2, StudName="Rahgvendra",
        //    StudAddress="Hardoi", StudMONO=321, StudCourse="BCA"},
        //    new Student{StudRollNO=3, StudName="Ankur",
        //    StudAddress="Lucknow", StudMONO=567, StudCourse="MBA"}
        //    };
      //  string strCon = ConfigurationManager.ConnectionStrings["ReservationConnectionString"].ConnectionString;
        // GET api/<controller>
        //public IEnumerable<Student> Get()
        //{

        //    return stud;

        //}

        // GET api/<controller>/5
        //localhost:12182/api/Cruise/7 night 8 day MV Mahabaahu Upstream Cruise/2018-01-14/247
        //163.172.206.126:83/api/Cruise/7 night 8 day MV Mahabaahu Upstream Cruise/2018-01-14/247
        public DataTable Get(string packageid, DateTime departure, int AgentId,int AccomId)
        {

            if (!string.IsNullOrEmpty(packageid))
            {


                DataTable dt2 = new DataTable();
                string value = "";
                HttpContext httpContext = HttpContext.Current;
                string authHeader = httpContext.Request.Headers["Authorization"];
                if (authHeader != null && authHeader.StartsWith("Basic"))
                {
                    string encodedUsernamePassword = authHeader.Substring("Basic ".Length).Trim();

                    AgentMaster oAgentMaster = new AgentMaster();

                    AgentDTO[] oAgentData = oAgentMaster.GetApiAuth(AgentId);
                    string TokenNo = oAgentData[0].TokenNo.ToString();
                    if (TokenNo != "" && TokenNo == encodedUsernamePassword)
                    {
                        var dateAndTime = DateTime.Now;
                        var date = dateAndTime.Date;
                        //DateTime d = DateTime.Now.Year;
                        DataTable dt1 = dlsr.getpackaageid(packageid, departure, AccomId);
                        if (dt1 != null && dt1.Rows.Count > 0)
                        {
                            pakackageid = dt1.Rows[0]["PackageId"].ToString();
                            departureId = Convert.ToInt32(dt1.Rows[0]["Id"].ToString());
                        }
                        DataTable dt = cab.bindroomddl(pakackageid, departureId, AccomId);
                        dv = new DataView();
                        dv = new DataView(dt, "BookedStatus='" + "Available" + "'", "BookedStatus", DataViewRowState.CurrentRows);
                        // DataTable dt2 = dv.ToTable();
                        dt2 = dv.ToTable();
                        DataSet ds = new DataSet();
                        ds.Tables.Add(dt2);
                        value = ds.GetXml();
                        // return value;
                        return dt2;


                    }
                    else
                    {
                        //return value;
                        return dt2;
                    }
                }
                else
                {
                    // return value;
                    return dt2;
                }
            }
            else
            {
                DataTable dt12 = new DataTable();
                DataColumn dc = new DataColumn("Error", typeof(String));
                dt12.Columns.Add(dc);
                DataRow dr = dt12.NewRow();
                dr[0] = "Invalid date formta(format should be yyyy-mm-dd)";
                // insert in the desired place
                dt12.Rows.InsertAt(dr, 0);
                return dt12;

            }
        }

        ///api/Cruise/5 nights 6 days MV Mahabaahu Upstream Cruise/2018-10-16/2018-10-21/4/rabiul/102,208/7/2/online/247
        //163.172.206.126:83/api/Cruise/5 nights 6 days MV Mahabaahu Upstream Cruise/2018-10-16/2018-10-21/4/rabiul/206,109/7/2/online/247
        public DataTable GetBooking(string packageid, DateTime chekindate, DateTime checkoutdate, int total, string refrence, string roomno, int accomid, int roomcatid, string paymentmethod, int agentid)
        {
            string value = "";
            DataSet ds = new DataSet();
            DataTable dtvalue = new DataTable();
            HttpContext httpContext = HttpContext.Current;
            string authHeader = httpContext.Request.Headers["Authorization"];
            string packagename = "";

            try
            {

                if (authHeader != null && authHeader.StartsWith("Basic"))
                {
                    string encodedUsernamePassword = authHeader.Substring("Basic ".Length).Trim();

                    AgentMaster oAgentMaster = new AgentMaster();

                    AgentDTO[] oAgentData = oAgentMaster.GetApiAuth(agentid);
                    string TokenNo = oAgentData[0].TokenNo.ToString();
                    if (TokenNo != "" && TokenNo == encodedUsernamePassword)
                    {
                        DataTable dt1 = dlsr.getpackaageid(packageid, chekindate, accomid);
                        if (dt1 != null && dt1.Rows.Count > 0)
                        {
                            pakackageid = dt1.Rows[0]["PackageId"].ToString();
                            departureId = Convert.ToInt32(dt1.Rows[0]["Id"].ToString());
                            packagename = dt1.Rows[0]["PackageName"].ToString();
                        }
                        DataTable dt = cab.bindroomddl(pakackageid, departureId, accomid);
                        dv = new DataView();

                        string[] strArray = roomno.Split(',');


                        //foreach (object obj in strArray)
                        //{
                        //    roomno = obj.ToString();
                        //}
                        //var values = Request.GetQueryNameValuePairs()
                        //      .Where(kvp => kvp.Key == "roomno")
                        //      .Select(kvp => int.Parse(kvp.Value))
                        //      .ToArray();
                        int n = cab.InsertParentTableData(packageid, chekindate, checkoutdate, total, refrence, agentid);
                        foreach (var lockRoom in strArray)
                        {

                            roomno = lockRoom.ToString();
                            dv = new DataView(dt, "RoomNo='" + roomno + "'", "RoomNo", DataViewRowState.CurrentRows);
                            DataTable dtcheck = dv.ToTable();
                            dv = new DataView(dtcheck, "BookedStatus='" + "Available" + "'", "BookedStatus", DataViewRowState.CurrentRows);

                            if (dv.Count > 0)
                            {


                                //var values = Request.GetQueryNameValuePairs()
                                //   .Where(kvp => kvp.Key == "roomno")
                                //   .Select(kvp => int.Parse(kvp.Value))
                                //   .ToArray();
                                //foreach (var lockRoom in strArray)
                                //{
                                //roomno = lockRoom.ToString();
                                cab.InsertChildTableData(n, total, roomno, paymentmethod, chekindate, packageid, total, roomcatid);
                                cab.LockTheBooking(roomno, accomid, roomcatid);
                                blsr.action = "fetchbybookingId";
                                blsr._iBookingId = n;
                                DataTable dtfetch = dlsr.fetchbybookingid(blsr);
                                // DataTable dtvalue = cab.loadbookingDetails(pakackagename, n, total, roomno, paymentmethod, chekindate, checkoutdate, pakackageid, total, roomcatid, Convert.ToInt32(dtfetch.Rows[0]["NoOFNights"].ToString()));
                                //DataSet ds = new DataSet();
                                dtvalue = cab.loadbookingDetails(packagename, n, total, roomno, paymentmethod, chekindate, checkoutdate, packageid, total, roomcatid, Convert.ToInt32(dtfetch.Rows[0]["NoOFNights"].ToString()));
                                ds.Tables.Add(dtvalue);
                                //value = ds.GetXml();
                                //// }
                                //return value;

                            }
                            //else
                            //{
                            //    return "Room is already booked";
                            //}
                        }
                    }
                    value = ds.GetXml();
                    // }
                    if (value != "<NewDataSet />")
                    {
                        return dtvalue;
                    }
                    else
                    {
                        if (TokenNo == encodedUsernamePassword)
                        {
                            dtvalue = GetTable();
                            //return "Room is already booked";
                            return dtvalue;
                        }
                        else
                        {
                            dtvalue = GetToken();
                            //return value;
                            return dtvalue;
                        }
                    }
                }


                else
                {
                    dtvalue = GetHeader();
                    //return value;
                    return dtvalue;
                }
            }

            catch(Exception ex) {

                dtvalue = GetHeader();
                //return value;
                return dtvalue;
            }
        }
        static DataTable GetTable()
        {
            // Here we create a DataTable with four columns.
            DataTable table = new DataTable();
          
            table.Columns.Add("Status", typeof(string));
           

            // Here we add five DataRows.
            table.Rows.Add("Room is already booked");
           
            return table;
        }

        static DataTable GetTableNotAvailable()
        {
            // Here we create a DataTable with four columns.
            DataTable table = new DataTable();

            table.Columns.Add("Status", typeof(string));


            // Here we add five DataRows.
            table.Rows.Add("Room's not available");

            return table;
        }
        static DataTable GetToken()
        {
            // Here we create a DataTable with four columns.
            DataTable table = new DataTable();

            table.Columns.Add("Status", typeof(string));


            // Here we add five DataRows.
            table.Rows.Add("Please Check Token No");

            return table;
        }
        static DataTable GetHeader()
        {
            // Here we create a DataTable with four columns.
            DataTable table = new DataTable();

            table.Columns.Add("Status", typeof(string));


            // Here we add five DataRows.
            table.Rows.Add("Please Add Header");

            return table;
        }
        //localhost:5024/api/Values/203,109,108/7/2/248 call MultiplelockRoom
        //public string GetLockRoom(string roomno, int accomid, int roomcatid, int AgentId)
        //{
        //    string value = "";
        //    HttpContext httpContext = HttpContext.Current;
        //    string authHeader = httpContext.Request.Headers["Authorization"];
        //    if (authHeader != null && authHeader.StartsWith("Basic"))
        //    {
        //        string encodedUsernamePassword = authHeader.Substring("Basic ".Length).Trim();
        //        //var values = Request.GetQueryNameValuePairs()
        //        //    .Where(kvp => kvp.Key == "roomno")
        //        //    .Select(kvp => int.Parse(kvp.Value))
        //        //    .ToArray();
        //        AgentMaster oAgentMaster = new AgentMaster();
        //        // int AgentId = blsrch.AgentId;
        //        AgentDTO[] oAgentData = oAgentMaster.GetApiAuth(AgentId);
        //        string TokenNo = oAgentData[0].TokenNo.ToString();

        //        if (TokenNo != "" && TokenNo == encodedUsernamePassword)
        //        {
        //            string[] values = roomno.Split(',');
        //            foreach (var lockRoom in values)
        //            {
        //                roomno = lockRoom.ToString();
        //                cab.LockTheBooking(roomno, accomid, roomcatid);

        //            }

        //            value = "Room is Locked";
        //            return value;
        //        }

        //        return value;
        //    }
        //    else
        //    {
        //        value = "Please set Header Authentication";
        //        return value;
        //    }
        //}

        // Url:localhost:12182/api/Cruise/GetCruiseOpendates?AgentId=247
        // 163.172.206.126:83/api/Cruise/GetCruiseOpendates? AgentId = 247
        public DataTable GetCruiseOpendates(int AgentId,string StartDate=null ,string EndDate=null,int? Month=0,int? Year=0,int AccomId=0,string packageid=null)
        {
            DataTable dt12 = new DataTable();

            HttpContext httpContext = HttpContext.Current;
            string authHeader = httpContext.Request.Headers["Authorization"];
            if (authHeader != null && authHeader.StartsWith("Basic"))
            {
                string encodedUsernamePassword = authHeader.Substring("Basic ".Length).Trim();
                blsrch.action = "GetOpenDatesCruiseApi";


                if(!string.IsNullOrEmpty(StartDate) && !string.IsNullOrEmpty(EndDate))
                {
                    //DateTime date1 =DateTime.ParseExact(StartDate, "mm-dd-yyyy", null);
                    //DateTime date2 = DateTime.ParseExact(EndDate, "mm-dd-yyyy", null);

                    try
                    {
                        blsrch.StartDate = Convert.ToDateTime(StartDate);
                        blsrch.EndDate = Convert.ToDateTime(EndDate);
                    }
                    catch
                    {

                        DataColumn dc = new DataColumn("Error", typeof(String));
                        dt12.Columns.Add(dc);
                        DataRow dr = dt12.NewRow();
                        dr[0] = "Invalid date formta(format should be yyyy-mm-dd)";
                        // insert in the desired place
                        dt12.Rows.InsertAt(dr, 0);
                        return dt12;
                    }
                }

                if(Month>0 && Year>0)
                {
                    string datestring = Year + "-" + Month + "-01";

                    blsrch.StartDate = Convert.ToDateTime(datestring);
                    blsrch.EndDate = Convert.ToDateTime(datestring).AddMonths(1);

                }

                if(Month==0 &&Year==0 && StartDate==null && EndDate==null)
                
                {
                    blsrch.StartDate = System.DateTime.Now;
                    blsrch.EndDate = Convert.ToDateTime("1900-01-01");

                }

                if (Month == 0 && Year >0 && StartDate == null && EndDate == null)

                {
                    string datestring = Year + "-01" + "-01";

                    blsrch.StartDate = Convert.ToDateTime(datestring);
                    blsrch.EndDate = Convert.ToDateTime(datestring).AddYears(1);

                }
                blsrch.AgentId = AgentId;
                //{
                //    //blsrch.AgentId = 247;
                //}
                AgentMaster oAgentMaster = new AgentMaster();
                // int AgentId = blsrch.AgentId;
                AgentDTO[] oAgentData = oAgentMaster.GetApiAuth(AgentId);
                string TokenNo = oAgentData[0].TokenNo.ToString();

                if (TokenNo != "" && TokenNo == encodedUsernamePassword)
                {

                    dtres = dlsrch.GetCruiseOpenDatesPackage(blsrch);
                    DataTable dtall = dlsrch.fetchall();

                    DataTable dt = new DataTable();
                    if(  (packageid==null || Convert.ToString(packageid).ToLower()=="all") && AccomId>0)
                    {
                        DataView dvdis = new DataView(dtall);
                        string st = "Hotelid=" + AccomId;
                        //  string st = "Packageid = '" + dtres.Rows[i]["packageId"].ToString() + "',Boardingdate =" + dtres.Rows[i]["CheckInDate"];
                        dvdis.RowFilter = st;
                        dt = dvdis.ToTable();

                    }

                    else 
                    {
                        DataView dvdis = new DataView(dtall);
                        string st = "Packageid = '" + packageid + "' and Hotelid=" + AccomId;
                        //  string st = "Packageid = '" + dtres.Rows[i]["packageId"].ToString() + "',Boardingdate =" + dtres.Rows[i]["CheckInDate"];
                        dvdis.RowFilter = st;
                       dt = dvdis.ToTable();


                    }


                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            blsrch.PackageId = dt.Rows[i]["PackageId"].ToString();
                            dtres = dlsrch.GetCruiseOpenDatesPackage(blsrch);
                            dt12.Merge(dtres);
                            dt12.AcceptChanges();

                        }
                    }
                    DataSet ds = new DataSet();
                    ds.Tables.Add(dt12);
                    string value = ds.GetXml();
                }
                return dt12;
            }
            else
            {
                return dt12;
            }

        }





        //public DataTable fetchbybookingid(BALBooking obj)
        //{
        //    try
        //    {
        //        SqlConnection cn = new SqlConnection(strCon);
        //        SqlDataAdapter da = new SqlDataAdapter();
        //        da.SelectCommand = new SqlCommand("[sp_CruiseBooking]", cn);
        //        da.SelectCommand.Parameters.Clear();
        //        da.SelectCommand.Parameters.AddWithValue("@action", obj.action);
        //        da.SelectCommand.Parameters.AddWithValue("@bookingId", obj._iBookingId);



        //        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        //        cn.Open();
        //        da.SelectCommand.ExecuteReader();
        //        DataTable dtReturnData = new DataTable();
        //        cn.Close();
        //        da.Fill(dtReturnData);
        //        if (dtReturnData != null)
        //            return dtReturnData;
        //        else
        //            return null;
        //    }
        //    catch (Exception ex)

        //    {
        //        return null;
        //    }
        //}
        // POST api/<controller>

        //public void Post([FromBody] LockEntry lok)
        //{
        //    string roomno = lok.roomno;
        //    int accomid = lok.accomid;
        //    int roomcatid = lok.roomcategoryid;
        //    cab.LockTheBooking(roomno, accomid, roomcatid);

        //}

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }

   
    //public class person
    //{
    //    public string name { get; set; }
    //    public string surname { get; set; }
    //}


}


