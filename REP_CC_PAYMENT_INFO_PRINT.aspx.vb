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
Partial Class _REP_CC_PAYMENT_INFO_Print
    Inherits System.Web.UI.Page

    Public ds1 As New DataSet()
    Protected dbad As New OleDbDataAdapter
    Public dbadc As New OleDbCommand
    Public conn As New Dbconn
    Dim strQuery As String = String.Empty
    Dim dstTempData As New DataSet
    Dim dataSource As IList
  Dim strSessID As String
Dim strType As String
Dim strPF As String
Dim FQuery, TQuery As String
Dim ReportSavePath As String
 Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
If Not Request.QueryString("SPath") Is Nothing Then
    ReportSavePath = String.Empty
    ReportSavePath = Request.QueryString.Get("SPath")
Else
    ReportSavePath = ""
End If
  If Not Request.QueryString("Type") Is Nothing Then
                 strType = String.Empty
                 strType = Request.QueryString.Get("Type")
             Else
                 strType = ""
             End If
If Not Request.QueryString("PF") Is Nothing Then
                strPF = String.Empty
                strPF = Request.QueryString.Get("PF")
            Else
                strPF = "PL"
            End If
 If Session("user_id") <> "" Then
 strSessID = Session("user_id").ToString  & "_" & RID.Value 
 Dim ds3 As New DataTable
            Dim conn As New Dbconn
            dbad.SelectCommand = New OleDbCommand
If (Equals(Session("db_connection"), System.DBNull.Value)) Then
           Session("db_connection") = conn.getconnection()
End If
           '' dbad.SelectCommand.Connection = Session("db_connection")
 If Not Session(strSessID &"_QSData") Is Nothing Then
                ds3 = Session(strSessID &"_QSData")
                If Not ds3 Is Nothing Then
                    If ds3.Rows.Count > 0 Then
                        dataSource = ds3.DefaultView
                        sgrid.DataSource = dataSource
                        sgrid.DataBind()
  Else
                             dataSource = ds3.DefaultView
                             sgrid.DataSource = dataSource
                             sgrid.DataBind()
                             sgrid.Settings.ShowColumnHeaders = False
                    End If
  Else
                             dataSource = ds3.DefaultView
                             sgrid.DataSource = dataSource
                             sgrid.DataBind()
                             sgrid.Settings.ShowColumnHeaders = False
                End If
            End If
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
            Call data_bind()
Call Apply_Sort()
  If strType = "PDF" Or strType = "PRT" Then
           Call Export_To()
  else
  Call Export_To_Excel()
  End If
 ElseIf ReportSavePath <> "" Then  
    Call data_bind_new() 
    If strType = "PDF" Or strType = "PRT" Then  
        Call Export_To()    
    Else  
        Call Export_To_Excel()   
    End If    
 Else
Response.Redirect("login.aspx")
End If
        Catch ex As Exception
Call insert_sys_log(RID.Value & " - " & "ASPxGridView1_Init", ex.Message.ToString())
        End Try
    End Sub
 Public Sub data_bind()
        Try
dbad.SelectCommand.Connection = Session("db_connection")
        dbad.SelectCommand.CommandText = Session(strSessID &"_repQueryWc").ToString()
        ds1.Clear()
If dbad.SelectCommand.Connection.State = ConnectionState.Closed Then
dbad.SelectCommand.Connection.Open()
End If
        dbad.Fill(ds1)
If dbad.SelectCommand.Connection.State = ConnectionState.Open Then
dbad.SelectCommand.Connection.Close()
End If
        If (ds1.Tables(0).Rows.Count > 0) Then
            dataSource = ds1.Tables(0).DefaultView
            ASPxGridView1.LoadClientLayout(Session(strSessID &"_gridState").ToString())
            ASPxGridView1.DataSource = dataSource
            ASPxGridView1.DataBind()
        End If
        Catch ex As Exception
Call insert_sys_log(RID.Value & " - " & "ASPxGridView1_Init", ex.Message.ToString())
        End Try
    End Sub
Public Sub data_bind_new()
Try
Dim Query, str_dateTo, str_dateFrom As String
Dim dtDate As New DataTable
str_dateTo = now.date()
str_dateFrom = now.date.adddays(-2)
FQuery = str_dateFrom
TQuery = str_dateTo
dbad.SelectCommand = New OleDbCommand
dbad.SelectCommand.Connection = conn.getconnection()
Query = "SELECT DOC_TYPE, DOC_NO, AMOUNT, RESPONSE, to_CHAR(TRANS_DATE,'MM/DD/YYYY') TRANS_DATE, API_TOKEN, CUSTOMER_NO, NAME, STATUS, USER_ID, CREATED_DATE,CUSTTOKEN,PMTOKEN,TRANRESULTCODE,TRANREFNUM,MASKEDCC,CCTYPE,PAYBYTYPE FROM V_CC_PAYMENT_INFO WHERE TRUNC(TRANS_DATE) BETWEEN TO_DATE('[FROMDATE]','MM/DD/YYYY') AND TO_DATE('[TODATE]','MM/DD/YYYY')  order by Tranrefnum"
dbad.SelectCommand.CommandText = Query.Replace("[TODATE]", str_dateTo).Replace("[FROMDATE]", str_dateFrom)
dtDate = Build_DateFilters()
ds1.Clear()
If dbad.SelectCommand.Connection.State = ConnectionState.Closed Then
dbad.SelectCommand.Connection.Open()
End If
dbad.Fill(ds1)
If dbad.SelectCommand.Connection.State = ConnectionState.Open Then
dbad.SelectCommand.Connection.Close()
End If
If (ds1.Tables(0).Rows.Count > 0) Then
'dataSource = ds1.Tables(0).DefaultView
'ASPxGridView1.DataSource = dataSource
ASPxGridView1.DataSource = ds1.Tables(0).DefaultView
ASPxGridView1.DataBind()
If Not dtDate Is Nothing Then
    If dtDate.Rows.Count > 0 Then
        dataSource = dtDate.DefaultView
        dateGrid.DataSource = dataSource
        dateGrid.DataBind()
        dateGrid.Settings.ShowColumnHeaders = False
    End If
End If
End If
Catch ex As Exception
Call INSERT_SYS_LOG(RID.Value & " - " & "data_bind_New", ex.Message.ToString())
End Try
End Sub
Public Function Return_Value(ByVal strSQL As String) As String
        Dim strValue As String = String.Empty
        Try
            strValue = String.Empty
            Dim dscb As New DataSet
            dbad.SelectCommand = New OleDbCommand
            dbad.SelectCommand.Connection = conn.getconnection()
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
  Protected Sub ASPxGridViewExporter1_RenderBrick(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewExportRenderingEventArgs) Handles ASPxGridViewExporter1.RenderBrick
        Try
            Dim dataColumn As GridViewDataColumn = TryCast(e.Column, GridViewDataColumn)
If (E.RowType = GridViewRowType.Header) Then
e.Text = e.Text.Replace("<br>", " ").Replace("<br/>", " ").Replace("<br />", " ").Replace("</br>", " ").Replace("< /br>", " ").Replace("<BR>", " ")
End If
   If (e.RowType = GridViewRowType.Data) Then
                e.Text = HtmlToPlainText(e.Text)
            End If
   If (e.RowType = GridViewRowType.Group) Then
                e.Text = HtmlToPlainText(e.Text)
            End If
        Catch ex As Exception
Call insert_sys_log(RID.Value & " - " & "ASPxGridViewExporter1_Print_RenderBrick", ex.Message.ToString())
        End Try
    End Sub
Public Sub Export_To()
        Try
 Call Remove_Colors(ASPxGridViewExporter1)
 Remove_Colors(ASPxGridViewExporter3)
            ASPxGridViewExporter1.Styles.Title.Font.Size = "10"
            ASPxGridViewExporter1.Styles.Title.Font.Name = "Arial"
            ASPxGridViewExporter1.Styles.Title.BackColor = Color.White
            ASPxGridViewExporter1.Styles.Title.ForeColor = Color.Black
            ASPxGridViewExporter1.Styles.Header.Font.Size = "10"
            ASPxGridViewExporter1.Styles.Header.Font.Name = "Arial"
            ASPxGridViewExporter1.Styles.Cell.Font.Size = "10"
            ASPxGridViewExporter1.Styles.Cell.Font.Name = "Arial"
            ASPxGridViewExporter1.Styles.Footer.Font.Size = "10"
            ASPxGridViewExporter1.Styles.Footer.Font.Name = "Arial"
            ASPxGridViewExporter1.Styles.GroupFooter.Font.Size = "10"
            ASPxGridViewExporter1.Styles.GroupFooter.Font.Name = "Arial"
 ASPxGridViewExporter1.Styles.Footer.HorizontalAlign = HorizontalAlign.Right
 ASPxGridViewExporter1.Styles.Footer.VerticalAlign = VerticalAlign.Middle
 ASPxGridViewExporter1.Styles.GroupFooter.HorizontalAlign = HorizontalAlign.Right
 ASPxGridViewExporter1.Styles.GroupFooter.VerticalAlign = VerticalAlign.Middle
Dim ps As New PrintingSystem()
            Dim link As New DevExpress.Web.Export.GridViewLink(ASPxGridViewExporter1)
      Dim link3 As New DevExpress.Web.Export.GridViewLink(ASPxGridViewExporter3)
            Dim pcLink1 As PrintableComponentLink = New PrintableComponentLink()
            Dim linkMainReport As Link = New Link()
            AddHandler linkMainReport.CreateDetailArea, AddressOf linkMainReport_CreateDetailArea
            Dim compositeLink As New DevExpress.XtraPrintingLinks.CompositeLink()
            compositeLink.PrintingSystem = ps
            Dim leftColumn As String = "Pages: [Page # of Pages #]"
Dim middleColumn As String
 If Request.QueryString("SPath") Is Nothing Then 
     middleColumn = "User: " & Session("user_id").ToString()
 Else 
     middleColumn = "User: Auto"
 End If
            Dim rightColumn As String = "Date: [Date Printed]"
            Dim phf As PageHeaderFooter = TryCast(compositeLink.PageHeaderFooter, PageHeaderFooter)
            phf.Footer.Content.AddRange(New String() {leftColumn, middleColumn, rightColumn})
            phf.Footer.LineAlignment = BrickAlignment.Far
 compositeLink.Links.AddRange(New Object() {link})
            If strType = "PDF" Or strType = "PRT" Then
            compositeLink.PaperKind = System.Drawing.Printing.PaperKind.A4
            End If
 If strPF = "PL" Then
                 compositeLink.Landscape = True
             Else
                 compositeLink.Landscape = False
             End If
            compositeLink.Margins.Top = 10
            compositeLink.Margins.Bottom = 10
            compositeLink.Margins.Left = 10
            compositeLink.Margins.Right = 10
            AddHandler compositeLink.CreateReportHeaderArea, AddressOf compositeLink_CreateMarginalHeaderArea
            AddHandler compositeLink.CreateInnerPageHeaderArea, AddressOf compositeLink_CreatePageHeaderArea
            compositeLink.CreateDocument()
            If strType = "PDF" Or strType = "PRT" Then
                compositeLink.PrintingSystem.Document.AutoFitToPagesWidth = 1
            End If
            Dim stream As New System.IO.MemoryStream()
If strType = "PDF" Then
If ReportSavePath <> "" Then
compositeLink.ExportToPdf(ReportSavePath & "\" & Request.QueryString.Get("REPORT_NAME").ToString() & ".pdf")
Else 
compositeLink.PrintingSystem.ExportToPdf(Stream)
WriteToResponse("REP_CC_PAYMENT_INFO", True, "pdf", stream)
End If
           ElseIf strType = "XLS" Then
If ReportSavePath <> "" Then
compositeLink.ExportToXlsx(ReportSavePath & "\" & Request.QueryString.Get("REPORT_NAME").ToString() & ".xlsx")
Else
compositeLink.PrintingSystem.ExportToXlsx(Stream)
 WriteToResponse("REP_CC_PAYMENT_INFO", True, "xlsx", stream)
End If
           Else
               compositeLink.PrintingSystem.ExportToPdf(stream)
               WriteToResponse("REP_CC_PAYMENT_INFO", False, "pdf", stream)
           End If
        Catch ex As Exception
Call insert_sys_log(RID.Value & " - " & "Export_To_Print", ex.Message.ToString())
        End Try
    End Sub
 Public Sub Export_To_Excel()
        Try
            Call Remove_Colors(ASPxGridViewExporter1)
Dim ps As New PrintingSystem()
Dim header As New Link()
ps.Links.Add(header)
AddHandler header.CreateMarginalHeaderArea, AddressOf compositeLink_CreateMarginalHeaderArea
      AddHandler header.CreateInnerPageHeaderArea, AddressOf compositeLink_CreatePageHeaderArea
            Dim link As New PrintableComponentLink
           link.Component = Me.ASPxGridViewExporter1
            Dim compositeLink As New DevExpress.XtraPrintingLinks.CompositeLink(ps)
 compositeLink.Links.AddRange(New Object() {header, link})
            compositeLink.CreateDocument()
            Dim stream As New System.IO.MemoryStream()
            If strType = "XLS" Then
If ReportSavePath <> "" Then
    compositeLink.ExportToXlsx(ReportSavePath & "\" & Request.QueryString.Get("REPORT_NAME").ToString() & ".xlsx")
Else
    compositeLink.ExportToXlsx(Stream)
    WriteToResponse("REP_CC_PAYMENT_INFO", True, "xlsx", Stream)
End If
            ElseIf strType = "PRT" Then
                compositeLink.PrintingSystem.ExportToPdf(stream)
                WriteToResponse("REP_CC_PAYMENT_INFO", False, "pdf", stream)
            End If
        Catch ex As Exception
            Call INSERT_SYS_LOG(RID.Value & " - " & "Export_To_Excel", ex.Message.ToString())
        End Try
    End Sub
Protected Sub compositeLink_CreateMarginalHeaderArea(ByVal sender As Object, ByVal e As CreateAreaEventArgs)
Dim l As Link = TryCast(sender, Link)
        Dim tb As TextBrick = New TextBrick()
        tb.Text = "Credit Card Payment/Refund Info"
        tb.Font = New Font("Tahoma", 21, FontStyle.Bold)
 tb.Rect = New RectangleF(0, 0, e.Graph.PrintingSystem.PageBounds.Width - 150, 35 * CInt(Math.Ceiling(tb.Text.Length / 64)))
        tb.BorderWidth = 0
        tb.BorderColor = Color.Transparent
        tb.HorzAlignment = DevExpress.Utils.HorzAlignment.Center
e.Graph.DrawBrick(tb, New RectangleF(0, 0, e.Graph.PrintingSystem.PageBounds.Width - 150, 35 * CInt(Math.Ceiling(tb.Text.Length / 64))))
    End Sub
Protected Sub compositeLink_CreatePageHeaderArea(ByVal sender As Object, ByVal e As CreateAreaEventArgs)
Dim l As Link = TryCast(sender, Link)
        Dim tb As TextBrick = New TextBrick()
 Dim Str As String = String.Empty
 Str = "From "
 If Not Session(strSessID & "_dateFrom") Is Nothing Then
     Str = Str + Session(strSessID & "_dateFrom")
 Else
     Str = Str + FQuery
 End If
 Str = Str + " To "
 If Not Session(strSessID & "_dateTo") Is Nothing Then
     Str = Str + Session(strSessID & "_dateTo")
 Else
     Str = Str + TQuery
 End If
 tb.Text = Str
 tb.Text = Str
        tb.Font = New Font("Tahoma", 18, FontStyle.Underline Or FontStyle.Bold)
 tb.Rect = New RectangleF(0, 0, e.Graph.PrintingSystem.PageBounds.Width - 150, 30 * (CInt(Math.Ceiling(tb.Text.Length / 70)) + 1)) 
        tb.BorderWidth = 0
        tb.BackColor = Color.Transparent
        tb.HorzAlignment = DevExpress.Utils.HorzAlignment.Center
 e.Graph.DrawBrick(tb, New RectangleF(0, 0, e.Graph.PrintingSystem.PageBounds.Width - 150, 30 * (CInt(Math.Ceiling(tb.Text.Length / 70)) + 1)))
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
    End Sub
Protected Sub ASPxGridView1_SummaryDisplayText(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewSummaryDisplayTextEventArgs) Handles ASPxGridView1.SummaryDisplayText
    End Sub
 Private Sub linkMainReport_CreateDetailArea(ByVal sender As Object, ByVal e As CreateAreaEventArgs)
        Dim tb As TextBrick = New TextBrick()
        tb.Rect = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 20)
        tb.BackColor = Color.Transparent
        tb.BorderColor = Color.Transparent
        e.Graph.DrawBrick(tb)
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
 Public Sub Apply_Sort()
        Try
            Dim tblSo As New DataTable()
            Dim tblCSo As New DataTable()
            If (Session(strSessID & "_SOData") IsNot Nothing) Then
                tblSo = DirectCast(Session(strSessID & "_SOData"), DataTable)
            End If
            If (Session(strSessID & "_SODataCol") IsNot Nothing) Then
                tblCSo = DirectCast(Session(strSessID & "_SODataCol"), DataTable)
            End If
            Try
                If Not tblCSo Is Nothing Then
                    If tblCSo.Rows.Count > 0 Then
                        ApplyReportSort(tblCSo)
                    Else
                        ApplyReportSort(tblSo)
                    End If
                Else
                    ApplyReportSort(tblSo)
                End If
            Catch ex As Exception
                ApplyReportSort(tblSo)
            End Try
        Catch ex As Exception
            Call INSERT_SYS_LOG(RID.Value & " - " & "Apply_Sort", ex.Message.ToString())
        End Try
    End Sub
Public Sub ApplyReportSort(ByVal tblSo As DataTable)
        Try
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
            Call INSERT_SYS_LOG(RID.Value & " - " & "ApplyReportSort", ex.Message.ToString())
        End Try
    End Sub
  Private Shared Function HtmlToPlainText(ByVal html As String) As String
        Const tagWhiteSpace As String = "(>|$)(\W|\n|\r)+<"
        'matches one or more (white space or line breaks) between '>' and '<'
        Const stripFormatting As String = "<[^>]*(>|$)"
        'match any character between '<' and '>', even when end tag is missing
        Const lineBreak As String = "<(br|BR)\s{0,1}\/{0,1}>"
        'matches: <br>,<br/>,<br />,<BR>,<BR/>,<BR />
        Dim lineBreakRegex = New Regex(lineBreak, RegexOptions.Multiline)
        Dim stripFormattingRegex = New Regex(stripFormatting, RegexOptions.Multiline)
        Dim tagWhiteSpaceRegex = New Regex(tagWhiteSpace, RegexOptions.Multiline)

        Dim text = html
        'Decode html specific characters
        text = System.Web.HttpUtility.HtmlDecode(text)

        'Remove tag whitespace/line breaks
        text = tagWhiteSpaceRegex.Replace(text, "><")
        'Replace <br /> with line breaks
        text = lineBreakRegex.Replace(text, Environment.NewLine)
        'Strip formatting
        text = stripFormattingRegex.Replace(text, String.Empty)

        Return text
    End Function
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
End Class
