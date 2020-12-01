Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Configuration
Imports System.Collections
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.Collections.Generic
Imports DevExpress.XtraCharts
Imports System.Data.OleDb
Imports DevExpress.XtraPivotGrid
Imports DevExpress.Web.Internal
Imports DevExpress.Utils
Imports System.IO
Imports System.Drawing
Imports System.ComponentModel
Imports AjaxControlToolkit
Imports System.Globalization
Imports DevExpress.XtraPrinting
Imports DevExpress.Web
Imports DevExpress.Data
Imports System.Reflection
Imports System.Linq
Partial Class _REP_CC_PAYMENT_INFO
    Inherits System.Web.UI.Page
    Public ds1 As New DataSet()
    Protected dbad As New OleDbDataAdapter
    Public dbadc As New OleDbCommand
    Public conn As New Dbconn
    Dim strQuery As String = String.Empty
    Dim dstTempData As New DataSet
    Dim dataSource As IList
Dim strSessID As String
Dim wClause As String
Public vName As String
Dim count As Integer = 0
Dim strPF As String  
Dim strType As String   
Dim ReportSavePath As String = Server.MapPath("emailAttachments")   
Dim FQuery, TQuery As String    
Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
Try
If (Session("user_id") = "") Then
               Response.Redirect("login.aspx?s=o")
               Exit Sub
           End If
 dgQuickSearch.Attributes.Add("bordercolor", "white")
dgSort.Attributes.Add("bordercolor", "white")
dgApplyFilter.Attributes.Add("bordercolor", "white")
   Session(strSessID & "_dateFrom") = dateFrom.Text
   Session(strSessID & "_dateTo") = dateTo.Text
If (Not IsPostBack) Then
ddlGridPages.SelectedValue = 0      'Added By Bharath For No.of Records Per page
strType = "PDF"      'Added By Bharath
'Call FillGridQuickFilter()
call RECENT_TRANS()
 If Check_UserType() Then
                    btnEdit.Visible = True
                Else
                    btnEdit.Visible = False
                End If
Else
  Dim ds3 As New DataTable
 If Not Session(strSessID & "_QSData") Is Nothing Then
                ds3 = Session(strSessID & "_QSData")
                If Not ds3 Is Nothing Then
                    If ds3.Rows.Count > 0 Then
                        dataSource = ds3.DefaultView
                        searchCriteria.DataSource = dataSource
                        searchCriteria.DataBind()
                    End If
                End If
            End If
 Dim ds4 As New DataTable
                If Not Session(strSessID & "_SOData") Is Nothing Then
                    ds4 = Session(strSessID & "_SOData")
                    If Not ds4 Is Nothing Then
                        If ds4.Rows.Count > 0 Then
                            dataSource = ds4.DefaultView
                            sortCriteria.DataSource = dataSource
                            sortCriteria.DataBind()
                        End If
                    End If
                End If
End If
Apply_GroupFooter_Mode()
Session(strSessID &"_gridState") = ASPxGridView1.SaveClientLayout()
If rblPrintFormat.SelectedIndex = 0 Then
btnPrintS.Attributes.Add("OnClick", "window.open('REP_CC_PAYMENT_INFO_PRINT.aspx?Type=PRT&PF=PP','wd','width=600,Height=400,scrollbars=2,resizable=1,menubar=1,toolbar=1, ');return false;")
btnPdfPreview.Attributes.Add("OnClick", "window.open('REP_CC_PAYMENT_INFO_PRINT.aspx?Type=PDF&PF=PP','wd','width=1,Height=1,scrollbars=2,resizable=1,menubar=1,toolbar=1, ');return false;")
btnExcel.Attributes.Add("OnClick", "window.open('REP_CC_PAYMENT_INFO_PRINT.aspx?Type=XLS&PF=PP','wd','width=1,Height=1,scrollbars=2,resizable=1,menubar=1,toolbar=1, ');return false;")
 ElseIf rblPrintFormat.SelectedIndex = 1 Then
btnPrintS.Attributes.Add("OnClick", "window.open('REP_CC_PAYMENT_INFO_PRINT.aspx?Type=PRT&PF=PL','wd','width=600,Height=400,scrollbars=2,resizable=1,menubar=1,toolbar=1, ');return false;")
btnPdfPreview.Attributes.Add("OnClick", "window.open('REP_CC_PAYMENT_INFO_PRINT.aspx?Type=PDF&PF=PL','wd','width=1,Height=1,scrollbars=2,resizable=1,menubar=1,toolbar=1, ');return false;")
btnExcel.Attributes.Add("OnClick", "window.open('REP_CC_PAYMENT_INFO_PRINT.aspx?Type=XLS&PF=PL','wd','width=1,Height=1,scrollbars=2,resizable=1,menubar=1,toolbar=1, ');return false;")
 Else
btnPrintS.Attributes.Add("OnClick", "window.open('REP_CC_PAYMENT_INFO_PRINT.aspx?Type=PRT&PF=PL','wd','width=600,Height=400,scrollbars=2,resizable=1,menubar=1,toolbar=1, ');return false;")
btnPdfPreview.Attributes.Add("OnClick", "window.open('REP_CC_PAYMENT_INFO_PRINT.aspx?Type=PDF&PF=PL','wd','width=1,Height=1,scrollbars=2,resizable=1,menubar=1,toolbar=1, ');return false;")
btnExcel.Attributes.Add("OnClick", "window.open('REP_CC_PAYMENT_INFO_PRINT.aspx?Type=XLS&PF=PL','wd','width=1,Height=1,scrollbars=2,resizable=1,menubar=1,toolbar=1, ');return false;")
  End If
btnEdit.Attributes.Add("OnClick", "window.open('SYS_REPORTS_NEW.aspx?Report_ID=REP_CC_PAYMENT_INFO','wd','width=600,Height=400,scrollbars=2,resizable=1,menubar=1,toolbar=1, ');return false;")
btnChart.Attributes.Add("OnClick", "window.open('Chart_Report.aspx?Report_ID=REP_CC_PAYMENT_INFO','wd','width=900,Height=700,scrollbars=2,resizable=1,menubar=1,toolbar=1, ');return false;")
btnGroupBy.Attributes.Add("OnClick", "window.open('GroupBy_Report.aspx?Report_ID=REP_CC_PAYMENT_INFO','wd','width=900,Height=700,scrollbars=2,resizable=1,menubar=1,toolbar=1, ');return false;")
btnCtrBreak.Attributes.Add("OnClick", "window.open('ControlBreak_Report.aspx?Report_ID=REP_CC_PAYMENT_INFO','wd','width=900,Height=700,scrollbars=2,resizable=1,menubar=1,toolbar=1, ');return false;")
btnPvtReport.Attributes.Add("OnClick", "window.open('Cross_Tab_Report.aspx?Report_ID=REP_CC_PAYMENT_INFO','wd','width=900,Height=700,scrollbars=2,resizable=1,menubar=1,toolbar=1, ');return false;")
PopupControl.ContentUrl = "~/LIST_REPORT_OPTIONS.aspx?Report_ID=REP_CC_PAYMENT_INFO"
ASPxGridView1.SettingsBehavior.AllowDragDrop = False
rbl.Attributes.Add("OnClick", "setTextValueSC();")
  lstColumns.Attributes.Add("OnClick", "setTextValueCT();")
lblColumnname.Text = hdnLblValue.Value
Call Apply_Sort("N")
txtFltValue.Attributes.Add("onkeypress", "Return controlEnter('" + btnGo.ClientID + "', event)")
  ddlAlter.Attributes.Add("onchange", "return ShowAlWindow('" & ddlAlter.ClientID & "');")
Session(strSessID & "_whereCluase") = ""
  lblMessage.Visible = True
Catch ex As Exception
  Call insert_sys_log(RID.Value & " - " & "Page Load", ex.Message.ToString())
 End Try
End Sub
 Public Sub RECENT_TRANS()
Try
 Dim Rmenu as string= Request.QueryString.Get("mid") 
 Dim menu_catagory as string= "REPORTS" 
 If (rmenu<>"") then 
            dbad.SelectCommand = New OleDbCommand
            dbad.InsertCommand = New OleDbCommand
            dbad.UpdateCommand = New OleDbCommand
        dbad.SelectCommand.Connection = Session("db_connection")
        dbad.InsertCommand.Connection = Session("db_connection")
        dbad.UpdateCommand.Connection = Session("db_connection")
        Dim dsf121 As New DataSet
        dbad.SelectCommand.CommandText = "select MENU_CATEGORY FROM SYS_MENU_MASTER WHERE MENU_ID='" & Rmenu & "' "
        dsf121.Clear()
        dbad.Fill(dsf121)
        If (dsf121.Tables(0).Rows.Count > 0) Then
menu_catagory=dsf121.Tables(0).Rows(0)(0).Tostring()
            End If
        Dim dsf12 As New DataSet
        dbad.SelectCommand.CommandText = "select id from SYS_RECENT_TRANSACTIONS where PAGE_ID='REP_CC_PAYMENT_INFO' and USER_ID='" & Session("user_id") & "' "
        dsf12.Clear()
        dbad.Fill(dsf12)
        If (dsf12.Tables(0).Rows.Count > 0) Then
            If (dbad.UpdateCommand.Connection.State = ConnectionState.Closed) Then
                dbad.UpdateCommand.Connection.Open()
            End If
if (menu_catagory="REPORTS") Then
            dbad.UpdateCommand.CommandText = "Update SYS_RECENT_TRANSACTIONS set  CREATED_DATE=sysdate where PAGE_ID='REP_CC_PAYMENT_INFO' and USER_ID='" & Session("user_id") & "' "
Else
            dbad.UpdateCommand.CommandText = "Update SYS_RECENT_TRANSACTIONS set PRIMARY_KEY1='1',PRIMARY_KEY1_VALUE='1', CREATED_DATE=sysdate where PAGE_ID='REP_CC_PAYMENT_INFO' and USER_ID='" & Session("user_id") & "' "
End If
            dbad.UpdateCommand.ExecuteNonQuery()
        Else
            If (dbad.InsertCommand.Connection.State = ConnectionState.Closed) Then
                dbad.InsertCommand.Connection.Open()
            End If
if (menu_catagory="REPORTS") Then
            dbad.InsertCommand.CommandText = "INSERT INTO SYS_RECENT_TRANSACTIONS (ID, PAGE_ID, PAGE_NAME, MENU_ID, MENU_PARENT_LEVEL, USER_ID, CREATED_BY, CREATED_DATE) VALUES (" & conn.sf_get_Max_plus_one("SYS_RECENT_TRANSACTIONS", "ID", "") & ",'REP_CC_PAYMENT_INFO',(SELECT MENU_DISPLAY_NAME FROM SYS_MENU_MASTER WHERE MENU_ID='" & Request.QueryString.Get("mid") & "'),'" & Request.QueryString.Get("mid") & "','1','" & Session("user_id") & "','" & Session("user_id") & "',sysdate)"
Else
            dbad.InsertCommand.CommandText = "INSERT INTO SYS_RECENT_TRANSACTIONS (ID, PAGE_ID, PAGE_NAME, MENU_ID, MENU_PARENT_LEVEL, USER_ID, CREATED_BY, CREATED_DATE,PRIMARY_KEY1,PRIMARY_KEY1_VALUE) VALUES (" & conn.sf_get_Max_plus_one("SYS_RECENT_TRANSACTIONS", "ID", "") & ",'REP_CC_PAYMENT_INFO',(SELECT MENU_DISPLAY_NAME FROM SYS_MENU_MASTER WHERE MENU_ID='" & Request.QueryString.Get("mid") & "'),'" & Request.QueryString.Get("mid") & "','1','" & Session("user_id") & "','" & Session("user_id") & "',sysdate,'1','1')"
End If
            dbad.InsertCommand.ExecuteNonQuery()
        End If
        dbad.SelectCommand.Connection.Close()
        dbad.InsertCommand.Connection.Close()
        dbad.UpdateCommand.Connection.Close()
End If
Catch ex As Exception
  Call insert_sys_log(RID.Value & " - " & "Recent", ex.Message.ToString())
 End Try
    End Sub
Public Sub BindGrid()
Try
Dim wCluase As String = BuildWhereClause()
If Not String.IsNullOrEmpty(wCluase) Then
If wCluase <> "" Then
Session("qsGridData") = dstTempData
data_bind1(wCluase)
End If
Else
Call data_bind()
End If
ApplyLayout(Int32.Parse(ddlGroupBy.SelectedValue.ToString()))
Apply_GroupFooter_Mode()
Session(strSessID & "_gridState") = ASPxGridView1.SaveClientLayout()
Catch ex As Exception
Call insert_sys_log(RID.Value & " - " & "BindGrid", ex.Message.ToString())
End Try
End Sub
Public Sub Apply_GroupFooter_Mode()
Try
	lblMessage.Visible = True
	Dim mode As GridViewGroupFooterMode = CType(System.Enum.Parse(GetType(GridViewGroupFooterMode), ddlGroupFooter.Text), GridViewGroupFooterMode)
	ASPxGridView1.Settings.ShowGroupFooter = mode
	ASPxGridView1.SettingsPager.AlwaysShowPager = True
	ASPxGridView1.SettingsPager.Position = PagerPosition.TopAndBottom
	If ddlGridPages.SelectedValue.ToString <> "0" Then
		ASPxGridView1.SettingsPager.Mode = GridViewPagerMode.ShowPager
		ASPxGridView1.SettingsPager.PageSize = Integer.Parse(ddlGridPages.SelectedValue.ToString)
		ASPxGridView1.DataBind()
	Else		
		ASPxGridView1.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords		
		ASPxGridView1.DataBind()
	End If
Catch ex As Exception
	Call INSERT_SYS_LOG(RID.Value & " - " & "Apply_GroupFooter_Mode", ex.Message.ToString())
End Try
End Sub
   Public Shared Function CaseInsenstiveReplace(ByVal originalString As String, ByVal oldValue As String, ByVal newValue As String) As String
        Dim regEx As New System.Text.RegularExpressions.Regex(oldValue, RegexOptions.IgnoreCase Or RegexOptions.Multiline)
        Return regEx.Replace(originalString, newValue)
    End Function
Public Sub data_bind()
Try
            lblMessage.Visible = True
 Dim strSQLMain As String = "SELECT DOC_TYPE, DOC_NO, AMOUNT, RESPONSE, to_CHAR(TRANS_DATE,'MM/DD/YYYY') TRANS_DATE, API_TOKEN, CUSTOMER_NO, NAME, STATUS, USER_ID, CREATED_DATE,CUSTTOKEN,PMTOKEN,TRANRESULTCODE,TRANREFNUM,MASKEDCC,CCTYPE,PAYBYTYPE FROM V_CC_PAYMENT_INFO WHERE TRUNC(TRANS_DATE) BETWEEN TO_DATE('[FROMDATE]','MM/DD/YYYY') AND TO_DATE('[TODATE]','MM/DD/YYYY')  order by Tranrefnum"
Session(strSessID & "_whereCluase") = ""
Session(strSessID & "_repQuery") =   "SELECT DOC_TYPE, DOC_NO, AMOUNT, RESPONSE, to_CHAR(TRANS_DATE,'MM/DD/YYYY') TRANS_DATE, API_TOKEN, CUSTOMER_NO, NAME, STATUS, USER_ID, CREATED_DATE,CUSTTOKEN,PMTOKEN,TRANRESULTCODE,TRANREFNUM,MASKEDCC,CCTYPE,PAYBYTYPE FROM V_CC_PAYMENT_INFO WHERE TRUNC(TRANS_DATE) BETWEEN TO_DATE('[FROMDATE]','MM/DD/YYYY') AND TO_DATE('[TODATE]','MM/DD/YYYY')  order by Tranrefnum"
dbad.SelectCommand.CommandText = Session(strSessID & "_repQuery").ToString()
dbad.SelectCommand.CommandText = Session(strSessID & "_repQuery").ToString().Replace("[TODATE]", dateTo.Text).Replace("[FROMDATE]", dateFrom.Text)
Session(strSessID & "_repQueryCB") = strSQLMain.Replace("[TODATE]", dateTo.Text).Replace("[FROMDATE]", dateFrom.Text)
Session(strSessID & "_repQueryWc") = dbad.SelectCommand.CommandText.ToString()
Dim LINK_TAG_PATTERN As String = "<[a|A][^>]*>|</[a|A]>"
Session(strSessID & "_repQueryWc") = System.Text.RegularExpressions.Regex.Replace(dbad.SelectCommand.CommandText.ToString(), LINK_TAG_PATTERN, "", RegexOptions.IgnoreCase)
dbad.SelectCommand.Connection = Session("db_connection")
ds1.Clear()
  If dbad.SelectCommand.Connection.State = ConnectionState.Closed Then
 dbad.SelectCommand.Connection.Open()
 End If
dbad.Fill(ds1)
If dbad.SelectCommand.Connection.State = ConnectionState.Open Then
dbad.SelectCommand.Connection.Close()
End If
    ASPxGridView1.DataSource = ds1.Tables(0)
    ASPxGridView1.DataBind()
    Session(strSessID & "REP_CC_PAYMENT_INFO") = ds1
Session(strSessID & "_gridState") = ASPxGridView1.SaveClientLayout()
If ds1.Tables(0).Rows.Count > 0 Then
lblMessage.Text = "Total No Of Records: " & ds1.Tables(0).Rows.Count
Else
lblMessage.Text = "Total No Of Records: 0" 
    End If
Catch ex As Exception
Call insert_sys_log(RID.Value & " - " & "data_bind", ex.Message.ToString())
End Try
End Sub
Public Sub data_bind1(ByVal wCluase As String)
Try
            lblMessage.Visible = True
If Not String.IsNullOrEmpty(wCluase) Then
Session(strSessID & "_whereCluase") = wCluase
Session(strSessID & "_repQuery") = "SELECT DOC_TYPE, DOC_NO, AMOUNT, RESPONSE, to_CHAR(TRANS_DATE,'MM/DD/YYYY') TRANS_DATE, API_TOKEN, CUSTOMER_NO, NAME, STATUS, USER_ID, CREATED_DATE,CUSTTOKEN,PMTOKEN,TRANRESULTCODE,TRANREFNUM,MASKEDCC,CCTYPE,PAYBYTYPE FROM V_CC_PAYMENT_INFO WHERE TRUNC(TRANS_DATE) BETWEEN TO_DATE('[FROMDATE]','MM/DD/YYYY') AND TO_DATE('[TODATE]','MM/DD/YYYY')  order by Tranrefnum"
Dim strQuery1 As String = DirectCast(Session(strSessID & "_repQuery"), String)
strQuery = String.Empty
If strQuery1.IndexOf("WHERE", 0, StringComparison.CurrentCultureIgnoreCase) <> -1 Then
    strQuery = CaseInsenstiveReplace(strQuery1, "where", " WHERE " & wCluase & " AND ")
ElseIf strQuery1.IndexOf("group by", 0, StringComparison.CurrentCultureIgnoreCase) <> -1 Then
    strQuery = CaseInsenstiveReplace(strQuery1, "group by", " WHERE " & wCluase & " GROUP BY ")
ElseIf strQuery1.IndexOf("order by", 0, StringComparison.CurrentCultureIgnoreCase) <> -1 Then
    strQuery = CaseInsenstiveReplace(strQuery1, "order by", " WHERE " & wCluase & " ORDER BY ")
Else
    strQuery = String.Concat(strQuery1, " WHERE ", wCluase)
End If
 strQuery = "select * from (" & strQuery1.Replace("[TODATE]", dateTo.Text).Replace("[FROMDATE]", dateFrom.Text) & ") where " & wCluase
Session(strSessID & "_repQueryWc") = strQuery
Dim LINK_TAG_PATTERN As String = "<[a|A][^>]*>|</[a|A]>"
Session(strSessID & "_repQueryWc") = System.Text.RegularExpressions.Regex.Replace(strQuery, LINK_TAG_PATTERN, "", RegexOptions.IgnoreCase)
dbad.SelectCommand.Connection = Session("db_connection")
dbad.SelectCommand.CommandText = strQuery
ds1.Clear()
  If dbad.SelectCommand.Connection.State = ConnectionState.Closed Then
 dbad.SelectCommand.Connection.Open()
 End If
dbad.Fill(ds1)
If dbad.SelectCommand.Connection.State = ConnectionState.Open Then
dbad.SelectCommand.Connection.Close()
End If
ASPxGridView1.DataSource = ds1.Tables(0)
ASPxGridView1.DataBind()
Session(strSessID & "REP_CC_PAYMENT_INFO") = ds1
Else
Session(strSessID & "_whereCluase") = ""
Call data_bind()
End If
Session(strSessID & "_gridState") = ASPxGridView1.SaveClientLayout()
If ds1.Tables(0).Rows.Count > 0 Then
lblMessage.Text = "Total No Of Records: " & ds1.Tables(0).Rows.Count
Else
lblMessage.Text = "Total No Of Records: 0" 
    End If
Catch ex As Exception
Call insert_sys_log(RID.Value & " - " & "data_bind1", ex.Message.ToString())
End Try
End Sub
Private Sub FillGridQuickFilter()
Try
Dim dsExternal As New DataSet()
strQuery = String.Empty
dbad.SelectCommand.Connection = Session("db_connection")
strQuery = "SELECT NVL(FIELD_LABEL, FIELD_NAME) AS FIELD_LABEL,FIELD_NAME, DEFAULT_VALUE,FIELD_TYPE,DECODE(NVL(FIELD_OPERATOR,'='),'N','=',NVL(FIELD_OPERATOR,'=')) AS FIELD_OPERATOR  FROM SYS_REPORT_FILTER WHERE COLUMN_ACTIVE='Y' AND REPORT_ID=  '" & RID.Value & "'"
dbad.SelectCommand.CommandText = strQuery
dsExternal.Clear()
If dbad.SelectCommand.Connection.State = ConnectionState.Closed Then
dbad.SelectCommand.Connection.Open()
End If
dbad.Fill(dsExternal)
If dbad.SelectCommand.Connection.State = ConnectionState.Open Then
dbad.SelectCommand.Connection.Close()
End If
If Not dsExternal Is Nothing Then
If dsExternal.Tables(0).Rows.Count > 0 Then
lstColumns.Items.Clear()
lstColumns.Items.Add(New ListItem("All", "All", True))
For j As Integer = 0 To dsExternal.Tables(0).Rows.Count - 1
If Not IsDBNull(dsExternal.Tables(0).Rows(j)("FIELD_NAME").ToString) AndAlso Not String.IsNullOrEmpty(dsExternal.Tables(0).Rows(j)("FIELD_NAME").ToString) Then
If dsExternal.Tables(0).Rows(j)("FIELD_TYPE").ToString <> "DATE" Then
    lstColumns.Items.Add(New ListItem(dsExternal.Tables(0).Rows(j)("FIELD_LABEL").ToString, dsExternal.Tables(0).Rows(j)("FIELD_NAME"), True))
End If
End If
Next
    btnClear.Visible = True
    PnlContent.Visible = True
pnlNoSearch.Visible = False
tblSearch.Visible = True
    Session("qsGridData") = dsExternal
    dgQuickSearch.DataSource = dsExternal.Tables(0)
    dgQuickSearch.DataBind()
btnFilter.Visible = True
GlobalSearch.Visible = True
Else
btnFilter.Visible = False
GlobalSearch.Visible = False
 btnClear.Visible = False
tblSearch.Visible = False
pnlNoSearch.Visible = True
    PnlContent.Visible = False
End If
Else
btnFilter.Visible = False
GlobalSearch.Visible = False
 btnClear.Visible = False
tblSearch.Visible = False
pnlNoSearch.Visible = True
PnlContent.Visible = False
End If
Catch ex As Exception
Call insert_sys_log(RID.Value & " - " & "FillGridQuickFilter", ex.Message.ToString())
End Try
End Sub
 Protected Sub dgQuickSearch_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgQuickSearch.ItemDataBound
        Try
            If (e.Item.ItemType = ListItemType.Pager) OrElse (e.Item.ItemType = ListItemType.Header) OrElse (e.Item.ItemType = ListItemType.Footer) OrElse (e.Item.ItemType = ListItemType.Item) OrElse (e.Item.ItemType = ListItemType.AlternatingItem) Then
                e.Item.Cells(1).CssClass = "hiddencol"
                e.Item.Cells(4).CssClass = "hiddencol"
            End If
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                Dim intRow As Integer = e.Item.ItemIndex
                If intRow = -1 Then
                    Exit Sub
                End If
                If (Session("qsGridData") IsNot Nothing) Then
                    dstTempData = DirectCast(Session("qsGridData"), DataSet)
                    Dim CurrentDrop As String
                    Dim dValue As String = dstTempData.Tables(0).Rows(intRow)("Default_value").ToString
                    Dim dValue1 As String() = dValue.Split(",")
CurrentDrop = StrConv(dstTempData.Tables(0).Rows(intRow)("FIELD_OPERATOR").ToString, VbStrConv.ProperCase)
If CurrentDrop Is Nothing Then
CurrentDrop = "="
Else
If String.IsNullOrEmpty(CurrentDrop) Then
CurrentDrop = "="
End If
End If
                    Dim torD As String = dstTempData.Tables(0).Rows(intRow)("FIELD_TYPE").ToString().ToUpper
                    Dim itemSub As ListItem = Nothing
                    Dim lblFLabel As Label = DirectCast(e.Item.Cells(0).FindControl("lblFLabel"), Label)
                    Dim lblFname As Label = DirectCast(e.Item.Cells(1).FindControl("lblFName"), Label)
                    Dim ddlDGDrop As DropDownList = DirectCast(e.Item.Cells(2).FindControl("ddlDrop"), DropDownList)
                    Dim ddlDate As DropDownList = DirectCast(e.Item.Cells(2).FindControl("ddlDate"), DropDownList)
                    Dim txtExp As TextBox = DirectCast(e.Item.Cells(3).FindControl("expression"), TextBox)
                    Dim txtExp2 As TextBox = DirectCast(e.Item.Cells(3).FindControl("expression2"), TextBox)
Dim frmDate As ASPxDateEdit = DirectCast(e.Item.Cells(3).FindControl("frmDate"), ASPxDateEdit)
Dim toDate As ASPxDateEdit = DirectCast(e.Item.Cells(3).FindControl("toDate"), ASPxDateEdit)
Dim imgB As ImageButton = CType(e.Item.Cells(5).FindControl("imgBtn1"), ImageButton)
                    lblFLabel.Text = dstTempData.Tables(0).Rows(intRow)("FIELD_LABEL").ToString()
                    lblFname.Text = dstTempData.Tables(0).Rows(intRow)("Field_Name").ToString()
ddlDate.Attributes.Add("onchange", "return SetDDDates('" & ddlDate.ClientID & "','" & frmDate.ClientID & "','" & toDate.ClientID & "');")
ddlDGDrop.Attributes.Add("onchange", "return SetDDVar('" & ddlDGDrop.ClientID & "','" & txtExp.ClientID & "','" & txtExp2.ClientID & "');")
                    If torD = "DATE" Then
           imgB.Visible = False
                        ddlDate.Visible = True
                        BindListControlsDate(ddlDate)
                        ddlDGDrop.Visible = False
                        itemSub = ddlDate.Items.FindByText(CurrentDrop)
                        txtExp.Visible = False
                        txtExp2.Visible = False
                        frmDate.Visible = True
                        txtExp.CssClass = "hiddencol"
                        If itemSub.Text = "Between" Then
                            toDate.Visible = True
                        Else
                      toDate.CssClass = "hiddencol"
                        End If
                    Else
                        ddlDate.Visible = False
                        BindListControls(ddlDGDrop)
                        ddlDGDrop.Visible = True
                        itemSub = ddlDGDrop.Items.FindByText(CurrentDrop)
frmDate.CssClass = "hiddencol"
toDate.CssClass = "hiddencol"
                        txtExp.Visible = True
                        If itemSub.Text = "Between" Then
imgB.Visible = False
                            txtExp2.Visible = True
                        Else
 txtExp2.CssClass = "hiddencol"
                        End If
                    End If
                    If itemSub.Text = "Is Null" Or itemSub.Text = "Is Not Null" Then
  imgB.Visible = False
                        txtExp.Enabled = False
                        txtExp2.Enabled = False
                        frmDate.Enabled = False
                    End If
                    If (itemSub IsNot Nothing) Then
                        itemSub.Selected = True
                    End If
                End If
            End If
        Catch ex As Exception
Call insert_sys_log(RID.Value & " - " & "dgQuickSearch_ItemDataBound", ex.Message.ToString())
        Finally
        End Try
    End Sub
Private Sub BindListControls(ByVal ddlDrop As DropDownList)
Try
        Dim lstType As ListItem = Nothing
        lstType = New ListItem("=", "=")
        ddlDrop.Items.Insert(0, lstType)
        lstType = New ListItem("!=", "!=")
        ddlDrop.Items.Insert(1, lstType)
        lstType = New ListItem("Contains", "contains")
        ddlDrop.Items.Insert(2, lstType)
        lstType = New ListItem("Does Not Contain", "does not contain")
        ddlDrop.Items.Insert(3, lstType)
        lstType = New ListItem("Starts With", "starts with")
        ddlDrop.Items.Insert(4, lstType)
        lstType = New ListItem("Ends With", "ends with")
        ddlDrop.Items.Insert(5, lstType)
        lstType = New ListItem("In", "in")
        ddlDrop.Items.Insert(6, lstType)
        lstType = New ListItem("Not In", "not in")
        ddlDrop.Items.Insert(7, lstType)
        lstType = New ListItem("Is Null", "is null")
        ddlDrop.Items.Insert(8, lstType)
        lstType = New ListItem("Is Not Null", "is not null")
        ddlDrop.Items.Insert(9, lstType)
        lstType = New ListItem("Between", "between")
        ddlDrop.Items.Insert(10, lstType)
        lstType = New ListItem(">", ">")
        ddlDrop.Items.Insert(11, lstType)
        lstType = New ListItem("<", "<")
        ddlDrop.Items.Insert(12, lstType)
        lstType = New ListItem(">=", ">=")
        ddlDrop.Items.Insert(13, lstType)
        lstType = New ListItem("<=", "<=")
        ddlDrop.Items.Insert(14, lstType)
Catch ex As Exception
Call insert_sys_log(RID.Value & " - " & "BindListControls", ex.Message.ToString())
End Try
    End Sub
 Private Sub BindListControlsDate(ByVal ddlDrop As DropDownList)
Try
        Dim lstType As ListItem = Nothing
        lstType = New ListItem("Between", "between")
        ddlDrop.Items.Insert(0, lstType)
        lstType = New ListItem("=", "=")
        ddlDrop.Items.Insert(1, lstType)
        lstType = New ListItem("!=", "!=")
        ddlDrop.Items.Insert(2, lstType)
        lstType = New ListItem(">", ">")
        ddlDrop.Items.Insert(3, lstType)
        lstType = New ListItem("<", "<")
        ddlDrop.Items.Insert(4, lstType)
        lstType = New ListItem(">=", ">=")
        ddlDrop.Items.Insert(5, lstType)
        lstType = New ListItem("<=", "<=")
        ddlDrop.Items.Insert(6, lstType)
        lstType = New ListItem("Is Null", "is null")
        ddlDrop.Items.Insert(7, lstType)
        lstType = New ListItem("Is Not Null", "is not null")
        ddlDrop.Items.Insert(8, lstType)
Catch ex As Exception
Call insert_sys_log(RID.Value & " - " & "BindListControlsDate", ex.Message.ToString())
End Try
    End Sub
  Protected Sub ddlDate_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
Try
        Dim ddllist As DropDownList = CType(sender, DropDownList)
        Dim cell As TableCell = CType(ddllist.Parent, TableCell)
        Dim item As DataGridItem = CType(cell.Parent, DataGridItem)
        Dim ddlType As DropDownList = CType(item.Cells(0).FindControl("ddlDate"), DropDownList)
        Dim tExp As TextBox = CType(item.Cells(2).FindControl("expression"), TextBox)
        Dim tExp2 As TextBox = CType(item.Cells(2).FindControl("expression2"), TextBox)
Dim t1 As ASPxDateEdit = DirectCast(item.Cells(2).FindControl("frmDate"), ASPxDateEdit)
Dim t2 As ASPxDateEdit = DirectCast(item.Cells(2).FindControl("toDate"), ASPxDateEdit)
Dim imgB As ImageButton = CType(item.Cells(5).FindControl("imgBtn1"), ImageButton)
        tExp.Visible = False
        tExp2.Visible = False
        Select Case StrConv(ddlType.SelectedValue, VbStrConv.ProperCase)
            Case "Between"
imgB.Visible = False
                t1.Visible = True
                t1.Enabled = True
                t2.Visible = True
                t2.Enabled = True              
            Case "Is Null"
imgB.Visible = False
                t1.Text = ""
                t1.Visible = True
                t1.Enabled = False
                t2.Visible = False
            Case "Is Not Null"
imgB.Visible = False
                t1.Text = ""
                t1.Visible = True
                t1.Enabled = False
                t2.Visible = False
            Case Else
imgB.Visible = False
                t1.Enabled = True
                t2.Enabled = False
                t1.Visible = True
                t2.Visible = False              
        End Select
Catch ex As Exception
Call insert_sys_log(RID.Value & " - " & "ddlDate_SelectedIndexChanged", ex.Message.ToString())
End Try
    End Sub
 Protected Sub ddlVarchar_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
Try
        Dim ddllist As DropDownList = CType(sender, DropDownList)
        Dim cell As TableCell = CType(ddllist.Parent, TableCell)
        Dim item As DataGridItem = CType(cell.Parent, DataGridItem)
        Dim ddlType As DropDownList = CType(item.Cells(0).FindControl("ddlDrop"), DropDownList)
        Dim tExp As TextBox = CType(item.Cells(2).FindControl("expression"), TextBox)
        Dim tExp2 As TextBox = CType(item.Cells(2).FindControl("expression2"), TextBox)
Dim t1 As ASPxDateEdit = DirectCast(item.Cells(2).FindControl("frmDate"), ASPxDateEdit)
Dim t2 As ASPxDateEdit = DirectCast(item.Cells(2).FindControl("toDate"), ASPxDateEdit)
Dim imgB As ImageButton = CType(item.Cells(5).FindControl("imgBtn1"), ImageButton)
        t1.Visible = False
        t2.Visible = False
        tExp2.Visible = False
        tExp.Enabled = False
        Select Case StrConv(ddlType.SelectedValue, VbStrConv.ProperCase)
            Case "Between"
 imgB.Visible = False
                tExp.Visible = True
                tExp2.Visible = True
                tExp.Enabled = True
                tExp2.Enabled = True
            Case "Is Null"
 imgB.Visible = False
                tExp.Text = ""
                tExp.Attributes.Add("class", "titleTextclsDisabled")
                tExp.Visible = True
                tExp.Enabled = False
            Case "Is Not Null"
 imgB.Visible = False
                tExp.Text = ""
                tExp.Visible = True
                tExp.Enabled = False
            Case Else
 imgB.Visible = True
                tExp.Visible = True
                tExp.Enabled = True
        End Select
Catch ex As Exception
Call insert_sys_log(RID.Value & " - " & "ddlVarchar_SelectedIndexChanged", ex.Message.ToString())
End Try
    End Sub
 Protected Sub btnApply_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnApply.Click
        Try
            Call BindGrid()
 Call Apply_Sort("Y")
 pnldgApply.Visible = False
            Session(strSessID & "_gridState") = ASPxGridView1.SaveClientLayout()
        Catch ex As Exception
Call insert_sys_log(RID.Value & " - " & "btnApply_Click", ex.Message.ToString())
        End Try
    End Sub
  Public Function BuildWhereClause() As String
        Dim tbl As New DataTable()
        Dim dcFType As New DataColumn("FType", System.Type.[GetType]("System.String"))
        Dim dcFname As New DataColumn("FieldName", System.Type.[GetType]("System.String"))
        Dim dcOperator As New DataColumn("Operator", System.Type.[GetType]("System.String"))
        Dim dcExpression As New DataColumn("Expression", System.Type.[GetType]("System.String"))
        Dim dcFltAply As New DataColumn("FltAply", System.Type.[GetType]("System.String"))
        Dim dcColor As New DataColumn("FltColor", System.Type.[GetType]("System.String"))
        tbl.Columns.Add(dcFname)
        tbl.Columns.Add(dcOperator)
        tbl.Columns.Add(dcExpression)
        tbl.Columns.Add(dcFltAply)
 tbl.Columns.Add(dcFType)
tbl.Columns.Add(dcColor)
        tbl.Columns(0).ColumnName = "FieldName"
        tbl.Columns(1).ColumnName = "Operator"
        tbl.Columns(2).ColumnName = "Expression"
        tbl.Columns(3).ColumnName = "FltAply"
  tbl.Columns(4).ColumnName = "FltType"
tbl.Columns(5).ColumnName = "FltColor"
        Dim dr As DataRow
        Dim strWClause As String = String.Empty
        Dim count As Integer = 0
        Dim strOperator As String = String.Empty
        Dim strExp As String = String.Empty
        Dim strDtype As String = String.Empty
        Dim opt As String = String.Empty
        Dim isApply As String = String.Empty
        Try
            For Each dgItem As DataGridItem In dgQuickSearch.Items
                Dim intRow As Integer = dgItem.ItemIndex
                If intRow <> -1 Then
                    dr = tbl.NewRow()
                    Dim strValues As String = String.Empty
                    Dim lblFLabel As Label = DirectCast(dgItem.Cells(1).FindControl("lblFLabel"), Label)
                    Dim lblFname As Label = DirectCast(dgItem.Cells(2).FindControl("lblFName"), Label)
                    Dim ddlDGDrop As DropDownList = DirectCast(dgItem.Cells(2).FindControl("ddlDrop"), DropDownList)
                    Dim ddlDate As DropDownList = DirectCast(dgItem.Cells(2).FindControl("ddlDate"), DropDownList)
                    Dim exp As TextBox = DirectCast(dgItem.Cells(3).FindControl("expression"), TextBox)
                    Dim exp2 As TextBox = DirectCast(dgItem.Cells(3).FindControl("expression2"), TextBox)
Dim frmDate As ASPxDateEdit = DirectCast(dgItem.Cells(3).FindControl("frmDate"), ASPxDateEdit)
Dim toDate As ASPxDateEdit = DirectCast(dgItem.Cells(3).FindControl("toDate"), ASPxDateEdit)
                    Dim type As String = dgItem.Cells(4).Text
                    If type.ToUpper = "Date".ToUpper Then
            frmDate.Visible = True
                        If ddlDate.SelectedValue = "between" Then
toDate.Style.Add("display", "block")
 toDate.CssClass = "GridTextBox"
                            strValues = frmDate.Text + " And " + toDate.Text
                        Else
toDate.CssClass = "hiddencol"
  toDate.Style.Add("display", "none")
                            strValues = frmDate.Text
                        End If
                        strOperator = ddlDate.SelectedValue
                    Else
exp.Visible = True
                        If ddlDGDrop.SelectedValue = "between" Then
exp2.Style.Add("display", "block")
                     exp2.CssClass = "GridTextBox"
                            strValues = exp.Text + " And " + exp2.Text
                        Else
 exp2.CssClass = "hiddencol"
                     exp2.Style.Add("display", "none")
                            strValues = exp.Text
                        End If
                        'exp.Text = strVales
                        strOperator = ddlDGDrop.SelectedValue
                    End If
                    If (strValues <> "") Then
                        strExp = AddUpperQS(strOperator, strValues.Replace("'", "''"), type)
                    Else
                        strExp = ""
                    End If
                    Select Case strOperator
                        Case "contains"
                            opt = "Like"
                        Case "does not contain"
                            opt = "Not Like"
                        Case "starts with"
                            opt = "Like"
                        Case "ends with"
                            opt = "Like"
                        Case Else
                            opt = strOperator
                    End Select
                    Dim fltAppl As String = IsApplySearch(type, strOperator, exp.Text, exp2.Text, frmDate.Text, toDate.Text)
                   Dim strColName As String = String.Concat(" UPPER(", lblFname.Text, ") ")
                    If (fltAppl = "1") Then
                        If count = 0 Then
                            strWClause = String.Concat(strColName, " ", opt, " ", strExp)
                        Else
                            strWClause = String.Concat(strWClause, " and ", strColName, " ", opt, " ", strExp)
                        End If
                        count = count + 1
                    End If
                     dr("FieldName") = lblFLabel.Text
                    dr("Operator") = opt
                    dr("Expression") = strValues
                    If (fltAppl = "1") Then
                        dr("FltAply") = "Yes"
                    Else
                        dr("FltAply") = "No"
                    End If
dr("FltType") = "FILTER"
dr("FltColor") = "White"
                    If (fltAppl = "1") Then
                    tbl.Rows.Add(dr)
                    End If
                End If
            Next
If tbl.Rows.Count > 0 Then
	Session(strSessID & "filterGridData") = tbl
	dgApplyFilter.DataSource = tbl
	dgApplyFilter.DataBind()
	pnldgApply.Visible = True
    Else
	pnldgApply.Visible = False
    End If
Dim dtnew As New DataTable
dtnew = Get_Formating_Rules(tbl)
Session(strSessID & "_QSData") = dtnew
searchCriteria.DataSource = dtnew
searchCriteria.DataBind()
        Catch ex As Exception
Call insert_sys_log(RID.Value & " - " & "BuildWhereClause", ex.Message.ToString())
        End Try
Session(strSessID & "_repQueryWClause") = strWClause
        Return strWClause
    End Function
Public Function IsApplySearch(ByVal Type As String, ByVal fltOperator As String, ByVal exp As String, ByVal exp2 As String, ByVal frmDate As String, ByVal toDate As String) As String
        Dim fltApplicable As String = String.Empty
Try
        Select Case StrConv(Type, VbStrConv.Uppercase)
            Case "DATE"
                Select Case StrConv(fltOperator, VbStrConv.ProperCase)
                    Case "!="
                        If frmDate.Trim <> String.Empty Then
                            fltApplicable = 1
                        Else
                            fltApplicable = 0
                        End If
                    Case "="
                        If frmDate.Trim <> String.Empty Then
                            fltApplicable = 1
                        Else
                            fltApplicable = 0
                        End If
                    Case ">"
                        If frmDate.Trim <> String.Empty Then
                            fltApplicable = 1
                        Else
                            fltApplicable = 0
                        End If
                    Case "<"
                        If frmDate.Trim <> String.Empty Then
                            fltApplicable = 1
                        Else
                            fltApplicable = 0
                        End If
                    Case ">="
                        If frmDate.Trim <> String.Empty Then
                            fltApplicable = 1
                        Else
                            fltApplicable = 0
                        End If
                    Case "<="
                        If frmDate.Trim <> String.Empty Then
                            fltApplicable = 1
                        Else
                            fltApplicable = 0
                        End If
                    Case "Between"
                        If (frmDate.Trim <> String.Empty AndAlso toDate.Trim <> String.Empty) Then
                            fltApplicable = 1
                        ElseIf (frmDate.Trim <> String.Empty Or toDate.Trim <> String.Empty) Then
                            fltApplicable = 0
                        Else
                            fltApplicable = 0
                        End If
                    Case "Is Not Null"
                        fltApplicable = 1
                    Case "Is Null"
                        fltApplicable = 1
                    Case Else
                        fltApplicable = 0
                End Select
            Case Else
                Select Case StrConv(fltOperator, VbStrConv.ProperCase)
                    Case "Contains"
                        If exp.Trim <> String.Empty Then
                            fltApplicable = 1
                        Else
                            fltApplicable = 0
                        End If
                    Case "Does Not Contain"
                        If exp.Trim <> String.Empty Then
                            fltApplicable = 1
                        Else
                            fltApplicable = 0
                        End If
                    Case "!="
                        If exp.Trim <> String.Empty Then
                            fltApplicable = 1
                        Else
                            fltApplicable = 0
                        End If
                    Case "="
                        If exp.Trim <> String.Empty Then
                            fltApplicable = 1
                        Else
                            fltApplicable = 0
                        End If
                    Case "Starts With"
                        If exp.Trim <> String.Empty Then
                            fltApplicable = 1
                        Else
                            fltApplicable = 0
                        End If
                    Case "Ends With"
                        If exp.Trim <> String.Empty Then
                            fltApplicable = 1
                        Else
                            fltApplicable = 0
                        End If
                    Case "In"
                        If exp.Trim <> String.Empty Then
                            fltApplicable = 1
                        Else
                            fltApplicable = 0
                        End If
                    Case "Not In"
                        If exp.Trim <> String.Empty Then
                            fltApplicable = 1
                        Else
                            fltApplicable = 0
                        End If
                    Case ">"
                        If exp.Trim <> String.Empty Then
                            fltApplicable = 1
                        Else
                            fltApplicable = 0
                        End If
                    Case ">="
                        If exp.Trim <> String.Empty Then
                            fltApplicable = 1
                        Else
                            fltApplicable = 0
                        End If
                    Case "<"
                        If exp.Trim <> String.Empty Then
                            fltApplicable = 1
                        Else
                            fltApplicable = 0
                        End If
                    Case "<="
                        If exp.Trim <> String.Empty Then
                            fltApplicable = 1
                        Else
                            fltApplicable = 0
                        End If
                    Case "Is Not Null"
                        fltApplicable = 1
                    Case "Is Null"
                        fltApplicable = 1
                    Case "Between"
                        If (exp.Trim <> String.Empty AndAlso exp2.Trim <> String.Empty) Then
                            fltApplicable = 1
                        ElseIf (exp.Trim <> String.Empty Or exp2.Trim <> String.Empty) Then
                            fltApplicable = 0
                        Else
                            fltApplicable = 0
                        End If
                    Case Else
                        fltApplicable = 0
                End Select
        End Select
Catch ex As Exception
Call insert_sys_log(RID.Value & " - " & "IsApplySearch", ex.Message.ToString())
End Try
        Return fltApplicable
    End Function
Public Function AddUpperQS(ByVal Operator1 As String, ByVal Expression As String, ByVal dType As String) As String
        Dim str As String = ""
Try
        Select Case StrConv(dType, VbStrConv.Uppercase)
            Case "DATE"
                Select Case StrConv(Operator1, VbStrConv.ProperCase)
                    Case "!="
                        str = String.Concat("to_date('", Expression, "','mm/dd/yyyy')")
                    Case "="
                        str = String.Concat("to_date('", Expression, "','mm/dd/yyyy')")
                    Case ">"
                        str = String.Concat("to_date('", Expression, "','mm/dd/yyyy')")
                    Case "<"
                        str = String.Concat("to_date('", Expression, "','mm/dd/yyyy')")
                    Case ">="
                        str = String.Concat("to_date('", Expression, "','mm/dd/yyyy')")
                    Case "<="
                        str = String.Concat("to_date('", Expression, "','mm/dd/yyyy')")
                    Case "Between"
                        Dim strDates As String() = Expression.Split(" and ")
                        If strDates.Length > 2 Then
                            str = String.Concat("to_date('", strDates(0), "','mm/dd/yyyy') and ", "to_date('", strDates(2), "','mm/dd/yyyy')")
                        Else
                            str = String.Concat("to_date('", strDates(0), "',''m/dd/yyyy') and ", "to_date('','mm/dd/yyyy')")
                        End If
                    Case Else
                        str = ""
                End Select
Case "NUMBER"
                    Select Case StrConv(Operator1, VbStrConv.ProperCase)
                        Case "Contains"
                            str = String.Concat("'%", Expression, "%'")
                        Case "Does Not Contain"
                            str = String.Concat("'%", Expression, "%'")
                        Case "!="
                            str = Expression
                        Case "="
                            str = Expression
                        Case ">"
                            str = Expression
                        Case ">="
                            str = Expression
                        Case "<"
                            str = Expression
                        Case "<="
                            str = Expression
                        Case "Starts With"
                            str = String.Concat("'", Expression, "%'")
                        Case "Ends With"
                            str = String.Concat("'%", Expression, "'")
                        Case "Between"
                            Dim strNo As String() = Expression.Split(" and ")

                            If strNo.Length > 2 Then
                                str = String.Concat(strNo(0), " and ", strNo(2))
                            Else
                                str = String.Concat(strNo(0), " and ", " ")
                            End If
                        Case "In"
                            Dim strExp = Expression.Split(",")
                            Dim count As Integer = 0

                            For i As Integer = 0 To strExp.Length - 1
                                If count = 0 Then
                                    str = strExp(i)
                                Else
                                    str = String.Concat(str, ",", strExp(i))
                                End If
                                count = count + 1
                            Next i
                            str = String.Concat("(", str, ")")
                        Case "Not In"
                            Dim strExp = Expression.Split(",")
                            Dim count As Integer = 0

                            For i As Integer = 0 To strExp.Length - 1
                                If count = 0 Then
                                    str = strExp(i)
                                Else
                                    str = String.Concat(str, ",", strExp(i))
                                End If
                                count = count + 1
                            Next i
                            str = String.Concat("(", str, ")")
                        Case Else
                            str = ""
                    End Select
            Case Else
                Select Case StrConv(Operator1, VbStrConv.ProperCase)
                    Case "Between"
                        'Split(dsData2.Tables(0).Rows(0)(0).ToString.ToUpper, " FROM ", , CompareMethod.Text)
                        'Dim strVar As String() = Expression.Split(" and ")
                        Dim strVar As String() = Split(Expression, " and ", , CompareMethod.Text)
                        If strVar.Length > 1 Then
                            str = String.Concat("'", strVar(0), "' and ", "'", strVar(1), "'")
                        Else
                            str = String.Concat("'", strVar(0), "' and ", "''")
                        End If
                    Case ">"
                        str = String.Concat("upper('", Expression, "')")
                    Case "<"
                        str = String.Concat("upper('", Expression, "')")
                    Case ">="
                        str = String.Concat("upper('", Expression, "')")
                    Case "<="
                        str = String.Concat("upper('", Expression, "')")
                    Case "Contains"
                        str = String.Concat("(upper('%", Expression, "%'))")
                    Case "Does Not Contain"
                        str = String.Concat("(upper('%", Expression, "%'))")
                    Case "!="
                        str = String.Concat("(upper('", Expression, "'))")
                    Case "="
                        str = String.Concat("(upper('", Expression, "'))")
                    Case "Starts With"
                        str = String.Concat("(upper('", Expression, "%'))")
                    Case "Ends With"
                        str = String.Concat("(upper('%", Expression, "'))")
                    Case "In"
                        Dim strExp = Expression.Split(",")
                        Dim count As Integer = 0
                        For i = 0 To strExp.Length - 1
                            strExp(i) = String.Concat("upper('", strExp(i), "')")
                            If count = 0 Then
                                str = strExp(i)
                            Else
                                str = String.Concat(str, ",", strExp(i))
                            End If
                            count = count + 1
                        Next i
                        str = String.Concat("(", str, ")")
                    Case "Not In"
                        Dim strExp = Expression.Split(",")
                        Dim count As Integer = 0
                        For i = 0 To strExp.Length - 1
                            strExp(i) = String.Concat("upper('", strExp(i), "')")
                            If count = 0 Then
                                str = strExp(i)
                            Else
                                str = String.Concat(str, ",", strExp(i))
                            End If
                            count = count + 1
                        Next i
                        str = String.Concat("(", str, ")")
                    Case Else
                        str = ""
                End Select
        End Select
Catch ex As Exception
Call insert_sys_log(RID.Value & " - " & "AddUpperQS", ex.Message.ToString())
End Try
        Return str
    End Function
    Protected Sub ASPxGridView1_HtmlDataCellPrepared(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewTableDataCellEventArgs) Handles ASPxGridView1.HtmlDataCellPrepared      
Try
Catch ex As Exception
Call insert_sys_log(RID.Value & " - " & "ASPxGridView1_HtmlDataCellPrepared", ex.Message.ToString())
End Try
    End Sub
    Protected Sub ASPxGridView1_HtmlRowPrepared(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewTableRowEventArgs) Handles ASPxGridView1.HtmlRowPrepared     
Try
If e.RowType <> GridViewRowType.Data Then
    Return
 End If
Catch ex As Exception
Call insert_sys_log(RID.Value & " - " & "ASPxGridView1_HtmlRowPrepared", ex.Message.ToString())
End Try
    End Sub
Private Sub ApplyLayout(ByVal layoutIndex As Integer)
        ASPxGridView1.BeginUpdate()
        Try
            ASPxGridView1.ClearSort()
            Select Case layoutIndex
                Case 0
                    ASPxGridView1.GroupBy(ASPxGridView1.Columns("TRANS_DATE"))
            End Select
Catch ex As Exception
Call insert_sys_log(RID.Value & " - " & "ApplyLayout", ex.Message.ToString())
        Finally
            ASPxGridView1.EndUpdate()
        End Try
        If chkAll.Checked Then
            ASPxGridView1.ExpandAll()
        Else
            ASPxGridView1.CollapseAll()
        End If
    End Sub
   Protected Sub ASPxGridViewExporter1_RenderBrick(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewExportRenderingEventArgs) Handles ASPxGridViewExporter1.RenderBrick
         Try
             Dim dataColumn As GridViewDataColumn = TryCast(e.Column, GridViewDataColumn)
If (E.RowType = GridViewRowType.Header) Then
e.Text = e.Text.Replace("<br>", " ")
End If
         Catch ex As Exception
Call insert_sys_log(RID.Value & " - " & "AspxGridView_CodeBehind_RenderBrick", ex.Message.ToString())
         End Try
Try
    Dim dataColumn As GridViewDataColumn = TryCast(e.Column, GridViewDataColumn)
    If (e.RowType = GridViewRowType.Header) Then
        e.Text = e.Text.Replace("<br>", " ").Replace("<br/>", " ").Replace("<br />", " ").Replace("</br>", " ").Replace("< /br>", " ").Replace("<BR>", " ")
    End If
    If (e.RowType = GridViewRowType.Data) Then
        e.Text = HtmlToPlainText(e.Text)
    End If
    If (e.RowType = GridViewRowType.Group) Then
        e.Text = HtmlToPlainText(e.Text)
    End If
Catch ex As Exception
    Call INSERT_SYS_LOG(rID.Value & " - " & "ASPxGridViewExporter1_Print_RenderBrick", ex.Message.ToString())
End Try
     End Sub
  Public Sub Export_To(ByVal strFType As String)
        Try
 Call Remove_Colors()
ASPxGridViewExporter1.Styles.Header.Font.Bold = True
            Dim link As New DevExpress.Web.Export.GridViewLink(ASPxGridViewExporter1)
            If True Then
                Dim leftColumn As String = "Pages: [Page # of Pages #]"
                Dim middleColumn As String = "User: " & Session("user_id").ToString() 
                Dim rightColumn As String = "Date: [Date Printed]"
                Dim phf As PageHeaderFooter = TryCast(link.PageHeaderFooter, PageHeaderFooter)
                phf.Footer.Content.Clear()
                phf.Footer.Content.AddRange(New String() {leftColumn, middleColumn, rightColumn})
                phf.Header.LineAlignment = BrickAlignment.Center
                phf.Footer.LineAlignment = BrickAlignment.Center
            End If
            AddHandler link.CreateReportHeaderArea, New CreateAreaEventHandler(AddressOf compositeLink_CreateMarginalHeaderArea)
            AddHandler link.CreateInnerPageHeaderArea, New CreateAreaEventHandler(AddressOf compositeLink_CreatePageHeaderArea)
            Dim ps As DevExpress.XtraPrinting.PrintingSystem = link.CreatePS()
            ps.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4
            ps.PageSettings.Landscape = True
            ps.PageSettings.TopMargin = 10
            ps.PageSettings.BottomMargin = 30
            ps.PageSettings.LeftMargin = 10
            ps.PageSettings.RightMargin = 10
 ps.Document.AutoFitToPagesWidth = 1
            Dim stream As New System.IO.MemoryStream()
      If strFType.ToUpper = "PDF" Then
                    ps.ExportToPdf(stream)
                    ps.PrintDlg()
                    WriteToResponse("REP_CC_PAYMENT_INFO", True, "pdf", stream)
                ElseIf strFType.ToUpper = "XLS" Then
                    ps.ExportToXls(stream)
                    WriteToResponse("REP_CC_PAYMENT_INFO", True, "xls", stream)
                End If
        Catch ex As Exception
Call insert_sys_log(RID.Value & " - " & "AspxGridView_CodeBehind_ExportTo", ex.Message.ToString())
        End Try
    End Sub
 Protected Sub compositeLink_CreateMarginalHeaderArea(ByVal sender As Object, ByVal e As CreateAreaEventArgs)
Try
Dim l As Link = TryCast(sender, Link)
         Dim tb As TextBrick = New TextBrick()
         tb.Text = "Credit Card Payment/Refund Info"
         tb.Font = New Font("Tahoma", 21, FontStyle.Bold)
tb.Rect = New RectangleF(0, 0, e.Graph.PrintingSystem.PageBounds.Width - 150, 35 * CInt(Math.Ceiling(tb.Text.Length / 64)))
         tb.BorderWidth = 0
         tb.BorderColor = Color.Transparent
         tb.HorzAlignment = DevExpress.Utils.HorzAlignment.Center
 e.Graph.DrawBrick(tb, New RectangleF(0, 0, e.Graph.PrintingSystem.PageBounds.Width - 150, 35 * CInt(Math.Ceiling(tb.Text.Length / 64))))
Catch ex As Exception
Call insert_sys_log(RID.Value & " - " & "compositeLink_CreateMarginalHeaderArea", ex.Message.ToString())
End Try
     End Sub
 Protected Sub compositeLink_CreatePageHeaderArea(ByVal sender As Object, ByVal e As CreateAreaEventArgs)
Try
Dim l As Link = TryCast(sender, Link)
         Dim tb As TextBrick = New TextBrick()
         tb.Text = ""
         tb.Font = New Font("Tahoma", 18, FontStyle.Underline Or FontStyle.Bold)
tb.Rect = New RectangleF(0, 0, e.Graph.PrintingSystem.PageBounds.Width - 150, 30 * (CInt(Math.Ceiling(tb.Text.Length / 70)) + 1))
         tb.BorderWidth = 0
         tb.BorderColor = Color.Transparent
         tb.HorzAlignment = DevExpress.Utils.HorzAlignment.Center
 e.Graph.DrawBrick(tb, New RectangleF(0, 0, e.Graph.PrintingSystem.PageBounds.Width - 150, 30 * (CInt(Math.Ceiling(tb.Text.Length / 70)) + 1)))
Catch ex As Exception
Call insert_sys_log(RID.Value & " - " & "compositeLink_CreatePageHeaderArea", ex.Message.ToString())
End Try
     End Sub
Protected Sub WriteToResponse(ByVal fileName As String, ByVal saveAsFile As Boolean, ByVal fileFormat As String, ByVal stream As System.IO.MemoryStream)
        If Page Is Nothing OrElse Page.Response Is Nothing Then
            Return
        End If
        Dim disposition As String = If(saveAsFile, "attachment", "inline")
        Page.Response.Clear()
        Page.Response.Buffer = False
        Page.Response.AppendHeader("Content-Type", String.Format("application/{0}", fileFormat))
        Page.Response.AppendHeader("Content-Transfer-Encoding", "binary")
        Page.Response.AppendHeader("Content-Disposition", String.Format("{0}; filename={1}.{2}", disposition, fileName, fileFormat))
        Page.Response.BinaryWrite(stream.ToArray)
        Page.Response.[End]()
    End Sub
Protected Sub ASPxGridView1_CustomSummaryCalculate(ByVal sender As Object, ByVal e As DevExpress.Data.CustomSummaryEventArgs) Handles ASPxGridView1.CustomSummaryCalculate
Try
Catch ex As Exception
Call insert_sys_log(RID.Value & " - " & "ASPxGridView1_CustomSummaryCalculate", ex.Message.ToString())
End Try
  End Sub
Protected Sub ASPxGridView1_SummaryDisplayText(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewSummaryDisplayTextEventArgs) Handles ASPxGridView1.SummaryDisplayText
Try
Catch ex As Exception
Call insert_sys_log(RID.Value & " - " & "ASPxGridView1_SummaryDisplayText", ex.Message.ToString())
End Try
    End Sub
 Private Sub linkMainReport_CreateDetailArea(ByVal sender As Object, ByVal e As CreateAreaEventArgs)
Try
        Dim tb As TextBrick = New TextBrick()
        tb.Rect = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 40)
        tb.BackColor = Color.Transparent
        tb.BorderColor = Color.Transparent
        e.Graph.DrawBrick(tb)
Catch ex As Exception
Call insert_sys_log(RID.Value & " - " & "linkMainReport_CreateDetailArea", ex.Message.ToString())
End Try
    End Sub
  Public Sub Remove_Colors()
        Try
            ASPxGridViewExporter1.Styles.Cell.BorderSides = BorderSide.All
            ASPxGridViewExporter1.Styles.Cell.BackColor = Color.Transparent
            ASPxGridViewExporter1.Styles.Header.BorderSides = BorderSide.All
            ASPxGridViewExporter1.Styles.Header.BackColor = Color.Transparent
            ASPxGridViewExporter1.Styles.Header.ForeColor = Color.Black
            ASPxGridViewExporter1.Styles.GroupRow.BorderSides = BorderSide.All
            ASPxGridViewExporter1.Styles.GroupRow.BackColor = Color.Transparent
            ASPxGridViewExporter1.Styles.GroupFooter.BorderSides = BorderSide.All
            ASPxGridViewExporter1.Styles.GroupFooter.BackColor = Color.Transparent
            ASPxGridViewExporter1.Styles.Footer.BorderSides = BorderSide.All
            ASPxGridViewExporter1.Styles.Footer.BackColor = Color.Transparent
            ASPxGridViewExporter1.Styles.AlternatingRowCell.BorderSides = BorderSide.All
            ASPxGridViewExporter1.Styles.AlternatingRowCell.BackColor = Color.Transparent
        Catch ex As Exception
Call insert_sys_log(RID.Value & " - " & "Remove_Colors", ex.Message.ToString())
        End Try
    End Sub
 Protected Sub ASPxGridView1_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles ASPxGridView1.Init
        Try
If Session("user_id") <> "" Then
 strSessID = Session("user_id").ToString  & "_" & RID.Value 
            Dim conn As New Dbconn
            dbad.SelectCommand = New OleDbCommand
            If Not IsPostBack Then
Session(strSessID & "_repQuery") = "SELECT DOC_TYPE, DOC_NO, AMOUNT, RESPONSE, to_CHAR(TRANS_DATE,'MM/DD/YYYY') TRANS_DATE, API_TOKEN, CUSTOMER_NO, NAME, STATUS, USER_ID, CREATED_DATE,CUSTTOKEN,PMTOKEN,TRANRESULTCODE,TRANREFNUM,MASKEDCC,CCTYPE,PAYBYTYPE FROM V_CC_PAYMENT_INFO WHERE TRUNC(TRANS_DATE) BETWEEN TO_DATE('[FROMDATE]','MM/DD/YYYY') AND TO_DATE('[TODATE]','MM/DD/YYYY')  order by Tranrefnum"
Session(strSessID & "_repQueryCB") = "SELECT DOC_TYPE, DOC_NO, AMOUNT, RESPONSE, to_CHAR(TRANS_DATE,'MM/DD/YYYY') TRANS_DATE, API_TOKEN, CUSTOMER_NO, NAME, STATUS, USER_ID, CREATED_DATE,CUSTTOKEN,PMTOKEN,TRANRESULTCODE,TRANREFNUM,MASKEDCC,CCTYPE,PAYBYTYPE FROM V_CC_PAYMENT_INFO WHERE TRUNC(TRANS_DATE) BETWEEN TO_DATE('[FROMDATE]','MM/DD/YYYY') AND TO_DATE('[TODATE]','MM/DD/YYYY')  order by Tranrefnum"
If (Equals(Session("db_connection"), System.DBNull.Value)) Then
Session("db_connection") = conn.getconnection()
  End If
'dbad.SelectCommand.Connection = Session("db_connection")
                Dim names() As String = System.Enum.GetNames(GetType(GridViewGroupFooterMode))
                For Each name As String In names
                    ddlGroupFooter.Items.Add(name)
                Next name
                ddlGroupFooter.Text = ASPxGridView1.Settings.ShowGroupFooter.ToString()
If ddlGroupBy.Items.Count = 1 Then
ddlGroupBy.SelectedIndex = ddlGroupBy.Items.Count - 1
Else
ddlGroupBy.SelectedIndex = ddlGroupBy.Items.Count - 2
End If
dateFrom.Text = now.date.adddays(-2)
dateTo.Text = now.date()
Call FillSortListView()
 Call FillGridSort()
                 Call FillGridQuickFilter()
                Call BindGrid()
Call Apply_Sort("Y")
Call Fill_Alternative()
            Else
                Dim ds2 As New DataSet
                Dim ds3 As New DataTable
                If Not Session(strSessID & "REP_CC_PAYMENT_INFO") Is Nothing Then
                    ds2 = Session(strSessID & "REP_CC_PAYMENT_INFO")
                    If Not ds2.Tables(0) Is Nothing Then
                        If ds2.Tables(0).Rows.Count > 0 Then
                            dataSource = ds2.Tables(0).DefaultView
                            ASPxGridView1.DataSource = dataSource
                            ASPxGridView1.DataBind()
                        End If
                    End If
                End If
            End If
            Session(strSessID & "_gridState") = ASPxGridView1.SaveClientLayout()
Else
Response.Redirect("login.aspx")
End If
        Catch ex As Exception
Call insert_sys_log(RID.Value & " - " & "ASPxGridView1_Init", ex.Message.ToString())
        End Try
    End Sub
 Public Sub INSERT_SYS_LOG(ByVal str1 As String, ByVal message As String)
         Dim sterr1, sterr2, sterr3, sterr4, sterr As String
         sterr = Replace(message, "'", "''")
         If (Len(sterr) > 4000) Then
             sterr1 = Mid(sterr, 1, 4000)
             If (Len(sterr) > 8000) Then
                 sterr2 = Mid(sterr, 4000, 8000)
                 If (Len(sterr) > 12000) Then
                     sterr3 = Mid(sterr, 8000, 12000)
                     If (Len(sterr) > 16000) Then
                         sterr4 = Mid(sterr, 12000, 16000)
                     Else
                         sterr4 = Mid(sterr, 12000, Len(sterr))
                     End If
                 Else
                     sterr3 = Mid(sterr, 8000, Len(sterr))
                     sterr4 = ""
                 End If
             Else
                 sterr2 = Mid(sterr, 4000, Len(sterr))
                 sterr3 = ""
                 sterr3 = ""
                 sterr4 = ""
             End If
         Else
             sterr1 = sterr
             sterr2 = ""
             sterr3 = ""
             sterr4 = ""
         End If
         dbad.InsertCommand = New OleDbCommand
         dbad.InsertCommand.Connection = conn.getconnection()
         If dbad.InsertCommand.Connection.State = ConnectionState.Closed Then
             dbad.InsertCommand.Connection.Open()
         End If
         dbad.InsertCommand.CommandText = "Insert into SYS_ACTIVATE_STATUS_LOG (LINE_NO, CHANGE_REQUEST_NO,  OBJECT_TYPE, OBJECT_NAME, ERROR_TEXT, STATUS,LOG_DATE,ERROR_TEXT1, ERROR_TEXT2, ERROR_TEXT3) values ((select nvl(max(to_number(line_no)),0)+1 from SYS_ACTIVATE_STATUS_LOG),'','" & RID.Value & "','" & str1 & "','" & sterr1 & "','N',sysdate,'" & sterr2 & "','" & sterr3 & "','" & sterr4 & "')"
         dbad.InsertCommand.ExecuteNonQuery()
         If dbad.InsertCommand.Connection.State = ConnectionState.Open Then
             dbad.InsertCommand.Connection.Close()
         End If
     End Sub
 Protected Overridable Function GetLabelText(ByVal container As GridViewGroupRowTemplateContainer) As String
 Dim strLabel As String = String.Empty
Try
  If container.GroupText.Contains("&lt;a href=") Then
  strLabel = container.Column.Caption & ": " & container.KeyValue
   Else
  strLabel = container.Column.Caption & ": " & container.KeyValue
  End If
        Catch ex As Exception
Call insert_sys_log(RID.Value & " - " & "ASPxGridView1_Init", ex.Message.ToString())
        End Try
        Return strLabel
    End Function
 Protected Overridable Function GetLabelVisible(ByVal container As GridViewGroupRowTemplateContainer) As String
 Return True
    End Function
 Protected Overridable Function GetHyperLinkVisible(ByVal container As GridViewGroupRowTemplateContainer) As String
Dim hylkvVisible As Boolean = False
 hylkvVisible = False
  Return hylkvVisible
    End Function
Protected Overridable Function GetHprLinkLabelText(ByVal container As GridViewGroupRowTemplateContainer) As String
 Return container.GroupText
End Function
 Public Function GetColValue(ByVal strValue As Object) As Object
        Dim fieldValue As String = String.Empty
        If Not (Equals(strValue, System.DBNull.Value)) Then
            If Not strValue Is Nothing Then
            Dim txtVale As String = DirectCast(strValue, Object)
            If txtVale.Contains("<a ") Then
                    Dim htmltextValue As String = strValue
                    Dim LINK_TAG_PATTERN As String = "<[a|A][^>]*>|</[a|A]>"
                    fieldValue = System.Text.RegularExpressions.Regex.Replace(htmltextValue, LINK_TAG_PATTERN, "", RegexOptions.IgnoreCase)
                Else
                    fieldValue = strValue
                End If
            Else
                fieldValue = strValue
            End If
            Return fieldValue.ToString().Replace("&", "%26").Replace("#", "%23").Replace(" ", "%20")
        Else
            Return DBNull.Value
        End If
    End Function
Protected Sub btnShowPopup_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Try
            Dim oprp As New ImageButton
            Dim dgItem1 As DataGridItem
            Dim cell4 As TableCell
            Dim i1 As Integer

            Dim ddlValue As String = String.Empty
            Dim strInValues As String = String.Empty

            Dim dr As DataGridColumnCollection = dgQuickSearch.Columns()

            oprp = CType(sender, ImageButton)
            cell4 = CType(oprp.Parent, TableCell)
            dgItem1 = CType(cell4.Parent, DataGridItem)
            i1 = dgItem1.ItemIndex

            Dim mdlPopup1 As ModalPopupExtender = CType(dgItem1.FindControl("mdlPopup"), ModalPopupExtender)

            Dim imgBtn1 As ImageButton = CType(dgItem1.FindControl("imgBtn1"), ImageButton)
          
            Dim TextBox1 As TextBox = CType(dgItem1.FindControl("expression"), TextBox)
            Dim lblFname As Label = CType(dgItem1.FindControl("lblFName"), Label)

            Dim torD As String = dgItem1.Cells(4).Text.ToString()

            If torD = "DATE" Then
                Dim ddlDate As DropDownList = CType(dgItem1.FindControl("ddlDate"), DropDownList)
                ddlValue = ddlDate.SelectedItem.Value.ToString()
            Else
                Dim ddlDGDrop As DropDownList = CType(dgItem1.FindControl("ddlDrop"), DropDownList)
                ddlValue = ddlDGDrop.SelectedItem.Value.ToString()
            End If
            strInValues = String.Empty
            If ddlValue = "in" Or ddlValue = "not in" Then
                If TextBox1.Text <> String.Empty Then
                    strInValues = TextBox1.Text & ","
                Else
                    strInValues = String.Empty
                End If
            Else
                strInValues = String.Empty
            End If
            dstTempData.Clear()
            wClause = ""
            wClause = CascadingFilter(lblFname.Text.Trim)
            dstTempData = GetUniqueData(lblFname.Text, wClause)
            rbl.Items.Clear()
            TextBox9.Text = mdlPopup1.ClientID.ToString()

            For j As Integer = 0 To dstTempData.Tables(0).Rows.Count - 1
                If Not IsDBNull(dstTempData.Tables(0).Rows(j)(lblFname.Text).ToString) AndAlso Not String.IsNullOrEmpty(dstTempData.Tables(0).Rows(j)(lblFname.Text).ToString) Then
                    rbl.Items.Add(New ListItem(dstTempData.Tables(0).Rows(j)(lblFname.Text).ToString, dstTempData.Tables(0).Rows(j)(lblFname.Text), True))
                End If
            Next
            hFTextBox1.Value = String.Empty
            hFstrInValues.Value = String.Empty
            hFpnlPopUp1.Value = String.Empty
            hFmdlPopup1.Value = String.Empty
            hFTextBox1.Value = TextBox1.ClientID
            hFstrInValues.Value = strInValues
            hFpnlPopUp1.Value = pnlPopUp1.ClientID
            hFmdlPopup1.Value = mdlPopup1.ClientID
    If chkAll.Checked Then
ASPxGridView1.ExpandAll()
Else
ASPxGridView1.CollapseAll()
End If
            mdlPopup1.Show()
        Catch ex As Exception
            Call INSERT_SYS_LOG(RID.Value & " - " & "btnShowPopup_Click:-", ex.Message)
        End Try
    End Sub
    Public Function CascadingFilter(ByVal colName As String) As String

        Dim strWClause As String = String.Empty

        Dim strOperator As String = String.Empty
        Dim strExp As String = String.Empty
        Dim strDtype As String = String.Empty
        Dim opt As String = String.Empty
        Dim isApply As String = String.Empty

        Try
            For Each dgItem As DataGridItem In dgQuickSearch.Items
                Dim intRow As Integer = dgItem.ItemIndex
                If intRow <> -1 Then
                    'Dim qS As String = dgItem.Cells(4).Text
                    'If qS = 1 Then

                    Dim lblFname As Label = DirectCast(dgItem.Cells(2).FindControl("lblFName"), Label)
                    Dim ddlDGDrop As DropDownList = DirectCast(dgItem.Cells(2).FindControl("ddlDrop"), DropDownList)
                    Dim ddlDate As DropDownList = DirectCast(dgItem.Cells(2).FindControl("ddlDate"), DropDownList)
                    Dim exp As TextBox = DirectCast(dgItem.Cells(3).FindControl("expression"), TextBox)
                    Dim exp2 As TextBox = DirectCast(dgItem.Cells(3).FindControl("expression2"), TextBox)
                    'Dim frmDate As TextBox = DirectCast(dgItem.Cells(3).FindControl("frmDate"), TextBox)
                    'Dim toDate As TextBox = DirectCast(dgItem.Cells(3).FindControl("toDate"), TextBox)
  Dim frmDate As ASPxDateEdit = DirectCast(dgItem.Cells(3).FindControl("frmDate"), ASPxDateEdit)   
  Dim toDate As ASPxDateEdit = DirectCast(dgItem.Cells(3).FindControl("toDate"), ASPxDateEdit)  
                    Dim type As String = dgItem.Cells(4).Text

                    'Dim lblFname As Label = DirectCast(dgItem.Cells(1).FindControl("lblFName"), Label)
                    'Dim ddlDGDrop As DropDownList = DirectCast(dgItem.Cells(2).FindControl("ddlDrop"), DropDownList)
                    'Dim ddlDate As DropDownList = DirectCast(dgItem.Cells(2).FindControl("ddlDate"), DropDownList)
                    'Dim exp As TextBox = DirectCast(dgItem.Cells(3).FindControl("expression"), TextBox)
                    'Dim exp2 As TextBox = DirectCast(dgItem.Cells(3).FindControl("expression2"), TextBox)

                    'Dim type As String = dgItem.Cells(5).Text

                    If lblFname.Text.Trim <> colName Then
                        If type = "DATE" Then
                            If ddlDate.SelectedValue = "between" Then
                                exp.Text = frmDate.Text + " and " + toDate.Text
                            Else
                                exp.Text = frmDate.Text
                            End If
                            strOperator = ddlDate.SelectedValue
                        Else
                            strOperator = ddlDGDrop.SelectedValue
                        End If
                        ''fltExpression = AddUpper(fltOperator, exp.Text.ToString, type)
                        'dstTempData.Tables(0).Rows(intRow)("Filter_Expression") = AddUpperQS(dstTempData.Tables(0).Rows(intRow)("Filter_operator"), exp.Text.ToString, type)
                        strExp = AddUpperQS(strOperator, exp.Text.ToString, type)

                        Select Case strOperator
                            Case "contains"
                                opt = "Like"
                            Case "does not contain"
                                opt = "Not Like"
                            Case "starts with"
                                opt = "Like"
                            Case "ends with"
                                opt = "Like"
                            Case Else
                                opt = strOperator
                        End Select

                        isApply = IsApplySearch(type, strOperator, exp.Text, exp2.Text, frmDate.Text, toDate.Text)
                        'Dim fltAppl As String = 
                        If isApply = 1 Then
                            If count = 0 Then
                                strWClause = String.Concat(" UPPER(", lblFname.Text, ") ", opt, " ", strExp)
                            Else
                                strWClause = String.Concat(strWClause, " and UPPER(", lblFname.Text, ") ", opt, " ", strExp)
                            End If
                            count = count + 1
                        End If
                    End If
                    'End If
                End If
            Next

        Catch ex As Exception
            Call insert_sys_log(RID.Value & " - " & "CascadingFilter:-", ex.Message)
        End Try
        Return strWClause
    End Function
    Public Function GetUniqueData(ByVal columnName As String, ByVal whereClause As String) As DataSet
        Dim dsV As New DataSet
        Try
            
            wClause = String.Empty
            strQuery = String.Empty
            If Not String.IsNullOrEmpty(whereClause.Trim) Then
                wClause = " WHERE " & whereClause & " "
            Else
                wClause = ""
            End If
            'strQuery = "SELECT " & columnName & " FROM ( SELECT DISTINCT(" & columnName & ") FROM " & vName & " WHERE 1=1 AND " & whereClause & " ORDER BY '" & columnName & "') WHERE ROWNUM<=500"
            vName = hdnViewName.Value
            strQuery = "SELECT " & columnName & " FROM ( SELECT DISTINCT(" & columnName & ") FROM " & vName & wClause & " ORDER BY '" & columnName & "')  "
            dbad.SelectCommand.Connection = Session("db_connection")
            If (dbad.SelectCommand.Connection.State = ConnectionState.Closed) Then
                dbad.SelectCommand.Connection.Open()
            End If
            dbad.SelectCommand.CommandText = strQuery
            dsV.Clear()
            dbad.Fill(dsV)
            If (dbad.SelectCommand.Connection.State = ConnectionState.Open) Then
                dbad.SelectCommand.Connection.Close()
            End If
        Catch ex As Exception
            dbad.SelectCommand.Connection.Close()
            Call INSERT_SYS_LOG(RID.Value & " - " & "GetUniqueData:-", ex.Message)
        Finally
        End Try
        Return dsV
    End Function
Protected Sub searchCriteria_HtmlDataCellPrepared(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewTableDataCellEventArgs) Handles searchCriteria.HtmlDataCellPrepared
Try
    If e.DataColumn.FieldName = "Expression" Then
	Dim reading As String = searchCriteria.GetRowValues(Convert.ToString(e.VisibleIndex), "FltColor").ToString()
	If reading <> "White" Then
	    e.Cell.BackColor = Color.FromName(reading.ToUpper.Replace("COLOR.", ""))
	End If
    End If
Catch ex As Exception
    Call INSERT_SYS_LOG(RID.Value & " - " & "searchCriteria_HtmlDataCellPrepared:-", ex.Message)
End Try
End Sub
Public Function Get_Formating_Rules(ByVal dt As DataTable) As DataTable
        Try
            Dim dss11 As New DataSet
            Dim strOprt As String = String.Empty
            Dim dr As DataRow
dbad.SelectCommand.Connection = Session("db_connection")
            If dbad.SelectCommand.Connection.State = ConnectionState.Closed Then
                dbad.SelectCommand.Connection.Open()
            End If
             dbad.SelectCommand.CommandText = " SELECT NVL(B.FIELD_TITLE,B.FIELD_NAME) AS FIELD_LABEL,A.FIELD_NAME,A.CONDITION,A.CONDITION_VALUE,A.CELL_COLOR FROM SYS_REPORT_FOMRAT_RULES A ,SYS_REPORT_DETAIL B WHERE  A.REPORT_ID=B.REPORT_ID AND A.FIELD_NAME = B.FIELD_NAME AND  A.CONDITION IS NOT NULL AND  A.CONDITION_VALUE IS NOT NULL AND A.CELL_COLOR IS NOT NULL AND UPPER(A.REPORT_ID)= UPPER('" & RID.Value & "') ORDER BY A.LINE_NO "
            dss11.Clear()
            dbad.Fill(dss11)
            If dbad.SelectCommand.Connection.State = ConnectionState.Open Then
                dbad.SelectCommand.Connection.Close()
            End If
            If Not dss11.Tables(0) Is Nothing Then
                If (dss11.Tables(0).Rows.Count > 0) Then
                    For i = 0 To dss11.Tables(0).Rows.Count - 1
                        If Not String.IsNullOrEmpty(dss11.Tables(0).Rows(i)("CONDITION").ToString) Then
                            If Not String.IsNullOrEmpty(dss11.Tables(0).Rows(i)("CONDITION_VALUE").ToString) Then
                                strOprt = String.Empty
                                dr = dt.NewRow()
                                If dss11.Tables(0).Rows(i)("CONDITION").ToString.ToUpper = "EQUAL" Then
                                    strOprt = "="
                                ElseIf dss11.Tables(0).Rows(i)("CONDITION").ToString.ToUpper = "NOTEQUAL" Then
                                    strOprt = "<>"
                                ElseIf dss11.Tables(0).Rows(i)("CONDITION").ToString.ToUpper = "LESSTHAN" Then
                                    strOprt = "<"
                                ElseIf dss11.Tables(0).Rows(i)("CONDITION").ToString.ToUpper = "LESSTHANOREQUAL" Then
                                    strOprt = "<="
                                ElseIf dss11.Tables(0).Rows(i)("CONDITION").ToString.ToUpper = "GREATERTHAN" Then
                                    strOprt = ">"
                                ElseIf dss11.Tables(0).Rows(i)("CONDITION").ToString.ToUpper = "GREATERTHANOREQUAL" Then
                                    strOprt = ">="
                                Else
                                    strOprt = dss11.Tables(0).Rows(i)("CONDITION").ToString.ToUpper
                                End If
                                dr("FieldName") = dss11.Tables(0).Rows(i)("FIELD_LABEL").ToString
                                dr("Operator") = strOprt
                                dr("Expression") = dss11.Tables(0).Rows(i)("CONDITION_VALUE").ToString
                                dr("FltColor") = dss11.Tables(0).Rows(i)("CELL_COLOR").ToString.ToUpper
                                dr("FltType") = "COLOR"
                                dr("FltAply") = "COLOR FORMAT"
                                dt.Rows.Add(dr)
                            End If
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            Call INSERT_SYS_LOG(RID.Value & " - " & "Get_Formating_Rules:-", ex.Message)
        End Try
        Return dt
    End Function
Protected Function GetImageName(ByVal dataValue As Object, ByVal dataType As Object) As String
        Dim val As String = String.Empty
        Dim dtype As String = String.Empty
        Try
            val = CStr(dataValue)
            dtype = CStr(dataType)
        Catch
        End Try
        Select Case val
            Case "FILTER"
                If dtype = "Yes" Then
                    Return "~/Images/Green1.jpg"
                Else
                    Return "~/Images/Red1.jpg"
                End If
            Case "COLOR"
                Return "~/Images/edit_col_322.gif"
 Case "SORT"
  Return "~/Images/sort_32.gif"
            Case Else
                Return ""
        End Select
    End Function
 Protected Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            Call BindGrid()
Call Apply_Sort("Y")
            Session(strSessID & "_gridState") = ASPxGridView1.SaveClientLayout()
Call Fill_Alternative()
        Catch ex As Exception
Call insert_sys_log(RID.Value & " - " & "btnRefresh_Click", ex.Message.ToString())
        End Try
    End Sub
 Private Sub FillSortListView()
        Dim dstCopy As New DataSet()
        Try
dbad.SelectCommand.Connection = Session("db_connection")
            If dbad.SelectCommand.Connection.State = ConnectionState.Closed Then
                dbad.SelectCommand.Connection.Open()
            End If
            dbad.SelectCommand.CommandText = "SELECT FIELD_NAME FROM SYS_REPORT_DETAIL WHERE UPPER(REPORT_ID )= UPPER('" & RID.Value & "') AND COLUMN_VISIBLE='Y' AND NVL(CONTROL_BREAK,'N') <>'Y' AND SORTORDER IS NULL ORDER BY AREA_INDEX"
            dstCopy.Clear()
            dbad.Fill(dstCopy)
            If dbad.SelectCommand.Connection.State = ConnectionState.Open Then
                dbad.SelectCommand.Connection.Close()
            End If
            Session(strSessID & "_lvColData") = dstCopy

            lstSort.DataSource = dstCopy
            lstSort.DataSource = dstCopy.Tables(0)
            lstSort.DataTextField = dstCopy.Tables(0).Columns("FIELD_NAME").ColumnName.ToString
            lstSort.DataValueField = dstCopy.Tables(0).Columns("FIELD_NAME").ColumnName.ToString
            lstSort.DataBind()
            SortListBox(lstSort, ListBoxSortOrder.Ascending)
        Catch ex As Exception
            Call INSERT_SYS_LOG(RID.Value & " - " & "FillSortListView:-", ex.Message)
        Finally
        End Try
    End Sub
  Private Function CompareListItemsAscending(ByVal li1 As ListItem, ByVal li2 As ListItem) As Integer
        Return [String].Compare(li1.Text, li2.Text)
    End Function
    Private Function CompareListItemsDecending(ByVal li1 As ListItem, ByVal li2 As ListItem) As Integer
        Return [String].Compare(li1.Text, li2.Text) * -1
    End Function
Public Sub SortListBox(ByVal lbxId As ListBox, ByVal sortOrder As ListBoxSortOrder)
        If lbxId.Items.Count > 1 Then
            Dim t As New List(Of ListItem)()
            Dim compare As Comparison(Of ListItem)

            If sortOrder = ListBoxSortOrder.Ascending Then
                compare = New Comparison(Of ListItem)(AddressOf CompareListItemsAscending)
            Else
                compare = New Comparison(Of ListItem)(AddressOf CompareListItemsDecending)
            End If

            For Each lbItem As ListItem In lbxId.Items
                t.Add(lbItem)
            Next           
            t.Sort(compare)
          
            lbxId.Items.Clear()
            lbxId.Items.AddRange(t.ToArray())
        End If
    End Sub
 Private Sub FillGridSort()
        Try
            Dim dsSort As New DataSet()
            strQuery = String.Empty

dbad.SelectCommand.Connection = Session("db_connection")
            If dbad.SelectCommand.Connection.State = ConnectionState.Closed Then
                dbad.SelectCommand.Connection.Open()
            End If
            dbad.SelectCommand.Connection = Session("db_connection")
            strQuery = "SELECT FIELD_NAME,SORTORDER FROM SYS_REPORT_DETAIL WHERE REPORT_ID = '" & RID.Value & "' AND COLUMN_VISIBLE='Y' AND NVL(CONTROL_BREAK,'N') <>'Y' AND SORTORDER IS NOT NULL ORDER BY SORT_ORDER_SEQ"
            'strQuery = "SELECT FIELD_NAME,SORTORDER FROM SYS_REPORT_DETAIL WHERE REPORT_ID = '" & RID.Value & "' AND COLUMN_VISIBLE='Y'"
            dbad.SelectCommand.CommandText = strQuery
            dsSort.Clear()
            dbad.Fill(dsSort)
            If dbad.SelectCommand.Connection.State = ConnectionState.Open Then
                dbad.SelectCommand.Connection.Close()
            End If
            Session("sortGridData") = dsSort
            'dsExternal = report.getQuickSearchFieldsInfo(searchID.Value)
            If Not dsSort.Tables(0) Is Nothing Then
                If dsSort.Tables(0).Rows.Count > 0 Then
                    Dim dt As New DataTable
                    dt = dsSort.Tables(0)
                    dgSort.DataSource = dt.DefaultView
                    dgSort.DataBind()
                    pnlNoSort.Visible = False
                    pnlContentSort.Visible = True
                Else
                    pnlContentSort.Visible = False
                    pnlNoSort.Visible = True
                End If
            Else
                pnlContentSort.Visible = False
                pnlNoSort.Visible = True
            End If
         
        Catch ex As Exception
            Call insert_sys_log("Search Page FillGridSort:- ", ex.Message)
        Finally
        End Try
    End Sub
Protected Sub imgAddSort_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgAddSort.Click
        AddSort()
    End Sub
 Public Sub AddSort()
        Dim licCollection As ListItemCollection
        Try
            licCollection = New ListItemCollection()
            For intCount As Integer = 0 To lstSort.Items.Count - 1
                If lstSort.Items(intCount).Selected = True Then
                    licCollection.Add(lstSort.Items(intCount))
                End If
            Next

            For intCount As Integer = 0 To licCollection.Count - 1
                lstSort.Items.Remove(licCollection(intCount))           
                AddSortToGrid(licCollection(intCount).Text, licCollection(intCount).Value)
            Next
        Catch ex As Exception        
            Call insert_sys_log("Search Page AddColumns:-", ex.Message)
        Finally
            licCollection = Nothing
        End Try
    End Sub
Public Sub AddSortToGrid(ByVal FName As String, ByVal FLabel As String)
        Try
            Dim dstTempData1 As New DataSet
            If (Session("sortGridData") IsNot Nothing) Then
                dstTempData1.Clear()
                dstTempData1 = DirectCast(Session("sortGridData"), DataSet)
                Dim drwRow As DataRow = Nothing
                Dim intPreRow As Integer = 0
                intPreRow = dstTempData1.Tables(0).Rows.Count
                For Each dgItem As DataGridItem In dgSort.Items
                    Dim intRow As Integer = dgItem.ItemIndex
                    If intRow <> -1 Then
                        'Dim name As String = dgItem.Cells(1).Text
                        Dim lblFname As Label = DirectCast(dgItem.Cells(1).FindControl("lblFName"), Label)
                        Dim ddlSort As DropDownList = DirectCast(dgItem.Cells(2).FindControl("ddlSort"), DropDownList)
                        dstTempData1.Tables(0).Rows(intRow)("FIELD_NAME") = lblFname.Text
                        dstTempData1.Tables(0).Rows(intRow)("SORTORDER") = ddlSort.SelectedValue
                    End If
                Next
                Dim intFinalRow As Integer = 0
                drwRow = dstTempData1.Tables(0).NewRow()
                dstTempData1.Tables(0).Rows.Add(drwRow)
                intFinalRow = dstTempData1.Tables(0).Rows.Count - 1

                dstTempData1.Tables(0).Rows(intFinalRow)("FIELD_NAME") = FName.ToString
                dstTempData1.Tables(0).Rows(intFinalRow)("SORTORDER") = "Ascending"

                Session("sortGridData") = dstTempData1

                BindSortGrid()

            End If
        Catch ex As Exception
            Call INSERT_SYS_LOG("Search Page AddSortToGrid:- ", ex.Message)
        Finally

        End Try
    End Sub
 Private Sub BindSortGrid()
        Dim dstTempData1 As New DataSet
        If (Session("sortGridData") IsNot Nothing) Then
            dstTempData1 = DirectCast(Session("sortGridData"), DataSet)
            If (dstTempData1.Tables(0) IsNot Nothing) Then
                If dstTempData1.Tables(0).Rows.Count > 0 Then
                    pnlNoSort.Visible = False
                    pnlContentSort.Visible = True
                Else
                    pnlContentSort.Visible = False
                    pnlNoSort.Visible = True
                End If
                dgSort.DataSource = dstTempData1.Tables(0)
                dgSort.DataBind()
            End If
        Else
            pnlNoSort.Visible = True
            pnlContentSort.Visible = False
        End If
    End Sub
Protected Sub dgSort_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgSort.ItemCommand
        Try
            If (Session("sortGridData") IsNot Nothing) Then
                dstTempData = DirectCast(Session("sortGridData"), DataSet)
                Dim rowID As Int32 = DirectCast(e.Item.ItemIndex, Int32)
                Dim rowIndex As Int32 = 0
                Dim rowToBeMoved As DataRow = dstTempData.Tables(0).NewRow()
                'create a copy of the row to be moved
                For i As Int16 = 0 To dstTempData.Tables(0).Rows.Count - 1
                    Dim lblFname As Label = DirectCast(dgSort.Items(rowID).Cells(1).FindControl("lblFName"), Label)
                    Dim ddlSort As DropDownList = DirectCast(e.Item.Cells(2).FindControl("ddlSort"), DropDownList)
                    If dstTempData.Tables(0).Rows(i)("FIELD_NAME").Equals(lblFname.Text) Then
                        rowIndex = i
                        rowToBeMoved("FIELD_NAME") = lblFname.Text
                        rowToBeMoved("SORTORDER") = ddlSort.SelectedItem.Text
                        Exit For
                    End If
                Next

                Select Case e.CommandName
                    Case "MoveUp"
                        If rowIndex > 0 Then
                            'delete the selected row
                            dstTempData.Tables(0).Rows(rowIndex).Delete()
                            dstTempData.Tables(0).AcceptChanges()
                            'add the rowToBeMoved
                            dstTempData.Tables(0).Rows.InsertAt(rowToBeMoved, rowIndex - 1)
                            dstTempData.Tables(0).AcceptChanges()
                            ''dgColumns.SelectedIndex = rowIndex - 1
                        End If
                        Exit Select
                    Case "MoveDown"
                        If rowIndex < dstTempData.Tables(0).Rows.Count - 1 Then
                            dstTempData.Tables(0).Rows(rowIndex).Delete()
                            dstTempData.Tables(0).AcceptChanges()
                            dstTempData.Tables(0).Rows.InsertAt(rowToBeMoved, rowIndex + 1)
                            dstTempData.Tables(0).AcceptChanges()
                            ''dgColumns.SelectedIndex = rowIndex + 1
                        End If
                        Exit Select
                End Select
                Session("sortGridData") = dstTempData
                BindSortGrid()
            End If
        Catch ex As Exception
            Call INSERT_SYS_LOG("Search Page dgColumns_ItemCommand:-", ex.Message)
        End Try
    End Sub
Protected Sub dgSort_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgSort.ItemDataBound
        Try
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                Dim intRow As Integer = e.Item.ItemIndex
                If intRow = -1 Then
                    Exit Sub
                End If
                If (Session("sortGridData") IsNot Nothing) Then  ' New
                    dstTempData = DirectCast(Session("sortGridData"), DataSet)

                    If dstTempData.Tables(0).Rows.Count > 0 Then
                        ''Dim CurrentDrop As String = StrConv(dstTempData.Tables(0).Rows(intRow)("SORT_DIRECTION").ToString, VbStrConv.ProperCase)
                        Dim CurrentDrop As String = dstTempData.Tables(0).Rows(intRow)("SORTORDER").ToString

                        Dim itemSub As ListItem = Nothing

                        Dim lblFname As Label = DirectCast(e.Item.Cells(1).FindControl("lblFName"), Label)
                        Dim ddlSort As DropDownList = DirectCast(e.Item.Cells(2).FindControl("ddlSort"), DropDownList)

                        itemSub = ddlSort.Items.FindByValue(CurrentDrop)

                        lblFname.Text = dstTempData.Tables(0).Rows(intRow)("FIELD_NAME").ToString

                        If (itemSub IsNot Nothing) Then
                            itemSub.Selected = True
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            Call INSERT_SYS_LOG("Search Page Extended ItemBound:-", ex.Message)
        Finally

        End Try
    End Sub
Public Sub DeleteSort_Item(ByVal s As Object, ByVal e As DataGridCommandEventArgs)
        Try
            If (Session("sortGridData") IsNot Nothing) Then
                dstTempData = DirectCast(Session("sortGridData"), DataSet)

                lstSort.Items.Add(New ListItem(dstTempData.Tables(0).Rows(e.Item.ItemIndex)("FIELD_NAME").ToString(), dstTempData.Tables(0).Rows(e.Item.ItemIndex)("FIELD_NAME").ToString()))

                For Each dgItem As DataGridItem In dgSort.Items
                    Dim intRow As Integer = dgItem.ItemIndex
                    If intRow <> -1 Then
                        Dim lblFname As Label = DirectCast(dgItem.Cells(1).FindControl("lblFName"), Label)

                        Dim ddlSort As DropDownList = DirectCast(dgItem.Cells(2).FindControl("ddlSort"), DropDownList)

                        dstTempData.Tables(0).Rows(intRow)("FIELD_NAME") = lblFname.Text
                        dstTempData.Tables(0).Rows(intRow)("SORTORDER") = ddlSort.SelectedValue.ToString
                    End If
                Next

                dstTempData.Tables(0).Rows(e.Item.ItemIndex).Delete()
                dstTempData.AcceptChanges()
                Session("sortGridData") = dstTempData

                BindSortGrid()
                SortListBox(lstSort, ListBoxSortOrder.Ascending) '' New 
            End If
        Catch ex As Exception
            Call insert_sys_log("Search Page DeleteSort_Item:-", ex.Message)
        End Try
    End Sub
 Public Function BuildSortOrders() As DataTable
        Dim tblSo As New DataTable()
        Dim dcFname As New DataColumn("FieldName", System.Type.[GetType]("System.String"))
        Dim dcSortOrder As New DataColumn("SortOrder", System.Type.[GetType]("System.String"))
        Dim dcSortIndex As New DataColumn("SortIndex", System.Type.[GetType]("System.Int32"))
        Dim dcFltType As New DataColumn("FltType", System.Type.[GetType]("System.String"))
        Dim dcFltAply As New DataColumn("FltAply", System.Type.[GetType]("System.String"))

        tblSo.Columns.Add(dcFname)
        tblSo.Columns.Add(dcSortOrder)
        tblSo.Columns.Add(dcSortIndex)
        tblSo.Columns.Add(dcFltType)
        tblSo.Columns.Add(dcFltAply)

        tblSo.Columns(0).ColumnName = "FieldName"
        tblSo.Columns(1).ColumnName = "SortOrder"
        tblSo.Columns(2).ColumnName = "SortIndex"
        tblSo.Columns(3).ColumnName = "FltType"
        tblSo.Columns(4).ColumnName = "FltAply"

        Dim dr As DataRow
        Dim strWClause As String = String.Empty
        Dim count As Integer = 0
        Dim strOperator As String = String.Empty
        Dim strExp As String = String.Empty
        Dim strDtype As String = String.Empty
        Dim opt As String = String.Empty
        Dim isApply As String = String.Empty
        Try
            Dim cOrder As Integer = 1
            For Each dgItem As DataGridItem In dgSort.Items
                Dim intRow As Integer = dgItem.ItemIndex
                If intRow <> -1 Then
                    dr = tblSo.NewRow()
                    Dim strValues As String = String.Empty
                    Dim lblFname As Label = DirectCast(dgItem.Cells(0).FindControl("lblFName"), Label)
                    Dim ddlDGDrop As DropDownList = DirectCast(dgItem.Cells(1).FindControl("ddlSort"), DropDownList)

                    dr("FieldName") = lblFname.Text
                    dr("SortOrder") = ddlDGDrop.SelectedValue
                    dr("SortIndex") = cOrder
                    dr("FltType") = "SORT"
                    dr("FltAply") = "SORTING"

                    tblSo.Rows.Add(dr)
                    cOrder += 1
                End If
            Next
            cOrder = 0
            Session(strSessID & "_SOData") = tblSo
            'If Not tblSo Is Nothing Then
            '    If tblSo.Rows.Count > 0 Then
            sortCriteria.DataSource = ""
            sortCriteria.DataSource = tblSo
            sortCriteria.DataBind()
            'End If
            'End If
        Catch ex As Exception
            Call INSERT_SYS_LOG(RID.Value & " - " & "BuildSortOrders", ex.Message.ToString())
        End Try
        Return tblSo
    End Function
  Private Function ColumnExists(ByVal dt As DataTable, ByVal col As String) As Boolean
        Dim colExists As Boolean = False
        For j As Integer = 0 To dt.Rows.Count - 1
            If dt.Rows(j)(0).ToString() = col Then
                colExists = True
                Exit For
            End If
        Next
        Return colExists

    End Function
   Private Function GetColumnSortOrder(ByVal dt As DataTable, ByVal col As String) As String
         Dim colSorder As String = String.Empty
         For j As Integer = 0 To dt.Rows.Count - 1
             If dt.Rows(j)(0).ToString() = col Then
                 colSorder = dt.Rows(j)(1).ToString() & "$" & dt.Rows(j)(2).ToString()
                 Exit For
             End If
         Next
         Return colSorder
    End Function
 Public Enum ListBoxSortOrder
        Ascending
        Decending
    End Enum
 Public Sub Apply_Sort(ByVal chkLoad As String)
        Try
            Dim tblSo As New DataTable()
            If chkLoad = "Y" Then
                tblSo = BuildSortOrders()
            Else
                If (Session(strSessID & "_SOData") IsNot Nothing) Then
                    tblSo = DirectCast(Session(strSessID & "_SOData"), DataTable)
                Else
                    tblSo = BuildSortOrders()
                End If
            End If
            If Not tblSo Is Nothing Then
                If tblSo.Rows.Count > 0 Then
Dim dv As New DataView()
dv = tblSo.DefaultView
dv.Sort = "SortIndex"
tblSo = dv.ToTable()

For i As Integer = 0 To tblSo.Rows.Count - 1
If tblSo.Rows(i)("SortOrder").ToString().StartsWith("A", StringComparison.CurrentCultureIgnoreCase) Then
    ASPxGridView1.SortBy(ASPxGridView1.Columns(tblSo.Rows(i)("FieldName").ToString()), ColumnSortOrder.Ascending)
Else
    ASPxGridView1.SortBy(ASPxGridView1.Columns(tblSo.Rows(i)("FieldName").ToString()), ColumnSortOrder.Descending)
End If
Next
                End If
            End If
        Catch ex As Exception
            Call INSERT_SYS_LOG(RID.Value & " - " & "Apply_Sort", ex.Message.ToString())
        End Try
    End Sub
   Protected Sub btnGo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGo.Click
        Try
            If txtFltValue.Text.ToString.Trim <> "" Then
                Dim wCluase As String = String.Empty
                Dim wrCluase As String = String.Empty
                If lblColumnname.Text.ToString.Trim <> "" Then
                    Call Update_QuickSearch1(lblColumnname.Text, txtFltValue.Text)
                    Call BindGrid()
                Else
                    'Call Update_QuickSearch1("ROW", txtFltValue.Text)
                    For Each dgItem As DataGridItem In dgQuickSearch.Items
                        Dim intRow As Integer = dgItem.ItemIndex
                        If intRow <> -1 Then
                            Dim strValues As String = String.Empty
                            Dim lblFname1 As Label = DirectCast(dgItem.Cells(2).FindControl("lblFName"), Label)
                            Dim exp As TextBox = DirectCast(dgItem.Cells(3).FindControl("expression"), TextBox)
                            Dim exp2 As TextBox = DirectCast(dgItem.Cells(3).FindControl("expression2"), TextBox)
  Dim frmDate As ASPxDateEdit = DirectCast(dgItem.Cells(3).FindControl("frmDate"), ASPxDateEdit)   
  Dim toDate As ASPxDateEdit = DirectCast(dgItem.Cells(3).FindControl("toDate"), ASPxDateEdit)  
                            Dim type As String = dgItem.Cells(4).Text
                            If type = "DATE" Then
                                frmDate.Text = ""
                                toDate.Text = ""
                            Else
                                exp.Text = ""
                                exp2.Text = ""
                            End If
                        End If
                    Next

                    Call Build_Temp_Table("ROW", "Like", txtFltValue.Text)
                    Dim counter As Integer = 0
                    If lstColumns.Items.Count > 0 Then
                        For i = 0 To lstColumns.Items.Count - 1
                            If lstColumns.Items(i).Value <> "All" Then
                                Dim isVarchar As Boolean = Check_Column_DataType(lstColumns.Items(i).Value)
                                If isVarchar Then
                                    If counter = 0 Then
                                        wCluase = "upper(" & lstColumns.Items(i).Value & ") LIKE upper('%" & txtFltValue.Text.ToString.Replace("'", "''") & "%')"
                                    Else
                                        wCluase = wCluase & " or " & "upper(" & lstColumns.Items(i).Value & ") LIKE upper('%" & txtFltValue.Text.ToString.Replace("'", "''") & "%')"
                                    End If
                                    counter = counter + 1
                                End If
                            End If
                        Next
                        counter = 0
                    End If
                    wrCluase = String.Empty
'wrCluase  = BuildWhereClause()
                    If Not String.IsNullOrEmpty(wrCluase) Then
                        If wCluase <> "" Then
                            wCluase = "( " & wCluase & " ) AND " & wrCluase
                        End If
                    End If
                    If wCluase IsNot Nothing Then
                        data_bind1(wCluase)
                    End If
                End If
            Else
                If lblColumnname.Text.ToString.Trim = "" Then
                    Call Clear_QuickSearch()
                    pnldgApply.Visible = False
                End If
                Call BindGrid()
            End If
            hdnLblValue.Value = ""
            lblColumnname.Text = ""
            txtFltValue.Text = ""
            ApplyLayout(Int32.Parse(ddlGroupBy.SelectedValue.ToString()))
            Apply_GroupFooter_Mode()
            Session(strSessID & "_gridState") = ASPxGridView1.SaveClientLayout()
            Call Apply_Sort("Y")
        Catch ex As Exception
            Call INSERT_SYS_LOG(RID.Value & " - " & "btnGo_Click:-", ex.Message)
        End Try
    End Sub
  Public Sub Update_QuickSearch1(ByVal strColName As String, ByVal strColValue As String)
         Try
             For Each dgItem As DataGridItem In dgQuickSearch.Items
                 Dim intRow As Integer = dgItem.ItemIndex
                 If intRow <> -1 Then
                     Dim strValues As String = String.Empty
                     Dim lblFname As Label = DirectCast(dgItem.Cells(2).FindControl("lblFName"), Label)
                     Dim ddlDGDrop As DropDownList = DirectCast(dgItem.Cells(2).FindControl("ddlDrop"), DropDownList)
                     Dim ddlDate As DropDownList = DirectCast(dgItem.Cells(2).FindControl("ddlDate"), DropDownList)
                     Dim exp As TextBox = DirectCast(dgItem.Cells(3).FindControl("expression"), TextBox)
                     Dim exp2 As TextBox = DirectCast(dgItem.Cells(3).FindControl("expression2"), TextBox)
  Dim frmDate As ASPxDateEdit = DirectCast(dgItem.Cells(3).FindControl("frmDate"), ASPxDateEdit)   
  Dim toDate As ASPxDateEdit = DirectCast(dgItem.Cells(3).FindControl("toDate"), ASPxDateEdit)  
                     Dim type As String = dgItem.Cells(4).Text
If lblFname.Text = strColName Then                   
                        If type = "DATE" Then
                            frmDate.Text = strColValue
                            ddlDate.SelectedItem.Value = "="
                        Else
                            exp.Text = strColValue
                            For ddli = 0 To ddlDGDrop.Items.Count - 1
                                If (ddlDGDrop.Items(ddli).Value = "contains") Then
                                    ddlDGDrop.SelectedIndex = ddli
                                    Exit For
                                End If
                            Next
                        End If
                        Exit For
                    End If
                End If
             Next
         Catch ex As Exception
             Call INSERT_SYS_LOG(RID.Value & " - " & "Update_QuickSearch1:-", ex.Message)
         End Try
    End Sub
Public Function Check_Column_DataType(ByVal strColName As String) As Boolean
        Dim isVarchar As Boolean = False
        Try
 For Each dgItem As DataGridItem In dgQuickSearch.Items
                Dim intRow As Integer = dgItem.ItemIndex
                If intRow <> -1 Then
                    Dim lblFname As Label = DirectCast(dgItem.Cells(2).FindControl("lblFName"), Label)
                    Dim type As String = dgItem.Cells(4).Text
                    If lblFname.Text = strColName Then
                        If type = "DATE" Then
                            Return False
                        Else
                            Return True
                        End If
                        Exit For
                    End If
                End If
            Next
        Catch ex As Exception
            Call INSERT_SYS_LOG(RID.Value & " - " & "Check_Column_DataType:-", ex.Message)
        End Try
        Return isVarchar
    End Function
 Public Sub Build_Temp_Table(ByVal strFname As String, ByVal strOpr As String, ByVal strValue As String)
        Try
            Dim tblTemp As New DataTable()
            Dim dcLabel As New DataColumn("FieldName", System.Type.[GetType]("System.String"))
            Dim dcOperator As New DataColumn("Operator", System.Type.[GetType]("System.String"))
            Dim dcDvalue As New DataColumn("Expression", System.Type.[GetType]("System.String"))

            tblTemp.Columns.Add(dcLabel)
            tblTemp.Columns.Add(dcOperator)
            tblTemp.Columns.Add(dcDvalue)

            tblTemp.Columns(0).ColumnName = "FieldName"
            tblTemp.Columns(1).ColumnName = "Operator"
            tblTemp.Columns(2).ColumnName = "Expression"

            Dim dr As DataRow
            dr = tblTemp.NewRow()

            dr("FieldName") = strFname
            dr("Operator") = strOpr
            dr("Expression") = strValue
            tblTemp.Rows.Add(dr)


            Dim dv As New DataView()
            dv = tblTemp.DefaultView

            tblTemp = dv.ToTable()

            If tblTemp.Rows.Count > 0 Then
                Session(strSessID & "filterGridData") = tblTemp
                pnldgApply.Visible = True
                dgApplyFilter.Visible = True
                dgApplyFilter.DataSource = tblTemp
                dgApplyFilter.DataBind()
            Else
                pnldgApply.Visible = False
            End If

        Catch ex As Exception
            Call INSERT_SYS_LOG(RID.Value & " - " & "Build_Temp_Table -", ex.Message)
        End Try
    End Sub
 Public Sub DeleteFilter_Item(ByVal s As Object, ByVal e As DataGridCommandEventArgs)
        Try
            Dim dt As New DataTable
            Dim dsResult As New DataSet

            Dim lblFname As String = String.Empty
            Dim lblOpr As String = String.Empty
            Dim lblValue As String = String.Empty

            If (Session(strSessID & "filterGridData") IsNot Nothing) Then

                dt = DirectCast(Session(strSessID & "filterGridData"), DataTable)
                lblFname = dt.Rows(e.Item.ItemIndex)("FieldName").ToString.Replace("""", String.Empty)
                For Each dgItem As DataGridItem In dgQuickSearch.Items
                    Dim intRow As Integer = dgItem.ItemIndex
                    If intRow <> -1 Then
                        Dim strValues As String = String.Empty
 Dim lblFLabel As Label = DirectCast(dgItem.Cells(1).FindControl("lblFLabel"), Label)
                        Dim lblFname1 As Label = DirectCast(dgItem.Cells(2).FindControl("lblFName"), Label)
                        Dim exp As TextBox = DirectCast(dgItem.Cells(3).FindControl("expression"), TextBox)
                        Dim exp2 As TextBox = DirectCast(dgItem.Cells(3).FindControl("expression2"), TextBox)
  Dim frmDate As ASPxDateEdit = DirectCast(dgItem.Cells(3).FindControl("frmDate"), ASPxDateEdit)   
  Dim toDate As ASPxDateEdit = DirectCast(dgItem.Cells(3).FindControl("toDate"), ASPxDateEdit)  
                        Dim type As String = dgItem.Cells(4).Text
                        If lblFLabel.Text = lblFname Then
                            If type = "DATE" Then
                                frmDate.Text = ""
                                toDate.Text = ""
                            Else
                                exp.Text = ""
                                exp2.Text = ""
                            End If
                            Exit For
                        End If
                    End If
                Next
                dt.Rows(e.Item.ItemIndex).Delete()
                dt.AcceptChanges()
                Session(strSessID & "filterGridData") = dt
                If dt.Rows.Count > 0 Then
                    Session(strSessID & "filterGridData") = dt
                    pnldgApply.Visible = True
                    dgApplyFilter.DataSource = dt
                    dgApplyFilter.DataBind()
                    Call BindGrid()
                Else
                    pnldgApply.Visible = False
 Dim wrClaue As String = BuildWhereClause()
                    If Not String.IsNullOrEmpty(wrClaue) Then
                        If wrClaue.Trim <> "" Then
                            data_bind1(wrClaue)
                        Else
                            Call BindGrid()
                        End If
                    Else
                        Call BindGrid()
                    End If
                End If
                ApplyLayout(Int32.Parse(ddlGroupBy.SelectedValue.ToString()))
                Apply_GroupFooter_Mode()
                Call Apply_Sort("Y")
                Session(strSessID & "_gridState") = ASPxGridView1.SaveClientLayout()
            End If
        Catch ex As Exception
            Call INSERT_SYS_LOG(RID.Value & " - " & "DeleteFilter_Item:-", ex.Message)
        End Try
    End Sub
Protected Sub btnFltApply_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFltApply.Click
        Try
            Call BindGrid()
            Call Apply_Sort("Y")
            Session(strSessID & "_gridState") = ASPxGridView1.SaveClientLayout()
ASPxPopupControl2.ShowOnPageLoad = False
      '      pnldgApply.Visible = False
        Catch ex As Exception
            Call INSERT_SYS_LOG(RID.Value & " - " & "btnFltApply_Click", ex.Message.ToString())
        End Try
    End Sub

    Protected Sub btnSortApply_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSortApply.Click
        Try
            Call BindGrid()
            Call Apply_Sort("Y")
  '          pnldgApply.Visible = False
            Session(strSessID & "_gridState") = ASPxGridView1.SaveClientLayout()
            ASPxPopupControl3.ShowOnPageLoad = False
        Catch ex As Exception
            Call INSERT_SYS_LOG(RID.Value & " - " & "btnSortApply_Click", ex.Message.ToString())
        End Try
    End Sub
    Public Function Check_UserType() As Boolean
        Dim isUser As Boolean = False
        Try
Dim s_m_level as integer
s_m_level=3
s_m_level=2
If (cint(Session("MAINTENANCE_LEVEL"))<=s_m_level) then
                        isUser = True
Else
                        isUser = False
End If
        Catch ex As Exception
            isUser = False
            Call INSERT_SYS_LOG(RID.Value & " - " & "Check_UserType", ex.Message.ToString())
        End Try
        Return isUser
    End Function
Protected Sub btnRefresh1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh1.Click
 Try
            Call BindGrid()
            ApplyLayout(Int32.Parse(ddlGroupBy.SelectedValue.ToString()))
            Apply_GroupFooter_Mode()
            Call Apply_Sort("Y")
        Catch ex As Exception
            Call INSERT_SYS_LOG(RID.Value & " - " & "btnRefresh1_Click:-", ex.Message)
        End Try
End Sub
 Public Sub Clear_QuickSearch()
        Try
            Dim dt As New DataTable
            Dim dsResult As New DataSet

            Dim lblFname As String = String.Empty
            Dim lblOpr As String = String.Empty
            Dim lblValue As String = String.Empty

            If (Session("filterGridData") IsNot Nothing) Then
                dt = DirectCast(Session("filterGridData"), DataTable)
                For Each dgItem As DataGridItem In dgApplyFilter.Items
                    Dim intRow As Integer = dgItem.ItemIndex
                    If intRow <> -1 Then
                        lblFname = dgItem.Cells(1).Text
                        lblOpr = dgItem.Cells(2).Text
                        lblValue = dgItem.Cells(3).Text

                        dt.Rows(intRow)("FIELD_LABEL") = lblFname
                        dt.Rows(intRow)("FIELD_OPERATOR") = lblOpr
                        dt.Rows(intRow)("DEFAULT_VALUE") = lblValue

                    End If
                Next

                For Each dgItem As DataGridItem In dgQuickSearch.Items
                    Dim intRow As Integer = dgItem.ItemIndex
                    If intRow <> -1 Then
                        Dim strValues As String = String.Empty
                        Dim lblFname1 As Label = DirectCast(dgItem.Cells(2).FindControl("lblFName"), Label)
                        Dim exp As TextBox = DirectCast(dgItem.Cells(3).FindControl("expression"), TextBox)
                        Dim frmDate As TextBox = DirectCast(dgItem.Cells(3).FindControl("frmDate"), TextBox)
                        Dim type As String = dgItem.Cells(4).Text
                        If lblFname1.Text = lblFname Then
                            If type = "DATE" Then
                                frmDate.Text = ""
                            Else
                                exp.Text = ""
                            End If
                            Exit For
                        End If
                    End If
                Next
            End If
        Catch ex As Exception
        End Try
    End Sub
Public Sub Fill_Alternative()
        Try
If Not Request.QueryString("Not_Alt_Report") Is Nothing Then
      If Not Request.QueryString.Get("Not_Alt_Report").ToString = "Y" Then
      Call Alternative_Rpt()
      End If
End If
            dbad.SelectCommand.Connection = Session("db_connection")
            Dim dsp49 As New DataSet()
            Dim sdrpi19 As Integer
            Dim sdrp19 As String

             sdrp19 = " SELECT DISTINCT ALT_REPORT_ID,ALTERNATIVE_TYPE,CASE WHEN ALTERNATIVE_TYPE IN ('GB','CH','CB') THEN LINE_NO||'$'||ALTERNATIVE_TYPE ELSE ALT_REPORT_ID||'$'||ALTERNATIVE_TYPE END AS OPTION_VALUE ,REPORT_HEADING AS OPTION_TEXT FROM SYS_REPORT_ALTERNATIVE  WHERE REPORT_ID = '" & RID.Value & "' AND ACTIVE_FLG='Y' "
            dbad.SelectCommand.CommandText = sdrp19
            dsp49.Clear()
            If dbad.SelectCommand.Connection.State = ConnectionState.Closed Then
                dbad.SelectCommand.Connection.Open()
            End If
            dbad.Fill(dsp49)
            If dbad.SelectCommand.Connection.State = ConnectionState.Open Then
                dbad.SelectCommand.Connection.Close()
            End If
            If dsp49.Tables.Count > 0 Then
                If (dsp49.Tables(0).Rows.Count > 0) Then
                    lblAlter.Visible = True
                    ddlAlter.Visible = True
                    ddlAlter.Items.Clear()
                    ddlAlter.Items.Add("")
                    For sdrpi19 = 0 To dsp49.Tables(0).Rows.Count - 1
                        ddlAlter.Items.Add("")
                        If Not (Equals(dsp49.Tables(0).Rows(sdrpi19)("OPTION_TEXT"), System.DBNull.Value)) Then
                            ddlAlter.Items(sdrpi19 + 1).Text = dsp49.Tables(0).Rows(sdrpi19)("OPTION_TEXT")
                        Else
                            ddlAlter.Items(sdrpi19 + 1).Text = ""
                        End If
                        If Not (Equals(dsp49.Tables(0).Rows(sdrpi19)("OPTION_VALUE"), System.DBNull.Value)) Then
                            ddlAlter.Items(sdrpi19 + 1).Value = dsp49.Tables(0).Rows(sdrpi19)("OPTION_VALUE")
                        Else
                            ddlAlter.Items(sdrpi19 + 1).Value = ""
                        End If
                    Next
                Else
                    lblAlter.Visible = False
                    ddlAlter.Visible = False
                End If
            Else
                lblAlter.Visible = False
                ddlAlter.Visible = False
            End If
        Catch ex As Exception
                    lblAlter.Visible = False
                    ddlAlter.Visible = False
            Call INSERT_SYS_LOG(RID.Value & " - " & "Fill_Alternative", ex.Message.ToString() & ex.StackTrace.ToString)
        End Try
    End Sub
Private Sub Alternative_Rpt()
Try
    Dim ds As New System.Data.DataSet
    ds.Clear()
    dbad.SelectCommand.Connection = Session("db_connection")
    If dbad.SelectCommand.Connection.State = ConnectionState.Closed Then
        dbad.SelectCommand.Connection.Open()
    End If
    dbad.SelectCommand.CommandText = "Select ALT_REPORT FROM SYS_REPORT_PERSONAL WHERE MID='" & RID.Value.ToString & "' AND CREATED_BY='" & Session("user_id").ToString & "' AND DEFAULT_RPT='Y'"
    dbad.Fill(ds)
    If dbad.SelectCommand.Connection.State = ConnectionState.Open Then
        dbad.SelectCommand.Connection.Close()
    End If
    If Not ds Is Nothing Then
        If ds.Tables(0).Rows.Count > 0 Then
            If Not (Equals(ds.Tables(0).Rows(0)(0), System.DBNull.Value)) Then
                Server.TransferRequest(ds.Tables(0).Rows(0)(0).ToString & ".ASPX?Not_Alt_Report=Y")
            End If
        End If
    End If
Catch ex As Exception
    Call INSERT_SYS_LOG(RID.Value & " - " & "Fill_Alternative", ex.Message.ToString() & ex.StackTrace.ToString )
End Try
End Sub
Public Function Return_Value(ByVal strSQL As String) As String
        Dim strValue As String = String.Empty
        Try
            strValue = String.Empty
            Dim dscb As New DataSet
            dbad.SelectCommand.Connection = Session("db_connection")
            If dbad.SelectCommand.Connection.State = ConnectionState.Closed Then
                dbad.SelectCommand.Connection.Open()
            End If
            dbad.SelectCommand.CommandText = strSQL
            dscb.Clear()
            dbad.Fill(dscb)
            If dbad.SelectCommand.Connection.State = ConnectionState.Open Then
                dbad.SelectCommand.Connection.Close()
            End If
            If Not dscb Is Nothing Then
                If dscb.Tables(0).Rows.Count > 0 Then
                    If Not (Equals(dscb.Tables(0).Rows(0)(0), System.DBNull.Value)) Then
                        strValue = dscb.Tables(0).Rows(0)(0).ToString()
                    Else
                        strValue = String.Empty
                    End If
                Else
                    strValue = String.Empty
                End If
            Else
                strValue = String.Empty
            End If
        Catch ex As Exception
            strValue = String.Empty
        End Try
        Return strValue
    End Function
 Public Sub Export_To()
        Try
            Call Remove_Colors(ASPxGridViewExporter1)
            Call Remove_Colors(ASPxGridViewExporter4)
            ASPxGridView1.SettingsText.Title = "Credit Card Payment/Refund Info"
            ASPxGridView1.Settings.ShowTitlePanel = True
            ASPxGridViewExporter1.Styles.Title.Font.Size = "10"
            ASPxGridViewExporter1.Styles.Title.Font.Name = "Arial"
             ASPxGridViewExporter1.Styles.Title.Font.Bold = True
            ASPxGridViewExporter1.Styles.Title.BackColor = Color.White
            ASPxGridViewExporter1.Styles.Title.ForeColor = Color.Black
            ASPxGridViewExporter1.Styles.Header.Font.Size = "10"
            ASPxGridViewExporter1.Styles.Header.Font.Name = "Arial"
            ASPxGridViewExporter1.Styles.Header.Font.Bold = True
            ASPxGridViewExporter1.Styles.Cell.Font.Size = "10"
            ASPxGridViewExporter1.Styles.Cell.Font.Name = "Arial"
            ASPxGridViewExporter1.Styles.Cell.Font.Bold = True
            ASPxGridViewExporter1.Styles.Footer.Font.Size = "10"
            ASPxGridViewExporter1.Styles.Footer.Font.Name = "Arial"
            ASPxGridViewExporter1.Styles.Footer.Font.Bold = True
            ASPxGridViewExporter1.Styles.GroupFooter.Font.Size = "10"
            ASPxGridViewExporter1.Styles.GroupFooter.Font.Name = "Arial"
            ASPxGridViewExporter1.Styles.GroupFooter.Font.Bold = True
            Dim ps As New PrintingSystem()
            Dim link As New DevExpress.Web.Export.GridViewLink(ASPxGridViewExporter1)
            Dim link3 As New DevExpress.Web.Export.GridViewLink(ASPxGridViewExporter4)
            Dim compositeLink As New DevExpress.XtraPrintingLinks.CompositeLink()
            compositeLink.PrintingSystem = ps
            If strType = "PDF" Or strType = "PRT" Then
            Dim leftColumn As String = "Pages: [Page # of Pages #]"
Dim middleColumn As String
     middleColumn = "User: " & Session("user_id").ToString()
            Dim rightColumn As String = "Date: [Date Printed]"
            Dim phf As PageHeaderFooter = TryCast(compositeLink.PageHeaderFooter, PageHeaderFooter)
            phf.Footer.Content.AddRange(New String() {leftColumn, middleColumn, rightColumn})
            phf.Footer.LineAlignment = BrickAlignment.Center
            End If
            compositeLink.Links.AddRange(New Object() {link3, link})
            compositeLink.PaperKind = System.Drawing.Printing.PaperKind.A4
 If strPF = "PL" Then
                 compositeLink.Landscape = True
             Else
                 compositeLink.Landscape = False
             End If
            compositeLink.Margins.Top = 10
            compositeLink.Margins.Bottom = 10
            compositeLink.Margins.Left = 10
            compositeLink.Margins.Right = 10
            compositeLink.CreateDocument()
            If strType = "PDF" Or strType = "PRT" Then
AddHandler compositeLink.CreateReportHeaderArea, AddressOf compositeLink_CreateMarginalHeaderArea
AddHandler compositeLink.CreateInnerPageHeaderArea, AddressOf compositeLink_CreatePageHeaderArea
compositeLink.PrintingSystem.Document.AutoFitToPagesWidth = 1
            End If
            Dim stream As New System.IO.MemoryStream()
            If strType = "PDF" Then
If ReportSavePath <> "" Then
compositeLink.ExportToPdf(ReportSavePath & "\" & RID.Value.ToString().Trim & ".pdf")
Else 
compositeLink.PrintingSystem.ExportToPdf(Stream)
WriteToResponse("REP_CC_PAYMENT_INFO", True, "pdf", stream)
End If
            ElseIf strType = "XLS" Then
If ReportSavePath <> "" Then
compositeLink.ExportToXlsx(ReportSavePath & "\" & RID.Value.ToString().Trim & ".xlsx")
Else
compositeLink.PrintingSystem.ExportToXlsx(Stream)
 WriteToResponse("REP_CC_PAYMENT_INFO", True, "xlsx", stream)
End If
            ElseIf strType = "PRT" Then
                compositeLink.PrintingSystem.ExportToPdf(stream)
                WriteToResponse("REP_CC_PAYMENT_INFO", False, "pdf", stream)
            End If
        Catch ex As Exception
            Call INSERT_SYS_LOG(RID.Value & " - " & "Export_To", ex.Message.ToString())
        End Try
    End Sub
Public Function Build_DateFilters() As DataTable
    Dim tbl As New DataTable()
    Try
        Dim dcFromLabel As New DataColumn("FromLabel", System.Type.[GetType]("System.String"))
        Dim dcFromDate As New DataColumn("FromDate", System.Type.[GetType]("System.String"))
        Dim dcToLabel As New DataColumn("ToLabel", System.Type.[GetType]("System.String"))
        Dim dcTodate As New DataColumn("ToDate", System.Type.[GetType]("System.String"))
        tbl.Columns.Add(dcFromLabel)
        tbl.Columns.Add(dcFromDate)
        tbl.Columns.Add(dcToLabel)
        tbl.Columns.Add(dcTodate)
        tbl.Columns(0).ColumnName = "FromLabel"
        tbl.Columns(1).ColumnName = "FromDate"
        tbl.Columns(2).ColumnName = "ToLabel"
        tbl.Columns(3).ColumnName = "ToDate"
        Dim dr As DataRow

        dr = tbl.NewRow()

        dr("FromLabel") = "From"
        If Not Session(strSessID & "_dateFrom") Is Nothing Then
            dr("FromDate") = Session(strSessID & "_dateFrom")
        Else
            dr("FromDate") = FQuery
        End If
        dr("ToLabel") = "To"
        If Not Session(strSessID & "_dateTo") Is Nothing Then
            dr("ToDate") = Session(strSessID & "_dateTo")
        Else
            dr("ToDate") = TQuery
        End If

        tbl.Rows.Add(dr)
    Catch ex As Exception
        tbl.Clear()
    End Try
    Return tbl
End Function
 Protected Sub btnExcel_Email_Click(sender As Object, e As EventArgs)
     Try
         If File.Exists(ReportSavePath & "\" & RID.Value.ToString.Trim & ".xlsx") Then
             File.Delete(ReportSavePath & "\" & RID.Value.ToString.Trim & ".xlsx")
         End If
 
FQuery = dateTo.Text
TQuery = dateFrom.Text
Dim dtDate As New DataTable
dtDate = Build_DateFilters()
If Not dtDate Is Nothing Then
    If dtDate.Rows.Count > 0 Then
        dataSource = dtDate.DefaultView
        dateGrid.DataSource = dataSource
        dateGrid.DataBind()
        dateGrid.Settings.ShowColumnHeaders = False
    End If
End If
         strType = "XLS"
         'Call data_bind()
         Call Export_To()
         'ASPxPopupControl1.PopupElementID = "btnExcel_Email"
         'ASPxPopupControl1.HeaderText = "EMail Report Excel"
        'ASPxPopupControl1.ContentUrl = "Email_Inbox2.aspx?RID=" & RID.Value & "&Type=" & strType
         'ASPxPopupControl1.ShowOnPageLoad = True
  'Response.Write("<script> window.showModalDialog('Email_Inbox2.aspx?rid=" & RID.Value & "&Type=" & strType & "',null,'status:no;dialogWidth:700px;dialogHeight:600px;dialogHide:true;help:no;scroll:yes' ); </script>")
       Page.ClientScript.RegisterStartupScript(Me.[GetType](), "OpenWindow", "window.open('Email_Inbox2.aspx?rid=" & RID.Value & "&Type=" & strType & "','_newtab');", True)
'Response.End()
     Catch ex As Exception
 Call INSERT_SYS_LOG(RID.Value & " - " & "btnExcel_Email", ex.Message.ToString())
     End Try
 End Sub
Protected Sub btnPDF_Email_Click(sender As Object, e As EventArgs) 
    Try
        If File.Exists(ReportSavePath & "\" & RID.Value.ToString.Trim & ".pdf") Then
            File.Delete(ReportSavePath & "\" & RID.Value.ToString.Trim & ".pdf")
        End If

FQuery = dateTo.Text
TQuery = dateFrom.Text
Dim dtDate As New DataTable
dtDate = Build_DateFilters()
If Not dtDate Is Nothing Then
    If dtDate.Rows.Count > 0 Then
        dataSource = dtDate.DefaultView
        dateGrid.DataSource = dataSource
        dateGrid.DataBind()
        dateGrid.Settings.ShowColumnHeaders = False
    End If
End If
        strType = "PDF"
        'Call data_bind()
        Call Export_To()
        'ASPxPopupControl1.PopupElementID = "btnPDF_Email"
        'ASPxPopupControl1.HeaderText = "EMail Report PDF"
        'ASPxPopupControl1.ContentUrl = "Email_Inbox2.aspx?RID=" & RID.Value & "&Type=" & strType
        'ASPxPopupControl1.ShowOnPageLoad = True
  'Response.Write("<script> window.showModalDialog('Email_Inbox2.aspx?rid=" & RID.Value & "&Type=" & strType & "',null,'status:no;dialogWidth:700px;dialogHeight:600px;dialogHide:true;help:no;scroll:yes' ); </script>")
Page.ClientScript.RegisterStartupScript(Me.[GetType](), "OpenWindow", "window.open('Email_Inbox2.aspx?rid=" & RID.Value & "&Type=" & strType & "','_newtab');", True)
 'Response.End()
    Catch ex As Exception

    End Try
End Sub
 Protected Sub ddlEmail_SelectedIndexChanged(sender As Object, e As EventArgs)
If ddlEmail.SelectedItem.Value = "PDF" Then
    Call btnPDF_Email_Click(sender, e)
Else
    Call btnExcel_Email_Click(sender, e)
End If
 End Sub
Public Sub Remove_Colors(ByVal exporter As ASPxGridViewExporter)
    Try
        exporter.Styles.Cell.BorderSides = BorderSide.All
        exporter.Styles.Cell.BackColor = Color.Transparent
        exporter.Styles.Header.BorderSides = BorderSide.All
        exporter.Styles.Header.BackColor = Color.Transparent
        exporter.Styles.Header.ForeColor = Color.Black
        exporter.Styles.GroupRow.BorderSides = BorderSide.All
        exporter.Styles.GroupRow.BackColor = Color.Transparent
        exporter.Styles.GroupFooter.BorderSides = BorderSide.All
        exporter.Styles.GroupFooter.BackColor = Color.Transparent
        exporter.Styles.Footer.BorderSides = BorderSide.All
        exporter.Styles.Footer.BackColor = Color.Transparent
        exporter.Styles.AlternatingRowCell.BorderSides = BorderSide.All
        exporter.Styles.AlternatingRowCell.BackColor = Color.Transparent
        exporter.Styles.GroupFooter.HorizontalAlign = HorizontalAlign.Right
        exporter.Styles.GroupFooter.VerticalAlign = VerticalAlign.Middle
        exporter.Styles.Footer.HorizontalAlign = HorizontalAlign.Right
        exporter.Styles.Footer.VerticalAlign = VerticalAlign.Middle
    Catch ex As Exception
        Call INSERT_SYS_LOG(RID.Value & " - " & "Remove_Colors_Print", ex.Message.ToString())
    End Try
End Sub
Private Shared Function HtmlToPlainText(ByVal html As String) As String
    Const tagWhiteSpace As String = "(>|$)(\W|\n|\r)+<"
    Const stripFormatting As String = "<[^>]*(>|$)"
    Const lineBreak As String = "<(br|BR)\s{0,1}\/{0,1}>"
    Dim lineBreakRegex = New Regex(lineBreak, RegexOptions.Multiline)
    Dim stripFormattingRegex = New Regex(stripFormatting, RegexOptions.Multiline)
    Dim tagWhiteSpaceRegex = New Regex(tagWhiteSpace, RegexOptions.Multiline)

    Dim text = html
    text = System.Web.HttpUtility.HtmlDecode(text)
    text = tagWhiteSpaceRegex.Replace(text, " <> ")
    text = lineBreakRegex.Replace(text, Environment.NewLine)
    text = stripFormattingRegex.Replace(text, String.Empty)
    Return text
End Function
 Protected Sub ASPxGridView1_BeforeColumnSortingGrouping(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewBeforeColumnGroupingSortingEventArgs) Handles ASPxGridView1.BeforeColumnSortingGrouping
        Try
            Dim sortColName As String = String.Empty
            Dim sortOrder As String = String.Empty
            Dim sortIndex As String = String.Empty

            Dim tblSo As New DataTable()
            Dim dcFname As New DataColumn("FieldName", System.Type.[GetType]("System.String"))
            Dim dcSortOrder As New DataColumn("SortOrder", System.Type.[GetType]("System.String"))
            Dim dcSortIndex As New DataColumn("SortIndex", System.Type.[GetType]("System.Int32"))
            tblSo.Columns.Add(dcFname)
            tblSo.Columns.Add(dcSortOrder)
            tblSo.Columns.Add(dcSortIndex)
            tblSo.Columns(0).ColumnName = "FieldName"
            tblSo.Columns(1).ColumnName = "SortOrder"
            tblSo.Columns(2).ColumnName = "SortIndex"
            Dim dr As DataRow
            If (ASPxGridView1.GetSortedColumns.Count <> 0) Then
                For i = 0 To ASPxGridView1.GetSortedColumns().Count - 1
                    If ASPxGridView1.GetSortedColumns(i).SortOrder = ColumnSortOrder.Ascending Then
                        sortOrder = "ASC"
                    Else
                        sortOrder = "DESC"
                    End If
                    dr = tblSo.NewRow()

                    dr("FieldName") = ASPxGridView1.GetSortedColumns(i).FieldName
                    dr("SortOrder") = sortOrder
                    dr("SortIndex") = ASPxGridView1.GetSortedColumns(i).SortIndex

                    tblSo.Rows.Add(dr)
                Next
            End If
            If tblSo.Rows.Count > 0 Then
                Session(strSessID & "_SODataCol") = tblSo
            End If
  Apply_GroupFooter_Mode()
        Catch ex As Exception
            Call INSERT_SYS_LOG(RID.Value & " - " & "ASPxGridView1_BeforeColumnSortingGrouping:-", ex.Message)
        End Try
    End Sub
Protected Sub UpdatePanel1_Unload(sender As Object, e As EventArgs)
Dim methodInfo As MethodInfo = GetType(ScriptManager).GetMethods(BindingFlags.NonPublic Or BindingFlags.Instance).Where(Function(i) i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First()
methodInfo.Invoke(ScriptManager.GetCurrent(Page), New Object() {TryCast(sender, UpdatePanel)})
End Sub
End Class
